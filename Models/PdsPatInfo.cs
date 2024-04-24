using System.Windows;

namespace Models
{
    public class PdsPatInfo : BaseModel<PdsPatInfo>
    {
        private string _icbcode_code;
        /// <summary>
        /// 藥袋條碼
        /// </summary>
        public string icbcode_code
        {
            get => _icbcode_code;
            set => Set(ref _icbcode_code, value);
        }

        private string _lst_code;
        /// <summary>
        /// 配藥單條碼
        /// </summary>
        public string lst_code
        {
            get => _lst_code;
            set => Set(ref _lst_code, value);
        }

        private string _pat_no;
        public string pat_no
        {
            get => _pat_no;
            set => Set(ref _pat_no, value);
        }

        private string _ipd_no;
        public string ipd_no
        {
            get => _ipd_no;
            set => Set(ref _ipd_no, value);
        }

        private string _chudrec_bed;
        /// <summary>
        ///  藥車調劑時床號
        /// </summary>
        public string chudrec_bed
        {
            get => _chudrec_bed;
            set => Set(ref _chudrec_bed, value);
        }

        private string _chudrec_bed_unit;
        /// <summary>
        /// 藥車調劑時護理站
        /// </summary>
        public string chudrec_bed_unit
        {
            get => _chudrec_bed_unit;
            set => Set(ref _chudrec_bed_unit, value);
        }

        private string _chudrecchk_bed;
        /// <summary>
        /// 藥車核對時床號
        /// </summary>
        public string chudrecchk_bed
        {
            get => _chudrecchk_bed;
            set => Set(ref _chudrecchk_bed, value);
        }

        private string _chudrecchk_bed_unit;
        /// <summary>
        ///  藥車核對時護理站
        /// </summary>
        public string chudrecchk_bed_unit
        {
            get => _chudrecchk_bed_unit;
            set => Set(ref _chudrecchk_bed_unit, value);
        }

        private string _bed;
        /// <summary>
        /// 床號顯示
        /// </summary>
        public string bed
        {
            get => _bed;
            set => Set(ref _bed, value);
        }

        private string _bed_unit;
        /// <summary>
        /// 護理站顯示
        /// </summary>
        public string bed_unit
        {
            get => _bed_unit;
            set => Set(ref _bed_unit, value);
        }

        private string _pat_name;
        public string pat_name
        {
            get => _pat_name;
            set => Set(ref _pat_name, value);
        }

        private string _pat_sex;
        public string pat_sex
        {
            get => _pat_sex;
            set => Set(ref _pat_sex, value);
        }

        private string _pat_age;
        public string pat_age
        {
            get => _pat_age;
            set => Set(ref _pat_age, value);
        }

        private string _pat_height;
        public string pat_height
        {
            get => _pat_height;
            set => Set(ref _pat_height, value);
        }

        private string _pat_weight;
        public string pat_weight
        {
            get => _pat_weight;
            set => Set(ref _pat_weight, value);
        }

        private string _ipd_mj_dr1_name;
        public string ipd_mj_dr1_name
        {
            get => _ipd_mj_dr1_name;
            set => Set(ref _ipd_mj_dr1_name, value);
        }

        private string _ipdt_idzs_1_c_name;
        public string ipdt_idzs_1_c_name
        {
            get => _ipdt_idzs_1_c_name;
            set => Set(ref _ipdt_idzs_1_c_name, value);
        }

        private string _scr;
        public string SCr
        {
            get => _scr;
            set => Set(ref _scr, value);
        }

        private string _k;
        public string K
        {
            get => _k;
            set => Set(ref _k, value);
        }

        private string _mnsl;
        /// <summary>
        /// 是否管灌：Y
        /// </summary>
        public string mnsl
        {
            get => _mnsl;
            set
            {
                Set(ref _mnsl, value);
                mnslVisibility = _mnsl == "Y" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _mnslVisibility = Visibility.Collapsed;
        /// <summary>
        /// 是否管灌：Visibility
        /// </summary>
        public Visibility mnslVisibility
        {
            get => _mnslVisibility;
            set => Set(ref _mnslVisibility, value);
        }

        private string _hdb;
        /// <summary>
        /// 是否洗腎：Y
        /// </summary>
        public string hdb
        {
            get => _hdb;
            set
            {
                Set(ref _hdb, value);
                hdbVisibility = _hdb == "Y" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _hdbVisibility = Visibility.Collapsed;
        /// <summary>
        /// 是否洗腎：Visibility
        /// </summary>
        public Visibility hdbVisibility
        {
            get => _hdbVisibility;
            set => Set(ref _hdbVisibility, value);
        }

    }
}
