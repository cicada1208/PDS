using Lib;
using Models;
using Params;

namespace Repositorys
{
    /// <summary>
    /// 查詢DB連線主機是否為正式區
    /// </summary>
    public class DBRepository : BaseRepository<DB>
    {
        public ApiResult<DB> QueryDB(DB db, int option = 0)
        {
            if (db.DBName == DBParam.DBName.SYB2)
                db.IsFormal = DBUtil.GetConnString(db.DBName).Contains("Data Source='s2'");
            else
                db.IsFormal = DBUtil.GetConnString(db.DBName).Contains("Data Source='s1'");
            var result = new ApiResult<DB>(db);
            return result;
        }
    }
}
