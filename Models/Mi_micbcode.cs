using Params;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_micbcode")]
    public class Mi_micbcode : BaseModel<Mi_micbcode>
    {
        private string _icbcode_code;
        [Key]
        public string icbcode_code
        {
            get => _icbcode_code;
            set => Set(ref _icbcode_code, value);
        }

        private int? _icbcode_send_dt;
        public int? icbcode_send_dt
        {
            get => _icbcode_send_dt;
            set => Set(ref _icbcode_send_dt, value);
        }

        private string _icbcode_ipd_no;
        public string icbcode_ipd_no
        {
            get => _icbcode_ipd_no;
            set => Set(ref _icbcode_ipd_no, value);
        }

        private string _icbcode_pat_no;
        public string icbcode_pat_no
        {
            get => _icbcode_pat_no;
            set => Set(ref _icbcode_pat_no, value);
        }

        private string _icbcode_fee_key;
        public string icbcode_fee_key
        {
            get => _icbcode_fee_key;
            set => Set(ref _icbcode_fee_key, value);
        }

        private string _icbcode_rx_way1;
        public string icbcode_rx_way1
        {
            get => _icbcode_rx_way1;
            set => Set(ref _icbcode_rx_way1, value);
        }

        private string _icbcode_rx_way2;
        public string icbcode_rx_way2
        {
            get => _icbcode_rx_way2;
            set => Set(ref _icbcode_rx_way2, value);
        }

        private string _icbcode_rx_uqty1;
        public string icbcode_rx_uqty1
        {
            get => _icbcode_rx_uqty1;
            set => Set(ref _icbcode_rx_uqty1, value);
        }

        private string _icbcode_rx_uqty2;
        public string icbcode_rx_uqty2
        {
            get => _icbcode_rx_uqty2;
            set => Set(ref _icbcode_rx_uqty2, value);
        }

        private string _icbcode_rx_unit;
        public string icbcode_rx_unit
        {
            get => _icbcode_rx_unit;
            set => Set(ref _icbcode_rx_unit, value);
        }

        private string _icbcode_rx_qty1;
        public string icbcode_rx_qty1
        {
            get => _icbcode_rx_qty1;
            set => Set(ref _icbcode_rx_qty1, value);
        }

        private string _icbcode_rx_qty2;
        public string icbcode_rx_qty2
        {
            get => _icbcode_rx_qty2;
            set => Set(ref _icbcode_rx_qty2, value);
        }

        private int? _icbcode_beg_dt;
        public int? icbcode_beg_dt
        {
            get => _icbcode_beg_dt;
            set => Set(ref _icbcode_beg_dt, value);
        }

        private int? _icbcode_end_dt;
        public int? icbcode_end_dt
        {
            get => _icbcode_end_dt;
            set => Set(ref _icbcode_end_dt, value);
        }

        private string _icbcode_pha_unit;
        public string icbcode_pha_unit
        {
            get => _icbcode_pha_unit;
            set => Set(ref _icbcode_pha_unit, value);
        }

        private string _icbcode_pack;
        /// <summary>
        /// 加總包數(磨粉分包)
        /// </summary>
        public string icbcode_pack
        {
            get => _icbcode_pack;
            set => Set(ref _icbcode_pack, value);
        }

        private string _icbcode_med_type;
        /// <summary>
        /// 藥袋藥品類型
        /// </summary>
        public string icbcode_med_type
        {
            get => _icbcode_med_type;
            set
            {
                Set(ref _icbcode_med_type, value);
                icbcode_med_type_p = (_icbcode_med_type == Mi_micbcodeParam.Med_type.P ||
                    _icbcode_med_type == Mi_micbcodeParam.Med_type.FOURSP) ? true : false;
            }
        }

        private string _icbcode_st;
        public string icbcode_st
        {
            get => _icbcode_st;
            set => Set(ref _icbcode_st, value);
        }

        private string _icbcode_cd_barcode;
        public string icbcode_cd_barcode
        {
            get => _icbcode_cd_barcode;
            set => Set(ref _icbcode_cd_barcode, value);
        }

        private string _icbcode_filler;
        public string icbcode_filler
        {
            get => _icbcode_filler;
            set => Set(ref _icbcode_filler, value);
        }

        private bool _icbcode_med_type_p;
        /// <summary>
        /// 藥袋藥品類型：磨粉分包及4級管制藥(磨粉分包)
        /// </summary>
        [NotMapped]
        public bool icbcode_med_type_p
        {
            get => _icbcode_med_type_p;
            set => Set(ref _icbcode_med_type_p, value);
        }

        private string _icbcode_rx_uqty;
        /// <summary>
        /// 次劑量
        /// </summary>
        [NotMapped]
        public string icbcode_rx_uqty
        {
            get => _icbcode_rx_uqty;
            set => Set(ref _icbcode_rx_uqty, value);
        }

        private bool _icbcode_rx_uqtyEnabled;
        /// <summary>
        /// 次劑量：Enabled
        /// </summary>
        [NotMapped]
        public bool icbcode_rx_uqtyEnabled
        {
            get => _icbcode_rx_uqtyEnabled;
            set => Set(ref _icbcode_rx_uqtyEnabled, value);
        }

        private string _icbcode_rx_qty;
        /// <summary>
        /// 加總總量
        /// </summary>
        [NotMapped]
        public string icbcode_rx_qty
        {
            get => _icbcode_rx_qty;
            set => Set(ref _icbcode_rx_qty, value);
        }

        private Visibility _icbcode_rx_qtyVisibility = Visibility.Collapsed;
        /// <summary>
        /// 加總總量：Visibility
        /// </summary>
        [NotMapped]
        public Visibility icbcode_rx_qtyVisibility
        {
            get => _icbcode_rx_qtyVisibility;
            set => Set(ref _icbcode_rx_qtyVisibility, value);
        }

        private Visibility _icbcode_packVisibility = Visibility.Collapsed;
        /// <summary>
        /// 加總包數(磨粉分包)：Visibility
        /// </summary>
        [NotMapped]
        public Visibility icbcode_packVisibility
        {
            get => _icbcode_packVisibility;
            set => Set(ref _icbcode_packVisibility, value);
        }

        private int? _icbcode_sort;
        /// <summary>
        /// 配藥單排序
        /// </summary>
        [NotMapped]
        public int? icbcode_sort
        {
            get => _icbcode_sort;
            set => Set(ref _icbcode_sort, value);
        }

        private string _icbcode_udl_seq;
        /// <summary>
        /// 醫囑序號
        /// </summary>
        [NotMapped]
        public string icbcode_udl_seq
        {
            get => _icbcode_udl_seq;
            set => Set(ref _icbcode_udl_seq, value);
        }

        #region mi_micfcode

        private int? _icfcode_prt_dt;
        [NotMapped]
        public int? icfcode_prt_dt
        {
            get => _icfcode_prt_dt;
            set => Set(ref _icfcode_prt_dt, value);
        }

        private int? _icfcode_prt_ti;
        [NotMapped]
        public int? icfcode_prt_ti
        {
            get => _icfcode_prt_ti;
            set => Set(ref _icfcode_prt_ti, value);
        }

        private string _icfcode_id;
        [NotMapped]
        public string icfcode_id
        {
            get => _icfcode_id;
            set => Set(ref _icfcode_id, value);
        }

        private short? _icfcode_seq;
        [NotMapped]
        public short? icfcode_seq
        {
            get => _icfcode_seq;
            set => Set(ref _icfcode_seq, value);
        }

        private short? _icfcode_pill_no;
        [NotMapped]
        public short? icfcode_pill_no
        {
            get => _icfcode_pill_no;
            set => Set(ref _icfcode_pill_no, value);
        }

        private string _icfcode_bed;
        [NotMapped]
        public string icfcode_bed
        {
            get => _icfcode_bed;
            set => Set(ref _icfcode_bed, value);
        }

        private string _icfcode_clinical;
        [NotMapped]
        public string icfcode_clinical
        {
            get => _icfcode_clinical;
            set => Set(ref _icfcode_clinical, value);
        }

        private string _icfcode_odr_no;
        [NotMapped]
        public string icfcode_odr_no
        {
            get => _icfcode_odr_no;
            set => Set(ref _icfcode_odr_no, value);
        }

        private short? _icfcode_fee_no;
        [NotMapped]
        public short? icfcode_fee_no
        {
            get => _icfcode_fee_no;
            set => Set(ref _icfcode_fee_no, value);
        }

        private string _icfcode_lst_type;
        [NotMapped]
        public string icfcode_lst_type
        {
            get => _icfcode_lst_type;
            set => Set(ref _icfcode_lst_type, value);
        }

        private string _icfcode_secretary;
        [NotMapped]
        public string icfcode_secretary
        {
            get => _icfcode_secretary;
            set => Set(ref _icfcode_secretary, value);
        }

        #endregion

        private string _pds_rec_st;
        [NotMapped]
        public string pds_rec_st
        {
            get => _pds_rec_st;
            set => Set(ref _pds_rec_st, value);
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

        #region ch_prs

        private string _chprs_id_name;
        [NotMapped]
        public string chprs_id_name
        {
            get => _chprs_id_name;
            set => Set(ref _chprs_id_name, value);
        }

        private string _chprs_way_id;
        /// <summary>
        /// 藥品途徑識別 O:口服, E:外用, I:注射
        /// </summary>
        [NotMapped]
        public string chprs_way_id
        {
            get => _chprs_way_id;
            set => Set(ref _chprs_way_id, value);
        }

        private string _chprs_typ_id;
        /// <summary>
        /// 藥品劑型識別 1:水劑, 2:粉劑, 3:錠劑, 4:煎劑, 5:塞劑, 6:膏劑, 7:噴劑
        /// </summary>
        [NotMapped]
        public string chprs_typ_id
        {
            get => _chprs_typ_id;
            set => Set(ref _chprs_typ_id, value);
        }

        private string _atc_code_prefix5;
        /// <summary>
        /// ATC CODE 前五碼群組
        /// </summary>
        [NotMapped]
        public string atc_code_prefix5
        {
            get => _atc_code_prefix5;
            set => Set(ref _atc_code_prefix5, value);
        }

        private string _atc_code_prefix5_color;
        /// <summary>
        /// ATC CODE 前五碼群組顏色
        /// </summary>
        [NotMapped]
        public string atc_code_prefix5_color
        {
            get => _atc_code_prefix5_color;
            set => Set(ref _atc_code_prefix5_color, value);
        }

        private string _chprs_orig_rehrig;
        /// <summary>
        /// 原包裝儲存方式：冷藏：Y
        /// </summary>
        [NotMapped]
        public string chprs_orig_rehrig
        {
            get => _chprs_orig_rehrig;
            set => Set(ref _chprs_orig_rehrig, value);
        }

        #endregion

        #region ch_cdr

        private int? _cdr_pre_date;
        /// <summary>
        /// 管制藥調劑日期
        /// </summary>
        [NotMapped]
        public int? cdr_pre_date
        {
            get => _cdr_pre_date;
            set => Set(ref _cdr_pre_date, value);
        }

        private short? _cdr_pre_time;
        /// <summary>
        /// 管制藥調劑時間
        /// </summary>
        [NotMapped]
        public short? cdr_pre_time
        {
            get => _cdr_pre_time;
            set => Set(ref _cdr_pre_time, value);
        }

        private string _cdr_pre_usr;
        /// <summary>
        /// 管制藥調劑人員
        /// </summary>
        [NotMapped]
        public string cdr_pre_usr
        {
            get => _cdr_pre_usr;
            set => Set(ref _cdr_pre_usr, value);
        }

        #endregion

    }
}
