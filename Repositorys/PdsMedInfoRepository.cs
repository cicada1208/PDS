using Models;
using Params;
using System.IO;
using System.Linq;

namespace Repositorys
{
    public class PdsMedInfoRepository : BaseRepository<PdsMedInfo>
    {
        public ApiResult<PdsMedInfo> QueryPdsMedInfo(PdsMedInfo pdsMedInfo, int option = 0)
        {
            bool succ = true;
            string msg = MsgParam.ApiSuccess;
            var result = new PdsMedInfo();
            var mi_micbcode = new Mi_micbcode();

            if (string.IsNullOrWhiteSpace(pdsMedInfo.mi_micbcode.icbcode_code))
            {
                succ = false; msg = MsgParam.PdsRecNoBagCode; goto exit;
            }

            int icbOption = 1, mdOption = 2;
            switch (option)
            {
                case 1: // 刷藥袋條碼，取得藥品資訊
                    icbOption = 1; // 1: 依參數自動組建
                    mdOption = 2; // 2: 依主檔作業類型、藥袋條碼、歷程檔狀態原因，查詢最新處方修改、核對處方修改
                    break;
                case 2: // 刷首日量藥袋條碼，取得藥品資訊
                    icbOption = 7; // 7: 依首日量藥袋條碼，查詢藥品資訊
                    mdOption = 4;  // 4: 依主檔作業類型、藥袋條碼、歷程檔狀態原因，查詢首日量最新調劑/核對/發藥處方修改
                    break;
            }

            mi_micbcode.icbcode_code = pdsMedInfo.mi_micbcode.icbcode_code;
            mi_micbcode = DB.Mi_micbcodeRepository.QueryMi_micbcode(mi_micbcode, icbOption).Data.FirstOrDefault();

            if (mi_micbcode == null)
            {
                succ = false; msg = MsgParam.PdsRecNoBagCode; goto exit;
            }
            result.mi_micbcode = mi_micbcode;

            var ch_prs = new Ch_prs();
            ch_prs.chprs_mst_id = mi_micbcode.icbcode_fee_key;
            // 3: 依處置代碼，查詢藥品資訊含說明、圖片、異動、網頁
            result.ch_prs = DB.Ch_prsRepository.QueryCh_prs(ch_prs, 3).Data.FirstOrDefault();

            // 處方若為磨粉分包類型，藥品圖片更換
            if (result.mi_micbcode.icbcode_med_type_p)
            {
                var sysPara = new SysParameter();
                sysPara.parameterName = "'drugniImageUrl'";
                // 2: 依參數查詢(多筆)
                var sysParaList = DB.SysParameterRepository.QuerySysParameter(sysPara, 2).Data;
                var drugniImageUrl = sysParaList.Find(p => p.parameterName == "drugniImageUrl")?.value;
                var pic_url_typep = $@"{drugniImageUrl}磨粉分包.jpg";
                if (File.Exists(pic_url_typep))
                    result.ch_prs.pic_url = pic_url_typep;
            }

            var mr_mexp = new Mr_mexp();
            mr_mexp.exp_key = "E" + mi_micbcode.icbcode_fee_key + "%";
            // 2: 依鍵值處置代碼，查詢健保規範
            result.exp_data = DB.Mr_mexpRepository.QueryMr_mexp(mr_mexp, 2).Data.FirstOrDefault()?.exp_data;

            var mdInfo = new Pds_recd();
            mdInfo.pds_rec_bag_code = mi_micbcode.icbcode_code;
            result.mdInfo = DB.Pds_recdRepository.QueryPds_recd(mdInfo, mdOption).Data.FirstOrDefault();

            // 顯示頻次、總量/總包(修改後)
            result.icbcode_rx_way1_final = result.mi_micbcode.icbcode_rx_way1;
            result.icbcode_rx_qty_final = result.mi_micbcode.icbcode_rx_qty;
            result.icbcode_pack_final = result.mi_micbcode.icbcode_pack;
            if (result.mdInfo != null)
            {
                if (result.mdInfo.pds_recd_md_way1 != string.Empty)
                    result.icbcode_rx_way1_final = result.mdInfo.pds_recd_md_way1;
                if (result.mdInfo.pds_recd_md_qty != string.Empty)
                {
                    if (result.mi_micbcode.icbcode_med_type_p)
                        result.icbcode_pack_final = result.mdInfo.pds_recd_md_qty;
                    else
                        result.icbcode_rx_qty_final = result.mdInfo.pds_recd_md_qty;
                }
            }

        //// 測試數據
        //result.mi_micbcode.icbcode_med_type = "P";
        //result.ch_prs.chprs_liver_func = "Y";
        //result.ch_prs.chprs_liver_func_note = "測試 test";
        //result.ch_prs.chprs_renal_func = "Y";
        //result.ch_prs.chprs_orig_rehrig = "Y";
        //result.ch_prs.chprs_tube_feed = "N";
        //result.ch_prs.chprs_multi_type = "Y";
        //result.ch_prs.chprs_multi_type_note = "藥水";
        //result.ch_prs.chprs_multi_dose = "Y";
        //result.ch_prs.chprs_multi_dose_note = "5mg";

        exit:
            return new ApiResult<PdsMedInfo>(succ, result, msg);
        }

    }
}
