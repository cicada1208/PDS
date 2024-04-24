using Lib;
using MahApps.Metro.Controls;
using Models;
using Params;
using ViewModels;

namespace Controls
{
    /// <summary>
    /// PdsUdrecChkInsertWindow.xaml 的互動邏輯
    /// </summary>
    public partial class PdsUdrecChkInsertWindow : MetroWindow
    {
        public PdsUdrecChkInsertWindow()
        {
            InitializeComponent();
        }

        private UtilLocator _util;
        protected UtilLocator Util =>
            _util ?? (_util = new UtilLocator());

        private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var result = ApiUtil.HttpClientEx<ApiResult<Ch_udrec_chk>>(
            RouteParam.Service(),
            RouteParam.Ch_udrec_chk.InsertCh_udrec_chk,
            new Ch_udrec_chk { chudrecchk_filler=$"{LoginViewModel.LoginUser.UserId}"}, 
            new { option = 2 }); // 2: 依傳送日期(當下)，新增啟動藥車核對時護理站及床號

            //if (!result.Succ)
            Util.Media.MsgSpeech(result.Msg);

            DialogResult = true;
        }
    }
}
