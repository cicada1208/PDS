using Models;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class SchemaController : BaseController
    {
        [HttpPost]
        public ApiResult<string> CreateModel([FromBody] Schema schema, int option = 0) =>
            DB.SchemaRepository.CreateModel(schema, option);

    }
}