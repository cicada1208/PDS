using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Mi_micbcodeController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Mi_micbcode>> QueryMi_micbcode([FromBody] Mi_micbcode param, int option = 0) =>
            DB.Mi_micbcodeRepository.QueryMi_micbcode(param, option);

        [HttpPost]
        public ApiResult<bool> QueryLstComplete([FromBody] Mi_micbcode param, int option = 0) =>
            DB.Mi_micbcodeRepository.QueryLstComplete(param, option);

    }
}