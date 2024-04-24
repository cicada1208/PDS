using Params;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Lib
{
    public class ApiUtil
    {
        public static string SetQueryParams(dynamic queryParams = null)
        {
            string query = string.Empty;
            string result = string.Empty;

            if (queryParams != null)
            {
                if (queryParams.GetType().Name == typeof(Dictionary<,>).Name)
                {
                    foreach (KeyValuePair<object, object> param in queryParams)
                        query += $"&{param.Key}={param.Value}";
                    query = query.TrimStart('&');
                }
                else
                { // anonymous type
                    foreach (var prop in queryParams.GetType().GetProperties())
                        query += $"&{prop.Name}={prop.GetValue(queryParams)}";
                    query = query.TrimStart('&');
                }
                result = (query != string.Empty) ? "?" + query : query;
            }

            return result;
        }

        public static TResult HttpClientEx<TResult>(string service, string requestUri="", object body = null,
        dynamic queryParams = null, string method = "POST") where TResult : new()
        {
            // 安裝套件：Microsoft.Net.Http、Microsoft.AspNet.WebApi.Client
            TResult result = default;

            try
            {
                requestUri += SetQueryParams(queryParams);

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(service);
                    // Accept 用於宣告客戶端要求服務端回應的文件型態
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    switch (method.ToUpper())
                    {
                        case "GET":
                            using (HttpResponseMessage response = client.GetAsync(requestUri).Result)
                                result = response.Content.ReadAsAsync<TResult>().Result;
                            break;
                        case "POST":
                            using (HttpResponseMessage response = client.PostAsJsonAsync(requestUri, body).Result)
                                result = response.Content.ReadAsAsync<TResult>().Result;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (typeof(TResult).Name == typeof(ApiResult<>).Name && result == null)
                {
                    result = new TResult();
                    (result as dynamic).Succ = false;
                    (result as dynamic).Msg = ex.ToString();
                }
                else if (typeof(TResult).Name == typeof(ApiResult<>).Name && result != null)
                {
                    (result as dynamic).Msg = ex.ToString() + Environment.NewLine + (result as dynamic).Msg;
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }

        public static async Task<TResult> HttpClientExAsync<TResult>(string service, string requestUri="", object body = null,
        dynamic queryParams = null, string method = "POST") where TResult : new()
        {
            TResult result = default;

            try
            {
                requestUri += SetQueryParams(queryParams);

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(service);
                    client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                    client.Timeout = Timeout.InfiniteTimeSpan;
                    switch (method.ToUpper())
                    {
                        case "GET":
                            using (HttpResponseMessage response = await client.GetAsync(requestUri))
                                result = await response.Content.ReadAsAsync<TResult>();
                            break;
                        case "POST":
                            using (HttpResponseMessage response = await client.PostAsJsonAsync(requestUri, body))
                                result = await response.Content.ReadAsAsync<TResult>();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                if (typeof(TResult).Name == typeof(ApiResult<>).Name && result == null)
                {
                    result = new TResult();
                    (result as dynamic).Succ = false;
                    (result as dynamic).Msg = ex.ToString();
                }
                else if (typeof(TResult).Name == typeof(ApiResult<>).Name && result != null)
                {
                    (result as dynamic).Msg = ex.ToString() + Environment.NewLine + (result as dynamic).Msg;
                }
                else
                {
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// Get string content like html
        /// </summary>
        /// <remarks>https://www.cych.org.tw/pharm/searchdrugdetailmis.aspx?drug_serial3=APT</remarks>
        public static string HttpClientGetString(string service, string requestUri = "", dynamic queryParams = null)
        {
            string result = string.Empty;

            requestUri += SetQueryParams(queryParams);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(service);
                client.Timeout = Timeout.InfiniteTimeSpan;
                //using (HttpResponseMessage response = client.GetAsync(requestUri).Result)
                //    result = response.Content.ReadAsStringAsync().Result;
                // 上段可簡化為下段
                result = client.GetStringAsync(requestUri).Result;
            }

            return result;
        }

    }

    /// <summary>
    /// API 呼叫時，回傳的統一類別
    /// </summary>
    /// <remarks>因專案參考循環相依，重複建立此 Model</remarks>
    internal class ApiResult<TData>
    {
        /// <summary>
        /// 是否執行成功
        /// </summary>
        public bool Succ { get; set; } = false;

        /// <summary>
        /// Http Status Code
        /// </summary>
        public HttpStatusCode Code { get; set; } = HttpStatusCode.NotFound;

        /// <summary>
        /// 訊息
        /// </summary>
        public string Msg { get; set; } = MsgParam.ApiFailure;

        /// <summary>
        /// 資料
        /// </summary>
        public TData Data { get; set; }

        /// <summary>
        /// 處理筆數
        /// </summary>
        public int RowsAffected { get; set; } = 0;
    }

}
