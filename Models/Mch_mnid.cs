using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "mch_mnid")]
    public class Mch_mnid : BaseModel<Mch_mnid>
    {
        private string _chnid_id;
        [Key]
        public string chnid_id
        {
            get => _chnid_id;
            set => Set(ref _chnid_id, value);
        }

        private string _chnid_code;
        [Key]
        public string chnid_code
        {
            get => _chnid_code;
            set => Set(ref _chnid_code, value);
        }

        private string _chnid_trn;
        public string chnid_trn
        {
            get => _chnid_trn;
            set => Set(ref _chnid_trn, value);
        }

        private string _chnid_name;
        public string chnid_name
        {
            get => _chnid_name;
            set => Set(ref _chnid_name, value);
        }

        private string _chnid_rec;
        public string chnid_rec
        {
            get => _chnid_rec;
            set => Set(ref _chnid_rec, value);
        }

    }
}

