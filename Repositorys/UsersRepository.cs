using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class UsersRepository : BaseRepository<Users>
    {
        public ApiResult<List<Users>> QueryUser(Users param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依userId、狀態、未離職
                    sql = @"
                    select * from ni_Users
                    where userId = @userId
                    and isActive = @isActive
                    and dimission = 'N'";
                    break;
                case 3: // 依部門、狀態做用中、未離職
                    sql = @"
                    select * from ni_Users
                    where employeeDept = @employeeDept
                    and isActive = 1
                    and dimission = 'N'";
                    break;
            }

            var queryList = sql == string.Empty ?
                DB.NIS.Query<Users>(param, schemaOnly: option != 1).ToList() :
                DB.NIS.Query<Users>(sql, param).ToList();

            // way 3:
            //DataTable dtUser = new DataTable(this.TableName);
            //DB.NIS.Query(dtUser, sql, user);
            //userList = dtUser.ToList<Users>();

            // way 4:
            //AseCommand cmd = new AseCommand();
            //StringBuilder sbSQL = new StringBuilder();
            //sbSQL.Append(@"
            //select * from ni_Users
            //where userId = @userId
            //and isActive = @isActive 
            //and dimission = 'N'");
            //cmd.CommandText = sbSQL.ToString();
            //cmd.Parameters.Add("@userId", user.userId);
            //cmd.Parameters.Add("@isActive", user.isActive);
            //DB.NIS.ExecQuery(dtUser, cmd);
            //userList = dtUser.ToList<Users>();

            var result = new ApiResult<List<Users>>(queryList);
            return result;
        }

        public ApiResult<Users> UpdateUser(Users param, int option = 0)
        {
            int rowsAffected = 0;

            switch (option)
            {
                case 1: // 依參數自動組建
                    // way 1:
                    rowsAffected = DB.NIS.Update<Users>(param);
                    break;
                case 2: // 儲存userTerseName、systemDt
                    // way 2:
                    string sql = @"
                    update ni_Users set
                    userTerseName = @userTerseName,
                    systemDt = @systemDt
                    where userId = @userId";
                    rowsAffected = DB.NIS.Execute(sql, param);

                    // way 3:
                    //AseCommand cmd = new AseCommand();
                    //StringBuilder sbSQL = new StringBuilder();
                    //sbSQL.Append(
                    //@"update ni_Users set
                    //userTerseName = @userTerseName,
                    //systemDt = @systemDt
                    //where userId = @userId");
                    //cmd.CommandText = sbSQL.ToString();
                    //cmd.Parameters.Add("@userId", user.userId);
                    //cmd.Parameters.Add("@userTerseName", user.userTerseName);
                    //cmd.Parameters.Add("@systemDt", user.systemDt);
                    //succ = DB.NIS.ExecuteNonQuery(cmd);
                    break;
            }

            var result = new ApiResult<Users>(rowsAffected, param);
            result.Msg = result.Succ ? MsgParam.UpdateSuccess : MsgParam.UpdateFailure;
            return result;
        }

        public ApiResult<Users> InsertUser(Users param, int option = 0)
        {
            int rowsAffected = 0;

            param.userId = "test";
            param.loginId = "test";
            param.userName = "test";
            param.userTerseName = null;
            param.isActive = true;
            param.isService = true;
            param.isTester = true;
            param.systemUserId = "";
            param.systemDt = DateTime.Now;
            rowsAffected = DB.NIS.Insert<Users>(param);

            var result = new ApiResult<Users>(rowsAffected, param);
            result.Msg = result.Succ ? MsgParam.InsertSuccess : MsgParam.InsertFailure;
            return result;
        }

    }
}
