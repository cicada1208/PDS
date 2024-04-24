using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_torRepository : BaseRepository<Ch_tor>
    {
        public ApiResult<List<Ch_tor>> QueryCh_tor(Ch_tor param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依病歷號、住院序號，查詢最新身高
                    sql = @"
                    select top 1 * from (
                        select ai_ctlmf_update_da as chtor_cre_dt, 0 as chtor_cre_ti, 
                        ai_ctlmf_body_height as chtor_value_num
                        from ai_ctlmf
                        where ai_ctlmf_pat_no = @chtor_pat_no
                        union
                        select chtor_cre_dt, chtor_cre_ti, chtor_value_num
                        from ch_tor
                        where chtor_pat_no = @chtor_pat_no
                        and chtor_item = 'BH'
                        and chtor_del_mark =''
                        and chtor_from_sys_1 = 'I'
                        and chtor_from_sys_2 = 'NIS'
                        union
                        select 0 as chtor_cre_dt, 0 as chtor_cre_ti,
                        case when isnumeric(chpre_value2)=1 then convert(decimal,chpre_value2)
                        else 0 end as chtor_value_num
                        from ch_pre
                        where chpre_sys_id = '01'
                        and chpre_ipd_no = @chtor_ipd_no
                        and chpre_type = 'Admission'
                        and chpre_item = 'Physical'
                        and chpre_id = '101'
                    ) as ch_tor
                    order by chtor_cre_dt desc, chtor_cre_ti desc";
                    break;
                case 3: // 依病歷號、住院序號，查詢最新體重
                    sql = @"
                    select top 1 * from (
                        select ai_ctlmf_update_da as chtor_cre_dt, 0 as chtor_cre_ti, 
                        ai_ctlmf_body_weight as chtor_value_num
                        from ai_ctlmf
                        where ai_ctlmf_pat_no = @chtor_pat_no
                        union
                        select chtor_cre_dt, chtor_cre_ti, chtor_value_num
                        from ch_tor
                        where chtor_pat_no = @chtor_pat_no
                        and chtor_item = 'BW'
                        and chtor_del_mark =''
                        and chtor_from_sys_1 = 'I'
                        and chtor_from_sys_2 = 'NIS'
                        union
                        select 0 as chtor_cre_dt, 0 as chtor_cre_ti, 
                        case when isnumeric(chpre_value2)=1 then convert(decimal,chpre_value2)
                        else 0 end as chtor_value_num
                        from ch_pre
                        where chpre_sys_id = '01'
                        and chpre_ipd_no = @chtor_ipd_no
                        and chpre_type = 'Admission'
                        and chpre_item = 'Physical'
                        and chpre_id = '102'
                    ) as ch_tor
                    order by chtor_cre_dt desc, chtor_cre_ti desc";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_tor>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_tor>(sql, param).ToList();

            return new ApiResult<List<Ch_tor>>(queryList);
        }

    }
}
