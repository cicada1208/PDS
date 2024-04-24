namespace Models
{
    public class PdsRecMicbcode : BaseModel<PdsRecMicbcode>
    {

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
        public string icbcode_rx_uqty
        {
            get => _icbcode_rx_uqty;
            set => Set(ref _icbcode_rx_uqty, value);
        }

        private string _icbcode_rx_qty;
        /// <summary>
        /// 加總總量
        /// </summary>
        public string icbcode_rx_qty
        {
            get => _icbcode_rx_qty;
            set => Set(ref _icbcode_rx_qty, value);
        }

        private string _icbcode_send_dt_fmt;
        public string icbcode_send_dt_fmt
        {
            get => _icbcode_send_dt_fmt;
            set => Set(ref _icbcode_send_dt_fmt, value);
        }

        private string _icbcode_med_type_name;
        public string icbcode_med_type_name
        {
            get => _icbcode_med_type_name;
            set => Set(ref _icbcode_med_type_name, value);
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
        public string icfcode_prt_dt_fmt
        {
            get => _icfcode_prt_dt_fmt;
            set => Set(ref _icfcode_prt_dt_fmt, value);
        }

        private string _icfcode_ipill_no;
        /// <summary>
        /// 領藥號
        /// </summary>
        public string icfcode_ipill_no
        {
            get => _icfcode_ipill_no;
            set => Set(ref _icfcode_ipill_no, value);
        }

        #endregion

        #region pds_rec

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
        public string pds_rec_op_dtm_begin
        {
            get => _pds_rec_op_dtm_begin;
            set => Set(ref _pds_rec_op_dtm_begin, value);
        }

        private string _pds_rec_op_dtm_end;
        public string pds_rec_op_dtm_end
        {
            get => _pds_rec_op_dtm_end;
            set => Set(ref _pds_rec_op_dtm_end, value);
        }

        private string _pds_rec_bag_code;
        public string pds_rec_bag_code
        {
            get => _pds_rec_bag_code;
            set => Set(ref _pds_rec_bag_code, value);
        }

        private string _pds_rec_lst_code;
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
        public string pds_rec_clinical
        {
            get => _pds_rec_clinical;
            set => Set(ref _pds_rec_clinical, value);
        }

        private string _pds_rec_st;
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

        private int? _pds_rec_send_dt_begin;
        public int? pds_rec_send_dt_begin
        {
            get => _pds_rec_send_dt_begin;
            set => Set(ref _pds_rec_send_dt_begin, value);
        }

        private int? _pds_rec_send_dt_end;
        public int? pds_rec_send_dt_end
        {
            get => _pds_rec_send_dt_end;
            set => Set(ref _pds_rec_send_dt_end, value);
        }

        private string _pds_rec_send_dt_begin_fmt;
        public string pds_rec_send_dt_begin_fmt
        {
            get => _pds_rec_send_dt_begin_fmt;
            set => Set(ref _pds_rec_send_dt_begin_fmt, value);
        }

        private string _pds_rec_send_dt_end_fmt;
        public string pds_rec_send_dt_end_fmt
        {
            get => _pds_rec_send_dt_end_fmt;
            set => Set(ref _pds_rec_send_dt_end_fmt, value);
        }

        private string _pds_rec_send_dt_fmt;
        public string pds_rec_send_dt_fmt
        {
            get => _pds_rec_send_dt_fmt;
            set => Set(ref _pds_rec_send_dt_fmt, value);
        }

        private string _pds_rec_op_name;
        public string pds_rec_op_name
        {
            get => _pds_rec_op_name;
            set => Set(ref _pds_rec_op_name, value);
        }

        private string _pds_rec_st_name;
        public string pds_rec_st_name
        {
            get => _pds_rec_st_name;
            set => Set(ref _pds_rec_st_name, value);
        }

        private string _pds_rec_reason_name;
        public string pds_rec_reason_name
        {
            get => _pds_rec_reason_name;
            set => Set(ref _pds_rec_reason_name, value);
        }

        #endregion

        #region pds_recd

        private string _pds_recd_no;
        public string pds_recd_no
        {
            get => _pds_recd_no;
            set => Set(ref _pds_recd_no, value);
        }

        private string _pds_recd_rec_no;
        public string pds_recd_rec_no
        {
            get => _pds_recd_rec_no;
            set => Set(ref _pds_recd_rec_no, value);
        }

        private string _pds_recd_op_type;
        public string pds_recd_op_type
        {
            get => _pds_recd_op_type;
            set => Set(ref _pds_recd_op_type, value);
        }

        private string _pds_recd_op_dtm;
        public string pds_recd_op_dtm
        {
            get => _pds_recd_op_dtm;
            set => Set(ref _pds_recd_op_dtm, value);
        }

        private string _pds_recd_code;
        public string pds_recd_code
        {
            get => _pds_recd_code;
            set => Set(ref _pds_recd_code, value);
        }

        private string _pds_recd_mst_id;
        public string pds_recd_mst_id
        {
            get => _pds_recd_mst_id;
            set => Set(ref _pds_recd_mst_id, value);
        }

        private string _pds_recd_pack;
        public string pds_recd_pack
        {
            get => _pds_recd_pack;
            set => Set(ref _pds_recd_pack, value);
        }

        private string _pds_recd_st;
        public string pds_recd_st
        {
            get => _pds_recd_st;
            set => Set(ref _pds_recd_st, value);
        }

        private string _pds_recd_reason;
        public string pds_recd_reason
        {
            get => _pds_recd_reason;
            set => Set(ref _pds_recd_reason, value);
        }

        private string _pds_recd_reason_oth;
        public string pds_recd_reason_oth
        {
            get => _pds_recd_reason_oth;
            set => Set(ref _pds_recd_reason_oth, value);
        }

        private string _pds_recd_nondeliver;
        public string pds_recd_nondeliver
        {
            get => _pds_recd_nondeliver;
            set => Set(ref _pds_recd_nondeliver, value);
        }

        private string _pds_recd_md_qty;
        public string pds_recd_md_qty
        {
            get => _pds_recd_md_qty;
            set => Set(ref _pds_recd_md_qty, value);
        }

        private string _pds_recd_md_way1;
        public string pds_recd_md_way1
        {
            get => _pds_recd_md_way1;
            set => Set(ref _pds_recd_md_way1, value);
        }

        private string _pds_recd_err_mst_id;
        public string pds_recd_err_mst_id
        {
            get => _pds_recd_err_mst_id;
            set => Set(ref _pds_recd_err_mst_id, value);
        }

        private string _pds_recd_err_qty;
        public string pds_recd_err_qty
        {
            get => _pds_recd_err_qty;
            set => Set(ref _pds_recd_err_qty, value);
        }

        private string _pds_recd_err_uqty1;
        public string pds_recd_err_uqty1
        {
            get => _pds_recd_err_uqty1;
            set => Set(ref _pds_recd_err_uqty1, value);
        }

        private string _pds_recd_err_uqty2;
        public string pds_recd_err_uqty2
        {
            get => _pds_recd_err_uqty2;
            set => Set(ref _pds_recd_err_uqty2, value);
        }

        private string _pds_recd_err_expdt;
        public string pds_recd_err_expdt
        {
            get => _pds_recd_err_expdt;
            set => Set(ref _pds_recd_err_expdt, value);
        }

        private string _pds_recd_md_man;
        public string pds_recd_md_man
        {
            get => _pds_recd_md_man;
            set => Set(ref _pds_recd_md_man, value);
        }

        private string _pds_recd_md_name;
        public string pds_recd_md_name
        {
            get => _pds_recd_md_name;
            set => Set(ref _pds_recd_md_name, value);
        }

        private string _pds_recd_md_pc;
        public string pds_recd_md_pc
        {
            get => _pds_recd_md_pc;
            set => Set(ref _pds_recd_md_pc, value);
        }

        private string _pds_recd_md_ver;
        public string pds_recd_md_ver
        {
            get => _pds_recd_md_ver;
            set => Set(ref _pds_recd_md_ver, value);
        }

        private string _pds_recd_md_dt;
        public string pds_recd_md_dt
        {
            get => _pds_recd_md_dt;
            set => Set(ref _pds_recd_md_dt, value);
        }

        private string _pds_recd_md_time;
        public string pds_recd_md_time
        {
            get => _pds_recd_md_time;
            set => Set(ref _pds_recd_md_time, value);
        }

        private string _pds_recd_filler;
        public string pds_recd_filler
        {
            get => _pds_recd_filler;
            set => Set(ref _pds_recd_filler, value);
        }

        private string _pds_recd_op_name;
        public string pds_recd_op_name
        {
            get => _pds_recd_op_name;
            set => Set(ref _pds_recd_op_name, value);
        }

        private string _pds_recd_st_name;
        public string pds_recd_st_name
        {
            get => _pds_recd_st_name;
            set => Set(ref _pds_recd_st_name, value);
        }

        private string _pds_recd_reason_name;
        public string pds_recd_reason_name
        {
            get => _pds_recd_reason_name;
            set => Set(ref _pds_recd_reason_name, value);
        }

        private string _pds_recd_md_name_a;
        /// <summary>
        /// 調劑錯誤時調劑藥師
        /// </summary>
        public string pds_recd_md_name_a
        {
            get => _pds_recd_md_name_a;
            set => Set(ref _pds_recd_md_name_a, value);
        }

        private string _pds_recd_md_name_c;
        /// <summary>
        /// 調劑錯誤時核對藥師
        /// </summary>
        public string pds_recd_md_name_c
        {
            get => _pds_recd_md_name_c;
            set => Set(ref _pds_recd_md_name_c, value);
        }

        #endregion

        #region ch_udrec

        private string _chudrec_bed;
        public string chudrec_bed
        {
            get => _chudrec_bed;
            set => Set(ref _chudrec_bed, value);
        }

        private string _chudrec_bed_unit;
        public string chudrec_bed_unit
        {
            get => _chudrec_bed_unit;
            set => Set(ref _chudrec_bed_unit, value);
        }

        #endregion

        #region ch_udrec_chk

        private string _chudrecchk_bed;
        public string chudrecchk_bed
        {
            get => _chudrecchk_bed;
            set => Set(ref _chudrecchk_bed, value);
        }

        private string _chudrecchk_bed_unit;
        public string chudrecchk_bed_unit
        {
            get => _chudrecchk_bed_unit;
            set => Set(ref _chudrecchk_bed_unit, value);
        }

        #endregion

        private string _pat_name;
        public string pat_name
        {
            get => _pat_name;
            set => Set(ref _pat_name, value);
        }

    }
}
