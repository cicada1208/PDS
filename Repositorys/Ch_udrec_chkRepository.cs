using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_udrec_chkRepository : BaseRepository<Ch_udrec_chk>
    {
        public ApiResult<List<Ch_udrec_chk>> QueryCh_udrec_chk(Ch_udrec_chk param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依傳送日期，查詢是否已有資料
                    sql = @"
                    select top 1 * from ch_udrec_chk
                    where chudrecchk_date = @chudrecchk_date";
                    break;
                case 3: // 依傳送日期，查詢護理站
                    sql = @"
                    select distinct chudrecchk_date, chudrecchk_bed_unit 
                    from ch_udrec_chk
                    where chudrecchk_date = @chudrecchk_date
                    and chudrecchk_bed_unit not in ('100')
                    order by chudrecchk_bed_unit ";
                    break;
                case 4: // 依傳送日期、護理站，查詢未完成核對配藥單
                        //sql = @"
                        //select udrec_chk.*, chudrec_bed, pds_rec_st
                        //from ch_udrec_chk as udrec_chk
                        //left join ch_udrec as udrec
                        //on (chudrec_date=chudrecchk_date and chudrec_ipd_no=chudrecchk_ipd_no)
                        //left join pds_rec
                        //on (pds_rec_op_type='CLST' and pds_rec_send_dt=chudrecchk_date 
                        //and pds_rec_ipd_no=chudrecchk_ipd_no)
                        //where chudrecchk_date = @chudrecchk_date
                        //and chudrecchk_bed_unit = @chudrecchk_bed_unit
                        //and isnull(pds_rec_st,'') not in ('Y','S')
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
                        //)";

                    // 2022.02.11 只有 icbcode_med_type = S、V 的配藥單，仍須核對只是到刷藥盒就結束
                    sql = @"
                    select udrec_chk.*, chudrec_bed, pds_rec_st
                    from ch_udrec_chk as udrec_chk
                    left join ch_udrec as udrec
                    on (chudrec_date=chudrecchk_date and chudrec_ipd_no=chudrecchk_ipd_no)
                    left join pds_rec
                    on (pds_rec_op_type='CLST' and pds_rec_send_dt=chudrecchk_date 
                    and pds_rec_ipd_no=chudrecchk_ipd_no)
                    where chudrecchk_date = @chudrecchk_date
                    and chudrecchk_bed_unit = @chudrecchk_bed_unit
                    and isnull(pds_rec_st,'') not in ('Y','S')
                    and exists (
                        select *
                        from mi_micbcode as icbcode
                        where icbcode_send_dt=udrec_chk.chudrecchk_date
                        and icbcode_ipd_no=udrec_chk.chudrecchk_ipd_no
                        and icbcode_st <> 'D'
                    )";
                    break;
                case 5: // 依傳送日期，查詢PSY是否已有資料
                    sql = @"
                    select top 1 * from ch_udrec_chk
                    where chudrecchk_date = @chudrecchk_date
                    and chudrecchk_bed_unit = 'PSY'";
                    break;
                case 6: // 依傳送日期，查詢非PSY是否已有資料
                    sql = @"
                    select top 1 * from ch_udrec_chk
                    where chudrecchk_date = @chudrecchk_date
                    and chudrecchk_bed_unit <> 'PSY'";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_udrec_chk>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_udrec_chk>(sql, param).ToList();

            return new ApiResult<List<Ch_udrec_chk>>(queryList);
        }

        public ApiResult<Ch_udrec_chk> InsertCh_udrec_chk(Ch_udrec_chk param, int option = 0)
        {
            string sql = string.Empty;
            int rowsAffected = 0;
            string msg = string.Empty;
            int today;
            string time = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    rowsAffected = DB.Syb1.Insert<Ch_udrec_chk>(param);
                    break;
                case 2: // 依傳送日期(當下)，新增啟動藥車核對時護理站及床號
                    today = DateTimeUtil.ConvertAD(DateTime.Now.ToString("yyyyMMdd"), outFormat: "yyyMMdd").ToInt();
                    time = DateTime.Now.ToString("yyyyMMddHHmm");
                    //// 6: 依傳送日期，查詢非PSY是否已有資料
                    //if (QueryCh_udrec_chk(new Ch_udrec_chk { chudrecchk_date = today }, 6).Data.Count > 0)
                    //{
                    //    rowsAffected = 0; msg = $"傳送日期：{DateTime.Now.ToString("yyyy/MM/dd")}，" +
                    //        "啟動藥車核對時護理站及床號已建立，無法再次建立。"; goto exit;
                    //}

                    sql = $@"
                    insert into ch_udrec_chk (chudrecchk_date, chudrecchk_pat_no,
                    chudrecchk_bed, chudrecchk_bed_unit, chudrecchk_ipd_no, chudrecchk_filler)
                    select chudrec_date as chudrecchk_date,
                    chudrec_pat_no as chudrecchk_pat_no,
                    case when ipd1.ipd_bed is not null then ipd1.ipd_bed 
                    else ipd2.ipd_bed end as chudrecchk_bed, 
                    case when ipd1.ipd_bed is not null then bed1.bed_unit
                    else bed2.bed_unit end as chudrecchk_bed_unit,
                    chudrec_ipd_no as chudrecchk_ipd_no,
                    '{param.chudrecchk_filler.TrimEnd() + time}'
                    from ch_udrec
                    left join mi_mipd as ipd1
                    on (ipd1.ipd_pat_no = chudrec_pat_no and ipd1.ipd_out_dt = 0 and ipd1.ipd_del_mark = '')
                    left join mi_mipd as ipd2
                    on (ipd2.ipd_no = chudrec_ipd_no)
                    left join mi_mbed as bed1
                    on (bed1.bed_bed = ipd1.ipd_bed) 
                    left join mi_mbed as bed2
                    on (bed2.bed_bed = ipd2.ipd_bed) 
                    where chudrec_date = {today}
                    and chudrec_ipd_no not in (select chudrecchk_ipd_no from ch_udrec_chk where chudrecchk_date = {today})";
                    rowsAffected = DB.Syb1.Execute(sql, param);
                    msg = $"此次建立床位快照：{rowsAffected} 床。";
                    break;
                case 3: // 依傳送日期(當下)，新增PSY啟動藥車核對時護理站及床號
                    today = DateTimeUtil.ConvertAD(DateTime.Now.ToString("yyyyMMdd"), outFormat: "yyyMMdd").ToInt();
                    time = DateTime.Now.ToString("yyyyMMddHHmm");
                    // 5: 依傳送日期，查詢PSY是否已有資料
                    //if (QueryCh_udrec_chk(new Ch_udrec_chk { chudrecchk_date = today }, 5).Data.Count > 0)
                    //{
                    //    rowsAffected = 0; msg = $"傳送日期：{DateTime.Now.ToString("yyyy/MM/dd")}，" +
                    //        "PSY啟動藥車核對時護理站及床號已建立，無法再次建立。"; goto exit;
                    //}

                    sql = $@"
                    insert into ch_udrec_chk (chudrecchk_date, chudrecchk_pat_no,
                    chudrecchk_bed, chudrecchk_bed_unit, chudrecchk_ipd_no, chudrecchk_filler)
                    select chudrec_date as chudrecchk_date,
                    chudrec_pat_no as chudrecchk_pat_no,
                    case when ipd1.ipd_bed is not null then ipd1.ipd_bed 
                    else ipd2.ipd_bed end as chudrecchk_bed, 
                    case when ipd1.ipd_bed is not null then bed1.bed_unit
                    else bed2.bed_unit end as chudrecchk_bed_unit,
                    chudrec_ipd_no as chudrecchk_ipd_no,
                    '{param.chudrecchk_filler.TrimEnd() + time}'
                    from ch_udrec
                    left join mi_mipd as ipd1
                    on (ipd1.ipd_pat_no = chudrec_pat_no and ipd1.ipd_out_dt = 0 and ipd1.ipd_del_mark = '')
                    left join mi_mipd as ipd2
                    on (ipd2.ipd_no = chudrec_ipd_no)
                    left join mi_mbed as bed1
                    on (bed1.bed_bed = ipd1.ipd_bed) 
                    left join mi_mbed as bed2
                    on (bed2.bed_bed = ipd2.ipd_bed) 
                    where chudrec_date = {today}
                    and chudrec_bed_unit = 'PSY'
                    and chudrec_ipd_no not in (select chudrecchk_ipd_no from ch_udrec_chk where chudrecchk_date = {today})";
                    rowsAffected = DB.Syb1.Execute(sql, param);
                    msg = $"此次建立床位快照：{rowsAffected} 床。";
                    break;
            }

            //exit:
            return new ApiResult<Ch_udrec_chk>(rowsAffected, null, msg, ApiParam.ApiMsgType.INSERT);
        }

        public ApiResult<Ch_udrec_chk> UpdateCh_udrec_chk(Ch_udrec_chk param, int option = 0)
        {
            string sql = string.Empty;
            int rowsAffected = 0;

            switch (option)
            {
                case 1:   // 依參數自動組建
                    rowsAffected = DB.Syb1.Update<Ch_udrec_chk>(param);
                    break;
                case 2: // 依傳送日期、住院序號，更新護理站、床號
                    sql = @"
                    update ch_udrec_chk set
                    chudrecchk_bed = @chudrecchk_bed,
                    chudrecchk_bed_unit = @chudrecchk_bed_unit
                    where chudrecchk_date = @chudrecchk_date
                    and chudrecchk_ipd_no = @chudrecchk_ipd_no";
                    rowsAffected = DB.Syb1.Execute(sql, param);
                    break;
            }

            return new ApiResult<Ch_udrec_chk>(rowsAffected, null, msgType: ApiParam.ApiMsgType.UPDATE);
        }

    }
}
