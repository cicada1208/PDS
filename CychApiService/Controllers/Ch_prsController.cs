using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Ch_prsController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Ch_prs>> QueryCh_prs([FromBody] Ch_prs param, int option = 0) =>
             DB.Ch_prsRepository.QueryCh_prs(param, option);

    }
}