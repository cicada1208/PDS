using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Mg_mnidRepository : BaseRepository<Mg_mnid>
    {
        public ApiResult<List<Mg_mnid>> QueryUser(Mg_mnid param, int option = 0)
        {
            string sql = string.Empty;
            List<Mg_mnid> queryList = null;

            switch (option)
            {
                case 1: // 依員編，查詢人員資料
                    sql = @"
                    select nid_code as UserId, nid_name as UserName, 
                    case when substring(nid_rec,35,2)='Z0' then 'Y'
                    else 'N' end as Dimission
                    from mg_mnid
                    where nid_id='5100' 
                    and nid_code=@UserId";
                    queryList = DB.Syb2.Query<Mg_mnid>(sql, param).ToList();
                    break;
            }

            return new ApiResult<List<Mg_mnid>>(queryList);
        }

        public ApiResult<List<Mg_mnid>> QueryMg_mnid(Mg_mnid param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依nid_code=醫師員編(4碼)，查詢姓名
                    sql = @"
                    select nid_code, nid_name
                    from mg_mnid
                    where nid_id='0503' 
                    and nid_code = @nid_code";
                    break;
                case 3: // 查詢全部護理站
                    sql = @"
                    select nid_code, nid_name
                    from mg_mnid
                    where nid_id='0704' 
                    order by nid_code";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb2.Query<Mg_mnid>(param, schemaOnly: option != 1).ToList() :
            DB.Syb2.Query<Mg_mnid>(sql, param).ToList();

            return new ApiResult<List<Mg_mnid>>(queryList);
        }

    }
}
