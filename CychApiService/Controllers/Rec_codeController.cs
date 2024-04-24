using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Rec_codeController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Rec_code>> QueryRec_code([FromBody] Rec_code param, int option = 0) =>
            DB.Rec_codeRepository.QueryRec_code(param, option);

    }
}