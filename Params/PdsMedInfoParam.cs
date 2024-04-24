namespace Params
{
    public class PdsMedInfoParam
    {
        /// <summary>
        /// 操作模式
        /// </summary>
        public enum OpMode
        {
            /// <summary>
            /// 未定
            /// </summary>
            NONE = 0,
            /// <summary>
            /// 藥車調劑
            /// </summary>
            A = 1,
            /// <summary>
            /// 藥車核對
            /// </summary>
            C = 2,
            /// <summary>
            /// 首日量調劑
            /// </summary>
            FA = 3,
            /// <summary>
            /// 首日量核對
            /// </summary>
            FC = 4,
            /// <summary>
            /// 首日量發藥
            /// </summary>
            FR = 5,
        }

    }
}
