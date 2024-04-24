using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "rec_code")]
    public class Rec_code : BaseModel<Rec_code>
    {
        private string _rec_code_model;
        [Key]
        public string rec_code_model
        {
            get => _rec_code_model;
            set => Set(ref _rec_code_model, value);
        }

        private string _rec_code_group;
        [Key]
        public string rec_code_group
        {
            get => _rec_code_group;
            set => Set(ref _rec_code_group, value);
        }

        private string _rec_code_short;
        [Key]
        public string rec_code_short
        {
            get => _rec_code_short;
            set => Set(ref _rec_code_short, value);
        }

        private string _rec_code_ver;
        [Key]
        public string rec_code_ver
        {
            get => _rec_code_ver;
            set => Set(ref _rec_code_ver, value);
        }

        private string _rec_code_name;
        public string rec_code_name
        {
            get => _rec_code_name;
            set => Set(ref _rec_code_name, value);
        }

        private string _rec_code_text1;
        public string rec_code_text1
        {
            get => _rec_code_text1;
            set => Set(ref _rec_code_text1, value);
        }

        private string _rec_code_text2;
        public string rec_code_text2
        {
            get => _rec_code_text2;
            set => Set(ref _rec_code_text2, value);
        }

        private string _rec_code_text3;
        public string rec_code_text3
        {
            get => _rec_code_text3;
            set => Set(ref _rec_code_text3, value);
        }

        private int? _rec_code_gseq;
        public int? rec_code_gseq
        {
            get => _rec_code_gseq;
            set => Set(ref _rec_code_gseq, value);
        }

        private int? _rec_code_sseq;
        public int? rec_code_sseq
        {
            get => _rec_code_sseq;
            set => Set(ref _rec_code_sseq, value);
        }

        private string _rec_code_st;
        public string rec_code_st
        {
            get => _rec_code_st;
            set => Set(ref _rec_code_st, value);
        }

        private string _rec_code_md_man;
        public string rec_code_md_man
        {
            get => _rec_code_md_man;
            set => Set(ref _rec_code_md_man, value);
        }

        private string _rec_code_md_name;
        public string rec_code_md_name
        {
            get => _rec_code_md_name;
            set => Set(ref _rec_code_md_name, value);
        }

        private string _rec_code_md_pc;
        public string rec_code_md_pc
        {
            get => _rec_code_md_pc;
            set => Set(ref _rec_code_md_pc, value);
        }

        private string _rec_code_md_ver;
        public string rec_code_md_ver
        {
            get => _rec_code_md_ver;
            set => Set(ref _rec_code_md_ver, value);
        }

        private string _rec_code_md_dt;
        public string rec_code_md_dt
        {
            get => _rec_code_md_dt;
            set => Set(ref _rec_code_md_dt, value);
        }

        private string _rec_code_md_time;
        public string rec_code_md_time
        {
            get => _rec_code_md_time;
            set => Set(ref _rec_code_md_time, value);
        }

    }
}

