using Repositorys;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class BaseController : ApiController
    {
        private DBContext _DB;
        protected DBContext DB =>
            _DB ?? (_DB = new DBContext());

    }
}