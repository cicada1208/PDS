using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Ch_remakemarController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Ch_remakemar>> QueryCh_remakemar([FromBody] Ch_remakemar param, int option = 0) =>
             DB.Ch_remakemarRepository.QueryCh_remakemar(param, option);

    }
}