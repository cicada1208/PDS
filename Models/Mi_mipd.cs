using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_mipd")]
    public class Mi_mipd : BaseModel<Mi_mipd>
    {
        private string _ipd_no;
        [Key]
        public string ipd_no
        {
            get => _ipd_no;
            set => Set(ref _ipd_no, value);
        }

        private int? _ipd_dt;
        public int? ipd_dt
        {
            get => _ipd_dt;
            set => Set(ref _ipd_dt, value);
        }

        private int? _ipd_pat_no;
        public int? ipd_pat_no
        {
            get => _ipd_pat_no;
            set => Set(ref _ipd_pat_no, value);
        }

        private string _ipd_name;
        public string ipd_name
        {
            get => _ipd_name;
            set => Set(ref _ipd_name, value);
        }

        private string _ipd_bed;
        public string ipd_bed
        {
            get => _ipd_bed;
            set => Set(ref _ipd_bed, value);
        }

        private int? _ipd_out_dt;
        public int? ipd_out_dt
        {
            get => _ipd_out_dt;
            set => Set(ref _ipd_out_dt, value);
        }

        private string _ipd_alt;
        public string ipd_alt
        {
            get => _ipd_alt;
            set => Set(ref _ipd_alt, value);
        }

        private string _ipd_no_v;
        public string ipd_no_v
        {
            get => _ipd_no_v;
            set => Set(ref _ipd_no_v, value);
        }

        private int? _ipd_dt_v;
        public int? ipd_dt_v
        {
            get => _ipd_dt_v;
            set => Set(ref _ipd_dt_v, value);
        }

        private string _ipd_del_mark;
        public string ipd_del_mark
        {
            get => _ipd_del_mark;
            set => Set(ref _ipd_del_mark, value);
        }

        private string _ipd_mj_dr1;
        public string ipd_mj_dr1
        {
            get => _ipd_mj_dr1;
            set => Set(ref _ipd_mj_dr1, value);
        }

        private int? _ipd_apy_enddt;
        public int? ipd_apy_enddt
        {
            get => _ipd_apy_enddt;
            set => Set(ref _ipd_apy_enddt, value);
        }

        private string _ipd_group1;
        public string ipd_group1
        {
            get => _ipd_group1;
            set => Set(ref _ipd_group1, value);
        }

        private string _ipd_group2;
        public string ipd_group2
        {
            get => _ipd_group2;
            set => Set(ref _ipd_group2, value);
        }

        private string _ipd_mj_dr1_name;
        [NotMapped]
        public string ipd_mj_dr1_name
        {
            get => _ipd_mj_dr1_name;
            set => Set(ref _ipd_mj_dr1_name, value);
        }

        private string _ipd_out_res;
        [NotMapped]
        public string ipd_out_res
        {
            get => _ipd_out_res;
            set => Set(ref _ipd_out_res, value);
        }

    }
}
