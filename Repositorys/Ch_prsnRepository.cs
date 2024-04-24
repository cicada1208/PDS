using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_prsnRepository : BaseRepository<Ch_prsn>
    {
        public ApiResult<List<Ch_prsn>> QueryCh_prsn(Ch_prsn param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依系統別多筆、處置代碼
                    sql = $@"
                    select *
                    from ch_prsn
                    where chprsn_sys_id in ({param.chprsn_sys_id})
                    and chprsn_maj_id+chprsn_aux_id = @chprs_mst_id
                    order by chprsn_sys_id, chprsn_seq";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_prsn>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_prsn>(sql, param).ToList();

            return new ApiResult<List<Ch_prsn>>(queryList);
        }

    }
}
