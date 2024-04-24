using Lib;
using Models;
using Params;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace PdsWpfApp
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

            CtrlUtil ctrlUtil = new CtrlUtil();
            ctrlUtil.SetToolTipShowOnDisabled();

            // 設定 Button 元件不接受 Enter 鍵確認，避免刷條碼過快導致掠過需確認的訊息
            KeyboardNavigation.AcceptsReturnProperty.OverrideMetadata(
            typeof(Button), new FrameworkPropertyMetadata(false));
        }

        private void App_DispatcherUnhandledException(object sender,
            DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.ToString());
            e.Handled = true;
        }
    }
}
