namespace Params
{
    public class FunctionParam
    {
        public enum FuncType
        {
            /// <summary>
            /// 未定
            /// </summary>
            None = 0,
            /// <summary>
            /// 內部程式Page
            /// </summary>
            InnerPage = 1,
            /// <summary>
            /// 外部程式Exe
            /// </summary>
            Exe = 2,
            /// <summary>
            /// 外部程式dll
            /// </summary>
            Dll = 3,
            /// <summary>
            /// 網址
            /// </summary>
            Site = 4,
            /// <summary>
            /// 內部程式Window
            /// </summary>
            InnerWin = 5,
        }
    }
}
