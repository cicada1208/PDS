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
    public class PdsRecStatisticViewModel : BaseViewModel<PdsRecStatisticViewModel>
    {
        private string _filteredOption = "1";
        /// <summary>
        /// 作業及方式篩選
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
        /// 作業及方式清單
        /// </summary>
        public ObservableCollection<RecSt> OptionList
        {
            get
            {
                if (_optionList == null)
                {
                    _optionList = new ObservableCollection<RecSt>();
                    _optionList.Add(new RecSt { Status = "1", StatusName = "藥車作業依護理站" });
                    _optionList.Add(new RecSt { Status = "2", StatusName = "藥車作業依配藥單" });
                    _optionList.Add(new RecSt { Status = "3", StatusName = "藥車作業依藥袋" });
                    _optionList.Add(new RecSt { Status = "4", StatusName = "首日量作業依配藥單" });
                    _optionList.Add(new RecSt { Status = "5", StatusName = "首日量作業依藥袋" });
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

        private ObservableCollection<PdsRecAC> _recStatisticList;
        /// <summary>
        /// 統計明細
        /// </summary>
        public ObservableCollection<PdsRecAC> RecStatisticList
        {
            get => _recStatisticList;
            set => Set(ref _recStatisticList, value);
        }

        protected override string ExportTitle =>
            $"統計明細 {DateTime.Now.ToString("yyyyMMddHHmmss")}";


        private DelegateCommand<DataGrid> _queryCommand;
        public DelegateCommand<DataGrid> QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand<DataGrid>
            (OnQuery, param => ProgressVisibility != System.Windows.Visibility.Visible));
        /// <summary>
        /// 查詢
        /// </summary>
        private async void OnQuery(DataGrid dataGrid)
        {
            ProgressVisibility = System.Windows.Visibility.Visible;
            HashSet<string> cols = null;

            switch (FilteredOption)
            {
                case "1":
                    cols = new HashSet<string> { "pds_rec_send_dt_fmt", "pds_rec_op_name", "pds_rec_clinical",
                    "pds_rec_op_dtm_begin", "pds_rec_op_dtm_end", "pds_rec_op_dtm_diff",
                    "c_pds_rec_op_dtm_begin","c_pds_rec_op_dtm_end","c_pds_rec_op_dtm_diff"};
                    break;
                case "2":
                    cols = new HashSet<string> { "pds_rec_send_dt_fmt", "pds_rec_op_name",
                    "pds_rec_lst_code", "c_pds_rec_clinical", "c_pds_rec_bed", "pds_rec_pat_no", "pat_name",
                    "pds_rec_op_dtm_begin", "pds_rec_op_dtm_end", "pds_rec_op_dtm_diff",
                    "c_pds_rec_op_dtm_begin","c_pds_rec_op_dtm_end","c_pds_rec_op_dtm_diff"};
                    break;
                case "3":
                    cols = new HashSet<string> { "pds_rec_send_dt_fmt", "pds_rec_op_name",
                    "pds_rec_bag_code", "c_pds_rec_clinical", "c_pds_rec_bed", "pds_rec_pat_no", "pat_name",
                    "icbcode_fee_key","icbcode_rx_way1", "icbcode_rx_way2", "icbcode_rx_uqty", "icbcode_rx_unit",
                    "icbcode_rx_qty", "icbcode_pha_unit", "icbcode_pack", "qty_pack_final",
                    "pds_rec_op_dtm_begin", "pds_rec_op_dtm_end", "pds_rec_op_dtm_diff", "pds_rec_st_name", "pds_rec_md_name",
                    "c_pds_rec_op_dtm_begin","c_pds_rec_op_dtm_end","c_pds_rec_op_dtm_diff", "c_pds_rec_st_name", "c_pds_rec_md_name"};
                    break;
                case "4":
                    cols = new HashSet<string> { "pds_rec_op_name",
                    "icfcode_ipill_no", "icfcode_clinical", "icfcode_bed", "pds_rec_pat_no", "pat_name",
                    "tra_odr_dtm", "tra_prn_dtm",
                    "pds_rec_op_dtm_begin", "pds_rec_op_dtm_end", "pds_rec_op_dtm_diff",
                    "c_pds_rec_op_dtm_begin","c_pds_rec_op_dtm_end","c_pds_rec_op_dtm_diff",
                    "r_pds_rec_op_dtm_begin","r_pds_rec_op_dtm_end","r_pds_rec_op_dtm_diff",
                    "ar_pds_rec_op_dtm_diff","or_pds_rec_op_dtm_diff"};
                    break;
                case "5":
                    cols = new HashSet<string> { "pds_rec_op_name",
                    "icfcode_ipill_no", "pds_rec_bag_code", "icfcode_clinical", "icfcode_bed", "pds_rec_pat_no", "pat_name",
                    "icbcode_fee_key","icbcode_rx_way1", "icbcode_rx_way2", "icbcode_rx_uqty", "icbcode_rx_unit",
                    "icbcode_rx_qty", "icbcode_pha_unit", "icbcode_pack", "qty_pack_final",
                    "tra_prn_dtm",
                    "pds_rec_op_dtm_begin", "pds_rec_op_dtm_end", "pds_rec_op_dtm_diff", "pds_rec_st_name", "pds_rec_md_name",
                    "c_pds_rec_op_dtm_begin","c_pds_rec_op_dtm_end","c_pds_rec_op_dtm_diff", "c_pds_rec_st_name", "c_pds_rec_md_name",
                    "r_pds_rec_op_dtm_begin","r_pds_rec_op_dtm_end","r_pds_rec_op_dtm_diff", "r_pds_rec_st_name", "r_pds_rec_md_name",
                    "ar_pds_rec_op_dtm_diff"};
                    break;
            }

            Util.Ctrl.DataGridDefineCols<PdsRecAC>(dataGrid, cols);

            var result = await ApiUtil.HttpClientExAsync<ApiResult<List<PdsRecAC>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPdsRecAC,
                FilteredParam, new { option = FilteredOption });
            RecStatisticList = new ObservableCollection<PdsRecAC>(result.Data);
            ProgressVisibility = System.Windows.Visibility.Collapsed;
        }

    }
}
