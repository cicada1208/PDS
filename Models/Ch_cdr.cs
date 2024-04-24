using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_cdr")]
    public class Ch_cdr : BaseModel<Ch_cdr>
    {
        private short? _cdr_knd;
        [Key]
        public short? cdr_knd
        {
            get => _cdr_knd;
            set => Set(ref _cdr_knd, value);
        }

        private int? _cdr_ymd;
        [Key]
        public int? cdr_ymd
        {
            get => _cdr_ymd;
            set => Set(ref _cdr_ymd, value);
        }

        private int? _cdr_seq;
        [Key]
        public int? cdr_seq
        {
            get => _cdr_seq;
            set => Set(ref _cdr_seq, value);
        }

        private int? _cdr_pat_no;
        public int? cdr_pat_no
        {
            get => _cdr_pat_no;
            set => Set(ref _cdr_pat_no, value);
        }

        private string _cdr_pill_id;
        public string cdr_pill_id
        {
            get => _cdr_pill_id;
            set => Set(ref _cdr_pill_id, value);
        }

        private short? _cdr_pill_no;
        public short? cdr_pill_no
        {
            get => _cdr_pill_no;
            set => Set(ref _cdr_pill_no, value);
        }

        private string _cdr_bed_no;
        public string cdr_bed_no
        {
            get => _cdr_bed_no;
            set => Set(ref _cdr_bed_no, value);
        }

        private int? _cdr_med_qty;
        public int? cdr_med_qty
        {
            get => _cdr_med_qty;
            set => Set(ref _cdr_med_qty, value);
        }

        private int? _cdr_ins_date;
        public int? cdr_ins_date
        {
            get => _cdr_ins_date;
            set => Set(ref _cdr_ins_date, value);
        }

        private short? _cdr_ins_time;
        public short? cdr_ins_time
        {
            get => _cdr_ins_time;
            set => Set(ref _cdr_ins_time, value);
        }

        private string _cdr_lin_knd;
        public string cdr_lin_knd
        {
            get => _cdr_lin_knd;
            set => Set(ref _cdr_lin_knd, value);
        }

        private string _cdr_dept_no;
        public string cdr_dept_no
        {
            get => _cdr_dept_no;
            set => Set(ref _cdr_dept_no, value);
        }

        private string _cdr_pre_yn;
        public string cdr_pre_yn
        {
            get => _cdr_pre_yn;
            set => Set(ref _cdr_pre_yn, value);
        }

        private int? _cdr_pre_date;
        public int? cdr_pre_date
        {
            get => _cdr_pre_date;
            set => Set(ref _cdr_pre_date, value);
        }

        private short? _cdr_pre_time;
        public short? cdr_pre_time
        {
            get => _cdr_pre_time;
            set => Set(ref _cdr_pre_time, value);
        }

        private string _cdr_pre_usr;
        public string cdr_pre_usr
        {
            get => _cdr_pre_usr;
            set => Set(ref _cdr_pre_usr, value);
        }

        private string _cdr_mark;
        public string cdr_mark
        {
            get => _cdr_mark;
            set => Set(ref _cdr_mark, value);
        }

        private string _cdr_ctl_lv;
        public string cdr_ctl_lv
        {
            get => _cdr_ctl_lv;
            set => Set(ref _cdr_ctl_lv, value);
        }

        private string _cdr_alt_key;
        public string cdr_alt_key
        {
            get => _cdr_alt_key;
            set => Set(ref _cdr_alt_key, value);
        }

        private string _cdr_filler;
        public string cdr_filler
        {
            get => _cdr_filler;
            set => Set(ref _cdr_filler, value);
        }

    }
}

