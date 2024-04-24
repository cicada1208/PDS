using Models;
using System.Collections.Generic;
using System.Web.Http;

namespace CychApiService.Controllers
{
    public class UsersController : BaseController
    {
        /// <summary>
        /// http://localhost:44385/api/Users/Prnt?msg=test 才可找到 Prnt
        /// </summary>
        [HttpGet]
        public string Prnt(string msg) =>
             $"UserController Get Method Prnt. msg={msg}";

        /// <summary>
        /// http://localhost:44385/api/Users/Prnt2?title=test&msg=qaq 才可找到 Prnt2
        /// http://localhost:44385/api/Users/Prnt2?title=test 找不到 Prnt2
        /// </summary>
        [HttpPost]
        public string Prnt2(string title, string msg) =>
             $"UserController Post Method Prnt2. title={title}, msg={msg}";

        /// <summary>
        /// http://localhost:44385/api/Users/Prnt3?loginId=10964 可找到 Prnt3
        /// http://localhost:44385/api/Users/Prnt3?loginId=10964&userName=test 可找到 Prnt3
        /// http://localhost:44385/api/Users/Prnt3?loginId=10964&userName1=test 可找到 Prnt3
        /// </summary>
        [HttpGet]
        public string Prnt3([FromUri] Users user) =>
             $"UserController Get Method Prnt3. loginId={user.loginId}, userName={user.userName}";

        /// <summary>
        /// HttpGet 可使用 [FromBody]
        /// </summary>
        [HttpGet]
        public string Prnt4([FromBody] Users user) =>
             $"UserController Get Method Prnt3. loginId={user.loginId}, userName={user.userName}";

        /// <summary>
        /// http://localhost:44385/api/Users/QueryUser?option=1
        /// </summary>
        [HttpPost]
        public ApiResult<List<Users>> QueryUser([FromBody] Users param, int option = 0) =>
            DB.UsersRepository.QueryUser(param, option);
        //return new ApiResult<DataTable>(DB.UsersRepository.QueryUser(param, option));

        [HttpPost]
        public ApiResult<Users> UpdateUser([FromBody] Users param, int option = 0) =>
            DB.UsersRepository.UpdateUser(param, option);

        //public DataTable Query([FromBody] User objUsr)
        //{
        //    try
        //    {
        //        UserDal dalUser = new UserDal();
        //        return dalUser.QueryUser(objUsr, 1);
        //    }
        //    catch (System.Exception ex)
        //    {
        //        HttpResponseMessage resp = new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.InternalServerError,
        //            Content = new StringContent(ex.ToString()),
        //            ReasonPhrase = "Web API Error"
        //        };
        //        throw new HttpResponseException(resp);
        //    }
        //}

    }
}