using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "pds_recd")]
    public class Pds_recd : BaseModel<Pds_recd>
    {
        private string _pds_recd_no;
        [Key]
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

        private string _pds_rec_op_type;
        [NotMapped]
        public string pds_rec_op_type
        {
            get => _pds_rec_op_type;
            set => Set(ref _pds_rec_op_type, value);
        }

        private string _pds_rec_op_name;
        [NotMapped]
        public string pds_rec_op_name
        {
            get => _pds_rec_op_name;
            set => Set(ref _pds_rec_op_name, value);
        }

        private string _pds_rec_bag_code;
        [NotMapped]
        public string pds_rec_bag_code
        {
            get => _pds_rec_bag_code;
            set => Set(ref _pds_rec_bag_code, value);
        }

        private string _pds_rec_lst_code;
        [NotMapped]
        public string pds_rec_lst_code
        {
            get => _pds_rec_lst_code;
            set => Set(ref _pds_rec_lst_code, value);
        }

        private string _pds_recd_op_name;
        [NotMapped]
        public string pds_recd_op_name
        {
            get => _pds_recd_op_name;
            set => Set(ref _pds_recd_op_name, value);
        }

        private string _pds_recd_st_name;
        [NotMapped]
        public string pds_recd_st_name
        {
            get => _pds_recd_st_name;
            set => Set(ref _pds_recd_st_name, value);
        }

        private string _pds_recd_reason_name;
        [NotMapped]
        public string pds_recd_reason_name
        {
            get => _pds_recd_reason_name;
            set => Set(ref _pds_recd_reason_name, value);
        }

        private Pds_recParam.DetailMode _detailMode;
        /// <summary>
        /// ©ú²Ó¾úµ{¼Ò¦¡
        /// </summary>
        [NotMapped]
        public Pds_recParam.DetailMode DetailMode
        {
            get => _detailMode;
            set => Set(ref _detailMode, value);
        }

    }
}
