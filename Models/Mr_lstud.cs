using Params;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mr_lstud")]
    public class Mr_lstud : BaseModel<Mr_lstud>
    {
        private string _lstud_key;
        [Key]
        public string lstud_key
        {
            get => _lstud_key;
            set => Set(ref _lstud_key, value);
        }

        private int? _lstud_mod_dt;
        [Key]
        public int? lstud_mod_dt
        {
            get => _lstud_mod_dt;
            set => Set(ref _lstud_mod_dt, value);
        }

        private int? _lstud_mod_time;
        [Key]
        public int? lstud_mod_time
        {
            get => _lstud_mod_time;
            set => Set(ref _lstud_mod_time, value);
        }

        private short? _lstud_seq;
        [Key]
        public short? lstud_seq
        {
            get => _lstud_seq;
            set => Set(ref _lstud_seq, value);
        }

        private string _lstud_mod_date_v;
        public string lstud_mod_date_v
        {
            get => _lstud_mod_date_v;
            set => Set(ref _lstud_mod_date_v, value);
        }

        private string _lstud_grade;
        public string lstud_grade
        {
            get => _lstud_grade;
            set => Set(ref _lstud_grade, value);
        }

        private string _lstud_cre_user;
        public string lstud_cre_user
        {
            get => _lstud_cre_user;
            set => Set(ref _lstud_cre_user, value);
        }

        private string _lstud_data1;
        public string lstud_data1
        {
            get => _lstud_data1;
            set => Set(ref _lstud_data1, value);
        }

        private string _lstud_data2;
        public string lstud_data2
        {
            get => _lstud_data2;
            set => Set(ref _lstud_data2, value);
        }

        private string _lstud_data3;
        public string lstud_data3
        {
            get => _lstud_data3;
            set => Set(ref _lstud_data3, value);
        }

        private string _lstud_data4;
        public string lstud_data4
        {
            get => _lstud_data4;
            set => Set(ref _lstud_data4, value);
        }

        private string _lstud_data5;
        public string lstud_data5
        {
            get => _lstud_data5;
            set => Set(ref _lstud_data5, value);
        }

        private string _effect;
        /// <summary>
        /// 影響
        /// </summary>
        [NotMapped]
        public string effect
        {
            get => _effect;
            set => Set(ref _effect, value);
        }

        private string _treatment;
        /// <summary>
        /// 處置
        /// </summary>
        [NotMapped]
        public string treatment
        {
            get => _treatment;
            set => Set(ref _treatment, value);
        }

        private List<Mi_micbcode> _icbcodeList;
        /// <summary>
        /// 符合該交互作用的處方
        /// </summary>
        [NotMapped]
        public List<Mi_micbcode> icbcodeList
        {
            get => _icbcodeList;
            set => Set(ref _icbcodeList, value);
        }

        private int? _icbcode_send_dt;
        [NotMapped]
        public int? icbcode_send_dt
        {
            get => _icbcode_send_dt;
            set => Set(ref _icbcode_send_dt, value);
        }

        private string _icbcode_ipd_no;
        [NotMapped]
        public string icbcode_ipd_no
        {
            get => _icbcode_ipd_no;
            set => Set(ref _icbcode_ipd_no, value);
        }

        private string _icbcode_fee_key;
        /// <summary>
        /// 藥品處置代碼1
        /// </summary>
        [NotMapped]
        public string icbcode_fee_key
        {
            get => _icbcode_fee_key;
            set => Set(ref _icbcode_fee_key, value);
        }

        private string _icbcode_fee_key2;
        /// <summary>
        /// 藥品處置代碼2
        /// </summary>
        [NotMapped]
        public string icbcode_fee_key2
        {
            get => _icbcode_fee_key2;
            set => Set(ref _icbcode_fee_key2, value);
        }

        private int? _icfcode_prt_dt;
        [NotMapped]
        public int? icfcode_prt_dt
        {
            get => _icfcode_prt_dt;
            set => Set(ref _icfcode_prt_dt, value);
        }

        private string _icfcode_id;
        [NotMapped]
        public string icfcode_id
        {
            get => _icfcode_id;
            set => Set(ref _icfcode_id, value);
        }

        private short? _icfcode_pill_no;
        [NotMapped]
        public short? icfcode_pill_no
        {
            get => _icfcode_pill_no;
            set => Set(ref _icfcode_pill_no, value);
        }

    }
}

