using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class Pds_noteController : BaseController
    {
        [HttpPost]
        public ApiResult<List<Pds_note>> QueryPds_note([FromBody] Pds_note param, int option = 0) =>
            DB.Pds_noteRepository.QueryPds_note(param, option);

        [HttpPost]
        public ApiResult<List<PdsNoteMicbcode>> QueryPdsNoteMicbcode([FromBody] PdsNoteMicbcode param, int option = 0) =>
            DB.Pds_noteRepository.QueryPdsNoteMicbcode(param, option);

        [HttpPost]
        public ApiResult<Pds_note> SavePds_note([FromBody] Pds_note param, int option = 0) =>
            DB.Pds_noteRepository.SavePds_note(param, option);

    }
}