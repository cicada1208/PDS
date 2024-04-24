using Params;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "pds_rec")]
    public class Pds_rec : BaseModel<Pds_rec>
    {
        private string _pds_rec_no;
        [Key]
        public string pds_rec_no
        {
            get => _pds_rec_no;
            set => Set(ref _pds_rec_no, value);
        }

        private string _pds_rec_op_type;
        public string pds_rec_op_type
        {
            get => _pds_rec_op_type;
            set => Set(ref _pds_rec_op_type, value);
        }

        private string _pds_rec_op_dtm_begin;
        public string pds_rec_op_dtm_begin
        {
            get => _pds_rec_op_dtm_begin;
            set => Set(ref _pds_rec_op_dtm_begin, value);
        }

        private string _pds_rec_op_dtm_end;
        public string pds_rec_op_dtm_end
        {
            get => _pds_rec_op_dtm_end;
            set => Set(ref _pds_rec_op_dtm_end, value);
        }

        private string _pds_rec_bag_code;
        public string pds_rec_bag_code
        {
            get => _pds_rec_bag_code;
            set => Set(ref _pds_rec_bag_code, value);
        }

        private string _pds_rec_lst_code;
        public string pds_rec_lst_code
        {
            get => _pds_rec_lst_code;
            set => Set(ref _pds_rec_lst_code, value);
        }

        private int? _pds_rec_send_dt;
        public int? pds_rec_send_dt
        {
            get => _pds_rec_send_dt;
            set => Set(ref _pds_rec_send_dt, value);
        }

        private string _pds_rec_ipd_no;
        public string pds_rec_ipd_no
        {
            get => _pds_rec_ipd_no;
            set => Set(ref _pds_rec_ipd_no, value);
        }

        private string _pds_rec_pat_no;
        public string pds_rec_pat_no
        {
            get => _pds_rec_pat_no;
            set => Set(ref _pds_rec_pat_no, value);
        }

        private string _pds_rec_bed;
        public string pds_rec_bed
        {
            get => _pds_rec_bed;
            set => Set(ref _pds_rec_bed, value);
        }

        private string _pds_rec_clinical;
        public string pds_rec_clinical
        {
            get => _pds_rec_clinical;
            set => Set(ref _pds_rec_clinical, value);
        }

        private string _pds_rec_st;
        public string pds_rec_st
        {
            get => _pds_rec_st;
            set => Set(ref _pds_rec_st, value);
        }

        private string _pds_rec_reason;
        public string pds_rec_reason
        {
            get => _pds_rec_reason;
            set => Set(ref _pds_rec_reason, value);
        }

        private string _pds_rec_reason_oth;
        public string pds_rec_reason_oth
        {
            get => _pds_rec_reason_oth;
            set => Set(ref _pds_rec_reason_oth, value);
        }

        private string _pds_rec_nondeliver;
        [Display(Name = "無法交車註記")]
        [MaxLength(100)] 
        public string pds_rec_nondeliver
        {
            get => _pds_rec_nondeliver;
            set => Set(ref _pds_rec_nondeliver, value);
        }

        private string _pds_rec_note;
        [Display(Name = "備註")]
        [MaxLength(60)]
        public string pds_rec_note
        {
            get => _pds_rec_note;
            set => Set(ref _pds_rec_note, value);
        }

        private string _pds_rec_md_qty;
        public string pds_rec_md_qty
        {
            get => _pds_rec_md_qty;
            set => Set(ref _pds_rec_md_qty, value);
        }

        private string _pds_rec_md_way1;
        public string pds_rec_md_way1
        {
            get => _pds_rec_md_way1;
            set => Set(ref _pds_rec_md_way1, value);
        }

        private string _pds_rec_md_man;
        public string pds_rec_md_man
        {
            get => _pds_rec_md_man;
            set => Set(ref _pds_rec_md_man, value);
        }

        private string _pds_rec_md_name;
        public string pds_rec_md_name
        {
            get => _pds_rec_md_name;
            set => Set(ref _pds_rec_md_name, value);
        }

        private string _pds_rec_md_pc;
        public string pds_rec_md_pc
        {
            get => _pds_rec_md_pc;
            set => Set(ref _pds_rec_md_pc, value);
        }

        private string _pds_rec_md_ver;
        public string pds_rec_md_ver
        {
            get => _pds_rec_md_ver;
            set => Set(ref _pds_rec_md_ver, value);
        }

        private string _pds_rec_md_dt;
        public string pds_rec_md_dt
        {
            get => _pds_rec_md_dt;
            set => Set(ref _pds_rec_md_dt, value);
        }

        private string _pds_rec_md_time;
        public string pds_rec_md_time
        {
            get => _pds_rec_md_time;
            set => Set(ref _pds_rec_md_time, value);
        }

        private string _pds_rec_filler;
        public string pds_rec_filler
        {
            get => _pds_rec_filler;
            set => Set(ref _pds_rec_filler, value);
        }

        private List<Pds_recd> _pds_recdList;
        [NotMapped]
        public List<Pds_recd> pds_recdList
        {
            get => _pds_recdList ?? (_pds_recdList = new List<Pds_recd>());
            set => Set(ref _pds_recdList, value);
        }

        private int? _pds_rec_send_dt_begin;
        [NotMapped]
        public int? pds_rec_send_dt_begin
        {
            get => _pds_rec_send_dt_begin;
            set => Set(ref _pds_rec_send_dt_begin, value);
        }

        private int? _pds_rec_send_dt_end;
        [NotMapped]
        public int? pds_rec_send_dt_end
        {
            get => _pds_rec_send_dt_end;
            set => Set(ref _pds_rec_send_dt_end, value);
        }

        private string _pds_rec_send_dt_begin_fmt;
        [NotMapped]
        public string pds_rec_send_dt_begin_fmt
        {
            get => _pds_rec_send_dt_begin_fmt;
            set => Set(ref _pds_rec_send_dt_begin_fmt, value);
        }

        private string _pds_rec_send_dt_end_fmt;
        [NotMapped]
        public string pds_rec_send_dt_end_fmt
        {
            get => _pds_rec_send_dt_end_fmt;
            set => Set(ref _pds_rec_send_dt_end_fmt, value);
        }

        private string _pds_rec_send_dt_fmt;
        [NotMapped]
        public string pds_rec_send_dt_fmt
        {
            get => _pds_rec_send_dt_fmt;
            set => Set(ref _pds_rec_send_dt_fmt, value);
        }

        private Pds_recParam.EditMode _editMode;
        /// <summary>
        /// 編輯模式
        /// </summary>
        [NotMapped]
        public Pds_recParam.EditMode EditMode
        {
            get => _editMode;
            set =>  Set(ref _editMode, value);
        }

        private string _pds_rec_op_name;
        [NotMapped]
        public string pds_rec_op_name
        {
            get => _pds_rec_op_name;
            set => Set(ref _pds_rec_op_name, value);
        }

    }
}
