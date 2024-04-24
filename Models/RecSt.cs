using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class RecSt : BaseModel<RecSt>
    {
        private object _status;
        public object Status
        {
            get => _status;
            set => Set(ref _status, value);
        }

        private string _statusName;
        public string StatusName
        {
            get => _statusName;
            set => Set(ref _statusName, value);
        }
    }
}
