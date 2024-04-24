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
    public class PdsRecFAdjustViewModel : PdsRecFBaseViewModel<PdsRecFAdjustViewModel>
    {
        /// <summary>
        ///藥品資訊
        /// </summary>
        public override PdsMedInfo PdsMedInfo
        {
            get
            {
                if (_pdsMedInfo == null)
                    _pdsMedInfo = new PdsMedInfo();
                _pdsMedInfo.opMode = PdsMedInfoParam.OpMode.FA;
                return _pdsMedInfo;
            }
            set => Set(ref _pdsMedInfo, value);
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


        /// <summary>
        /// 刷配藥單條碼
        /// </summary>
        protected override void OnLstCode()
        {
            var msg = string.Empty;
            int prt_dt = LstCode.SubStr(0, 7).ToInt();
            string ipill_no = LstCode.SubStr(7, 5);
            string id = LstCode.SubStr(7, 1);
            short? pill_no = LstCode.SubStr(8, 4).ToNullableShort();

            ApiResult<PdsPatInfo> pdsPatInfoResult =
              ApiUtil.HttpClientEx<ApiResult<PdsPatInfo>>(
                RouteParam.Service(),
                RouteParam.PdsPatInfo.QueryPdsPatInfo,
                new PdsPatInfo { lst_code = LstCode },
                new { option = 3 }); // 3:  刷首日量配藥單條碼，取得病人資訊

            if (!pdsPatInfoResult.Succ)
            {
                msg = pdsPatInfoResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }

            IPillNo = ipill_no;

            ApiResult<List<Mi_micbcode>> orderListResult =
              ApiUtil.HttpClientEx<ApiResult<List<Mi_micbcode>>>(
                RouteParam.Service(),
                RouteParam.Mi_micbcode.QueryMi_micbcode,
                new Mi_micbcode { icfcode_prt_dt = prt_dt, icfcode_id = id, icfcode_pill_no = pill_no },
                new { option = 9 }); // 9: 依首日量配藥單條碼(列印日期、領藥號)，查詢處方明細及調劑藥袋的狀態

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
                new Mr_lstud { icfcode_prt_dt = prt_dt, icfcode_id = id, icfcode_pill_no = pill_no },
                new { option = 3 }); // 3: 依首日量配藥單條碼(列印日期、領藥號)，查詢配藥單交互作用

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

            var sodrResult = SOrderMsg(OrderList);
            if (!sodrResult.IsNullOrWhiteSpace())
            {
                msg = sodrResult;
                LstCodeReset(true, true);
                goto exit;
            }

            ApiResult<List<Pds_rec>> adjustResult =
              ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPds_rec,
                new Pds_rec { pds_rec_lst_code = LstCode },
                new { option = 10 }); // 10: 依首日量配藥單條碼，查詢記錄是否完成調劑

            if (!adjustResult.Succ)
            {
                msg = adjustResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }
            else if (adjustResult.Data.Count > 0)
            {
                var LSTRResult = LSTR(true);
                if (LSTRResult != MessageBoxResult.OK)
                {
                    LstCodeReset(true, true);
                    goto exit; ;
                }
            }

            var notNeedType = new HashSet<string> { Mi_micbcodeParam.Med_type.FOURS, Mi_micbcodeParam.Med_type.S, Mi_micbcodeParam.Med_type.V };
            var st = OrderList.Where(o => !notNeedType.Contains(o.icbcode_med_type)).Count() > 0 ?
                Pds_recParam.Rec_st.U : Pds_recParam.Rec_st.Y;

            SetRec("");
            SetRec(FLSToptype, Pds_recParam.Recd_op_type.LST,
                null,
                LstCode, "", "",
                st);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 10 }); // 10: 首日量作業，FALST/FCLST/FRLST 儲存

            if (!saveResult.Succ)
            {
                msg = saveResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }
            else
            {
                if (st == Pds_recParam.Rec_st.U)
                {
                    LstCodeEnabled = false;
                    BagCodeEnabled = true;
                    BagCodeFocused = true;
                    LSTCEnabled = true;
                }
                else
                {
                    Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
                    CheckLstComplete();
                }
            }

        exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 刷藥袋條碼
        /// </summary>
        protected override void OnBagCode()
        {
            var msg = string.Empty;
            var msgShow = string.Empty;

            SetRec("");

            ApiResult<PdsMedInfo> pdsMedInfoResult =
              ApiUtil.HttpClientEx<ApiResult<PdsMedInfo>>(
                RouteParam.Service(),
                RouteParam.PdsMedInfo.QueryPdsMedInfo,
                new { mi_micbcode = new Mi_micbcode() { icbcode_code = BagCode } },
                new { option = 2 }); // 2: 刷首日量藥袋條碼，取得藥品資訊
            PdsMedInfo = pdsMedInfoResult.Data;

            SelectedOrder = OrderList.FirstOrDefault(o => o.icbcode_code == BagCode);
            bool errPat = SelectedOrder == null;

            if (!pdsMedInfoResult.Succ)
            {
                msg = pdsMedInfoResult.Msg;
                BagCodeReset(true, true);
            }
            else if (errPat)
            {   // N06應記錄在該病人配藥單之下
                SetRec(FLSToptype, Pds_recParam.Recd_op_type.BAG,
                    null,
                    BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N06);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 10 }); // 10: 首日量作業，FALST/FCLST/FRLST 儲存

                msg = GetReasonName(Pds_recParam.Rec_reason.N06);
                BagCodeReset(true, true);
            }
            else if (PdsMedInfo.mi_micbcode.icbcode_med_type == Mi_micbcodeParam.Med_type.FOURS ||
                PdsMedInfo.mi_micbcode.icbcode_med_type == Mi_micbcodeParam.Med_type.S)
            {
                msg = MsgParam.PdsRecFABAGSOrder;
                CheckLstComplete();
            }
            else
            {
                SetRec(FBAGoptype, Pds_recParam.Recd_op_type.BAG,
                    PdsMedInfo.mi_micbcode,
                    BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                    Pds_recParam.Rec_st.U);

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
            if (PdsMedInfo.mi_micbcode.icbcode_med_type_p)
                OnMedCodeTypeP();
            else
                OnMedCodeTypeNonP();
        }

        /// <summary>
        /// 刷藥品條碼(非磨粉分包)
        /// </summary>
        private void OnMedCodeTypeNonP()
        {
            var msg = string.Empty;
            bool medTypeG = PdsMedInfo.mi_micbcode.icbcode_med_type == Mi_micbcodeParam.Med_type.G;

            var prscResult = ApiUtil.HttpClientEx<ApiResult<List<Ch_prs_code>>>(
            RouteParam.Service(),
            RouteParam.Ch_prs_code.QueryCh_prs_code,
            new { prscode_code = MedCode },
            new { option = 3 }); // 3: 依藥品條碼、有效的項目

            if (!prscResult.Succ)
            {
                msg = prscResult.Msg;
                MedCodeReset(true, true);
            }
            else if (MedCode.StartsWith("ICBC") || MedCode.StartsWith("ICFC"))
            {
                SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                    PdsMedInfo.mi_micbcode,
                    MedCode, "", "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N02);

                msg = GetReasonName(Pds_recParam.Rec_reason.N02);
                MedCodeReset(true, true);
            }
            // 首日量調劑：自包機除可刷總包條碼，亦可刷藥品條碼，或藥品代碼。
            //else if (medTypeG && MedCode != BagCode.Replace("ICFC", ""))
            else if (medTypeG && !(MedCode == BagCode.Replace("ICFC", "") ||
                prscResult.Data.FirstOrDefault()?.prscode_mst_id == PdsMedInfo.mi_micbcode.icbcode_fee_key ||
                MedCode == PdsMedInfo.mi_micbcode.icbcode_fee_key))
            {
                string mst_id = "";
                string reason = Pds_recParam.Rec_reason.N01;
                bool norec = true;

                if (prscResult.Data.FirstOrDefault()?.prscode_mst_id != null)
                {
                    mst_id = prscResult.Data.FirstOrDefault()?.prscode_mst_id;
                    reason = Pds_recParam.Rec_reason.N01;
                    norec = false;
                }
                else
                {
                    var icfResult = ApiUtil.HttpClientEx<ApiResult<List<Mi_micbcode>>>(
                    RouteParam.Service(),
                    RouteParam.Mi_micbcode.QueryMi_micbcode,
                    new Mi_micbcode { icbcode_code = "ICFC" + MedCode },
                    new { option = 7 }); // 7: 依首日量藥袋條碼，查詢藥品資訊
                    if (icfResult.Succ)
                    {
                        mst_id = icfResult.Data.FirstOrDefault()?.icbcode_fee_key;
                        reason = icfResult.Data.FirstOrDefault()?.icbcode_ipd_no != PdsMedInfo.mi_micbcode.icbcode_ipd_no ?
                            Pds_recParam.Rec_reason.N06 : Pds_recParam.Rec_reason.N01;
                        norec = icfResult.Data.Count == 0;
                    }
                }

                if (!norec)
                    SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                        PdsMedInfo.mi_micbcode,
                        MedCode, mst_id, "",
                        Pds_recParam.Rec_st.N, reason);

                msg = norec ? MsgParam.PdsRecNoMedCode : GetReasonName(reason);
                MedCodeReset(true, true);
            }
            else if ((!medTypeG) && prscResult.Data.Count == 0 && 
                MedCode != PdsMedInfo.mi_micbcode.icbcode_fee_key)
            {
                msg = MsgParam.PdsRecNoMedCode;
                MedCodeReset(true, true);
            }
            // 首日量調劑：一般藥品除可刷藥品條碼，或藥品代碼。
            else if ((!medTypeG) && 
                prscResult.Data.FirstOrDefault()?.prscode_mst_id != PdsMedInfo.mi_micbcode.icbcode_fee_key &&
                MedCode != PdsMedInfo.mi_micbcode.icbcode_fee_key)
            {
                SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                    PdsMedInfo.mi_micbcode,
                    MedCode, prscResult.Data.FirstOrDefault()?.prscode_mst_id, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N01);

                msg = GetReasonName(Pds_recParam.Rec_reason.N01);
                MedCodeReset(true, true);
            }
            else
            {
                string mst_id = "";
                //if (medTypeG)
                //    mst_id = PdsMedInfo.mi_micbcode.icbcode_fee_key;
                //else
                //    mst_id = prscResult.Data.FirstOrDefault()?.prscode_mst_id;
                mst_id = PdsMedInfo.mi_micbcode.icbcode_fee_key;

                SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                    PdsMedInfo.mi_micbcode,
                    MedCode, mst_id, "",
                    Pds_recParam.Rec_st.Y);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 11 }); // 11: 首日量調劑/核對/發藥作業，排除備註欄位儲存

                if (!saveResult.Succ)
                {
                    msg = saveResult.Msg;
                    SetOrderComplete(BagCode, Pds_recParam.Rec_st.U);
                    BagCodeReset(true, true);
                    MedCodeReset();
                }
                else
                {
                    SetOrderComplete(BagCode, Pds_recParam.Rec_st.Y);
                    Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
                    CheckLstComplete();
                }
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
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
                SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                    PdsMedInfo.mi_micbcode,
                    MedCode, "", "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N02);

                msg = GetReasonName(Pds_recParam.Rec_reason.N02);
                MedCodeReset(true, true);
            }
            else if ((PackBarCode.mst_id != PdsMedInfo.mi_micbcode.icbcode_fee_key) ||
                (!StrUtil.CompareEqualStrNum(PackBarCode.uqty1, PdsMedInfo.mi_micbcode.icbcode_rx_uqty1)) ||
                (!StrUtil.CompareEqualStrNum(PackBarCode.uqty2, PdsMedInfo.mi_micbcode.icbcode_rx_uqty2)))
            {
                SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                    PdsMedInfo.mi_micbcode,
                    MedCode, PackBarCode.mst_id, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N01, "",
                    "", "", "", "",
                    PackBarCode.uqty1, PackBarCode.uqty2, "");

                msg = GetReasonName(Pds_recParam.Rec_reason.N01);
                MedCodeReset(true, true);
            }
            else if (expdtResult == StrParam.CompareResult.OneLessTwo ||
                expdtResult == StrParam.CompareResult.FormatErr)
            {
                SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                    PdsMedInfo.mi_micbcode,
                    MedCode, PackBarCode.mst_id, "",
                    Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N03, "",
                    "", "", "", "",
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
                    SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                        PdsMedInfo.mi_micbcode,
                        MedCode, PackBarCode.mst_id, Pack,
                        st);

                    MedCodeReset(true, true);
                    PackReset();
                }
                else if (packSumResult == StrParam.CompareResult.OneMoreTwo ||
                    packSumResult == StrParam.CompareResult.FormatErr)
                {
                    SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                        PdsMedInfo.mi_micbcode,
                        MedCode, PackBarCode.mst_id, Pack,
                        Pds_recParam.Rec_st.N, Pds_recParam.Rec_reason.N04);

                    PackReSum();
                    msg = GetReasonName(Pds_recParam.Rec_reason.N04) + "，重新計數。";
                    MedCodeReset(true, true);
                    PackReset();
                }
                else
                {
                    SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MED,
                        PdsMedInfo.mi_micbcode,
                        MedCode, PackBarCode.mst_id, Pack,
                        st);

                    var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                    RouteParam.Service(),
                    RouteParam.Pds_rec.SavePds_rec,
                    Rec, new { option = 11 }); // 11: 首日量調劑/核對/發藥作業，排除備註欄位儲存

                    if (!saveResult.Succ)
                    {
                        msg = saveResult.Msg;
                        SetOrderComplete(BagCode, Pds_recParam.Rec_st.U);

                        BagCodeReset(true, true);
                        MedCodeReset();
                        PackReset();
                    }
                    else
                    {
                        SetOrderComplete(BagCode, Pds_recParam.Rec_st.Y);
                        Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
                        CheckLstComplete();
                    }
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

            if (!double.TryParse(PdsMedInfo.icbcode_pack_final.NullableToStr(), out pack))
                pack = 1;

            double packSum = Rec.pds_recdList.Where(d =>
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
                Rec?.pds_recdList?.RemoveAll(d =>
                d.pds_recd_op_type == Pds_recParam.Recd_op_type.MED &&
                d.pds_recd_st == Pds_recParam.Rec_st.U);
            }
        }

        /// <summary>
        /// 單筆取消
        /// </summary>
        protected override void OnBAGC()
        {
            string msg = string.Empty;

            var cancelModel = Pds_recParam.CancelMode.FABAGC;
            PdsRecCancelViewModel vm = ShowPdsRecCancelWindow?.Invoke(cancelModel);
            if (vm == null) return;

            string st = Pds_recParam.Rec_st.C;
            if (vm.pds_rec_reason == Pds_recParam.Rec_reason.C05)
                st = Pds_recParam.Rec_st.Y;

            SetRec(FBAGoptype, Pds_recParam.Recd_op_type.BAGC,
                PdsMedInfo.mi_micbcode,
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
                Rec, new { option = 11 }); // 11: 首日量調劑/核對/發藥作業，排除備註欄位儲存

                if (!saveResult.Succ)
                {
                    msg = saveResult.Msg;
                    st = Pds_recParam.Rec_st.U;
                }

                SetOrderComplete(BagCode, st);
                CheckLstComplete();
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 整張取消
        /// </summary>
        protected override void OnLSTC()
        {
            string msg = string.Empty;

            SetRec("");

            var cancelModel = Pds_recParam.CancelMode.FALSTC;
            PdsRecCancelViewModel vm = ShowPdsRecCancelWindow?.Invoke(cancelModel);
            if (vm == null) return;

            string st = Pds_recParam.Rec_st.C;
            SetRec(FLSToptype, Pds_recParam.Recd_op_type.LSTC,
                null,
                LstCode, "", "",
                st, vm.pds_rec_reason, vm.pds_rec_reason_oth);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 9 }); // 9: 首日量調劑/核對/發藥作業，整張取消/整張重新

            if (!saveResult.Succ)
            {
                msg = saveResult.Msg;
                goto exit;
            }

            LstCodeReset(true, true);
            BagCodeReset();
            MedCodeReset();
            PackReset();

        exit:
            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 確認配藥單是否完成
        /// </summary>
        protected override void CheckLstComplete()
        {
            var msg = string.Empty;

            ApiResult<List<Pds_rec>> lstResult =
              ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPds_rec,
                new Pds_rec { pds_rec_lst_code = LstCode },
                new { option = 10 }); // 10: 依首日量配藥單條碼，查詢記錄是否完成調劑

            if (lstResult.Succ && lstResult.Data.Count > 0)
            {
                msg = $"{Util.Media.SpeechBedNoStr(IPillNo)}已完成{OpTitle}。{Environment.NewLine}";
                if (OrderList.Where(o => o.icbcode_med_type == Mi_micbcodeParam.Med_type.FOURS ||
                    o.icbcode_med_type == Mi_micbcodeParam.Med_type.S).Count() > 0)
                {
                    msg += "尚有管制藥需調劑。";
                }
                LstCodeReset(true, true);
                BagCodeReset();
                MedCodeReset();
                PackReset();
            }
            else
            {
                BagCodeReset(true, true);
                MedCodeReset();
                PackReset();
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg, false);
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

    }
}
