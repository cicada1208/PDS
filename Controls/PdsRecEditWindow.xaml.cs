using MahApps.Metro.Controls;
using ViewModels;

namespace Controls
{
    /// <summary>
    /// PdsRecEditWindow.xaml 的互動邏輯
    /// </summary>
    public partial class PdsRecEditWindow : MetroWindow
    {
        public PdsRecEditWindow()
        {
            InitializeComponent();
        }

        private PdsRecEditViewModel PdsRecEditViewModel =>
            this.DataContext as PdsRecEditViewModel;

    }
}
