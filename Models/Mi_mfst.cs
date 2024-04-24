using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_mfst")]
    public class Mi_mfst : BaseModel<Mi_mfst>
    {
        private int? _fst_prt_dt;
        [Key]
        public int? fst_prt_dt
        {
            get => _fst_prt_dt;
            set => Set(ref _fst_prt_dt, value);
        }

        private string _fst_id;
        [Key]
        public string fst_id
        {
            get => _fst_id;
            set => Set(ref _fst_id, value);
        }

        private short? _fst_seq;
        [Key]
        public short? fst_seq
        {
            get => _fst_seq;
            set => Set(ref _fst_seq, value);
        }

        private short? _fst_pill_no;
        [Key]
        public short? fst_pill_no
        {
            get => _fst_pill_no;
            set => Set(ref _fst_pill_no, value);
        }

        private int? _fst_ins_date;
        [Key]
        public int? fst_ins_date
        {
            get => _fst_ins_date;
            set => Set(ref _fst_ins_date, value);
        }

        private short? _fst_odr_seq;
        [Key]
        public short? fst_odr_seq
        {
            get => _fst_odr_seq;
            set => Set(ref _fst_odr_seq, value);
        }

        private string _fst_filler;
        public string fst_filler
        {
            get => _fst_filler;
            set => Set(ref _fst_filler, value);
        }

    }
}

