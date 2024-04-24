using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Pds_recdController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Pds_recd>> QueryPds_recd([FromBody] Pds_recd param, int option = 0) =>
            DB.Pds_recdRepository.QueryPds_recd(param, option);
    }
}