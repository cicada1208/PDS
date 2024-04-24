using Lib;
using LiveCharts;
using LiveCharts.Wpf;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfLib;

namespace ViewModels
{
    public class PdsRecFstAvgViewModel : BaseViewModel<PdsRecFstAvgViewModel>
    {
        private string _filteredOption = "1";
        /// <summary>
        /// 階段
        /// </summary>
        public string FilteredOption
        {
            get => _filteredOption;
            set => Set(ref _filteredOption, value);
        }

        private FstAvg _filteredParam;
        /// <summary>
        /// 篩選
        /// </summary>
        public FstAvg FilteredParam
        {
            get
            {
                if (_filteredParam == null)
                    _filteredParam = new FstAvg();
                if (_filteredParam.icfcode_prt_dt_begin_fmt.IsNullOrWhiteSpace())
                    _filteredParam.icfcode_prt_dt_begin_fmt = DateTime.Now.ToString("yyyy/MM/dd");
                if (_filteredParam.icfcode_prt_dt_end_fmt.IsNullOrWhiteSpace())
                    _filteredParam.icfcode_prt_dt_end_fmt = DateTime.Now.ToString("yyyy/MM/dd");
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
                    _optionList.Add(new RecSt { Status = "1", StatusName = "開立-調劑" });
                    _optionList.Add(new RecSt { Status = "2", StatusName = "開立-核對" });
                    _optionList.Add(new RecSt { Status = "3", StatusName = "開立-發藥" });
                    _optionList.Add(new RecSt { Status = "4", StatusName = "調劑-核對" });
                    _optionList.Add(new RecSt { Status = "5", StatusName = "調劑-發藥" });
                    _optionList.Add(new RecSt { Status = "6", StatusName = "核對-發藥" });
                }
                return _optionList;
            }
            set => Set(ref _optionList, value);
        }

        private ObservableCollection<Rec_code> _lstTypeList;
        /// <summary>
        /// 配藥單類型清單
        /// </summary>
        public ObservableCollection<Rec_code> LstTypeList
        {
            get
            {
                if (_lstTypeList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Rec_code>>>(
                    RouteParam.Service(),
                    RouteParam.Rec_code.QueryRec_code,
                    new Rec_code { rec_code_model = "icfcode_lst_type", rec_code_group = "icfcode_lst_type", rec_code_st = "1" },
                    new { option = 1 }); // 1 依參數自動組建
                    result.Data.Insert(0, new Rec_code { rec_code_short = "", rec_code_name = "全部" });
                    _lstTypeList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _lstTypeList;
            }
            set => Set(ref _lstTypeList, value);
        }

        private ObservableCollection<FstAvg> _fstAvgList;
        /// <summary>
        /// 平均時間
        /// </summary>
        public ObservableCollection<FstAvg> FstAvgList
        {
            get => _fstAvgList;
            set => Set(ref _fstAvgList, value);
        }

        private HashSet<string> _dataGridCols;
        /// <summary>
        /// DataGrid Columns
        /// </summary>
        public HashSet<string> DataGridCols
        {
            get
            {
                if (_dataGridCols is null)
                    _dataGridCols = new HashSet<string> { "pds_rec_op_name", "dt", "dayofWeek",
                "hr0000", "hr0030", "hr0100", "hr0130", "hr0200", "hr0230", "hr0300", "hr0330",
                "hr0400", "hr0430", "hr0500", "hr0530", "hr0600", "hr0630", "hr0700", "hr0730",
                "hr0800", "hr0830", "hr0900", "hr0930", "hr1000", "hr1030", "hr1100", "hr1130",
                "hr1200", "hr1230", "hr1300", "hr1330", "hr1400", "hr1430", "hr1500", "hr1530",
                "hr1600", "hr1630", "hr1700", "hr1730", "hr1800", "hr1830", "hr1900", "hr1930",
                "hr2000", "hr2030", "hr2100", "hr2130", "hr2200", "hr2230", "hr2300", "hr2330"};
                return _dataGridCols;
            }
            set => Set(ref _dataGridCols, value);
        }

        private SeriesCollection _seriesCollection;
        /// <summary>
        /// 圖表趨勢線
        /// </summary>
        public SeriesCollection SeriesCollection
        {
            get => _seriesCollection;
            set => Set(ref _seriesCollection, value);
        }

        private string[] _chartLabels;
        /// <summary>
        /// 圖表X軸 Labels
        /// </summary>
        public string[] ChartLabels
        {
            get => _chartLabels;
            set => Set(ref _chartLabels, value);
        }

        protected override string ExportTitle =>
            $"首日量平均時間 {DateTime.Now.ToString("yyyyMMddHHmmss")}";


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
            Util.Ctrl.DataGridDefineCols<FstAvg>(dataGrid, DataGridCols);
            dataGrid.FrozenColumnCount = 3;
            await Task.Run(() => SetDataDrid());
            SetChart();
            ProgressVisibility = System.Windows.Visibility.Collapsed;
        }

        private void SetDataDrid()
        {
            FilteredParam.icfcode_prt_dt_begin = DateTimeUtil.ConvertAD(FilteredParam.icfcode_prt_dt_begin_fmt,
                inFormat: "yyyy/MM/dd", outFormat: "yyyMMdd").ToInt();
            FilteredParam.icfcode_prt_dt_end = DateTimeUtil.ConvertAD(FilteredParam.icfcode_prt_dt_end_fmt,
                inFormat: "yyyy/MM/dd", outFormat: "yyyMMdd").ToInt();

            var result = ApiUtil.HttpClientEx<ApiResult<List<FstAvg>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryFstAvg,
                FilteredParam, new { option = FilteredOption });
            FstAvgList = new ObservableCollection<FstAvg>(result.Data);
        }

        /// <summary>
        /// 繪製圖表
        /// </summary>
        private void SetChart()
        {
            SeriesCollection = new SeriesCollection();
            List<LineSeries> seriesList = new List<LineSeries>();
            LineSeries lineSeries;
            ChartValues<double> chartValues;
            List<double> values;

            if (ChartLabels == null || ChartLabels.Length == 0)
            {
                List<String> lables = new List<String>();
                foreach (var col in DataGridCols)
                    if (col.StartsWith("hr")) lables.Add(ReflectionUtil.GetPropertyDisplayName<FstAvg>(col));
                ChartLabels = lables.ToArray();
            }

            var colors = Util.Color.CreateColorsBrush(FstAvgList.Count);
            var colortors = colors.GetEnumerator();

            foreach (var fstAvg in FstAvgList)
            {
                chartValues = new ChartValues<double>();
                values = new List<double>();
                foreach (var col in DataGridCols)
                    if (col.StartsWith("hr")) values.Add(fstAvg.GetPropertyValue(col).ToDouble());
                chartValues.AddRange(values);

                colortors.MoveNext();

                lineSeries = new LineSeries
                {
                    Title = $"{fstAvg.dt}({fstAvg.dayofWeek})",
                    Values = chartValues,
                    Stroke = colortors.Current
                };

                seriesList.Add(lineSeries);
            }

            SeriesCollection.AddRange(seriesList);
        }

    }
}
