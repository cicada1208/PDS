using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mch_msen")]
    public class Mch_msen : BaseModel<Mch_msen>
    {
        private string _chsen_type;
        [Key]
        public string chsen_type
        {
            get => _chsen_type;
            set => Set(ref _chsen_type, value);
        }

        private int? _chsen_pat_no;
        [Key]
        public int? chsen_pat_no
        {
            get => _chsen_pat_no;
            set => Set(ref _chsen_pat_no, value);
        }

        private string _chsen_code;
        [Key]
        public string chsen_code
        {
            get => _chsen_code;
            set => Set(ref _chsen_code, value);
        }

        private string _chsen_cre_opr;
        [Key]
        public string chsen_cre_opr
        {
            get => _chsen_cre_opr;
            set => Set(ref _chsen_cre_opr, value);
        }

        private int? _chsen_cre_dt;
        [Key]
        public int? chsen_cre_dt
        {
            get => _chsen_cre_dt;
            set => Set(ref _chsen_cre_dt, value);
        }

        private int? _chsen_cre_tm;
        [Key]
        public int? chsen_cre_tm
        {
            get => _chsen_cre_tm;
            set => Set(ref _chsen_cre_tm, value);
        }

        private string _chsen_data;
        public string chsen_data
        {
            get => _chsen_data;
            set => Set(ref _chsen_data, value);
        }

        private string _chsen_info;
        /// <summary>
        /// 用藥過敏暨不良反應資訊
        /// </summary>
        [NotMapped]
        public string chsen_info
        {
            get => _chsen_info;
            set => Set(ref _chsen_info, value);
        }

    }
}

