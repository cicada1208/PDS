using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_pre")]
    public class Ch_pre : BaseModel<Ch_pre>
    {
        private string _chpre_sys_id;
        [Key]
        public string chpre_sys_id
        {
            get => _chpre_sys_id;
            set => Set(ref _chpre_sys_id, value);
        }

        private decimal? _chpre_ipd_no;
        [Key]
        public decimal? chpre_ipd_no
        {
            get => _chpre_ipd_no;
            set => Set(ref _chpre_ipd_no, value);
        }

        private short? _chpre_ipd_seq;
        [Key]
        public short? chpre_ipd_seq
        {
            get => _chpre_ipd_seq;
            set => Set(ref _chpre_ipd_seq, value);
        }

        private string _chpre_filler1;
        [Key]
        public string chpre_filler1
        {
            get => _chpre_filler1;
            set => Set(ref _chpre_filler1, value);
        }

        private string _chpre_item;
        [Key]
        public string chpre_item
        {
            get => _chpre_item;
            set => Set(ref _chpre_item, value);
        }

        private string _chpre_type;
        [Key]
        public string chpre_type
        {
            get => _chpre_type;
            set => Set(ref _chpre_type, value);
        }

        private string _chpre_id;
        [Key]
        public string chpre_id
        {
            get => _chpre_id;
            set => Set(ref _chpre_id, value);
        }

        private string _chpre_value1;
        public string chpre_value1
        {
            get => _chpre_value1;
            set => Set(ref _chpre_value1, value);
        }

        private string _chpre_value2;
        public string chpre_value2
        {
            get => _chpre_value2;
            set => Set(ref _chpre_value2, value);
        }

        private string _chpre_rec;
        public string chpre_rec
        {
            get => _chpre_rec;
            set => Set(ref _chpre_rec, value);
        }

        private string _chpre_note;
        public string chpre_note
        {
            get => _chpre_note;
            set => Set(ref _chpre_note, value);
        }

        private string _chpre_filler2;
        public string chpre_filler2
        {
            get => _chpre_filler2;
            set => Set(ref _chpre_filler2, value);
        }

    }
}
