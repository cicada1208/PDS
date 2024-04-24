using Params;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB2, "mh_mpat")]
    public class Mh_mpat : BaseModel<Mh_mpat>
    {
        private int? _pat_no;
        [Key]
        public int? pat_no
        {
            get => _pat_no;
            set => Set(ref _pat_no, value);
        }

        private int? _pat_srt;
        public int? pat_srt
        {
            get => _pat_srt;
            set => Set(ref _pat_srt, value);
        }

        private string _pat_orig;
        public string pat_orig
        {
            get => _pat_orig;
            set => Set(ref _pat_orig, value);
        }

        private string _pat_idno;
        public string pat_idno
        {
            get => _pat_idno;
            set => Set(ref _pat_idno, value);
        }

        private string _pat_name;
        public string pat_name
        {
            get => _pat_name;
            set => Set(ref _pat_name, value);
        }

        private int? _pat_birth_dt;
        public int? pat_birth_dt
        {
            get => _pat_birth_dt;
            set => Set(ref _pat_birth_dt, value);
        }

        private string _pat_ano_yn;
        public string pat_ano_yn
        {
            get => _pat_ano_yn;
            set => Set(ref _pat_ano_yn, value);
        }

        private string _pat_data_1;
        public string pat_data_1
        {
            get => _pat_data_1;
            set => Set(ref _pat_data_1, value);
        }

        private string _pat_data_2;
        public string pat_data_2
        {
            get => _pat_data_2;
            set => Set(ref _pat_data_2, value);
        }

        private string _pat_data_3;
        public string pat_data_3
        {
            get => _pat_data_3;
            set => Set(ref _pat_data_3, value);
        }

        private string _pat_data_4;
        public string pat_data_4
        {
            get => _pat_data_4;
            set => Set(ref _pat_data_4, value);
        }

        private string _pat_sex;
        [NotMapped]
        public string pat_sex
        {
            get => _pat_sex;
            set => Set(ref _pat_sex, value);
        }

        private string _pat_age;
        [NotMapped]
        public string pat_age
        {
            get => _pat_age;
            set => Set(ref _pat_age, value);
        }

        private string _pat_no_m;
        [NotMapped]
        public string pat_no_m
        {
            get => _pat_no_m;
            set => Set(ref _pat_no_m, value);
        }

        private string _pat_test_yn;
        [NotMapped]
        public string pat_test_yn
        {
            get => _pat_test_yn;
            set => Set(ref _pat_test_yn, value);
        }

    }
}
