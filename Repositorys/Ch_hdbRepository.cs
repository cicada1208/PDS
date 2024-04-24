using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_hdbRepository : BaseRepository<Ch_hdb>
    {
        public ApiResult<List<Ch_hdb>> QueryCh_hdb(Ch_hdb param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依病歷號，查詢是否有洗腎資料
                    sql = @"
                    select top 1 *
                    from ch_hdb
                    where chhdb_pat_no = @chhdb_pat_no";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_hdb>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_hdb>(sql, param).ToList();

            return new ApiResult<List<Ch_hdb>>(queryList);
        }

    }
}
