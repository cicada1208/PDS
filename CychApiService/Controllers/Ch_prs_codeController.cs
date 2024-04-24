using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Ch_prs_codeController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Ch_prs_code>> QueryCh_prs_code([FromBody] Ch_prs_code param, int option = 0) =>
            DB.Ch_prs_codeRepository.QueryCh_prs_code(param, option);

        [HttpPost]
        public ApiResult<Ch_prs_code> SaveCh_prs_code([FromBody] Ch_prs_code param, int option = 0) =>
            DB.Ch_prs_codeRepository.SaveCh_prs_code(param, option);

    }
}