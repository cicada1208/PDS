using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WpfLib;

namespace ViewModels
{
    public class PdsRecCheckViewModel : BaseViewModel<PdsRecCheckViewModel>
    {
        private Pds_rec _rec;
        /// <summary>
        /// 藥車核對記錄
        /// </summary>
        public Pds_rec Rec
        {
            get => _rec ?? (_rec = new Pds_rec());
            set => Set(ref _rec, value);
        }

        private Pds_rec _typeG_op_dtm;
        /// <summary>
        /// 自包機核對藥品起迄時間
        /// </summary>
        public Pds_rec TypeG_op_dtm
        {
            get => _typeG_op_dtm ?? (_typeG_op_dtm = new Pds_rec());
            set => Set(ref _typeG_op_dtm, value);
        }

        private PdsPatInfo _pdsPatInfo;
        /// <summary>
        ///病人資訊
        /// </summary>
        public PdsPatInfo PdsPatInfo
        {
            get => _pdsPatInfo ?? (_pdsPatInfo = new PdsPatInfo());
            set => Set(ref _pdsPatInfo, value);
        }

        /// <summary>
        /// 病人是否出院
        /// </summary>
        public bool IsMBD
        {
            get => PdsPatInfo?.chudrecchk_bed == ClinicalParam.MBD;
        }

        private PdsMedInfo _pdsMedInfo;
        /// <summary>
        ///藥品資訊
        /// </summary>
        public PdsMedInfo PdsMedInfo
        {
            get
            {
                if (_pdsMedInfo == null)
                {
                    _pdsMedInfo = new PdsMedInfo();
                    _pdsMedInfo.SaveMEDV = OnMEDVTypeNonG;
                }
                _pdsMedInfo.opMode = PdsMedInfoParam.OpMode.C;
                return _pdsMedInfo;
            }
            set
            {
                Set(ref _pdsMedInfo, value);
                if (_pdsMedInfo != null && _pdsMedInfo.SaveMEDV == null)
                    _pdsMedInfo.SaveMEDV = OnMEDVTypeNonG;
            }
        }

        private Visibility _pdsMedInfoVisibility = Visibility.Collapsed;
        /// <summary>
        ///藥品資訊：Visibility
        /// </summary>
        public Visibility PdsMedInfoVisibility
        {
            get => _pdsMedInfoVisibility;
            set => Set(ref _pdsMedInfoVisibility, value);
        }

        private PdsBcodeInfoViewModel _pdsBcodeInfoViewModel;
        /// <summary>
        ///自包機(餐包)資訊
        /// </summary>
        public PdsBcodeInfoViewModel PdsBcodeInfoViewModel
        {
            get
            {
                if (_pdsBcodeInfoViewModel == null)
                {
                    _pdsBcodeInfoViewModel = new PdsBcodeInfoViewModel();
                    _pdsBcodeInfoViewModel.SaveMEDV = OnMEDVTypeG;
                }
                return _pdsBcodeInfoViewModel;
            }
            set
            {
                Set(ref _pdsBcodeInfoViewModel, value);
                if (_pdsBcodeInfoViewModel != null && _pdsBcodeInfoViewModel.SaveMEDV == null)
                    _pdsBcodeInfoViewModel.SaveMEDV = OnMEDVTypeG;
            }
        }

        private Visibility _pdsBcodeInfoVisibility = Visibility.Collapsed;
        /// <summary>
        ///自包機(餐包)資訊：Visibility
        /// </summary>
        public Visibility PdsBcodeInfoVisibility
        {
            get => _pdsBcodeInfoVisibility;
            set => Set(ref _pdsBcodeInfoVisibility, value);
        }

        private bool _pdsBcodeInfoScrollToTop;
        /// <summary>
        ///自包機(餐包)資訊：ScrollToTop
        /// </summary>
        public bool PdsBcodeInfoScrollToTop
        {
            get => _pdsBcodeInfoScrollToTop;
            set => Set(ref _pdsBcodeInfoScrollToTop, value);
        }

        private ObservableCollection<Mi_micbcode> _orderList;
        /// <summary>
        /// 處方明細清單
        /// </summary>
        public ObservableCollection<Mi_micbcode> OrderList
        {
            get => _orderList;
            set
            {
                Set(ref _orderList, value);
                if (_orderList != null)
                {
                    BAGCOrderList = new ObservableCollection<Mi_micbcode>(
                        _orderList.Where(o => o.icbcode_med_type == Mi_micbcodeParam.Med_type.G).ToList());

                    AtcVisibility = _orderList.Where(o => !o.atc_code_prefix5.IsNullOrWhiteSpace()).Count() > 0 ?
                         Visibility.Visible : Visibility.Collapsed;
                }
                else
                {
                    BAGCOrderList = null;
                    AtcVisibility = Visibility.Collapsed;
                }
            }
        }

        private Mi_micbcode _selectedOrder;
        /// <summary>
        /// 處方明細選取
        /// </summary>
        public Mi_micbcode SelectedOrder
        {
            get => _selectedOrder;
            set => Set(ref _selectedOrder, value);
        }

        private ObservableCollection<Mi_micbcode> _BAGCOrderList;
        /// <summary>
        /// 單筆取消藥品清單(處方明細自包機項目)
        /// </summary>
        public ObservableCollection<Mi_micbcode> BAGCOrderList
        {
            get => _BAGCOrderList;
            set => Set(ref _BAGCOrderList, value);
        }

        private Mi_micbcode _selectedBAGCOrder;
        /// <summary>
        /// 單筆取消藥品選取
        /// </summary>
        public Mi_micbcode SelectedBAGCOrder
        {
            get => _selectedBAGCOrder;
            set => Set(ref _selectedBAGCOrder, value);
        }

        private ObservableCollection<Mch_msen> _msenList;
        /// <summary>
        /// 用藥過敏暨不良反應清單
        /// </summary>
        public ObservableCollection<Mch_msen> MsenList
        {
            get => _msenList;
            set => Set(ref _msenList, value);
        }

        private ObservableCollection<Mr_lstud> _lstudList;
        /// <summary>
        /// 交互作用
        /// </summary>
        public ObservableCollection<Mr_lstud> LstudList
        {
            get => _lstudList;
            set
            {
                Set(ref _lstudList, value);
                LstudVisibility = (_lstudList != null && _lstudList.Count > 0) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _lstudVisibility = Visibility.Collapsed;
        /// <summary>
        /// 交互作用：Visibility
        /// </summary>
        public Visibility LstudVisibility
        {
            get => _lstudVisibility;
            set => Set(ref _lstudVisibility, value);
        }

        private Visibility _atcVisibility = Visibility.Collapsed;
        /// <summary>
        /// 同藥理：Visibility
        /// </summary>
        public Visibility AtcVisibility
        {
            get => _atcVisibility;
            set => Set(ref _atcVisibility, value);
        }

        private bool _clinicalSelectFocused = true;
        /// <summary>
        /// 選取護理站按鈕：Focused
        /// </summary>
        public bool ClinicalSelectFocused
        {
            get => _clinicalSelectFocused;
            set => Set(ref _clinicalSelectFocused, value);
        }

        private string _clinical;
        /// <summary>
        /// 護理站
        /// </summary>
        public string Clinical
        {
            get => _clinical;
            set => Set(ref _clinical, value?.Trim());
        }

        private string _sendDt;
        /// <summary>
        /// 傳送日期
        /// </summary>
        public string SendDt
        {
            get => _sendDt;
            set => Set(ref _sendDt, value?.Trim());
        }

        private string _lstCode;
        /// <summary>
        /// 配藥單條碼
        /// </summary>
        public string LstCode
        {
            get => _lstCode;
            set => Set(ref _lstCode, value?.Trim());
        }

        private bool _lstCodeFocused;
        /// <summary>
        /// 配藥單條碼：Focused
        /// </summary>
        public bool LstCodeFocused
        {
            get => _lstCodeFocused;
            set => Set(ref _lstCodeFocused, value);
        }

        private bool _lstCodeEnabled;
        /// <summary>
        /// 配藥單條碼：Enabled
        /// </summary>
        public bool LstCodeEnabled
        {
            get => _lstCodeEnabled;
            set => Set(ref _lstCodeEnabled, value);
        }

        private string _bedCode;
        /// <summary>
        /// 藥盒(床號)條碼
        /// </summary>
        public string BedCode
        {
            get => _bedCode;
            set => Set(ref _bedCode, value?.Trim());
        }

        private bool _bedCodeFocused;
        /// <summary>
        /// 藥盒(床號)條碼：Focused
        /// </summary>
        public bool BedCodeFocused
        {
            get => _bedCodeFocused;
            set => Set(ref _bedCodeFocused, value);
        }

        private bool _bedCodeEnabled;
        /// <summary>
        /// 藥盒(床號)條碼：Enabled
        /// </summary>
        public bool BedCodeEnabled
        {
            get => _bedCodeEnabled;
            set => Set(ref _bedCodeEnabled, value);
        }

        private string _bagCode;
        /// <summary>
        /// 藥袋條碼
        /// </summary>
        public string BagCode
        {
            get => _bagCode;
            set => Set(ref _bagCode, value?.Trim());
        }

        private bool _bagCodeFocused;
        /// <summary>
        /// 藥袋條碼：Focused
        /// </summary>
        public bool BagCodeFocused
        {
            get => _bagCodeFocused;
            set => Set(ref _bagCodeFocused, value);
        }

        private bool _bagCodeEnabled;
        /// <summary>
        /// 藥袋條碼：Enabled
        /// </summary>
        public bool BagCodeEnabled
        {
            get => _bagCodeEnabled;
            set => Set(ref _bagCodeEnabled, value);
        }

        private bool _LSTCEnabled;
        /// <summary>
        /// 整張取消：Enabled
        /// </summary>
        public bool LSTCEnabled
        {
            get => _LSTCEnabled;
            set => Set(ref _LSTCEnabled, value);
        }

        private bool _LSTREnabled;
        /// <summary>
        /// 重新核對：Enabled
        /// </summary>
        public bool LSTREnabled
        {
            get => _LSTREnabled;
            set => Set(ref _LSTREnabled, value);
        }

        private bool _BAGCEnabled;
        /// <summary>
        /// 單筆取消：Enabled
        /// </summary>
        public bool BAGCEnabled
        {
            get => _BAGCEnabled;
            set => Set(ref _BAGCEnabled, value);
        }

        private bool _CCAREndEnabled;
        /// <summary>
        /// 藥車核對完成：Enabled
        /// </summary>
        public bool CCAREndEnabled
        {
            get => _CCAREndEnabled;
            set => Set(ref _CCAREndEnabled, value);
        }

        private ObservableCollection<Rec_code> _reasonList;
        /// <summary>
        /// 狀態原因設定檔
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
                    _reasonList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _reasonList;
            }
            set => Set(ref _reasonList, value);
        }

        private ObservableCollection<Rec_code> _stList;
        /// <summary>
        /// 狀態設定檔
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
                    _stList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _stList;
            }
            set => Set(ref _stList, value);
        }

        private ObservableCollection<Rec_code> _opdList;
        /// <summary>
        /// 動作類型設定檔
        /// </summary>
        public ObservableCollection<Rec_code> OpdList
        {
            get
            {
                if (_opdList == null)
                {
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Rec_code>>>(
                    RouteParam.Service(),
                    RouteParam.Rec_code.QueryRec_code,
                    new Rec_code { rec_code_model = "pds_recd_op_type", rec_code_group = "pds_recd_op_type", rec_code_st = "1" },
                    new { option = 1 }); // 1 依參數自動組建
                    _opdList = new ObservableCollection<Rec_code>(result.Data);
                }
                return _opdList;
            }
            set => Set(ref _opdList, value);
        }


        /// <summary>
        /// 顯示取消畫面
        /// </summary>
        public Func<Pds_recParam.CancelMode, PdsRecCancelViewModel> ShowPdsRecCancelWindow;

        /// <summary>
        /// 顯示同藥理畫面
        /// </summary>
        public Action<List<Mi_micbcode>> ShowAtcCodeGroupWindow;

        /// <summary>
        /// 顯示交互作用畫面
        /// </summary>
        public Action<ObservableCollection<Mr_lstud>> ShowLstudWindow;

        /// <summary>
        /// 顯示通報畫面
        /// </summary>
        public Func<Pds_note, bool> ShowPdsNoteEditWindow;

        private DelegateCommand _clinicalSelectCommand;
        public DelegateCommand ClinicalSelectCommand =>
            _clinicalSelectCommand ?? (_clinicalSelectCommand = new DelegateCommand
            (OnClinicalSelect));
        /// <summary>
        /// 選取護理站
        /// </summary>
        private void OnClinicalSelect()
        {
            Window win = Util.Ctrl.GetWindow("Controls.PdsUdrecChkClinicalWindow");
            var result = win?.ShowDialog();
            if ((!result.HasValue) || (!result.Value)) return;
            var vm = win.DataContext as PdsUdrecChkClinicalViewModel;
            string clinical = vm.SelectedClinical.chudrecchk_bed_unit;
            string sendDt = DateTimeUtil.ConvertROC(vm.SelectedClinical.chudrecchk_date.NullableToStr());
            win?.Close(); win = null;
            //if (Clinical == clinical && SendDt == sendDt) return;

            Clinical = clinical;
            SendDt = sendDt;
            CCAREndEnabled = true;
            LstCodeReset(true, true);
            BedCodeReset();
            BagCodeReset();
        }

        private DelegateCommand _lstCodeCommand;
        public DelegateCommand LstCodeCommand =>
            _lstCodeCommand ?? (_lstCodeCommand = new DelegateCommand
            (OnLstCode));
        /// <summary>
        /// 刷配藥單條碼
        /// </summary>
        private void OnLstCode()
        {
            var msg = string.Empty;
            int send_dt = LstCode.SubStr(0, 7).ToInt();
            string ipd_no = LstCode.SubStr(7, 11);

            ApiResult<PdsPatInfo> pdsPatInfoResult =
              ApiUtil.HttpClientEx<ApiResult<PdsPatInfo>>(
                RouteParam.Service(),
                RouteParam.PdsPatInfo.QueryPdsPatInfo,
                new PdsPatInfo { lst_code = LstCode },
                new { option = 2 }); // 2: 刷配藥單條碼，取得病人資訊

            if (!pdsPatInfoResult.Succ)
            {
                msg = pdsPatInfoResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }

            var trans4SResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec_S,
            new Pds_rec
            {
                pds_rec_lst_code = LstCode,
                pds_rec_md_pc = HostUtil.GetHostNameAndAddress(),
                pds_rec_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr()
            },
            new { option = 1 }); // 1: 藥車轉入4級管制藥調劑資訊

            //ApiResult<List<Pds_rec>> adjustResult =
            //  ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
            //    RouteParam.Service(),
            //    RouteParam.Pds_rec.QueryPds_rec,
            //    new Pds_rec { pds_rec_lst_code = LstCode }, 
            //    new { option = 7 }); // 7: 依配藥單條碼，查詢記錄是否完成調劑

            // 案例：配藥單只有公藥等不需調劑的藥，故改此判斷
            ApiResult<bool> adjustResult =
              ApiUtil.HttpClientEx<ApiResult<bool>>(
                RouteParam.Service(),
                RouteParam.Mi_micbcode.QueryLstComplete,
                new Mi_micbcode { icbcode_send_dt = send_dt, icbcode_ipd_no = ipd_no },
                new { option = 2 }); // 2: 依配藥單條碼(傳送日期、住院序號)，查詢需調劑藥袋的狀態

            if (!adjustResult.Succ)
            {
                msg = adjustResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }
            else if (!adjustResult.Data)
            {
                msg = MsgParam.PdsRecAdjustNotDone;
                LstCodeReset(true, true);
                goto exit;
            }

            ApiResult<List<Mi_micbcode>> orderListResult =
              ApiUtil.HttpClientEx<ApiResult<List<Mi_micbcode>>>(
                RouteParam.Service(),
                RouteParam.Mi_micbcode.QueryMi_micbcode,
                new Mi_micbcode { icbcode_send_dt = send_dt, icbcode_ipd_no = ipd_no },
                new { option = 5 }); // 5: 依配藥單條碼(傳送日期、住院序號)，查詢處方明細及核對藥袋的狀態

            if (!orderListResult.Succ)
            {
                msg = orderListResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }

            ApiResult<List<Mch_msen>> msenListResult =
              ApiUtil.HttpClientEx<ApiResult<List<Mch_msen>>>(
                RouteParam.Service(),
                RouteParam.Mch_msen.QueryMch_msen,
                new Mch_msen { chsen_pat_no = pdsPatInfoResult.Data.pat_no.ToNullableInt() },
                new { option = 2 }); // 2: 依病歷號，查詢用藥過敏暨不良反應

            if (!msenListResult.Succ)
            {
                msg = msenListResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }

            ApiResult<List<Mr_lstud>> lstudListResult =
              ApiUtil.HttpClientEx<ApiResult<List<Mr_lstud>>>(
                RouteParam.Service(),
                RouteParam.Mr_lstud.QueryMr_lstud,
                new Mr_lstud { icbcode_send_dt = send_dt, icbcode_ipd_no = ipd_no },
                new { option = 2 }); // 2: 依配藥單條碼(傳送日期、住院序號)，查詢配藥單交互作用

            if (!lstudListResult.Succ)
            {
                msg = lstudListResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }

            PdsPatInfo = pdsPatInfoResult.Data;
            OrderList = new ObservableCollection<Mi_micbcode>(orderListResult.Data);
            MsenList = new ObservableCollection<Mch_msen>(msenListResult.Data);
            LstudList = new ObservableCollection<Mr_lstud>(lstudListResult.Data);

            if (PdsPatInfo.chudrec_bed != PdsPatInfo.chudrecchk_bed)
                msg += $"轉床：{Util.Media.SpeechBedNoStr(PdsPatInfo.chudrecchk_bed)}。";

            if ((!IsMBD) && Clinical != PdsPatInfo.chudrecchk_bed_unit)
            { // 點選護理站與配藥單新護理站不同，提示且配藥單不鎖
                msg += msg.IsNullOrWhiteSpace() ? "" : Environment.NewLine;
                msg += $"選取護理站 {Clinical} 與病人核對時護理站 {PdsPatInfo.chudrecchk_bed_unit} 不同。";
                LstCodeReset(true, true);
                goto exit;
            }
            else if (DateTimeUtil.ConvertAD(SendDt, inFormat: "yyyy/MM/dd", outFormat: "yyyMMdd").ToInt() != send_dt)
            {
                msg += msg.IsNullOrWhiteSpace() ? "" : Environment.NewLine;
                msg += $"選取日期 {SendDt} 與配藥單日期 {DateTimeUtil.ConvertROC(send_dt.NullableToStr())} 不同。";
                LstCodeReset(true, true);
                goto exit;
            }

            if (IsMBD)
            {
                var udrecchkResult = ApiUtil.HttpClientEx<ApiResult<Ch_udrec_chk>>(
                    RouteParam.Service(),
                    RouteParam.Ch_udrec_chk.UpdateCh_udrec_chk,
                    new Ch_udrec_chk
                    {
                        chudrecchk_date = send_dt,
                        chudrecchk_ipd_no = ipd_no,
                        chudrecchk_bed = ClinicalParam.MBD,
                        chudrecchk_bed_unit = ClinicalParam.MBD
                    },
                    new { option = 2 }); // 2: 依傳送日期、住院序號，更新護理站、床號

                if (!udrecchkResult.Succ)
                {
                    msg = udrecchkResult.Msg;
                    LstCodeReset(true, true);
                    goto exit;
                }
            }

            ApiResult<List<Pds_rec>> checkResult =
              ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPds_rec,
                new Pds_rec { pds_rec_lst_code = LstCode },
                new { option = 8 }); // 8: 依配藥單條碼，查詢記錄是否完成核對

            if (!checkResult.Succ)
            {
                msg = checkResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }
            else if (checkResult.Data.Count > 0)
            {
                var LSTRResult = LSTR(true);
                if (LSTRResult != MessageBoxResult.OK)
                {
                    LstCodeReset(true, true);
                    goto exit; ;
                }
            }

            SetRec("");
            SetRec(Pds_recParam.Rec_op_type.CLST, Pds_recParam.Recd_op_type.LST,
                null, "",
                LstCode, "", "",
                Pds_recParam.Rec_st.U);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 5 }); // 5: 藥車核對作業，CLST 儲存

            if (!saveResult.Succ)
            {
                msg = saveResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }
            else if (PdsPatInfo.chudrecchk_bed_unit == ClinicalParam.PSY)
            { // PSY藥盒自動帶入該病人床號，因PSY實際為裝在大袋子裡。
                LstCodeEnabled = false;
                LSTCEnabled = true;
                BedCode = PdsPatInfo.chudrecchk_bed;
                OnBedCode();
            }
            else
            {
                LstCodeEnabled = false;
                BedCodeEnabled = true;
                BedCodeFocused = true;
                LSTCEnabled = true;
            }

        exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        private DelegateCommand _bedCodeCommand;
        public DelegateCommand BedCodeCommand =>
            _bedCodeCommand ?? (_bedCodeCommand = new DelegateCommand
            (OnBedCode));
        /// <summary>
        /// 刷藥盒條碼
        /// </summary>
        private void OnBedCode()
        {
            var msg = string.Empty;

            SetRec("");

            if (PdsPatInfo.chudrecchk_bed != BedCode)
            {
                SetRec(Pds_recParam.Rec_op_type.CLST, Pds_recParam.Recd_op_type.BED,
                    null, "",
                    BedCode, "", "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N05);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 5 }); // 5: 藥車核對作業，CLST 儲存

                msg = GetReasonName(Pds_recParam.Rec_reason.N05);
                BedCodeReset(true, true);
            }
            else
            {
                // 案例：配藥單只有公藥、1-3級管制藥等不需核對的藥，刷配藥單 & 藥盒 => 結束
                var notNeedType = new HashSet<string> { Mi_micbcodeParam.Med_type.S, Mi_micbcodeParam.Med_type.V };
                var st = OrderList.Where(o => !notNeedType.Contains(o.icbcode_med_type)).Count() > 0 ?
                    Pds_recParam.Rec_st.U : Pds_recParam.Rec_st.Y;

                SetRec(Pds_recParam.Rec_op_type.CLST, Pds_recParam.Recd_op_type.BED,
                    null, "",
                    BedCode, "", "",
                    st);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 5 }); // 5: 藥車核對作業，CLST 儲存

                if (!saveResult.Succ)
                {
                    msg = saveResult.Msg;
                    BedCodeReset(true, true);
                }
                else
                {
                    if (st == Pds_recParam.Rec_st.U)
                    {
                        BedCodeEnabled = false;
                        BagCodeEnabled = true;
                        BagCodeFocused = true;
                        LSTREnabled = true;
                    }
                    else
                    {
                        Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
                        CheckLstComplete();
                    }
                }
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        private DelegateCommand _bagCodeCommand;
        public DelegateCommand BagCodeCommand =>
            _bagCodeCommand ?? (_bagCodeCommand = new DelegateCommand
            (OnBagCode));
        /// <summary>
        /// 刷藥袋條碼
        /// </summary>
        private void OnBagCode()
        {
            if (BagCode.StartsWith("ICBC") || BagCode.Length == 16)
                OnBagCodeTypeNonG();
            else
                OnBagCodeTypeG();
        }

        /// <summary>
        /// 刷藥袋條碼(非自包機)
        /// </summary>
        private void OnBagCodeTypeNonG()
        {
            var msg = string.Empty;
            bool doMEDV = false;
            string bagCode = BagCode;

            SetRec("");

            if (!BagCode.StartsWith("ICBC"))
                bagCode = "ICBC" + BagCode;

            ApiResult<PdsMedInfo> pdsMedInfoResult =
              ApiUtil.HttpClientEx<ApiResult<PdsMedInfo>>(
                RouteParam.Service(),
                RouteParam.PdsMedInfo.QueryPdsMedInfo,
                new { mi_micbcode = new Mi_micbcode() { icbcode_code = bagCode } },
                new { option = 1 }); // 1: 刷藥袋條碼，取得藥品資訊
            PdsMedInfo = pdsMedInfoResult.Data;
            PdsMedInfoVisibility = Visibility.Visible;
            PdsBcodeInfoVisibility = Visibility.Collapsed;

            SelectedOrder = OrderList.FirstOrDefault(o => o.icbcode_code == bagCode);
            bool errPat = SelectedOrder == null;

            if (!pdsMedInfoResult.Succ)
            {
                msg = pdsMedInfoResult.Msg;
                BagCodeReset(true, true);
            }
            else if (errPat)
            {   // N06應記錄在該病人配藥單之下
                SetRec(Pds_recParam.Rec_op_type.CLST, Pds_recParam.Recd_op_type.BAG,
                    null, "",
                    BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N06);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 5 }); // 5: 藥車核對作業，CLST 儲存

                msg = GetReasonName(Pds_recParam.Rec_reason.N06);
                BagCodeReset(true, true);
            }
            else
            {
                SetRec(Pds_recParam.Rec_op_type.CBAG, Pds_recParam.Recd_op_type.BAG,
                    PdsMedInfo.mi_micbcode, "",
                    BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                    Pds_recParam.Rec_st.U);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 1 }); // 1: 藥車調劑/核對作業，排除備註欄位儲存

                if (!saveResult.Succ)
                {
                    msg = saveResult.Msg;
                    BagCodeReset(true, true);
                }
                else
                {
                    if (PdsMedInfo.mdInfo != null)
                    {
                        msg += $"{GetReasonName(PdsMedInfo.mdInfo.pds_recd_reason)}：";
                        msg += $"{(PdsMedInfo.mi_micbcode.icbcode_med_type_p ? "總包" : "總量")}：{PdsMedInfo.mdInfo.pds_recd_md_qty}，";
                        msg += $"頻次：{PdsMedInfo.mdInfo.pds_recd_md_way1.ConcatSeparator()}{Environment.NewLine}";
                    }
                    if (PdsMedInfo.ch_prs.chprs_give_dilu == "Y")
                        msg += $"另給稀釋液：{PdsMedInfo.ch_prs.chprs_give_dilu_note}{Environment.NewLine}";

                    // 無法交車註記：訊息確認完後，該藥結束核對。
                    // 處方DC：訊息確認完後，該藥結束核對。
                    ApiResult<List<Pds_rec>> abagResult =
                      ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
                        RouteParam.Service(),
                        RouteParam.Pds_rec.QueryPds_rec,
                        new Pds_rec { pds_rec_op_type = Pds_recParam.Rec_op_type.ABAG, pds_rec_bag_code = PdsMedInfo.mi_micbcode.icbcode_code },
                        new { option = 1 }); // 1: 依參數自動組建

                    var abag = abagResult.Data?.FirstOrDefault();
                    if (abag != null && abag.pds_rec_st == Pds_recParam.Rec_st.S)
                    {
                        msg += $"{GetStName(abag.pds_rec_st)}：{abag.pds_rec_nondeliver}{Environment.NewLine}";
                        doMEDV = true;
                    }
                    if (abag != null && abag.pds_rec_reason == Pds_recParam.Rec_reason.C05)
                    {
                        msg += $"{GetReasonName(abag.pds_rec_reason)}{Environment.NewLine}";
                        doMEDV = true;
                    }

                    if (IsMBD)
                    {
                        msg += $"{GetOpdName(Pds_recParam.Recd_op_type.MEDC)}{Environment.NewLine}";
                        doMEDV = true;
                    }

                    BagCodeEnabled = false;
                    BAGCEnabled = true;
                }
            }

            if (msg != string.Empty)
            {
                Util.Media.MsgSpeech(msg);
                BagCodeFocused = false; // 訊息前 BagCodeFocused = true，MBD訊息確認後仍為true，予重置
            }

            if (doMEDV) OnMEDVTypeNonG();
        }

        /// <summary>
        /// 刷藥袋條碼(自包機)
        /// </summary>
        private void OnBagCodeTypeG()
        {
            var msg = string.Empty;
            bool doMEDV = false;

            SetRec("");

            if ((!BagCode.IsNumeric()) || BagCode.Length != 18)
            {
                msg = MsgParam.PdsRecNoBagCode;
                BagCodeReset(true, true);
                goto exit;
            }

            var bcode_code_pat_no = BagCode.Substring(0, 8);
            var bcode_code_rx_dt = BagCode.Substring(8, 7);
            var bcode_code_rx_hh = BagCode.Substring(15, 2);
            var bcode_code_page = BagCode.Substring(17, 1);

            string send_dt = LstCode.SubStr(0, 7);
            string ipd_no = LstCode.SubStr(7, 11);

            ApiResult<List<Ch_bcode>> bcodeResult =
              ApiUtil.HttpClientEx<ApiResult<List<Ch_bcode>>>(
                RouteParam.Service(),
                RouteParam.Ch_bcode.QueryCh_bcode,
                new { bcode_code_pat_no, bcode_code_rx_dt, bcode_code_rx_hh, bcode_code_page, bcode_ipd_no = ipd_no },
                new { option = 4 });  // 4: 依條碼及住院序號 // 處理 PSY 病人週四時會於 ch_bcode 自包機展開 7 天的資料，但後來轉至其他單位

            if (!bcodeResult.Succ)
            {
                msg = bcodeResult.Msg;
                BagCodeReset(true, true);
                goto exit;
            }
            else if (bcodeResult.Data.Count == 0)
            {
                msg = MsgParam.PdsRecNoBagCode;
                BagCodeReset(true, true);
                goto exit;
            }

            var bcode = bcodeResult.Data.FirstOrDefault();
            bool errPat = bcode == null || bcode.bcode_send_dt != send_dt || bcode.bcode_ipd_no != ipd_no;

            if (errPat)
            {
                SetRec(Pds_recParam.Rec_op_type.CLST, Pds_recParam.Recd_op_type.BAG,
                    null, "",
                    BagCode, bcode.bcode_fee_key, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N06);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 5 }); // 5: 藥車核對作業，CLST 儲存

                msg = GetReasonName(Pds_recParam.Rec_reason.N06);
                BagCodeReset(true, true);
                goto exit;
            }

            var pdsBcodeInfoViewModel = new PdsBcodeInfoViewModel()
            {
                bcode_send_dt = send_dt,
                bcode_ipd_no = ipd_no,
                bcode_code_pat_no = PdsPatInfo.pat_no.ToNullableInt()
            };
            PdsBcodeInfoViewModel = pdsBcodeInfoViewModel;
            PdsMedInfoVisibility = Visibility.Collapsed;
            PdsBcodeInfoVisibility = Visibility.Visible;
            PdsBcodeInfoScrollToTop = true;
            BagCodeEnabled = false;
            BAGCEnabled = true;
            TypeG_op_dtm.pds_rec_op_dtm_begin = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

            if (IsMBD)
            {
                msg = $"{GetOpdName(Pds_recParam.Recd_op_type.MEDC)}{Environment.NewLine}";
                doMEDV = true;
            }

        exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);

            if (doMEDV) OnMEDVTypeG();
        }

        /// <summary>
        /// 核對藥品(非自包機)
        /// </summary>
        private void OnMEDVTypeNonG()
        {
            var msg = string.Empty;

            SetRec("");

            string recd_op_type = IsMBD ? Pds_recParam.Recd_op_type.MEDC : Pds_recParam.Recd_op_type.MEDV;
            SetRec(Pds_recParam.Rec_op_type.CBAG, recd_op_type,
                PdsMedInfo.mi_micbcode, "",
                BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                Pds_recParam.Rec_st.Y);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 1 }); // 1: 藥車調劑/核對作業，排除備註欄位儲存

            if (!saveResult.Succ)
            {
                msg = saveResult.Msg;
                SetOrderComplete(PdsMedInfo.mi_micbcode.icbcode_code, Pds_recParam.Rec_st.U);
                BagCodeReset(true, true);
            }
            else
            {
                SetOrderComplete(PdsMedInfo.mi_micbcode.icbcode_code, Pds_recParam.Rec_st.Y);
                Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
                CheckLstComplete();
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 核對藥品(自包機)
        /// </summary>
        private void OnMEDVTypeG()
        {
            var msg = string.Empty;

            TypeG_op_dtm.pds_rec_op_dtm_end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            string recd_op_type = IsMBD ? Pds_recParam.Recd_op_type.MEDC : Pds_recParam.Recd_op_type.MEDV;
            foreach (var order in OrderList.Where(ic => ic.icbcode_med_type == Mi_micbcodeParam.Med_type.G))
            {
                SetRec("");
                SetRec(Pds_recParam.Rec_op_type.CBAG, Pds_recParam.Recd_op_type.BAG,
                    order, TypeG_op_dtm.pds_rec_op_dtm_begin,
                    order.icbcode_code, order.icbcode_fee_key, "",
                    Pds_recParam.Rec_st.U);
                SetRec(Pds_recParam.Rec_op_type.CBAG, recd_op_type,
                    order, TypeG_op_dtm.pds_rec_op_dtm_end,
                    order.icbcode_code, order.icbcode_fee_key, "",
                    Pds_recParam.Rec_st.Y);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 1 }); // 1: 藥車調劑/核對作業，排除備註欄位儲存

                if (!saveResult.Succ)
                {
                    msg = saveResult.Msg;
                    SetOrderComplete(order.icbcode_code, Pds_recParam.Rec_st.U);
                }
                else
                    SetOrderComplete(order.icbcode_code, Pds_recParam.Rec_st.Y);
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
            else
                Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);

            CheckLstComplete();
        }

        private DelegateCommand _BAGCCommand;
        public DelegateCommand BAGCCommand =>
            _BAGCCommand ?? (_BAGCCommand = new DelegateCommand
            (OnBAGC));
        /// <summary>
        /// 單筆取消
        /// </summary>
        private void OnBAGC()
        {
            string msg = string.Empty;

            SetRec("");

            var cancelModel = PdsMedInfoVisibility == Visibility.Visible ?
                Pds_recParam.CancelMode.CBAGC : Pds_recParam.CancelMode.CBAGCG;

            if (cancelModel == Pds_recParam.CancelMode.CBAGCG &&
                SelectedBAGCOrder == null)
            {
                msg = "單筆取消藥品未選取。";
                goto exit;
            }

            PdsRecCancelViewModel vm = ShowPdsRecCancelWindow?.Invoke(cancelModel);
            if (vm == null) goto exit;

            string st = Pds_recParam.Rec_st.C;
            if (vm.pds_rec_reason == Pds_recParam.Rec_reason.C05)
                st = Pds_recParam.Rec_st.Y;
            else if (vm.pds_rec_reason == Pds_recParam.Rec_reason.C08 &&
                cancelModel == Pds_recParam.CancelMode.CBAGCG)
                st = Pds_recParam.Rec_st.Y;

            if (cancelModel == Pds_recParam.CancelMode.CBAGC)
            {   // 非自包機
                SetRec(Pds_recParam.Rec_op_type.CBAG, Pds_recParam.Recd_op_type.BAGC,
                    PdsMedInfo.mi_micbcode, "",
                    BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                    st, vm.pds_rec_reason, vm.pds_rec_reason_oth,
                    vm.pds_rec_md_qty.NullableToStr(), vm.pds_rec_md_way1,
                    vm.pds_recd_err_mst_id, vm.pds_recd_err_qty);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 1 }); // 1: 藥車調劑/核對作業，排除備註欄位儲存

                if (!saveResult.Succ)
                {
                    msg = saveResult.Msg;
                    st = Pds_recParam.Rec_st.U;
                }
                else if (Rec.pds_rec_reason == Pds_recParam.Rec_reason.C06 ||
                    Rec.pds_rec_reason == Pds_recParam.Rec_reason.C07)
                {
                    // 通報(藥袋)
                    var note = $"{GetReasonName(Rec.pds_rec_reason)}：";
                    if (Rec.pds_rec_reason == Pds_recParam.Rec_reason.C06)
                        note += $"{vm.pds_recd_err_mst_id}。";
                    else if (Rec.pds_rec_reason == Pds_recParam.Rec_reason.C07)
                    {
                        note += $"{vm.pds_recd_err_qty}";
                        note += (PdsMedInfo.mi_micbcode.icbcode_med_type_p
                            ? "包" : PdsMedInfo.mi_micbcode.icbcode_pha_unit) + "。";
                    }

                    var pds_note = new Pds_note
                    {
                        pds_note_dtm = Rec.pds_rec_op_dtm_end,
                        pds_note_type = Rec.pds_rec_reason,
                        pds_note_op = Pds_noteParam.Op.UD,
                        pds_note_bag_code = Rec.pds_rec_bag_code,
                        pds_note_lst_code = Rec.pds_rec_lst_code,
                        pds_note_send_dt = Rec.pds_rec_send_dt,
                        pds_note_ipd_no = Rec.pds_rec_ipd_no,
                        pds_note_pat_no = Rec.pds_rec_pat_no,
                        pds_note_bed = Rec.pds_rec_bed,
                        pds_note_clinical = Rec.pds_rec_clinical,
                        pds_note_note = note,
                        pds_note_st = Pds_noteParam.St.U,
                        pds_note_md_man = LoginViewModel.LoginUser.UserId,
                        pds_note_md_name = LoginViewModel.LoginUser.UserName,
                        pds_note_md_pc = HostUtil.GetHostNameAndAddress(),
                        pds_note_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr()
                    };

                    ApiUtil.HttpClientEx<ApiResult<Pds_note>>(
                    RouteParam.Service(),
                    RouteParam.Pds_note.SavePds_note,
                    pds_note);
                }
                else if (Rec.pds_rec_reason == Pds_recParam.Rec_reason.C08)
                {
                    if (vm.pds_rec_md_way1.NullableToStr() != string.Empty)
                        PdsMedInfo.icbcode_rx_way1_final = vm.pds_rec_md_way1;

                    if (vm.pds_rec_md_qty.NullableToStr() != string.Empty)
                    {
                        if (PdsMedInfo.mi_micbcode.icbcode_med_type_p)
                            PdsMedInfo.icbcode_pack_final = vm.pds_rec_md_qty;
                        else
                            PdsMedInfo.icbcode_rx_qty_final = vm.pds_rec_md_qty;
                    }
                }

                SetOrderComplete(PdsMedInfo.mi_micbcode.icbcode_code, st);
                CheckLstComplete();
            }
            else
            {   // 自包機
                TypeG_op_dtm.pds_rec_op_dtm_end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
                //SetRec(Pds_recParam.Rec_op_type.CBAG, Pds_recParam.Recd_op_type.BAG,
                //    SelectedBAGCOrder, TypeG_op_dtm.pds_rec_op_dtm_begin,
                //    SelectedBAGCOrder.icbcode_code, SelectedBAGCOrder.icbcode_fee_key, "",
                //    Pds_recParam.Rec_st.U);
                SetRec(Pds_recParam.Rec_op_type.CBAG, Pds_recParam.Recd_op_type.BAGC,
                    SelectedBAGCOrder, TypeG_op_dtm.pds_rec_op_dtm_end,
                    SelectedBAGCOrder.icbcode_code, SelectedBAGCOrder.icbcode_fee_key, "",
                    st, vm.pds_rec_reason, vm.pds_rec_reason_oth,
                    vm.pds_rec_md_qty.NullableToStr(), vm.pds_rec_md_way1,
                    vm.pds_recd_err_mst_id, vm.pds_recd_err_qty);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 1 }); // 1: 藥車調劑/核對作業，排除備註欄位儲存

                if (!saveResult.Succ)
                {
                    msg = saveResult.Msg;
                    st = Pds_recParam.Rec_st.U;
                }

                SetOrderComplete(SelectedBAGCOrder.icbcode_code, st);
            }

        exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);

            if (cancelModel == Pds_recParam.CancelMode.CBAGCG)
            {
                var resultTypeG = Util.Media.CheckMsgSpeech("是否繼續處理餐包?");
                if (resultTypeG != MessageBoxResult.OK)
                    CheckLstComplete();
            }
        }

        private DelegateCommand _LSTCCommand;
        public DelegateCommand LSTCCommand =>
            _LSTCCommand ?? (_LSTCCommand = new DelegateCommand
            (OnLSTC));
        /// <summary>
        /// 整張取消
        /// </summary>
        private void OnLSTC()
        {
            string msg = string.Empty;

            SetRec("");

            var cancelModel = Pds_recParam.CancelMode.CLSTC;
            PdsRecCancelViewModel vm = ShowPdsRecCancelWindow?.Invoke(cancelModel);
            if (vm == null) return;

            string st = Pds_recParam.Rec_st.C;
            SetRec(Pds_recParam.Rec_op_type.CLST, Pds_recParam.Recd_op_type.LSTC,
                null, "",
                LstCode, "", "",
                st, vm.pds_rec_reason, vm.pds_rec_reason_oth);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 6 }); // 6: 藥車核對作業，整張取消/整張重新

            if (!saveResult.Succ)
            {
                msg = saveResult.Msg;
                goto exit;
            }

            LstCodeReset(true, true);
            BedCodeReset();
            BagCodeReset();

        exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        private DelegateCommand _LSTRCommand;
        public DelegateCommand LSTRCommand =>
            _LSTRCommand ?? (_LSTRCommand = new DelegateCommand
            (OnLSTR));
        /// <summary>
        /// 重新核對
        /// </summary>
        private void OnLSTR()
        {
            var LSTRResult = LSTR();
            if (LSTRResult == MessageBoxResult.OK)
                BagCodeReset(true, true);
        }

        /// <summary>
        /// 重新核對
        /// </summary>
        private MessageBoxResult LSTR(bool checkComplete = false)
        {
            string msg = string.Empty;

            SetRec("");

            var LSTRResult = Util.Media.CheckMsgSpeech(
                checkComplete ? "已完成核對，是否重新核對?" : "是否重新核對?");
            if (LSTRResult != MessageBoxResult.OK) goto exit;

            string st = Pds_recParam.Rec_st.C;
            SetRec(Pds_recParam.Rec_op_type.CLST, Pds_recParam.Recd_op_type.LSTR,
                null, "",
                LstCode, "", "",
                st, Pds_recParam.Rec_reason.C09);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 6 }); // 6: 藥車核對作業，整張取消/整張重新

            if (!saveResult.Succ)
            {
                msg = saveResult.Msg;
                LSTRResult = MessageBoxResult.Cancel;
                goto exit;
            }

            SetOrderComplete("", st, true);

        exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);

            return LSTRResult;
        }

        private DelegateCommand _CCAREndCommand;
        public DelegateCommand CCAREndCommand =>
            _CCAREndCommand ?? (_CCAREndCommand = new DelegateCommand
            (OnCCAREnd));
        /// <summary>
        /// 藥車核對完成
        /// </summary>
        private void OnCCAREnd()
        {
            string msg = string.Empty;

            if (Clinical.IsNullOrWhiteSpace() || SendDt.IsNullOrWhiteSpace())
            {
                msg = "護理站或日期未選取，無法執行藥車核對完成。";
                goto exit;
            }

            SetRec("");

            ApiResult<List<Ch_udrec_chk>> udrec_chkResult =
              ApiUtil.HttpClientEx<ApiResult<List<Ch_udrec_chk>>>(
                RouteParam.Service(),
                RouteParam.Ch_udrec_chk.QueryCh_udrec_chk,
                new Ch_udrec_chk
                {
                    chudrecchk_date = DateTimeUtil.ConvertAD(SendDt, inFormat: "yyyy/MM/dd", outFormat: "yyyMMdd").ToInt(),
                    chudrecchk_bed_unit = Clinical
                },
                new { option = 4 });  // 4: 依傳送日期、護理站，查詢未完成核對配藥單

            if (!udrec_chkResult.Succ)
            {
                msg = udrec_chkResult.Msg;
                goto exit;
            }
            else if (udrec_chkResult.Data.Count > 0)
            {
                udrec_chkResult.Data.ForEach(uk =>
                {
                    msg += $"{Util.Media.SpeechBedNoStr(uk.chudrecchk_bed)}";
                    if (uk.chudrecchk_bed != uk.chudrec_bed)
                        msg += $"(原床位{Util.Media.SpeechBedNoStr(uk.chudrec_bed)})";
                    msg += "尚未核對。" + Environment.NewLine;
                });
                goto exit;
            }

            SetRec(Pds_recParam.Rec_op_type.CCAR, "",
                null, "",
                "", "", "",
                Pds_recParam.Rec_st.Y);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 7 }); // 7: 藥車核對作業，CCAR 作業日期時間-結儲存

            if (!saveResult.Succ)
            {
                msg = saveResult.Msg;
                goto exit;
            }
            else
                msg = "藥車記得上鎖。";

            exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg, closeSpeechAfterOK: true);
        }

        private DelegateCommand _atcCommand;
        public DelegateCommand AtcCommand =>
            _atcCommand ?? (_atcCommand = new DelegateCommand
            (OnAtc));
        /// <summary>
        /// 同藥理
        /// </summary>
        private void OnAtc()
        {
            ShowAtcCodeGroupWindow?.Invoke(OrderList.OrderByDescending(o => o.atc_code_prefix5).ToList());
        }

        private DelegateCommand _lstudCommand;
        public DelegateCommand LstudCommand =>
            _lstudCommand ?? (_lstudCommand = new DelegateCommand
            (OnLstud));
        /// <summary>
        /// 交互作用
        /// </summary>
        private void OnLstud()
        {
            ShowLstudWindow?.Invoke(LstudList);
        }

        private DelegateCommand _noticeCommand;
        public DelegateCommand NoticeCommand =>
            _noticeCommand ?? (_noticeCommand = new DelegateCommand
            (OnNotice, () => !PdsPatInfo.ipd_no.IsNullOrWhiteSpace()));
        /// <summary>
        /// 通報(配藥單)
        /// </summary>
        private void OnNotice()
        {
            int send_dt = LstCode.SubStr(0, 7).ToInt();
            string ipd_no = LstCode.SubStr(7, 11);

            ShowPdsNoteEditWindow?.Invoke(new Pds_note()
            {
                EditMode = Pds_noteParam.EditMode.UDLST,
                pds_note_op = Pds_noteParam.Op.UD,
                pds_note_lst_code = LstCode,
                pds_note_send_dt = send_dt,
                pds_note_ipd_no = ipd_no,
                pds_note_pat_no = PdsPatInfo.pat_no,
                pds_note_bed = PdsPatInfo.chudrecchk_bed,
                pds_note_clinical = PdsPatInfo.chudrecchk_bed_unit,
                pds_note_st = Pds_noteParam.St.U
            });
        }

        /// <summary>
        /// 取得狀態原因名稱
        /// </summary>
        private string GetReasonName(string shortCode) =>
            ReasonList.FirstOrDefault(c => c.rec_code_short == shortCode)?.rec_code_name ?? string.Empty;

        /// <summary>
        /// 取得狀態名稱
        /// </summary>
        private string GetStName(string shortCode) =>
            StList.FirstOrDefault(c => c.rec_code_short == shortCode)?.rec_code_name ?? string.Empty;

        /// <summary>
        /// 取得動作類型名稱
        /// </summary>
        private string GetOpdName(string shortCode) =>
            OpdList.FirstOrDefault(c => c.rec_code_short == shortCode)?.rec_code_name ?? string.Empty;

        /// <summary>
        /// 確認配藥單是否完成核對
        /// </summary>
        private void CheckLstComplete()
        {
            var msg = string.Empty;

            ApiResult<List<Pds_rec>> checkResult =
              ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPds_rec,
                new Pds_rec { pds_rec_lst_code = LstCode },
                new { option = 8 }); // 8: 依配藥單條碼，查詢記錄是否完成核對

            if (checkResult.Succ && checkResult.Data.Count > 0)
            { // 所有藥品完成核對後，跳「10D02XXX已完成核對」。
                string acttext = IsMBD ? "回收" : "核對";
                msg = $"{Util.Media.SpeechBedNoStr(BedCode)}已完成{acttext}。";
                LstCodeReset(true, true);
                BedCodeReset();
                BagCodeReset();
            }
            else
                BagCodeReset(true, true);

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 設定處方明細是否完成(反黑/反灰)
        /// </summary>
        private void SetOrderComplete(string icbcode_code, string rec_st, bool setAll = false)
        {
            if (!setAll)
            {
                var cbag = OrderList.Where(o => o.icbcode_code == icbcode_code).FirstOrDefault();
                if (cbag != null) cbag.pds_rec_st = rec_st; // 反黑/反灰
            }
            else
            {
                foreach (var o in OrderList)
                {
                    if (o.icbcode_med_type == Mi_micbcodeParam.Med_type.S ||
                        o.icbcode_med_type == Mi_micbcodeParam.Med_type.V)
                        continue;
                    o.pds_rec_st = rec_st;
                }
            }
        }

        /// <summary>
        /// 刷配藥單條碼 Reset
        /// </summary>
        private void LstCodeReset(bool lstCodeEnabled = false, bool lstCodeFocused = false)
        {
            LstCode = string.Empty;
            LstCodeEnabled = lstCodeEnabled;
            LstCodeFocused = lstCodeFocused;
            LSTCEnabled = false;
            LSTREnabled = false;
            PdsPatInfo = null;
            OrderList = null;
            MsenList = null;
            LstudList = null;
        }

        /// <summary>
        /// 刷藥盒條碼 Reset
        /// </summary>
        private void BedCodeReset(bool bedCodeEnabled = false, bool bedCodeFocused = false)
        {
            BedCode = string.Empty;
            BedCodeEnabled = bedCodeEnabled;
            BedCodeFocused = bedCodeFocused;
            LSTREnabled = false;
        }

        /// <summary>
        /// 刷藥袋條碼 Reset
        /// </summary>
        private void BagCodeReset(bool bagCodeEnabled = false, bool bagCodeFocused = false)
        {
            BagCode = string.Empty;
            BagCodeEnabled = bagCodeEnabled;
            BagCodeFocused = bagCodeFocused;
            BAGCEnabled = false;
            PdsMedInfo = null;
            PdsBcodeInfoViewModel = null;
        }

        /// <summary>
        /// 設定 Rec
        /// </summary>
        /// <param name="rec_op_type">空白代表清空 Rec</param>
        /// <param name="typeG_op_dtm">自包機核對藥品起迄時間</param>
        private void SetRec(string rec_op_type, string recd_op_type = "",
           Mi_micbcode icbcode = null, string typeG_op_dtm = "",
           string code = "", string mst_id = "", string pack = "",
           string st = "", string reason = "", string reason_oth = "",
           string md_qty = "", string md_way1 = "",
           string err_mst_id = "", string err_qty = "")
        {
            if (rec_op_type == "")
            {
                Rec = null;
                return;
            }

            Rec.pds_rec_op_type = rec_op_type;

            string pds_recd_op_dtm = (typeG_op_dtm.IsNullOrWhiteSpace()) ?
                DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") : typeG_op_dtm;
            if (string.IsNullOrWhiteSpace(Rec.pds_rec_op_dtm_begin))
                Rec.pds_rec_op_dtm_begin = pds_recd_op_dtm;
            Rec.pds_rec_op_dtm_end = pds_recd_op_dtm;

            switch (Rec.pds_rec_op_type)
            {
                case Pds_recParam.Rec_op_type.CCAR:
                    Rec.pds_rec_send_dt = DateTimeUtil.ConvertAD(SendDt, inFormat: "yyyy/MM/dd", outFormat: "yyyMMdd").ToInt();
                    Rec.pds_rec_clinical = Clinical;
                    break;
                case Pds_recParam.Rec_op_type.CLST:
                    Rec.pds_rec_bag_code = string.Empty;
                    Rec.pds_rec_lst_code = LstCode;
                    Rec.pds_rec_send_dt = Rec.pds_rec_lst_code.SubStr(0, 7).ToInt();
                    Rec.pds_rec_ipd_no = Rec.pds_rec_lst_code.SubStr(7, 11);
                    Rec.pds_rec_pat_no = PdsPatInfo.pat_no;
                    Rec.pds_rec_bed = PdsPatInfo.chudrecchk_bed;
                    Rec.pds_rec_clinical = PdsPatInfo.chudrecchk_bed_unit;
                    break;
                default: // CBAG
                    Rec.pds_rec_bag_code = icbcode.icbcode_code;
                    Rec.pds_rec_lst_code = icbcode.icbcode_send_dt.NullableToStr().PadLeft(7, '0') + icbcode.icbcode_ipd_no;
                    Rec.pds_rec_send_dt = icbcode.icbcode_send_dt;
                    Rec.pds_rec_ipd_no = icbcode.icbcode_ipd_no;
                    Rec.pds_rec_pat_no = icbcode.icbcode_pat_no;
                    Rec.pds_rec_bed = PdsPatInfo.chudrecchk_bed;
                    Rec.pds_rec_clinical = PdsPatInfo.chudrecchk_bed_unit;
                    break;
            }

            Rec.pds_rec_st = st;
            Rec.pds_rec_reason = reason;
            Rec.pds_rec_reason_oth = reason_oth;
            // 若有處方修改才更新，否則保留最後改過的處方修改
            if (reason == Pds_recParam.Rec_reason.C08)
            {
                Rec.pds_rec_md_qty = md_qty;
                Rec.pds_rec_md_way1 = md_way1;
            }
            Rec.pds_rec_md_man = LoginViewModel.LoginUser.UserId;
            Rec.pds_rec_md_name = LoginViewModel.LoginUser.UserName;
            Rec.pds_rec_md_pc = HostUtil.GetHostNameAndAddress();
            Rec.pds_rec_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr();

            if (!recd_op_type.IsNullOrWhiteSpace())
            {
                var pds_recd = new Pds_recd()
                {
                    pds_recd_op_type = recd_op_type,
                    pds_recd_op_dtm = Rec.pds_rec_op_dtm_end,
                    pds_recd_code = code,
                    pds_recd_mst_id = mst_id,
                    pds_recd_pack = pack,
                    pds_recd_st = st,
                    pds_recd_reason = reason,
                    pds_recd_reason_oth = reason_oth,
                    pds_recd_md_qty = md_qty,
                    pds_recd_md_way1 = md_way1,
                    pds_recd_err_mst_id = err_mst_id,
                    pds_recd_err_qty = err_qty,
                    pds_recd_md_man = LoginViewModel.LoginUser.UserId,
                    pds_recd_md_name = LoginViewModel.LoginUser.UserName,
                    pds_recd_md_pc = HostUtil.GetHostNameAndAddress(),
                    pds_recd_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr()
                };
                Rec.pds_recdList.Add(pds_recd);
            }
        }

    }
}
