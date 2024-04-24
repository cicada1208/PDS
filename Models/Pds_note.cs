using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "pds_note")]
    public class Pds_note : BaseModel<Pds_note>
    {
        private string _pds_note_no;
        [Key]
        public string pds_note_no
        {
            get => _pds_note_no;
            set => Set(ref _pds_note_no, value);
        }

        private string _pds_note_dtm;
        public string pds_note_dtm
        {
            get => _pds_note_dtm;
            set => Set(ref _pds_note_dtm, value);
        }

        private string _pds_note_type;
        [Display(Name = "通報類別")]
        public string pds_note_type
        {
            get => _pds_note_type;
            set => Set(ref _pds_note_type, value);
        }

        private string _pds_note_op;
        public string pds_note_op
        {
            get => _pds_note_op;
            set => Set(ref _pds_note_op, value);
        }

        private string _pds_note_bag_code;
        public string pds_note_bag_code
        {
            get => _pds_note_bag_code;
            set => Set(ref _pds_note_bag_code, value);
        }

        private string _pds_note_lst_code;
        public string pds_note_lst_code
        {
            get => _pds_note_lst_code;
            set => Set(ref _pds_note_lst_code, value);
        }

        private int? _pds_note_send_dt;
        public int? pds_note_send_dt
        {
            get => _pds_note_send_dt;
            set => Set(ref _pds_note_send_dt, value);
        }

        private string _pds_note_ipd_no;
        public string pds_note_ipd_no
        {
            get => _pds_note_ipd_no;
            set => Set(ref _pds_note_ipd_no, value);
        }

        private string _pds_note_pat_no;
        public string pds_note_pat_no
        {
            get => _pds_note_pat_no;
            set => Set(ref _pds_note_pat_no, value);
        }

        private string _pds_note_bed;
        public string pds_note_bed
        {
            get => _pds_note_bed;
            set => Set(ref _pds_note_bed, value);
        }

        private string _pds_note_clinical;
        public string pds_note_clinical
        {
            get => _pds_note_clinical;
            set => Set(ref _pds_note_clinical, value);
        }

        private string _pds_note_note;
        public string pds_note_note
        {
            get => _pds_note_note;
            set => Set(ref _pds_note_note, value);
        }

        private string _pds_note_st;
        public string pds_note_st
        {
            get => _pds_note_st;
            set => Set(ref _pds_note_st, value);
        }

        private string _pds_note_md_man;
        public string pds_note_md_man
        {
            get => _pds_note_md_man;
            set => Set(ref _pds_note_md_man, value);
        }

        private string _pds_note_md_name;
        public string pds_note_md_name
        {
            get => _pds_note_md_name;
            set => Set(ref _pds_note_md_name, value);
        }

        private string _pds_note_md_pc;
        public string pds_note_md_pc
        {
            get => _pds_note_md_pc;
            set => Set(ref _pds_note_md_pc, value);
        }

        private string _pds_note_md_ver;
        public string pds_note_md_ver
        {
            get => _pds_note_md_ver;
            set => Set(ref _pds_note_md_ver, value);
        }

        private string _pds_note_md_dt;
        public string pds_note_md_dt
        {
            get => _pds_note_md_dt;
            set => Set(ref _pds_note_md_dt, value);
        }

        private string _pds_note_md_time;
        public string pds_note_md_time
        {
            get => _pds_note_md_time;
            set => Set(ref _pds_note_md_time, value);
        }

        private string _pds_note_filler;
        public string pds_note_filler
        {
            get => _pds_note_filler;
            set => Set(ref _pds_note_filler, value);
        }

        private Pds_noteParam.EditMode _editMode;
        /// <summary>
        /// 編輯模式
        /// </summary>
        [NotMapped]
        public Pds_noteParam.EditMode EditMode
        {
            get => _editMode;
            set => Set(ref _editMode, value);
        }

    }
}
