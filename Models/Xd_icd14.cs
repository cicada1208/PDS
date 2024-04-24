using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "xd_icd14")]
    public class Xd_icd14 : BaseModel<Xd_icd14>
    {
        private string _icd14_code;
        [Key]
        public string icd14_code
        {
            get => _icd14_code;
            set => Set(ref _icd14_code, value);
        }

        private string _icd14_id;
        public string icd14_id
        {
            get => _icd14_id;
            set => Set(ref _icd14_id, value);
        }

        private string _icd14_10_cm;
        public string icd14_10_cm
        {
            get => _icd14_10_cm;
            set => Set(ref _icd14_10_cm, value);
        }

        private string _icd14_type;
        public string icd14_type
        {
            get => _icd14_type;
            set => Set(ref _icd14_type, value);
        }

        private string _icd14_pcs_code;
        public string icd14_pcs_code
        {
            get => _icd14_pcs_code;
            set => Set(ref _icd14_pcs_code, value);
        }

        private string _icd14_diag_system;
        public string icd14_diag_system
        {
            get => _icd14_diag_system;
            set => Set(ref _icd14_diag_system, value);
        }

        private string _icd14_big_dept;
        public string icd14_big_dept
        {
            get => _icd14_big_dept;
            set => Set(ref _icd14_big_dept, value);
        }

        private string _icd14_sub_dept;
        public string icd14_sub_dept
        {
            get => _icd14_sub_dept;
            set => Set(ref _icd14_sub_dept, value);
        }

        private string _icd14_level_3;
        public string icd14_level_3
        {
            get => _icd14_level_3;
            set => Set(ref _icd14_level_3, value);
        }

        private string _icd14_level_4;
        public string icd14_level_4
        {
            get => _icd14_level_4;
            set => Set(ref _icd14_level_4, value);
        }

        private string _icd14_level_5;
        public string icd14_level_5
        {
            get => _icd14_level_5;
            set => Set(ref _icd14_level_5, value);
        }

        private short? _icd14_seq_no;
        public short? icd14_seq_no
        {
            get => _icd14_seq_no;
            set => Set(ref _icd14_seq_no, value);
        }

        private int? _icd14_use_count;
        public int? icd14_use_count
        {
            get => _icd14_use_count;
            set => Set(ref _icd14_use_count, value);
        }

        private int? _icd14_use_count_v;
        public int? icd14_use_count_v
        {
            get => _icd14_use_count_v;
            set => Set(ref _icd14_use_count_v, value);
        }

        private string _icd14_diag_end;
        public string icd14_diag_end
        {
            get => _icd14_diag_end;
            set => Set(ref _icd14_diag_end, value);
        }

        private string _icd14_mdc;
        public string icd14_mdc
        {
            get => _icd14_mdc;
            set => Set(ref _icd14_mdc, value);
        }

        private string _icd14_mdc_1;
        public string icd14_mdc_1
        {
            get => _icd14_mdc_1;
            set => Set(ref _icd14_mdc_1, value);
        }

        private string _icd14_mdc_2;
        public string icd14_mdc_2
        {
            get => _icd14_mdc_2;
            set => Set(ref _icd14_mdc_2, value);
        }

        private string _icd14_mdc_3;
        public string icd14_mdc_3
        {
            get => _icd14_mdc_3;
            set => Set(ref _icd14_mdc_3, value);
        }

        private short? _icd14_inp_day;
        public short? icd14_inp_day
        {
            get => _icd14_inp_day;
            set => Set(ref _icd14_inp_day, value);
        }

        private string _icd14_s_rmk;
        public string icd14_s_rmk
        {
            get => _icd14_s_rmk;
            set => Set(ref _icd14_s_rmk, value);
        }

        private string _icd14_b_rmk;
        public string icd14_b_rmk
        {
            get => _icd14_b_rmk;
            set => Set(ref _icd14_b_rmk, value);
        }

        private string _icd14_t_rmk;
        public string icd14_t_rmk
        {
            get => _icd14_t_rmk;
            set => Set(ref _icd14_t_rmk, value);
        }

        private string _icd14_g_rmk;
        public string icd14_g_rmk
        {
            get => _icd14_g_rmk;
            set => Set(ref _icd14_g_rmk, value);
        }

        private string _icd14_p_rmk;
        public string icd14_p_rmk
        {
            get => _icd14_p_rmk;
            set => Set(ref _icd14_p_rmk, value);
        }

        private string _icd14_or_rmk;
        public string icd14_or_rmk
        {
            get => _icd14_or_rmk;
            set => Set(ref _icd14_or_rmk, value);
        }

        private string _icd14_detail_rmk;
        public string icd14_detail_rmk
        {
            get => _icd14_detail_rmk;
            set => Set(ref _icd14_detail_rmk, value);
        }

        private string _icd14_sex_rmk;
        public string icd14_sex_rmk
        {
            get => _icd14_sex_rmk;
            set => Set(ref _icd14_sex_rmk, value);
        }

        private string _icd14_age_rmk;
        public string icd14_age_rmk
        {
            get => _icd14_age_rmk;
            set => Set(ref _icd14_age_rmk, value);
        }

        private string _icd14_area_rmk;
        public string icd14_area_rmk
        {
            get => _icd14_area_rmk;
            set => Set(ref _icd14_area_rmk, value);
        }

        private string _icd14_side_rmk;
        public string icd14_side_rmk
        {
            get => _icd14_side_rmk;
            set => Set(ref _icd14_side_rmk, value);
        }

        private string _icd14_ins_rmk;
        public string icd14_ins_rmk
        {
            get => _icd14_ins_rmk;
            set => Set(ref _icd14_ins_rmk, value);
        }

        private string _icd14_shock_rmk;
        public string icd14_shock_rmk
        {
            get => _icd14_shock_rmk;
            set => Set(ref _icd14_shock_rmk, value);
        }

        private string _icd14_rare_rmk;
        public string icd14_rare_rmk
        {
            get => _icd14_rare_rmk;
            set => Set(ref _icd14_rare_rmk, value);
        }

        private string _icd14_pcare_rmk;
        public string icd14_pcare_rmk
        {
            get => _icd14_pcare_rmk;
            set => Set(ref _icd14_pcare_rmk, value);
        }

        private string _icd14_special_rmk;
        public string icd14_special_rmk
        {
            get => _icd14_special_rmk;
            set => Set(ref _icd14_special_rmk, value);
        }

        private string _icd14_9cm_2001;
        public string icd14_9cm_2001
        {
            get => _icd14_9cm_2001;
            set => Set(ref _icd14_9cm_2001, value);
        }

        private string _icd14_9cm_code;
        public string icd14_9cm_code
        {
            get => _icd14_9cm_code;
            set => Set(ref _icd14_9cm_code, value);
        }

        private string _icd14_filler1;
        public string icd14_filler1
        {
            get => _icd14_filler1;
            set => Set(ref _icd14_filler1, value);
        }

        private string _icd14_e_name;
        public string icd14_e_name
        {
            get => _icd14_e_name;
            set => Set(ref _icd14_e_name, value);
        }

        private string _icd14_c_name;
        public string icd14_c_name
        {
            get => _icd14_c_name;
            set => Set(ref _icd14_c_name, value);
        }

        private string _icd14_data_1;
        public string icd14_data_1
        {
            get => _icd14_data_1;
            set => Set(ref _icd14_data_1, value);
        }

    }
}

