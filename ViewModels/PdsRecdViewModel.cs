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
    public class PdsRecdViewModel : BaseViewModel<PdsRecdViewModel>
    {
        private Pds_recd _recdParam;
        /// <summary>
        /// 記錄查詢參數
        /// </summary>
        public Pds_recd RecdParam
        {
            get => _recdParam;
            set => Set(ref _recdParam, value);
        }

        private ObservableCollection<RecSt> _optionList;
        /// <summary>
        /// 方式清單
        /// </summary>
        public ObservableCollection<RecSt> OptionList
        {
            get
            {
                if (_optionList == null)
                {
                    _optionList = new ObservableCollection<RecSt>();
                    if (!RecdParam.pds_rec_lst_code.IsNullOrWhiteSpace())
                        _optionList.Add(new RecSt { Status = Pds_recParam.DetailMode.LST_CODE, StatusName = "依配藥單條碼" });
                    if (!RecdParam.pds_rec_bag_code.IsNullOrWhiteSpace())
                        _optionList.Add(new RecSt { Status = Pds_recParam.DetailMode.BAG_CODE, StatusName = "依藥袋條碼" });
                }
                return _optionList;
            }
            set => Set(ref _optionList, value);
        }

        private ObservableCollection<Pds_recd> _recdList;
        /// <summary>
        /// 明細歷程清單
        /// </summary>
        public ObservableCollection<Pds_recd> RecdList
        {
            get
            {
                if (_recdList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Pds_recd>>>(
                    RouteParam.Service(),
                    RouteParam.Pds_recd.QueryPds_recd,
                    RecdParam,
                    new { option = (int)RecdParam.DetailMode });
                    _recdList = new ObservableCollection<Pds_recd>(result.Data);
                }
                return _recdList;
            }
            set => Set(ref _recdList, value);
        }

        protected override string ExportTitle
        {
            get
            {
                string title = string.Empty;
                switch (RecdParam.DetailMode)
                {
                    case Pds_recParam.DetailMode.LST_CODE:
                        title = $"配藥單 {RecdParam.pds_rec_lst_code}";
                        break;
                    case Pds_recParam.DetailMode.BAG_CODE:
                        title = $"藥袋 {RecdParam.pds_rec_bag_code}";
                        break;
                    case Pds_recParam.DetailMode.REC_NO:
                        title = $"主檔單號 {RecdParam.pds_recd_rec_no}";
                        break;
                }
                return $"{title} 明細歷程 {DateTime.Now.ToString("yyyyMMddHHmmss")}";
            }
        }


        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand
            (OnQuery));
        /// <summary>
        /// 查詢
        /// </summary>
        private void OnQuery() =>
            RecdList = null;

    }
}
