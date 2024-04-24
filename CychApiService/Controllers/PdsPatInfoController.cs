using Models;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class PdsPatInfoController : BaseController
    {
        [HttpPost]
        public ApiResult<PdsPatInfo> QueryPdsPatInfo([FromBody] PdsPatInfo patInfo, int option = 0) =>
            DB.PdsPatInfoRepository.QueryPdsPatInfo(patInfo, option);
    }
}