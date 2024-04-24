using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using WpfLib;

namespace ViewModels
{
    public class PdsRecLstuViewModel : BaseViewModel<PdsRecLstuViewModel>
    {
        private string _filteredOption = "3";
        /// <summary>
        /// 階段篩選
        /// </summary>
        public string FilteredOption
        {
            get => _filteredOption;
            set => Set(ref _filteredOption, value);
        }

        private Pds_rec _filteredParam;
        /// <summary>
        /// 篩選
        /// </summary>
        public Pds_rec FilteredParam
        {
            get
            {
                if (_filteredParam == null)
                    _filteredParam = new Pds_rec();
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
                    _optionList.Add(new RecSt { Status = "3", StatusName = "調劑" });
                    _optionList.Add(new RecSt { Status = "4", StatusName = "核對" });
                    _optionList.Add(new RecSt { Status = "5", StatusName = "全部" });
                }
                return _optionList;
            }
            set => Set(ref _optionList, value);
        }

        private ObservableCollection<Pds_rec> _lstuList;
        /// <summary>
        /// 配藥單未完成明細
        /// </summary>
        public ObservableCollection<Pds_rec> LstuList
        {
            get => _lstuList;
            set => Set(ref _lstuList, value);
        }

        protected override string ExportTitle =>
            $"藥車配藥單未完成明細 {DateTime.Now.ToString("yyyyMMddHHmmss")}";


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
            var result = await ApiUtil.HttpClientExAsync<ApiResult<List<Pds_rec>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPds_rec,
                FilteredParam, new { option = FilteredOption });
            LstuList = new ObservableCollection<Pds_rec>(result.Data);
            ProgressVisibility = System.Windows.Visibility.Collapsed;
        }

    }
}
