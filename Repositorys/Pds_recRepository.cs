using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;
using static MoreLinq.Extensions.DistinctByExtension;

namespace Repositorys
{
    public class Pds_recRepository : BaseRepository<Pds_rec>
    {
        public ApiResult<List<Pds_rec>> QueryPds_rec(Pds_rec param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依作業類型、傳送日期、護理站
                    sql = @"
                    select * 
                    from pds_rec
                    where pds_rec_send_dt = @pds_rec_send_dt
                    and pds_rec_op_type = @pds_rec_op_type
                    and pds_rec_clinical = @pds_rec_clinical";
                    break;
                case 3: // 依傳送日期區間、調劑階段，查詢配藥單未完成明細
                    //sql = @"
                    //select chudrec_date as pds_rec_send_dt, '調劑' as pds_rec_op_name, 
                    //convert(varchar,chudrec_date)+chudrec_ipd_no as pds_rec_lst_code,
                    //str(chudrec_pat_no, 8, '0') as pds_rec_pat_no, 
                    //chudrec_bed as pds_rec_bed, chudrec_bed_unit as pds_rec_clinical, 
                    //case pds_rec_st when null then 'U' else pds_rec_st end as pds_rec_st
                    //from ch_udrec
                    //left join pds_rec
                    //on (pds_rec_lst_code=convert(varchar,chudrec_date)+chudrec_ipd_no and pds_rec_op_type='ALST')
                    //where chudrec_date between @pds_rec_send_dt_begin and @pds_rec_send_dt_end
                    //and pds_rec_st in ('U','N','C',null)
                    //order by chudrec_date, chudrec_bed_unit, chudrec_bed";

                    // 案例：配藥單只有公藥等不需調劑的藥
                    sql = @"
                    select chudrec_date as pds_rec_send_dt, '調劑' as pds_rec_op_name, 
                    convert(varchar,chudrec_date)+chudrec_ipd_no as pds_rec_lst_code,
                    str(chudrec_pat_no, 8, '0') as pds_rec_pat_no, 
                    chudrec_bed as pds_rec_bed, chudrec_bed_unit as pds_rec_clinical,
                    case pds_rec_st when null then 'U' else pds_rec_st end as pds_rec_st
                    from ch_udrec as udrec
                    left join pds_rec
                    on (pds_rec_lst_code=convert(varchar,chudrec_date)+chudrec_ipd_no and pds_rec_op_type='ALST')
                    where chudrec_date between @pds_rec_send_dt_begin and @pds_rec_send_dt_end
                    and exists (
                        select *
                        from mi_micbcode as icbcode
                        left join pds_rec as rec
                        on (pds_rec_bag_code=icbcode_code
                        and pds_rec_op_type='ABAG'
                        and pds_rec_st<>'D')
                        where icbcode_send_dt=udrec.chudrec_date
                        and icbcode_ipd_no=udrec.chudrec_ipd_no
                        and icbcode_med_type not in ('4S', 'S', 'V', 'G')
                        and icbcode_st <> 'D'
                        and pds_rec_st in ('U','N','C',null)
                    )
                    order by chudrec_date, chudrec_bed_unit, chudrec_bed";
                    break;
                case 4: // 依傳送日期區間、核對階段，查詢配藥單未完成明細
                        //sql = @"
                        //select chudrecchk_date as pds_rec_send_dt, '核對' as pds_rec_op_name, 
                        //convert(varchar,chudrecchk_date)+chudrecchk_ipd_no as pds_rec_lst_code,
                        //str(chudrecchk_pat_no, 8, '0') as pds_rec_pat_no, 
                        //chudrecchk_bed as pds_rec_bed, chudrecchk_bed_unit as pds_rec_clinical, 
                        //case pds_rec_st when null then 'U' else pds_rec_st end as pds_rec_st
                        //from ch_udrec_chk as udrec_chk
                        //left join pds_rec
                        //on (pds_rec_lst_code=convert(varchar,chudrecchk_date)+chudrecchk_ipd_no and pds_rec_op_type='CLST')
                        //where chudrecchk_date between @pds_rec_send_dt_begin and @pds_rec_send_dt_end
                        //and exists (
                        //    select *
                        //    from mi_micbcode as icbcode
                        //    left join pds_rec as rec
                        //    on (pds_rec_bag_code=icbcode_code
                        //    and pds_rec_op_type='CBAG'
                        //    and pds_rec_st<>'D')
                        //    where icbcode_send_dt=udrec_chk.chudrecchk_date
                        //    and icbcode_ipd_no=udrec_chk.chudrecchk_ipd_no
                        //    and icbcode_med_type not in ('S', 'V')
                        //    and icbcode_st <> 'D'
                        //    and pds_rec_st in ('U','N','C',null)
                        //)
                        //order by chudrecchk_date, chudrecchk_bed_unit, chudrecchk_bed";

                    // 2022.02.11 只有 icbcode_med_type = S、V 的配藥單，仍須核對只是到刷藥盒就結束
                    sql = @"
                    select chudrecchk_date as pds_rec_send_dt, '核對' as pds_rec_op_name, 
                    convert(varchar,chudrecchk_date)+chudrecchk_ipd_no as pds_rec_lst_code,
                    str(chudrecchk_pat_no, 8, '0') as pds_rec_pat_no, 
                    chudrecchk_bed as pds_rec_bed, chudrecchk_bed_unit as pds_rec_clinical, 
                    case pds_rec_st when null then 'U' else pds_rec_st end as pds_rec_st
                    from ch_udrec_chk as udrec_chk
                    left join pds_rec
                    on (pds_rec_lst_code=convert(varchar,chudrecchk_date)+chudrecchk_ipd_no and pds_rec_op_type='CLST')
                    where chudrecchk_date between @pds_rec_send_dt_begin and @pds_rec_send_dt_end
                    and isnull(pds_rec_st,'') not in ('Y','S')
                    and exists (
                        select *
                        from mi_micbcode as icbcode
                        where icbcode_send_dt=udrec_chk.chudrecchk_date
                        and icbcode_ipd_no=udrec_chk.chudrecchk_ipd_no
                        and icbcode_st <> 'D'
                    )
                    order by chudrecchk_date, chudrecchk_bed_unit, chudrecchk_bed";
                    break;
                case 5: // 依傳送日期區間、全部階段，查詢配藥單未完成明細
                    var list = QueryPds_rec(param, 3).Data;
                    list.AddRange(QueryPds_rec(param, 4).Data);
                    return new ApiResult<List<Pds_rec>>(list);
                case 6: // 依主檔單號
                    sql = @"
                    select * 
                    from pds_rec
                    where pds_rec_no = @pds_rec_no";
                    break;
                case 7: // 依配藥單條碼，查詢記錄是否完成調劑
                    sql = @"
                    select *
                    from pds_rec
                    where pds_rec_lst_code = @pds_rec_lst_code
                    and pds_rec_op_type='ALST'
                    and pds_rec_st in ('Y','S')";
                    break;
                case 8: // 依配藥單條碼，查詢記錄是否完成核對
                    sql = @"
                    select *
                    from pds_rec
                    where pds_rec_lst_code = @pds_rec_lst_code
                    and pds_rec_op_type='CLST'
                    and pds_rec_st in ('Y','S')";
                    break;
                case 9: // 依配藥單條碼、作業類型、狀態非刪除
                    sql = @"
                    select * 
                    from pds_rec
                    where pds_rec_lst_code = @pds_rec_lst_code
                    and pds_rec_op_type = @pds_rec_op_type
                    and pds_rec_st not in ('D')";
                    break;
                case 10: // 依首日量配藥單條碼，查詢記錄是否完成調劑
                    sql = @"
                    select *
                    from pds_rec
                    where pds_rec_lst_code = @pds_rec_lst_code
                    and pds_rec_op_type='FALST'
                    and pds_rec_st in ('Y','S')";
                    break;
                case 11: // 依首日量配藥單條碼，查詢記錄是否完成核對
                    sql = @"
                    select *
                    from pds_rec
                    where pds_rec_lst_code = @pds_rec_lst_code
                    and pds_rec_op_type='FCLST'
                    and pds_rec_st in ('Y','S')";
                    break;
                case 12: // 依首日量配藥單條碼，查詢記錄是否完成發藥
                    sql = @"
                    select *
                    from pds_rec
                    where pds_rec_lst_code = @pds_rec_lst_code
                    and pds_rec_op_type='FRLST'
                    and pds_rec_st in ('Y','S')";
                    break;
                case 13: // 依藥袋條碼、作業類型多筆、狀態非刪除
                    sql = $@"
                    select * 
                    from pds_rec
                    where pds_rec_bag_code = @pds_rec_bag_code
                    and pds_rec_op_type in ({param.pds_rec_op_type})
                    and pds_rec_st not in ('D')";
                    break;
                case 14: // 依首日量配藥單條碼、作業類型、狀態非刪除、藥品類型
                    sql = $@"
                    select * 
                    from pds_rec
                    {(param.pds_rec_op_type == Pds_recParam.Rec_op_type.FABAG ?
                    "left join mi_micfcode on(icfcode_code = pds_rec_bag_code)" : "")}
                    where pds_rec_lst_code = @pds_rec_lst_code
                    and pds_rec_op_type = @pds_rec_op_type
                    and pds_rec_st not in ('D')
                    {(param.pds_rec_op_type == Pds_recParam.Rec_op_type.FABAG ? "and icfcode_med_type not in ('4S')" : "")}";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Pds_rec>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Pds_rec>(sql, param).ToList();

            switch (option)
            {
                case 3:
                case 4:
                    // 排除測試病歷號
                    if (queryList.Count > 0)
                    {
                        var mh_mpat = new Mh_mpat { pat_no_m = string.Join(",", queryList.Select(r => r.pds_rec_pat_no).Distinct()) };
                        // 2: 依病歷號多筆，查詢測試病歷號
                        var testPat = DB.Mh_mpatRepository.QueryMh_mpat(mh_mpat, 2).Data.
                            Select(pat => pat.pat_no.ToString().PadLeft(8, '0')).ToHashSet();
                        queryList = queryList.Where(r => !testPat.Contains(r.pds_rec_pat_no)).ToList();
                    }

                    queryList.ForEach(r =>
                    {
                        r.pds_rec_send_dt_fmt = DateTimeUtil.ConvertROC(r.pds_rec_send_dt.ToString());
                    });
                    break;
            }

            return new ApiResult<List<Pds_rec>>(queryList);
        }

        public ApiResult<List<PdsRecMicbcode>> QueryPdsRecMicbcode(PdsRecMicbcode param, int option = 0)
        {
            string sql = string.Empty;
            string fst_select = @"
                    icfcode_code as icbcode_code, icfcode_prt_dt, icfcode_prt_ti, icfcode_id, icfcode_seq,
                    icfcode_pill_no, icfcode_ipd_no as icbcode_ipd_no, icfcode_pat_no as icbcode_pat_no,
                    icfcode_bed, icfcode_clinical, icfcode_odr_no, icfcode_fee_no, icfcode_fee_key as icbcode_fee_key,
                    icfcode_rx_way1 as icbcode_rx_way1, icfcode_rx_way2 as icbcode_rx_way2,
                    icfcode_rx_uqty1 as icbcode_rx_uqty1, icfcode_rx_uqty2 as icbcode_rx_uqty2,
                    icfcode_rx_unit as icbcode_rx_unit, icfcode_rx_qty1 as icbcode_rx_qty1,
                    icfcode_rx_qty2 as icbcode_rx_qty2, icfcode_beg_dt as icbcode_beg_dt,
                    icfcode_end_dt as icbcode_end_dt, icfcode_pha_unit as icbcode_pha_unit,
                    icfcode_pack as icbcode_pack, icfcode_med_type as icbcode_med_type, icfcode_lst_type,
                    icfcode_secretary, icfcode_st as icbcode_st, icfcode_cd_barcode as icbcode_cd_barcode,
                    icfcode_filler as icbcode_filler, icfcode_id + str(icfcode_pill_no, 4, '0') as icfcode_ipill_no";

            switch (option)
            {
                //case 1: // 依作業日期時間區間、調劑階段、非空值篩選，查詢藥袋未完成明細
                //case 2: // 依作業日期時間區間、核對階段、非空值篩選，查詢藥袋未完成明細
                //    sql = @"
                //    select rec.*, icbcode.*,
                //    opcode.rec_code_name as pds_rec_op_name,
                //    stcode.rec_code_name as pds_rec_st_name,
                //    rncode.rec_code_name as pds_rec_reason_name
                //    from pds_rec as rec
                //    left join mi_micbcode as icbcode
                //    on (icbcode_code=pds_rec_bag_code)
                //    left join rec_code as opcode
                //    on (opcode.rec_code_model='pds_rec_op_type' and opcode.rec_code_group='pds_rec_op_type' 
                //    and opcode.rec_code_short=pds_rec_op_type and opcode.rec_code_st='1')
                //    left join rec_code as stcode
                //    on (stcode.rec_code_model='pds_rec_st' and stcode.rec_code_group='pds_rec_st' 
                //    and stcode.rec_code_short=pds_rec_st and stcode.rec_code_st='1')
                //    left join rec_code as rncode
                //    on (rncode.rec_code_model='pds_rec_reason'
                //    and rncode.rec_code_short=pds_rec_reason and rncode.rec_code_st='1')
                //    where pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end";
                //    if (option == 1)
                //        sql += Environment.NewLine + "and pds_rec_op_type in ('ABAG')";
                //    else
                //        sql += Environment.NewLine + "and pds_rec_op_type in ('CBAG')";
                //    if (param.pds_rec_st == Pds_recParam.Rec_st.S)
                //        sql += Environment.NewLine + $"and pds_rec_st in ('{param.pds_rec_st}')";
                //    else
                //        sql += Environment.NewLine + "and pds_rec_st in ('U','N','C')";
                //    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                //        sql += Environment.NewLine + "and pds_rec_clinical = @pds_rec_clinical";
                //    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                //        sql += Environment.NewLine + "and pds_rec_pat_no = @pds_rec_pat_no";
                //    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                //        sql += Environment.NewLine + "and icbcode_fee_key = @icbcode_fee_key";
                //    if (!param.pds_rec_reason.IsNullOrWhiteSpace())
                //        sql += Environment.NewLine + "and pds_rec_reason = @pds_rec_reason";
                //    break;
                case 1: // 依傳送日期區間、調劑階段、非空值篩選，查詢藥袋未完成明細
                    sql = @"
                    select rec.*, icbcode.*,
                    chudrec_bed_unit, chudrec_bed,
                    opcode.rec_code_name as pds_rec_op_name,
                    stcode.rec_code_name as pds_rec_st_name,
                    rncode.rec_code_name as pds_rec_reason_name,
                    mtcode.rec_code_name as icbcode_med_type_name
                    from mi_micbcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icbcode_code
                    and pds_rec_op_type='ABAG'
                    and pds_rec_st<>'D')
                    left join ch_udrec as udrec
                    on (chudrec_date=icbcode_send_dt and chudrec_ipd_no=icbcode_ipd_no)
                    left join rec_code as opcode
                    on (opcode.rec_code_model='pds_rec_op_type' and opcode.rec_code_group='pds_rec_op_type' 
                    and opcode.rec_code_short='ABAG' and opcode.rec_code_st='1')
                    left join rec_code as stcode
                    on (stcode.rec_code_model='pds_rec_st' and stcode.rec_code_group='pds_rec_st' 
                    and stcode.rec_code_short=pds_rec_st and stcode.rec_code_st='1')
                    left join rec_code as rncode
                    on (rncode.rec_code_model='pds_rec_reason'
                    and rncode.rec_code_short=pds_rec_reason and rncode.rec_code_st='1')
                    left join rec_code as mtcode
                    on (mtcode.rec_code_model='icbcode_med_type' and mtcode.rec_code_group='icbcode_med_type' 
                    and mtcode.rec_code_short=icbcode_med_type and mtcode.rec_code_st='1')
                    where icbcode_send_dt between @pds_rec_send_dt_begin and @pds_rec_send_dt_end
                    and icbcode_med_type not in ('4S', 'S', 'V', 'G')
                    and icbcode_st <> 'D'";
                    if (param.pds_rec_st == Pds_recParam.Rec_st.S)
                        sql += Environment.NewLine + $"and pds_rec_st in ('{param.pds_rec_st}')";
                    else
                        sql += Environment.NewLine + "and pds_rec_st in ('U','N','C',null)";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                    {
                        sql += Environment.NewLine + "and (pds_rec_clinical = @pds_rec_clinical";
                        sql += Environment.NewLine + "  or chudrec_bed_unit = @pds_rec_clinical)";
                    }
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icbcode_pat_no = @pds_rec_pat_no";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icbcode_fee_key = @icbcode_fee_key";
                    if (!param.pds_rec_reason.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_rec_reason = @pds_rec_reason";
                    sql += Environment.NewLine + "order by chudrec_date, chudrec_bed_unit, chudrec_bed";
                    break;
                case 2: // 依傳送日期區間、核對階段、非空值篩選，查詢藥袋未完成明細
                    sql = @"
                    select rec.*, icbcode.*,
                    chudrec_bed_unit, chudrec_bed, chudrecchk_bed_unit, chudrecchk_bed,
                    opcode.rec_code_name as pds_rec_op_name,
                    stcode.rec_code_name as pds_rec_st_name,
                    rncode.rec_code_name as pds_rec_reason_name,
                    mtcode.rec_code_name as icbcode_med_type_name
                    from mi_micbcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icbcode_code
                    and pds_rec_op_type='CBAG'
                    and pds_rec_st<>'D')
                    left join ch_udrec as udrec
                    on (chudrec_date=icbcode_send_dt and chudrec_ipd_no=icbcode_ipd_no)
                    left join ch_udrec_chk as udrec_chk
                    on (chudrecchk_date=icbcode_send_dt and chudrecchk_ipd_no=icbcode_ipd_no)
                    left join rec_code as opcode
                    on (opcode.rec_code_model='pds_rec_op_type' and opcode.rec_code_group='pds_rec_op_type' 
                    and opcode.rec_code_short='CBAG' and opcode.rec_code_st='1')
                    left join rec_code as stcode
                    on (stcode.rec_code_model='pds_rec_st' and stcode.rec_code_group='pds_rec_st' 
                    and stcode.rec_code_short=pds_rec_st and stcode.rec_code_st='1')
                    left join rec_code as rncode
                    on (rncode.rec_code_model='pds_rec_reason'
                    and rncode.rec_code_short=pds_rec_reason and rncode.rec_code_st='1')
                    left join rec_code as mtcode
                    on (mtcode.rec_code_model='icbcode_med_type' and mtcode.rec_code_group='icbcode_med_type' 
                    and mtcode.rec_code_short=icbcode_med_type and mtcode.rec_code_st='1')
                    where icbcode_send_dt between @pds_rec_send_dt_begin and @pds_rec_send_dt_end
                    and icbcode_med_type not in ('S', 'V')
                    and icbcode_st <> 'D'";
                    if (param.pds_rec_st == Pds_recParam.Rec_st.S)
                        sql += Environment.NewLine + $"and pds_rec_st in ('{param.pds_rec_st}')";
                    else
                        sql += Environment.NewLine + "and pds_rec_st in ('U','N','C',null)";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                    {
                        sql += Environment.NewLine + "and (pds_rec_clinical = @pds_rec_clinical";
                        sql += Environment.NewLine + "  or chudrec_bed_unit = @pds_rec_clinical";
                        sql += Environment.NewLine + "  or chudrecchk_bed_unit = @pds_rec_clinical)";
                    }
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icbcode_pat_no = @pds_rec_pat_no";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icbcode_fee_key = @icbcode_fee_key";
                    if (!param.pds_rec_reason.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_rec_reason = @pds_rec_reason";
                    sql += Environment.NewLine + "order by chudrec_date, chudrecchk_bed_unit, chudrecchk_bed, chudrec_bed_unit, chudrec_bed";
                    break;
                case 3: // 依傳送日期區間、全部階段、非空值篩選，查詢藥袋未完成明細
                    var list = QueryPdsRecMicbcode(param, 1).Data;
                    list.AddRange(QueryPdsRecMicbcode(param, 2).Data);
                    return new ApiResult<List<PdsRecMicbcode>>(list);
                case 4: // 依作業日期時間區間、藥車調劑階段、非空值篩選，查詢錯誤/取消明細
                case 5: // 依作業日期時間區間、藥車核對階段、非空值篩選，查詢錯誤/取消明細
                case 11: // 依作業日期時間區間、首日量調劑階段、非空值篩選，查詢錯誤/取消明細
                case 12: // 依作業日期時間區間、首日量核對階段、非空值篩選，查詢錯誤/取消明細
                case 13: // 依作業日期時間區間、首日量發藥階段、非空值篩選，查詢錯誤/取消明細
                    string icbJoin = (option == 4 || option == 5) ? @"
                    left join mi_micbcode as icbcode
                    on (icbcode_code=pds_rec_bag_code)"
                    : @"
                    left join mi_micfcode as icbcode
                    on (icfcode_code=pds_rec_bag_code)";
                    string icbSelect = (option == 4 || option == 5) ? "icbcode.*" : fst_select;

                    sql = @"
                    select ";
                    sql += icbSelect;
                    sql += $@"
                    , rec.*, recd.*,
                    opcode.rec_code_name as pds_rec_op_name,
                    opdcode.rec_code_name as pds_recd_op_name,
                    stdcode.rec_code_name as pds_recd_st_name,
                    rndcode.rec_code_name as pds_recd_reason_name
                    from pds_rec as rec
                    left join pds_recd as recd
                    on (pds_recd_rec_no=pds_rec_no)
                    {icbJoin}
                    left join rec_code as opcode
                    on (opcode.rec_code_model='pds_rec_op_type' and opcode.rec_code_group='pds_rec_op_type' 
                    and opcode.rec_code_short=pds_rec_op_type and opcode.rec_code_st='1')
                    left join rec_code as opdcode
                    on (opdcode.rec_code_model='pds_recd_op_type' and opdcode.rec_code_group='pds_recd_op_type' 
                    and opdcode.rec_code_short=pds_recd_op_type and opdcode.rec_code_st='1')
                    left join rec_code as stdcode
                    on (stdcode.rec_code_model='pds_rec_st' and stdcode.rec_code_group='pds_rec_st' 
                    and stdcode.rec_code_short=pds_recd_st and stdcode.rec_code_st='1')
                    left join rec_code as rndcode
                    on (rndcode.rec_code_model='pds_rec_reason'
                    and rndcode.rec_code_short=pds_recd_reason and rndcode.rec_code_st='1')
                    where pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    and (pds_recd_st in ('N','C') or pds_recd_reason like 'C%')";
                    if (option == 4)
                        sql += Environment.NewLine + "and pds_rec_op_type in ('ABAG', 'ALST', 'ACAR')";
                    else if (option == 5)
                        sql += Environment.NewLine + "and pds_rec_op_type in ('CBAG', 'CLST', 'CCAR')";
                    else if (option == 11)
                        sql += Environment.NewLine + "and pds_rec_op_type in ('FABAG', 'FALST')";
                    else if (option == 12)
                        sql += Environment.NewLine + "and pds_rec_op_type in ('FCBAG', 'FCLST')";
                    else if (option == 13)
                        sql += Environment.NewLine + "and pds_rec_op_type in ('FRBAG', 'FRLST')";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_rec_clinical = @pds_rec_clinical";
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_rec_pat_no = @pds_rec_pat_no";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                    {
                        if (option == 4 || option == 5)
                            sql += Environment.NewLine + "and icbcode_fee_key = @icbcode_fee_key";
                        else
                            sql += Environment.NewLine + "and icfcode_fee_key = @icbcode_fee_key";
                    }
                    if (!param.pds_recd_st.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_recd_st = @pds_recd_st";
                    if (!param.pds_recd_reason.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_recd_reason = @pds_recd_reason";
                    break;
                case 6: // 依作業日期時間區間、全部階段、非空值篩選，查詢錯誤/取消明細
                    var list2 = QueryPdsRecMicbcode(param, 4).Data;
                    list2.AddRange(QueryPdsRecMicbcode(param, 5).Data);
                    list2.AddRange(QueryPdsRecMicbcode(param, 11).Data);
                    list2.AddRange(QueryPdsRecMicbcode(param, 12).Data);
                    list2.AddRange(QueryPdsRecMicbcode(param, 13).Data);
                    return new ApiResult<List<PdsRecMicbcode>>(list2);
                case 7: // 依列印日期區間、調劑階段、非空值篩選，查詢首日量藥袋未完成明細
                case 8: // 依列印日期區間、核對階段、非空值篩選，查詢首日量藥袋未完成明細
                case 9: // 依列印日期區間、發藥階段、非空值篩選，查詢首日量藥袋未完成明細
                    string pds_rec_op_type = string.Empty;
                    string icfcode_med_type = string.Empty;
                    if (option == 7)
                    {
                        pds_rec_op_type = Pds_recParam.Rec_op_type.FABAG;
                        icfcode_med_type = "'V'"; // "'4S', 'S', 'V'"
                    }
                    else if (option == 8)
                    {
                        pds_rec_op_type = Pds_recParam.Rec_op_type.FCBAG;
                        icfcode_med_type = "'V'"; // "'S', 'V'"
                    }
                    else
                    {
                        pds_rec_op_type = Pds_recParam.Rec_op_type.FRBAG;
                        icfcode_med_type = "'V'";
                    }

                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , rec.*,
                    opcode.rec_code_name as pds_rec_op_name,
                    stcode.rec_code_name as pds_rec_st_name,
                    rncode.rec_code_name as pds_rec_reason_name,
                    mtcode.rec_code_name as icbcode_med_type_name
                    from mi_micfcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icfcode_code
                    and pds_rec_op_type='{pds_rec_op_type}'
                    and pds_rec_st<>'D')
                    left join rec_code as opcode
                    on (opcode.rec_code_model='pds_rec_op_type' and opcode.rec_code_group='pds_rec_op_type' 
                    and opcode.rec_code_short='{pds_rec_op_type}' and opcode.rec_code_st='1')
                    left join rec_code as stcode
                    on (stcode.rec_code_model='pds_rec_st' and stcode.rec_code_group='pds_rec_st' 
                    and stcode.rec_code_short=pds_rec_st and stcode.rec_code_st='1')
                    left join rec_code as rncode
                    on (rncode.rec_code_model='pds_rec_reason'
                    and rncode.rec_code_short=pds_rec_reason and rncode.rec_code_st='1')
                    left join rec_code as mtcode
                    on (mtcode.rec_code_model='icbcode_med_type' and mtcode.rec_code_group='icbcode_med_type' 
                    and mtcode.rec_code_short=icfcode_med_type and mtcode.rec_code_st='1')
                    where icfcode_prt_dt between @pds_rec_send_dt_begin and @pds_rec_send_dt_end
                    and icfcode_med_type not in ({icfcode_med_type})
                    and icfcode_st <> 'D'";
                    if (param.pds_rec_st == Pds_recParam.Rec_st.S)
                        sql += Environment.NewLine + $"and pds_rec_st in ('{param.pds_rec_st}')";
                    else
                        sql += Environment.NewLine + "and pds_rec_st in ('U','N','C',null)";
                    if (!param.icfcode_ipill_no.IsNullOrWhiteSpace())
                    {
                        param.icfcode_id = param.icfcode_ipill_no.SubStr(0, 1);
                        param.icfcode_pill_no = param.icfcode_ipill_no.SubStr(1, 4).ToNullableShort();
                        sql += Environment.NewLine + "and icfcode_id = @icfcode_id";
                        sql += Environment.NewLine + "and icfcode_pill_no = @icfcode_pill_no";
                    }
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                    {
                        sql += Environment.NewLine + "and (pds_rec_clinical = @pds_rec_clinical";
                        sql += Environment.NewLine + "  or icfcode_clinical = @pds_rec_clinical)";
                    }
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_pat_no = @pds_rec_pat_no";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_fee_key = @icbcode_fee_key";
                    if (!param.pds_rec_reason.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_rec_reason = @pds_rec_reason";
                    sql += Environment.NewLine + "order by icfcode_prt_dt, icfcode_clinical, icfcode_bed";
                    break;
                case 10: // 依列印日期區間、全部階段、非空值篩選，查詢首日量藥袋未完成明細
                    var list3 = QueryPdsRecMicbcode(param, 7).Data;
                    list3.AddRange(QueryPdsRecMicbcode(param, 8).Data);
                    list3.AddRange(QueryPdsRecMicbcode(param, 9).Data);
                    return new ApiResult<List<PdsRecMicbcode>>(list3);
            }

            var queryList = DB.Syb1.Query<PdsRecMicbcode>(sql, param).ToList();

            var mh_mpat = new Mh_mpat();
            Pds_recd pds_recd = new Pds_recd();
            queryList.ForEach(r =>
            {
                switch (option)
                {
                    case 1:
                    case 2:
                        r.icbcode_send_dt_fmt = DateTimeUtil.ConvertROC(r.icbcode_send_dt.ToString());
                        if (r.pds_rec_pat_no.IsNullOrWhiteSpace())
                            r.pds_rec_pat_no = r.icbcode_pat_no;
                        if (r.pds_rec_clinical.IsNullOrWhiteSpace())
                        {
                            r.pds_rec_clinical = r.chudrec_bed_unit;
                            if (option == 2 && !r.chudrecchk_bed_unit.IsNullOrWhiteSpace())
                                r.pds_rec_clinical = r.chudrecchk_bed_unit;
                        }
                        if (r.pds_rec_bed.IsNullOrWhiteSpace())
                        {
                            r.pds_rec_bed = r.chudrec_bed;
                            if (option == 2 && !r.chudrecchk_bed.IsNullOrWhiteSpace())
                                r.pds_rec_bed = r.chudrecchk_bed;
                        }
                        break;
                    case 7:
                    case 8:
                    case 9:
                        r.icfcode_prt_dt_fmt = DateTimeUtil.ConvertROC(r.icfcode_prt_dt.ToString());
                        if (r.pds_rec_pat_no.IsNullOrWhiteSpace())
                            r.pds_rec_pat_no = r.icbcode_pat_no;
                        if (r.pds_rec_clinical.IsNullOrWhiteSpace())
                            r.pds_rec_clinical = r.icfcode_clinical;
                        if (r.pds_rec_bed.IsNullOrWhiteSpace())
                            r.pds_rec_bed = r.icfcode_bed;
                        break;
                    case 5:
                    case 12:
                    case 13:
                        if (r.pds_recd_reason == Pds_recParam.Rec_reason.C06 ||
                        r.pds_recd_reason == Pds_recParam.Rec_reason.C07)
                        {
                            // 7: 依藥袋條碼、主檔作業類型、動作日期時間之前，查詢最後作業藥師
                            pds_recd.pds_rec_bag_code = r.pds_rec_bag_code;
                            pds_recd.pds_rec_op_type = (option == 5) ? Pds_recParam.Rec_op_type.ABAG : Pds_recParam.Rec_op_type.FABAG;
                            pds_recd.pds_recd_op_dtm = r.pds_recd_op_dtm;
                            r.pds_recd_md_name_a = DB.Pds_recdRepository.QueryPds_recd(pds_recd, 7).Data.FirstOrDefault()?.pds_recd_md_name;

                            if (option == 13)
                            {
                                pds_recd.pds_rec_bag_code = r.pds_rec_bag_code;
                                pds_recd.pds_rec_op_type = Pds_recParam.Rec_op_type.FCBAG;
                                pds_recd.pds_recd_op_dtm = r.pds_recd_op_dtm;
                                r.pds_recd_md_name_c = DB.Pds_recdRepository.QueryPds_recd(pds_recd, 7).Data.FirstOrDefault()?.pds_recd_md_name;
                            }
                        }
                        break;
                }

                mh_mpat.pat_no = r.pds_rec_pat_no.ToNullableInt();
                if (mh_mpat.pat_no != null)
                {
                    // 1: 依參數自動組建
                    r.pat_name = DB.Mh_mpatRepository.QueryMh_mpat(mh_mpat, 1).Data.FirstOrDefault()?.pat_name;
                }
                r.icbcode_rx_uqty = Util.Medical.DoseFormat(r.icbcode_rx_uqty1, r.icbcode_rx_uqty2);
                r.icbcode_rx_qty = Util.Medical.DoseFormat(r.icbcode_rx_qty1, r.icbcode_rx_qty2, false);
                if (r.icbcode_pack.IsNumeric()) r.icbcode_pack = double.Parse(r.icbcode_pack).ToString();
            });

            return new ApiResult<List<PdsRecMicbcode>>(queryList);
        }

        public ApiResult<List<PdsRecAC>> QueryPdsRecAC(PdsRecAC param, int option = 0)
        {
            string sql = string.Empty;
            string icfcode_prt_dt_begin = string.Empty;
            string icfcode_prt_dt_end = string.Empty;
            string fst_select = @"
                    icfcode_code as icbcode_code, icfcode_prt_dt, icfcode_prt_ti, icfcode_id, icfcode_seq,
                    icfcode_pill_no, icfcode_ipd_no as icbcode_ipd_no, icfcode_pat_no as icbcode_pat_no,
                    icfcode_bed, icfcode_clinical, icfcode_odr_no, icfcode_fee_no, icfcode_fee_key as icbcode_fee_key,
                    icfcode_rx_way1 as icbcode_rx_way1, icfcode_rx_way2 as icbcode_rx_way2,
                    icfcode_rx_uqty1 as icbcode_rx_uqty1, icfcode_rx_uqty2 as icbcode_rx_uqty2,
                    icfcode_rx_unit as icbcode_rx_unit, icfcode_rx_qty1 as icbcode_rx_qty1,
                    icfcode_rx_qty2 as icbcode_rx_qty2, icfcode_beg_dt as icbcode_beg_dt,
                    icfcode_end_dt as icbcode_end_dt, icfcode_pha_unit as icbcode_pha_unit,
                    icfcode_pack as icbcode_pack, icfcode_med_type as icbcode_med_type, icfcode_lst_type,
                    icfcode_secretary, icfcode_st as icbcode_st, icfcode_cd_barcode as icbcode_cd_barcode,
                    icfcode_filler as icbcode_filler, icfcode_id + str(icfcode_pill_no, 4, '0') as icfcode_ipill_no";

            switch (option)
            {
                case 1: // 藥車作業依護理站
                    sql = @"
                    select 
                    acar.pds_rec_send_dt, '藥車-護理站' as pds_rec_op_name, acar.pds_rec_clinical, 
                    acar.pds_rec_op_dtm_begin, acar.pds_rec_op_dtm_end,
                    ccar.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
                    ccar.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end
                    from pds_rec as acar
                    left join pds_rec as ccar
                    on (ccar.pds_rec_send_dt=acar.pds_rec_send_dt
                    and ccar.pds_rec_op_type='CCAR'
                    and ccar.pds_rec_clinical=acar.pds_rec_clinical
                    and ccar.pds_rec_st <> 'D')
                    where acar.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    and acar.pds_rec_op_type='ACAR'
                    and acar.pds_rec_st <> 'D'";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and acar.pds_rec_clinical = @pds_rec_clinical";
                    break;
                case 2: // 藥車作業依配藥單
                    sql = @"
                    select 
                    alst.pds_rec_send_dt, '藥車-配藥單' as pds_rec_op_name, alst.pds_rec_lst_code,
                    case when isnull(clst.pds_rec_clinical,'')='' then alst.pds_rec_clinical
                    else clst.pds_rec_clinical end as c_pds_rec_clinical,
                    case when isnull(clst.pds_rec_bed,'')='' then alst.pds_rec_bed
                    else clst.pds_rec_bed end as c_pds_rec_bed,
                    alst.pds_rec_pat_no, 
                    alst.pds_rec_op_dtm_begin, alst.pds_rec_op_dtm_end, alst.pds_rec_md_name,
                    clst.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
                    clst.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end,
                    clst.pds_rec_md_name as c_pds_rec_md_name
                    from pds_rec as alst
                    left join pds_rec as clst
                    on (clst.pds_rec_lst_code = alst.pds_rec_lst_code
                    and clst.pds_rec_op_type='CLST'
                    and clst.pds_rec_st <> 'D')
                    where alst.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    and alst.pds_rec_op_type='ALST'
                    and alst.pds_rec_st <> 'D'";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (alst.pds_rec_clinical = @pds_rec_clinical or clst.pds_rec_clinical = @pds_rec_clinical)";
                    if (!param.pds_rec_bed.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (alst.pds_rec_bed = @pds_rec_bed or clst.pds_rec_bed = @pds_rec_bed)";
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (alst.pds_rec_pat_no = @pds_rec_pat_no or clst.pds_rec_pat_no = @pds_rec_pat_no)";

                    // 案例：配藥單只有公藥等不需調劑的藥
                    sql += @"
                    union
                    select 
                    clst.pds_rec_send_dt, '藥車-配藥單' as pds_rec_op_name, clst.pds_rec_lst_code,
                    case when isnull(clst.pds_rec_clinical,'')='' then alst.pds_rec_clinical
                    else clst.pds_rec_clinical end as c_pds_rec_clinical,
                    case when isnull(clst.pds_rec_bed,'')='' then alst.pds_rec_bed
                    else clst.pds_rec_bed end as c_pds_rec_bed,
                    clst.pds_rec_pat_no, 
                    alst.pds_rec_op_dtm_begin, alst.pds_rec_op_dtm_end, alst.pds_rec_md_name,
                    clst.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
                    clst.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end,
                    clst.pds_rec_md_name as c_pds_rec_md_name
                    from pds_rec as clst
                    left join pds_rec as alst
                    on (alst.pds_rec_lst_code = clst.pds_rec_lst_code
                    and alst.pds_rec_op_type='ALST'
                    and alst.pds_rec_st <> 'D')
                    where clst.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    and clst.pds_rec_op_type='CLST'
                    and clst.pds_rec_st <> 'D'
                    and alst.pds_rec_no is null";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (alst.pds_rec_clinical = @pds_rec_clinical or clst.pds_rec_clinical = @pds_rec_clinical)";
                    if (!param.pds_rec_bed.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (alst.pds_rec_bed = @pds_rec_bed or clst.pds_rec_bed = @pds_rec_bed)";
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (alst.pds_rec_pat_no = @pds_rec_pat_no or clst.pds_rec_pat_no = @pds_rec_pat_no)";
                    break;
                case 3: // 藥車作業依藥袋
                    sql = @"
                    select 
                    abag.pds_rec_send_dt, '藥車-藥袋' as pds_rec_op_name, abag.pds_rec_bag_code,
                    case when isnull(cbag.pds_rec_clinical,'')='' then abag.pds_rec_clinical
                    else cbag.pds_rec_clinical end as c_pds_rec_clinical,
                    case when isnull(cbag.pds_rec_bed,'')='' then abag.pds_rec_bed
                    else cbag.pds_rec_bed end as c_pds_rec_bed,
                    abag.pds_rec_pat_no,
                    icbcode.*,
                    abag.pds_rec_op_dtm_begin, abag.pds_rec_op_dtm_end, 
                    abag.pds_rec_st, astcode.rec_code_name as pds_rec_st_name,
                    abag.pds_rec_md_name,
                    cbag.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
                    cbag.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end, 
                    cbag.pds_rec_st as c_pds_rec_st, cstcode.rec_code_name as c_pds_rec_st_name,
                    cbag.pds_rec_md_name as c_pds_rec_md_name
                    from pds_rec as abag
                    left join pds_rec as cbag
                    on (cbag.pds_rec_bag_code = abag.pds_rec_bag_code
                    and cbag.pds_rec_op_type='CBAG'
                    and cbag.pds_rec_st <> 'D')
                    left join mi_micbcode as icbcode
                    on (icbcode_code=abag.pds_rec_bag_code)
                    left join rec_code as astcode
                    on (astcode.rec_code_model='pds_rec_st' and astcode.rec_code_group='pds_rec_st' 
                    and astcode.rec_code_short=abag.pds_rec_st and astcode.rec_code_st='1')
                    left join rec_code as cstcode
                    on (cstcode.rec_code_model='pds_rec_st' and cstcode.rec_code_group='pds_rec_st' 
                    and cstcode.rec_code_short=cbag.pds_rec_st and cstcode.rec_code_st='1')
                    where abag.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    and abag.pds_rec_op_type='ABAG'
                    and abag.pds_rec_st <> 'D'";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (abag.pds_rec_clinical = @pds_rec_clinical or cbag.pds_rec_clinical = @pds_rec_clinical)";
                    if (!param.pds_rec_bed.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (abag.pds_rec_bed = @pds_rec_bed or cbag.pds_rec_bed = @pds_rec_bed)";
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (abag.pds_rec_pat_no = @pds_rec_pat_no or cbag.pds_rec_pat_no = @pds_rec_pat_no)";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icbcode_fee_key = @icbcode_fee_key";

                    // 案例：餐包、配藥單只有公藥等不需調劑的藥
                    sql += @"
                    union
                    select 
                    cbag.pds_rec_send_dt, '藥車-藥袋' as pds_rec_op_name, cbag.pds_rec_bag_code,
                    case when isnull(cbag.pds_rec_clinical,'')='' then abag.pds_rec_clinical
                    else cbag.pds_rec_clinical end as c_pds_rec_clinical,
                    case when isnull(cbag.pds_rec_bed,'')='' then abag.pds_rec_bed
                    else cbag.pds_rec_bed end as c_pds_rec_bed,
                    cbag.pds_rec_pat_no,
                    icbcode.*,
                    abag.pds_rec_op_dtm_begin, abag.pds_rec_op_dtm_end, 
                    abag.pds_rec_st, astcode.rec_code_name as pds_rec_st_name,
                    abag.pds_rec_md_name,
                    cbag.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
                    cbag.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end, 
                    cbag.pds_rec_st as c_pds_rec_st, cstcode.rec_code_name as c_pds_rec_st_name,
                    cbag.pds_rec_md_name as c_pds_rec_md_name
                    from pds_rec as cbag
                    left join pds_rec as abag
                    on (abag.pds_rec_bag_code = cbag.pds_rec_bag_code
                    and abag.pds_rec_op_type='ABAG'
                    and abag.pds_rec_st <> 'D')
                    left join mi_micbcode as icbcode
                    on (icbcode_code=cbag.pds_rec_bag_code)
                    left join rec_code as astcode
                    on (astcode.rec_code_model='pds_rec_st' and astcode.rec_code_group='pds_rec_st' 
                    and astcode.rec_code_short=abag.pds_rec_st and astcode.rec_code_st='1')
                    left join rec_code as cstcode
                    on (cstcode.rec_code_model='pds_rec_st' and cstcode.rec_code_group='pds_rec_st' 
                    and cstcode.rec_code_short=cbag.pds_rec_st and cstcode.rec_code_st='1')
                    where cbag.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    and cbag.pds_rec_op_type='CBAG'
                    and cbag.pds_rec_st <> 'D'
                    and abag.pds_rec_no is null";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (abag.pds_rec_clinical = @pds_rec_clinical or cbag.pds_rec_clinical = @pds_rec_clinical)";
                    if (!param.pds_rec_bed.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (abag.pds_rec_bed = @pds_rec_bed or cbag.pds_rec_bed = @pds_rec_bed)";
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and (abag.pds_rec_pat_no = @pds_rec_pat_no or cbag.pds_rec_pat_no = @pds_rec_pat_no)";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icbcode_fee_key = @icbcode_fee_key";
                    break;
                case 4: // 首日量作業依配藥單
                    icfcode_prt_dt_begin = DateTimeUtil.ConvertAD(param.pds_rec_op_dtm_begin_begin,
                       inFormat: "yyyy/MM/dd HH:mm:ss", outFormat: "yyyMMdd");
                    icfcode_prt_dt_end = DateTimeUtil.ConvertAD(param.pds_rec_op_dtm_begin_end,
                       inFormat: "yyyy/MM/dd HH:mm:ss", outFormat: "yyyMMdd");

                    //sql = $@"
                    //select 
                    //'首日量-配藥單' as pds_rec_op_name, 
                    //icfcode_lst_code as pds_rec_lst_code, icfcode_ipill_no,
                    //icfcode_clinical, icfcode_bed, icfcode_pat_no as pds_rec_pat_no,
                    //tra_odr_date, tra_odr_time, 
                    //substring(tra_prn_dtm,1,8) as tra_prn_date, substring(tra_prn_dtm,9,4) as tra_prn_time,
                    //icfcode_prt_dt, icfcode_prt_ti,
                    //alst.pds_rec_op_dtm_begin, alst.pds_rec_op_dtm_end, alst.pds_rec_md_name,
                    //clst.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
                    //clst.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end,
                    //clst.pds_rec_md_name as c_pds_rec_md_name,
                    //rlst.pds_rec_op_dtm_begin as r_pds_rec_op_dtm_begin, 
                    //rlst.pds_rec_op_dtm_end as r_pds_rec_op_dtm_end,
                    //rlst.pds_rec_md_name as r_pds_rec_md_name 
                    //from (
                    //    select icfcode_prt_dt, icfcode_prt_ti, icfcode_id, icfcode_pill_no,
                    //    convert(varchar,icfcode_prt_dt) + icfcode_id + str(icfcode_pill_no,4,'0') as icfcode_lst_code, 
                    //    icfcode_id + str(icfcode_pill_no, 4, '0') as icfcode_ipill_no,
                    //    icfcode_clinical, icfcode_bed, icfcode_pat_no,
                    //    tra_odr_date, tra_odr_time, min(str(tra_prn_date,8,'0')+str(tra_prn_time,4,'0')) as tra_prn_dtm
                    //    from mi_micfcode
                    //    left join ch_tra
                    //    on (tra_ipd_no=convert(decimal,icfcode_ipd_no) and tra_odr_no=convert(decimal,icfcode_odr_no) and tra_fee_no=icfcode_fee_no)
                    //    where icfcode_prt_dt between {icfcode_prt_dt_begin} and {icfcode_prt_dt_end}
                    //    and icfcode_st <> 'D'
                    //    group by icfcode_prt_dt, icfcode_prt_ti, icfcode_id, icfcode_pill_no, 
                    //    icfcode_clinical, icfcode_bed, icfcode_pat_no, tra_odr_date, tra_odr_time
                    //) as icbcode
                    //left join pds_rec as alst
                    //on (alst.pds_rec_lst_code = icfcode_lst_code
                    //and alst.pds_rec_op_type = 'FALST'
                    //and alst.pds_rec_st <> 'D')
                    //left join pds_rec as clst
                    //on (clst.pds_rec_lst_code = icfcode_lst_code
                    //and clst.pds_rec_op_type = 'FCLST'
                    //and clst.pds_rec_st <> 'D')
                    //left join pds_rec as rlst
                    //on (rlst.pds_rec_lst_code = icfcode_lst_code
                    //and rlst.pds_rec_op_type = 'FRLST'
                    //and rlst.pds_rec_st <> 'D')
                    //where (alst.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    //or clst.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    //or rlst.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end)
                    //and (alst.pds_rec_no is not null or clst.pds_rec_no is not null or rlst.pds_rec_no is not null)";
                    sql = $@"
                    select 
                    '首日量-配藥單' as pds_rec_op_name, 
                    icfcode_lst_code as pds_rec_lst_code, icfcode_ipill_no,
                    icfcode_clinical, icfcode_bed, icfcode_pat_no as pds_rec_pat_no,
                    tra_odr_date, tra_odr_time, tra_prn_date, tra_prn_time,
                    icfcode_prt_dt, icfcode_prt_ti,
                    alst.pds_rec_op_dtm_begin, alst.pds_rec_op_dtm_end, alst.pds_rec_md_name,
                    clst.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
                    clst.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end,
                    clst.pds_rec_md_name as c_pds_rec_md_name,
                    rlst.pds_rec_op_dtm_begin as r_pds_rec_op_dtm_begin, 
                    rlst.pds_rec_op_dtm_end as r_pds_rec_op_dtm_end,
                    rlst.pds_rec_md_name as r_pds_rec_md_name 
                    from (
                        select distinct icfcode_prt_dt, icfcode_prt_ti, icfcode_id, icfcode_pill_no,
                        convert(varchar,icfcode_prt_dt) + icfcode_id + str(icfcode_pill_no,4,'0') as icfcode_lst_code,
                        icfcode_id + str(icfcode_pill_no, 4, '0') as icfcode_ipill_no,
                        icfcode_clinical, icfcode_bed,icfcode_pat_no,
                        tra_odr_date, tra_odr_time, tra_prn_date, tra_prn_time
                        from mi_micfcode
                        left join ch_tra
                        on (tra_ipd_no=convert(decimal,icfcode_ipd_no) and tra_odr_no=convert(decimal,icfcode_odr_no) and tra_fee_no=icfcode_fee_no)
                        where icfcode_prt_dt between {icfcode_prt_dt_begin} and {icfcode_prt_dt_end}
                        and icfcode_st <> 'D'
                    ) as icbcode
                    left join pds_rec as alst
                    on (alst.pds_rec_lst_code = icfcode_lst_code
                    and alst.pds_rec_op_type = 'FALST'
                    and alst.pds_rec_st <> 'D')
                    left join pds_rec as clst
                    on (clst.pds_rec_lst_code = icfcode_lst_code
                    and clst.pds_rec_op_type = 'FCLST'
                    and clst.pds_rec_st <> 'D')
                    left join pds_rec as rlst
                    on (rlst.pds_rec_lst_code = icfcode_lst_code
                    and rlst.pds_rec_op_type = 'FRLST'
                    and rlst.pds_rec_st <> 'D')
                    where (alst.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    or clst.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    or rlst.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end)
                    and (alst.pds_rec_no is not null or clst.pds_rec_no is not null or rlst.pds_rec_no is not null)";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_clinical = @pds_rec_clinical";
                    if (!param.pds_rec_bed.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_bed = @pds_rec_bed";
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_pat_no = @pds_rec_pat_no";
                    break;
                case 5:  // 首日量作業依藥袋
                    icfcode_prt_dt_begin = DateTimeUtil.ConvertAD(param.pds_rec_op_dtm_begin_begin,
                       inFormat: "yyyy/MM/dd HH:mm:ss", outFormat: "yyyMMdd");
                    icfcode_prt_dt_end = DateTimeUtil.ConvertAD(param.pds_rec_op_dtm_begin_end,
                       inFormat: "yyyy/MM/dd HH:mm:ss", outFormat: "yyyMMdd");

                    sql = @"
                    select ";
                    sql += fst_select;
                    sql += $@"
                    , '首日量-藥袋' as pds_rec_op_name, 
                    icfcode_code as pds_rec_bag_code,
                    icfcode_pat_no as pds_rec_pat_no,
                    tra_odr_date, tra_odr_time, tra_prn_date, tra_prn_time,
                    abag.pds_rec_op_dtm_begin, abag.pds_rec_op_dtm_end, 
                    abag.pds_rec_st, astcode.rec_code_name as pds_rec_st_name,
                    abag.pds_rec_md_name,
                    cbag.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
                    cbag.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end, 
                    cbag.pds_rec_st as c_pds_rec_st, cstcode.rec_code_name as c_pds_rec_st_name,
                    cbag.pds_rec_md_name as c_pds_rec_md_name,
                    rbag.pds_rec_op_dtm_begin as r_pds_rec_op_dtm_begin, 
                    rbag.pds_rec_op_dtm_end as r_pds_rec_op_dtm_end, 
                    rbag.pds_rec_st as r_pds_rec_st, rstcode.rec_code_name as r_pds_rec_st_name,
                    rbag.pds_rec_md_name as r_pds_rec_md_name
                    from mi_micfcode as icbcode
                    left join ch_tra
                    on (tra_ipd_no=convert(decimal,icfcode_ipd_no) and tra_odr_no=convert(decimal,icfcode_odr_no) and tra_fee_no=icfcode_fee_no)
                    left join pds_rec as abag
                    on (abag.pds_rec_bag_code = icfcode_code
                    and abag.pds_rec_op_type='FABAG'
                    and abag.pds_rec_st <> 'D')
                    left join pds_rec as cbag
                    on (cbag.pds_rec_bag_code = icfcode_code
                    and cbag.pds_rec_op_type='FCBAG'
                    and cbag.pds_rec_st <> 'D')
                    left join pds_rec as rbag
                    on (rbag.pds_rec_bag_code = icfcode_code
                    and rbag.pds_rec_op_type='FRBAG'
                    and rbag.pds_rec_st <> 'D')
                    left join rec_code as astcode
                    on (astcode.rec_code_model='pds_rec_st' and astcode.rec_code_group='pds_rec_st' 
                    and astcode.rec_code_short=abag.pds_rec_st and astcode.rec_code_st='1')
                    left join rec_code as cstcode
                    on (cstcode.rec_code_model='pds_rec_st' and cstcode.rec_code_group='pds_rec_st' 
                    and cstcode.rec_code_short=cbag.pds_rec_st and cstcode.rec_code_st='1')
                    left join rec_code as rstcode
                    on (rstcode.rec_code_model='pds_rec_st' and rstcode.rec_code_group='pds_rec_st' 
                    and rstcode.rec_code_short=rbag.pds_rec_st and rstcode.rec_code_st='1')
                    where (icfcode_prt_dt between {icfcode_prt_dt_begin} and {icfcode_prt_dt_end})
                    and (abag.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    or cbag.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end
                    or rbag.pds_rec_op_dtm_begin between @pds_rec_op_dtm_begin_begin and @pds_rec_op_dtm_begin_end)
                    and (abag.pds_rec_no is not null or cbag.pds_rec_no is not null or rbag.pds_rec_no is not null)";
                    if (!param.pds_rec_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_clinical = @pds_rec_clinical";
                    if (!param.pds_rec_bed.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_bed = @pds_rec_bed";
                    if (!param.pds_rec_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_pat_no = @pds_rec_pat_no";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_fee_key = @icbcode_fee_key";
                    break;
            }

            var queryList = DB.Syb1.Query<PdsRecAC>(sql, param).ToList();

            var mh_mpat = new Mh_mpat();
            var pds_recd = new Pds_recd();
            string pds_rec_op_dtm_begin = string.Empty;
            string pds_rec_op_dtm_end = string.Empty;

            if (option == 4) // 因為同張配藥單其藥袋的 tra_prn_date, tra_prn_time 會不同，故取其一。group by sql 速度慢，故採此法。
                queryList = queryList.DistinctBy(r => r.pds_rec_lst_code).ToList();

            queryList.ForEach(r =>
            {
                r.pds_rec_op_dtm_diff = Util.DateTime.DateTimeDiff(r.pds_rec_op_dtm_begin, r.pds_rec_op_dtm_end);
                r.c_pds_rec_op_dtm_diff = Util.DateTime.DateTimeDiff(r.c_pds_rec_op_dtm_begin, r.c_pds_rec_op_dtm_end);
                r.r_pds_rec_op_dtm_diff = Util.DateTime.DateTimeDiff(r.r_pds_rec_op_dtm_begin, r.r_pds_rec_op_dtm_end);

                if (option == 1 || option == 2 || option == 3)
                    r.pds_rec_send_dt_fmt = DateTimeUtil.ConvertROC(r.pds_rec_send_dt.ToString().PadLeft(7, '0'));

                if (option == 4 || option == 5)
                {
                    //r.icfcode_prt_dt_fmt = DateTimeUtil.ConvertROC(r.icfcode_prt_dt.ToString().PadLeft(7, '0'));
                    //r.icfcode_prt_dtm = DateTimeUtil.ConvertROC(r.icfcode_prt_dt.ToString().PadLeft(7, '0') + r.icfcode_prt_ti.ToString().PadLeft(6, '0'),
                    //    true, "yyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");
                    r.tra_prn_dtm = DateTimeUtil.ConvertAD(r.tra_prn_date.ToString().PadLeft(8, '0') + r.tra_prn_time.ToString().PadLeft(4, '0') + "00",
                        false, "yyyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");
                    r.tra_odr_dtm = DateTimeUtil.ConvertROC(r.tra_odr_date.ToString().PadLeft(7, '0') + r.tra_odr_time.ToString().PadLeft(4, '0') + "00",
                        true, "yyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");

                    if (r.tra_odr_dtm.CompareTo(r.tra_prn_dtm) > 0 &&
                    (r.tra_odr_time == 2359 && r.tra_prn_time == 0))
                    {
                        // cobol 取 date & time 是分開取的，因應剛好跨日問題
                        r.tra_prn_dtm = r.tra_odr_dtm;
                        r.tra_prn_date = DateTimeUtil.ConvertROC(r.tra_odr_date.ToString().PadLeft(7, '0'), outFormat: "yyyyMMdd").ToNullableInt();
                        r.tra_prn_time = r.tra_odr_time;
                    }

                    if (r.tra_odr_dtm.CompareTo(r.tra_prn_dtm) > 0)
                    {
                        // 取最小值當處方開立時間
                        r.tra_odr_date = DateTimeUtil.ConvertAD(r.tra_prn_date.ToString(), outFormat: "yyyMMdd").ToNullableInt();
                        r.tra_odr_time = r.tra_prn_time;
                        r.tra_odr_dtm = r.tra_prn_dtm;
                    }

                    pds_rec_op_dtm_begin = r.pds_rec_op_dtm_begin;
                    if (pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) pds_rec_op_dtm_begin = r.c_pds_rec_op_dtm_begin;
                    if (pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) pds_rec_op_dtm_begin = r.r_pds_rec_op_dtm_begin;
                    pds_rec_op_dtm_end = r.r_pds_rec_op_dtm_end;
                    if (pds_rec_op_dtm_end.IsNullOrWhiteSpace()) pds_rec_op_dtm_end = r.c_pds_rec_op_dtm_end;
                    if (pds_rec_op_dtm_end.IsNullOrWhiteSpace()) pds_rec_op_dtm_end = r.pds_rec_op_dtm_end;
                    r.ar_pds_rec_op_dtm_diff = Util.DateTime.DateTimeDiff(pds_rec_op_dtm_begin, pds_rec_op_dtm_end);
                }

                if (option == 4)
                {
                    pds_rec_op_dtm_begin = r.tra_odr_dtm;
                    if (pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) pds_rec_op_dtm_begin = r.pds_rec_op_dtm_begin;
                    if (pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) pds_rec_op_dtm_begin = r.c_pds_rec_op_dtm_begin;
                    if (pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) pds_rec_op_dtm_begin = r.r_pds_rec_op_dtm_begin;
                    pds_rec_op_dtm_end = r.r_pds_rec_op_dtm_end;
                    if (pds_rec_op_dtm_end.IsNullOrWhiteSpace()) pds_rec_op_dtm_end = r.c_pds_rec_op_dtm_end;
                    if (pds_rec_op_dtm_end.IsNullOrWhiteSpace()) pds_rec_op_dtm_end = r.pds_rec_op_dtm_end;
                    if (pds_rec_op_dtm_end.IsNullOrWhiteSpace()) pds_rec_op_dtm_end = r.tra_odr_dtm;
                    r.or_pds_rec_op_dtm_diff = Util.DateTime.DateTimeDiff(pds_rec_op_dtm_begin, pds_rec_op_dtm_end);
                }

                if (option == 3 || option == 5)
                {
                    r.icbcode_rx_uqty = Util.Medical.DoseFormat(r.icbcode_rx_uqty1, r.icbcode_rx_uqty2);
                    r.icbcode_rx_qty = Util.Medical.DoseFormat(r.icbcode_rx_qty1, r.icbcode_rx_qty2, false);
                    if (r.icbcode_pack.IsNumeric()) r.icbcode_pack = double.Parse(r.icbcode_pack).ToString();

                    // 實際交車量
                    if (r.pds_rec_st == Pds_recParam.Rec_st.S || r.c_pds_rec_st == Pds_recParam.Rec_st.S || r.r_pds_rec_st == Pds_recParam.Rec_st.S)
                        r.qty_pack_final = string.Empty;
                    else
                    {
                        // 2: 依主檔作業類型、藥袋條碼、歷程檔狀態原因，查詢最新處方修改、核對處方修改
                        // 4: 依主檔作業類型、藥袋條碼、歷程檔狀態原因，查詢首日量最新調劑/核對/發藥處方修改
                        pds_recd.pds_rec_bag_code = r.pds_rec_bag_code;
                        r.qty_pack_final = DB.Pds_recdRepository.QueryPds_recd(pds_recd,
                            (option == 3) ? 2 : 4).Data.FirstOrDefault()?.pds_recd_md_qty ?? string.Empty;
                    }
                }

                switch (option)
                {
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        mh_mpat.pat_no = r.pds_rec_pat_no.ToNullableInt();
                        if (mh_mpat.pat_no != null)
                        {
                            // 1: 依參數自動組建
                            r.pat_name = DB.Mh_mpatRepository.QueryMh_mpat(mh_mpat, 1).Data.FirstOrDefault()?.pat_name;
                        }
                        break;
                }

            });

            return new ApiResult<List<PdsRecAC>>(queryList);
        }

        public ApiResult<List<FstAvg>> QueryFstAvg(FstAvg param, int option = 0)
        {
            string sql = string.Empty;

            //sql = @"
            //select 
            //icfcode_id, icfcode_pill_no, icfcode_lst_type,
            //tra_odr_date, tra_odr_time, icfcode_prt_dt, icfcode_prt_ti,
            //alst.pds_rec_op_dtm_begin, alst.pds_rec_op_dtm_end,
            //clst.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
            //clst.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end,
            //rlst.pds_rec_op_dtm_begin as r_pds_rec_op_dtm_begin, 
            //rlst.pds_rec_op_dtm_end as r_pds_rec_op_dtm_end
            //from (
            //    select distinct icfcode_prt_dt, icfcode_prt_ti, icfcode_id, icfcode_pill_no,
            //    convert(varchar,icfcode_prt_dt) + icfcode_id + str(icfcode_pill_no,4,'0') as icfcode_lst_code,
            //    icfcode_lst_type, tra_odr_date, tra_odr_time
            //    from mi_micfcode
            //    left join ch_tra
            //    on (tra_ipd_no=convert(decimal,icfcode_ipd_no) and tra_odr_no=convert(decimal,icfcode_odr_no) 
            //    and tra_fee_no=icfcode_fee_no)
            //    where icfcode_prt_dt between  @icfcode_prt_dt_begin and @icfcode_prt_dt_end
            //    and icfcode_st <> 'D'
            //) as icfcode
            //left join pds_rec as alst
            //on (alst.pds_rec_lst_code = icfcode_lst_code
            //and alst.pds_rec_op_type = 'FALST'
            //and alst.pds_rec_st <> 'D')
            //left join pds_rec as clst
            //on (clst.pds_rec_lst_code = icfcode_lst_code
            //and clst.pds_rec_op_type = 'FCLST'
            //and clst.pds_rec_st <> 'D')
            //left join pds_rec as rlst
            //on (rlst.pds_rec_lst_code = icfcode_lst_code
            //and rlst.pds_rec_op_type = 'FRLST'
            //and rlst.pds_rec_st <> 'D')
            //where 1=1";
            sql = @"
            select 
            icfcode_id, icfcode_pill_no, icfcode_lst_type,
            tra_odr_date, tra_odr_time, tra_prn_date, tra_prn_time, 
            icfcode_prt_dt, icfcode_prt_ti,
            alst.pds_rec_op_dtm_begin, alst.pds_rec_op_dtm_end,
            clst.pds_rec_op_dtm_begin as c_pds_rec_op_dtm_begin, 
            clst.pds_rec_op_dtm_end as c_pds_rec_op_dtm_end,
            rlst.pds_rec_op_dtm_begin as r_pds_rec_op_dtm_begin, 
            rlst.pds_rec_op_dtm_end as r_pds_rec_op_dtm_end
            from (
                select distinct icfcode_prt_dt, icfcode_prt_ti, icfcode_id, icfcode_pill_no,
                convert(varchar,icfcode_prt_dt) + icfcode_id + str(icfcode_pill_no,4,'0') as icfcode_lst_code,
                icfcode_lst_type, tra_odr_date, tra_odr_time, tra_prn_date, tra_prn_time
                from mi_micfcode
                left join ch_tra
                on (tra_ipd_no=convert(decimal,icfcode_ipd_no) and tra_odr_no=convert(decimal,icfcode_odr_no) 
                and tra_fee_no=icfcode_fee_no)
                where icfcode_prt_dt between  @icfcode_prt_dt_begin and @icfcode_prt_dt_end
                and icfcode_st <> 'D'
            ) as icfcode
            left join pds_rec as alst
            on (alst.pds_rec_lst_code = icfcode_lst_code
            and alst.pds_rec_op_type = 'FALST'
            and alst.pds_rec_st <> 'D')
            left join pds_rec as clst
            on (clst.pds_rec_lst_code = icfcode_lst_code
            and clst.pds_rec_op_type = 'FCLST'
            and clst.pds_rec_st <> 'D')
            left join pds_rec as rlst
            on (rlst.pds_rec_lst_code = icfcode_lst_code
            and rlst.pds_rec_op_type = 'FRLST'
            and rlst.pds_rec_st <> 'D')
            where 1=1";
            if (!param.icfcode_lst_type.IsNullOrWhiteSpace())
                sql += Environment.NewLine + "and icfcode_lst_type = @icfcode_lst_type";

            // 因應剛好跨日問題
            param.icfcode_prt_dt_end = Util.DateTime.ROCNextDay(param.icfcode_prt_dt_end.ToString()).ToNullableInt();
            var queryList = DB.Syb1.Query<PdsRecAC>(sql, param).ToList();
            // 因為同張配藥單其藥袋的 tra_prn_date, tra_prn_time 會不同，故取其一。group by sql 速度慢，故採此法。
            queryList = queryList.DistinctBy(r => new { r.icfcode_prt_dt, r.icfcode_id, r.icfcode_pill_no }).ToList();

            queryList.ForEach(r =>
            {
                r.tra_prn_dtm = DateTimeUtil.ConvertAD(r.tra_prn_date.ToString().PadLeft(8, '0') + r.tra_prn_time.ToString().PadLeft(4, '0') + "00",
                    false, "yyyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");
                r.tra_odr_dtm = DateTimeUtil.ConvertROC(r.tra_odr_date.ToString().PadLeft(7, '0') + r.tra_odr_time.ToString().PadLeft(4, '0') + "00",
                    true, "yyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");

                if (r.tra_odr_dtm.CompareTo(r.tra_prn_dtm) > 0 &&
                (r.tra_odr_time == 2359 && r.tra_prn_time == 0))
                {
                    // cobol 取 date & time 是分開取的，因應剛好跨日問題
                    r.tra_prn_dtm = r.tra_odr_dtm;
                    r.tra_prn_date = DateTimeUtil.ConvertROC(r.tra_odr_date.ToString().PadLeft(7, '0'), outFormat: "yyyyMMdd").ToNullableInt();
                    r.tra_prn_time = r.tra_odr_time;
                }

                if (r.tra_odr_dtm.CompareTo(r.tra_prn_dtm) > 0)
                {
                    // 取最小值當處方開立時間
                    r.tra_odr_date = DateTimeUtil.ConvertAD(r.tra_prn_date.ToString(), outFormat: "yyyMMdd").ToNullableInt();
                    r.tra_odr_time = r.tra_prn_time;
                    r.tra_odr_dtm = r.tra_prn_dtm;
                }
            });

            List<FstAvg> fstAvgList = new List<FstAvg>();
            FstAvg fstAvg;
            IEnumerable<PdsRecAC> lsts;
            DateTime begin, end;
            string propName = string.Empty;
            double diffTotal = 0;
            switch (option)
            {
                case 1: // 開立-調劑
                    foreach (var lstDate in queryList.DistinctBy(r => r.tra_odr_date).
                        Where(r => r.tra_odr_date != 0 && r.tra_odr_date.HasValue))
                    {
                        fstAvg = new FstAvg();
                        fstAvg.pds_rec_op_name = "開立-調劑";
                        fstAvg.dt = lstDate.tra_odr_dtm.SubStr(0, 10);
                        if (fstAvg.dt.CompareTo(param.icfcode_prt_dt_end_fmt) > 0) // 因應剛好跨日問題
                            continue;
                        fstAvg.dayofWeek = Util.DateTime.DayOfWeek(fstAvg.dt);

                        begin = DateTime.Parse(fstAvg.dt + " 00:00:00");
                        end = DateTime.Parse(fstAvg.dt + " 23:59:59");
                        while (begin <= end)
                        {
                            lsts = queryList.Where(r => (!r.tra_odr_dtm.IsNullOrWhiteSpace()) &&
                              (!r.pds_rec_op_dtm_end.IsNullOrWhiteSpace()) &&
                             r.tra_odr_dtm.CompareTo(begin.ToString("yyyy/MM/dd HH:mm:ss")) >= 0 &&
                             r.tra_odr_dtm.CompareTo(begin.AddMinutes(30).ToString("yyyy/MM/dd HH:mm:ss")) < 0);
                            propName = $"hr{begin.ToString("HHmm")}";
                            diffTotal = 0;
                            foreach (var lst in lsts)
                                diffTotal += Util.DateTime.DateTimeDiffTS(lst.tra_odr_dtm, lst.pds_rec_op_dtm_end).TotalMinutes;
                            fstAvg.SetPropertyValue(propName,
                                diffTotal == 0 ? 0 : Math.Round(diffTotal / lsts.Count(), 2, MidpointRounding.AwayFromZero));
                            begin = begin.AddMinutes(30);
                        }

                        fstAvgList.Add(fstAvg);
                    }
                    break;
                case 2: // 開立-核對
                    foreach (var lstDate in queryList.DistinctBy(r => r.tra_odr_date).
                        Where(r => r.tra_odr_date != 0 && r.tra_odr_date.HasValue))
                    {
                        fstAvg = new FstAvg();
                        fstAvg.pds_rec_op_name = "開立-核對";
                        fstAvg.dt = lstDate.tra_odr_dtm.SubStr(0, 10);
                        if (fstAvg.dt.CompareTo(param.icfcode_prt_dt_end_fmt) > 0) // 因應剛好跨日問題
                            continue;
                        fstAvg.dayofWeek = Util.DateTime.DayOfWeek(fstAvg.dt);

                        begin = DateTime.Parse(fstAvg.dt + " 00:00:00");
                        end = DateTime.Parse(fstAvg.dt + " 23:59:59");
                        while (begin <= end)
                        {
                            lsts = queryList.Where(r => (!r.tra_odr_dtm.IsNullOrWhiteSpace()) &&
                              (!r.c_pds_rec_op_dtm_end.IsNullOrWhiteSpace()) &&
                             r.tra_odr_dtm.CompareTo(begin.ToString("yyyy/MM/dd HH:mm:ss")) >= 0 &&
                             r.tra_odr_dtm.CompareTo(begin.AddMinutes(30).ToString("yyyy/MM/dd HH:mm:ss")) < 0);
                            propName = $"hr{begin.ToString("HHmm")}";
                            diffTotal = 0;
                            foreach (var lst in lsts)
                                diffTotal += Util.DateTime.DateTimeDiffTS(lst.tra_odr_dtm, lst.c_pds_rec_op_dtm_end).TotalMinutes;
                            fstAvg.SetPropertyValue(propName,
                                diffTotal == 0 ? 0 : Math.Round(diffTotal / lsts.Count(), 2, MidpointRounding.AwayFromZero));
                            begin = begin.AddMinutes(30);
                        }

                        fstAvgList.Add(fstAvg);
                    }
                    break;
                case 3: // 開立-發藥
                    foreach (var lstDate in queryList.DistinctBy(r => r.tra_odr_date).
                        Where(r => r.tra_odr_date != 0 && r.tra_odr_date.HasValue))
                    {
                        fstAvg = new FstAvg();
                        fstAvg.pds_rec_op_name = "開立-發藥";
                        fstAvg.dt = lstDate.tra_odr_dtm.SubStr(0, 10);
                        if (fstAvg.dt.CompareTo(param.icfcode_prt_dt_end_fmt) > 0) // 因應剛好跨日問題
                            continue;
                        fstAvg.dayofWeek = Util.DateTime.DayOfWeek(fstAvg.dt);

                        begin = DateTime.Parse(fstAvg.dt + " 00:00:00");
                        end = DateTime.Parse(fstAvg.dt + " 23:59:59");
                        while (begin <= end)
                        {
                            lsts = queryList.Where(r => (!r.tra_odr_dtm.IsNullOrWhiteSpace()) &&
                              (!r.r_pds_rec_op_dtm_end.IsNullOrWhiteSpace()) &&
                             r.tra_odr_dtm.CompareTo(begin.ToString("yyyy/MM/dd HH:mm:ss")) >= 0 &&
                             r.tra_odr_dtm.CompareTo(begin.AddMinutes(30).ToString("yyyy/MM/dd HH:mm:ss")) < 0);
                            propName = $"hr{begin.ToString("HHmm")}";
                            diffTotal = 0;
                            foreach (var lst in lsts)
                                diffTotal += Util.DateTime.DateTimeDiffTS(lst.tra_odr_dtm, lst.r_pds_rec_op_dtm_end).TotalMinutes;
                            fstAvg.SetPropertyValue(propName,
                                diffTotal == 0 ? 0 : Math.Round(diffTotal / lsts.Count(), 2, MidpointRounding.AwayFromZero));
                            begin = begin.AddMinutes(30);
                        }

                        fstAvgList.Add(fstAvg);
                    }
                    break;
                case 4: // 調劑-核對
                    foreach (var lstDate in queryList.DistinctBy(r => r.pds_rec_op_dtm_begin.SubStr(0, 10)).
                        Where(r => !r.pds_rec_op_dtm_begin.IsNullOrWhiteSpace()))
                    {
                        fstAvg = new FstAvg();
                        fstAvg.pds_rec_op_name = "調劑-核對";
                        fstAvg.dt = lstDate.pds_rec_op_dtm_begin.SubStr(0, 10);
                        if (fstAvg.dt.CompareTo(param.icfcode_prt_dt_end_fmt) > 0) // 因應剛好跨日問題
                            continue;
                        fstAvg.dayofWeek = Util.DateTime.DayOfWeek(fstAvg.dt);

                        begin = DateTime.Parse(fstAvg.dt + " 00:00:00");
                        end = DateTime.Parse(fstAvg.dt + " 23:59:59");
                        while (begin <= end)
                        {
                            lsts = queryList.Where(r => (!r.pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) &&
                              (!r.c_pds_rec_op_dtm_end.IsNullOrWhiteSpace()) &&
                             r.pds_rec_op_dtm_begin.CompareTo(begin.ToString("yyyy/MM/dd HH:mm:ss")) >= 0 &&
                             r.pds_rec_op_dtm_begin.CompareTo(begin.AddMinutes(30).ToString("yyyy/MM/dd HH:mm:ss")) < 0);
                            propName = $"hr{begin.ToString("HHmm")}";
                            diffTotal = 0;
                            foreach (var lst in lsts)
                                diffTotal += Util.DateTime.DateTimeDiffTS(lst.pds_rec_op_dtm_begin, lst.c_pds_rec_op_dtm_end).TotalMinutes;
                            fstAvg.SetPropertyValue(propName,
                                diffTotal == 0 ? 0 : Math.Round(diffTotal / lsts.Count(), 2, MidpointRounding.AwayFromZero));
                            begin = begin.AddMinutes(30);
                        }

                        fstAvgList.Add(fstAvg);
                    }
                    break;
                case 5: // 調劑-發藥
                    foreach (var lstDate in queryList.DistinctBy(r => r.pds_rec_op_dtm_begin.SubStr(0, 10)).
                        Where(r => !r.pds_rec_op_dtm_begin.IsNullOrWhiteSpace()))
                    {
                        fstAvg = new FstAvg();
                        fstAvg.pds_rec_op_name = "調劑-發藥";
                        fstAvg.dt = lstDate.pds_rec_op_dtm_begin.SubStr(0, 10);
                        if (fstAvg.dt.CompareTo(param.icfcode_prt_dt_end_fmt) > 0) // 因應剛好跨日問題
                            continue;
                        fstAvg.dayofWeek = Util.DateTime.DayOfWeek(fstAvg.dt);

                        begin = DateTime.Parse(fstAvg.dt + " 00:00:00");
                        end = DateTime.Parse(fstAvg.dt + " 23:59:59");
                        while (begin <= end)
                        {
                            lsts = queryList.Where(r => (!r.pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) &&
                              (!r.r_pds_rec_op_dtm_end.IsNullOrWhiteSpace()) &&
                             r.pds_rec_op_dtm_begin.CompareTo(begin.ToString("yyyy/MM/dd HH:mm:ss")) >= 0 &&
                             r.pds_rec_op_dtm_begin.CompareTo(begin.AddMinutes(30).ToString("yyyy/MM/dd HH:mm:ss")) < 0);
                            propName = $"hr{begin.ToString("HHmm")}";
                            diffTotal = 0;
                            foreach (var lst in lsts)
                                diffTotal += Util.DateTime.DateTimeDiffTS(lst.pds_rec_op_dtm_begin, lst.r_pds_rec_op_dtm_end).TotalMinutes;
                            fstAvg.SetPropertyValue(propName,
                                diffTotal == 0 ? 0 : Math.Round(diffTotal / lsts.Count(), 2, MidpointRounding.AwayFromZero));
                            begin = begin.AddMinutes(30);
                        }

                        fstAvgList.Add(fstAvg);
                    }
                    break;
                case 6: // 核對-發藥
                    foreach (var lstDate in queryList.DistinctBy(r => r.c_pds_rec_op_dtm_begin.SubStr(0, 10)).
                        Where(r => !r.c_pds_rec_op_dtm_begin.IsNullOrWhiteSpace()))
                    {
                        fstAvg = new FstAvg();
                        fstAvg.pds_rec_op_name = "核對-發藥";
                        fstAvg.dt = lstDate.c_pds_rec_op_dtm_begin.SubStr(0, 10);
                        if (fstAvg.dt.CompareTo(param.icfcode_prt_dt_end_fmt) > 0) // 因應剛好跨日問題
                            continue;
                        fstAvg.dayofWeek = Util.DateTime.DayOfWeek(fstAvg.dt);

                        begin = DateTime.Parse(fstAvg.dt + " 00:00:00");
                        end = DateTime.Parse(fstAvg.dt + " 23:59:59");
                        while (begin <= end)
                        {
                            lsts = queryList.Where(r => (!r.c_pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) &&
                              (!r.r_pds_rec_op_dtm_end.IsNullOrWhiteSpace()) &&
                             r.c_pds_rec_op_dtm_begin.CompareTo(begin.ToString("yyyy/MM/dd HH:mm:ss")) >= 0 &&
                             r.c_pds_rec_op_dtm_begin.CompareTo(begin.AddMinutes(30).ToString("yyyy/MM/dd HH:mm:ss")) < 0);
                            propName = $"hr{begin.ToString("HHmm")}";
                            diffTotal = 0;
                            foreach (var lst in lsts)
                                diffTotal += Util.DateTime.DateTimeDiffTS(lst.c_pds_rec_op_dtm_begin, lst.r_pds_rec_op_dtm_end).TotalMinutes;
                            fstAvg.SetPropertyValue(propName,
                                diffTotal == 0 ? 0 : Math.Round(diffTotal / lsts.Count(), 2, MidpointRounding.AwayFromZero));
                            begin = begin.AddMinutes(30);
                        }

                        fstAvgList.Add(fstAvg);
                    }
                    break;
            }

            return new ApiResult<List<FstAvg>>(fstAvgList);
        }

        /// <summary>
        /// 查詢並建立主檔單號/明細檔單號
        /// </summary>
        /// <returns>主檔 existRec is null 為新增；existRec isnot null 為修改</returns>
        private Pds_rec SetPdsRecNo(Pds_rec param, int option)
        {
            RECSerialNo sno = new RECSerialNo();
            sno.SDATE = DateTime.Now.ToString("yyyyMMdd");

            Pds_rec existRec = null;
            switch (param.pds_rec_op_type)
            {
                case Pds_recParam.Rec_op_type.ACAR:
                case Pds_recParam.Rec_op_type.CCAR:
                    // 2: 依作業類型、傳送日期、護理站
                    existRec = QueryPds_rec(param, 2).Data.FirstOrDefault();
                    break;
                case Pds_recParam.Rec_op_type.ALST:
                case Pds_recParam.Rec_op_type.CLST:
                case Pds_recParam.Rec_op_type.FALST:
                case Pds_recParam.Rec_op_type.FCLST:
                case Pds_recParam.Rec_op_type.FRLST:
                    // 1: 依參數自動組建
                    existRec = QueryPds_rec(new Pds_rec
                    {
                        pds_rec_op_type = param.pds_rec_op_type,
                        pds_rec_lst_code = param.pds_rec_lst_code
                    }, 1).Data.FirstOrDefault();
                    break;
                default: // ABAG、CBAG、FABAG、FCBAG、FRBAG
                    if (option == 1 || option == 11)
                    {
                        // 1: 依參數自動組建
                        existRec = QueryPds_rec(new Pds_rec
                        {
                            pds_rec_op_type = param.pds_rec_op_type,
                            pds_rec_bag_code = param.pds_rec_bag_code,
                        }, 1).Data.FirstOrDefault();
                    }
                    else if (option == 2 || option == 12)
                    {
                        // 6: 依主檔單號
                        existRec = QueryPds_rec(param, 6).Data.FirstOrDefault();
                    }
                    break;
            }

            if (existRec != null)
            { // update
                param.pds_rec_no = existRec.pds_rec_no;
                param.pds_rec_op_dtm_begin = existRec.pds_rec_op_dtm_begin;

                // 轉入4級管制藥調劑資訊，其調劑時間是過去的
                var endDtmResult = DateTimeUtil.CompareStrDateTime(existRec.pds_rec_op_dtm_end, param.pds_rec_op_dtm_end);
                if (endDtmResult == StrParam.CompareResult.OneMoreTwo)
                    param.pds_rec_op_dtm_end = existRec.pds_rec_op_dtm_end;

                // 若有處方修改才更新，否則保留最後改過的處方修改
                if (param.pds_rec_md_qty.IsNullOrWhiteSpace() &&
                param.pds_rec_md_way1.IsNullOrWhiteSpace() &&
                (param.pds_rec_op_type == Pds_recParam.Rec_op_type.ABAG ||
                param.pds_rec_op_type == Pds_recParam.Rec_op_type.CBAG ||
                param.pds_rec_op_type == Pds_recParam.Rec_op_type.FABAG ||
                param.pds_rec_op_type == Pds_recParam.Rec_op_type.FCBAG ||
                param.pds_rec_op_type == Pds_recParam.Rec_op_type.FRBAG))
                {
                    param.pds_rec_md_qty = existRec.pds_rec_md_qty;
                    param.pds_rec_md_way1 = existRec.pds_rec_md_way1;
                }
            }
            else
            { // insert
                sno.SYSID = SNoParam.PDSREC;
                param.pds_rec_no = DB.RECSerialNoRepository.CreateRECSerialNo(sno, sno.SDATE, 10).Data;
            }

            sno.SYSID = SNoParam.PDSRECD;
            param?.pds_recdList?.ForEach(d =>
            {
                d.pds_recd_rec_no = param.pds_rec_no;
                if (d.pds_recd_no.IsNullOrWhiteSpace())
                    d.pds_recd_no = DB.RECSerialNoRepository.CreateRECSerialNo(sno, sno.SDATE, 10).Data;
                d.pds_recd_md_dt = param.pds_rec_md_dt;
                d.pds_recd_md_time = param.pds_rec_md_time;
            });

            return existRec;
        }

        public ApiResult<Pds_rec> SavePds_rec(Pds_rec param, int option = 0)
        {
            int rowsAffected = 0;
            bool? succ = null;
            string msg = string.Empty;
            HashSet<string> excludeCol = null;
            List<SqlCmdUtil> sqlCmds = null;
            var notDoneSt = new HashSet<string> { "N", "C", "U", "", null };

            if (param.pds_rec_op_type.IsNullOrWhiteSpace() &&
               (option != 8))
            {
                rowsAffected = 0; msg = MsgParam.PdsRecNoOpType; goto exit;
            }

            if (param.pds_rec_lst_code.IsNullOrWhiteSpace() &&
                (option == 1 || option == 2 || option == 5 || option == 6 || option == 8 ||
                option == 9 || option == 10 || option == 11 || option == 12))
            {
                rowsAffected = 0; msg = MsgParam.PdsRecNoLstCode; goto exit;
            }

            if (param.pds_rec_bag_code.IsNullOrWhiteSpace() &&
                (option == 1 || option == 2 || option == 11 || option == 12))
            {
                rowsAffected = 0; msg = MsgParam.PdsRecNoBagCode; goto exit;
            }

            switch (option)
            {
                case 1:
                case 2:
                case 11:
                case 12:
                    excludeCol = new HashSet<string> { "pds_rec_note" };
                    break;
            }

            param.StrProcess();
            param?.pds_recdList?.ForEach(d => d.StrProcess());
            param.pds_rec_md_dt = DateTime.Now.ToString("yyyy/MM/dd");
            param.pds_rec_md_time = DateTime.Now.ToString("HH:mm:ss");

            switch (option)
            {
                case 1: // 藥車調劑/核對作業，排除備註欄位儲存
                case 2: // 藥車無法交車註記，排除備註欄位儲存
                    // ABAG、CBAG
                    Pds_rec existBag = SetPdsRecNo(param, option);
                    sqlCmds = new List<SqlCmdUtil>();
                    if (existBag == null)
                        sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_rec), param, excludeCol));
                    else
                        sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), param, excludeCol));
                    param?.pds_recdList?.ForEach(d =>
                    {
                        sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_recd), d));
                    });
                    rowsAffected = DB.Syb1.Execute(sqlCmds);
                    msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    if (rowsAffected == 0) goto exit;

                    // ALST、ACAR不和ABAG包成一transaction，因需先存入才能計算
                    // ALST、CLST
                    bool IsAdjust = (param.pds_rec_op_type == Pds_recParam.Rec_op_type.ABAG); // 是否為調劑
                    var lstParam = param;
                    lstParam.pds_rec_op_type = IsAdjust ? Pds_recParam.Rec_op_type.ALST : Pds_recParam.Rec_op_type.CLST;
                    lstParam.pds_rec_bag_code = "";
                    lstParam.pds_rec_reason = "";
                    lstParam.pds_rec_reason_oth = null;
                    lstParam.pds_rec_nondeliver = null;
                    lstParam.pds_rec_note = null;
                    lstParam.pds_rec_md_qty = "";
                    lstParam.pds_rec_md_way1 = "";
                    lstParam.pds_recdList = null;
                    // 2: 依配藥單條碼(傳送日期、住院序號)，查詢需調劑藥袋的狀態
                    // 3: 依配藥單條碼(傳送日期、住院序號)，查詢需核對藥袋的狀態
                    var micbcodeList = DB.Mi_micbcodeRepository.QueryMi_micbcode(
                        new Mi_micbcode
                        {
                            icbcode_send_dt = lstParam.pds_rec_send_dt,
                            icbcode_ipd_no = lstParam.pds_rec_ipd_no
                        }, IsAdjust ? 2 : 3).Data;
                    lstParam.pds_rec_st = micbcodeList.Exists(c => notDoneSt.Contains(c.pds_rec_st)) ? "U" : "Y";
                    micbcodeList = null;
                    Pds_rec existLst = SetPdsRecNo(lstParam, option);
                    sqlCmds = new List<SqlCmdUtil>();
                    if (existLst == null)
                        sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_rec), lstParam));
                    else
                        sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), lstParam));
                    DB.Syb1.Execute(sqlCmds);

                    // ACAR、CCAR
                    SaveCAR(lstParam, option, IsAdjust, notDoneSt);
                    break;
                case 3: // 備註欄位儲存
                    string sql = @"
                    update pds_rec set 
                    pds_rec_note = @pds_rec_note,
                    pds_rec_md_man = @pds_rec_md_man,
                    pds_rec_md_name = @pds_rec_md_name,
                    pds_rec_md_pc = @pds_rec_md_pc,
                    pds_rec_md_ver = @pds_rec_md_ver,
                    pds_rec_md_dt = @pds_rec_md_dt,
                    pds_rec_md_time = @pds_rec_md_time
                    where pds_rec_no = @pds_rec_no";
                    rowsAffected = DB.Syb1.Execute(sql, param);
                    msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    break;
                case 4: // 藥車核對作業，CCAR 作業日期時間-始儲存
                    Pds_rec existCcarS = SetPdsRecNo(param, option);
                    sqlCmds = new List<SqlCmdUtil>();
                    if (existCcarS == null)
                    {
                        sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_rec), param));
                        rowsAffected = DB.Syb1.Execute(sqlCmds);
                        msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    }
                    else
                        succ = true;
                    break;
                case 5: // 藥車核對作業，CLST 儲存
                        // CLST
                    sqlCmds = new List<SqlCmdUtil>();
                    rowsAffected = SaveLST(sqlCmds, param, option);
                    msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    if (rowsAffected == 0) goto exit;

                    // CCAR
                    SaveCAR(param, option, false, notDoneSt);
                    break;
                case 6: // 藥車核對作業，整張取消/整張重新
                    sqlCmds = new List<SqlCmdUtil>();
                    // CBAG
                    var cbagList = QueryPds_rec(new Pds_rec
                    {
                        pds_rec_lst_code = param.pds_rec_lst_code,
                        pds_rec_op_type = Pds_recParam.Rec_op_type.CBAG
                    }, 9).Data; // 9: 依配藥單條碼、作業類型、狀態非刪除
                    cbagList.ForEach(b =>
                    {
                        b.pds_rec_op_dtm_end = param.pds_rec_op_dtm_end;
                        b.pds_rec_st = param.pds_rec_st;
                        b.pds_rec_reason = param.pds_rec_reason;
                        b.pds_rec_reason_oth = param.pds_rec_reason_oth;
                        b.pds_rec_md_man = param.pds_rec_md_man;
                        b.pds_rec_md_name = param.pds_rec_md_name;
                        b.pds_rec_md_pc = param.pds_rec_md_pc;
                        b.pds_rec_md_ver = param.pds_rec_md_ver;
                        b.pds_rec_md_dt = param.pds_rec_md_dt;
                        b.pds_rec_md_time = param.pds_rec_md_time;
                        sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), b));
                    });

                    // CLST
                    rowsAffected = SaveLST(sqlCmds, param, option);
                    msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    if (rowsAffected == 0) goto exit;

                    // CCAR
                    SaveCAR(param, option, false, notDoneSt);
                    break;
                case 7: // 藥車核對作業，CCAR 作業日期時間-結儲存
                    Pds_rec existCcarE = SetPdsRecNo(param, option);
                    sqlCmds = new List<SqlCmdUtil>();
                    if (existCcarE == null)
                        sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_rec), param));
                    else
                        sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), param));
                    rowsAffected = DB.Syb1.Execute(sqlCmds);
                    msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    break;
                case 8: // 轉入4級管制藥調劑資訊 (待移除)
                    return SavePds_rec_4S(param, option);
                case 9: // 首日量調劑/核對/發藥作業，整張取消/整張重新
                    sqlCmds = new List<SqlCmdUtil>();
                    // FABAG、FCBAG、FRBAG
                    string pds_rec_op_type = "";
                    if (param.pds_rec_op_type == Pds_recParam.Rec_op_type.FALST)
                        pds_rec_op_type = Pds_recParam.Rec_op_type.FABAG;
                    else if (param.pds_rec_op_type == Pds_recParam.Rec_op_type.FCLST)
                        pds_rec_op_type = Pds_recParam.Rec_op_type.FCBAG;
                    else if (param.pds_rec_op_type == Pds_recParam.Rec_op_type.FRLST)
                        pds_rec_op_type = Pds_recParam.Rec_op_type.FRBAG;
                    // 2022.05.11 首日量4級管制藥(非磨粉分包)於調劑作業的整張取消/整張重新，狀態不用取消，取消會導致下一階段無法作業
                    var fbagList = QueryPds_rec(new Pds_rec
                    {
                        pds_rec_lst_code = param.pds_rec_lst_code,
                        pds_rec_op_type = pds_rec_op_type
                    }, 14).Data; // 14: 依首日量配藥單條碼、作業類型、狀態非刪除、藥品類型
                    fbagList.ForEach(b =>
                    {
                        b.pds_rec_op_dtm_end = param.pds_rec_op_dtm_end;
                        b.pds_rec_st = param.pds_rec_st;
                        b.pds_rec_reason = param.pds_rec_reason;
                        b.pds_rec_reason_oth = param.pds_rec_reason_oth;
                        b.pds_rec_md_man = param.pds_rec_md_man;
                        b.pds_rec_md_name = param.pds_rec_md_name;
                        b.pds_rec_md_pc = param.pds_rec_md_pc;
                        b.pds_rec_md_ver = param.pds_rec_md_ver;
                        b.pds_rec_md_dt = param.pds_rec_md_dt;
                        b.pds_rec_md_time = param.pds_rec_md_time;
                        sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), b));
                    });

                    // FALST、FCLST、FRLST
                    rowsAffected = SaveLST(sqlCmds, param, option);
                    msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    if (rowsAffected == 0) goto exit;
                    break;
                case 10: // 首日量作業，FALST/FCLST/FRLST 儲存
                    // FALST、FCLST、FRLST
                    sqlCmds = new List<SqlCmdUtil>();
                    rowsAffected = SaveLST(sqlCmds, param, option);
                    msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    if (rowsAffected == 0) goto exit;
                    break;
                case 11: // 首日量調劑/核對/發藥作業，排除備註欄位儲存
                case 12: // 首日量無法交車註記，排除備註欄位儲存
                    // FABAG、FCBAG、FRBAG
                    Pds_rec existFBag = SetPdsRecNo(param, option);
                    sqlCmds = new List<SqlCmdUtil>();
                    if (existFBag == null)
                        sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_rec), param, excludeCol));
                    else
                        sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), param, excludeCol));
                    param?.pds_recdList?.ForEach(d =>
                    {
                        sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_recd), d));
                    });
                    DB.Ch_traRepository.Pds_recUpdateToCh_tra(sqlCmds, param);
                    rowsAffected = DB.Syb1.Execute(sqlCmds);
                    msg = rowsAffected > 0 ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                    if (rowsAffected == 0) goto exit;

                    // 不包成一transaction，因需先存入才能計算
                    // FALST、FCLST、FRLST
                    var flstParam = param;
                    int icfOption = 12;
                    if (param.pds_rec_op_type == Pds_recParam.Rec_op_type.FABAG)
                    {
                        flstParam.pds_rec_op_type = Pds_recParam.Rec_op_type.FALST;
                        icfOption = 12;
                    }
                    else if (param.pds_rec_op_type == Pds_recParam.Rec_op_type.FCBAG)
                    {
                        flstParam.pds_rec_op_type = Pds_recParam.Rec_op_type.FCLST;
                        icfOption = 13;
                    }
                    else if (param.pds_rec_op_type == Pds_recParam.Rec_op_type.FRBAG)
                    {
                        flstParam.pds_rec_op_type = Pds_recParam.Rec_op_type.FRLST;
                        icfOption = 14;
                    }
                    flstParam.pds_rec_bag_code = "";
                    flstParam.pds_rec_reason = "";
                    flstParam.pds_rec_reason_oth = null;
                    flstParam.pds_rec_nondeliver = null;
                    flstParam.pds_rec_note = null;
                    flstParam.pds_rec_md_qty = "";
                    flstParam.pds_rec_md_way1 = "";
                    flstParam.pds_recdList = null;
                    // 12: 依首日量配藥單條碼(列印日期、領藥號)，查詢需調劑藥袋的狀態
                    // 13: 依首日量配藥單條碼(列印日期、領藥號)，查詢需核對藥袋的狀態
                    // 14: 依首日量配藥單條碼(列印日期、領藥號)，查詢需發藥藥袋的狀態
                    var micfcodeList = DB.Mi_micbcodeRepository.QueryMi_micbcode(
                        new Mi_micbcode
                        {
                            icfcode_prt_dt = flstParam.pds_rec_lst_code.SubStr(0, 7).ToInt(),
                            icfcode_id = flstParam.pds_rec_lst_code.SubStr(7, 1),
                            icfcode_pill_no = flstParam.pds_rec_lst_code.SubStr(8, 4).ToNullableShort(),
                        }, icfOption).Data;
                    flstParam.pds_rec_st = micfcodeList.Exists(c => notDoneSt.Contains(c.pds_rec_st)) ? "U" : "Y";
                    micfcodeList = null;
                    Pds_rec existFLst = SetPdsRecNo(flstParam, option);
                    sqlCmds = new List<SqlCmdUtil>();
                    if (existFLst == null)
                        sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_rec), flstParam));
                    else
                        sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), flstParam));
                    DB.Syb1.Execute(sqlCmds);
                    break;
            }

        exit:
            return new ApiResult<Pds_rec>(rowsAffected, null, msg, succ: succ);
        }

        /// <summary>
        /// Save CLST/FALST/FCLST/FRLST
        /// </summary>
        private int SaveLST(List<SqlCmdUtil> sqlCmds, Pds_rec lstParam, int option)
        {
            Pds_rec existLst = SetPdsRecNo(lstParam, option);
            if (existLst == null)
                sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_rec), lstParam));
            else
                sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), lstParam));
            lstParam?.pds_recdList?.ForEach(d =>
            {
                sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_recd), d));
            });
            int rowsAffected = DB.Syb1.Execute(sqlCmds);
            return rowsAffected;
        }

        /// <summary>
        /// Save ACAR/CCAR
        /// </summary>
        private void SaveCAR(Pds_rec lstParam, int option, bool IsAdjust, HashSet<string> notDoneSt)
        {
            if (lstParam.pds_rec_clinical.IsNullOrWhiteSpace()) return;

            var carParam = new Pds_rec()
            {
                pds_rec_op_type = IsAdjust ? Pds_recParam.Rec_op_type.ACAR : Pds_recParam.Rec_op_type.CCAR,
                pds_rec_op_dtm_begin = lstParam.pds_rec_op_dtm_begin,
                pds_rec_op_dtm_end = lstParam.pds_rec_op_dtm_end,
                pds_rec_send_dt = lstParam.pds_rec_send_dt,
                pds_rec_clinical = lstParam.pds_rec_clinical,
                pds_rec_md_man = lstParam.pds_rec_md_man,
                pds_rec_md_name = lstParam.pds_rec_md_name,
                pds_rec_md_pc = lstParam.pds_rec_md_pc,
                pds_rec_md_ver = lstParam.pds_rec_md_ver,
                pds_rec_md_dt = lstParam.pds_rec_md_dt,
                pds_rec_md_time = lstParam.pds_rec_md_time,
            };

            // 2: 依作業類型、傳送日期、護理站
            var lstList = QueryPds_rec(new Pds_rec
            {
                pds_rec_op_type = IsAdjust ? Pds_recParam.Rec_op_type.ALST : Pds_recParam.Rec_op_type.CLST,
                pds_rec_send_dt = carParam.pds_rec_send_dt,
                pds_rec_clinical = carParam.pds_rec_clinical,
            }, 2).Data.Where(r => r.pds_rec_st != "D").ToList();
            carParam.pds_rec_st = lstList.Exists(a => notDoneSt.Contains(a.pds_rec_st)) ? "U" : "Y"; // 依據已記錄的配藥單，故此狀態不準

            Pds_rec existCar = SetPdsRecNo(carParam, option);
            if (carParam.pds_rec_op_dtm_begin.IsNullOrWhiteSpace()) // 核對時點選護理站為最早的 pds_rec_op_dtm_begin
                carParam.pds_rec_op_dtm_begin = lstList.Min(a => a.pds_rec_op_dtm_begin);
            carParam.pds_rec_op_dtm_end = lstList.Max(a => a.pds_rec_op_dtm_end);
            carParam.StrProcess();

            var sqlCmds = new List<SqlCmdUtil>();
            if (existCar == null)
                sqlCmds.Add(Util.SqlBuild.Insert(typeof(Pds_rec), carParam));
            else
                sqlCmds.Add(Util.SqlBuild.Update(typeof(Pds_rec), carParam));
            DB.Syb1.Execute(sqlCmds);
        }

        /// <summary>
        /// 轉入4級管制藥調劑資訊 (待移除)
        /// </summary>
        private ApiResult<Pds_rec> SavePds_rec_4S(Pds_rec param, int option = 0)
        {
            int rowsAffected = 0;
            bool? succ = null;
            string msg = string.Empty;

            var mi_micbcode = new Mi_micbcode
            {
                icbcode_send_dt = param.pds_rec_lst_code.SubStr(0, 7).ToInt(),
                icbcode_ipd_no = param.pds_rec_lst_code.SubStr(7, 11),
                icbcode_med_type = "'" + Mi_micbcodeParam.Med_type.FOURS + "'"
            };
            // 6: 依配藥單條碼(傳送日期、住院序號)、藥品類型多筆，查詢未轉入且已完成調劑管制藥
            List<Mi_micbcode> icbcodeList = DB.Mi_micbcodeRepository.QueryMi_micbcode(mi_micbcode, 6).Data;

            if (icbcodeList == null || icbcodeList.Count == 0)
            {
                succ = true;
                goto exit;
            }

            var ch_udrec = new Ch_udrec
            {
                chudrec_date = mi_micbcode.icbcode_send_dt,
                chudrec_ipd_no = mi_micbcode.icbcode_ipd_no
            };
            // 1: 依參數自動組建
            ch_udrec = DB.Ch_udrecRepository.QueryCh_udrec(ch_udrec, 1).Data.FirstOrDefault();

            string cdr_pre_dtm = string.Empty;
            string st = Pds_recParam.Rec_st.Y;
            Mg_mnid user = new Mg_mnid();
            ApiResult<Pds_rec> result;
            icbcodeList.ForEach(ic =>
            {
                cdr_pre_dtm = $"{ic.cdr_pre_date.NullableToStr().PadLeft(7, '0')}{ic.cdr_pre_time.NullableToStr().PadLeft(4, '0')}00";
                cdr_pre_dtm = DateTimeUtil.ConvertROC(cdr_pre_dtm, true, "yyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");
                user.UserId = ic.cdr_pre_usr;
                user = DB.Mg_mnidRepository.QueryUser(user, 1).Data.FirstOrDefault();
                Pds_rec pds_rec = new Pds_rec
                {
                    pds_rec_op_type = Pds_recParam.Rec_op_type.ABAG,
                    pds_rec_op_dtm_begin = cdr_pre_dtm,
                    pds_rec_op_dtm_end = cdr_pre_dtm,
                    pds_rec_bag_code = ic.icbcode_code,
                    pds_rec_lst_code = ic.icbcode_send_dt.NullableToStr().PadLeft(7, '0') + ic.icbcode_ipd_no,
                    pds_rec_send_dt = ic.icbcode_send_dt,
                    pds_rec_ipd_no = ic.icbcode_ipd_no,
                    pds_rec_pat_no = ic.icbcode_pat_no,
                    pds_rec_bed = ch_udrec?.chudrec_bed ?? string.Empty,
                    pds_rec_clinical = ch_udrec?.chudrec_bed_unit ?? string.Empty,
                    pds_rec_st = st,
                    pds_rec_md_man = ic.cdr_pre_usr,
                    pds_rec_md_name = user?.UserName ?? string.Empty,
                    pds_rec_md_pc = param.pds_rec_md_pc,
                    pds_rec_md_ver = param.pds_rec_md_ver,
                };
                var pds_recd = new Pds_recd()
                {
                    pds_recd_op_type = Pds_recParam.Recd_op_type.BAG,
                    pds_recd_op_dtm = cdr_pre_dtm,
                    pds_recd_code = ic.icbcode_code,
                    pds_recd_mst_id = ic.icbcode_fee_key,
                    pds_recd_st = st,
                    pds_recd_md_man = pds_rec.pds_rec_md_man,
                    pds_recd_md_name = pds_rec.pds_rec_md_name,
                    pds_recd_md_pc = pds_rec.pds_rec_md_pc,
                    pds_recd_md_ver = pds_rec.pds_rec_md_ver
                };
                pds_rec.pds_recdList.Add(pds_recd);
                // 1: 藥車調劑/核對作業，排除備註欄位儲存
                result = SavePds_rec(pds_rec, 1);
                if (!result.Succ)
                {
                    rowsAffected = result.RowsAffected;
                    msg = result.Msg;
                    succ = false;
                }
            });

            if (!succ.HasValue) succ = true;

            exit:
            return new ApiResult<Pds_rec>(rowsAffected, null, msg, succ: succ);
        }

        /// <summary>
        /// 轉入管制藥資訊
        /// </summary>
        public ApiResult<Pds_rec> SavePds_rec_S(Pds_rec param, int option = 0)
        {
            int rowsAffected = 0;
            bool? succ = null;
            string msg = string.Empty;

            if (param.pds_rec_lst_code.IsNullOrWhiteSpace())
            {
                rowsAffected = 0; msg = MsgParam.PdsRecNoLstCode; goto exit;
            }

            Mi_micbcode mi_micbcode = null;
            List<Mi_micbcode> icbcodeList = null;
            Ch_udrec ch_udrec = null;
            string pds_rec_op_type = string.Empty;
            string pds_rec_bed = string.Empty;
            string pds_rec_clinical = string.Empty;
            switch (option)
            {
                case 1: // 藥車轉入4級管制藥調劑資訊
                    mi_micbcode = new Mi_micbcode
                    {
                        icbcode_send_dt = param.pds_rec_lst_code.SubStr(0, 7).ToInt(),
                        icbcode_ipd_no = param.pds_rec_lst_code.SubStr(7, 11),
                        icbcode_med_type = "'" + Mi_micbcodeParam.Med_type.FOURS + "'"
                    };
                    // 6: 依配藥單條碼(傳送日期、住院序號)、藥品類型多筆，查詢未轉入且已完成調劑管制藥
                    icbcodeList = DB.Mi_micbcodeRepository.QueryMi_micbcode(mi_micbcode, 6).Data;
                    if (icbcodeList == null || icbcodeList.Count == 0)
                    {
                        succ = true;
                        goto exit;
                    }

                    ch_udrec = new Ch_udrec
                    {
                        chudrec_date = mi_micbcode.icbcode_send_dt,
                        chudrec_ipd_no = mi_micbcode.icbcode_ipd_no
                    };
                    // 1: 依參數自動組建
                    ch_udrec = DB.Ch_udrecRepository.QueryCh_udrec(ch_udrec, 1).Data.FirstOrDefault();

                    pds_rec_op_type = Pds_recParam.Rec_op_type.ABAG;
                    pds_rec_bed = ch_udrec?.chudrec_bed ?? string.Empty;
                    pds_rec_clinical = ch_udrec?.chudrec_bed_unit ?? string.Empty;
                    break;
                case 2: // 首日量轉入1-4級管制藥調劑資訊
                    mi_micbcode = new Mi_micbcode
                    {
                        icfcode_prt_dt = param.pds_rec_lst_code.SubStr(0, 7).ToInt(),
                        icfcode_id = param.pds_rec_lst_code.SubStr(7, 1),
                        icfcode_pill_no = param.pds_rec_lst_code.SubStr(8, 4).ToNullableShort(),
                        icbcode_med_type = $"'{ Mi_micbcodeParam.Med_type.FOURS}','{Mi_micbcodeParam.Med_type.S}'"
                    };
                    // 15: 依首日量配藥單條碼(列印日期、領藥號)、藥品類型多筆，查詢未轉入且已完成調劑管制藥
                    icbcodeList = DB.Mi_micbcodeRepository.QueryMi_micbcode(mi_micbcode, 15).Data;
                    if (icbcodeList == null || icbcodeList.Count == 0)
                    {
                        succ = true;
                        goto exit;
                    }

                    pds_rec_op_type = Pds_recParam.Rec_op_type.FABAG;
                    break;
                case 3: // 首日量轉入1-3級管制藥核對資訊
                    mi_micbcode = new Mi_micbcode
                    {
                        icfcode_prt_dt = param.pds_rec_lst_code.SubStr(0, 7).ToInt(),
                        icfcode_id = param.pds_rec_lst_code.SubStr(7, 1),
                        icfcode_pill_no = param.pds_rec_lst_code.SubStr(8, 4).ToNullableShort(),
                        icbcode_med_type = $"'{Mi_micbcodeParam.Med_type.S}'"
                    };
                    // 16: 依首日量配藥單條碼(列印日期、領藥號)、藥品類型多筆，查詢未轉入且已完成核對管制藥
                    icbcodeList = DB.Mi_micbcodeRepository.QueryMi_micbcode(mi_micbcode, 16).Data;
                    if (icbcodeList == null || icbcodeList.Count == 0)
                    {
                        succ = true;
                        goto exit;
                    }

                    pds_rec_op_type = Pds_recParam.Rec_op_type.FCBAG;
                    break;
            }

            string cdr_pre_dtm = string.Empty;
            string st = Pds_recParam.Rec_st.Y;
            Mg_mnid user = new Mg_mnid();
            ApiResult<Pds_rec> result;
            string pds_rec_lst_code = string.Empty;
            int? pds_rec_send_dt = null;
            int pds_rec_option = 0;
            icbcodeList.ForEach(ic =>
            {
                cdr_pre_dtm = $"{ic.cdr_pre_date.NullableToStr().PadLeft(7, '0')}{ic.cdr_pre_time.NullableToStr().PadLeft(4, '0')}00";
                cdr_pre_dtm = DateTimeUtil.ConvertROC(cdr_pre_dtm, true, "yyyMMddHHmmss", "yyyy/MM/dd HH:mm:ss");
                user.UserId = ic.cdr_pre_usr;
                user = DB.Mg_mnidRepository.QueryUser(user, 1).Data.FirstOrDefault();

                switch (option)
                {
                    case 1:
                        pds_rec_lst_code = ic.icbcode_send_dt.NullableToStr().PadLeft(7, '0') + ic.icbcode_ipd_no;
                        pds_rec_send_dt = ic.icbcode_send_dt;
                        break;
                    case 2:
                    case 3:
                        pds_rec_lst_code = ic.icfcode_prt_dt.NullableToStr().PadLeft(7, '0') + ic.icfcode_id.PadLeft(1, ' ') + ic.icfcode_pill_no.NullableToStr().PadLeft(4, '0');
                        pds_rec_send_dt = ic.icfcode_prt_dt;
                        pds_rec_bed = ic.icfcode_bed;
                        pds_rec_clinical = ic.icfcode_clinical;
                        break;
                }

                Pds_rec pds_rec = new Pds_rec
                {
                    pds_rec_op_type = pds_rec_op_type,
                    pds_rec_op_dtm_begin = cdr_pre_dtm,
                    pds_rec_op_dtm_end = cdr_pre_dtm,
                    pds_rec_bag_code = ic.icbcode_code,
                    pds_rec_lst_code = pds_rec_lst_code,
                    pds_rec_send_dt = pds_rec_send_dt,
                    pds_rec_ipd_no = ic.icbcode_ipd_no,
                    pds_rec_pat_no = ic.icbcode_pat_no,
                    pds_rec_bed = pds_rec_bed,
                    pds_rec_clinical = pds_rec_clinical,
                    pds_rec_st = st,
                    pds_rec_md_man = ic.cdr_pre_usr,
                    pds_rec_md_name = user?.UserName ?? string.Empty,
                    pds_rec_md_pc = param.pds_rec_md_pc,
                    pds_rec_md_ver = param.pds_rec_md_ver,
                };
                var pds_recd = new Pds_recd()
                {
                    pds_recd_op_type = Pds_recParam.Recd_op_type.BAG,
                    pds_recd_op_dtm = cdr_pre_dtm,
                    pds_recd_code = ic.icbcode_code,
                    pds_recd_mst_id = ic.icbcode_fee_key,
                    pds_recd_st = st,
                    pds_recd_md_man = pds_rec.pds_rec_md_man,
                    pds_recd_md_name = pds_rec.pds_rec_md_name,
                    pds_recd_md_pc = pds_rec.pds_rec_md_pc,
                    pds_recd_md_ver = pds_rec.pds_rec_md_ver
                };
                pds_rec.pds_recdList.Add(pds_recd);

                switch (option)
                {
                    case 1:
                        pds_rec_option = 1;
                        break;
                    case 2:
                    case 3:
                        pds_rec_option = 11;
                        break;
                }
                // 1: 藥車調劑/核對作業，排除備註欄位儲存
                // 11: 首日量調劑/核對/發藥作業，排除備註欄位儲存
                result = SavePds_rec(pds_rec, pds_rec_option);
                if (!result.Succ)
                {
                    rowsAffected = result.RowsAffected;
                    msg = result.Msg;
                    succ = false;
                }
            });

            if (!succ.HasValue) succ = true;

            exit:
            return new ApiResult<Pds_rec>(rowsAffected, null, msg, succ: succ);
        }

    }
}
