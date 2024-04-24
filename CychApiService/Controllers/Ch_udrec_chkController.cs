using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Ch_udrec_chkController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Ch_udrec_chk>> QueryCh_udrec_chk([FromBody] Ch_udrec_chk param, int option = 0) =>
            DB.Ch_udrec_chkRepository.QueryCh_udrec_chk(param, option);

        [HttpPost]
        public ApiResult<Ch_udrec_chk> InsertCh_udrec_chk([FromBody] Ch_udrec_chk param, int option = 0) =>
            DB.Ch_udrec_chkRepository.InsertCh_udrec_chk(param, option);

        [HttpPost]
        public ApiResult<Ch_udrec_chk> UpdateCh_udrec_chk([FromBody] Ch_udrec_chk param, int option = 0) =>
            DB.Ch_udrec_chkRepository.UpdateCh_udrec_chk(param, option);
    }
}