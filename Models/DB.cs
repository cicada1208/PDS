namespace Models
{
    public class DB : BaseModel<DB>
    {
        private string _DBName;
        public string DBName
        {
            get => _DBName;
            set => Set(ref _DBName, value);
        }

        private bool? _isFormal;
        public bool? IsFormal
        {
            get => _isFormal;
            set => Set(ref _isFormal, value);
        }
    }
}
