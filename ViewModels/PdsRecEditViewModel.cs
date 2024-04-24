using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WpfLib;

namespace ViewModels
{
    public class PdsRecEditViewModel : BaseViewModel<PdsRecEditViewModel>
    {
        private Pds_rec _recParam;
        /// <summary>
        /// 記錄查詢參數
        /// </summary>
        public Pds_rec RecParam
        {
            get => _recParam;
            set
            {
                Set(ref _recParam, value);
                switch (_recParam.EditMode)
                {
                    case Pds_recParam.EditMode.NONDELIVER:
                        NondeliverVisibility = Visibility.Visible;
                        NoteVisibility = Visibility.Collapsed;
                        break;
                    case Pds_recParam.EditMode.NOTE:
                        NondeliverVisibility = Visibility.Collapsed;
                        NoteVisibility = Visibility.Visible;
                        break;
                    case Pds_recParam.EditMode.NONE:
                        NondeliverVisibility = Visibility.Collapsed;
                        NoteVisibility = Visibility.Collapsed;
                        break;
                }
            }
        }

        private Pds_rec _rec;
        /// <summary>
        /// 記錄編輯儲存
        /// </summary>
        public Pds_rec Rec
        {
            get
            {
                if (_rec == null)
                {
                    _rec = ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
                    RouteParam.Service(),
                    RouteParam.Pds_rec.QueryPds_rec,
                    RecParam,
                    new { option = 6 }).Data.FirstOrDefault(); // 6: 依主檔單號
                }
                return _rec;
            }
            set => Set(ref _rec, value);
        }

        private Visibility _nondeliverVisibility = Visibility.Collapsed;
        /// <summary>
        /// 無法交車註記：Visibility
        /// </summary>
        public Visibility NondeliverVisibility
        {
            get => _nondeliverVisibility;
            set => Set(ref _nondeliverVisibility, value);
        }

        private Visibility _noteVisibility = Visibility.Collapsed;
        /// <summary>
        /// 備註：Visibility
        /// </summary>
        public Visibility NoteVisibility
        {
            get => _noteVisibility;
            set => Set(ref _noteVisibility, value);
        }


        private DelegateCommand _okCommand;
        public DelegateCommand OKCommand =>
            _okCommand ?? (_okCommand = new DelegateCommand
            (OnOK, () => Validate().IsValid));
        private void OnOK()
        {
            ApiResult<Pds_rec> saveResult = null;
            int option = 0;
            string pds_recd_op_dtm = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            Rec.pds_rec_md_man = LoginViewModel.LoginUser.UserId;
            Rec.pds_rec_md_name = LoginViewModel.LoginUser.UserName;
            Rec.pds_rec_md_pc = HostUtil.GetHostNameAndAddress();
            Rec.pds_rec_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr();

            switch (RecParam.EditMode)
            {
                case Pds_recParam.EditMode.NONDELIVER:
                    Rec.pds_rec_op_dtm_end = pds_recd_op_dtm;
                    Pds_recd pds_recd = new Pds_recd()
                    {
                        pds_recd_rec_no = Rec.pds_rec_no,
                        pds_recd_op_type = Pds_recParam.Recd_op_type.BAGS,
                        pds_recd_op_dtm = pds_recd_op_dtm,
                        pds_recd_nondeliver = Rec.pds_rec_nondeliver,
                        pds_recd_md_man = Rec.pds_rec_md_man,
                        pds_recd_md_name = Rec.pds_rec_md_name,
                        pds_recd_md_pc = Rec.pds_rec_md_pc,
                        pds_recd_md_ver = Rec.pds_rec_md_ver
                    };
                    if (!Rec.pds_rec_nondeliver.IsNullOrWhiteSpace())
                    { // 無法交車註記
                        Rec.pds_rec_reason = "";
                        Rec.pds_rec_reason_oth = "";
                        Rec.pds_rec_st = Pds_recParam.Rec_st.S;
                        pds_recd.pds_recd_st = Rec.pds_rec_st;
                    }
                    else if (Rec.pds_rec_nondeliver.IsNullOrWhiteSpace() &&
                        Rec.pds_rec_st == Pds_recParam.Rec_st.S)
                    { // 取消無法交車註記
                        Rec.pds_rec_st = Pds_recParam.Rec_st.C;
                        pds_recd.pds_recd_st = Rec.pds_rec_st;
                    }
                    else
                    {
                        DialogResult = true;
                        goto exit;
                    }

                    Rec.pds_recdList.Clear();
                    Rec.pds_recdList.Add(pds_recd);

                    if (Rec.pds_rec_bag_code.StartsWith("ICBC"))
                        option = 2; // 2: 藥車無法交車註記，排除備註欄位儲存
                    else if (Rec.pds_rec_bag_code.StartsWith("ICFC"))
                        option = 12; // 12: 首日量無法交車註記，排除備註欄位儲存 
                    break;
                case Pds_recParam.EditMode.NOTE:
                    option = 3; // 3: 備註欄位儲存
                    break;
                default:
                    DialogResult = true;
                    goto exit;
            }

            saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option });

            if (!saveResult.Succ)
                Util.Media.MsgSpeech(saveResult.Msg);
            else
                DialogResult = true;

            exit:;
        }

    }
}
