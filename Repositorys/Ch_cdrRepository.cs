using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_cdrRepository : BaseRepository<Ch_cdr>
    {
        public ApiResult<List<Ch_cdr>> QueryCh_cdr(Ch_cdr param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_cdr>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_cdr>(sql, param).ToList();

            return new ApiResult<List<Ch_cdr>>(queryList);
        }

    }
}
