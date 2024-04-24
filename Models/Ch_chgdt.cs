using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_chgdt")]
    public class Ch_chgdt : BaseModel<Ch_chgdt>
    {
        private string _chgdt_no;
        [Key]
        public string chgdt_no
        {
            get => _chgdt_no;
            set => Set(ref _chgdt_no, value);
        }

        private int? _chgdt_pre_date;
        public int? chgdt_pre_date
        {
            get => _chgdt_pre_date;
            set => Set(ref _chgdt_pre_date, value);
        }

        private short? _chgdt_pre_time;
        public short? chgdt_pre_time
        {
            get => _chgdt_pre_time;
            set => Set(ref _chgdt_pre_time, value);
        }

        private string _chgdt_md_no;
        public string chgdt_md_no
        {
            get => _chgdt_md_no;
            set => Set(ref _chgdt_md_no, value);
        }

        private int? _chgdt_pat_no;
        public int? chgdt_pat_no
        {
            get => _chgdt_pat_no;
            set => Set(ref _chgdt_pat_no, value);
        }

        private string _chgdt_pat_name;
        public string chgdt_pat_name
        {
            get => _chgdt_pat_name;
            set => Set(ref _chgdt_pat_name, value);
        }

        private decimal? _chgdt_use_qty;
        public decimal? chgdt_use_qty
        {
            get => _chgdt_use_qty;
            set => Set(ref _chgdt_use_qty, value);
        }

        private decimal? _chgdt_remain_qty;
        public decimal? chgdt_remain_qty
        {
            get => _chgdt_remain_qty;
            set => Set(ref _chgdt_remain_qty, value);
        }

        private string _chgdt_unit;
        public string chgdt_unit
        {
            get => _chgdt_unit;
            set => Set(ref _chgdt_unit, value);
        }

        private string _chgdt_bed_no;
        public string chgdt_bed_no
        {
            get => _chgdt_bed_no;
            set => Set(ref _chgdt_bed_no, value);
        }

        private string _chgdt_barcode;
        public string chgdt_barcode
        {
            get => _chgdt_barcode;
            set => Set(ref _chgdt_barcode, value);
        }

        private string _chgdt_scrip_key;
        public string chgdt_scrip_key
        {
            get => _chgdt_scrip_key;
            set => Set(ref _chgdt_scrip_key, value);
        }

        private string _chgdt_filler;
        public string chgdt_filler
        {
            get => _chgdt_filler;
            set => Set(ref _chgdt_filler, value);
        }

    }
}

