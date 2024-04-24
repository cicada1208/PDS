using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.NIS, "ni_Users")]
    public class Users : BaseModel<Users>
    {
        private string _userId;
        [Key]
        public string userId
        {
            get => _userId;
            set => Set(ref _userId, value);
        }

        private string _loginId;
        public string loginId
        {
            get => _loginId;
            set => Set(ref _loginId, value);
        }

        private string _userName;
        public string userName
        {
            get => _userName;
            set => Set(ref _userName, value);
        }

        private string _userTerseName;
        [Display(Name = "使用者簡稱")]
        public string userTerseName
        {
            get => _userTerseName;
            set => Set(ref _userTerseName, value);
        }

        private string _employeeNo;
        public string employeeNo
        {
            get => _employeeNo;
            set => Set(ref _employeeNo, value);
        }

        private string _employeeDept;
        public string employeeDept
        {
            get => _employeeDept;
            set => Set(ref _employeeDept, value);
        }

        private string _proTitleId;
        public string proTitleId
        {
            get => _proTitleId;
            set => Set(ref _proTitleId, value);
        }

        private string _proTitleLevelId;
        public string proTitleLevelId
        {
            get => _proTitleLevelId;
            set => Set(ref _proTitleLevelId, value);
        }

        private bool? _isService;
        public bool? isService
        {
            get => _isService;
            set => Set(ref _isService, value);
        }

        private bool? _isTester;
        public bool? isTester
        {
            get => _isTester;
            set => Set(ref _isTester, value);
        }

        private string _dimission;
        public string dimission
        {
            get => _dimission;
            set => Set(ref _dimission, value);
        }

        private string _DutyCode;
        public string DutyCode
        {
            get => _DutyCode;
            set => Set(ref _DutyCode, value);
        }

        private string _empCategory;
        public string empCategory
        {
            get => _empCategory;
            set => Set(ref _empCategory, value);
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

        private DateTime? _systemDtBegin;
        [NotMapped]
        public DateTime? systemDtBegin
        {
            get => _systemDtBegin;
            set => Set(ref _systemDtBegin, value);
        }

        private DateTime? _systemDtEnd;
        [NotMapped]
        public DateTime? systemDtEnd
        {
            get => _systemDtEnd;
            set => Set(ref _systemDtEnd, value);
        }

    }
}
