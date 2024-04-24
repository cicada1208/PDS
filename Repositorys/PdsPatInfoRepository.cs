using Lib;
using Models;
using Params;
using System.Linq;

namespace Repositorys
{
    public class PdsPatInfoRepository : BaseRepository<PdsPatInfo>
    {
        public ApiResult<PdsPatInfo> QueryPdsPatInfo(PdsPatInfo pdsPatInfo, int option = 0)
        {
            bool succ = true;
            string msg = MsgParam.ApiSuccess;
            var result = new PdsPatInfo();
            var mi_micbcode = new Mi_micbcode();

            switch (option)
            {
                case 1:
                    if (string.IsNullOrWhiteSpace(pdsPatInfo.icbcode_code))
                    {
                        succ = false; msg = MsgParam.PdsRecNoBagCode; goto exit;
                    }
                    break;
                case 2:
                case 3:
                    if (string.IsNullOrWhiteSpace(pdsPatInfo.lst_code))
                    {
                        succ = false; msg = MsgParam.PdsRecNoLstCode; goto exit;
                    }
                    break;
            }

            switch (option)
            {
                case 1: // 刷藥袋條碼，取得病人資訊
                    result.icbcode_code = pdsPatInfo.icbcode_code;
                    mi_micbcode.icbcode_code = pdsPatInfo.icbcode_code;
                    // 1: 依參數自動組建
                    mi_micbcode = DB.Mi_micbcodeRepository.QueryMi_micbcode(mi_micbcode, 1).Data.FirstOrDefault();
                    break;
                case 2: // 刷配藥單條碼，取得病人資訊
                    result.lst_code = pdsPatInfo.lst_code;
                    mi_micbcode.icbcode_send_dt = pdsPatInfo.lst_code.SubStr(0, 7).ToInt();
                    mi_micbcode.icbcode_ipd_no = pdsPatInfo.lst_code.SubStr(7, 11);
                    // 4: 依配藥單條碼(傳送日期、住院序號)，查詢一筆病人資訊
                    mi_micbcode = DB.Mi_micbcodeRepository.QueryMi_micbcode(mi_micbcode, 4).Data.FirstOrDefault();
                    break;
                case 3: // 刷首日量配藥單條碼，取得病人資訊
                    result.lst_code = pdsPatInfo.lst_code;
                    mi_micbcode.icfcode_prt_dt = pdsPatInfo.lst_code.SubStr(0, 7).ToInt(); ;
                    mi_micbcode.icfcode_id = pdsPatInfo.lst_code.SubStr(7, 1);
                    mi_micbcode.icfcode_pill_no = pdsPatInfo.lst_code.SubStr(8, 4).ToNullableShort();
                    // 8: 依首日量配藥單條碼(列印日期、領藥號)，查詢一筆病人資訊
                    mi_micbcode = DB.Mi_micbcodeRepository.QueryMi_micbcode(mi_micbcode, 8).Data.FirstOrDefault();
                    break;
            }

            if (mi_micbcode == null)
            {
                succ = false;
                msg = (option == 2 || option == 3) ? MsgParam.PdsRecNoLstCode : MsgParam.PdsRecNoBagCode;
                goto exit;
            }
            result.pat_no = mi_micbcode.icbcode_pat_no;
            result.ipd_no = mi_micbcode.icbcode_ipd_no;

            var mi_mipd = new Mi_mipd();
            mi_mipd.ipd_no = mi_micbcode.icbcode_ipd_no;
            // 2: 依住院序號
            mi_mipd = DB.Mi_mipdRepository.QueryMi_mipd(mi_mipd, 2).Data.FirstOrDefault();
            result.ipd_mj_dr1_name = mi_mipd?.ipd_mj_dr1_name;

            switch (option)
            {
                case 1:
                case 2:
                    var ch_udrec = new Ch_udrec();
                    ch_udrec.chudrec_date = mi_micbcode.icbcode_send_dt;
                    ch_udrec.chudrec_ipd_no = mi_micbcode.icbcode_ipd_no;
                    // 2: 依傳送日期、住院序號，查詢調劑 & 核對護理站床位
                    ch_udrec = DB.Ch_udrecRepository.QueryCh_udrec(ch_udrec, 2).Data.FirstOrDefault();
                    result.chudrec_bed = ch_udrec?.chudrec_bed;
                    result.chudrec_bed_unit = ch_udrec?.chudrec_bed_unit;
                    result.chudrecchk_bed = ch_udrec?.chudrecchk_bed;
                    result.chudrecchk_bed_unit = ch_udrec?.chudrecchk_bed_unit;
                    if (option == 2 && mi_mipd != null && mi_mipd.ipd_out_dt != 0
                        && !(mi_mipd.ipd_out_res == "2" || mi_mipd.ipd_out_res == "7" 
                        || mi_mipd.ipd_out_res == "B" || mi_mipd.ipd_out_res == "E" || mi_mipd.ipd_out_res == "K"))
                    {
                        result.chudrecchk_bed = ClinicalParam.MBD;
                        result.chudrecchk_bed_unit = ClinicalParam.MBD;
                    }
                    result.bed = option == 1 ? result.chudrec_bed : result.chudrecchk_bed;
                    result.bed_unit = option == 1 ? result.chudrec_bed_unit : result.chudrecchk_bed_unit;
                    break;
                case 3:
                    result.bed = mi_micbcode.icfcode_bed;
                    result.bed_unit = mi_micbcode.icfcode_clinical;
                    break;
            }

            var mh_mpat = new Mh_mpat();
            mh_mpat.pat_no = int.Parse(mi_micbcode.icbcode_pat_no);
            // 1: 依參數自動組建
            mh_mpat = DB.Mh_mpatRepository.QueryMh_mpat(mh_mpat, 1).Data.FirstOrDefault();
            result.pat_name = mh_mpat?.pat_name;
            result.pat_sex = mh_mpat?.pat_sex;
            result.pat_age = mh_mpat?.pat_age;

            var ch_tor = new Ch_tor();
            ch_tor.chtor_pat_no = int.Parse(mi_micbcode.icbcode_pat_no);
            ch_tor.chtor_ipd_no = long.Parse(mi_micbcode.icbcode_ipd_no);
            // 2: 依病歷號、住院序號，查詢最新身高
            result.pat_height = DB.Ch_torRepository.QueryCh_tor(ch_tor, 2).Data.FirstOrDefault()?.chtor_value_num.ToNullableDouble().NullableToStr();
            // 3: 依病歷號、住院序號，查詢最新體重
            result.pat_weight = DB.Ch_torRepository.QueryCh_tor(ch_tor, 3).Data.FirstOrDefault()?.chtor_value_num.ToNullableDouble().NullableToStr();

            var ch_ipdt = new Ch_ipdt();
            ch_ipdt.ipdt_no = mi_micbcode.icbcode_ipd_no;
            // 2: 依住院序號
            ch_ipdt = DB.Ch_ipdtRepository.QueryCh_ipdt(ch_ipdt, 2).Data.FirstOrDefault();
            result.ipdt_idzs_1_c_name = ch_ipdt?.ipdt_idzs_1_c_name;

            var ch_rel2 = new Ch_rel2();
            Ch_rel2 ch_rel2_result;
            ch_rel2.chrel2_pat_no = int.Parse(mi_micbcode.icbcode_pat_no);
            // 2: 依病歷號、報告日期7天內，查詢最新SCr
            ch_rel2_result = DB.Ch_rel2Repository.QueryCh_rel2(ch_rel2, 2).Data.FirstOrDefault();
            if (ch_rel2_result != null)
                result.SCr = $"{ch_rel2_result.chrel2_ctm_value.NullableToStr()}  {ch_rel2_result.chrel2_ctm_unit}  " +
                    $"{DateTimeUtil.ConvertROC(ch_rel2_result.chrel2_rp_date.NullableToStr())}";
            // 3: 依病歷號、報告日期7天內，查詢最新K
            ch_rel2_result = DB.Ch_rel2Repository.QueryCh_rel2(ch_rel2, 3).Data.FirstOrDefault();
            if (ch_rel2_result != null)
                result.K = $"{ch_rel2_result.chrel2_ctm_value.NullableToStr()}  {ch_rel2_result.chrel2_ctm_unit}  " +
                    $"{DateTimeUtil.ConvertROC(ch_rel2_result.chrel2_rp_date.NullableToStr())}";

            var mn_mnsl = new Mn_mnsl();
            mn_mnsl.nsl_pat_no = int.Parse(mi_micbcode.icbcode_pat_no);
            // 2: 依病歷號，查詢今天是否有管灌資料
            result.mnsl = DB.Mn_mnslRepository.QueryMn_mnsl(mn_mnsl, 2).Data.Count > 0 ? "Y" : "";

            var ch_hdb = new Ch_hdb();
            ch_hdb.chhdb_pat_no = int.Parse(mi_micbcode.icbcode_pat_no);
            //  2: 依病歷號，查詢是否有洗腎資料
            result.hdb = DB.Ch_hdbRepository.QueryCh_hdb(ch_hdb, 2).Data.Count > 0 ? "Y" : "";

        exit:
            return new ApiResult<PdsPatInfo>(succ, result, msg);
        }

    }
}
