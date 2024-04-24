using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_rel2Repository : BaseRepository<Ch_rel2>
    {
        public ApiResult<List<Ch_rel2>> QueryCh_rel2(Ch_rel2 param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2:  // 依病歷號、報告日期7天內，查詢最新SCr
                    sql = @"
                    select top 1 *, substring(chrel2_filler,6,10) as chrel2_ctm_unit
                    from ch_rel2
                    where chrel2_pat_no = @chrel2_pat_no
                    and chrel2_itm_cd in ('0421B','0421Y')
                    and chrel2_rp_date >= convert(int,convert(varchar, dateadd(dd, -7, getdate()), 112))-19110000
                    order by chrel2_rp_date desc";
                    break;
                case 3:  // 依病歷號、報告日期7天內，查詢最新K
                    sql = @"
                    select top 1 *, substring(chrel2_filler,6,10) as chrel2_ctm_unit
                    from ch_rel2
                    where chrel2_pat_no = @chrel2_pat_no
                    and chrel2_itm_cd in ('0427B','0427Y')
                    and chrel2_rp_date >= convert(int,convert(varchar, dateadd(dd, -7, getdate()), 112))-19110000
                    order by chrel2_rp_date desc";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_rel2>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_rel2>(sql, param).ToList();

            return new ApiResult<List<Ch_rel2>>(queryList);
        }
    }
}
