using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Pds_noteRepository : BaseRepository<Pds_note>
    {
        public ApiResult<List<Pds_note>> QueryPds_note(Pds_note param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依PK
                    sql = @"
                    select * 
                    from pds_note
                    where pds_note_no = @pds_note_no";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Pds_note>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Pds_note>(sql, param).ToList();

            return new ApiResult<List<Pds_note>>(queryList);
        }

        public ApiResult<List<PdsNoteMicbcode>> QueryPdsNoteMicbcode(PdsNoteMicbcode param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依非空值篩選，查詢藥車及首日量
                    var list = QueryPdsNoteMicbcode(param, 2).Data;
                    list.AddRange(QueryPdsNoteMicbcode(param, 3).Data);
                    return new ApiResult<List<PdsNoteMicbcode>>(list);
                case 2: // 依非空值篩選，查詢藥車
                    sql = $@"
                    select note.*, icbcode.*,
                    typecode.rec_code_name as pds_note_type_name,
                    stcode.rec_code_name as pds_note_st_name
                    from pds_note as note
                    left join mi_micbcode as icbcode
                    on (icbcode_code=pds_note_bag_code)
                    left join rec_code as typecode
                    on (typecode.rec_code_model='pds_note_type' and typecode.rec_code_group='pds_note_type' 
                    and typecode.rec_code_short=pds_note_type and typecode.rec_code_st='1')
                    left join rec_code as stcode
                    on (stcode.rec_code_model='pds_note_st' and stcode.rec_code_group='pds_note_st' 
                    and stcode.rec_code_short=pds_note_st and stcode.rec_code_st='1')
                    where pds_note_dtm between @pds_note_dtm_begin and @pds_note_dtm_end
                    and pds_note_op='{Pds_noteParam.Op.UD}'";
                    if (!param.pds_note_md_man.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_md_man = @pds_note_md_man";
                    if (!param.pds_note_type.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_type = @pds_note_type";
                    if (!param.pds_note_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_clinical = @pds_note_clinical";
                    if (!param.pds_note_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_pat_no = @pds_note_pat_no";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icbcode_fee_key = @icbcode_fee_key";
                    if (!param.pds_note_st.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_st = @pds_note_st";
                    sql += Environment.NewLine + "order by pds_note_dtm desc";
                    break;
                case 3: // 依非空值篩選，查詢首日量
                    string fst_select = @"
                    icfcode_fee_key as icbcode_fee_key,
                    icfcode_rx_way1 as icbcode_rx_way1, icfcode_rx_way2 as icbcode_rx_way2,
                    icfcode_rx_uqty1 as icbcode_rx_uqty1, icfcode_rx_uqty2 as icbcode_rx_uqty2,
                    icfcode_rx_unit as icbcode_rx_unit, icfcode_rx_qty1 as icbcode_rx_qty1,
                    icfcode_rx_qty2 as icbcode_rx_qty2, icfcode_pha_unit as icbcode_pha_unit,
                    icfcode_pack as icbcode_pack";

                    sql = @"
                    select";
                    sql += fst_select;
                    sql += $@"
                    , note.*,
                    typecode.rec_code_name as pds_note_type_name,
                    stcode.rec_code_name as pds_note_st_name
                    from pds_note as note
                    left join mi_micfcode as icbcode
                    on (icfcode_code=pds_note_bag_code)
                    left join rec_code as typecode
                    on (typecode.rec_code_model='pds_note_type' and typecode.rec_code_group='pds_note_type' 
                    and typecode.rec_code_short=pds_note_type and typecode.rec_code_st='1')
                    left join rec_code as stcode
                    on (stcode.rec_code_model='pds_note_st' and stcode.rec_code_group='pds_note_st' 
                    and stcode.rec_code_short=pds_note_st and stcode.rec_code_st='1')
                    where pds_note_dtm between @pds_note_dtm_begin and @pds_note_dtm_end
                    and pds_note_op='{Pds_noteParam.Op.FST}'";
                    if (!param.pds_note_md_man.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_md_man = @pds_note_md_man";
                    if (!param.pds_note_type.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_type = @pds_note_type";
                    if (!param.pds_note_clinical.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_clinical = @pds_note_clinical";
                    if (!param.pds_note_pat_no.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_pat_no = @pds_note_pat_no";
                    if (!param.icbcode_fee_key.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and icfcode_fee_key = @icbcode_fee_key";
                    if (!param.pds_note_st.IsNullOrWhiteSpace())
                        sql += Environment.NewLine + "and pds_note_st = @pds_note_st";
                    sql += Environment.NewLine + "order by pds_note_dtm desc";
                    break;
            }

            var queryList = DB.Syb1.Query<PdsNoteMicbcode>(sql, param).ToList();

            switch (option)
            {
                case 2:
                case 3:
                    var mh_mpat = new Mh_mpat();
                    queryList.ForEach(n =>
                    {
                        mh_mpat.pat_no = n.pds_note_pat_no.ToNullableInt();
                        if (mh_mpat.pat_no != null)
                        {
                            // 1: 依參數自動組建
                            n.pat_name = DB.Mh_mpatRepository.QueryMh_mpat(mh_mpat, 1).Data.FirstOrDefault()?.pat_name;
                        }
                        n.icbcode_rx_uqty = Util.Medical.DoseFormat(n.icbcode_rx_uqty1, n.icbcode_rx_uqty2);
                        n.icbcode_rx_qty = Util.Medical.DoseFormat(n.icbcode_rx_qty1, n.icbcode_rx_qty2, false);
                        if (n.icbcode_pack.IsNumeric()) n.icbcode_pack = double.Parse(n.icbcode_pack).ToString();
                    });
                    break;
            }

            return new ApiResult<List<PdsNoteMicbcode>>(queryList);
        }

        public ApiResult<Pds_note> SavePds_note(Pds_note param, int option = 0)
        {
            ApiResult<Pds_note> result;

            if (param.pds_note_no.IsNullOrWhiteSpace())
                result = InsertPds_note(param, 1);    // 1: 依參數自動組建
            else
                result = UpdatePds_note(param, 1); // 1: 依參數自動組建

            return result;
        }

        public ApiResult<Pds_note> InsertPds_note(Pds_note param, int option = 0)
        {
            int rowsAffected = 0;
            DateTime now = DateTime.Now;

            param.StrProcess();

            // PK
            if (param.pds_note_no.IsNullOrWhiteSpace())
            {
                RECSerialNo sno = new RECSerialNo();
                sno.SYSID = SNoParam.PDSNOTE;
                sno.SDATE = now.ToString("yyyyMMdd");
                param.pds_note_no = DB.RECSerialNoRepository.CreateRECSerialNo(sno, sno.SDATE, 8).Data;
            }
            // 通報日期時間
            if (param.pds_note_dtm.IsNullOrWhiteSpace())
                param.pds_note_dtm = now.ToString("yyyy/MM/dd HH:mm:ss");
            // 修改日期時間
            param.pds_note_md_dt = now.ToString("yyyy/MM/dd");
            param.pds_note_md_time = now.ToString("HH:mm:ss");

            switch (option)
            {
                case 1:   // 依參數自動組建
                    rowsAffected = DB.Syb1.Insert<Pds_note>(param);
                    break;
            }

            return new ApiResult<Pds_note>(rowsAffected, param, msgType: ApiParam.ApiMsgType.INSERT);
        }

        public ApiResult<Pds_note> UpdatePds_note(Pds_note param, int option = 0)
        {
            int rowsAffected = 0;

            param.StrProcess();

            // 修改日期時間
            param.pds_note_md_dt = DateTime.Now.ToString("yyyy/MM/dd");
            param.pds_note_md_time = DateTime.Now.ToString("HH:mm:ss");

            switch (option)
            {
                case 1:   // 依參數自動組建
                    rowsAffected = DB.Syb1.Update<Pds_note>(param);
                    break;
            }

            return new ApiResult<Pds_note>(rowsAffected, param, msgType: ApiParam.ApiMsgType.UPDATE);
        }

    }
}
