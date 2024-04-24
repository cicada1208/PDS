using Models;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class DBController : BaseController
    {
        [HttpPost]
        public ApiResult<DB> QueryDB([FromBody] DB db, int option = 0) =>
            DB.DBRepository.QueryDB(db, option);
    }
}