using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Repositorys
{
    public class Ch_bcodeRepository : BaseRepository<Ch_bcode>
    {
        public ApiResult<List<Ch_bcode>> QueryCh_bcode(Ch_bcode param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依條碼
                    sql = @"
                    select *
                    from ch_bcode
                    where bcode_code_pat_no = @bcode_code_pat_no
                    and bcode_code_rx_dt = @bcode_code_rx_dt
                    and bcode_code_rx_hh = @bcode_code_rx_hh
                    and bcode_code_page = @bcode_code_page";
                    break;
                case 3: // 依傳送日期、住院序號、病歷號，查詢自包機
                    // 自包機(餐包)：1包內會有多筆藥，故同一條碼的藥裝1包。
                    // bcode_code_pat_no：餐包且是磨粉的藥，自包機包好後，病歷號更新為0，因為該藥走磨粉調劑核對流程。
                    // bcode_code_page：包數，>6顆分包，第1包 bcode_code_page=1，
                    // 第2包 bcode_code_page=2，故條碼會不同；bcode_code_page=0代表總包。
                    // 總包(一藥一包)：bcode_code_pat_no=有值、bcode_code_rx_dt=服藥日、
                    // bcode_code_rx_hh=第幾個藥、bcode_code_page=0。
                    sql = @"
                    select bcode.*,
                    str(bcode_code_pat_no, 8, '0')+str(bcode_code_rx_dt, 7, '0')+
                    str(bcode_code_rx_hh, 2, '0')+convert(varchar, bcode_code_page) as bcode_code
                    from ch_bcode as bcode
                    left join ch_prs as prs
                    on (bcode_fee_key=chprs_mst_id)
                    where bcode_send_dt = @bcode_send_dt 
                    and bcode_ipd_no = @bcode_ipd_no
                    and bcode_code_pat_no = @bcode_code_pat_no
                    and substring(chprs_data7,129,1)='Y' --Y:入自包機(餐包)
                    order by bcode_code_rx_dt, bcode_code_rx_hh, bcode_code_page";
                    break;
                case 4: // 依條碼及住院序號
                    sql = @"
                    select *
                    from ch_bcode
                    where bcode_code_pat_no = @bcode_code_pat_no
                    and bcode_code_rx_dt = @bcode_code_rx_dt
                    and bcode_code_rx_hh = @bcode_code_rx_hh
                    and bcode_code_page = @bcode_code_page
                    and bcode_ipd_no = @bcode_ipd_no";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb2.Query<Ch_bcode>(param, schemaOnly: option != 1).ToList() :
            DB.Syb2.Query<Ch_bcode>(sql, param).ToList();

            switch (option)
            {
                case 3:
                    if (queryList.Count > 0)
                    {
                        string dose = string.Empty, doseDe = string.Empty;
                        var sysPara = new SysParameter();
                        sysPara.parameterName = "'drugniImageUrl'";
                        // 2: 依參數查詢(多筆)
                        var sysParaList = DB.SysParameterRepository.QuerySysParameter(sysPara, 2).Data;
                        var drugniImageUrl = sysParaList.Find(p => p.parameterName == "drugniImageUrl")?.value;

                        queryList.ForEach(b =>
                        {
                            if (b.bcode_rx_qty.Contains("/"))
                            {
                                dose = b.bcode_rx_qty.Split('/')[0];
                                doseDe = b.bcode_rx_qty.Split('/')[1];
                            }
                            else
                            {
                                dose = b.bcode_rx_qty;
                                doseDe = "1";
                            }
                            b.bcode_rx_qty = Util.Medical.DoseFormat(dose, doseDe, false);

                            if (b.bcode_rx_uqty.Contains("/"))
                            {
                                dose = b.bcode_rx_uqty.Split('/')[0];
                                doseDe = b.bcode_rx_uqty.Split('/')[1];
                            }
                            else
                            {
                                dose = b.bcode_rx_uqty;
                                doseDe = "1";
                            }
                            b.bcode_rx_uqty = Util.Medical.DoseFormat(dose, doseDe, true);

                            if (b.bcode_code_page == 0)
                            {
                                b.order_info = $"{b.bcode_id_name2}{Environment.NewLine}";
                                b.order_info += $"{b.bcode_rx_uqty}{b.bcode_rx_unit}  {b.bcode_rx_way1}  {b.bcode_rx_way2}  ";
                                b.order_info += $"總量 {b.bcode_rx_qty}{b.bcode_pha_unit}";
                            }
                            else
                                b.order_info = $"{b.bcode_id_name2}  {b.bcode_rx_qty}{b.bcode_pha_unit}";

                            b.ni_pic_url = $@"{drugniImageUrl}{b.bcode_fee_key}.jpg";
                            if (!File.Exists(b.ni_pic_url)) b.ni_pic_url = string.Empty;
                        });

                        // set day_group
                        foreach (var b in queryList.Where(b => b.bcode_code_page == 0))
                            b.day_group = "總包";
                        //var minDay = queryList.Where(b => b.bcode_code_page != 0).Min(b => b.bcode_code_rx_dt);
                        var minDay = param.bcode_send_dt.ToInt(); //queryList.Min(b => b.bcode_code_rx_dt);
                        var maxDay = queryList.Max(b => b.bcode_code_rx_dt);
                        int day = 1; int current, begin, end;
                        while (minDay <= maxDay)
                        {
                            begin = $"{minDay}14".ToInt();
                            minDay = Util.DateTime.ROCNextDay(minDay.ToString()).ToInt();
                            end = $"{minDay}13".ToInt();
                            var qroup = queryList.Where(b =>
                            {
                                if (b.bcode_code_page == 0)
                                    return false;
                                else
                                {
                                    current = $"{b.bcode_code_rx_dt.ToString().PadLeft(7, '0')}{b.bcode_code_rx_hh.ToString().PadLeft(2, '0')}".ToInt();
                                    return current >= begin && current <= end;
                                }
                            });
                            foreach (var b in qroup)
                                b.day_group = $"第{Environment.NewLine} {day} 天";
                            if (qroup.Count() > 0) day++;
                        }
                    }
                    break;
            }

            return new ApiResult<List<Ch_bcode>>(queryList);
        }

    }
}
