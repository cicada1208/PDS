namespace Params
{
    public class MsgParam
    {
        public const string ApiSuccess = "處理成功。";
        public const string ApiFailure = "處理失敗！";

        public const string SaveSuccess = "存檔成功。";
        public const string SaveFailure = "存檔失敗！";

        public const string InsertSuccess = "新增成功。";
        public const string InsertFailure = "新增失敗！";

        public const string UpdateSuccess = "修改成功。";
        public const string UpdateFailure = "修改失敗！";

        public const string DeleteSuccess = "刪除成功。";
        public const string DeleteFailure = "刪除失敗！";

        public const string TitlePrompt = "提示訊息";
        public const string TitleValidate = "資料驗證";
        public const string TitleExportRpt = "匯出報表";
        public const string TitleExportFile = "匯出文件";

        public const string LoginErrorId = "使用者帳號不存在。";
        public const string LoginErrorPw = "使用者密碼錯誤，請留意大小寫。";
        public const string LoginNoData = "查無使用者資料。";
        public const string LoginDimission = "使用者已離職，無使用權限。";

        public const string ExportNoData = "表格查無資料，無法匯出。";

        public const string DataTypeNum = "需輸入數值。";

        public const string PdsRecNoLstCode = "查無此配藥單條碼。";
        public const string PdsRecNoBagCode = "查無此藥袋條碼。";
        public const string PdsRecNoMedCode = "查無此藥品條碼。";
        public const string PdsRecNoOpType = "查無此作業類型。";
        public const string PdsRecDripOverQty = "點滴計數大於總量，重新計數。";
        public const string PdsRecAdjustNotDone = "未完成調劑，請先對下一床。";
        public const string PdsRecFAdjustNotDone = "未完成調劑，請先對下一單。";
        public const string PdsRecFCheckNotDone = "未完成核對，請先發下一單。";
        public const string PdsRecFLSTSOrder = "1至3級管制藥需由管制藥系統調劑、核對；4級管制藥需由管制藥系統調劑。";
        public const string PdsRecFABAGSOrder = "該藥袋需由管制藥系統調劑。";
        public const string PdsRecFIsOrdered = "是否有手寫處方或口頭醫囑單？";
        public const string PdsRecFCheckOrder = "請核對手寫處方或口頭醫囑單。";
        public const string PdsRecCheckRehrig = "確認配藥單夾冰夾，藥冰冰箱。";
    }
}
