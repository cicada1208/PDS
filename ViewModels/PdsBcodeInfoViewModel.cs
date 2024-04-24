using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WpfLib;

namespace ViewModels
{
    public class PdsBcodeInfoViewModel : BaseViewModel<PdsBcodeInfoViewModel>
    {
        private string _bcode_send_dt;
        public string bcode_send_dt
        {
            get => _bcode_send_dt;
            set => Set(ref _bcode_send_dt, value);
        }

        private string _bcode_ipd_no;
        public string bcode_ipd_no
        {
            get => _bcode_ipd_no;
            set => Set(ref _bcode_ipd_no, value);
        }

        private int? _bcode_code_pat_no;
        public int? bcode_code_pat_no
        {
            get => _bcode_code_pat_no;
            set => Set(ref _bcode_code_pat_no, value);
        }

        private ObservableCollection<Ch_bcode> _bcodeList;
        /// <summary>
        /// 自包機(餐包)清單
        /// </summary>
        public ObservableCollection<Ch_bcode> BcodeList
        {
            get
            {
                if (_bcodeList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Ch_bcode>>>(
                    RouteParam.Service(),
                    RouteParam.Ch_bcode.QueryCh_bcode,
                    new { bcode_send_dt, bcode_ipd_no, bcode_code_pat_no },
                    new { option = 3 }); //3: 依傳送日期、住院序號、病歷號，查詢自包機
                    _bcodeList = new ObservableCollection<Ch_bcode>(result.Data);
                }
                return _bcodeList;
            }
            set => Set(ref _bcodeList, value);
        }

        private ObservableCollection<Ch_bcode_daygrp> _dayList;
        /// <summary>
        /// 分組清單
        /// </summary>
        public ObservableCollection<Ch_bcode_daygrp> DayList
        {
            get
            {
                if (_dayList == null)
                    _dayList = SetGroup(BcodeList);
                return _dayList;
            }
            set => Set(ref _dayList, value);
        }


        /// <summary>
        /// 核對藥品儲存
        /// </summary>
        public Action SaveMEDV;

        private ObservableCollection<Ch_bcode_daygrp> SetGroup(ObservableCollection<Ch_bcode> bcodeList)
        {
            var dayList = bcodeList.GroupBy(d => d.day_group)
                .Select(dg => new Ch_bcode_daygrp
                {
                    day_group = dg.Key,
                    codeList = dg.GroupBy(c => c.bcode_code)
                    .Select(cg => new Ch_bcode_codegrp
                    {
                        code_group = cg.Key,
                        bcode_code_rx_dtm = cg.Key.EndsWith("0") ?
                        "" : DateTimeUtil.ConvertROC(cg.Key.Substring(8, 9), false, "yyyMMddHH", "yyy/MM/dd HH:mm"),
                        medList = cg.ToList()
                    }).ToList()
                }).ToList();

            dayList.ForEach(day =>
            {
                day.codeList.ForEach(code =>
                {
                    if (code.ni_pic_urlList == null)
                        code.ni_pic_urlList = new List<string>();

                    code.medList.ForEach(med =>
                    {
                        for (var i = 1; i <= med.bcode_rx_qty.ToInt(); i++)
                        {
                            code.ni_pic_urlList.Add(med.ni_pic_url);
                        }
                    });
                });
            });

            return new ObservableCollection<Ch_bcode_daygrp>(dayList); ;
        }

        private DelegateCommand<string> _NiPicViewCommand;
        public DelegateCommand<string> NiPicViewCommand =>
            _NiPicViewCommand ?? (_NiPicViewCommand = new DelegateCommand<string>
            (OnNiPicView));
        /// <summary>
        ///  查閱裸碇藥品圖片
        /// </summary>
        private void OnNiPicView(string url)
        {
            if (!string.IsNullOrWhiteSpace(url.ToString()))
                System.Diagnostics.Process.Start(url.ToString());
        }

        private DelegateCommand _dayGroupToggleCommand;
        public DelegateCommand DayGroupToggleCommand =>
            _dayGroupToggleCommand ?? (_dayGroupToggleCommand = new DelegateCommand
            (OnDayGroupToggle));
        /// <summary>
        /// 確認選取
        /// </summary>
        private void OnDayGroupToggle()
        {
            if (DayList.Where(d => d.IsChecked == false).Count() > 0)
                return;

            SaveMEDV?.Invoke();
        }

    }
}
