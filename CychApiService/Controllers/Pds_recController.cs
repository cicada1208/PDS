using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Pds_recController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Pds_rec>> QueryPds_rec([FromBody] Pds_rec param, int option = 0) =>
            DB.Pds_recRepository.QueryPds_rec(param, option);

        [HttpPost]
        public ApiResult<List<PdsRecMicbcode>> QueryPdsRecMicbcode([FromBody] PdsRecMicbcode param, int option = 0) =>
            DB.Pds_recRepository.QueryPdsRecMicbcode(param, option);

        [HttpPost]
        public ApiResult<List<PdsRecAC>> QueryPdsRecAC([FromBody] PdsRecAC param, int option = 0) =>
            DB.Pds_recRepository.QueryPdsRecAC(param, option);

        [HttpPost]
        public ApiResult<List<FstAvg>> QueryFstAvg([FromBody] FstAvg param, int option = 0) =>
            DB.Pds_recRepository.QueryFstAvg(param, option);

        [HttpPost]
        public ApiResult<Pds_rec> SavePds_rec([FromBody] Pds_rec param, int option = 0) =>
            DB.Pds_recRepository.SavePds_rec(param, option);

        [HttpPost]
        public ApiResult<Pds_rec> SavePds_rec_S([FromBody] Pds_rec param, int option = 0) =>
            DB.Pds_recRepository.SavePds_rec_S(param, option);

    }
}