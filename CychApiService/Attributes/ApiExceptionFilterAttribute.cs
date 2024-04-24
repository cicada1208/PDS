using Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace CychApiService.Attributes
{
    /// <summary>
    /// 集中處理 Api Exception Error Response
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception == null)
                return;

            // Client Error Response:
            // ExceptionFilter 可攔截所有類型 Exception，除了 HttpResponseException。
            // 可撰寫多個 ExceptionFilter class 篩選特定 Exception，使每種篩選器僅處理特定 Exception。
            // 例如: if (context.Exception is XyzException) ...
            var requestIP = $"{HttpContext.Current.Request.UserHostAddress}"; // HttpContext.Current.Request.UserHostName
            var uri = context.Request.RequestUri.ToString();
            var controllerName = context.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var actionName = context.ActionContext.ActionDescriptor.ActionName;
            var actionArguments = context.ActionContext.ActionArguments;
            var msg = Regex.Replace(context.Exception.ToString(), "\r\n", "@@");
            var error = new
            {
                RequestIP = requestIP,
                Uri = uri,
                ControllerName = controllerName,
                ActionName = actionName,
                ActionArguments = actionArguments,
                Msg = msg
            };
            var errorJson = JsonConvert.SerializeObject(error, Formatting.Indented);
            errorJson = Regex.Replace(errorJson, "@@", System.Environment.NewLine);

            //var resp = context.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, msg);
            var resp = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
            new ApiError(HttpStatusCode.InternalServerError));
            context.Response = resp;


            // Server Error Log:
            // NLog:
            //NLog.Logger logger = NLog.LogManager.GetLogger($"{controllerName}.{actionName}");
            logger.Error(errorJson);
            // Elmah:
            //Elmah.ErrorSignal.FromCurrentContext().Raise(context.Exception);

            // 手動 throw HttpResponseException，後續其他 ExceptionFilter 便不會執行
            //throw new HttpResponseException(context.Response);
        }
    }
}
