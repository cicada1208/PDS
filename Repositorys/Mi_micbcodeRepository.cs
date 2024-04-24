using ColorHelper;
using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Mi_micbcodeRepository : BaseRepository<Mi_micbcode>
    {
        public ApiResult<List<Mi_micbcode>> QueryMi_micbcode(Mi_micbcode param, int option = 0)
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
                    icfcode_filler as icbcode_filler";

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依配藥單條碼(傳送日期、住院序號)，查詢需調劑藥袋的狀態
                    sql = $@"
                    select icbcode.*, pds_rec_st
                    from mi_micbcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icbcode_code
                    and pds_rec_op_type='ABAG'
                    and pds_rec_st<>'D')
                    where icbcode_send_dt=@icbcode_send_dt
                    and icbcode_ipd_no=@icbcode_ipd_no
                    and icbcode_med_type not in ('4S', 'S', 'V', 'G')
                    and icbcode_st <> 'D'";
                    break;
                case 3: // 依配藥單條碼(傳送日期、住院序號)，查詢需核對藥袋的狀態
                    sql = $@"
                    select icbcode.*, pds_rec_st
                    from mi_micbcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icbcode_code
                    and pds_rec_op_type='CBAG'
                    and pds_rec_st<>'D')
                    where icbcode_send_dt=@icbcode_send_dt
                    and icbcode_ipd_no=@icbcode_ipd_no
                    and icbcode_med_type not in ('S', 'V')
                    and icbcode_st <> 'D'";
                    break;
                case 4: // 依配藥單條碼(傳送日期、住院序號)，查詢一筆病人資訊
                    sql = @"
                    select top 1 * from mi_micbcode
                    where icbcode_send_dt=@icbcode_send_dt
                    and icbcode_ipd_no=@icbcode_ipd_no
                    and icbcode_st <> 'D'";
                    break;
                case 5: // 依配藥單條碼(傳送日期、住院序號)，查詢處方明細及核對藥袋的狀態
                    sql = @"
                    select icbcode.*, substring(icbcode.icbcode_filler,1,3) as icbcode_udl_seq,
                    cbag.pds_rec_st 
                    from mi_micbcode as icbcode
                    left join pds_rec as cbag
                    on (pds_rec_bag_code=icbcode_code 
                    and pds_rec_op_type='CBAG' 
                    and pds_rec_st<>'D')
                    where icbcode_send_dt=@icbcode_send_dt
                    and icbcode_ipd_no=@icbcode_ipd_no
                    and icbcode_st <> 'D'";
                    break;
                case 6: // 依配藥單條碼(傳送日期、住院序號)、藥品類型多筆，查詢未轉入且已完成調劑管制藥
                    sql = $@"
                    select icbcode.*,
                    cdr_pre_date, cdr_pre_time, cdr_pre_usr
                    from mi_micbcode as icbcode
                    left join ch_cdr as cdr
                    on (cast(substring(icbcode_cd_barcode,1,1) as int) = cdr_knd
                    and cast(substring(icbcode_cd_barcode,2,7) as int) = cdr_ymd
                    and cast(substring(icbcode_cd_barcode,9,5) as int) = cdr_seq)
                    left join pds_rec as abag
                    on (abag.pds_rec_op_type='ABAG' and abag.pds_rec_bag_code=icbcode.icbcode_code)
                    where icbcode_send_dt = @icbcode_send_dt
                    and icbcode_ipd_no =@icbcode_ipd_no
                    and icbcode_med_type in ({param.icbcode_med_type})
                    and cdr_pre_yn ='Y'
                    and abag.pds_rec_no is null";
                    break;
                case 7: // 依首日量藥袋條碼，查詢藥品資訊
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += @"
                    from mi_micfcode
                    where icfcode_code=@icbcode_code";
                    break;
                case 8: // 依首日量配藥單條碼(列印日期、領藥號)，查詢一筆病人資訊
                    sql = @"
                    select top 1";
                    sql += fst_select;
                    sql += @"
                    from mi_micfcode
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_st <> 'D'";
                    break;
                case 9: // 依首日量配藥單條碼(列印日期、領藥號)，查詢處方明細及調劑藥袋的狀態
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += @"
                    , substring(icbcode.icfcode_filler,1,3) as icbcode_udl_seq,
                    fabag.pds_rec_st 
                    from mi_micfcode as icbcode
                    left join pds_rec as fabag
                    on (pds_rec_bag_code=icfcode_code 
                    and pds_rec_op_type='FABAG' 
                    and pds_rec_st<>'D')
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_st <> 'D'";
                    break;
                case 10: // 依首日量配藥單條碼(列印日期、領藥號)，查詢處方明細及核對藥袋的狀態
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += @"
                    , substring(icbcode.icfcode_filler,1,3) as icbcode_udl_seq,
                    fcbag.pds_rec_st 
                    from mi_micfcode as icbcode
                    left join pds_rec as fcbag
                    on (pds_rec_bag_code=icfcode_code 
                    and pds_rec_op_type='FCBAG' 
                    and pds_rec_st<>'D')
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_st <> 'D'";
                    break;
                case 11: // 依首日量配藥單條碼(列印日期、領藥號)，查詢處方明細及發藥藥袋的狀態
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += @"
                    , substring(icbcode.icfcode_filler,1,3) as icbcode_udl_seq,
                    frbag.pds_rec_st 
                    from mi_micfcode as icbcode
                    left join pds_rec as frbag
                    on (pds_rec_bag_code=icfcode_code 
                    and pds_rec_op_type='FRBAG' 
                    and pds_rec_st<>'D')
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_st <> 'D'";
                    break;
                case 12: // 依首日量配藥單條碼(列印日期、領藥號)，查詢需調劑藥袋的狀態
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , pds_rec_st
                    from mi_micfcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icfcode_code
                    and pds_rec_op_type='FABAG'
                    and pds_rec_st<>'D')
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_med_type not in ('4S', 'S', 'V')
                    and icfcode_st <> 'D'";
                    break;
                case 13: // 依首日量配藥單條碼(列印日期、領藥號)，查詢需核對藥袋的狀態
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , pds_rec_st
                    from mi_micfcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icfcode_code
                    and pds_rec_op_type='FCBAG'
                    and pds_rec_st<>'D')
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_med_type not in ('S', 'V')
                    and icfcode_st <> 'D'";
                    break;
                case 14: // 依首日量配藥單條碼(列印日期、領藥號)，查詢需發藥藥袋的狀態
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , pds_rec_st
                    from mi_micfcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icfcode_code
                    and pds_rec_op_type='FRBAG'
                    and pds_rec_st<>'D')
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_med_type not in ('V')
                    and icfcode_st <> 'D'";
                    break;
                case 15: // 依首日量配藥單條碼(列印日期、領藥號)、藥品類型多筆，查詢未轉入且已完成調劑管制藥
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , cdr_pre_date, cdr_pre_time, cdr_pre_usr
                    from mi_micfcode as icbcode
                    left join ch_cdr as cdr
                    on (cast(substring(icfcode_cd_barcode,1,1) as int) = cdr_knd
                    and cast(substring(icfcode_cd_barcode,2,7) as int) = cdr_ymd
                    and cast(substring(icfcode_cd_barcode,9,5) as int) = cdr_seq)
                    left join pds_rec as fabag
                    on (fabag.pds_rec_op_type='FABAG' and fabag.pds_rec_bag_code=icbcode.icfcode_code)
                    where icfcode_prt_dt = @icfcode_prt_dt
                    and icfcode_id =@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_med_type in ({param.icbcode_med_type})
                    and cdr_pre_yn ='Y'
                    and fabag.pds_rec_no is null";
                    break;
                case 16: // 依首日量配藥單條碼(列印日期、領藥號)、藥品類型多筆，查詢未轉入且已完成核對管制藥
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , cdr_pre_date, cdr_pre_time, cdr_pre_usr
                    from mi_micfcode as icbcode
                    left join ch_cdr as cdr
                    on (cast(substring(icfcode_cd_barcode,1,1) as int) = cdr_knd
                    and cast(substring(icfcode_cd_barcode,2,7) as int) = cdr_ymd
                    and cast(substring(icfcode_cd_barcode,9,5) as int) = cdr_seq)
                    left join pds_rec as fcbag
                    on (fcbag.pds_rec_op_type='FCBAG' and fcbag.pds_rec_bag_code=icbcode.icfcode_code)
                    where icfcode_prt_dt = @icfcode_prt_dt
                    and icfcode_id =@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_med_type in ({param.icbcode_med_type})
                    and cdr_pre_yn ='Y'
                    and fcbag.pds_rec_no is null";
                    break;
                case 17: // 依首日量配藥單條碼(列印日期、領藥號)，查詢需調劑藥袋的狀態(轉入後)
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , pds_rec_st
                    from mi_micfcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icfcode_code
                    and pds_rec_op_type='FABAG'
                    and pds_rec_st<>'D')
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_med_type not in ('V')
                    and icfcode_st <> 'D'";
                    break;
                case 18: // 依首日量配藥單條碼(列印日期、領藥號)，查詢需核對藥袋的狀態(轉入後)
                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , pds_rec_st
                    from mi_micfcode as icbcode
                    left join pds_rec as rec
                    on (pds_rec_bag_code=icfcode_code
                    and pds_rec_op_type='FCBAG'
                    and pds_rec_st<>'D')
                    where icfcode_prt_dt=@icfcode_prt_dt
                    and icfcode_id=@icfcode_id
                    and icfcode_pill_no=@icfcode_pill_no
                    and icfcode_med_type not in ('V')
                    and icfcode_st <> 'D'";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Mi_micbcode>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Mi_micbcode>(sql, param).ToList();

            switch (option)
            {
                case 4:
                    break;
                default:
                    queryList.ForEach(ic =>
                    {
                        ic.icbcode_rx_uqty = Util.Medical.DoseFormat(ic.icbcode_rx_uqty1, ic.icbcode_rx_uqty2);
                        ic.icbcode_rx_qty = Util.Medical.DoseFormat(ic.icbcode_rx_qty1, ic.icbcode_rx_qty2, false);
                        if (ic.icbcode_pack.IsNumeric()) ic.icbcode_pack = double.Parse(ic.icbcode_pack).ToString();
                    });
                    break;
            }

            switch (option)
            {
                case 5:
                case 9:
                case 10:
                case 11:
                    if (queryList.Count > 0)
                    {
                        var ch_prs = new Ch_prs { chprs_mst_id = string.Join(",", queryList.Select(ic => $"'{ic.icbcode_fee_key}'").Distinct()) };
                        // 4: 依處置代碼多筆
                        var prsList = DB.Ch_prsRepository.QueryCh_prs(ch_prs, 4).Data;
                        Ch_prs prs;
                        queryList.ForEach(ic =>
                        {
                            prs = prsList.Find(p => p.chprs_mst_id == ic.icbcode_fee_key);

                            ic.chprs_id_name = prs?.chprs_id_name ?? string.Empty;
                            ic.chprs_way_id = prs?.chprs_way_id ?? string.Empty;
                            ic.chprs_typ_id = prs?.chprs_typ_id ?? string.Empty;
                            ic.order_info = $"{ic.chprs_id_name}{Environment.NewLine}";
                            ic.order_info += $"{ic.icbcode_rx_uqty}{ic.icbcode_rx_unit}  {ic.icbcode_rx_way1}  {ic.icbcode_rx_way2}";
                            ic.atc_code_prefix5 = prs?.atc_code_prefix5 ?? string.Empty;
                            ic.chprs_orig_rehrig = prs?.chprs_orig_rehrig ?? string.Empty;
                        });
                        prsList = null;

                        // atc code group
                        var atcGroup = queryList.Where(ic => !ic.atc_code_prefix5.IsNullOrWhiteSpace())
                                     .GroupBy(ic => ic.atc_code_prefix5).Where(g => g.Count() > 1)
                                     .Select(g => new Mi_micbcode { atc_code_prefix5 = g.Key, atc_code_prefix5_color = "" }).ToList();

                        string color = string.Empty;
                        atcGroup.ForEach(g =>
                        {
                            do
                            {
                                //color = "#" + Util.Color.GetRandomColor().Name;
                                color = "#" + ColorConverter.RgbToHex(ColorGenerator.GetLightRandomColor<RGB>()).Value;
                            } while (atcGroup.Exists(a => a.atc_code_prefix5_color == color));
                            g.atc_code_prefix5_color = color;
                        });

                        queryList.ForEach(ic =>
                        {
                            var atc = atcGroup.FirstOrDefault(g => g.atc_code_prefix5 == ic.atc_code_prefix5);
                            if (atc == null)
                                ic.atc_code_prefix5 = string.Empty;
                            else
                                ic.atc_code_prefix5_color = atc.atc_code_prefix5_color;
                        });

                        queryList = SortMi_micbcode(queryList);
                    }
                    break;
            }

            return new ApiResult<List<Mi_micbcode>>(queryList);
        }

        /// <summary>
        /// 配藥單排序
        /// </summary>
        private List<Mi_micbcode> SortMi_micbcode(List<Mi_micbcode> queryList)
        {
            int sort = 0;

            // 外用
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "E")
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 口服 - 自包機(#)
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "O" &&
                ic.icbcode_med_type == Mi_micbcodeParam.Med_type.G)
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 口服 - 片裝(印藥袋)
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "O" &&
                ic.chprs_typ_id != "1" &&
                ic.icbcode_med_type != Mi_micbcodeParam.Med_type.G)
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 口服 - 水劑(印貼紙)
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "O" &&
                ic.chprs_typ_id == "1" &&
                ic.icbcode_med_type != Mi_micbcodeParam.Med_type.G)
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 4管口服
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "O" &&
                ic.icbcode_med_type.StartsWith("4S"))
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 4管針劑
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "I" &&
                ic.icbcode_med_type.StartsWith("4S"))
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 針劑 - 點滴(印貼紙)
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "I" &&
                ic.icbcode_med_type == Mi_micbcodeParam.Med_type.D &&
                !ic.icbcode_med_type.StartsWith("4S"))
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 針劑 - 一般(印藥袋)
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "I" &&
                !ic.icbcode_med_type.StartsWith("4S") &&
                ic.icbcode_med_type != Mi_micbcodeParam.Med_type.D)
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 1~3管口服
            foreach (var q in queryList
                .Where(ic => ic.chprs_way_id == "O" &&
                ic.icbcode_med_type == Mi_micbcodeParam.Med_type.S)
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // K - Glu
            foreach (var q in queryList
                .Where(ic => ic.icbcode_fee_key == "KGLUL")
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // KCL
            foreach (var q in queryList
                .Where(ic => ic.icbcode_fee_key == "KCL")
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 公藥(@)
            foreach (var q in queryList
                .Where(ic => ic.icbcode_med_type == Mi_micbcodeParam.Med_type.V &&
                ic.icbcode_fee_key != "KGLUL" &&
                ic.icbcode_fee_key != "KCL")
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;
            // 其他
            foreach (var q in queryList
                .Where(ic => !ic.icbcode_sort.HasValue)
                .OrderBy(ic => ic.icbcode_rx_way1)
                .ThenBy(ic => ic.icbcode_udl_seq)
                .ThenBy(ic => ic.icbcode_fee_key)
                .ThenBy(ic => ic.icbcode_med_type_p)
                .ThenBy(ic => ic.icbcode_rx_uqty.ToDouble()))
                q.icbcode_sort = ++sort;

            return queryList.OrderBy(ic => ic.icbcode_sort).ToList();
        }

        /// <summary>
        /// 查詢配藥單是否完成
        /// </summary>
        public ApiResult<bool> QueryLstComplete(Mi_micbcode param, int option = 0)
        {
            var notDoneSt = new HashSet<string> { "N", "C", "U", "", null };
            var icbList = QueryMi_micbcode(param, option).Data; // option: 2, 3, 12, 13, 14, 17, 18
            bool lstComplete = !icbList.Exists(ic => notDoneSt.Contains(ic.pds_rec_st));
            return new ApiResult<bool>(lstComplete, MsgParam.ApiSuccess);
        }

    }
}
