using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_rel2")]
    public class Ch_rel2 : BaseModel<Ch_rel2>
    {
        private int? _chrel2_pat_no;
        [Key]
        public int? chrel2_pat_no
        {
            get => _chrel2_pat_no;
            set => Set(ref _chrel2_pat_no, value);
        }

        private string _chrel2_itm_cd;
        [Key]
        public string chrel2_itm_cd
        {
            get => _chrel2_itm_cd;
            set => Set(ref _chrel2_itm_cd, value);
        }

        private string _chrel2_dpt_no;
        [Key]
        public string chrel2_dpt_no
        {
            get => _chrel2_dpt_no;
            set => Set(ref _chrel2_dpt_no, value);
        }

        private string _chrel2_cls_cd;
        [Key]
        public string chrel2_cls_cd
        {
            get => _chrel2_cls_cd;
            set => Set(ref _chrel2_cls_cd, value);
        }

        private int? _chrel2_pr_seq;
        [Key]
        public int? chrel2_pr_seq
        {
            get => _chrel2_pr_seq;
            set => Set(ref _chrel2_pr_seq, value);
        }

        private int? _chrel2_ip_date;
        [Key]
        public int? chrel2_ip_date
        {
            get => _chrel2_ip_date;
            set => Set(ref _chrel2_ip_date, value);
        }

        private string _chrel2_ctm_value;
        public string chrel2_ctm_value
        {
            get => _chrel2_ctm_value;
            set => Set(ref _chrel2_ctm_value, value);
        }

        private int? _chrel2_rp_date;
        public int? chrel2_rp_date
        {
            get => _chrel2_rp_date;
            set => Set(ref _chrel2_rp_date, value);
        }

        private int? _chrel2_rp_time;
        public int? chrel2_rp_time
        {
            get => _chrel2_rp_time;
            set => Set(ref _chrel2_rp_time, value);
        }

        private int? _chrel2_rp_date_v;
        public int? chrel2_rp_date_v
        {
            get => _chrel2_rp_date_v;
            set => Set(ref _chrel2_rp_date_v, value);
        }

        private int? _chrel2_rp_time_v;
        public int? chrel2_rp_time_v
        {
            get => _chrel2_rp_time_v;
            set => Set(ref _chrel2_rp_time_v, value);
        }

        private int? _chrel2_ck_date;
        public int? chrel2_ck_date
        {
            get => _chrel2_ck_date;
            set => Set(ref _chrel2_ck_date, value);
        }

        private int? _chrel2_ck_time;
        public int? chrel2_ck_time
        {
            get => _chrel2_ck_time;
            set => Set(ref _chrel2_ck_time, value);
        }

        private int? _chrel2_ck_date_v;
        public int? chrel2_ck_date_v
        {
            get => _chrel2_ck_date_v;
            set => Set(ref _chrel2_ck_date_v, value);
        }

        private int? _chrel2_ck_time_v;
        public int? chrel2_ck_time_v
        {
            get => _chrel2_ck_time_v;
            set => Set(ref _chrel2_ck_time_v, value);
        }

        private int? _chrel2_ac_date;
        public int? chrel2_ac_date
        {
            get => _chrel2_ac_date;
            set => Set(ref _chrel2_ac_date, value);
        }

        private int? _chrel2_ac_time;
        public int? chrel2_ac_time
        {
            get => _chrel2_ac_time;
            set => Set(ref _chrel2_ac_time, value);
        }

        private string _chrel2_ip_no;
        public string chrel2_ip_no
        {
            get => _chrel2_ip_no;
            set => Set(ref _chrel2_ip_no, value);
        }

        private string _chrel2_ip_man;
        public string chrel2_ip_man
        {
            get => _chrel2_ip_man;
            set => Set(ref _chrel2_ip_man, value);
        }

        private string _chrel2_filler;
        public string chrel2_filler
        {
            get => _chrel2_filler;
            set => Set(ref _chrel2_filler, value);
        }

        private decimal? _A4GLIdentity;
        public decimal? A4GLIdentity
        {
            get => _A4GLIdentity;
            set => Set(ref _A4GLIdentity, value);
        }

        private string _chrel2_ctm_unit;
        [NotMapped]
        public string chrel2_ctm_unit
        {
            get => _chrel2_ctm_unit;
            set => Set(ref _chrel2_ctm_unit, value);
        }
    }
}

