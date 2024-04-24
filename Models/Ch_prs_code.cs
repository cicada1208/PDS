using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_prs_code")]
    public class Ch_prs_code : BaseModel<Ch_prs_code>
    {
        private string _prscode_no;
        [Key]
        public string prscode_no
        {
            get => _prscode_no;
            set => Set(ref _prscode_no, value);
        }

        private string _prscode_mst_id;
        [Display(Name = "藥品代碼")]
        [MaxLength(9)]
        public string prscode_mst_id
        {
            get => _prscode_mst_id;
            set => Set(ref _prscode_mst_id, value);
        }

        private string _prscode_brand;
        [Display(Name = "廠牌名稱")]
        [MaxLength(20)]
        public string prscode_brand
        {
            get => _prscode_brand;
            set => Set(ref _prscode_brand, value);
        }

        private string _prscode_code;
        [Display(Name = "藥品條碼")]
        [MaxLength(20)]
        public string prscode_code
        {
            get => _prscode_code;
            set => Set(ref _prscode_code, value);
        }

        private string _prscode_invalid_dt;
        [Display(Name = "失效日期")]
        [MaxLength(10)]
        public string prscode_invalid_dt
        {
            get => _prscode_invalid_dt;
            set => Set(ref _prscode_invalid_dt, value);
        }

        private string _prscode_st;
        [Display(Name = "狀態")]
        [MaxLength(1)]
        public string prscode_st
        {
            get => _prscode_st;
            set => Set(ref _prscode_st, value);
        }

        private string _prscode_md_man;
        public string prscode_md_man
        {
            get => _prscode_md_man;
            set => Set(ref _prscode_md_man, value);
        }

        private string _prscode_md_name;
        public string prscode_md_name
        {
            get => _prscode_md_name;
            set => Set(ref _prscode_md_name, value);
        }

        private string _prscode_md_pc;
        public string prscode_md_pc
        {
            get => _prscode_md_pc;
            set => Set(ref _prscode_md_pc, value);
        }

        private string _prscode_md_ver;
        public string prscode_md_ver
        {
            get => _prscode_md_ver;
            set => Set(ref _prscode_md_ver, value);
        }

        private string _prscode_md_dt;
        public string prscode_md_dt
        {
            get => _prscode_md_dt;
            set => Set(ref _prscode_md_dt, value);
        }

        private string _prscode_md_time;
        public string prscode_md_time
        {
            get => _prscode_md_time;
            set => Set(ref _prscode_md_time, value);
        }

        private string _prscode_filler;
        public string prscode_filler
        {
            get => _prscode_filler;
            set => Set(ref _prscode_filler, value);
        }

        private string _chprs_id_name;
        [NotMapped]
        [Display(Name = "藥品名稱")]
        public string chprs_id_name
        {
            get => _chprs_id_name;
            set => Set(ref _chprs_id_name, value);
        }

    }
}
