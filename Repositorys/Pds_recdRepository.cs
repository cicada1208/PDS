using Models;
using Params;
using System.Collections.Generic;
using System.Linq;

namespace Repositorys
{
    public class Pds_recdRepository : BaseRepository<Pds_recd>
    {
        public ApiResult<List<Pds_recd>> QueryPds_recd(Pds_recd param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1: // 依參數自動組建
                    break;
                case 2: // 依主檔作業類型、藥袋條碼、歷程檔狀態原因，查詢最新處方修改、核對處方修改
                    sql = $@"
                    select top 1 recd.* 
                    from pds_rec as rec
                    left join pds_recd as recd
                    on (pds_recd_rec_no = pds_rec_no)
                    where pds_rec_bag_code=@pds_rec_bag_code
                    and pds_rec_op_type in ('ABAG','CBAG')
                    and pds_recd_reason in ('C04','C08')
                    order by pds_recd_op_dtm desc";
                    break;
                case 3: // 依主檔單號
                    sql = @"
                    select recd.*, pds_rec_bag_code, pds_rec_op_type,
                    opcode.rec_code_name as pds_rec_op_name,
                    opdcode.rec_code_name as pds_recd_op_name,
                    stdcode.rec_code_name as pds_recd_st_name,
                    rndcode.rec_code_name as pds_recd_reason_name
                    from pds_rec as rec
                    left join pds_recd as recd
                    on (pds_recd_rec_no=pds_rec_no)
                    left join rec_code as opcode
                    on (opcode.rec_code_model='pds_rec_op_type' and opcode.rec_code_group='pds_rec_op_type' 
                    and opcode.rec_code_short=pds_rec_op_type and opcode.rec_code_st='1')
                    left join rec_code as opdcode
                    on (opdcode.rec_code_model='pds_recd_op_type' and opdcode.rec_code_group='pds_recd_op_type' 
                    and opdcode.rec_code_short=pds_recd_op_type and opdcode.rec_code_st='1')
                    left join rec_code as stdcode
                    on (stdcode.rec_code_model='pds_rec_st' and stdcode.rec_code_group='pds_rec_st' 
                    and stdcode.rec_code_short=pds_recd_st and stdcode.rec_code_st='1')
                    left join rec_code as rndcode
                    on (rndcode.rec_code_model='pds_rec_reason'
                    and rndcode.rec_code_short=pds_recd_reason and rndcode.rec_code_st='1')
                    where pds_recd_rec_no = @pds_recd_rec_no
                    and pds_recd_no is not null
                    order by pds_recd_op_dtm,  pds_recd_no";
                    break;
                case 4: // 依主檔作業類型、藥袋條碼、歷程檔狀態原因，查詢首日量最新調劑/核對/發藥處方修改
                    sql = $@"
                    select top 1 recd.* 
                    from pds_rec as rec
                    left join pds_recd as recd
                    on (pds_recd_rec_no = pds_rec_no)
                    where pds_rec_bag_code=@pds_rec_bag_code
                    and pds_rec_op_type in ('FABAG','FCBAG','FRBAG')
                    and pds_recd_reason in ('C04','C08','C11')
                    order by pds_recd_op_dtm desc";
                    break;
                case 5: // 依配藥單條碼
                case 6: // 依藥袋條碼
                    string baglst = string.Empty;
                    if(option == 5) baglst = "pds_rec_lst_code = @pds_rec_lst_code";
                    else baglst = "pds_rec_bag_code = @pds_rec_bag_code";

                    sql = $@"
                    select recd.*, pds_rec_bag_code, pds_rec_op_type, 
                    opcode.rec_code_name as pds_rec_op_name,
                    opdcode.rec_code_name as pds_recd_op_name,
                    stdcode.rec_code_name as pds_recd_st_name,
                    rndcode.rec_code_name as pds_recd_reason_name
                    from pds_rec as rec
                    left join pds_recd as recd
                    on (pds_recd_rec_no=pds_rec_no)
                    left join rec_code as opcode
                    on (opcode.rec_code_model='pds_rec_op_type' and opcode.rec_code_group='pds_rec_op_type' 
                    and opcode.rec_code_short=pds_rec_op_type and opcode.rec_code_st='1')
                    left join rec_code as opdcode
                    on (opdcode.rec_code_model='pds_recd_op_type' and opdcode.rec_code_group='pds_recd_op_type' 
                    and opdcode.rec_code_short=pds_recd_op_type and opdcode.rec_code_st='1')
                    left join rec_code as stdcode
                    on (stdcode.rec_code_model='pds_rec_st' and stdcode.rec_code_group='pds_rec_st' 
                    and stdcode.rec_code_short=pds_recd_st and stdcode.rec_code_st='1')
                    left join rec_code as rndcode
                    on (rndcode.rec_code_model='pds_rec_reason'
                    and rndcode.rec_code_short=pds_recd_reason and rndcode.rec_code_st='1')
                    where {baglst}
                    and pds_recd_no is not null
                    order by pds_recd_op_dtm,  pds_recd_no";
                    break;
                case 7: // 依藥袋條碼、主檔作業類型、動作日期時間之前，查詢最後作業藥師
                    sql = @"
                    select top 1 recd.* 
                    from pds_rec as rec
                    left join pds_recd as recd
                    on (pds_recd_rec_no=pds_rec_no)
                    where pds_rec_bag_code=@pds_rec_bag_code
                    and pds_rec_op_type=@pds_rec_op_type
                    and pds_recd_op_dtm <= @pds_recd_op_dtm
                    order by pds_recd_op_dtm desc";
                    break;
            }

            var queryList = sql == string.Empty ?
            DB.Syb1.Query<Pds_recd>(param, schemaOnly: option != 1).ToList() :
            DB.Syb1.Query<Pds_recd>(sql, param).ToList();

            return new ApiResult<List<Pds_recd>>(queryList);
        }

    }
}
