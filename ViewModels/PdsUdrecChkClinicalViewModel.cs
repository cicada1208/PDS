using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using WpfLib;

namespace ViewModels
{
    public class PdsUdrecChkClinicalViewModel : BaseViewModel<PdsUdrecChkClinicalViewModel>
    {
        private string _send_dt;
        /// <summary>
        /// 傳送日期
        /// </summary>
        public string Send_dt
        {
            get
            {
                if (_send_dt.IsNullOrWhiteSpace())
                    _send_dt = DateTime.Now.ToString("yyyy/MM/dd");
                return _send_dt;
            }
            set => Set(ref _send_dt, value);
        }

        private Ch_udrec_chk _selectedClinical;
        /// <summary>
        /// 護理站
        /// </summary>
        [Display(Name = "護理站")]
        public Ch_udrec_chk SelectedClinical
        {
            get => _selectedClinical;
            set => Set(ref _selectedClinical, value);
        }

        private ObservableCollection<Ch_udrec_chk> _clinicalList;
        /// <summary>
        /// 護理站清單
        /// </summary>
        public ObservableCollection<Ch_udrec_chk> ClinicalList
        {
            get
            {
                if (_clinicalList == null)
                {
                    int chudrecchk_date = DateTimeUtil.ConvertAD(Send_dt, inFormat: "yyyy/MM/dd", outFormat: "yyyMMdd").ToInt();
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Ch_udrec_chk>>>(
                    RouteParam.Service(),
                    RouteParam.Ch_udrec_chk.QueryCh_udrec_chk, 
                    new Ch_udrec_chk { chudrecchk_date = chudrecchk_date },
                    new { option = 3 }); // 3: 依傳送日期，查詢護理站
                    _clinicalList = new ObservableCollection<Ch_udrec_chk>(result.Data);
                }
                return _clinicalList;
            }
            set => Set(ref _clinicalList, value);
        }


        private DelegateCommand _queryCommand;
        public DelegateCommand QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand
            (OnQuery));
        /// <summary>
        /// 查詢
        /// </summary>
        private void OnQuery() => ClinicalList = null;

        private DelegateCommand _okCommand;
        public DelegateCommand OKCommand =>
            _okCommand ?? (_okCommand = new DelegateCommand
            (OnOK, () => Validate().IsValid));
        private void OnOK()
        {
            // save
            string pds_rec_op_dtm = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Pds_rec rec = new Pds_rec
            {
                pds_rec_op_type = Pds_recParam.Rec_op_type.CCAR,
                pds_rec_op_dtm_begin = pds_rec_op_dtm,
                pds_rec_op_dtm_end = pds_rec_op_dtm,
                pds_rec_send_dt = SelectedClinical.chudrecchk_date,
                pds_rec_clinical = SelectedClinical.chudrecchk_bed_unit,
                pds_rec_st = Pds_recParam.Rec_st.U,
                pds_rec_md_man = LoginViewModel.LoginUser.UserId,
                pds_rec_md_name = LoginViewModel.LoginUser.UserName,
                pds_rec_md_pc = HostUtil.GetHostNameAndAddress(),
                pds_rec_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr()
            };

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            rec, new { option = 4 }); // 4: 藥車核對作業，CCAR 作業日期時間-始儲存

            if (!saveResult.Succ)
                Util.Media.MsgSpeech(saveResult.Msg);
            else
                DialogResult = true;
        }
    }
}
