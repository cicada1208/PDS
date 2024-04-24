using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Mch_msenRepository : BaseRepository<Mch_msen>
    {
        public ApiResult<List<Mch_msen>> QueryMch_msen(Mch_msen param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依病歷號，查詢用藥過敏暨不良反應
                    sql = @"
                    select sen.*,
                    case 
                    when chsen_type = '1' or chsen_type = '3' or chsen_type = '4' then chsen_type+''+chsen_code
                    when chsen_type = '2' then chsen_type+''+nid0038.chnid_name
                    when chsen_type = '6' then substring(convert(varchar(300),chsen_data),251,50)
                    when chsen_type = '7' then chsen_type+''+nid2116.chnid_name
                    when chsen_type = '8' then chsen_type+''+nid2118.chnid_name+'-'+substring(convert(varchar(300),chsen_data),251,50)
                    else '' 
                    end as chsen_info
                    from mch_msen as sen
                    left join mch_mnid as nid0038
                    on (chsen_type = '2' and nid0038.chnid_id = '0038' and nid0038.chnid_code=left(chsen_code,6))
                    left join mch_mnid as nid2116
                    on (chsen_type = '7' and nid2116.chnid_id = '2116' and nid2116.chnid_code=left(chsen_code,6))
                    left join mch_mnid as nid2118
                    on (chsen_type = '8' and nid2118.chnid_id = '2118' and nid2118.chnid_code=left(chsen_code,6))
                    where chsen_type in ('1','2','3','4','6','7','8') 
                    and chsen_pat_no = @chsen_pat_no";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Mch_msen>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Mch_msen>(sql, param).ToList();

            switch (option)
            {
                case 2:
                    if (queryList.Count > 0 && queryList.Exists(m => m.chsen_type == "1" || m.chsen_type == "6"))
                    {
                        var ch_prs = new Ch_prs { chprs_mst_id = string.Join(",", queryList
                            .Where(m=>m.chsen_type == "1" || m.chsen_type == "6")
                            .Select(m => $"'{m.chsen_code}'").Distinct()) };
                        // 4: 依處置代碼多筆
                        var prsList = DB.Ch_prsRepository.QueryCh_prs(ch_prs, 4).Data;
                        queryList.ForEach(m =>
                        {
                            if (m.chsen_type == "1")
                                m.chsen_info = $"{m.chsen_type} {prsList.Find(p => p.chprs_mst_id == m.chsen_code)?.chprs_id_name}";
                            else if(m.chsen_type == "6")
                                m.chsen_info = $"{m.chsen_type} {prsList.Find(p => p.chprs_mst_id == m.chsen_code)?.chprs_id_name} {m.chsen_info}";
                        });
                        prsList = null;
                    }
                    break;
            }

            return new ApiResult<List<Mch_msen>>(queryList);
        }
    }

}
