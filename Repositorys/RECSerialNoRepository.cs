using Lib;
using Models;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class RECSerialNoRepository : BaseRepository<RECSerialNo>
    {
        /// <summary>
        /// 建立流水號
        /// </summary>
        /// <param name="param"></param>
        /// <param name="numHead">流水號前綴</param>
        /// <param name="numLen">流水號前綴外的號碼長度</param>
        /// <returns>流水號</returns>
        public ApiResult<string> CreateRECSerialNo(RECSerialNo param, string numHead, int numLen)
        {
            string sql = string.Empty;
            RECSerialNo currentSNo, newSNo;
            string currentNum;
            long newNumNo;
            string newNum;
            int rowsAffected;

        Redo:
            // 2: 依鍵值SYSID、SDATE
            currentSNo = QueryRECSerialNo(param, 2).Data.FirstOrDefault();
            if (currentSNo != null)
            {
                currentNum = currentSNo.NUM;
                newNumNo = long.Parse(currentNum.SubStr(numHead.Length)) + 1;
                newNum = numHead + newNumNo.ToString().PadLeft(numLen, '0');

                sql = @"
                update ni_RECSerialNo set
                NUM = @newNum
                where SYSID = @SYSID
                and SDATE = @SDATE
                and NUM = @NUM";

                rowsAffected = DB.NIS.Execute(sql, new
                {
                    SYSID = currentSNo.SYSID,
                    SDATE = currentSNo.SDATE,
                    NUM = currentSNo.NUM,
                    newNum = newNum
                });

                if (rowsAffected == 0)
                    goto Redo;
            }
            else
            {
                newNum = numHead + "1".PadLeft(numLen, '0');
                newSNo = new RECSerialNo();
                newSNo.SYSID = param.SYSID;
                newSNo.SDATE = param.SDATE;
                newSNo.NUM = newNum;
                rowsAffected = DB.NIS.Insert<RECSerialNo>(newSNo);

                if (rowsAffected == 0)
                    goto Redo;
            }

            var result = new ApiResult<string>(rowsAffected, newNum);
            return result;
        }

        public ApiResult<List<RECSerialNo>> QueryRECSerialNo(RECSerialNo param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依鍵值SYSID、SDATE
                    sql = @"
                    select * from ni_RECSerialNo
                    where SYSID = @SYSID 
                    and SDATE = @SDATE";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.NIS.Query<RECSerialNo>(param, schemaOnly: option != 1).ToList() :
            DB.NIS.Query<RECSerialNo>(sql, param).ToList();

            var result = new ApiResult<List<RECSerialNo>>(queryList);
            return result;
        }

    }
}
