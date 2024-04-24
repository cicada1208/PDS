using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_SysParameter")]
    public class SysParameter : BaseModel<SysParameter>
    {
        private string _sysParameterId;
        [Key]
        public string sysParameterId
        {
            get => _sysParameterId;
            set => Set(ref _sysParameterId, value);
        }

        private string _parameterName;
        public string parameterName
        {
            get => _parameterName;
            set => Set(ref _parameterName, value);
        }

        private string _value;
        public string value
        {
            get => _value;
            set => Set(ref _value, value);
        }

        private string _description;
        public string description
        {
            get => _description;
            set => Set(ref _description, value);
        }

        private bool? _isActive;
        public bool? isActive
        {
            get => _isActive;
            set => Set(ref _isActive, value);
        }

        private string _systemUserId;
        public string systemUserId
        {
            get => _systemUserId;
            set => Set(ref _systemUserId, value);
        }

        private DateTime? _systemDt;
        public DateTime? systemDt
        {
            get => _systemDt;
            set => Set(ref _systemDt, value);
        }

    }
}
