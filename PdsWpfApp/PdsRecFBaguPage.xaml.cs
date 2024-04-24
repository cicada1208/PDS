using Controls;
using Models;
using System.Windows.Controls;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// PdsRecFBaguPage.xaml 的互動邏輯
    /// </summary>
    public partial class PdsRecFBaguPage : Page
    {
        public PdsRecFBaguPage()
        {
            InitializeComponent();
            PdsRecFBaguViewModel.ShowPdsRecEditWindow = ShowPdsRecEditWindow;
            PdsRecFBaguViewModel.ShowPdsRecdWindow = ShowPdsRecdWindow;
        }

        private PdsRecFBaguViewModel PdsRecFBaguViewModel =>
            this.DataContext as PdsRecFBaguViewModel;

        private bool ShowPdsRecEditWindow(Pds_rec rec)
        {
            var win = new PdsRecEditWindow();
            var vm = new PdsRecEditViewModel();
            vm.RecParam = rec;
            win.DataContext = vm;
            return (bool)win.ShowDialog();
        }

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
