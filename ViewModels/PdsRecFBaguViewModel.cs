using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using WpfLib;

namespace ViewModels
{
    public class PdsRecFBaguViewModel : BaseViewModel<PdsRecFBaguViewModel>
    {
        private string _filteredOption = "7";
        /// <summary>
        /// 階段篩選
        /// </summary>
        public string FilteredOption
        {
            get => _filteredOption;
            set => Set(ref _filteredOption, value);
        }

        private PdsRecMicbcode _filteredParam;
        /// <summary>
        /// 篩選
        /// </summary>
        public PdsRecMicbcode FilteredParam
        {
            get
            {
                if (_filteredParam == null)
                {
                    _filteredParam = new PdsRecMicbcode
                    {
                        pds_rec_st = "U"
                    };
                }
                if (_filteredParam.pds_rec_send_dt_begin_fmt.IsNullOrWhiteSpace())
                    _filteredParam.pds_rec_send_dt_begin_fmt = DateTime.Now.ToString("yyyy/MM/dd");
                if (_filteredParam.pds_rec_send_dt_end_fmt.IsNullOrWhiteSpace())
                    _filteredParam.pds_rec_send_dt_end_fmt = DateTime.Now.ToString("yyyy/MM/dd");
                return _filteredParam;
            }
            set => Set(ref _filteredParam, value);
        }

        private ObservableCollection<RecSt> _optionList;
        /// <summary>
        /// 階段清單
        /// </summary>
        public ObservableCollection<RecSt> OptionList
        {
            get
            {
                if (_optionList == null)
                {
                    _optionList = new ObservableCollection<RecSt>();
                    _optionList.Add(new RecSt { Status = "7", StatusName = "調劑" });
                    _optionList.Add(new RecSt { Status = "8", StatusName = "核對" });
                    _optionList.Add(new RecSt { Status = "9", StatusName = "發藥" });
                    _optionList.Add(new RecSt { Status = "10", StatusName = "全部" });
                }
                return _optionList;
            }
            set => Set(ref _optionList, value);
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
                    // 2: 依處置類別藥品，查詢處置代碼、處置名稱一
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Ch_prs>>>(
                        RouteParam.Service(),
                        RouteParam.Ch_prs.QueryCh_prs,
                        queryParams: new { option = 2 });
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
                    // 3: 查詢全部護理站
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Mg_mnid>>>(
                        RouteParam.Service(),
                        RouteParam.Mg_mnid.QueryMg_mnid,
                        queryParams: new { option = 3 });
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
                    var list = new List<Rec_code>();
                    list.Add(new Rec_code { rec_code_short = "U", rec_code_name = "未完成" });
                    list.Add(new Rec_code { rec_code_short = "S", rec_code_name = "無法給藥" });
                    _stList = new ObservableCollection<Rec_code>(list);
                }
                return _stList;
            }
            set => Set(ref _stList, value);
        }

        private ObservableCollection<Rec_code> _reasonList;
        /// <summary>
        /// 原因清單
        /// </summary>
        public ObservableCollection<Rec_code> ReasonList
        {
            get
            {
                if (_reasonList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Rec_code>>>(
                    RouteParam.Service(),
                    RouteParam.Rec_code.QueryRec_code, // 1 依參數自動組建
                    new Rec_code { rec_code_model = "pds_rec_reason", rec_code_st = "1" },
                    new { option = 1 });
                    result.Data.Insert(0, new Rec_code { rec_code_short = "", rec_code_name = "全部" });
                    _reasonList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _reasonList;
            }
            set => Set(ref _reasonList, value);
        }

        private ObservableCollection<PdsRecMicbcode> _baguList;
        /// <summary>
        /// 藥袋未完成明細
        /// </summary>
        public ObservableCollection<PdsRecMicbcode> BaguList
        {
            get => _baguList;
            set => Set(ref _baguList, value);
        }

        protected override string ExportTitle =>
            $"首日量藥袋未完成明細 {DateTime.Now.ToString("yyyyMMddHHmmss")}";


        /// <summary>
        /// 顯示編輯畫面
        /// </summary>
        public Func<Pds_rec, bool> ShowPdsRecEditWindow;

        /// <summary>
        /// 顯示明細畫面
        /// </summary>
        public Action<Pds_recd> ShowPdsRecdWindow;

        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand
            (OnQuery, () => ProgressVisibility != System.Windows.Visibility.Visible));
        /// <summary>
        /// 查詢
        /// </summary>
        private async void OnQuery()
        {
            ProgressVisibility = System.Windows.Visibility.Visible;
            FilteredParam.pds_rec_send_dt_begin = DateTimeUtil.ConvertAD(FilteredParam.pds_rec_send_dt_begin_fmt,
                inFormat: "yyyy/MM/dd", outFormat: "yyyMMdd").ToInt();
            FilteredParam.pds_rec_send_dt_end = DateTimeUtil.ConvertAD(FilteredParam.pds_rec_send_dt_end_fmt,
                inFormat: "yyyy/MM/dd", outFormat: "yyyMMdd").ToInt();
            var result = await ApiUtil.HttpClientExAsync<ApiResult<List<PdsRecMicbcode>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPdsRecMicbcode,
                FilteredParam, new { option = FilteredOption });
            BaguList = new ObservableCollection<PdsRecMicbcode>(result.Data);
            ProgressVisibility = System.Windows.Visibility.Collapsed;
        }

        private DelegateCommand<PdsRecMicbcode> _editNondeliverCommand;
        public DelegateCommand<PdsRecMicbcode> EditNondeliverCommand =>
            _editNondeliverCommand ?? (_editNondeliverCommand = new DelegateCommand<PdsRecMicbcode>
            (OnEditNondeliver));
        /// <summary>
        /// 編輯無法給藥註記
        /// </summary>
        private void OnEditNondeliver(PdsRecMicbcode rec)
        {
            var result = ShowPdsRecEditWindow?.Invoke(new Pds_rec
            {
                pds_rec_no = rec.pds_rec_no,
                EditMode = Pds_recParam.EditMode.NONDELIVER
            });
            if (result.HasValue && result.Value && ProgressVisibility != System.Windows.Visibility.Visible) OnQuery();
        }

        private DelegateCommand<PdsRecMicbcode> _editNoteCommand;
        public DelegateCommand<PdsRecMicbcode> EditNoteCommand =>
            _editNoteCommand ?? (_editNoteCommand = new DelegateCommand<PdsRecMicbcode>
            (OnEditNote));
        /// <summary>
        /// 編輯備註
        /// </summary>
        private void OnEditNote(PdsRecMicbcode rec)
        {
            var result = ShowPdsRecEditWindow?.Invoke(new Pds_rec
            {
                pds_rec_no = rec.pds_rec_no,
                EditMode = Pds_recParam.EditMode.NOTE
            });
            if (result.HasValue && result.Value && ProgressVisibility != System.Windows.Visibility.Visible) OnQuery();
        }

        private DelegateCommand<PdsRecMicbcode> _detailCommand;
        public DelegateCommand<PdsRecMicbcode> DetailCommand =>
            _detailCommand ?? (_detailCommand = new DelegateCommand<PdsRecMicbcode>
            (OnDetail));
        /// <summary>
        /// 查詢明細
        /// </summary>
        private void OnDetail(PdsRecMicbcode rec)
        {
            ShowPdsRecdWindow?.Invoke(new Pds_recd
            {
                pds_recd_rec_no = rec.pds_rec_no,
                pds_rec_lst_code = rec.pds_rec_lst_code,
                pds_rec_bag_code = rec.pds_rec_bag_code,
                DetailMode =   Pds_recParam.DetailMode.REC_NO
            });
        }

    }
}
