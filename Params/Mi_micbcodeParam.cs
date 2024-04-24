namespace Params
{
    public class Mi_micbcodeParam
    {
        public class Med_type
        {
            /// <summary>
            /// 4級管制藥(磨粉分包) 
            /// [藥車：要調劑、要核對]
            /// [首日量：要調劑、要核對、要發藥]
            /// </summary>
            public const string FOURSP = "4SP";

            /// <summary>
            /// 4級管制藥(非磨粉分包) 
            /// [藥車：不調劑需資料轉入、要核對]
            /// [首日量：不調劑需資料轉入、要核對、要發藥]
            /// </summary>
            public const string FOURS = "4S";

            /// <summary>
            /// 1-3級管制藥 
            /// [藥車：僅顯示在處方明細，不調劑、不核對]
            /// [首日量：不調劑需資料轉入、不核對需資料轉入、要發藥]
            /// </summary>
            public const string S = "S";

            /// <summary>
            /// 公藥、KGLUL、KCL、不列印藥袋的藥 
            /// [藥車：僅顯示在處方明細，不調劑、不核對]
            /// [首日量：目前無此類型]
            /// </summary>
            public const string V = "V";

            /// <summary>
            /// 磨粉分包
            /// </summary>
            public const string P = "P";

            /// <summary>
            /// 點滴
            /// </summary>
            public const string D = "D";

            /// <summary>
            /// 自包機
            /// [藥車：不調劑、要核對]
            /// [首日量：要調劑、要核對、要發藥]
            /// </summary>
            public const string G = "G";

            /// <summary>
            /// 一般藥品(非上述藥品)
            /// </summary>
            public const string M = "M";
        }

    }
}
