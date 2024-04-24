using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_remakemarRepository : BaseRepository<Ch_remakemar>
    {
        public ApiResult<List<Ch_remakemar>> QueryCh_remakemar(Ch_remakemar param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依住院序號、列印日期、領藥號
                    sql = @"
                    select * 
                    from ch_remakemar
                    where remake_ipd_date=@remake_ipd_date 
                    and remake_ipd_seq=@remake_ipd_seq
                    and remake_ins_date=@remake_ins_date  --icfcode_ prt_dt
                    and remake_odr_takeno=@remake_odr_takeno  --icfcode_pill_no
                    and remake_way='1'  --1:補藥
                    and isnull(remake_status,'') <> 'D'";
                    break;
                case 3: // 依病歷號、列印日期、領藥號
                    sql = @"
                    select * 
                    from ch_remakemar
                    where remake_pat_no=@remake_pat_no
                    and remake_ins_date=@remake_ins_date  --icfcode_ prt_dt
                    and remake_odr_takeno=@remake_odr_takeno  --icfcode_pill_no
                    and remake_way='1'  --1:補藥
                    and isnull(remake_status,'') <> 'D'";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_remakemar>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_remakemar>(sql, param).ToList();

            return new ApiResult<List<Ch_remakemar>>(queryList);
        }

    }
}
