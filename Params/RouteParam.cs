using System.Configuration;

namespace Params
{

    /// <summary>
    /// API Route
    /// </summary>
    public class RouteParam
    {
        /// <summary>
        /// API Service名稱(定義於 App.config appSettings)
        /// </summary>
        public class ServiceName
        {
            public const string CychApi = "CychApiService";
        }

        /// <summary>
        /// API Service
        /// </summary>
        public static string Service(string name = ServiceName.CychApi)
        {
            if (ConfigurationManager.AppSettings[name] != null)
                return ConfigurationManager.AppSettings[name].ToString();
            else
                return ConfigurationManager.AppSettings[ServiceName.CychApi].ToString();
        }

        public class Schema
        {
            public const string CreateModel = "api/Schema/CreateModel";
        }

        public class DB
        {
            public const string QueryDB = "api/DB/QueryDB";
        }

        public class Users
        {
            public const string QueryUser = "api/Users/QueryUser";
            public const string UpdateUser = "api/Users/UpdateUser";
        }

        public class Mg_mnid
        {
            public const string QueryUser = "api/Mg_mnid/QueryUser";
            public const string QueryMg_mnid = "api/Mg_mnid/QueryMg_mnid";
        }

        public class SysParameter
        {
            public const string QuerySysParameter = "api/SysParameter/QuerySysParameter";
        }

        public class Ch_prs_code
        {
            public const string QueryCh_prs_code = "api/Ch_prs_code/QueryCh_prs_code";
            public const string SaveCh_prs_code = "api/Ch_prs_code/SaveCh_prs_code";
        }

        public class Ch_prs
        {
            public const string QueryCh_prs = "api/Ch_prs/QueryCh_prs";
        }

        public class PdsPatInfo
        {
            public const string QueryPdsPatInfo = "api/PdsPatInfo/QueryPdsPatInfo";
        }

        public class PdsMedInfo
        {
            public const string QueryPdsMedInfo = "api/PdsMedInfo/QueryPdsMedInfo";
        }

        public class Rec_code
        {
            public const string QueryRec_code = "api/Rec_code/QueryRec_code";
        }

        public class Pds_rec
        {
            public const string QueryPds_rec = "api/Pds_rec/QueryPds_rec";
            public const string QueryPdsRecMicbcode = "api/Pds_rec/QueryPdsRecMicbcode";
            public const string QueryPdsRecAC = "api/Pds_rec/QueryPdsRecAC";
            public const string QueryFstAvg = "api/Pds_rec/QueryFstAvg";
            public const string SavePds_rec = "api/Pds_rec/SavePds_rec";
            public const string SavePds_rec_S = "api/Pds_rec/SavePds_rec_S";
        }

        public class Pds_recd
        {
            public const string QueryPds_recd = "api/Pds_recd/QueryPds_recd";
        }

        public class Ch_udrec
        {
            public const string QueryCh_udrec = "api/Ch_udrec/QueryCh_udrec";
        }

        public class Ch_udrec_chk
        {
            public const string QueryCh_udrec_chk = "api/Ch_udrec_chk/QueryCh_udrec_chk";
            public const string InsertCh_udrec_chk = "api/Ch_udrec_chk/InsertCh_udrec_chk";
            public const string UpdateCh_udrec_chk = "api/Ch_udrec_chk/UpdateCh_udrec_chk";
        }

        public class Mi_micbcode
        {
            public const string QueryMi_micbcode = "api/Mi_micbcode/QueryMi_micbcode";
            public const string QueryLstComplete = "api/Mi_micbcode/QueryLstComplete";
        }

        public class Ch_bcode
        {
            public const string QueryCh_bcode = "api/Ch_bcode/QueryCh_bcode";
        }

        public class Mch_msen
        {
            public const string QueryMch_msen = "api/Mch_msen/QueryMch_msen";
        }

        public class Mr_lstud
        {
            public const string QueryMr_lstud = "api/Mr_lstud/QueryMr_lstud";
        }

        public class Pds_note
        {
            public const string QueryPds_note = "api/Pds_note/QueryPds_note";
            public const string QueryPdsNoteMicbcode = "api/Pds_note/QueryPdsNoteMicbcode";
            public const string SavePds_note = "api/Pds_note/SavePds_note";
        }

        public class Ch_remakemar
        {
            public const string QueryCh_remakemar = "api/Ch_remakemar/QueryCh_remakemar";
        }

    }
}
