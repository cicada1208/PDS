using Lib;
using Models;
using Params;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfLib;

namespace ViewModels
{
    public class PdsNoteEditViewModel : BaseViewModel<PdsNoteEditViewModel>
    {
        private Pds_note _note;
        /// <summary>
        /// 記錄編輯儲存
        /// </summary>
        public Pds_note Note
        {
            get => _note;
            set
            {
                Set(ref _note, value);
                SetMode(_note);
            }
        }

        private ObservableCollection<Rec_code> _typeList;
        /// <summary>
        /// 通報類別清單
        /// </summary>
        public ObservableCollection<Rec_code> TypeList
        {
            get
            {
                if (_typeList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Rec_code>>>(
                    RouteParam.Service(),
                    RouteParam.Rec_code.QueryRec_code,
                    new Rec_code { rec_code_model = "pds_note_type", rec_code_group = "pds_note_type", rec_code_st = "1" },
                    new { option = 1 }); // 1 依參數自動組建
                    _typeList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _typeList;
            }
            set => Set(ref _typeList, value);
        }

        private ObservableCollection<Rec_code> _stList;
        /// <summary>
        /// 狀態清單
        /// </summary>
        public ObservableCollection<Rec_code> StList
        {
            get
            {
                if (_stList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Rec_code>>>(
                    RouteParam.Service(),
                    RouteParam.Rec_code.QueryRec_code,
                    new Rec_code { rec_code_model = "pds_note_st", rec_code_group = "pds_note_st", rec_code_st = "1" },
                    new { option = 1 }); // 1 依參數自動組建
                    _stList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _stList;
            }
            set => Set(ref _stList, value);
        }

        private ObservableCollection<Mi_micbcode> _orderList;
        /// <summary>
        /// 處方明細清單
        /// </summary>
        public ObservableCollection<Mi_micbcode> OrderList
        {
            get => _orderList;
            set => Set(ref _orderList, value);
        }

        private Mi_micbcode _selectedOrder;
        /// <summary>
        /// 處方明細選取
        /// </summary>
        public Mi_micbcode SelectedOrder
        {
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value);
        }

        private Visibility _lstVisibility = Visibility.Collapsed;
        /// <summary>
        /// 處方明細：Visibility
        /// </summary>
        public Visibility LstVisibility
        {
            get => _lstVisibility;
            set => Set(ref _lstVisibility, value);
        }


        public void SetNote(Pds_note pds_note)
        {
            var result = ApiUtil.HttpClientEx<ApiResult<List<Pds_note>>>(
            RouteParam.Service(),
            RouteParam.Pds_note.QueryPds_note,
            pds_note,
            new { option = 2 } // 2: 依PK
            ).Data.FirstOrDefault();
            result.EditMode = pds_note.EditMode;
            Note = result;
        }

        private void SetMode(Pds_note pds_note)
        {
            switch (pds_note?.EditMode)
            {
                case Pds_noteParam.EditMode.UDLST:
                    LstVisibility = Visibility.Visible;
                    var orderListResult = ApiUtil.HttpClientEx<ApiResult<List<Mi_micbcode>>>(
                        RouteParam.Service(),
                        RouteParam.Mi_micbcode.QueryMi_micbcode,
                        new Mi_micbcode
                        {
                            icbcode_send_dt = pds_note.pds_note_send_dt,
                            icbcode_ipd_no = pds_note.pds_note_ipd_no
                        },
                        new { option = 5 }); // 5: 依配藥單條碼(傳送日期、住院序號)，查詢處方明細及核對藥袋的狀態
                    OrderList = new ObservableCollection<Mi_micbcode>(orderListResult.Data);
                    break;
                case Pds_noteParam.EditMode.FSTLST:
                    LstVisibility = Visibility.Visible;
                    var icfResult = ApiUtil.HttpClientEx<ApiResult<List<Mi_micbcode>>>(
                           RouteParam.Service(),
                           RouteParam.Mi_micbcode.QueryMi_micbcode,
                           new Mi_micbcode
                           {
                               icfcode_prt_dt = pds_note.pds_note_lst_code.SubStr(0, 7).ToInt(),
                               icfcode_id = pds_note.pds_note_lst_code.SubStr(7, 1),
                               icfcode_pill_no = pds_note.pds_note_lst_code.SubStr(8, 4).ToNullableShort()
            },
                           new { option = 9 }); // 9: 依首日量配藥單條碼(列印日期、領藥號)，查詢處方明細及調劑藥袋的狀態
                    OrderList = new ObservableCollection<Mi_micbcode>(icfResult.Data);
                    break;
                case Pds_noteParam.EditMode.NONE:
                    LstVisibility = Visibility.Collapsed;
                    OrderList = null;
                    break;
                default:
                    LstVisibility = Visibility.Collapsed;
                    OrderList = null;
                    break;
            }
        }

        private DelegateCommand<TextBox> _selectOrderCommand;
        public DelegateCommand<TextBox> SelectOrderCommand =>
            _selectOrderCommand ?? (_selectOrderCommand = new DelegateCommand<TextBox>
            (OnSelectOrder));
        /// <summary>
        /// 選取處方
        /// </summary>
        private void OnSelectOrder(TextBox textBox)
        {
            string text = SelectedOrder?.order_info.Replace(System.Environment.NewLine, " ");
            Util.Ctrl.InsertTextAtCaret(textBox, text);
        }

        private DelegateCommand _okCommand;
        public DelegateCommand OKCommand =>
            _okCommand ?? (_okCommand = new DelegateCommand
            (OnOK, () => Validate().IsValid));
        private void OnOK()
        {
            Note.pds_note_md_man = LoginViewModel.LoginUser.UserId;
            Note.pds_note_md_name = LoginViewModel.LoginUser.UserName;
            Note.pds_note_md_pc = HostUtil.GetHostNameAndAddress();
            Note.pds_note_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr();

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_note>>(
            RouteParam.Service(),
            RouteParam.Pds_note.SavePds_note,
            Note);

            if (!saveResult.Succ)
                Util.Media.MsgSpeech(saveResult.Msg);
            else
                DialogResult = true;
        }

    }
}
