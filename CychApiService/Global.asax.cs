using CychApiService.Attributes;
using Models;
using System.Net.Http.Headers;
using System.Web.Http;

namespace CychApiService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Server response JSON 格式設定
            GlobalConfiguration.Configuration.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // ApiExceptionFilterAttribute 套用至整個應用程式
            GlobalConfiguration.Configuration.Filters.Add(new ApiExceptionFilterAttribute());

            ModelLocator modelLocator = new ModelLocator();
        }
    }
}
