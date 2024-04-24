using Models;
using System.Collections.Generic;
using System.Linq;
using Lib;
using System;

namespace Repositorys
{
    public class Mh_mpatRepository : BaseRepository<Mh_mpat>
    {
        public ApiResult<List<Mh_mpat>> QueryMh_mpat(Mh_mpat param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依病歷號多筆，查詢測試病歷號
                    sql = $@"
                    select *
                    from mh_mpat
                    where pat_no in ({param.pat_no_m})
                    and substring(pat_data_4,89,1) = 'Y'";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb2.Query<Mh_mpat>(param, schemaOnly: option != 1).ToList() :
            DB.Syb2.Query<Mh_mpat>(sql, param).ToList();

            switch (option)
            {
                case 2:
                    break;
                default:
                    queryList.ForEach(pat =>
                    {
                        pat.pat_sex =
                        pat.pat_data_1.SubStr(0, 1) == "1" ? "男" :
                        pat.pat_data_1.SubStr(0, 1) == "2" ? "女" : "?";

                        pat.pat_age = DateTimeUtil.GetAge(pat.pat_birth_dt.NullableToStr(),
                            DateTime.Now.ToString("yyyy/MM/dd"), "yyyMMdd");
                    });
                    break;
            }

            return new ApiResult<List<Mh_mpat>>(queryList);
        }
    }
}
