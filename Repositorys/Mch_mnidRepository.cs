using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Mch_mnidRepository : BaseRepository<Mch_mnid>
    {
        public ApiResult<List<Mch_mnid>> QueryMch_mnid(Mch_mnid param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Mch_mnid>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Mch_mnid>(sql, param).ToList();

            return new ApiResult<List<Mch_mnid>>(queryList);
        }

    }
}
