using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_mque")]
    public class Mi_mque : BaseModel<Mi_mque>
    {
        private int? _que_prt_dt;
        [Key]
        public int? que_prt_dt
        {
            get => _que_prt_dt;
            set => Set(ref _que_prt_dt, value);
        }

        private string _que_id;
        [Key]
        public string que_id
        {
            get => _que_id;
            set => Set(ref _que_id, value);
        }

        private short? _que_seq;
        [Key]
        public short? que_seq
        {
            get => _que_seq;
            set => Set(ref _que_seq, value);
        }

        private short? _que_pill_no;
        public short? que_pill_no
        {
            get => _que_pill_no;
            set => Set(ref _que_pill_no, value);
        }

        private string _que_data1;
        public string que_data1
        {
            get => _que_data1;
            set => Set(ref _que_data1, value);
        }

    }
}

