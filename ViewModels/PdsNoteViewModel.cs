using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfLib;

namespace ViewModels
{
    public class PdsNoteViewModel : BaseViewModel<PdsNoteViewModel>
    { 
        private PdsNoteMicbcode _filteredParam;
        /// <summary>
        /// 篩選
        /// </summary>
        public PdsNoteMicbcode FilteredParam
        {
            get
            {
                if (_filteredParam == null)
                {
                    _filteredParam = new PdsNoteMicbcode
                    {
                        pds_note_st = "U",
                        pds_note_md_man = LoginViewModel.LoginUser.UserId
                };
                }
                if (_filteredParam.pds_note_dtm_begin.IsNullOrWhiteSpace())
                    _filteredParam.pds_note_dtm_begin = DateTime.Now.ToString("yyyy/MM/dd 00:00:00");
                if (_filteredParam.pds_note_dtm_end.IsNullOrWhiteSpace())
                    _filteredParam.pds_note_dtm_end = DateTime.Now.ToString("yyyy/MM/dd 23:59:59");
                return _filteredParam;
            }
            set => Set(ref _filteredParam, value);
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
                    result.Data.Insert(0, new Rec_code { rec_code_short = "", rec_code_name = "全部" });
                    _typeList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _typeList;
            }
            set => Set(ref _typeList, value);
        }

        private ObservableCollection<Ch_prs> _prsList;
        /// <summary>
        /// 處置藥品檔
        /// </summary>
        public ObservableCollection<Ch_prs> PrsList
        {
            get
            {
                if (_prsList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Ch_prs>>>(
                        RouteParam.Service(),
                        RouteParam.Ch_prs.QueryCh_prs,
                        queryParams: new { option = 2 }); // 2: 依處置類別藥品，查詢處置代碼、處置名稱一
                    result.Data = result.Data.OrderBy(p => p.chprs_mst_id).ToList();
                    _prsList = new ObservableCollection<Ch_prs>(result.Data);
                }
                return _prsList;
            }
            set => Set(ref _prsList, value);
        }

        private ObservableCollection<Mg_mnid> _clinicalList;
        /// <summary>
        /// 護理站清單
        /// </summary>
        public ObservableCollection<Mg_mnid> ClinicalList
        {
            get
            {
                if (_clinicalList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Mg_mnid>>>(
                        RouteParam.Service(),
                        RouteParam.Mg_mnid.QueryMg_mnid,
                        queryParams: new { option = 3 }); // 3: 查詢全部護理站
                    _clinicalList = new ObservableCollection<Mg_mnid>(result.Data);
                }
                return _clinicalList;
            }
            set => Set(ref _clinicalList, value);
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
                    result.Data.Insert(0, new Rec_code { rec_code_short = "", rec_code_name = "全部" });
                    _stList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _stList;
            }
            set => Set(ref _stList, value);
        }

        private ObservableCollection<PdsNoteMicbcode> _noteList;
        /// <summary>
        /// 藥師個人筆記本
        /// </summary>
        public ObservableCollection<PdsNoteMicbcode> NoteList
        {
            get {
                if (_noteList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<PdsNoteMicbcode>>>(
                        RouteParam.Service(),
                        RouteParam.Pds_note.QueryPdsNoteMicbcode,
                        FilteredParam, new { option = 1 }); // 依非空值篩選
                    _noteList = new ObservableCollection<PdsNoteMicbcode>(result.Data);
                }
                return _noteList;
            }
            set => Set(ref _noteList, value);
        }

        protected override string ExportTitle =>
            $"藥師個人筆記本 {DateTime.Now.ToString("yyyyMMddHHmmss")}";


        /// <summary>
        /// 顯示編輯畫面
        /// </summary>
        public Func<Pds_note, bool> ShowPdsNoteEditWindow;

        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand
            (OnQuery));
        /// <summary>
        /// 查詢
        /// </summary>
        private void OnQuery() =>
            NoteList = null;

        private DelegateCommand<PdsNoteMicbcode> _editCommand;
        public DelegateCommand<PdsNoteMicbcode> EditCommand =>
            _editCommand ?? (_editCommand = new DelegateCommand<PdsNoteMicbcode>
            (OnEdit));
        /// <summary>
        /// 編輯
        /// </summary>
        private void OnEdit(PdsNoteMicbcode note)
        {
            Pds_noteParam.EditMode mode = Pds_noteParam.EditMode.NONE;
            if (note.pds_note_op == Pds_noteParam.Op.UD &&
                note.pds_note_bag_code.IsNullOrWhiteSpace())
                mode = Pds_noteParam.EditMode.UDLST;
            else if(note.pds_note_op == Pds_noteParam.Op.FST &&
                note.pds_note_bag_code.IsNullOrWhiteSpace())
                mode = Pds_noteParam.EditMode.FSTLST;

            var result = ShowPdsNoteEditWindow?.Invoke(new Pds_note
            {
                pds_note_no = note.pds_note_no,
                EditMode = mode
            });
            if (result.HasValue && result.Value) OnQuery();
        }

    }
}
