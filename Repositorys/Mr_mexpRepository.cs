using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Repositorys
{
    public class Mr_mexpRepository : BaseRepository<Mr_mexp>
    {
        public ApiResult<List<Mr_mexp>> QueryMr_mexp(Mr_mexp param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依鍵值處置代碼，查詢健保規範
                    sql = @"
                    select *
                    from mr_mexp
                    where exp_key like @exp_key";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb2.Query<Mr_mexp>(param, schemaOnly: option != 1).ToList() :
            DB.Syb2.Query<Mr_mexp>(sql, param).ToList();

            switch (option)
            {
                case 2:
                    var mr_mexp = new Mr_mexp();
                    mr_mexp.exp_data = string.Join(Environment.NewLine, queryList.Select(e => e.exp_data_1));
                    var exp_dataArray = Regex.Split(mr_mexp.exp_data, "健保使用規定：");
                    queryList.Clear();
                    if (exp_dataArray.Length >= 2)
                    {
                        mr_mexp.exp_data = "＊健保使用規定：" + exp_dataArray[1];
                        queryList.Add(mr_mexp);
                    }
                    break;
            }

            return new ApiResult<List<Mr_mexp>>(queryList);
        }
    }
}
