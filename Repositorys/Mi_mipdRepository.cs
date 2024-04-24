using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Mi_mipdRepository : BaseRepository<Mi_mipd>
    {
        public ApiResult<List<Mi_mipd>> QueryMi_mipd(Mi_mipd param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依住院序號
                    sql = @"
                    select *, substring(ipd_group1, 57, 1) as ipd_out_res
                    from mi_mipd
                    where ipd_no = @ipd_no";
                    break;
                case 3: // 依住院序號，查詢是否出院
                    sql = @"
                    select * 
                    from mi_mipd
                    where ipd_no = @ipd_no
                    and ipd_out_dt != 0 --出院
                    and substring(ipd_group1, 57, 1) not in ('2','7','B','E','K') --2：繼續住院、7：身分變更";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Mi_mipd>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Mi_mipd>(sql, param).ToList();

            switch (option)
            {
                case 1:
                case 2:
                    var mg_mnid = new Mg_mnid();
                    queryList.ForEach(ipd =>
                    {
                        mg_mnid.nid_code = ipd.ipd_mj_dr1;
                        // 2: 依nid_code=醫師員編(4碼)，查詢姓名
                        ipd.ipd_mj_dr1_name = DB.Mg_mnidRepository.QueryMg_mnid(mg_mnid, 2).Data
                                .FirstOrDefault()?.nid_name ?? string.Empty;
                    });
                    break;
            }

            return new ApiResult<List<Mi_mipd>>(queryList);
        }

    }
}
