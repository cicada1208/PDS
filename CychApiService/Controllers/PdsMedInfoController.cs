using Models;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class PdsMedInfoController : BaseController
    {
        [HttpPost]
        public ApiResult<PdsMedInfo> QueryPdsMedInfo([FromBody] PdsMedInfo medInfo, int option = 0) =>
            DB.PdsMedInfoRepository.QueryPdsMedInfo(medInfo, option);
    }
}