using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Repositorys
{
    public class Mr_lstudRepository : BaseRepository<Mr_lstud>
    {
        public ApiResult<List<Mr_lstud>> QueryMr_lstud(Mr_lstud param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依配藥單條碼(傳送日期、住院序號)，查詢配藥單交互作用
                    sql = @"
                    select *
                    from mr_lstud
                    where lstud_key in (
                        select 'DDI ' + a.icbcode_fee_key + b.icbcode_fee_key
                        from mi_micbcode as a, mi_micbcode as b
                        where a.icbcode_send_dt=@icbcode_send_dt and a.icbcode_ipd_no=@icbcode_ipd_no
                        and b.icbcode_send_dt=@icbcode_send_dt and b.icbcode_ipd_no=@icbcode_ipd_no
                    )";
                    break;
                case 3: // 依首日量配藥單條碼(列印日期、領藥號)，查詢配藥單交互作用
                    sql = @"
                    select *
                    from mr_lstud
                    where lstud_key in (
                        select 'DDI ' + a.icfcode_fee_key + b.icfcode_fee_key
                        from mi_micfcode as a, mi_micfcode as b
                        where a.icfcode_prt_dt=@icfcode_prt_dt and a.icfcode_id=@icfcode_id and a.icfcode_pill_no=@icfcode_pill_no
                        and b.icfcode_prt_dt=@icfcode_prt_dt and b.icfcode_id=@icfcode_id and b.icfcode_pill_no=@icfcode_pill_no
                    )";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Mr_lstud>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Mr_lstud>(sql, param).ToList();

            switch (option)
            {
                case 2:
                case 3:
                    if (queryList.Count > 0)
                    {
                        string[] fee_keys;
                        Mr_lstud lstud;
                        List<Mr_lstud> lstudList = new List<Mr_lstud>();
                        queryList.ForEach(l =>
                        {
                            l.effect = l.lstud_data1 + l.lstud_data2;
                            l.treatment = l.lstud_data3 + l.lstud_data4;
                            fee_keys = Regex.Split(l.lstud_key, @"\s+");
                            if (fee_keys.Count() >= 3)
                            {
                                l.icbcode_fee_key = fee_keys[1];
                                l.icbcode_fee_key2 = fee_keys[2];
                            }

                            // PRS1+PRS2 與 PRS2+PRS1 取其一
                            lstud = queryList.Find(q => q.lstud_key == l.lstud_key ||
                                q.lstud_key == $"DDI {l.icbcode_fee_key2.PadRight(9, ' ')}{l.icbcode_fee_key}");
                            if (!lstudList.Contains(lstud))
                                lstudList.Add(lstud);
                        });
                        queryList = lstudList;

                        List<Mi_micbcode> icbcodeList;
                        if (option == 2)
                        {
                            icbcodeList = DB.Mi_micbcodeRepository.QueryMi_micbcode(new Mi_micbcode
                            {
                                icbcode_send_dt = param.icbcode_send_dt,
                                icbcode_ipd_no = param.icbcode_ipd_no
                            }, 5).Data; // 5: 依配藥單條碼(傳送日期、住院序號)，查詢處方明細及核對藥袋的狀態
                        }
                        else
                        {
                            icbcodeList = DB.Mi_micbcodeRepository.QueryMi_micbcode(new Mi_micbcode
                            {
                                icfcode_prt_dt = param.icfcode_prt_dt,
                                icfcode_id = param.icfcode_id,
                                icfcode_pill_no = param.icfcode_pill_no
                            }, 9).Data; // 9: 依首日量配藥單條碼(列印日期、領藥號)，查詢處方明細及調劑藥袋的狀態
                        }

                        queryList.ForEach(l =>
                        {
                            l.icbcodeList = icbcodeList.Where(ic => ic.icbcode_fee_key == l.icbcode_fee_key ||
                            ic.icbcode_fee_key == l.icbcode_fee_key2).ToList();
                        });

                        icbcodeList = null;
                    }
                    break;
            }

            return new ApiResult<List<Mr_lstud>>(queryList);
        }

    }
}
