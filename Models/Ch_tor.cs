using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_tor")]
    public class Ch_tor : BaseModel<Ch_tor>
    {
        private int? _chtor_server_dt;
        [Key]
        public int? chtor_server_dt
        {
            get => _chtor_server_dt;
            set => Set(ref _chtor_server_dt, value);
        }

        private int? _chtor_server_ti;
        [Key]
        public int? chtor_server_ti
        {
            get => _chtor_server_ti;
            set => Set(ref _chtor_server_ti, value);
        }

        private int? _chtor_pat_no;
        [Key]
        public int? chtor_pat_no
        {
            get => _chtor_pat_no;
            set => Set(ref _chtor_pat_no, value);
        }

        private string _chtor_item;
        [Key]
        public string chtor_item
        {
            get => _chtor_item;
            set => Set(ref _chtor_item, value);
        }

        private string _chtor_from_sys_1;
        [Key]
        public string chtor_from_sys_1
        {
            get => _chtor_from_sys_1;
            set => Set(ref _chtor_from_sys_1, value);
        }

        private string _chtor_from_sys_2;
        [Key]
        public string chtor_from_sys_2
        {
            get => _chtor_from_sys_2;
            set => Set(ref _chtor_from_sys_2, value);
        }

        private decimal? _chtor_ipd_no;
        [Key]
        public decimal? chtor_ipd_no
        {
            get => _chtor_ipd_no;
            set => Set(ref _chtor_ipd_no, value);
        }

        private short? _chtor_ipd_seq;
        [Key]
        public short? chtor_ipd_seq
        {
            get => _chtor_ipd_seq;
            set => Set(ref _chtor_ipd_seq, value);
        }

        private string _chtor_filler1;
        [Key]
        public string chtor_filler1
        {
            get => _chtor_filler1;
            set => Set(ref _chtor_filler1, value);
        }

        private string _chtor_del_mark;
        public string chtor_del_mark
        {
            get => _chtor_del_mark;
            set => Set(ref _chtor_del_mark, value);
        }

        private int? _chtor_cre_dt;
        public int? chtor_cre_dt
        {
            get => _chtor_cre_dt;
            set => Set(ref _chtor_cre_dt, value);
        }

        private int? _chtor_cre_ti;
        public int? chtor_cre_ti
        {
            get => _chtor_cre_ti;
            set => Set(ref _chtor_cre_ti, value);
        }

        private int? _chtor_cre_dt_v;
        public int? chtor_cre_dt_v
        {
            get => _chtor_cre_dt_v;
            set => Set(ref _chtor_cre_dt_v, value);
        }

        private int? _chtor_cre_ti_v;
        public int? chtor_cre_ti_v
        {
            get => _chtor_cre_ti_v;
            set => Set(ref _chtor_cre_ti_v, value);
        }

        private string _chtor_value_type;
        public string chtor_value_type
        {
            get => _chtor_value_type;
            set => Set(ref _chtor_value_type, value);
        }

        private string _chtor_value_str;
        public string chtor_value_str
        {
            get => _chtor_value_str;
            set => Set(ref _chtor_value_str, value);
        }

        private decimal? _chtor_value_num;
        public decimal? chtor_value_num
        {
            get => _chtor_value_num;
            set => Set(ref _chtor_value_num, value);
        }

        private string _chtor_unit;
        public string chtor_unit
        {
            get => _chtor_unit;
            set => Set(ref _chtor_unit, value);
        }

        private string _chtor_memo;
        public string chtor_memo
        {
            get => _chtor_memo;
            set => Set(ref _chtor_memo, value);
        }

        private string _chtor_cre_user;
        public string chtor_cre_user
        {
            get => _chtor_cre_user;
            set => Set(ref _chtor_cre_user, value);
        }

        private string _chtor_cre_mj_dr;
        public string chtor_cre_mj_dr
        {
            get => _chtor_cre_mj_dr;
            set => Set(ref _chtor_cre_mj_dr, value);
        }

        private string _chtor_upd_user;
        public string chtor_upd_user
        {
            get => _chtor_upd_user;
            set => Set(ref _chtor_upd_user, value);
        }

        private string _chtor_upd_mj_dr;
        public string chtor_upd_mj_dr
        {
            get => _chtor_upd_mj_dr;
            set => Set(ref _chtor_upd_mj_dr, value);
        }

        private string _chtor_filler2;
        public string chtor_filler2
        {
            get => _chtor_filler2;
            set => Set(ref _chtor_filler2, value);
        }

    }
}
