using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_mpcb")]
    public class Mi_mpcb : BaseModel<Mi_mpcb>
    {
        private string _icb_ipd_no;
        [Key]
        public string icb_ipd_no
        {
            get => _icb_ipd_no;
            set => Set(ref _icb_ipd_no, value);
        }

        private string _icb_odr_no;
        [Key]
        public string icb_odr_no
        {
            get => _icb_odr_no;
            set => Set(ref _icb_odr_no, value);
        }

        private short? _icb_fee_seq;
        [Key]
        public short? icb_fee_seq
        {
            get => _icb_fee_seq;
            set => Set(ref _icb_fee_seq, value);
        }

        private string _icb_fee_key;
        public string icb_fee_key
        {
            get => _icb_fee_key;
            set => Set(ref _icb_fee_key, value);
        }

        private string _icb_std_fno;
        public string icb_std_fno
        {
            get => _icb_std_fno;
            set => Set(ref _icb_std_fno, value);
        }

        private string _icb_del_mark;
        public string icb_del_mark
        {
            get => _icb_del_mark;
            set => Set(ref _icb_del_mark, value);
        }

        private short? _icb_seq_no;
        public short? icb_seq_no
        {
            get => _icb_seq_no;
            set => Set(ref _icb_seq_no, value);
        }

        private string _icb_map_data;
        public string icb_map_data
        {
            get => _icb_map_data;
            set => Set(ref _icb_map_data, value);
        }

        private int? _icb_op_edate;
        public int? icb_op_edate
        {
            get => _icb_op_edate;
            set => Set(ref _icb_op_edate, value);
        }

        private string _icb_lb_dis;
        public string icb_lb_dis
        {
            get => _icb_lb_dis;
            set => Set(ref _icb_lb_dis, value);
        }

        private string _icb_data1;
        public string icb_data1
        {
            get => _icb_data1;
            set => Set(ref _icb_data1, value);
        }

        private string _icb_data2;
        public string icb_data2
        {
            get => _icb_data2;
            set => Set(ref _icb_data2, value);
        }

    }
}
