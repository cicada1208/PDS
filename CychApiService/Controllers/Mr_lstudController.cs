using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Mr_lstudController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Mr_lstud>> QueryMr_lstud([FromBody] Mr_lstud param, int option = 0) =>
           DB.Mr_lstudRepository.QueryMr_lstud(param, option);

    }
}