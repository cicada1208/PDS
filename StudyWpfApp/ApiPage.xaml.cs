using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace StudyWpfApp
{
    /// <summary>
    /// ApiPage.xaml 的互動邏輯
    /// </summary>
    public partial class ApiPage : Page
    {
        public ApiPage()
        {
            InitializeComponent();
        }

        private void QueryButton_Click(object sender, RoutedEventArgs e)
        {
            string service = RouteParam.Service();
            string requestUri = RouteParam.Users.QueryUser; // 1: 依參數自動組建 // 2: 依userId、狀態、未離職
            Users user = new Users();
            user.userId = "10964";
            user.userName = "王志豪";
            user.isActive = true;
            //user.userId = "14976";
            var result = ApiUtil.HttpClientEx<ApiResult<List<Users>>>(service, requestUri, user, new { option = 1 });
            ApiDataGrid.ItemsSource = result.Data;
            MessageBox.Show(result.Msg);
        }

        private void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            string service = RouteParam.Service();
            string requestUri = RouteParam.Users.UpdateUser;
            Dictionary<object, object> queryParams = new Dictionary<object, object>();
            queryParams.Add("option", 2); // 1: 依參數自動組建 // 2: 儲存userTerseName、systemDt
            Users user = new Users();
            user.userId = "10964";
            user.userTerseName = "草";
            user.systemDt = DateTime.Now;
            var result = ApiUtil.HttpClientEx<ApiResult<Users>>(service, requestUri, user, queryParams);
            MessageBox.Show(result.Msg);
        }

    }
}
