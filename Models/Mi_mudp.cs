using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mi_mudp")]
    public class Mi_mudp : BaseModel<Mi_mudp>
    {
        private string _udp_id;
        [Key]
        public string udp_id
        {
            get => _udp_id;
            set => Set(ref _udp_id, value);
        }

        private string _udp_ipd_no;
        [Key]
        public string udp_ipd_no
        {
            get => _udp_ipd_no;
            set => Set(ref _udp_ipd_no, value);
        }

        private int? _udp_send_dt;
        [Key]
        public int? udp_send_dt
        {
            get => _udp_send_dt;
            set => Set(ref _udp_send_dt, value);
        }

        private string _udp_odr_no;
        [Key]
        public string udp_odr_no
        {
            get => _udp_odr_no;
            set => Set(ref _udp_odr_no, value);
        }

        private string _udp_data1;
        public string udp_data1
        {
            get => _udp_data1;
            set => Set(ref _udp_data1, value);
        }

    }
}
