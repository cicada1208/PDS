using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mn_mnsl")]
    public class Mn_mnsl : BaseModel<Mn_mnsl>
    {
        private string _nsl_ipd_no;
        [Key]
        public string nsl_ipd_no
        {
            get => _nsl_ipd_no;
            set => Set(ref _nsl_ipd_no, value);
        }

        private string _nsl_eat_code;
        [Key]
        public string nsl_eat_code
        {
            get => _nsl_eat_code;
            set => Set(ref _nsl_eat_code, value);
        }

        private short? _nsl_seq_no;
        [Key]
        public short? nsl_seq_no
        {
            get => _nsl_seq_no;
            set => Set(ref _nsl_seq_no, value);
        }

        private int? _nsl_pat_no;
        public int? nsl_pat_no
        {
            get => _nsl_pat_no;
            set => Set(ref _nsl_pat_no, value);
        }

        private string _nsl_beg_data;
        public string nsl_beg_data
        {
            get => _nsl_beg_data;
            set => Set(ref _nsl_beg_data, value);
        }

        private string _nsl_end_data;
        public string nsl_end_data
        {
            get => _nsl_end_data;
            set => Set(ref _nsl_end_data, value);
        }

        private short? _nsl_knd_sel;
        public short? nsl_knd_sel
        {
            get => _nsl_knd_sel;
            set => Set(ref _nsl_knd_sel, value);
        }

        private int? _nsl_bld_dt;
        public int? nsl_bld_dt
        {
            get => _nsl_bld_dt;
            set => Set(ref _nsl_bld_dt, value);
        }

        private string _nsl_odr_code;
        public string nsl_odr_code
        {
            get => _nsl_odr_code;
            set => Set(ref _nsl_odr_code, value);
        }

        private int? _nsl_end_dt_v;
        public int? nsl_end_dt_v
        {
            get => _nsl_end_dt_v;
            set => Set(ref _nsl_end_dt_v, value);
        }

        private string _nsl_eat_code_v;
        public string nsl_eat_code_v
        {
            get => _nsl_eat_code_v;
            set => Set(ref _nsl_eat_code_v, value);
        }

        private string _nsl_key_v;
        public string nsl_key_v
        {
            get => _nsl_key_v;
            set => Set(ref _nsl_key_v, value);
        }

        private int? _nsl_bld_dt_v;
        public int? nsl_bld_dt_v
        {
            get => _nsl_bld_dt_v;
            set => Set(ref _nsl_bld_dt_v, value);
        }

        private string _nsl_data2;
        public string nsl_data2
        {
            get => _nsl_data2;
            set => Set(ref _nsl_data2, value);
        }

        private string _nsl_data3;
        public string nsl_data3
        {
            get => _nsl_data3;
            set => Set(ref _nsl_data3, value);
        }

    }
}

