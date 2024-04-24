using Params;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB2, "mr_mexp")]
    public class Mr_mexp : BaseModel<Mr_mexp>
    {
        private string _exp_key;
        [Key]
        public string exp_key
        {
            get => _exp_key;
            set => Set(ref _exp_key, value);
        }

        private string _exp_data_1;
        public string exp_data_1
        {
            get => _exp_data_1;
            set => Set(ref _exp_data_1, value);
        }

        private string _exp_data;
        [NotMapped]
        public string exp_data
        {
            get => _exp_data;
            set => Set(ref _exp_data, value);
        }

    }
}

