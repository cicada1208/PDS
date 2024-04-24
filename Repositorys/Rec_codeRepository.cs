using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Rec_codeRepository : BaseRepository<Rec_code>
    {
        public ApiResult<List<Rec_code>> QueryRec_code(Rec_code param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Rec_code>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Rec_code>(sql, param).ToList();

            switch (option)
            {
                case 1:
                    queryList = queryList.OrderBy(c=>c.rec_code_model)
                        .ThenBy(c=>c.rec_code_gseq)
                        .ThenBy(c=>c.rec_code_sseq).ToList() as List<Rec_code>;
                    break;
            }

            return new ApiResult<List<Rec_code>>(queryList);
        }

    }
}
