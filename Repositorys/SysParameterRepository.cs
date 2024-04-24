using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class SysParameterRepository : BaseRepository<SysParameter>
    {
        public ApiResult<List<SysParameter>> QuerySysParameter(SysParameter param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依參數查詢(多筆)
                    sql = $@"
                    select * from ni_SysParameter
                    where parameterName in ({param.parameterName})";
                    break;
            }

            var queryList = sql == string.Empty ?
                DB.NIS.Query<SysParameter>(param, schemaOnly: option != 1).ToList() :
                DB.NIS.Query<SysParameter>(sql, param).ToList();

            return new ApiResult<List<SysParameter>>(queryList);
        }

    }
}
