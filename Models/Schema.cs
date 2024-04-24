namespace Models
{
    public class Schema : BaseModel<Schema>
    {
        private string _Sql;
        public string Sql
        {
            get => _Sql;
            set => Set(ref _Sql, value);
        }

        private string _DBName;
        public string DBName
        {
            get => _DBName;
            set => Set(ref _DBName, value);
        }

        private string _index_name;
        public string index_name
        {
            get => _index_name;
            set => Set(ref _index_name, value);
        }

        private string _index_keys;
        public string index_keys
        {
            get => _index_keys;
            set => Set(ref _index_keys, value);
        }

        private string _index_description;
        public string index_description
        {
            get => _index_description;
            set => Set(ref _index_description, value);
        }
    }
}
