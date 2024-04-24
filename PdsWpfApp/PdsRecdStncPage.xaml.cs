using Controls;
using Models;
using System.Windows.Controls;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// PdsRecdStncPage.xaml 的互動邏輯
    /// </summary>
    public partial class PdsRecdStncPage : Page
    {
        public PdsRecdStncPage()
        {
            InitializeComponent();
            PdsRecdStncViewModel.ShowPdsRecdWindow = ShowPdsRecdWindow;
        }

        private PdsRecdStncViewModel PdsRecdStncViewModel =>
            this.DataContext as PdsRecdStncViewModel;

        private void ShowPdsRecdWindow(Pds_recd recd)
        {
            var win = new PdsRecdWindow();
            var vm = new PdsRecdViewModel();
            vm.RecdParam = recd;
            win.DataContext = vm;
            win.Show();
        }


    }
}
