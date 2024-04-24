using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PdsNoteMicbcode : BaseModel<PdsNoteMicbcode>
    {

        #region pds_note

        private string _pds_note_no;
        public string pds_note_no
        {
            get => _pds_note_no;
            set => Set(ref _pds_note_no, value);
        }

        private string _pds_note_dtm;
        public string pds_note_dtm
        {
            get => _pds_note_dtm;
            set => Set(ref _pds_note_dtm, value);
        }

        private string _pds_note_type;
        public string pds_note_type
        {
            get => _pds_note_type;
            set => Set(ref _pds_note_type, value);
        }

        private string _pds_note_op;
        public string pds_note_op
        {
            get => _pds_note_op;
            set => Set(ref _pds_note_op, value);
        }

        private string _pds_note_bag_code;
        public string pds_note_bag_code
        {
            get => _pds_note_bag_code;
            set => Set(ref _pds_note_bag_code, value);
        }

        private string _pds_note_lst_code;
        public string pds_note_lst_code
        {
            get => _pds_note_lst_code;
            set => Set(ref _pds_note_lst_code, value);
        }

        private int? _pds_note_send_dt;
        public int? pds_note_send_dt
        {
            get => _pds_note_send_dt;
            set => Set(ref _pds_note_send_dt, value);
        }

        private string _pds_note_ipd_no;
        public string pds_note_ipd_no
        {
            get => _pds_note_ipd_no;
            set => Set(ref _pds_note_ipd_no, value);
        }

        private string _pds_note_pat_no;
        public string pds_note_pat_no
        {
            get => _pds_note_pat_no;
            set => Set(ref _pds_note_pat_no, value);
        }

        private string _pds_note_bed;
        public string pds_note_bed
        {
            get => _pds_note_bed;
            set => Set(ref _pds_note_bed, value);
        }

        private string _pds_note_clinical;
        public string pds_note_clinical
        {
            get => _pds_note_clinical;
            set => Set(ref _pds_note_clinical, value);
        }

        private string _pds_note_note;
        public string pds_note_note
        {
            get => _pds_note_note;
            set => Set(ref _pds_note_note, value);
        }

        private string _pds_note_st;
        public string pds_note_st
        {
            get => _pds_note_st;
            set => Set(ref _pds_note_st, value);
        }

        private string _pds_note_md_man;
        public string pds_note_md_man
        {
            get => _pds_note_md_man;
            set => Set(ref _pds_note_md_man, value);
        }

        private string _pds_note_md_name;
        public string pds_note_md_name
        {
            get => _pds_note_md_name;
            set => Set(ref _pds_note_md_name, value);
        }

        private string _pds_note_md_pc;
        public string pds_note_md_pc
        {
            get => _pds_note_md_pc;
            set => Set(ref _pds_note_md_pc, value);
        }

        private string _pds_note_md_ver;
        public string pds_note_md_ver
        {
            get => _pds_note_md_ver;
            set => Set(ref _pds_note_md_ver, value);
        }

        private string _pds_note_md_dt;
        public string pds_note_md_dt
        {
            get => _pds_note_md_dt;
            set => Set(ref _pds_note_md_dt, value);
        }

        private string _pds_note_md_time;
        public string pds_note_md_time
        {
            get => _pds_note_md_time;
            set => Set(ref _pds_note_md_time, value);
        }

        private string _pds_note_filler;
        public string pds_note_filler
        {
            get => _pds_note_filler;
            set => Set(ref _pds_note_filler, value);
        }

        private string _pds_note_dtm_begin;
        public string pds_note_dtm_begin
        {
            get => _pds_note_dtm_begin;
            set => Set(ref _pds_note_dtm_begin, value);
        }

        private string _pds_note_dtm_end;
        public string pds_note_dtm_end
        {
            get => _pds_note_dtm_end;
            set => Set(ref _pds_note_dtm_end, value);
        }

        private string _pds_note_type_name;
        public string pds_note_type_name
        {
            get => _pds_note_type_name;
            set => Set(ref _pds_note_type_name, value);
        }

        private string _pds_note_st_name;
        public string pds_note_st_name
        {
            get => _pds_note_st_name;
            set => Set(ref _pds_note_st_name, value);
        }

        #endregion

        #region mi_micbcode

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

        #endregion


        private string _pat_name;
        public string pat_name
        {
            get => _pat_name;
            set => Set(ref _pat_name, value);
        }

    }
}
