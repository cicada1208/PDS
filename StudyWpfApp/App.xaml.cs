using Lib;
using Models;
using Params;
using System.Windows;
using System.Windows.Threading;

namespace StudyWpfApp
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        private static string _dbFormal;
        public static string DBFormal
        {
            get
            {
                if (_dbFormal == null)
                {
                    string service = RouteParam.Service();
                    string requestUri = RouteParam.DB.QueryDB;
                    DB db = new DB();
                    db.DBName = DBParam.DBName.SYB1;
                    var result = ApiUtil.HttpClientEx<ApiResult<DB>>(
                        service, requestUri, db);
                    _dbFormal = (bool)result.Data.IsFormal ? "【正式版】" : "【測試版】";
                }
                return _dbFormal;
            }
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            DateTimeUtil dtUtil = new DateTimeUtil();
            dtUtil.SetCurrentCulture();
        }

        private void App_DispatcherUnhandledException(object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
            e.Handled = true;
        }
    }
}
