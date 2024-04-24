using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Mn_mnslRepository : BaseRepository<Mn_mnsl>
    {
        public ApiResult<List<Mn_mnsl>> QueryMn_mnsl(Mn_mnsl param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依病歷號，查詢今天是否有管灌資料
                    var today = DateTimeUtil.ConvertAD(DateTime.Now.ToString("yyyyMMdd"), outFormat: "yyyMMdd");
                    sql = $@"
                    select top 1 *
                    from mn_mnsl
                    where nsl_pat_no = @nsl_pat_no
                    and nsl_eat_code = ''
                    and nsl_knd_sel = 2 --管灌
                    and cast(substring(nsl_beg_data,1,7) as int) <= {today}
                    and (
                        cast(substring(nsl_end_data,1,7) as int) = 0 or
                        cast(substring(nsl_end_data,1,7) as int) >= {today}
                    )";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Mn_mnsl>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Mn_mnsl>(sql, param).ToList();

            return new ApiResult<List<Mn_mnsl>>(queryList);
        }

    }
}
