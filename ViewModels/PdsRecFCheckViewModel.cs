using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ViewModels
{
    public class PdsRecFCheckViewModel : PdsRecFBaseViewModel<PdsRecFCheckViewModel>
    {
        /// <summary>
        ///藥品資訊
        /// </summary>
        public override PdsMedInfo PdsMedInfo
        {
            get
            {
                if (_pdsMedInfo == null)
                {
                    _pdsMedInfo = new PdsMedInfo();
                    _pdsMedInfo.SaveMEDV = OnMEDV;
                }
                _pdsMedInfo.opMode = PdsMedInfoParam.OpMode.FC;
                return _pdsMedInfo;
            }
            set
            {
                Set(ref _pdsMedInfo, value);
                if (_pdsMedInfo != null && _pdsMedInfo.SaveMEDV == null)
                    _pdsMedInfo.SaveMEDV = OnMEDV;
            }
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

            var trans1to4SResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec_S,
            new Pds_rec
            {
                pds_rec_lst_code = LstCode,
                pds_rec_md_pc = HostUtil.GetHostNameAndAddress(),
                pds_rec_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr()
            },
            new { option = 2 }); // 2: 首日量轉入1-4級管制藥調劑資訊

            ApiResult<bool> adjustResult =
              ApiUtil.HttpClientEx<ApiResult<bool>>(
                RouteParam.Service(),
                RouteParam.Mi_micbcode.QueryLstComplete,
                new Mi_micbcode { icfcode_prt_dt = prt_dt, icfcode_id = id, icfcode_pill_no = pill_no },
                new { option = 17 }); // 17: 依首日量配藥單條碼(列印日期、領藥號)，查詢需調劑藥袋的狀態(轉入後)

            if (!adjustResult.Succ)
            {
                msg = adjustResult.Msg;
                LstCodeReset(true, true);
                goto exit;
            }
            else if (!adjustResult.Data)
            {
                msg = MsgParam.PdsRecFAdjustNotDone;
                LstCodeReset(true, true);
                goto exit;
            }

            IPillNo = ipill_no;

            ApiResult<List<Mi_micbcode>> orderListResult =
              ApiUtil.HttpClientEx<ApiResult<List<Mi_micbcode>>>(
                RouteParam.Service(),
                RouteParam.Mi_micbcode.QueryMi_micbcode,
                new Mi_micbcode { icfcode_prt_dt = prt_dt, icfcode_id = id, icfcode_pill_no = pill_no },
                new { option = 10 }); // 10: 依首日量配藥單條碼(列印日期、領藥號)，查詢處方明細及核對藥袋的狀態

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

            // 書記批價傳藥局(排除預開醫囑帶入)
            if (OrderList.FirstOrDefault(o => o.icfcode_secretary == "Y") != null)
            {
                if (Util.Media.CheckMsgSpeech(MsgParam.PdsRecFIsOrdered) != MessageBoxResult.OK)
                {
                    SetRec("");
                    SetRec(FLSToptype, Pds_recParam.Recd_op_type.LST,
                        null,
                        LstCode, "", "",
                        Pds_recParam.Rec_st.C, Pds_recParam.Rec_reason.C10);

                    ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                    RouteParam.Service(),
                    RouteParam.Pds_rec.SavePds_rec,
                    Rec, new { option = 10 }); // 10: 首日量作業，FALST/FCLST/FRLST 儲存
                    LstCodeReset(true, true);
                    goto exit;
                }
            }
            else
            {
                var remakemarResult = ApiUtil.HttpClientEx<ApiResult<List<Ch_remakemar>>>(
                    RouteParam.Service(),
                    RouteParam.Ch_remakemar.QueryCh_remakemar,
                    new Ch_remakemar
                    {
                        //remake_ipd_date = PdsPatInfo.ipd_no.SubStr(0, 7).ToNullableInt(),
                        //remake_ipd_seq = PdsPatInfo.ipd_no.SubStr(7, 4).ToNullableShort(),
                        remake_pat_no = pdsPatInfoResult.Data.pat_no,
                        remake_ins_date = prt_dt,
                        remake_odr_takeno = pill_no
                    },
                    new { option = 3 }); // 3: 依病歷號、列印日期、領藥號  (因切帳由2改為3)

                if (!remakemarResult.Succ)
                {
                    msg = remakemarResult.Msg;
                    LstCodeReset(true, true);
                    goto exit;
                }
                else if (remakemarResult.Data.Count > 0)
                {
                    var reason = $"{remakemarResult.Data.FirstOrDefault()?.remake_reason}，請進行補藥審核。";

                    if (Util.Media.CheckMsgSpeech(reason, okButtonText: "合理", cancelButtonText: "不合理") != MessageBoxResult.OK)
                    {
                        SetRec("");
                        SetRec(FLSToptype, Pds_recParam.Recd_op_type.LST,
                            null,
                            LstCode, "", "",
                            Pds_recParam.Rec_st.C, Pds_recParam.Rec_reason.C12);

                        ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                        RouteParam.Service(),
                        RouteParam.Pds_rec.SavePds_rec,
                        Rec, new { option = 10 }); // 10: 首日量作業，FALST/FCLST/FRLST 儲存
                        LstCodeReset(true, true);
                        goto exit;
                    }
                }
            }

            ApiResult<List<Pds_rec>> checkResult =
              ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.QueryPds_rec,
                new Pds_rec { pds_rec_lst_code = LstCode },
                new { option = 11 }); // 11: 依首日量配藥單條碼，查詢記錄是否完成核對

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

            // 2024.01.24 因藥局人力不足暫將發藥併入首日量核對作業
            //var notNeedType = new HashSet<string> { Mi_micbcodeParam.Med_type.S, Mi_micbcodeParam.Med_type.V };
            var notNeedType = new HashSet<string> { Mi_micbcodeParam.Med_type.V };
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
            bool doMEDV = false;

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
            else
            {
                SetRec(FBAGoptype, Pds_recParam.Recd_op_type.BAG,
                    PdsMedInfo.mi_micbcode,
                    BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                    Pds_recParam.Rec_st.U);

                var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
                RouteParam.Service(),
                RouteParam.Pds_rec.SavePds_rec,
                Rec, new { option = 11 }); // 11: 首日量調劑/核對/發藥作業，排除備註欄位儲存

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
                    ApiResult<List<Pds_rec>> fabagResult =
                      ApiUtil.HttpClientEx<ApiResult<List<Pds_rec>>>(
                        RouteParam.Service(),
                        RouteParam.Pds_rec.QueryPds_rec,
                        new Pds_rec { pds_rec_op_type = Pds_recParam.Rec_op_type.FABAG, pds_rec_bag_code = BagCode },
                        new { option = 1 }); // 1: 依參數自動組建

                    var fabag = fabagResult.Data?.FirstOrDefault();
                    if (fabag != null && fabag.pds_rec_st == Pds_recParam.Rec_st.S)
                    {
                        msg += $"{GetStName(fabag.pds_rec_st)}：{fabag.pds_rec_nondeliver}{Environment.NewLine}";
                        doMEDV = true;
                    }
                    if (fabag != null && fabag.pds_rec_reason == Pds_recParam.Rec_reason.C05)
                    {
                        msg += $"{GetReasonName(fabag.pds_rec_reason)}{Environment.NewLine}";
                        doMEDV = true;
                    }

                    if (PdsMedInfo.mi_micbcode.icbcode_med_type == Mi_micbcodeParam.Med_type.G)
                        msg += $"請確認預包領藥號為 {Util.Media.SpeechBedNoStr(IPillNo)}{Environment.NewLine}";

                    BagCodeEnabled = false;
                    BAGCEnabled = true;
                }
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);

            if (doMEDV) OnMEDV();
        }

        /// <summary>
        /// 核對藥品
        /// </summary>
        private void OnMEDV()
        {
            var msg = string.Empty;

            SetRec("");

            SetRec(FBAGoptype, Pds_recParam.Recd_op_type.MEDV,
                PdsMedInfo.mi_micbcode,
                BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
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
            }
            else
            {
                OnMEDA();
                SetOrderComplete(BagCode, Pds_recParam.Rec_st.Y);
                Util.Media.MsgOKErrSoundPlay(MediaParam.MsgType.OK);
                CheckLstComplete();
            }

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

        /// <summary>
        /// 發藥確認 2024.01.24 因藥局人力不足暫將發藥併入首日量核對作業
        /// </summary>
        private void OnMEDA()
        {
            SetRec("");

            SetRec(Pds_recParam.Rec_op_type.FRBAG, Pds_recParam.Recd_op_type.MEDA,
                PdsMedInfo.mi_micbcode,
                BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                Pds_recParam.Rec_st.Y);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 11 }); // 11: 首日量調劑/核對/發藥作業，排除備註欄位儲存
        }

        /// <summary>
        /// 單筆取消
        /// </summary>
        protected override void OnBAGC()
        {
            string msg = string.Empty;

            SetRec("");

            var cancelModel = Pds_recParam.CancelMode.FCBAGC;
            PdsRecCancelViewModel vm = ShowPdsRecCancelWindow?.Invoke(cancelModel);
            if (vm == null) return;

            string st = Pds_recParam.Rec_st.C;
            if (vm.pds_rec_reason == Pds_recParam.Rec_reason.C05)
                st = Pds_recParam.Rec_st.Y;

            SetRec(FBAGoptype, Pds_recParam.Recd_op_type.BAGC,
                PdsMedInfo.mi_micbcode,
                BagCode, PdsMedInfo.mi_micbcode.icbcode_fee_key, "",
                st, vm.pds_rec_reason, vm.pds_rec_reason_oth,
                vm.pds_rec_md_qty.NullableToStr(), vm.pds_rec_md_way1,
                vm.pds_recd_err_mst_id, vm.pds_recd_err_qty);

            var saveResult = ApiUtil.HttpClientEx<ApiResult<Pds_rec>>(
            RouteParam.Service(),
            RouteParam.Pds_rec.SavePds_rec,
            Rec, new { option = 11 }); // 11: 首日量調劑/核對/發藥作業，排除備註欄位儲存

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
                    pds_note_op = Pds_noteParam.Op.FST,
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

            SetOrderComplete(BagCode, st);
            CheckLstComplete();

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

            var cancelModel = Pds_recParam.CancelMode.FCLSTC;
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
                new { option = 11 }); // 11: 依首日量配藥單條碼，查詢記錄是否完成核對

            if (lstResult.Succ && lstResult.Data.Count > 0)
            {
                msg = $"{Util.Media.SpeechBedNoStr(IPillNo)}已完成{OpTitle}。{Environment.NewLine}";

                if (OrderList.FirstOrDefault(o => o.chprs_orig_rehrig == "Y") != null)
                    msg += MsgParam.PdsRecCheckRehrig;

                LstCodeReset(true, true);
                BagCodeReset();
            }
            else
                BagCodeReset(true, true);

            if (msg != string.Empty)
                Util.Media.MsgSpeech(msg);
        }

    }
}
