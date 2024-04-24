using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_prsn")]
    public class Ch_prsn : BaseModel<Ch_prsn>
    {
        private string _chprsn_sys_id;
        [Key]
        public string chprsn_sys_id
        {
            get => _chprsn_sys_id;
            set => Set(ref _chprsn_sys_id, value);
        }

        private string _chprsn_maj_id;
        [Key]
        public string chprsn_maj_id
        {
            get => _chprsn_maj_id;
            set => Set(ref _chprsn_maj_id, value);
        }

        private string _chprsn_aux_id;
        [Key]
        public string chprsn_aux_id
        {
            get => _chprsn_aux_id;
            set => Set(ref _chprsn_aux_id, value);
        }

        private short? _chprsn_seq;
        [Key]
        public short? chprsn_seq
        {
            get => _chprsn_seq;
            set => Set(ref _chprsn_seq, value);
        }

        private string _chprsn_rec;
        public string chprsn_rec
        {
            get => _chprsn_rec;
            set => Set(ref _chprsn_rec, value);
        }

        private string _chprsn_filler2;
        public string chprsn_filler2
        {
            get => _chprsn_filler2;
            set => Set(ref _chprsn_filler2, value);
        }

        private string _chprs_mst_id;
        [NotMapped]
        public string chprs_mst_id
        {
            get => _chprs_mst_id;
            set => Set(ref _chprs_mst_id, value);
        }

    }
}

