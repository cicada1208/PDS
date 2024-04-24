namespace Params
{
    public class Pds_noteParam
    {
        /// <summary>
        /// 作業別
        /// </summary>
        public class Op
        {
            /// <summary>
            /// 藥車
            /// </summary>
            public const string UD = "UD";

            /// <summary>
            /// 首日量
            /// </summary>
            public const string FST = "FST";
        }

        /// <summary>
        /// 狀態
        /// </summary>
        public class St
        {
            /// <summary>
            /// 未完成
            /// </summary>
            public const string U = "U";

            /// <summary>
            /// 完成
            /// </summary>
            public const string Y = "Y";

            /// <summary>
            /// 刪除
            /// </summary>
            public const string D = "D";
        }

        /// <summary>
        /// 編輯模式
        /// </summary>
        public enum EditMode
        {
            /// <summary>
            /// 未定
            /// </summary>
            NONE = 0,
            /// <summary>
            /// 藥車配藥單
            /// </summary>
            UDLST = 1,
            /// <summary>
            /// 首日量配藥單
            /// </summary>
            FSTLST = 2,
        }

    }
}
