using Lib;
using Models;
using Params;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using WpfLib;

namespace ViewModels
{
    public class PdsRecCancelViewModel : BaseViewModel<PdsRecCancelViewModel>
    {
        private Pds_recParam.CancelMode _cancelModel;
        /// <summary>
        /// 取消模式
        /// </summary>
        public Pds_recParam.CancelMode CancelMode
        {
            get => _cancelModel;
            set
            {
                Set(ref _cancelModel, value);
                switch (_cancelModel)
                {
                    case Pds_recParam.CancelMode.ABAGC:
                        Title = "取消";
                        break;
                    case Pds_recParam.CancelMode.CBAGC:
                        Title = "單筆取消(非餐包)";
                        break;
                    case Pds_recParam.CancelMode.CBAGCG:
                        Title = "單筆取消(餐包)";
                        break;
                    case Pds_recParam.CancelMode.CLSTC:
                    case Pds_recParam.CancelMode.FALSTC:
                    case Pds_recParam.CancelMode.FCLSTC:
                    case Pds_recParam.CancelMode.FRLSTC:
                        Title = "整張取消";
                        break;
                    case Pds_recParam.CancelMode.FABAGC:
                    case Pds_recParam.CancelMode.FCBAGC:
                    case Pds_recParam.CancelMode.FRBAGC:
                        Title = "單筆取消";
                        break;
                }
                ReasonList = null; // 重抓
            }
        }

        private string _title;
        /// <summary>
        /// 標題
        /// </summary>
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private string _pds_rec_reason;
        [Display(Name = "原因")]
        public string pds_rec_reason
        {
            get => _pds_rec_reason;
            set
            {
                Set(ref _pds_rec_reason, value);

                OtherVisibility = _pds_rec_reason == Pds_recParam.Rec_reason.C99 ?
                    Visibility.Visible : Visibility.Collapsed;
                if (OtherVisibility == Visibility.Collapsed)
                    pds_rec_reason_oth = "";

                MdVisibility = (_pds_rec_reason == Pds_recParam.Rec_reason.C04 ||
                    _pds_rec_reason == Pds_recParam.Rec_reason.C08 ||
                     _pds_rec_reason == Pds_recParam.Rec_reason.C11) ?
                    Visibility.Visible : Visibility.Collapsed;
                if (MdVisibility == Visibility.Collapsed)
                {
                    pds_rec_md_qty = "";
                    pds_rec_md_way1 = "";
                }

                Err_mst_idVisibility = _pds_rec_reason == Pds_recParam.Rec_reason.C06 ?
                    Visibility.Visible : Visibility.Collapsed;
                if (Err_mst_idVisibility == Visibility.Collapsed)
                {
                    pds_recd_err_mst_id = "";
                    chprs_id_name = "";
                }

                Err_qtyVisibility = _pds_rec_reason == Pds_recParam.Rec_reason.C07 ?
                    Visibility.Visible : Visibility.Collapsed;
                if (Err_mst_idVisibility == Visibility.Collapsed)
                    pds_recd_err_qty = "";
            }
        }

        private string _pds_rec_reason_oth;
        [Display(Name = "原因(其他)")]
        [MaxLength(60)]
        public string pds_rec_reason_oth
        {
            get => _pds_rec_reason_oth;
            set => Set(ref _pds_rec_reason_oth, value);
        }

        private string _pds_rec_md_qty;
        [Display(Name = "總量/總包")]
        [MaxLength(7)]
        public string pds_rec_md_qty
        {
            get => _pds_rec_md_qty;
            set => Set(ref _pds_rec_md_qty, value);
        }

        private string _pds_rec_md_way1;
        [Display(Name = "頻次")]
        [MaxLength(5)]
        public string pds_rec_md_way1
        {
            get => _pds_rec_md_way1;
            set => Set(ref _pds_rec_md_way1, value);
        }

        private string _pds_recd_err_mst_id;
        [Display(Name = "錯誤藥品代碼")]
        [MaxLength(9)]
        public string pds_recd_err_mst_id
        {
            get => _pds_recd_err_mst_id;
            set => Set(ref _pds_recd_err_mst_id, value);
        }

        private string _chprs_id_name;
        [Display(Name = "錯誤藥品名稱")]
        public string chprs_id_name
        {
            get => _chprs_id_name;
            set => Set(ref _chprs_id_name, value);
        }

        private string _pds_recd_err_qty;
        [Display(Name = "錯誤總量/總包")]
        [MaxLength(7)]
        public string pds_recd_err_qty
        {
            get => _pds_recd_err_qty;
            set => Set(ref _pds_recd_err_qty, value);
        }

        private ObservableCollection<Rec_code> _reasonList;
        /// <summary>
        /// 取消原因設定檔
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
                    new Rec_code { rec_code_model = "pds_rec_reason", rec_code_group = "C", rec_code_st = "1" },
                    new { option = 1 }); // 1: 依參數自動組建

                    HashSet<string> filter = null;
                    switch (CancelMode)
                    {
                        case Pds_recParam.CancelMode.ABAGC:
                            filter = new HashSet<string> { "C01", "C02", "C03", "C04", "C05", "C99" };
                            break;
                        case Pds_recParam.CancelMode.CBAGC:
                            filter = new HashSet<string> { "C03", "C05", "C06", "C07", "C08", "C99" };
                            break;
                        case Pds_recParam.CancelMode.CBAGCG:
                            filter = new HashSet<string> { "C03", "C05", "C08", "C99" };
                            break;
                        case Pds_recParam.CancelMode.CLSTC:
                        case Pds_recParam.CancelMode.FALSTC:
                        case Pds_recParam.CancelMode.FCLSTC:
                        case Pds_recParam.CancelMode.FRLSTC:
                            filter = new HashSet<string> { "C03", "C99" };
                            break;
                        case Pds_recParam.CancelMode.FABAGC:
                            filter = new HashSet<string> { "C01",  "C03", "C04", "C05", "C99" };
                            break;
                        case Pds_recParam.CancelMode.FCBAGC:
                            filter = new HashSet<string> {  "C03", "C05", "C06", "C07", "C08",  "C99" };
                            break;
                        case Pds_recParam.CancelMode.FRBAGC:
                            filter = new HashSet<string> { "C03", "C05", "C06", "C07", "C11", "C99" };
                            break;
                    }
                    result.Data = result.Data.Where(c => filter.Contains(c.rec_code_short)).ToList();
                    _reasonList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _reasonList;
            }
            set => Set(ref _reasonList, value);
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

        private Visibility _otherVisibility = Visibility.Collapsed;
        /// <summary>
        /// 其他：Visibility
        /// </summary>
        public Visibility OtherVisibility
        {
            get => _otherVisibility;
            set => Set(ref _otherVisibility, value);
        }

        private Visibility _mdVisibility = Visibility.Collapsed;
        /// <summary>
        /// 處方修改、核對處方修改：Visibility
        /// </summary>
        public Visibility MdVisibility
        {
            get => _mdVisibility;
            set => Set(ref _mdVisibility, value);
        }

        private Visibility _err_mst_idVisibility = Visibility.Collapsed;
        /// <summary>
        /// 錯誤藥品：Visibility
        /// </summary>
        public Visibility Err_mst_idVisibility
        {
            get => _err_mst_idVisibility;
            set => Set(ref _err_mst_idVisibility, value);
        }

        private Visibility _err_qtyVisibility = Visibility.Collapsed;
        /// <summary>
        /// 錯誤總量/總包：Visibility
        /// </summary>
        public Visibility Err_qtyVisibility
        {
            get => _err_qtyVisibility;
            set => Set(ref _err_qtyVisibility, value);
        }


        private DelegateCommand<string> _selectPrsCommand;
        public DelegateCommand<string> SelectPrsCommand =>
            _selectPrsCommand ?? (_selectPrsCommand = new DelegateCommand<string>
            (OnSelectPrs));
        /// <summary>
        /// 選取藥品代碼，顯示藥品名稱
        /// </summary>
        public void OnSelectPrs(string selected_mst_id)
        {
            var prs = PrsList.FirstOrDefault(p => p.chprs_mst_id == selected_mst_id);
            chprs_id_name = prs?.chprs_id_name ?? string.Empty;
        }

        private DelegateCommand _okCommand;
        public DelegateCommand OKCommand =>
            _okCommand ?? (_okCommand = new DelegateCommand
            (OnOK, () => Validate().IsValid));
        private void OnOK() =>
            DialogResult = true;
    }
}
