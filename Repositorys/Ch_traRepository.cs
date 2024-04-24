using Lib;
using Models;
using Params;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_traRepository : BaseRepository<Ch_tra>
    {
        /// <summary>
        /// 回寫藥品傳送系統
        /// </summary>
        public void Pds_recUpdateToCh_tra(List<SqlCmdUtil> sqlCmds, Pds_rec param)
        {
            string sql = string.Empty;
            SqlCmdUtil sqlCmd = new SqlCmdUtil();
            Ch_tra ch_tra;

            if (!(param.pds_rec_st == Pds_recParam.Rec_st.Y ||
                param.pds_rec_st == Pds_recParam.Rec_st.S)) return;

            // 7: 依首日量藥袋條碼，查詢藥品資訊
            Mi_micbcode icf = DB.Mi_micbcodeRepository.QueryMi_micbcode(
                new Mi_micbcode { icbcode_code = param.pds_rec_bag_code }, 7).Data.FirstOrDefault();
            if (icf == null) return;

            int? tra_date = DateTimeUtil.ConvertAD(param.pds_rec_op_dtm_end, false,
                "yyyy/MM/dd HH:mm:ss", "yyyyMMdd").ToNullableInt();
            short? tra_time = DateTimeUtil.ConvertAD(param.pds_rec_op_dtm_end, false,
                "yyyy/MM/dd HH:mm:ss", "HHmm").ToNullableShort();

            switch (param.pds_rec_op_type)
            {
                case Pds_recParam.Rec_op_type.FABAG:
                    sql = $@"
                    update ch_tra set
                    tra_fin_no = @tra_fin_no,
                    tra_fin_name = @tra_fin_name,
                    tra_fin_date = @tra_fin_date,
                    tra_fin_time = @tra_fin_time
                    where tra_ipd_no = @tra_ipd_no
                    and tra_odr_no = @tra_odr_no
                    and tra_fee_no = @tra_fee_no";
                    ch_tra = new Ch_tra
                    {
                        tra_fin_no = param.pds_rec_md_man,
                        tra_fin_name = param.pds_rec_md_name,
                        tra_fin_date = tra_date,
                        tra_fin_time = tra_time,
                        tra_ipd_no = param.pds_rec_ipd_no.ToNullableLong(),
                        tra_odr_no = icf.icfcode_odr_no.ToNullableLong(),
                        tra_fee_no = icf.icfcode_fee_no
                    };
                    break;
                case Pds_recParam.Rec_op_type.FCBAG:
                    sql = @"
                    update ch_tra set
                    tra_chk_no = @tra_chk_no,
                    tra_chk_name = @tra_chk_name,
                    tra_chk_date = @tra_chk_date,
                    tra_chk_time = @tra_chk_time
                    where tra_ipd_no = @tra_ipd_no
                    and tra_odr_no = @tra_odr_no
                    and tra_fee_no = @tra_fee_no";
                    ch_tra = new Ch_tra
                    {
                        tra_chk_no = param.pds_rec_md_man,
                        tra_chk_name = param.pds_rec_md_name,
                        tra_chk_date = tra_date,
                        tra_chk_time = tra_time,
                        tra_ipd_no = param.pds_rec_ipd_no.ToNullableLong(),
                        tra_odr_no = icf.icfcode_odr_no.ToNullableLong(),
                        tra_fee_no = icf.icfcode_fee_no
                    };
                    break;
                case Pds_recParam.Rec_op_type.FRBAG:
                    sql = @"
                    update ch_tra set
                    tra_drug_no = @tra_drug_no,
                    tra_drug_name = @tra_drug_name,
                    tra_drug_date = @tra_drug_date,
                    tra_drug_time = @tra_drug_time
                    where tra_ipd_no = @tra_ipd_no
                    and tra_odr_no = @tra_odr_no
                    and tra_fee_no = @tra_fee_no";
                    ch_tra = new Ch_tra
                    {
                        tra_drug_no = param.pds_rec_md_man,
                        tra_drug_name = param.pds_rec_md_name,
                        tra_drug_date = tra_date,
                        tra_drug_time = tra_time,
                        tra_ipd_no = param.pds_rec_ipd_no.ToNullableLong(),
                        tra_odr_no = icf.icfcode_odr_no.ToNullableLong(),
                        tra_fee_no = icf.icfcode_fee_no
                    };
                    break;
                default:
                    return;
            }

            sqlCmd.Builder.Append(sql);
            sqlCmd.Param = ch_tra;
            sqlCmds.Add(sqlCmd);
        }

    }
}
