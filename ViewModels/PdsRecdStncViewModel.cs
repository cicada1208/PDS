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
    public class PdsRecdStncViewModel : BaseViewModel<PdsRecdStncViewModel>
    {
        private string _filteredOption = "4";
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
                    _filteredParam = new PdsRecMicbcode();
                if (_filteredParam.pds_rec_op_dtm_begin_begin.IsNullOrWhiteSpace())
                    _filteredParam.pds_rec_op_dtm_begin_begin = DateTime.Now.ToString("yyyy/MM/dd 00:00:00");
                if (_filteredParam.pds_rec_op_dtm_begin_end.IsNullOrWhiteSpace())
                    _filteredParam.pds_rec_op_dtm_begin_end = DateTime.Now.ToString("yyyy/MM/dd 23:59:59");
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
                    _optionList.Add(new RecSt { Status = "4", StatusName = "藥車調劑" });
                    _optionList.Add(new RecSt { Status = "5", StatusName = "藥車核對" });
                    _optionList.Add(new RecSt { Status = "11", StatusName = "首日量調劑" });
                    _optionList.Add(new RecSt { Status = "12", StatusName = "首日量核對" });
                    _optionList.Add(new RecSt { Status = "13", StatusName = "首日量發藥" });
                    _optionList.Add(new RecSt { Status = "6", StatusName = "全部" });
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
                    new Rec_code { rec_code_model = "pds_rec_st", rec_code_group = "pds_rec_st", rec_code_st = "1" },
                    new { option = 1 }); // 1 依參數自動組建
                    var filter = new HashSet<string> { "N", "C", "Y" };
                    result.Data = result.Data.Where(s => filter.Contains(s.rec_code_short)).ToList();
                    result.Data.Insert(0, new Rec_code { rec_code_short = "", rec_code_name = "全部" });
                    _stList = new ObservableCollection<Rec_code>(result.Data);
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
                    RouteParam.Rec_code.QueryRec_code,
                    new Rec_code { rec_code_model = "pds_rec_reason", rec_code_st = "1" },
                    new { option = 1 }); // 1 依參數自動組建
                    result.Data.Insert(0, new Rec_code { rec_code_short = "", rec_code_name = "全部" });
                    _reasonList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _reasonList;
            }
            set => Set(ref _reasonList, value);
        }

        private ObservableCollection<PdsRecMicbcode> _recdNCList;
        /// <summary>
        /// 錯誤/取消明細
        /// </summary>
        public ObservableCollection<PdsRecMicbcode> RecdNCList
        {
            get => _recdNCList;
            set => Set(ref _recdNCList, value);
        }

        protected override string ExportTitle =>
            $"錯誤&取消明細 {DateTime.Now.ToString("yyyyMMddHHmmss")}";


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
            var result = await ApiUtil.HttpClientExAsync<ApiResult<List<PdsRecMicbcode>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPdsRecMicbcode,
                FilteredParam, new { option = FilteredOption });
            RecdNCList = new ObservableCollection<PdsRecMicbcode>(result.Data);
            ProgressVisibility = System.Windows.Visibility.Collapsed;
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
                DetailMode = Pds_recParam.DetailMode.REC_NO
            });
        }

    }
}
