using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_udrec_chk")]
    public class Ch_udrec_chk : BaseModel<Ch_udrec_chk>
    {
        private int? _chudrecchk_date;
        /// <summary>
        /// ¶Ç°e¤é´Á
        /// </summary>
        [Key]
        public int? chudrecchk_date
        {
            get => _chudrecchk_date;
            set => Set(ref _chudrecchk_date, value);
        }

        private int? _chudrecchk_pat_no;
        [Key]
        public int? chudrecchk_pat_no
        {
            get => _chudrecchk_pat_no;
            set => Set(ref _chudrecchk_pat_no, value);
        }

        private string _chudrecchk_bed;
        [Key]
        public string chudrecchk_bed
        {
            get => _chudrecchk_bed;
            set => Set(ref _chudrecchk_bed, value);
        }

        private string _chudrecchk_bed_unit;
        [Key]
        public string chudrecchk_bed_unit
        {
            get => _chudrecchk_bed_unit;
            set => Set(ref _chudrecchk_bed_unit, value);
        }

        private string _chudrecchk_ipd_no;
        public string chudrecchk_ipd_no
        {
            get => _chudrecchk_ipd_no;
            set => Set(ref _chudrecchk_ipd_no, value);
        }

        private string _chudrecchk_filler;
        public string chudrecchk_filler
        {
            get => _chudrecchk_filler;
            set => Set(ref _chudrecchk_filler, value);
        }

        private string _chudrec_bed;
        [NotMapped]
        public string chudrec_bed
        {
            get => _chudrec_bed;
            set => Set(ref _chudrec_bed, value);
        }

        private string _pds_rec_st;
        [NotMapped]
        public string pds_rec_st
        {
            get => _pds_rec_st;
            set => Set(ref _pds_rec_st, value);
        }

    }
}
