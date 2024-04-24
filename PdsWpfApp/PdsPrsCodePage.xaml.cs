using System.Windows;
using System.Windows.Controls;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// PdsPrsCodePage.xaml 的互動邏輯
    /// </summary>
    public partial class PdsPrsCodePage : Page
    {
        public PdsPrsCodePage()
        {
            InitializeComponent();
        }

        private PdsPrsCodeViewModel PdsCodeViewModel =>
             this.DataContext as PdsPrsCodeViewModel;

        private void FilterExpander_Expanded(object sender, RoutedEventArgs e)
        {
            FilterRow.Height = new GridLength(170, GridUnitType.Auto);
            ListRow.Height = new GridLength(1, GridUnitType.Star);
        }

        private void FilterExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            FilterRow.Height = new GridLength(30, GridUnitType.Auto);
        }

        //private void SelectPrsEvent(object sender, TextChangedEventArgs e) =>
        //    PdsCodeViewModel.OnSelectPrs();
    }
}
