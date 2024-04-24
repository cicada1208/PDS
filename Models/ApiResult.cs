using System.Net;
using Params;

namespace Models
{
    /// <summary>
    /// API 呼叫時，回傳的統一類別
    /// </summary>
    public class ApiResult<TData>
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

        public ApiResult() { }

        /// <summary>
        /// 建立 Query 成功結果
        /// </summary>
        public ApiResult(TData data, string msg = MsgParam.ApiSuccess)
        {
            Succ = true;
            Code = HttpStatusCode.OK;
            Msg = msg;
            Data = data;
        }

        /// <summary>
        /// 建立結果：依succ
        /// </summary>
        public ApiResult(bool succ, TData data = default, string msg = "", int rowsAffected = 0,
            ApiParam.ApiMsgType msgType = ApiParam.ApiMsgType.NONE)
        {
            Succ = succ;
            Code = Succ ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;

            Msg = msg;
            if (Msg == string.Empty)
                switch (msgType)
                {
                    case ApiParam.ApiMsgType.INSERT:
                        Msg = Succ ? MsgParam.InsertSuccess : MsgParam.InsertFailure;
                        break;
                    case ApiParam.ApiMsgType.UPDATE:
                        Msg = Succ ? MsgParam.UpdateSuccess : MsgParam.UpdateFailure;
                        break;
                    case ApiParam.ApiMsgType.DELETE:
                        Msg = Succ ? MsgParam.DeleteSuccess : MsgParam.DeleteFailure;
                        break;
                    case ApiParam.ApiMsgType.SAVE:
                        Msg = Succ ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                        break;
                    default:
                        Msg = Succ ? MsgParam.ApiSuccess : MsgParam.ApiFailure;
                        break;
                }

            Data = data;
            RowsAffected = rowsAffected;
        }

        /// <summary>
        /// 建立結果：依rowsAffected
        /// </summary>
        public ApiResult(int rowsAffected, TData data = default, string msg = "",
            ApiParam.ApiMsgType msgType = ApiParam.ApiMsgType.NONE, bool? succ = null)
        {
            if (!succ.HasValue) Succ = rowsAffected > 0;
            else Succ = succ.Value;

            Code = Succ ? HttpStatusCode.OK : HttpStatusCode.InternalServerError;

            Msg = msg;
            if (Msg == string.Empty)
                switch (msgType)
                {
                    case ApiParam.ApiMsgType.INSERT:
                        Msg = Succ ? MsgParam.InsertSuccess : MsgParam.InsertFailure;
                        break;
                    case ApiParam.ApiMsgType.UPDATE:
                        Msg = Succ ? MsgParam.UpdateSuccess : MsgParam.UpdateFailure;
                        break;
                    case ApiParam.ApiMsgType.DELETE:
                        Msg = Succ ? MsgParam.DeleteSuccess : MsgParam.DeleteFailure;
                        break;
                    case ApiParam.ApiMsgType.SAVE:
                        Msg = Succ ? MsgParam.SaveSuccess : MsgParam.SaveFailure;
                        break;
                    default:
                        Msg = Succ ? MsgParam.ApiSuccess : MsgParam.ApiFailure;
                        break;
                }

            Data = data;
            RowsAffected = rowsAffected;
        }
    }

    public class ApiError : ApiResult<object>
    {
        /// <summary>
        /// 建立失敗結果
        /// </summary>
        public ApiError(HttpStatusCode code = HttpStatusCode.InternalServerError, string msg = MsgParam.ApiFailure)
        {
            Succ = false;
            Code = code;
            Msg = msg;
            Data = null;
        }
    }
}
