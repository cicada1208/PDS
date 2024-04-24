using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class SysParameterController : BaseController
    {
        [HttpPost]
        public ApiResult<List<SysParameter>> QuerySysParameter([FromBody] SysParameter param, int option = 0) =>
            DB.SysParameterRepository.QuerySysParameter(param, option);
    }
}