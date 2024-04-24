using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Ch_bcodeController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Ch_bcode>> QueryCh_bcode([FromBody] Ch_bcode param, int option = 0) =>
            DB.Ch_bcodeRepository.QueryCh_bcode(param, option);
    }
}