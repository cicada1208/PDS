using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB2, "ch_bcode")]
    public class Ch_bcode : BaseModel<Ch_bcode>
    {
        private int? _bcode_code_pat_no;
        [Key]
        public int? bcode_code_pat_no
        {
            get => _bcode_code_pat_no;
            set => Set(ref _bcode_code_pat_no, value);
        }

        private int? _bcode_code_rx_dt;
        [Key]
        public int? bcode_code_rx_dt
        {
            get => _bcode_code_rx_dt;
            set => Set(ref _bcode_code_rx_dt, value);
        }

        private short? _bcode_code_rx_hh;
        [Key]
        public short? bcode_code_rx_hh
        {
            get => _bcode_code_rx_hh;
            set => Set(ref _bcode_code_rx_hh, value);
        }

        private short? _bcode_code_page;
        [Key]
        public short? bcode_code_page
        {
            get => _bcode_code_page;
            set => Set(ref _bcode_code_page, value);
        }

        private string _bcode_ipd_no;
        [Key]
        public string bcode_ipd_no
        {
            get => _bcode_ipd_no;
            set => Set(ref _bcode_ipd_no, value);
        }

        private string _bcode_fee_key;
        [Key]
        public string bcode_fee_key
        {
            get => _bcode_fee_key;
            set => Set(ref _bcode_fee_key, value);
        }

        private string _bcode_rx_way1;
        [Key]
        public string bcode_rx_way1
        {
            get => _bcode_rx_way1;
            set => Set(ref _bcode_rx_way1, value);
        }

        private string _bcode_send_dt;
        [Key]
        public string bcode_send_dt
        {
            get => _bcode_send_dt;
            set => Set(ref _bcode_send_dt, value);
        }

        private string _bcode_bed;
        public string bcode_bed
        {
            get => _bcode_bed;
            set => Set(ref _bcode_bed, value);
        }

        private string _bcode_pat_no;
        public string bcode_pat_no
        {
            get => _bcode_pat_no;
            set => Set(ref _bcode_pat_no, value);
        }

        private string _bcode_pat_name;
        public string bcode_pat_name
        {
            get => _bcode_pat_name;
            set => Set(ref _bcode_pat_name, value);
        }

        private string _bcode_id_name2;
        public string bcode_id_name2
        {
            get => _bcode_id_name2;
            set => Set(ref _bcode_id_name2, value);
        }

        private string _bcode_rx_uqty;
        /// <summary>
        /// 次劑量
        /// </summary>
        public string bcode_rx_uqty
        {
            get => _bcode_rx_uqty;
            set => Set(ref _bcode_rx_uqty, value);
        }

        private string _bcode_rx_way2;
        public string bcode_rx_way2
        {
            get => _bcode_rx_way2;
            set => Set(ref _bcode_rx_way2, value);
        }

        private string _bcode_rx_days;
        public string bcode_rx_days
        {
            get => _bcode_rx_days;
            set => Set(ref _bcode_rx_days, value);
        }

        private string _bcode_rx_qty;
        /// <summary>
        /// 總量
        /// </summary>
        public string bcode_rx_qty
        {
            get => _bcode_rx_qty;
            set => Set(ref _bcode_rx_qty, value);
        }

        private string _bcode_rx_dt;
        public string bcode_rx_dt
        {
            get => _bcode_rx_dt;
            set => Set(ref _bcode_rx_dt, value);
        }

        private string _bcode_rx_hh;
        public string bcode_rx_hh
        {
            get => _bcode_rx_hh;
            set => Set(ref _bcode_rx_hh, value);
        }

        private string _bcode_style;
        public string bcode_style
        {
            get => _bcode_style;
            set => Set(ref _bcode_style, value);
        }

        private string _bcode_cname;
        public string bcode_cname
        {
            get => _bcode_cname;
            set => Set(ref _bcode_cname, value);
        }

        private string _bcode_rx_unit;
        public string bcode_rx_unit
        {
            get => _bcode_rx_unit;
            set => Set(ref _bcode_rx_unit, value);
        }

        private string _bcode_pha_unit;
        public string bcode_pha_unit
        {
            get => _bcode_pha_unit;
            set => Set(ref _bcode_pha_unit, value);
        }

        private string _bcode_way_id;
        public string bcode_way_id
        {
            get => _bcode_way_id;
            set => Set(ref _bcode_way_id, value);
        }

        private string _bcode_menu;
        public string bcode_menu
        {
            get => _bcode_menu;
            set => Set(ref _bcode_menu, value);
        }

        private string _bcode_powder1;
        public string bcode_powder1
        {
            get => _bcode_powder1;
            set => Set(ref _bcode_powder1, value);
        }

        private string _bcode_powder2;
        public string bcode_powder2
        {
            get => _bcode_powder2;
            set => Set(ref _bcode_powder2, value);
        }

        private string _bcode_chg_flag;
        public string bcode_chg_flag
        {
            get => _bcode_chg_flag;
            set => Set(ref _bcode_chg_flag, value);
        }

        private string _bcode_asu_pay;
        public string bcode_asu_pay
        {
            get => _bcode_asu_pay;
            set => Set(ref _bcode_asu_pay, value);
        }

        private string _bcode_memo;
        public string bcode_memo
        {
            get => _bcode_memo;
            set => Set(ref _bcode_memo, value);
        }

        private string _bcode_apy_dr;
        public string bcode_apy_dr
        {
            get => _bcode_apy_dr;
            set => Set(ref _bcode_apy_dr, value);
        }

        private int? _bcode_beg_dt;
        public int? bcode_beg_dt
        {
            get => _bcode_beg_dt;
            set => Set(ref _bcode_beg_dt, value);
        }

        private short? _bcode_beg_ti;
        public short? bcode_beg_ti
        {
            get => _bcode_beg_ti;
            set => Set(ref _bcode_beg_ti, value);
        }

        private int? _bcode_end_dt;
        public int? bcode_end_dt
        {
            get => _bcode_end_dt;
            set => Set(ref _bcode_end_dt, value);
        }

        private short? _bcode_end_ti;
        public short? bcode_end_ti
        {
            get => _bcode_end_ti;
            set => Set(ref _bcode_end_ti, value);
        }

        private decimal? _A4GLIdentity;
        public decimal? A4GLIdentity
        {
            get => _A4GLIdentity;
            set => Set(ref _A4GLIdentity, value);
        }

        private string _bcode_code;
        [NotMapped]
        public string bcode_code
        {
            get => _bcode_code;
            set => Set(ref _bcode_code, value);
        }

        private string _day_group;
        /// <summary>
        /// 第幾天用藥分組
        /// </summary>
        [NotMapped]
        public string day_group
        {
            get => _day_group;
            set => Set(ref _day_group, value);
        }

        private string _order_info;
        /// <summary>
        /// 處方資訊
        /// </summary>
        [NotMapped]
        public string order_info
        {
            get => _order_info;
            set => Set(ref _order_info, value);
        }

        private string _ni_pic_url;
        /// <summary>
        /// 裸碇藥品圖片路徑
        /// </summary>
        [NotMapped]
        public string ni_pic_url
        {
            get => _ni_pic_url;
            set => Set(ref _ni_pic_url, value);
        }

    }
}
