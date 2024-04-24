namespace Params
{
    public class Pds_recParam
    {
        /// <summary>
        /// 作業類型
        /// </summary>
        public class Rec_op_type
        {
            /// <summary>
            /// 藥車調劑-藥袋
            /// </summary>
            public const string ABAG = "ABAG";

            /// <summary>
            /// 藥車調劑-配藥單
            /// </summary>
            public const string ALST = "ALST";

            /// <summary>
            /// 藥車調劑-護理站
            /// </summary>
            public const string ACAR = "ACAR";

            /// <summary>
            /// 藥車核對-藥袋
            /// </summary>
            public const string CBAG = "CBAG";

            /// <summary>
            /// 藥車核對-配藥單
            /// </summary>
            public const string CLST = "CLST";

            /// <summary>
            /// 藥車核對-護理站
            /// </summary>
            public const string CCAR = "CCAR";

            /// <summary>
            /// 首日量調劑-藥袋
            /// </summary>
            public const string FABAG = "FABAG";

            /// <summary>
            /// 首日量調劑-配藥單
            /// </summary>
            public const string FALST = "FALST";

            /// <summary>
            /// 首日量核對-藥袋
            /// </summary>
            public const string FCBAG = "FCBAG";

            /// <summary>
            /// 首日量核對-配藥單
            /// </summary>
            public const string FCLST = "FCLST";

            /// <summary>
            /// 首日量發藥-藥袋
            /// </summary>
            public const string FRBAG = "FRBAG";

            /// <summary>
            /// 首日量發藥-配藥單
            /// </summary>
            public const string FRLST = "FRLST";
        }

        /// <summary>
        /// 作業狀態
        /// </summary>
        public class Rec_st
        {
            /// <summary>
            /// 未完成
            /// </summary>
            public const string U = "U";

            /// <summary>
            /// 無法給藥(也代表完成)
            /// </summary>
            public const string S = "S";

            /// <summary>
            /// 完成
            /// </summary>
            public const string Y = "Y";

            /// <summary>
            /// 錯誤
            /// </summary>
            public const string N = "N";

            /// <summary>
            /// 取消
            /// </summary>
            public const string C = "C";

            /// <summary>
            /// 刪除
            /// </summary>
            public const string D = "D";
        }

        /// <summary>
        /// 狀態原因
        /// </summary>
        public class Rec_reason
        {
            /// <summary>
            /// 藥品錯誤
            /// [藥車調劑：刷藥品錯誤]
            /// [首日量調劑：刷藥品錯誤]
            /// </summary>
            public const string N01 = "N01";

            /// <summary>
            /// 刷到藥袋
            /// [藥車調劑：刷藥品錯誤]
            /// [首日量調劑：刷藥品錯誤]
            /// </summary>
            public const string N02 = "N02";

            /// <summary>
            /// 將過期，請下架
            /// [藥車調劑：磨粉分包錯誤]
            /// [首日量調劑：磨粉分包錯誤]
            /// </summary>
            public const string N03 = "N03";

            /// <summary>
            /// 總包數不符
            /// [藥車調劑：磨粉分包錯誤]
            /// [首日量調劑：磨粉分包錯誤]
            /// </summary>
            public const string N04 = "N04";

            /// <summary>
            /// 藥盒錯誤
            /// [藥車核對：錯誤]
            /// </summary>
            public const string N05 = "N05";

            /// <summary>
            /// 病人不符
            /// [藥車核對：刷藥袋錯誤]
            /// [首日量調劑/核對/發藥：刷藥袋錯誤、刷藥品錯誤(自包機)]
            /// </summary>
            public const string N06 = "N06";

            /// <summary>
            /// 缺藥
            /// [藥車調劑：取消]
            /// [首日量調劑：取消]
            /// [表示未完成，不能核對 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C01 = "C01";

            /// <summary>
            /// 冰藥
            /// [藥車調劑：取消]
            /// [表示未完成，不能核對 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C02 = "C02";

            /// <summary>
            /// 處方待確認
            /// [藥車調劑：取消]
            /// [藥車核對：整張取消、單筆取消(自包機有)]
            /// [首日量調劑/核對/發藥：整張取消、單筆取消]
            /// [表示未完成，不能核對 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C03 = "C03";

            /// <summary>
            /// 處方修改
            /// [藥車調劑：取消；表示未完成，不能核對 pds_rec_st=C, pds_recd_st=C]
            /// [首日量調劑：單筆取消；表示未完成 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C04 = "C04";

            /// <summary>
            /// 處方DC
            /// [藥車調劑：取消]
            /// [藥車核對：單筆取消(自包機有)]
            /// [首日量調劑/核對/發藥：取消]
            /// [表示完成，可核對 pds_rec_st=Y, pds_recd_st=Y]
            /// </summary>
            public const string C05 = "C05";

            /// <summary>
            /// 調劑藥品錯誤
            /// [藥車核對：單筆取消(自包機無)]
            /// [首日量核對/發藥：單筆取消]
            /// [表示未完成 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C06 = "C06";

            /// <summary>
            /// 調劑總量錯誤
            /// [藥車核對：單筆取消(自包機無)]
            /// [首日量核對/發藥：單筆取消]
            /// [表示未完成 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C07 = "C07";

            /// <summary>
            /// 核對處方修改
            /// [藥車核對：單筆取消；
            /// 非自包機，回調劑作業，表示未完成 pds_rec_st=C, pds_recd_st=C；
            /// 自包機，無調劑作業，表示完成 pds_rec_st=Y, pds_recd_st=Y]
            /// [首日量核對：單筆取消；表示未完成 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C08 = "C08";

            /// <summary>
            /// 整張重新
            /// [藥車核對]
            /// [首日量調劑/核對/發藥]
            /// [表示未完成 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C09 = "C09";

            /// <summary>
            /// 處方未送達
            /// [首日量核對/發藥：刷配藥單時提示]
            /// </summary>
            public const string C10 = "C10";

            /// <summary>
            /// 發藥處方修改
            /// [首日量發藥：單筆取消；表示未完成 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C11 = "C11";

            /// <summary>
            /// 補藥單不合理
            /// [首日量核對/發藥：刷配藥單時提示]
            /// </summary>
            public const string C12 = "C12";

            /// <summary>
            /// 其他
            /// [藥車調劑：取消]
            /// [藥車核對：整張取消、單筆取消(自包機有)]
            /// [首日量調劑/核對/發藥：整張取消、單筆取消]
            /// [表示未完成，不能核對 pds_rec_st=C, pds_recd_st=C]
            /// </summary>
            public const string C99 = "C99";
        }

        /// <summary>
        /// 動作類型
        /// </summary>
        public class Recd_op_type
        {
            /// <summary>
            /// 刷藥袋
            /// [藥車調劑]
            /// [藥車核對：種類含藥袋、自包機、點滴藥袋條碼]
            /// [首日量調劑/核對/發藥]
            /// [同一 pds_recd_rec_no 可能會有多筆]
            /// </summary>
            public const string BAG = "BAG";

            /// <summary>
            /// 取消(單筆取消)
            /// [藥車調劑/核對]
            /// [首日量調劑/核對/發藥]
            /// </summary>
            public const string BAGC = "BAGC";

            /// <summary>
            /// 無法給藥
            /// [藥車調劑/核對]
            /// [首日量調劑/核對/發藥]
            /// </summary>
            public const string BAGS = "BAGS";

            /// <summary>
            /// 刷藥品
            /// [藥車調劑：種類含藥品、磨粉分包、點滴藥品條碼]
            /// [首日量調劑]
            /// </summary>
            public const string MED = "MED";

            /// <summary>
            /// 核對藥品 
            /// [藥車核對：記錄點最後按鈕]
            /// [首日量核對]
            /// </summary>
            public const string MEDV = "MEDV";

            /// <summary>
            /// 出院回收
            /// [藥車核對]
            /// </summary>
            public const string MEDC = "MEDC";

            /// <summary>
            /// 發藥確認
            /// [首日量發藥]
            /// </summary>
            public const string MEDA = "MEDA";

            /// <summary>
            /// 刷配藥單
            /// [藥車核對]
            /// [首日量調劑/核對/發藥]
            /// [同一 pds_recd_rec_no 可能會有多筆]
            /// </summary>
            public const string LST = "LST";

            /// <summary>
            /// 整張取消
            /// [藥車核對]
            /// [首日量調劑/核對/發藥]
            /// </summary>
            public const string LSTC = "LSTC";

            /// <summary>
            /// 整張重新
            /// [藥車核對]
            /// [首日量調劑/核對/發藥]
            /// </summary>
            public const string LSTR = "LSTR";

            /// <summary>
            /// 刷藥盒
            /// [藥車核對]
            /// </summary>
            public const string BED = "BED";
        }

        /// <summary>
        /// 取消模式
        /// </summary>
        public enum CancelMode
        {
            /// <summary>
            /// 藥車調劑：取消
            /// </summary>
            ABAGC = 1,
            /// <summary>
            /// 藥車核對：單筆取消(非自包機)
            /// </summary>
            CBAGC = 2,
            /// <summary>
            /// 藥車核對：單筆取消(自包機)
            /// </summary>
            CBAGCG = 3,
            /// <summary>
            /// 藥車核對：整張取消
            /// </summary>
            CLSTC = 4,
            /// <summary>
            /// 首日量調劑：單筆取消
            /// </summary>
            FABAGC = 5,
            /// <summary>
            /// 首日量調劑：整張取消
            /// </summary>
            FALSTC = 6,
            /// <summary>
            /// 首日量核對：單筆取消
            /// </summary>
            FCBAGC = 7,
            /// <summary>
            /// 首日量核對：整張取消
            /// </summary>
            FCLSTC = 8,
            /// <summary>
            /// 首日量發藥：單筆取消
            /// </summary>
            FRBAGC = 9,
            /// <summary>
            /// 首日量發藥：整張取消
            /// </summary>
            FRLSTC = 10,
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
            /// 無法給藥註記
            /// </summary>
            NONDELIVER = 1,
            /// <summary>
            /// 備註
            /// </summary>
            NOTE = 2,
        }

        /// <summary>
        /// 明細歷程模式
        /// </summary>
        public enum DetailMode
        {
            /// <summary>
            /// 依主檔單號
            /// </summary>
            REC_NO = 3,
            /// <summary>
            /// 依配藥單條碼
            /// </summary>
            LST_CODE = 5,
            /// <summary>
            /// 依藥袋條碼
            /// </summary>
            BAG_CODE = 6,
        }

    }
}
