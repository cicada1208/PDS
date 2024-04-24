using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Ch_udrecController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Ch_udrec>> QueryCh_udrec([FromBody] Ch_udrec param, int option = 0) =>
            DB.Ch_udrecRepository.QueryCh_udrec(param, option);
    }
}