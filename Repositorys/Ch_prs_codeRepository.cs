using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Ch_prs_codeRepository : BaseRepository<Ch_prs_code>
    {
        public ApiResult<List<Ch_prs_code>> QueryCh_prs_code(Ch_prs_code param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依藥品代碼、有效的項目
                    sql = @"
                    select * 
                    from ch_prs_code
                    where prscode_mst_id = @prscode_mst_id
                    and isnull(prscode_st,'') <> 'D'
                    and (prscode_invalid_dt = '' or convert(varchar, getdate(), 111) < prscode_invalid_dt)";
                    break;
                case 3: // 依藥品條碼、有效的項目
                    sql = @"
                    select * 
                    from ch_prs_code
                    where prscode_code = @prscode_code
                    and isnull(prscode_st,'') <> 'D'
                    and (prscode_invalid_dt = '' or convert(varchar, getdate(), 111) < prscode_invalid_dt)";
                    break;
                case 4: // 依非空值篩選
                    sql = @"
                    select * 
                    from ch_prs_code
                    where 1 = 1";
                    if (!string.IsNullOrWhiteSpace(param.prscode_mst_id))
                        sql += Environment.NewLine + "and prscode_mst_id = @prscode_mst_id";
                    if (!string.IsNullOrWhiteSpace(param.prscode_brand))
                    {
                        param.prscode_brand = $"%{param.prscode_brand}%";
                        sql += Environment.NewLine + "and prscode_brand like @prscode_brand";
                    }
                    if (!string.IsNullOrWhiteSpace(param.prscode_code))
                        sql += Environment.NewLine + "and prscode_code = @prscode_code";
                    if (!string.IsNullOrWhiteSpace(param.prscode_invalid_dt)) // 失效日期：填上02/18則查詢>02/18或空白的項目
                        sql += Environment.NewLine + "and (prscode_invalid_dt = '' or prscode_invalid_dt > @prscode_invalid_dt)";
                    sql += Environment.NewLine + "and prscode_st = @prscode_st";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Ch_prs_code>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Ch_prs_code>(sql, param).ToList();

            switch (option)
            {
                case 1:
                case 4:
                    // 2:  依處置類別藥品，查詢處置代碼、處置名稱一
                    var prsList = DB.Ch_prsRepository.QueryCh_prs(null, 2);
                    queryList.ForEach(code =>
                    {
                        code.chprs_id_name = prsList.Data.Find(
                            p => p.chprs_mst_id == code.prscode_mst_id)
                        ?.chprs_id_name ?? string.Empty;
                    });
                    prsList = null;
                    break;
            }

            return new ApiResult<List<Ch_prs_code>>(queryList);
        }

        public ApiResult<Ch_prs_code> SaveCh_prs_code(Ch_prs_code param, int option = 0)
        {
            ApiResult<Ch_prs_code> result;

            if (string.IsNullOrWhiteSpace(param.prscode_no))
                result = InsertCh_prs_code(param, 1);    // 1: 依參數自動組建
            else
                result = UpdateCh_prs_code(param, 1); // 1: 依參數自動組建

            return result;
        }

        public ApiResult<Ch_prs_code> InsertCh_prs_code(Ch_prs_code param, int option = 0)
        {
            int rowsAffected = 0;
            string msg = string.Empty;
            int maxCodeNum = 20;
            List<Ch_prs_code> queryData;

            // 稽核
            if (msg == string.Empty)
            {
                queryData = QueryCh_prs_code(param, 2).Data; // 2: 依藥品代碼、有效的項目
                if (queryData.Count >= maxCodeNum)
                    msg = $"每個藥品代碼，有效的條碼，最多{maxCodeNum}個，已達{queryData.Count}。";
            }
            if (msg == string.Empty)
            {
                queryData = QueryCh_prs_code(param, 3).Data; // 3: 依藥品條碼、有效的項目
                if (queryData.Count > 0)
                    msg = $"已存在藥品條碼{param.prscode_code}，所屬藥品代碼為{queryData.First().prscode_mst_id}。";
            }
            if (msg == string.Empty)
            {
                // PK
                if (string.IsNullOrWhiteSpace(param.prscode_no))
                {
                    RECSerialNo sno = new RECSerialNo();
                    sno.SYSID = SNoParam.PRSCODE;
                    sno.SDATE = DateTime.Now.ToString("yyyyMMdd");
                    param.prscode_no = DB.RECSerialNoRepository.CreateRECSerialNo(sno, sno.SDATE, 6).Data;
                }

                // 修改日期時間
                param.prscode_md_dt = DateTime.Now.ToString("yyyy/MM/dd");
                param.prscode_md_time = DateTime.Now.ToString("HH:mm:ss");

                switch (option)
                {
                    case 1:   // 依參數自動組建
                        rowsAffected = DB.Syb1.Insert<Ch_prs_code>(param);
                        break;
                }
            }

            var result = new ApiResult<Ch_prs_code>(rowsAffected, param);
            result.Msg = result.Succ ? MsgParam.InsertSuccess : MsgParam.InsertFailure;
            if (msg != string.Empty) result.Msg += Environment.NewLine + msg;
            return result;
        }

        public ApiResult<Ch_prs_code> UpdateCh_prs_code(Ch_prs_code param, int option = 0)
        {
            int rowsAffected = 0;
            string msg = string.Empty;
            List<Ch_prs_code> queryData;

            param = param.StrProcess(nullToEmpty: true);

            // 項目修改為未失效需稽核
            if (param.prscode_st != "D" &&
                (param.prscode_invalid_dt == string.Empty ||
               DateTime.Parse(param.prscode_invalid_dt) > DateTime.Today))
            {
                queryData = QueryCh_prs_code(param, 3).Data // 3: 依藥品條碼、有效的項目
                    .Where(c => c.prscode_no != param.prscode_no).ToList(); // 排除自身
                if (queryData.Count > 0)
                    msg = $"已存在藥品條碼{param.prscode_code}，所屬藥品代碼為{queryData.First().prscode_mst_id}。";
            }
            if (msg == string.Empty)
            {
                // 修改日期時間
                param.prscode_md_dt = DateTime.Now.ToString("yyyy/MM/dd");
                param.prscode_md_time = DateTime.Now.ToString("HH:mm:ss");

                switch (option)
                {
                    case 1:   // 依參數自動組建
                        rowsAffected = DB.Syb1.Update<Ch_prs_code>(param);
                        break;
                }
            }

            var result = new ApiResult<Ch_prs_code>(rowsAffected, param);
            result.Msg = result.Succ ? MsgParam.UpdateSuccess : MsgParam.UpdateFailure;
            if (msg != string.Empty) result.Msg += Environment.NewLine + msg;
            return result;
        }

    }
}
