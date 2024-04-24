using MahApps.Metro.Controls;
using ViewModels;

namespace Controls
{
    /// <summary>
    /// PdsRecCancelWindow.xaml 的互動邏輯
    /// </summary>
    public partial class PdsRecCancelWindow : MetroWindow
    {
        public PdsRecCancelWindow()
        {
            InitializeComponent();
        }

        private PdsRecCancelViewModel PdsCancelViewModel =>
            this.DataContext as PdsRecCancelViewModel;

        //private void OKButton_Click(object sender, System.Windows.RoutedEventArgs e) =>
        //    DialogResult = true;

        //private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e) =>
        //    DialogResult = false;

    }
}
