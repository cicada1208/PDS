using Controls;
using Params;
using System.Windows.Controls;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// PdsRecAdjustPage.xaml 的互動邏輯
    /// </summary>
    public partial class PdsRecAdjustPage : Page
    {
        public PdsRecAdjustPage()
        {
            InitializeComponent();
            PdsRecAdjustViewModel.ShowPdsRecCancelWindow = ShowPdsRecCancelWindow;
            this.Loaded += PdsRecAdjust_Loaded;
        }

        private void PdsRecAdjust_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PdsRecAdjustViewModel.BagCodeEnabled)
                this.BagCodeTextBox.Focus();
        }

        private PdsRecAdjustViewModel PdsRecAdjustViewModel =>
            this.DataContext as PdsRecAdjustViewModel;

        private PdsRecCancelViewModel ShowPdsRecCancelWindow()
        {
            var win = new PdsRecCancelWindow();
            var vm = new PdsRecCancelViewModel();
            vm.CancelMode = Pds_recParam.CancelMode.ABAGC;
            win.DataContext = vm;
            if ((bool)win.ShowDialog())
                return win.DataContext as PdsRecCancelViewModel;
            else
                return null;
        }

    }
}
