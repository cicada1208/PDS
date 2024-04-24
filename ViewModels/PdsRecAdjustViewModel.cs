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
    public class PdsRecAdjustViewModel : BaseViewModel<PdsRecAdjustViewModel>
    {
        private Pds_rec _abag;
        /// <summary>
        /// 藥車調劑-藥袋
        /// </summary>
        public Pds_rec Abag
        {
            get => _abag ?? (_abag = new Pds_rec());
            set => Set(ref _abag, value);
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

        private PdsMedInfo _pdsMedInfo;
        /// <summary>
        ///藥品資訊
        /// </summary>
        public PdsMedInfo PdsMedInfo
        {
            get
            {
                if (_pdsMedInfo == null)
                    _pdsMedInfo = new PdsMedInfo();
                _pdsMedInfo.opMode = PdsMedInfoParam.OpMode.A;
                return _pdsMedInfo;
            }
            set => Set(ref _pdsMedInfo, value);
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

        private bool _bagCodeFocused = true;
        /// <summary>
        /// 藥袋條碼：Focused
        /// </summary>
        public bool BagCodeFocused
        {
            get => _bagCodeFocused;
            set => Set(ref _bagCodeFocused, value);
        }

        private bool _bagCodeEnabled = true;
        /// <summary>
        /// 藥袋條碼：Enabled
        /// </summary>
        public bool BagCodeEnabled
        {
            get => _bagCodeEnabled;
            set => Set(ref _bagCodeEnabled, value);
        }

        private string _medCode;
        /// <summary>
        /// 藥品條碼
        /// </summary>
        public string MedCode
        {
            get => _medCode;
            set => Set(ref _medCode, value?.Trim());
        }

        private bool _medCodeFocused;
        /// <summary>
        /// 藥品條碼：Focused
        /// </summary>
        public bool MedCodeFocused
        {
            get => _medCodeFocused;
            set => Set(ref _medCodeFocused, value);
        }

        private bool _medCodeEnabled;
        /// <summary>
        /// 藥品條碼：Enabled
        /// </summary>
        public bool MedCodeEnabled
        {
            get => _medCodeEnabled;
            set => Set(ref _medCodeEnabled, value);
        }

        private string _pack;
        /// <summary>
        /// 粉包包數
        /// </summary>
        public string Pack
        {
            get => _pack;
            set => Set(ref _pack, value?.Trim());
        }

        private bool _packFocused;
        /// <summary>
        /// 粉包包數：Focused
        /// </summary>
        public bool PackFocused
        {
            get => _packFocused;
            set => Set(ref _packFocused, value);
        }

        private bool _packEnabled;
        /// <summary>
        /// 粉包包數：Enabled
        /// </summary>
        public bool PackEnabled
        {
            get => _packEnabled;
            set => Set(ref _packEnabled, value);
        }

        private PackBarCode _packBarCode;
        /// <summary>
        /// 磨粉分包條碼資訊(MedCode拆解)
        /// </summary>
        public PackBarCode PackBarCode
        {
            get => _packBarCode ?? (_packBarCode = new PackBarCode());
            set => Set(ref _packBarCode, value);
        }

        private bool _BAGCEnabled;
        /// <summary>
        /// 取消：Enabled
        /// </summary>
        public bool BAGCEnabled
        {
            get => _BAGCEnabled;
            set => Set(ref _BAGCEnabled, value);
        }

        private int _dripCount = 0;
        /// <summary>
        /// 點滴計數(刷藥品)
        /// </summary>
        public int DripCount
        {
            get => _dripCount;
            set => Set(ref _dripCount, value);
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


        /// <summary>
        /// 顯示取消畫面
        /// </summary>
        public Func<PdsRecCancelViewModel> ShowPdsRecCancelWindow;

        private DelegateCommand _bagCodeCommand;
        public DelegateCommand BagCodeCommand =>
            _bagCodeCommand ?? (_bagCodeCommand = new DelegateCommand
            (OnBagCode));
        /// <summary>
        /// 刷藥袋條碼
        /// </summary>
        private void OnBagCode()
        {
            var msg = string.Empty;
            var msgShow = string.Empty;

            SetAbag("");

            ApiResult<PdsPatInfo> pdsPatInfoResult =
              ApiUtil.HttpClientEx<ApiResult<PdsPatInfo>>(
                RouteParam.Service(),
                RouteParam.PdsPatInfo.QueryPdsPatInfo,
                new { icbcode_code = BagCode },
                new { option = 1 }); // 1: 刷藥袋條碼，取得病人資訊
            PdsPatInfo = pdsPatInfoResult.Data;

            ApiResult<PdsMedInfo> pdsMedInfoResult =
              ApiUtil.HttpClientEx<ApiResult<PdsMedInfo>>(
                RouteParam.Service(),
                RouteParam.PdsMedInfo.QueryPdsMedInfo,
                new { mi_micbcode = new Mi_micbcode() { icbcode_code = BagCode } },
                new { option = 1 }); // 1: 刷藥袋條碼，取得藥品資訊
            PdsMedInfo = pdsMedInfoResult.Data;

            if (!pdsPatInfoResult.Succ)
            {
                msg = pdsPatInfoResult.Msg;
                BagCodeReset(true, true);
            }
            else if (!pdsMedInfoResult.Succ)
            {
                msg = pdsMedInfoResult.Msg;
                BagCodeReset(true, true);
            }
            else
            {
                SetAbag(Pds_recParam.Recd_op_type.BAG,
                    BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                    Pds_recParam.Rec_st.U);

                //if (PdsMedInfo.cbag.pds_rec_reason == Pds_recParam.Rec_reason.C08)
                //    msg += $"核對處方修改：總量：{PdsMedInfo.cbag.pds_rec_md_qty}，頻次：{PdsMedInfo.cbag.pds_rec_md_way1}{Environment.NewLine}";
                if (PdsMedInfo.mdInfo != null)
                {
                    msg += $"{GetReasonName(PdsMedInfo.mdInfo.pds_recd_reason)}：";
                    msg += $"{(PdsMedInfo.mi_micbcode.icbcode_med_type_p ? "總包" : "總量")}：{PdsMedInfo.mdInfo.pds_recd_md_qty}，";
                    msg += $"頻次：{PdsMedInfo.mdInfo.pds_recd_md_way1.ConcatSeparator()}{Environment.NewLine}";
                }
                if (PdsMedInfo.ch_prs.chprs_give_dilu == "Y")
                    msg += $"另給稀釋液：{PdsMedInfo.ch_prs.chprs_give_dilu_note}{Environment.NewLine}";
                if (PdsMedInfo.ch_prs.chprs_spec_pack1 == "Y")
                {
                    msgShow += msg;
                    msg += $"特殊包裝量：{PdsMedInfo.ch_prs.chprs_spec_pack_speech}{Environment.NewLine}";
                    msgShow += $"特殊包裝量：{PdsMedInfo.ch_prs.chprs_spec_pack}{Environment.NewLine}";
                }

                BagCodeEnabled = false;
                MedCodeEnabled = true;
                MedCodeFocused = true;
                BAGCEnabled = true;
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg, msgShow: msgShow);
        }

        private DelegateCommand _medCodeCommand;
        public DelegateCommand MedCodeCommand =>
            _medCodeCommand ?? (_medCodeCommand = new DelegateCommand
            (OnMedCode));
        /// <summary>
        /// 刷藥品條碼
        /// </summary>
        private void OnMedCode()
        {
            //string msg = string.Empty;

            //if (BagCode.NullableToStr() == string.Empty)
            //{
            //    msg = MsgParam.PdsRecNoBagCode;
            //    MedCode = string.Empty;
            //    BagCodeEnabled = true;
            //    BagCodeFocused = true;
            //}
            if (PdsMedInfo.mi_micbcode.icbcode_med_type_p)
                OnMedCodeTypeP();
            else
                OnMedCodeTypeNonP();

            //if (msg != string.Empty)
            //    MediaUtil.MsgSpeeh(msg);
        }

        /// <summary>
        /// 刷藥品條碼(非磨粉分包)
        /// </summary>
        private void OnMedCodeTypeNonP()
        {
            var msg = string.Empty;

            var result = ApiUtil.HttpClientEx<ApiResult<List<Ch_prs_code>>>(
            RouteParam.Service(),
            RouteParam.Ch_prs_code.QueryCh_prs_code,
            new { prscode_code = MedCode },
            new { option = 3 }); // 3: 依藥品條碼、有效的項目

            if (!result.Succ)
            {
                msg = result.Msg;
                MedCodeReset(true, true);
            }
            else if (MedCode.StartsWith("ICBC") || MedCode.StartsWith("ICFC"))
            {
                SetAbag(Pds_recParam.Recd_op_type.MED,
                    MedCode, "", "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N02);

                //DripReCount();
                msg = GetReasonName(Pds_recParam.Rec_reason.N02);
                MedCodeReset(true, true);
            }
            else if (result.Data.Count == 0)
            {
                //DripReCount();
                msg = MsgParam.PdsRecNoMedCode;
                MedCodeReset(true, true);
            }
            else if (result.Data.FirstOrDefault()?.prscode_mst_id != PdsMedInfo.mi_micbcode.icbcode_fee_key)
            {
                SetAbag(Pds_recParam.Recd_op_type.MED,
                    MedCode, result.Data.FirstOrDefault()?.prscode_mst_id, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N01);

                //DripReCount();
                msg = GetReasonName(Pds_recParam.Rec_reason.N01);
                MedCodeReset(true, true);
            }
            else
            {
                bool medTypeD = PdsMedInfo.mi_micbcode.icbcode_med_type == Mi_micbcodeParam.Med_type.D;
                StrParam.CompareResult dripCountResult = StrParam.CompareResult.OneEqualTwo;
                string st = Pds_recParam.Rec_st.Y;

                if (medTypeD)
                {
                    DripCount++;
                    dripCountResult = DripCountComplete();
                    st = dripCountResult == StrParam.CompareResult.OneEqualTwo ?
                        Pds_recParam.Rec_st.Y : Pds_recParam.Rec_st.U;
                }

                SetAbag(Pds_recParam.Recd_op_type.MED,
                MedCode, result.Data.FirstOrDefault()?.prscode_mst_id, "",
                st);

                if (medTypeD && dripCountResult == StrParam.CompareResult.OneLessTwo) // 繼續刷藥品計數
                    MedCodeReset(true, true);
                else if (medTypeD && dripCountResult == StrParam.CompareResult.OneMoreTwo)
                {
                    DripReCount();
                    msg = MsgParam.PdsRecDripOverQty;
                    MedCodeReset(true, true);
                }
                else
                {
                    var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                    RouteParam.Service(),
                    RouteParam.Pds_rec.SavePds_rec,
                    Abag, new { option = 1 }); // 1: 藥車調劑/核對作業，排除備註欄位儲存

                    BagCodeReset(true, true);
                    MedCodeReset();
 
                    if (saveResult.Succ)
                        Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
                    else
                        msg = saveResult.Msg;
                }
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 點滴計數是否已達總量
        /// </summary>
        /// <returns>
        ///  DripCount 小於 qty：-1,
        ///  DripCount 等於 qty：0,
        ///  DripCount 大於 qty：1,
        /// </returns>
        private StrParam.CompareResult DripCountComplete()
        {
            int qty;

            //if (!int.TryParse(Abag.pds_rec_md_qty.NullableToStr(), out qty))
            //    if (!int.TryParse(PdsMedInfo.mi_micbcode.icbcode_rx_qty.NullableToStr(), out qty))
            //        qty = 1;

            if (!int.TryParse(PdsMedInfo.icbcode_rx_qty_final.NullableToStr(), out qty))
                qty = 1;

            return StrUtil.CompareStrNum(DripCount, qty);
        }

        /// <summary>
        /// 點滴計數重新計數
        /// </summary>
        private void DripReCount()
        {
            DripCount = 0;
            if (PdsMedInfo.mi_micbcode.icbcode_med_type == Mi_micbcodeParam.Med_type.D)
            {
                Abag?.pds_recdList?.RemoveAll(d =>
                d.pds_recd_op_type == Pds_recParam.Recd_op_type.MED &&
                d.pds_recd_st == Pds_recParam.Rec_st.U);
            }
        }

        /// <summary>
        /// 刷藥品條碼(磨粉分包)
        /// </summary>
        private void OnMedCodeTypeP()
        {
            var msg = string.Empty;
            PackBarCode = null;

            // 格式：P,藥品代碼,次劑量分子,次劑量分母,效期
            // EX：P,POLAT,001,003,20210406
            PackBarCode.barcode = MedCode;
            var aryPCode = MedCode.Split(',');
            for (int i = 0; i < aryPCode.Length; i++)
            {
                if (i == 1)
                    PackBarCode.mst_id = aryPCode[i].Trim();
                else if (i == 2)
                    PackBarCode.uqty1 = aryPCode[i].Trim();
                else if (i == 3)
                    PackBarCode.uqty2 = aryPCode[i].Trim();
                else if (i == 4)
                    PackBarCode.expdt = aryPCode[i].Trim();
            }

            PackBarCode.expdt_fmt = DateTimeUtil.ConvertAD(PackBarCode.expdt, false, outFormat: "yyyy/MM/dd");
            var expdtDeadline = DateTime.Now.AddDays(3).ToString("yyyy/MM/dd");
            var expdtResult = DateTimeUtil.CompareStrDateTime(PackBarCode.expdt_fmt, expdtDeadline);

            if (MedCode.StartsWith("ICBC") || MedCode.StartsWith("ICFC"))
            {
                SetAbag(Pds_recParam.Recd_op_type.MED,
                    MedCode, "", "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N02);

                msg = GetReasonName(Pds_recParam.Rec_reason.N02);
                MedCodeReset(true, true);
            }
            else if ((PackBarCode.mst_id != PdsMedInfo.mi_micbcode.icbcode_fee_key) ||
                (!StrUtil.CompareEqualStrNum(PackBarCode.uqty1, PdsMedInfo.mi_micbcode.icbcode_rx_uqty1)) ||
                (!StrUtil.CompareEqualStrNum(PackBarCode.uqty2, PdsMedInfo.mi_micbcode.icbcode_rx_uqty2)))
            {
                SetAbag(Pds_recParam.Recd_op_type.MED,
                    MedCode, PackBarCode.mst_id, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N01, "",
                    "", "",
                    PackBarCode.uqty1, PackBarCode.uqty2, "");

                msg = GetReasonName(Pds_recParam.Rec_reason.N01);
                MedCodeReset(true, true);
            }
            else if (expdtResult == StrParam.CompareResult.OneLessTwo ||
                expdtResult == StrParam.CompareResult.FormatErr)
            {
                SetAbag(Pds_recParam.Recd_op_type.MED,
                    MedCode, PackBarCode.mst_id, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N03, "",
                    "", "",
                    "", "", PackBarCode.expdt_fmt);

                msg = GetReasonName(Pds_recParam.Rec_reason.N03);
                MedCodeReset(true, true);
            }
            else
            {
                MedCodeEnabled = false;
                PackEnabled = true;
                PackFocused = true;
                Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        private DelegateCommand _PackCommand;
        public DelegateCommand PackCommand =>
            _PackCommand ?? (_PackCommand = new DelegateCommand
            (OnPack));
        /// <summary>
        /// 輸入粉包包數
        /// </summary>
        private void OnPack()
        {
            var msg = string.Empty;

            //if (MedCode.NullableToStr() == string.Empty)
            //{
            //    msg = MsgParam.PdsRecNoMedCode;
            //    Pack = string.Empty;
            //    MedCodeEnabled = true;
            //    MedCodeFocused = true;
            //}
            if (!Pack.IsNumeric())
            {
                msg = MsgParam.DataTypeNum;
                PackReset(true, true);
            }
            else
            {
                StrParam.CompareResult packSumResult = PackSumComplete(int.Parse(Pack));

                string st = packSumResult == StrParam.CompareResult.OneEqualTwo ?
                    Pds_recParam.Rec_st.Y : Pds_recParam.Rec_st.U;

                if (packSumResult == StrParam.CompareResult.OneLessTwo)
                { // 繼續刷藥品條碼及粉包包數
                    SetAbag(Pds_recParam.Recd_op_type.MED,
                        MedCode, PackBarCode.mst_id, Pack,
                        st);

                    MedCodeReset(true, true);
                    PackReset();
                }
                else if (packSumResult == StrParam.CompareResult.OneMoreTwo ||
                    packSumResult == StrParam.CompareResult.FormatErr)
                {
                    SetAbag(Pds_recParam.Recd_op_type.MED,
                        MedCode, PackBarCode.mst_id, Pack,
                        Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N04);

                    PackReSum();
                    msg = GetReasonName(Pds_recParam.Rec_reason.N04) + "，重新計數。";
                    MedCodeReset(true, true);
                    PackReset();
                }
                else
                {
                    SetAbag(Pds_recParam.Recd_op_type.MED,
                        MedCode, PackBarCode.mst_id, Pack,
                        st);

                    var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                    RouteParam.Service(),
                    RouteParam.Pds_rec.SavePds_rec,
                    Abag, new { option = 1 }); // 1: 藥車調劑/核對作業，排除備註欄位儲存

                    BagCodeReset(true, true);
                    MedCodeReset();
                    PackReset();

                    if (saveResult.Succ)
                        Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
                    else
                        msg = saveResult.Msg;
                }
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 粉包包數是否已達總包
        /// </summary>
        /// <returns>
        ///  packSum 小於 pack：-1,
        ///  packSum 等於 pack：0,
        ///  packSum 大於 pack：1,
        /// </returns>
        private StrParam.CompareResult PackSumComplete(int currentPack = 0)
        {
            double pack;

            //if (!double.TryParse(Abag.pds_rec_md_qty.NullableToStr(), out pack))
            //    if (!double.TryParse(PdsMedInfo.mi_micbcode.icbcode_pack.NullableToStr(), out pack))
            //        pack = 1;

            if (!double.TryParse(PdsMedInfo.icbcode_pack_final.NullableToStr(), out pack))
                pack = 1;

            double packSum = Abag.pds_recdList.Where(d =>
            d.pds_recd_op_type == Pds_recParam.Recd_op_type.MED &&
            d.pds_recd_st == Pds_recParam.Rec_st.U).Sum(d => double.Parse(d.pds_recd_pack));
            packSum += currentPack;

            return StrUtil.CompareStrNum(packSum, pack);
        }

        /// <summary>
        /// 粉包包數重新加總
        /// </summary>
        private void PackReSum()
        {
            if (PdsMedInfo.mi_micbcode.icbcode_med_type_p)
            {
                Abag?.pds_recdList?.RemoveAll(d =>
                d.pds_recd_op_type == Pds_recParam.Recd_op_type.MED &&
                d.pds_recd_st == Pds_recParam.Rec_st.U);
            }
        }

        private DelegateCommand _BAGCCommand;
        public DelegateCommand BAGCCommand =>
            _BAGCCommand ?? (_BAGCCommand = new DelegateCommand
            (OnBAGC));
        /// <summary>
        /// 取消藥袋
        /// </summary>
        private void OnBAGC()
        {
            string msg = string.Empty;

            PdsRecCancelViewModel vm = ShowPdsRecCancelWindow?.Invoke();
            if (vm == null) return;

            string st = Pds_recParam.Rec_st.C;
            //if (vm.pds_rec_reason == Pds_recParam.Rec_reason.C04)
            //    st = Pds_recParam.Rec_st.U;
            if (vm.pds_rec_reason == Pds_recParam.Rec_reason.C05)
                st = Pds_recParam.Rec_st.Y;

            SetAbag(Pds_recParam.Recd_op_type.BAGC,
                BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                st, vm.pds_rec_reason, vm.pds_rec_reason_oth,
                vm.pds_rec_md_qty.NullableToStr(), vm.pds_rec_md_way1);

            if (vm.pds_rec_reason == Pds_recParam.Rec_reason.C04)
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

                MedCodeReset(true, true);
                PackReset();
            }
            else
            {
                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Abag, new { option = 1 }); // 1: 藥車調劑/核對作業，排除備註欄位儲存

                if (!saveResult.Succ)
                    msg = saveResult.Msg;

                BagCodeReset(true, true);
                MedCodeReset();
                PackReset();
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 取得狀態原因名稱
        /// </summary>
        private string GetReasonName(string shortCode) =>
            ReasonList.FirstOrDefault(c => c.rec_code_short == shortCode)?.rec_code_name ?? string.Empty;

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
            PdsPatInfo = null;
        }

        /// <summary>
        /// 刷藥品條碼 Reset
        /// </summary>
        private void MedCodeReset(bool medCodeEnabled = false, bool medCodeFocused = false)
        {
            MedCode = string.Empty;
            MedCodeEnabled = medCodeEnabled;
            MedCodeFocused = medCodeFocused;
        }

        /// <summary>
        /// 輸入粉包包數 Reset
        /// </summary>
        private void PackReset(bool packEnabled = false, bool packFocused = false)
        {
            Pack = string.Empty;
            PackEnabled = packEnabled;
            PackFocused = packFocused;
        }

        /// <summary>
        /// 設定ABAG：藥車調劑-藥袋
        /// </summary>
        /// <param name="op_type">空白代表清空 Abag</param>
        private void SetAbag(string op_type,
           string code = "", string mst_id = "", string pack = "",
           string st = "", string reason = "", string reason_oth = "",
           string md_qty = "", string md_way1 = "",
           string err_uqty1 = "", string err_uqty2 = "", string err_expdt = "")
        {
            if (op_type == "")
            {
                Abag = null;
                DripReCount();
                return;
            }

            Abag.pds_rec_op_type = Pds_recParam.Rec_op_type.ABAG;
            string pds_recd_op_dtm = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            if (string.IsNullOrWhiteSpace(Abag.pds_rec_op_dtm_begin))
                Abag.pds_rec_op_dtm_begin = pds_recd_op_dtm;
            Abag.pds_rec_op_dtm_end = pds_recd_op_dtm;
            Abag.pds_rec_bag_code = PdsMedInfo.mi_micbcode.icbcode_code;
            Abag.pds_rec_lst_code = PdsMedInfo.mi_micbcode.icbcode_send_dt.NullableToStr().PadLeft(7, '0') + PdsMedInfo.mi_micbcode.icbcode_ipd_no;
            Abag.pds_rec_send_dt = PdsMedInfo.mi_micbcode.icbcode_send_dt;
            Abag.pds_rec_ipd_no = PdsMedInfo.mi_micbcode.icbcode_ipd_no;
            Abag.pds_rec_pat_no = PdsMedInfo.mi_micbcode.icbcode_pat_no;
            Abag.pds_rec_bed = PdsPatInfo.chudrec_bed;
            Abag.pds_rec_clinical = PdsPatInfo.chudrec_bed_unit;
            Abag.pds_rec_st = st;
            Abag.pds_rec_reason = reason;
            Abag.pds_rec_reason_oth = reason_oth;
            // 若有處方修改才更新，否則保留最後改過的處方修改
            if (reason == Pds_recParam.Rec_reason.C04)
            {
                Abag.pds_rec_md_qty = md_qty;
                Abag.pds_rec_md_way1 = md_way1;
            }
            Abag.pds_rec_md_man = LoginViewModel.LoginUser.UserId;
            Abag.pds_rec_md_name = LoginViewModel.LoginUser.UserName;
            Abag.pds_rec_md_pc = HostUtil.GetHostNameAndAddress();
            Abag.pds_rec_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr();

            var pds_recd = new Pds_recd()
            {
                pds_recd_op_type = op_type,
                pds_recd_op_dtm = pds_recd_op_dtm,
                pds_recd_code = code,
                pds_recd_mst_id = mst_id,
                pds_recd_pack = pack,
                pds_recd_st = st,
                pds_recd_reason = reason,
                pds_recd_reason_oth = reason_oth,
                pds_recd_md_qty = md_qty,
                pds_recd_md_way1 = md_way1,
                pds_recd_err_uqty1 = err_uqty1,
                pds_recd_err_uqty2 = err_uqty2,
                pds_recd_err_expdt = err_expdt,
                pds_recd_md_man = LoginViewModel.LoginUser.UserId,
                pds_recd_md_name = LoginViewModel.LoginUser.UserName,
                pds_recd_md_pc = HostUtil.GetHostNameAndAddress(),
                pds_recd_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr()
            };
            Abag.pds_recdList.Add(pds_recd);
        }

    }
}
