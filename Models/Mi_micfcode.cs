using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_micfcode")]
    public class Mi_micfcode : BaseModel<Mi_micfcode>
    {
        private string _icfcode_code;
        [Key]
        public string icfcode_code
        {
            get => _icfcode_code;
            set => Set(ref _icfcode_code, value);
        }

        private int? _icfcode_prt_dt;
        public int? icfcode_prt_dt
        {
            get => _icfcode_prt_dt;
            set => Set(ref _icfcode_prt_dt, value);
        }

        private int? _icfcode_prt_ti;
        public int? icfcode_prt_ti
        {
            get => _icfcode_prt_ti;
            set => Set(ref _icfcode_prt_ti, value);
        }

        private string _icfcode_id;
        public string icfcode_id
        {
            get => _icfcode_id;
            set => Set(ref _icfcode_id, value);
        }

        private short? _icfcode_seq;
        public short? icfcode_seq
        {
            get => _icfcode_seq;
            set => Set(ref _icfcode_seq, value);
        }

        private short? _icfcode_pill_no;
        public short? icfcode_pill_no
        {
            get => _icfcode_pill_no;
            set => Set(ref _icfcode_pill_no, value);
        }

        private string _icfcode_ipd_no;
        public string icfcode_ipd_no
        {
            get => _icfcode_ipd_no;
            set => Set(ref _icfcode_ipd_no, value);
        }

        private string _icfcode_pat_no;
        public string icfcode_pat_no
        {
            get => _icfcode_pat_no;
            set => Set(ref _icfcode_pat_no, value);
        }

        private string _icfcode_bed;
        public string icfcode_bed
        {
            get => _icfcode_bed;
            set => Set(ref _icfcode_bed, value);
        }

        private string _icfcode_clinical;
        public string icfcode_clinical
        {
            get => _icfcode_clinical;
            set => Set(ref _icfcode_clinical, value);
        }

        private string _icfcode_odr_no;
        public string icfcode_odr_no
        {
            get => _icfcode_odr_no;
            set => Set(ref _icfcode_odr_no, value);
        }

        private short? _icfcode_fee_no;
        public short? icfcode_fee_no
        {
            get => _icfcode_fee_no;
            set => Set(ref _icfcode_fee_no, value);
        }

        private string _icfcode_fee_key;
        public string icfcode_fee_key
        {
            get => _icfcode_fee_key;
            set => Set(ref _icfcode_fee_key, value);
        }

        private string _icfcode_rx_way1;
        public string icfcode_rx_way1
        {
            get => _icfcode_rx_way1;
            set => Set(ref _icfcode_rx_way1, value);
        }

        private string _icfcode_rx_way2;
        public string icfcode_rx_way2
        {
            get => _icfcode_rx_way2;
            set => Set(ref _icfcode_rx_way2, value);
        }

        private string _icfcode_rx_uqty1;
        public string icfcode_rx_uqty1
        {
            get => _icfcode_rx_uqty1;
            set => Set(ref _icfcode_rx_uqty1, value);
        }

        private string _icfcode_rx_uqty2;
        public string icfcode_rx_uqty2
        {
            get => _icfcode_rx_uqty2;
            set => Set(ref _icfcode_rx_uqty2, value);
        }

        private string _icfcode_rx_unit;
        public string icfcode_rx_unit
        {
            get => _icfcode_rx_unit;
            set => Set(ref _icfcode_rx_unit, value);
        }

        private string _icfcode_rx_qty1;
        public string icfcode_rx_qty1
        {
            get => _icfcode_rx_qty1;
            set => Set(ref _icfcode_rx_qty1, value);
        }

        private string _icfcode_rx_qty2;
        public string icfcode_rx_qty2
        {
            get => _icfcode_rx_qty2;
            set => Set(ref _icfcode_rx_qty2, value);
        }

        private int? _icfcode_beg_dt;
        public int? icfcode_beg_dt
        {
            get => _icfcode_beg_dt;
            set => Set(ref _icfcode_beg_dt, value);
        }

        private int? _icfcode_end_dt;
        public int? icfcode_end_dt
        {
            get => _icfcode_end_dt;
            set => Set(ref _icfcode_end_dt, value);
        }

        private string _icfcode_pha_unit;
        public string icfcode_pha_unit
        {
            get => _icfcode_pha_unit;
            set => Set(ref _icfcode_pha_unit, value);
        }

        private string _icfcode_pack;
        public string icfcode_pack
        {
            get => _icfcode_pack;
            set => Set(ref _icfcode_pack, value);
        }

        private string _icfcode_med_type;
        public string icfcode_med_type
        {
            get => _icfcode_med_type;
            set => Set(ref _icfcode_med_type, value);
        }

        private string _icfcode_lst_type;
        public string icfcode_lst_type
        {
            get => _icfcode_lst_type;
            set => Set(ref _icfcode_lst_type, value);
        }

        private string _icfcode_secretary;
        public string icfcode_secretary
        {
            get => _icfcode_secretary;
            set => Set(ref _icfcode_secretary, value);
        }

        private string _icfcode_st;
        public string icfcode_st
        {
            get => _icfcode_st;
            set => Set(ref _icfcode_st, value);
        }

        private string _icfcode_cd_barcode;
        public string icfcode_cd_barcode
        {
            get => _icfcode_cd_barcode;
            set => Set(ref _icfcode_cd_barcode, value);
        }

        private string _icfcode_filler;
        public string icfcode_filler
        {
            get => _icfcode_filler;
            set => Set(ref _icfcode_filler, value);
        }

    }
}

