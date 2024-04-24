using Lib;
using Params;

namespace Repositorys
{
    public abstract class BaseRepository<TModel> where TModel : class
    {
        public BaseRepository()
        {
            DBType = DBUtil.GetDBType<TModel>();
            DBName = DBUtil.GetDBName<TModel>();
            TableName = DBUtil.GetTableName<TModel>();
        }

        /// <summary>
        /// DB類型
        /// </summary>
        public DBParam.DBType DBType { get; set; } = DBParam.DBType.SYBASE;
        /// <summary>
        /// DB名稱
        /// </summary>
        public string DBName { get; set; } = string.Empty;
        /// <summary>
        /// 表格名稱
        /// </summary>
        public string TableName { get; set; } = string.Empty;

        private DBContext _DB;
        protected DBContext DB =>
            _DB ?? (_DB = new DBContext());

        private UtilLocator _util;
        protected UtilLocator Util =>
            _util ?? (_util = new UtilLocator());

    }
}
