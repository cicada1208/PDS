using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_ipdtRepository : BaseRepository<Ch_ipdt>
    {
        public ApiResult<List<Ch_ipdt>> QueryCh_ipdt(Ch_ipdt param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依住院序號
                    sql = @"
                    select ipdt.*, icd14.icd14_c_name as ipdt_idzs_1_c_name
                    from ch_ipdt as ipdt
                    left join xd_icd14 as icd14
                    on (icd14_code=ipdt_idzs_1)
                    where ipdt_no = @ipdt_no";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_ipdt>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_ipdt>(sql, param).ToList();

            return new ApiResult<List<Ch_ipdt>>(queryList);
        }

    }
}
