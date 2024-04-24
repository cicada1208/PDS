using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Mg_mnidController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Mg_mnid>> QueryUser([FromBody] Mg_mnid user, int option = 0) =>
            DB.Mg_mnidRepository.QueryUser(user, option);

        [HttpPost]
        public ApiResult<List<Mg_mnid>> QueryMg_mnid([FromBody] Mg_mnid param, int option = 0) =>
            DB.Mg_mnidRepository.QueryMg_mnid(param, option);

    }
}