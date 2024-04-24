using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_RECSerialNo")]
    public class RECSerialNo : BaseModel<RECSerialNo>
    {
        private string _SYSID;
        [Key]
        public string SYSID
        {
            get => _SYSID;
            set => Set(ref _SYSID, value);
        }

        private string _SDATE;
        [Key]
        public string SDATE
        {
            get => _SDATE;
            set => Set(ref _SDATE, value);
        }

        private string _NUM;
        public string NUM
        {
            get => _NUM;
            set => Set(ref _NUM, value);
        }

    }
}
