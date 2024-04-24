using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_udrec")]
    public class Ch_udrec : BaseModel<Ch_udrec>
    {
        private int? _chudrec_date;
        /// <summary>
        /// ¶Ç°e¤é´Á
        /// </summary>
        [Key]
        public int? chudrec_date
        {
            get => _chudrec_date;
            set => Set(ref _chudrec_date, value);
        }

        private int? _chudrec_pat_no;
        [Key]
        public int? chudrec_pat_no
        {
            get => _chudrec_pat_no;
            set => Set(ref _chudrec_pat_no, value);
        }

        private string _chudrec_bed;
        [Key]
        public string chudrec_bed
        {
            get => _chudrec_bed;
            set => Set(ref _chudrec_bed, value);
        }

        private string _chudrec_bed_unit;
        [Key]
        public string chudrec_bed_unit
        {
            get => _chudrec_bed_unit;
            set => Set(ref _chudrec_bed_unit, value);
        }

        private string _chudrec_ipd_no;
        public string chudrec_ipd_no
        {
            get => _chudrec_ipd_no;
            set => Set(ref _chudrec_ipd_no, value);
        }

        private string _chudrec_send_mark;
        public string chudrec_send_mark
        {
            get => _chudrec_send_mark;
            set => Set(ref _chudrec_send_mark, value);
        }

        private int? _chudrec_send_date;
        public int? chudrec_send_date
        {
            get => _chudrec_send_date;
            set => Set(ref _chudrec_send_date, value);
        }

        private int? _chudrec_send_time;
        public int? chudrec_send_time
        {
            get => _chudrec_send_time;
            set => Set(ref _chudrec_send_time, value);
        }

        private string _chudrec_send_user;
        public string chudrec_send_user
        {
            get => _chudrec_send_user;
            set => Set(ref _chudrec_send_user, value);
        }

        private int? _chudrec_dbt_date;
        public int? chudrec_dbt_date
        {
            get => _chudrec_dbt_date;
            set => Set(ref _chudrec_dbt_date, value);
        }

        private int? _chudrec_dbt_time;
        public int? chudrec_dbt_time
        {
            get => _chudrec_dbt_time;
            set => Set(ref _chudrec_dbt_time, value);
        }

        private string _chudrec_dbt_user;
        public string chudrec_dbt_user
        {
            get => _chudrec_dbt_user;
            set => Set(ref _chudrec_dbt_user, value);
        }

        private int? _chudrec_print_date;
        public int? chudrec_print_date
        {
            get => _chudrec_print_date;
            set => Set(ref _chudrec_print_date, value);
        }

        private int? _chudrec_print_time;
        public int? chudrec_print_time
        {
            get => _chudrec_print_time;
            set => Set(ref _chudrec_print_time, value);
        }

        private string _chudrec_print_user;
        public string chudrec_print_user
        {
            get => _chudrec_print_user;
            set => Set(ref _chudrec_print_user, value);
        }

        private int? _chudrec_send_days;
        public int? chudrec_send_days
        {
            get => _chudrec_send_days;
            set => Set(ref _chudrec_send_days, value);
        }

        private string _chudrec_filler;
        public string chudrec_filler
        {
            get => _chudrec_filler;
            set => Set(ref _chudrec_filler, value);
        }

        private string _chudrecchk_bed;
        [NotMapped]
        public string chudrecchk_bed
        {
            get => _chudrecchk_bed;
            set => Set(ref _chudrecchk_bed, value);
        }

        private string _chudrecchk_bed_unit;
        [NotMapped]
        public string chudrecchk_bed_unit
        {
            get => _chudrecchk_bed_unit;
            set => Set(ref _chudrecchk_bed_unit, value);
        }

    }
}
