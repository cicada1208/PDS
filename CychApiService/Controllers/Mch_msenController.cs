using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Mch_msenController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Mch_msen>> QueryMch_msen([FromBody] Mch_msen param, int option = 0) =>
           DB.Mch_msenRepository.QueryMch_msen(param, option);
    }
}