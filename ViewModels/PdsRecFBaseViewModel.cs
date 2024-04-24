using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfLib;

namespace ViewModels
{
    public abstract class PdsRecFBaseViewModel<T> : BaseViewModel<T> where T : class
    {
        private string _FLSToptype;
        /// <summary>
        /// FLST作業類型
        /// </summary>
        public string FLSToptype
        {
            get
            {
                if (_FLSToptype.IsNullOrWhiteSpace())
                {
                    if (typeof(T) == typeof(PdsRecFAdjustViewModel))
                        _FLSToptype = Pds_recParam.Rec_op_type.FALST;
                    else if (typeof(T) == typeof(PdsRecFCheckViewModel))
                        _FLSToptype = Pds_recParam.Rec_op_type.FCLST;
                    else if (typeof(T) == typeof(PdsRecFReleaseViewModel))
                        _FLSToptype = Pds_recParam.Rec_op_type.FRLST;
                    else
                        throw new Exception(MsgParam.PdsRecNoOpType);
                }
                return _FLSToptype;
            }
        }

        private string _FBAGoptype;
        /// <summary>
        /// FBAG作業類型
        /// </summary>
        public string FBAGoptype
        {
            get
            {
                if (_FBAGoptype.IsNullOrWhiteSpace())
                {
                    if (typeof(T) == typeof(PdsRecFAdjustViewModel))
                        _FBAGoptype = Pds_recParam.Rec_op_type.FABAG;
                    else if (typeof(T) == typeof(PdsRecFCheckViewModel))
                        _FBAGoptype = Pds_recParam.Rec_op_type.FCBAG;
                    else if (typeof(T) == typeof(PdsRecFReleaseViewModel))
                        _FBAGoptype = Pds_recParam.Rec_op_type.FRBAG;
                    else
                        throw new Exception(MsgParam.PdsRecNoOpType);
                }
                return _FBAGoptype;
            }
        }

        private string _OpTitle;
        /// <summary>
        /// 作業標題
        /// </summary>
        public string OpTitle
        {
            get
            {
                if (_OpTitle.IsNullOrWhiteSpace())
                {
                    if (typeof(T) == typeof(PdsRecFAdjustViewModel))
                        _OpTitle = "調劑";
                    else if (typeof(T) == typeof(PdsRecFCheckViewModel))
                        _OpTitle = "核對";
                    else if (typeof(T) == typeof(PdsRecFReleaseViewModel))
                        _OpTitle = "發藥";
                    else
                        throw new Exception(MsgParam.PdsRecNoOpType);
                }
                return _OpTitle;
            }
        }

        private Pds_rec _rec;
        /// <summary>
        /// 首日量記錄
        /// </summary>
        public Pds_rec Rec
        {
            get => _rec ?? (_rec = new Pds_rec());
            set => Set(ref _rec, value);
        }

        private PdsPatInfo _pdsPatInfo;
        /// <summary>
        ///病人資訊
        /// </summary>
        public PdsPatInfo PdsPatInfo
        {
            get => _pdsPatInfo ?? (_pdsPatInfo = new PdsPatInfo());
            set => Set(ref _pdsPatInfo, value);
        }

        protected PdsMedInfo _pdsMedInfo;
        /// <summary>
        ///藥品資訊
        /// </summary>
        public abstract PdsMedInfo PdsMedInfo { get; set; }

        private ObservableCollection<Mi_micbcode> _orderList;
        /// <summary>
        /// 處方明細清單
        /// </summary>
        public ObservableCollection<Mi_micbcode> OrderList
        {
            get => _orderList;
            set
            {
                Set(ref _orderList, value);
                if (_orderList != null)
                {
                    AtcVisibility = _orderList.Where(o => !o.atc_code_prefix5.IsNullOrWhiteSpace()).Count() > 0 ?
                         Visibility.Visible : Visibility.Collapsed;
                }
                else
                    AtcVisibility = Visibility.Collapsed;
            }
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

        private ObservableCollection<Mch_msen> _msenList;
        /// <summary>
        /// 用藥過敏暨不良反應清單
        /// </summary>
        public ObservableCollection<Mch_msen> MsenList
        {
            get => _msenList;
            set => Set(ref _msenList, value);
        }

        private ObservableCollection<Mr_lstud> _lstudList;
        /// <summary>
        /// 交互作用
        /// </summary>
        public ObservableCollection<Mr_lstud> LstudList
        {
            get => _lstudList;
            set
            {
                Set(ref _lstudList, value);
                LstudVisibility = (_lstudList != null && _lstudList.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _lstudVisibility = Visibility.Collapsed;
        /// <summary>
        /// 交互作用：Visibility
        /// </summary>
        public Visibility LstudVisibility
        {
            get => _lstudVisibility;
            set => Set(ref _lstudVisibility, value);
        }

        private Visibility _atcVisibility = Visibility.Collapsed;
        /// <summary>
        /// 同藥理：Visibility
        /// </summary>
        public Visibility AtcVisibility
        {
            get => _atcVisibility;
            set => Set(ref _atcVisibility, value);
        }

        private string _lstCode;
        /// <summary>
        /// 配藥單條碼
        /// </summary>
        public string LstCode
        {
            get => _lstCode;
            set => Set(ref _lstCode, value?.Trim());
        }

        private bool _lstCodeFocused = true;
        /// <summary>
        /// 配藥單條碼：Focused
        /// </summary>
        public bool LstCodeFocused
        {
            get => _lstCodeFocused;
            set => Set(ref _lstCodeFocused, value);
        }

        private bool _lstCodeEnabled = true;
        /// <summary>
        /// 配藥單條碼：Enabled
        /// </summary>
        public bool LstCodeEnabled
        {
            get => _lstCodeEnabled;
            set => Set(ref _lstCodeEnabled, value);
        }

        private string _iPillNo;
        /// <summary>
        /// 領藥號
        /// </summary>
        public string IPillNo
        {
            get => _iPillNo;
            set => Set(ref _iPillNo, value?.Trim());
        }

        private string _bagCode;
        /// <summary>
        /// 藥袋條碼
        /// </summary>
        public string BagCode
        {
            get => _bagCode;
            set => Set(ref _bagCode, value?.Trim());
        }

        private bool _bagCodeFocused;
        /// <summary>
        /// 藥袋條碼：Focused
        /// </summary>
        public bool BagCodeFocused
        {
            get => _bagCodeFocused;
            set => Set(ref _bagCodeFocused, value);
        }

        private bool _bagCodeEnabled;
        /// <summary>
        /// 藥袋條碼：Enabled
        /// </summary>
        public bool BagCodeEnabled
        {
            get => _bagCodeEnabled;
            set => Set(ref _bagCodeEnabled, value);
        }

        private bool _LSTCEnabled;
        /// <summary>
        /// 整張取消：Enabled
        /// </summary>
        public bool LSTCEnabled
        {
            get => _LSTCEnabled;
            set => Set(ref _LSTCEnabled, value);
        }

        private bool _BAGCEnabled;
        /// <summary>
        /// 單筆取消：Enabled
        /// </summary>
        public bool BAGCEnabled
        {
            get => _BAGCEnabled;
            set => Set(ref _BAGCEnabled, value);
        }

        private ObservableCollection<Rec_code> _reasonList;
        /// <summary>
        /// 狀態原因設定檔
        /// </summary>
        public ObservableCollection<Rec_code> ReasonList
        {
            get
            {
                if (_reasonList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Rec_code>>>(
                    RouteParam.Service(),
                    RouteParam.Rec_code.QueryRec_code,
                    new Rec_code { rec_code_model = "pds_rec_reason", rec_code_st = "1" },
                    new { option = 1 }); // 1 依參數自動組建
                    _reasonList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _reasonList;
            }
            set => Set(ref _reasonList, value);
        }

        private ObservableCollection<Rec_code> _stList;
        /// <summary>
        /// 狀態設定檔
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
                    new Rec_code { rec_code_model = "pds_rec_st", rec_code_group = "pds_rec_st", rec_code_st = "1" },
                    new { option = 1 }); // 1 依參數自動組建
                    _stList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _stList;
            }
            set => Set(ref _stList, value);
        }


        /// <summary>
        /// 顯示取消畫面
        /// </summary>
        public Func<Pds_recParam.CancelMode, PdsRecCancelViewModel> ShowPdsRecCancelWindow;

        /// <summary>
        /// 顯示同藥理畫面
        /// </summary>
        public Action<List<Mi_micbcode>> ShowAtcCodeGroupWindow;

        /// <summary>
        /// 顯示交互作用畫面
        /// </summary>
        public Action<ObservableCollection<Mr_lstud>> ShowLstudWindow;

        /// <summary>
        /// 顯示通報畫面
        /// </summary>
        public Func<Pds_note, bool> ShowPdsNoteEditWindow;

        private DelegateCommand _lstCodeCommand;
        public DelegateCommand LstCodeCommand =>
            _lstCodeCommand ?? (_lstCodeCommand = new DelegateCommand
            (OnLstCode));
        /// <summary>
        /// 刷配藥單條碼
        /// </summary>
        protected abstract void OnLstCode();

        private DelegateCommand _bagCodeCommand;
        public DelegateCommand BagCodeCommand =>
            _bagCodeCommand ?? (_bagCodeCommand = new DelegateCommand
            (OnBagCode));
        /// <summary>
        /// 刷藥袋條碼
        /// </summary>
        protected abstract void OnBagCode();

        private DelegateCommand _BAGCCommand;
        public DelegateCommand BAGCCommand =>
            _BAGCCommand ?? (_BAGCCommand = new DelegateCommand
            (OnBAGC));
        /// <summary>
        /// 單筆取消
        /// </summary>
        protected abstract void OnBAGC();

        private DelegateCommand _LSTCCommand;
        public DelegateCommand LSTCCommand =>
            _LSTCCommand ?? (_LSTCCommand = new DelegateCommand
            (OnLSTC));
        /// <summary>
        /// 整張取消
        /// </summary>
        protected abstract void OnLSTC();

        /// <summary>
        /// 整張重新
        /// </summary>
        protected MessageBoxResult LSTR(bool checkComplete = false)
        {
            string msg = string.Empty;

            SetRec("");

            var LSTRResult = Util.Media.CheckMsgSpeech(
                checkComplete ? $"已完成{OpTitle}，是否重新{OpTitle}?" : $"是否重新{OpTitle}?");
            if (LSTRResult != MessageBoxResult.OK) goto exit;

            string st = Pds_recParam.Rec_st.C;
            SetRec(FLSToptype, Pds_recParam.Recd_op_type.LSTR,
                null,
                LstCode, "", "",
                st, Pds_recParam.Rec_reason.C09);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 9 }); // 9: 首日量調劑/核對/發藥作業，整張取消/整張重新

            if (!saveResult.Succ)
            {
                msg = saveResult.Msg;
                LSTRResult = MessageBoxResult.Cancel;
                goto exit;
            }

            SetOrderComplete("", st, true);

        exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);

            return LSTRResult;
        }

        private DelegateCommand _atcCommand;
        public DelegateCommand AtcCommand =>
            _atcCommand ?? (_atcCommand = new DelegateCommand
            (OnAtc));
        /// <summary>
        /// 同藥理
        /// </summary>
        protected void OnAtc()
        {
            ShowAtcCodeGroupWindow?.Invoke(OrderList.OrderByDescending(o => o.atc_code_prefix5).ToList());
        }

        private DelegateCommand _lstudCommand;
        public DelegateCommand LstudCommand =>
            _lstudCommand ?? (_lstudCommand = new DelegateCommand
            (OnLstud));
        /// <summary>
        /// 交互作用
        /// </summary>
        protected void OnLstud()
        {
            ShowLstudWindow?.Invoke(LstudList);
        }

        private DelegateCommand _noticeCommand;
        public DelegateCommand NoticeCommand =>
            _noticeCommand ?? (_noticeCommand = new DelegateCommand
            (OnNotice, () => !PdsPatInfo.ipd_no.IsNullOrWhiteSpace()));
        /// <summary>
        /// 通報(配藥單)
        /// </summary>
        protected void OnNotice()
        {
            int prt_dt = LstCode.SubStr(0, 7).ToInt();

            ShowPdsNoteEditWindow?.Invoke(new Pds_note()
            {
                EditMode = Pds_noteParam.EditMode.FSTLST,
                pds_note_op = Pds_noteParam.Op.FST,
                pds_note_lst_code = LstCode,
                pds_note_send_dt = prt_dt,
                pds_note_ipd_no = PdsPatInfo.ipd_no,
                pds_note_pat_no = PdsPatInfo.pat_no,
                pds_note_bed = PdsPatInfo.bed,
                pds_note_clinical = PdsPatInfo.bed_unit,
                pds_note_st = Pds_noteParam.St.U
            });
        }

        /// <summary>
        /// 取得狀態原因名稱
        /// </summary>
        protected string GetReasonName(string shortCode) =>
            ReasonList.FirstOrDefault(c => c.rec_code_short == shortCode)?.rec_code_name ?? string.Empty;

        /// <summary>
        /// 取得狀態名稱
        /// </summary>
        protected string GetStName(string shortCode) =>
            StList.FirstOrDefault(c => c.rec_code_short == shortCode)?.rec_code_name ?? string.Empty;

        /// <summary>
        /// 確認配藥單是否完成
        /// </summary>
        protected abstract void CheckLstComplete();

        /// <summary>
        /// 設定處方明細是否完成(反黑/反灰)
        /// </summary>
        protected void SetOrderComplete(string icbcode_code, string rec_st, bool setAll = false)
        {
            if (!setAll)
            {
                var cbag = OrderList.Where(o => o.icbcode_code == icbcode_code).FirstOrDefault();
                if (cbag != null) cbag.pds_rec_st = rec_st; // 反黑/反灰
            }
            else
            {
                HashSet<string> noNeedMedType = new HashSet<string>();
                if (typeof(T) == typeof(PdsRecFAdjustViewModel))
                    noNeedMedType = new HashSet<string> { Mi_micbcodeParam.Med_type.FOURS, Mi_micbcodeParam.Med_type.S, Mi_micbcodeParam.Med_type.V };
                else if (typeof(T) == typeof(PdsRecFCheckViewModel))
                    noNeedMedType = new HashSet<string> { Mi_micbcodeParam.Med_type.S, Mi_micbcodeParam.Med_type.V };
                else if (typeof(T) == typeof(PdsRecFReleaseViewModel))
                    noNeedMedType = new HashSet<string> { Mi_micbcodeParam.Med_type.V };

                foreach (var o in OrderList)
                {
                    if (noNeedMedType.Contains(o.icbcode_med_type))
                        continue;
                    o.pds_rec_st = rec_st;
                }
            }
        }

        /// <summary>
        /// 配藥單管制藥訊息
        /// </summary>
        /// <returns>有訊息代表該配藥單不執行</returns>
        protected string SOrderMsg(ObservableCollection<Mi_micbcode> orderList)
        {
            string msg = string.Empty;
            HashSet<string> noNeedMedType = new HashSet<string>();
            if (typeof(T) == typeof(PdsRecFAdjustViewModel))
                noNeedMedType = new HashSet<string> { Mi_micbcodeParam.Med_type.FOURS, Mi_micbcodeParam.Med_type.S };
            else if (typeof(T) == typeof(PdsRecFCheckViewModel))
                // 2024.01.24 因藥局人力不足暫將發藥併入首日量核對作業
                //noNeedMedType = new HashSet<string> { Mi_micbcodeParam.Med_type.S };
                return msg;
            else if (typeof(T) == typeof(PdsRecFReleaseViewModel))
                return msg;

            msg = orderList.Where(o => !noNeedMedType.Contains(o.icbcode_med_type)).Count() > 0 ?
            string.Empty : MsgParam.PdsRecFLSTSOrder;

            return msg;
        }

        /// <summary>
        /// 刷配藥單條碼 Reset
        /// </summary>
        protected void LstCodeReset(bool lstCodeEnabled = false, bool lstCodeFocused = false)
        {
            LstCode = string.Empty;
            LstCodeEnabled = lstCodeEnabled;
            LstCodeFocused = lstCodeFocused;
            IPillNo = string.Empty;
            LSTCEnabled = false;
            PdsPatInfo = null;
            OrderList = null;
            MsenList = null;
            LstudList = null;
        }

        /// <summary>
        /// 刷藥袋條碼 Reset
        /// </summary>
        protected virtual void BagCodeReset(bool bagCodeEnabled = false, bool bagCodeFocused = false)
        {
            BagCode = string.Empty;
            BagCodeEnabled = bagCodeEnabled;
            BagCodeFocused = bagCodeFocused;
            BAGCEnabled = false;
            PdsMedInfo = null;
        }

        /// <summary>
        /// 設定 Rec
        /// </summary>
        /// <param name="rec_op_type">空白代表清空 Rec</param>
        protected void SetRec(string rec_op_type, string recd_op_type = "",
           Mi_micbcode icbcode = null,
           string code = "", string mst_id = "", string pack = "",
           string st = "", string reason = "", string reason_oth = "",
           string md_qty = "", string md_way1 = "",
           string err_mst_id = "", string err_qty = "",
           string err_uqty1 = "", string err_uqty2 = "", string err_expdt = "")
        {
            if (rec_op_type == "")
            {
                Rec = null;
                return;
            }

            Rec.pds_rec_op_type = rec_op_type;

            string pds_recd_op_dtm = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            if (string.IsNullOrWhiteSpace(Rec.pds_rec_op_dtm_begin))
                Rec.pds_rec_op_dtm_begin = pds_recd_op_dtm;
            Rec.pds_rec_op_dtm_end = pds_recd_op_dtm;

            switch (Rec.pds_rec_op_type)
            {
                case Pds_recParam.Rec_op_type.FALST:
                case Pds_recParam.Rec_op_type.FCLST:
                case Pds_recParam.Rec_op_type.FRLST:
                    Rec.pds_rec_bag_code = string.Empty;
                    Rec.pds_rec_lst_code = LstCode;
                    Rec.pds_rec_send_dt = Rec.pds_rec_lst_code.SubStr(0, 7).ToInt();
                    Rec.pds_rec_ipd_no = PdsPatInfo.ipd_no;
                    Rec.pds_rec_pat_no = PdsPatInfo.pat_no;
                    break;
                default: // FABAG、FCBAG、FRBAG
                    Rec.pds_rec_bag_code = icbcode.icbcode_code;
                    Rec.pds_rec_lst_code = icbcode.icfcode_prt_dt.NullableToStr().PadLeft(7, '0') + icbcode.icfcode_id.PadLeft(1, ' ') + icbcode.icfcode_pill_no.NullableToStr().PadLeft(4, '0');
                    Rec.pds_rec_send_dt = icbcode.icfcode_prt_dt;
                    Rec.pds_rec_ipd_no = icbcode.icbcode_ipd_no;
                    Rec.pds_rec_pat_no = icbcode.icbcode_pat_no;
                    break;
            }

            Rec.pds_rec_bed = PdsPatInfo.bed;
            Rec.pds_rec_clinical = PdsPatInfo.bed_unit;
            Rec.pds_rec_st = st;
            Rec.pds_rec_reason = reason;
            Rec.pds_rec_reason_oth = reason_oth;
            // 若有處方修改才更新，否則保留最後改過的處方修改
            if (reason == Pds_recParam.Rec_reason.C04 ||
                reason == Pds_recParam.Rec_reason.C08 ||
                reason == Pds_recParam.Rec_reason.C11)
            {
                Rec.pds_rec_md_qty = md_qty;
                Rec.pds_rec_md_way1 = md_way1;
            }
            Rec.pds_rec_md_man = LoginViewModel.LoginUser.UserId;
            Rec.pds_rec_md_name = LoginViewModel.LoginUser.UserName;
            Rec.pds_rec_md_pc = HostUtil.GetHostNameAndAddress();
            Rec.pds_rec_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr();

            if (!recd_op_type.IsNullOrWhiteSpace())
            {
                var pds_recd = new Pds_recd()
                {
                    pds_recd_op_type = recd_op_type,
                    pds_recd_op_dtm = Rec.pds_rec_op_dtm_end,
                    pds_recd_code = code,
                    pds_recd_mst_id = mst_id,
                    pds_recd_pack = pack,
                    pds_recd_st = st,
                    pds_recd_reason = reason,
                    pds_recd_reason_oth = reason_oth,
                    pds_recd_md_qty = md_qty,
                    pds_recd_md_way1 = md_way1,
                    pds_recd_err_mst_id = err_mst_id,
                    pds_recd_err_qty = err_qty,
                    pds_recd_err_uqty1 = err_uqty1,
                    pds_recd_err_uqty2 = err_uqty2,
                    pds_recd_err_expdt = err_expdt,
                    pds_recd_md_man = LoginViewModel.LoginUser.UserId,
                    pds_recd_md_name = LoginViewModel.LoginUser.UserName,
                    pds_recd_md_pc = HostUtil.GetHostNameAndAddress(),
                    pds_recd_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr()
                };
                Rec.pds_recdList.Add(pds_recd);
            }
        }

    }
}
