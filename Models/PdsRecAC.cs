using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class PdsRecAC : BaseModel<PdsRecAC>
    {
        #region a

        private string _pds_rec_no;
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
        [Display(Name = "調劑開始")]
        public string pds_rec_op_dtm_begin
        {
            get => _pds_rec_op_dtm_begin;
            set => Set(ref _pds_rec_op_dtm_begin, value);
        }

        private string _pds_rec_op_dtm_end;
        [Display(Name = "調劑結束")]
        public string pds_rec_op_dtm_end
        {
            get => _pds_rec_op_dtm_end;
            set => Set(ref _pds_rec_op_dtm_end, value);
        }

        private string _pds_rec_bag_code;
        [Display(Name = "藥袋條碼")]
        public string pds_rec_bag_code
        {
            get => _pds_rec_bag_code;
            set => Set(ref _pds_rec_bag_code, value);
        }

        private string _pds_rec_lst_code;
        [Display(Name = "配藥單條碼")]
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
        [Display(Name = "病歷號")]
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
        [Display(Name = "護理站")]
        public string pds_rec_clinical
        {
            get => _pds_rec_clinical;
            set => Set(ref _pds_rec_clinical, value);
        }

        private string _pds_rec_st;
        [Display(Name = "調劑狀態")]
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
        public string pds_rec_nondeliver
        {
            get => _pds_rec_nondeliver;
            set => Set(ref _pds_rec_nondeliver, value);
        }

        private string _pds_rec_note;
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
        [Display(Name = "調劑藥師")]
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

        private string _pds_rec_op_dtm_begin_begin;
        public string pds_rec_op_dtm_begin_begin
        {
            get => _pds_rec_op_dtm_begin_begin;
            set => Set(ref _pds_rec_op_dtm_begin_begin, value);
        }

        private string _pds_rec_op_dtm_begin_end;
        public string pds_rec_op_dtm_begin_end
        {
            get => _pds_rec_op_dtm_begin_end;
            set => Set(ref _pds_rec_op_dtm_begin_end, value);
        }

        private string _pds_rec_op_dtm_diff;
        [Display(Name = "調劑時間")]
        public string pds_rec_op_dtm_diff
        {
            get => _pds_rec_op_dtm_diff;
            set => Set(ref _pds_rec_op_dtm_diff, value);
        }

        private string _pds_rec_send_dt_fmt;
        [Display(Name = "傳送日期")]
        public string pds_rec_send_dt_fmt
        {
            get => _pds_rec_send_dt_fmt;
            set => Set(ref _pds_rec_send_dt_fmt, value);
        }

        private string _pds_rec_op_name;
        [Display(Name = "作業")]
        public string pds_rec_op_name
        {
            get => _pds_rec_op_name;
            set => Set(ref _pds_rec_op_name, value);
        }

        private string _pds_rec_st_name;
        [Display(Name = "調劑狀態")]
        public string pds_rec_st_name
        {
            get => _pds_rec_st_name;
            set => Set(ref _pds_rec_st_name, value);
        }

        #endregion

        #region c

        private string _c_pds_rec_no;
        public string c_pds_rec_no
        {
            get => _c_pds_rec_no;
            set => Set(ref _c_pds_rec_no, value);
        }

        private string _c_pds_rec_op_type;
        public string c_pds_rec_op_type
        {
            get => _c_pds_rec_op_type;
            set => Set(ref _c_pds_rec_op_type, value);
        }

        private string _c_pds_rec_op_dtm_begin;
        [Display(Name = "核對開始")]
        public string c_pds_rec_op_dtm_begin
        {
            get => _c_pds_rec_op_dtm_begin;
            set => Set(ref _c_pds_rec_op_dtm_begin, value);
        }

        private string _c_pds_rec_op_dtm_end;
        [Display(Name = "核對結束")]
        public string c_pds_rec_op_dtm_end
        {
            get => _c_pds_rec_op_dtm_end;
            set => Set(ref _c_pds_rec_op_dtm_end, value);
        }

        private string _c_pds_rec_bag_code;
        public string c_pds_rec_bag_code
        {
            get => _c_pds_rec_bag_code;
            set => Set(ref _c_pds_rec_bag_code, value);
        }

        private string _c_pds_rec_lst_code;
        public string c_pds_rec_lst_code
        {
            get => _c_pds_rec_lst_code;
            set => Set(ref _c_pds_rec_lst_code, value);
        }

        private int? _c_pds_rec_send_dt;
        public int? c_pds_rec_send_dt
        {
            get => _c_pds_rec_send_dt;
            set => Set(ref _c_pds_rec_send_dt, value);
        }

        private string _c_pds_rec_ipd_no;
        public string c_pds_rec_ipd_no
        {
            get => _c_pds_rec_ipd_no;
            set => Set(ref _c_pds_rec_ipd_no, value);
        }

        private string _c_pds_rec_pat_no;
        public string c_pds_rec_pat_no
        {
            get => _c_pds_rec_pat_no;
            set => Set(ref _c_pds_rec_pat_no, value);
        }

        private string _c_pds_rec_bed;
        [Display(Name = "床號")]
        public string c_pds_rec_bed
        {
            get => _c_pds_rec_bed;
            set => Set(ref _c_pds_rec_bed, value);
        }

        private string _c_pds_rec_clinical;
        [Display(Name = "護理站")]
        public string c_pds_rec_clinical
        {
            get => _c_pds_rec_clinical;
            set => Set(ref _c_pds_rec_clinical, value);
        }

        private string _c_pds_rec_st;
        [Display(Name = "核對狀態")]
        public string c_pds_rec_st
        {
            get => _c_pds_rec_st;
            set => Set(ref _c_pds_rec_st, value);
        }

        private string _c_pds_rec_reason;
        public string c_pds_rec_reason
        {
            get => _c_pds_rec_reason;
            set => Set(ref _c_pds_rec_reason, value);
        }

        private string _c_pds_rec_reason_oth;
        public string c_pds_rec_reason_oth
        {
            get => _c_pds_rec_reason_oth;
            set => Set(ref _c_pds_rec_reason_oth, value);
        }

        private string _c_pds_rec_nondeliver;
        public string c_pds_rec_nondeliver
        {
            get => _c_pds_rec_nondeliver;
            set => Set(ref _c_pds_rec_nondeliver, value);
        }

        private string _c_pds_rec_note;
        public string c_pds_rec_note
        {
            get => _c_pds_rec_note;
            set => Set(ref _c_pds_rec_note, value);
        }

        private string _c_pds_rec_md_qty;
        public string c_pds_rec_md_qty
        {
            get => _c_pds_rec_md_qty;
            set => Set(ref _c_pds_rec_md_qty, value);
        }

        private string _c_pds_rec_md_way1;
        public string c_pds_rec_md_way1
        {
            get => _c_pds_rec_md_way1;
            set => Set(ref _c_pds_rec_md_way1, value);
        }

        private string _c_pds_rec_md_man;
        public string c_pds_rec_md_man
        {
            get => _c_pds_rec_md_man;
            set => Set(ref _c_pds_rec_md_man, value);
        }

        private string _c_pds_rec_md_name;
        [Display(Name = "核對藥師")]
        public string c_pds_rec_md_name
        {
            get => _c_pds_rec_md_name;
            set => Set(ref _c_pds_rec_md_name, value);
        }

        private string _c_pds_rec_md_pc;
        public string c_pds_rec_md_pc
        {
            get => _c_pds_rec_md_pc;
            set => Set(ref _c_pds_rec_md_pc, value);
        }

        private string _c_pds_rec_md_ver;
        public string c_pds_rec_md_ver
        {
            get => _c_pds_rec_md_ver;
            set => Set(ref _c_pds_rec_md_ver, value);
        }

        private string _c_pds_rec_md_dt;
        public string c_pds_rec_md_dt
        {
            get => _c_pds_rec_md_dt;
            set => Set(ref _c_pds_rec_md_dt, value);
        }

        private string _c_pds_rec_md_time;
        public string c_pds_rec_md_time
        {
            get => _c_pds_rec_md_time;
            set => Set(ref _c_pds_rec_md_time, value);
        }

        private string _c_pds_rec_filler;
        public string c_pds_rec_filler
        {
            get => _c_pds_rec_filler;
            set => Set(ref _c_pds_rec_filler, value);
        }

        private string _c_pds_rec_op_dtm_diff;
        [Display(Name = "核對時間")]
        public string c_pds_rec_op_dtm_diff
        {
            get => _c_pds_rec_op_dtm_diff;
            set => Set(ref _c_pds_rec_op_dtm_diff, value);
        }

        private string _c_pds_rec_st_name;
        [Display(Name = "核對狀態")]
        public string c_pds_rec_st_name
        {
            get => _c_pds_rec_st_name;
            set => Set(ref _c_pds_rec_st_name, value);
        }

        #endregion

        #region r

        private string _r_pds_rec_no;
        public string r_pds_rec_no
        {
            get => _r_pds_rec_no;
            set => Set(ref _r_pds_rec_no, value);
        }

        private string _r_pds_rec_op_type;
        public string r_pds_rec_op_type
        {
            get => _r_pds_rec_op_type;
            set => Set(ref _r_pds_rec_op_type, value);
        }

        private string _r_pds_rec_op_dtm_begin;
        [Display(Name = "發藥開始")]
        public string r_pds_rec_op_dtm_begin
        {
            get => _r_pds_rec_op_dtm_begin;
            set => Set(ref _r_pds_rec_op_dtm_begin, value);
        }

        private string _r_pds_rec_op_dtm_end;
        [Display(Name = "發藥結束")]
        public string r_pds_rec_op_dtm_end
        {
            get => _r_pds_rec_op_dtm_end;
            set => Set(ref _r_pds_rec_op_dtm_end, value);
        }

        private string _r_pds_rec_bag_code;
        public string r_pds_rec_bag_code
        {
            get => _r_pds_rec_bag_code;
            set => Set(ref _r_pds_rec_bag_code, value);
        }

        private string _r_pds_rec_lst_code;
        public string r_pds_rec_lst_code
        {
            get => _r_pds_rec_lst_code;
            set => Set(ref _r_pds_rec_lst_code, value);
        }

        private int? _r_pds_rec_send_dt;
        public int? r_pds_rec_send_dt
        {
            get => _r_pds_rec_send_dt;
            set => Set(ref _r_pds_rec_send_dt, value);
        }

        private string _r_pds_rec_ipd_no;
        public string r_pds_rec_ipd_no
        {
            get => _r_pds_rec_ipd_no;
            set => Set(ref _r_pds_rec_ipd_no, value);
        }

        private string _r_pds_rec_pat_no;
        public string r_pds_rec_pat_no
        {
            get => _r_pds_rec_pat_no;
            set => Set(ref _r_pds_rec_pat_no, value);
        }

        private string _r_pds_rec_bed;
        [Display(Name = "床號")]
        public string r_pds_rec_bed
        {
            get => _r_pds_rec_bed;
            set => Set(ref _r_pds_rec_bed, value);
        }

        private string _r_pds_rec_clinical;
        [Display(Name = "護理站")]
        public string r_pds_rec_clinical
        {
            get => _r_pds_rec_clinical;
            set => Set(ref _r_pds_rec_clinical, value);
        }

        private string _r_pds_rec_st;
        [Display(Name = "發藥狀態")]
        public string r_pds_rec_st
        {
            get => _r_pds_rec_st;
            set => Set(ref _r_pds_rec_st, value);
        }

        private string _r_pds_rec_reason;
        public string r_pds_rec_reason
        {
            get => _r_pds_rec_reason;
            set => Set(ref _r_pds_rec_reason, value);
        }

        private string _r_pds_rec_reason_oth;
        public string r_pds_rec_reason_oth
        {
            get => _r_pds_rec_reason_oth;
            set => Set(ref _r_pds_rec_reason_oth, value);
        }

        private string _r_pds_rec_nondeliver;
        public string r_pds_rec_nondeliver
        {
            get => _r_pds_rec_nondeliver;
            set => Set(ref _r_pds_rec_nondeliver, value);
        }

        private string _r_pds_rec_note;
        public string r_pds_rec_note
        {
            get => _r_pds_rec_note;
            set => Set(ref _r_pds_rec_note, value);
        }

        private string _r_pds_rec_md_qty;
        public string r_pds_rec_md_qty
        {
            get => _r_pds_rec_md_qty;
            set => Set(ref _r_pds_rec_md_qty, value);
        }

        private string _r_pds_rec_md_way1;
        public string r_pds_rec_md_way1
        {
            get => _r_pds_rec_md_way1;
            set => Set(ref _r_pds_rec_md_way1, value);
        }

        private string _r_pds_rec_md_man;
        public string r_pds_rec_md_man
        {
            get => _r_pds_rec_md_man;
            set => Set(ref _r_pds_rec_md_man, value);
        }

        private string _r_pds_rec_md_name;
        [Display(Name = "發藥藥師")]
        public string r_pds_rec_md_name
        {
            get => _r_pds_rec_md_name;
            set => Set(ref _r_pds_rec_md_name, value);
        }

        private string _r_pds_rec_md_pc;
        public string r_pds_rec_md_pc
        {
            get => _r_pds_rec_md_pc;
            set => Set(ref _r_pds_rec_md_pc, value);
        }

        private string _r_pds_rec_md_ver;
        public string r_pds_rec_md_ver
        {
            get => _r_pds_rec_md_ver;
            set => Set(ref _r_pds_rec_md_ver, value);
        }

        private string _r_pds_rec_md_dt;
        public string r_pds_rec_md_dt
        {
            get => _r_pds_rec_md_dt;
            set => Set(ref _r_pds_rec_md_dt, value);
        }

        private string _r_pds_rec_md_time;
        public string r_pds_rec_md_time
        {
            get => _r_pds_rec_md_time;
            set => Set(ref _r_pds_rec_md_time, value);
        }

        private string _r_pds_rec_filler;
        public string r_pds_rec_filler
        {
            get => _r_pds_rec_filler;
            set => Set(ref _r_pds_rec_filler, value);
        }

        private string _r_pds_rec_op_dtm_diff;
        [Display(Name = "發藥時間")]
        public string r_pds_rec_op_dtm_diff
        {
            get => _r_pds_rec_op_dtm_diff;
            set => Set(ref _r_pds_rec_op_dtm_diff, value);
        }

        private string _r_pds_rec_st_name;
        [Display(Name = "發藥狀態")]
        public string r_pds_rec_st_name
        {
            get => _r_pds_rec_st_name;
            set => Set(ref _r_pds_rec_st_name, value);
        }

        #endregion

        private string _ar_pds_rec_op_dtm_diff;
        [Display(Name = "調劑開始至發藥結束")]
        public string ar_pds_rec_op_dtm_diff
        {
            get => _ar_pds_rec_op_dtm_diff;
            set => Set(ref _ar_pds_rec_op_dtm_diff, value);
        }

        private string _or_pds_rec_op_dtm_diff;
        [Display(Name = "處方開立至發藥結束")]
        public string or_pds_rec_op_dtm_diff
        {
            get => _or_pds_rec_op_dtm_diff;
            set => Set(ref _or_pds_rec_op_dtm_diff, value);
        }

        #region mi_micbcode

        private string _icbcode_code;
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
        [Display(Name = "藥品代碼")]
        public string icbcode_fee_key
        {
            get => _icbcode_fee_key;
            set => Set(ref _icbcode_fee_key, value);
        }

        private string _icbcode_rx_way1;
        [Display(Name = "頻次")]
        public string icbcode_rx_way1
        {
            get => _icbcode_rx_way1;
            set => Set(ref _icbcode_rx_way1, value);
        }

        private string _icbcode_rx_way2;
        [Display(Name = "途徑")]
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
        [Display(Name = "次劑量單位")]
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
        [Display(Name = "總量單位")]
        public string icbcode_pha_unit
        {
            get => _icbcode_pha_unit;
            set => Set(ref _icbcode_pha_unit, value);
        }

        private string _icbcode_pack;
        /// <summary>
        /// 加總包數(磨粉分包)
        /// </summary>
        [Display(Name = "總包")]
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
            set => Set(ref _icbcode_med_type, value);
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

        private string _icbcode_rx_uqty;
        /// <summary>
        /// 次劑量
        /// </summary>
        [Display(Name = "次劑量")]
        public string icbcode_rx_uqty
        {
            get => _icbcode_rx_uqty;
            set => Set(ref _icbcode_rx_uqty, value);
        }

        private string _icbcode_rx_qty;
        /// <summary>
        /// 加總總量
        /// </summary>
        [Display(Name = "總量")]
        public string icbcode_rx_qty
        {
            get => _icbcode_rx_qty;
            set => Set(ref _icbcode_rx_qty, value);
        }

        #endregion

        #region mi_micfcode

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

        private string _icfcode_bed;
        [Display(Name = "床號")]
        public string icfcode_bed
        {
            get => _icfcode_bed;
            set => Set(ref _icfcode_bed, value);
        }

        private string _icfcode_clinical;
        [Display(Name = "護理站")]
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

        private string _icfcode_prt_dt_fmt;
        [Display(Name = "列印日期")]
        public string icfcode_prt_dt_fmt
        {
            get => _icfcode_prt_dt_fmt;
            set => Set(ref _icfcode_prt_dt_fmt, value);
        }

        private string _icfcode_prt_dtm;
        [Display(Name = "列印時間")]
        public string icfcode_prt_dtm
        {
            get => _icfcode_prt_dtm;
            set => Set(ref _icfcode_prt_dtm, value);
        }

        private string _icfcode_ipill_no;
        /// <summary>
        /// 領藥號
        /// </summary>
        [Display(Name = "領藥號")]
        public string icfcode_ipill_no
        {
            get => _icfcode_ipill_no;
            set => Set(ref _icfcode_ipill_no, value);
        }

        #endregion

        private string _qty_pack_final;
        /// <summary>
        /// 實際給藥量
        /// </summary>
        [Display(Name = "實際給藥量")]
        public string qty_pack_final
        {
            get => _qty_pack_final;
            set => Set(ref _qty_pack_final, value);
        }

        private string _pat_name;
        [Display(Name = "姓名")]
        public string pat_name
        {
            get => _pat_name;
            set => Set(ref _pat_name, value);
        }

        #region ch_tra

        private int? _tra_odr_date;
        public int? tra_odr_date
        {
            get => _tra_odr_date;
            set => Set(ref _tra_odr_date, value);
        }

        private short? _tra_odr_time;
        public short? tra_odr_time
        {
            get => _tra_odr_time;
            set => Set(ref _tra_odr_time, value);
        }

        private string _tra_odr_dtm;
        [Display(Name = "處方時間")]
        public string tra_odr_dtm
        {
            get => _tra_odr_dtm;
            set => Set(ref _tra_odr_dtm, value);
        }

        private int? _tra_prn_date;
        public int? tra_prn_date
        {
            get => _tra_prn_date;
            set => Set(ref _tra_prn_date, value);
        }

        private short? _tra_prn_time;
        public short? tra_prn_time
        {
            get => _tra_prn_time;
            set => Set(ref _tra_prn_time, value);
        }

        private string _tra_prn_dtm;
        [Display(Name = "列印時間")]
        public string tra_prn_dtm
        {
            get => _tra_prn_dtm;
            set => Set(ref _tra_prn_dtm, value);
        }

        #endregion

    }
}
