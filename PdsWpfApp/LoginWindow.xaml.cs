using MahApps.Metro.Controls;
using System.Windows;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// LoginWindow.xaml 的互動邏輯
    /// </summary>
    public partial class LoginWindow : MetroWindow
    {
        public LoginWindow()
        {
            InitializeComponent();
            UserIdTextBox.Focus();
            LoginViewModel.ShowMainWindow = ShowMainWindow;
        }

        private LoginViewModel LoginViewModel =>
             this.DataContext as LoginViewModel;

        /// <summary>
        /// 主畫面
        /// </summary>
        private static MainWindow mainWindow;


        /// <summary>
        /// 登入成功，顯示主畫面
        /// </summary>
        private void ShowMainWindow()
        {
            if (mainWindow == null) // mainWindow == null || !mainWindow.IsLoaded
            {
                mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
                mainWindow.Activate();

            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e) =>
            this.Close(); // Application.Current.Shutdown();
    }
}
