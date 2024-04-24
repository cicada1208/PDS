using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repositorys
{
    public class Ch_prsRepository : BaseRepository<Ch_prs>
    {
        public ApiResult<List<Ch_prs>> QueryCh_prs(Ch_prs param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依處置類別藥品，查詢處置代碼、處置名稱一
                    sql = @"
                    select chprs_mst_id, chprs_id_name
                    from ch_prs
                    where chprs_fee_knd like 'A%'";
                    break;
                case 3: // 依處置代碼，查詢藥品資訊含說明、圖片、異動、網頁
                    sql = @"
                    select prs.*,
                    substring(convert(varchar(40),chprs_data6),1,40) as chprs_id_name4,
                    case when chprs_way_id='O' then '口服'
                    when chprs_way_id='E' then '外用'
                    when chprs_way_id='I' then '針劑'
                    else chprs_way_id end as chprs_way_name,
                    substring(chprs_data5,36,1) as chprs_orig_rehrig,
                    case when chprs_way_id='O' and substring(chprs_data5,35,1)='N' then 'N'
                    else '' end as chprs_tube_feed,
                    substring(chprs_data5,40,1) as chprs_liver_func,
                    substring(chprs_data5,41,1) as chprs_renal_func,
                    substring(chprs_data5,82,1) as chprs_multi_type,
                    substring(chprs_data5,83,1) as chprs_multi_dose,
                    substring(chprs_data5,65,1) as chprs_spec_pack1,
                    substring(chprs_data5,66,3) as chprs_spec_pack2,
                    substring(chprs_data5,69,4) as chprs_spec_pack3,
                    substring(chprs_data5,73,4) as chprs_spec_pack4,
                    substring(chprs_data5,77,1) as chprs_give_dilu
                    from ch_prs as prs
                    where chprs_mst_id= @chprs_mst_id";
                    break;
                case 4: // 依處置代碼多筆
                    sql = $@"
                    select *, 
                    substring(chprs_data5,36,1) as chprs_orig_rehrig,
                    substring(chprs_data4,48,5) as 'atc_code_prefix5'
                    from ch_prs
                    where chprs_mst_id in ({param.chprs_mst_id})";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb2.Query<Ch_prs>(param, schemaOnly: option != 1).ToList() :
            DB.Syb2.Query<Ch_prs>(sql, param).ToList();

            switch (option)
            {
                case 3:
                    Ch_prsn ch_prsn = new Ch_prsn();
                    List<Ch_prsn> ch_prsnList;
                    string url = string.Empty;
                    string chnid_trn = string.Empty;
                    Mch_mnid mch_mnid = new Mch_mnid();

                    var sysPara = new SysParameter();
                    sysPara.parameterName = "'MAR_WebRoot','MAR_WebInfoPath','drugniImageUrl','drugImageUrl'";
                    // 2: 依參數查詢(多筆)
                    var sysParaList = DB.SysParameterRepository.QuerySysParameter(sysPara, 2).Data;
                    var MAR_WebRoot = sysParaList.Find(p => p.parameterName == "MAR_WebRoot")?.value;
                    var MAR_WebInfoPath = sysParaList.Find(p => p.parameterName == "MAR_WebInfoPath")?.value;
                    var drugniImageUrl = sysParaList.Find(p => p.parameterName == "drugniImageUrl")?.value;
                    var drugImageUrl = sysParaList.Find(p => p.parameterName == "drugImageUrl")?.value;

                    queryList.ForEach(prs =>
                    {
                        prs.chprs_spec_pack2 = prs.chprs_spec_pack2.ToNullableDouble().NullableToStr();
                        if (prs.chprs_spec_pack2 + prs.chprs_spec_pack3 + prs.chprs_spec_pack4 != string.Empty)
                        {
                            prs.chprs_spec_pack = $"{prs.chprs_spec_pack2} {prs.chprs_spec_pack3}/{prs.chprs_spec_pack4}";
                            prs.chprs_spec_pack_speech = $"每{prs.chprs_spec_pack4} {prs.chprs_spec_pack2} {prs.chprs_spec_pack3}";
                        }

                        // 說明：
                        // 05:肝功能調整
                        // 06:腎功能調整
                        // 10:管灌提示
                        // 11:另給稀釋液
                        // 12:多劑型
                        // 13:多劑量
                        ch_prsn.chprsn_sys_id = "'05','06','10','11','12','13'";
                        ch_prsn.chprs_mst_id = prs.chprs_mst_id;
                        // 2: 依系統別多筆、處置代碼
                        ch_prsnList = DB.Ch_prsnRepository.QueryCh_prsn(ch_prsn, 2).Data;
                        prs.chprs_liver_func_note = string.Join(Environment.NewLine, ch_prsnList.Where(prsn => prsn.chprsn_sys_id == "05")
                            .Select(prsn => prsn.chprsn_rec).ToArray());
                        prs.chprs_renal_func_note = string.Join(Environment.NewLine, ch_prsnList.Where(prsn => prsn.chprsn_sys_id == "06")
                            .Select(prsn => prsn.chprsn_rec).ToArray());
                        prs.chprs_tube_feed_note = string.Join(Environment.NewLine, ch_prsnList.Where(prsn => prsn.chprsn_sys_id == "10")
                            .Select(prsn => prsn.chprsn_rec).ToArray());
                        prs.chprs_give_dilu_note = string.Join(Environment.NewLine, ch_prsnList.Where(prsn => prsn.chprsn_sys_id == "11")
                            .Select(prsn => prsn.chprsn_rec).ToArray());
                        prs.chprs_multi_type_note = string.Join(Environment.NewLine, ch_prsnList.Where(prsn => prsn.chprsn_sys_id == "12")
                            .Select(prsn => prsn.chprsn_rec).ToArray());
                        prs.chprs_multi_dose_note = string.Join(Environment.NewLine, ch_prsnList.Where(prsn => prsn.chprsn_sys_id == "13")
                            .Select(prsn => prsn.chprsn_rec).ToArray());

                        try
                        {
                            url = string.Empty;
                            url = ServiceUtil.UdMisPicProxy.WebUdMisPic(prs.chprs_mst_id);
                            if (url.Split('|').Length >= 1)
                                prs.pic_url = MAR_WebRoot + url.Split('|')[0];

                            if (url.Split('|').Length >= 2)
                                prs.drug_news_url = url.Split('|')[1];
                        }
                        catch (Exception)
                        {
                            if (!prs.chprs_mst_id.IsNullOrWhiteSpace()) // prs.pic_url.IsNullOrWhiteSpace() && !prs.chprs_mst_id.IsNullOrWhiteSpace()
                            {
                                // 1: 依參數自動組建
                                mch_mnid.chnid_id = "0037";
                                mch_mnid.chnid_code = prs.chprs_mst_id;
                                chnid_trn = DB.Mch_mnidRepository.QueryMch_mnid(mch_mnid, 1).Data.FirstOrDefault()?.chnid_trn;
                                prs.pic_url = $@"{drugImageUrl}{chnid_trn}.jpg";
                                if (File.Exists(prs.pic_url.ToString()))
                                    prs.pic_url = prs.pic_url.ToString().TryFileToBase64String();
                            }
                        }

                        prs.ni_pic_url = $@"{drugniImageUrl}{prs.chprs_mst_id}.jpg";
                        if (!File.Exists(prs.ni_pic_url)) prs.ni_pic_url = string.Empty;

                        prs.drug_info_url = MAR_WebInfoPath + prs.chprs_mst_id;
                    });
                    break;
            }

            return new ApiResult<List<Ch_prs>>(queryList);
        }
    }
}
