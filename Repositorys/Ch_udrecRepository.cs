using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_udrecRepository : BaseRepository<Ch_udrec>
    {
        public ApiResult<List<Ch_udrec>> QueryCh_udrec(Ch_udrec param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依傳送日期、住院序號，查詢調劑 & 核對護理站床位
                    sql = @"
                    select chudrec_bed, chudrec_bed_unit, chudrecchk_bed, chudrecchk_bed_unit
                    from ch_udrec
                    left join ch_udrec_chk
                    on (chudrecchk_date=chudrec_date and chudrecchk_ipd_no=chudrec_ipd_no)
                    where chudrec_date = @chudrec_date
                    and chudrec_ipd_no = @chudrec_ipd_no";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_udrec>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_udrec>(sql, param).ToList();

            return new ApiResult<List<Ch_udrec>>(queryList);
        }

    }
}

