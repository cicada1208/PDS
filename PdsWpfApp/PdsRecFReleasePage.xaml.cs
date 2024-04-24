using Controls;
using Models;
using Params;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// PdsRecFReleasePage.xaml 的互動邏輯
    /// </summary>
    public partial class PdsRecFReleasePage : Page
    {
        public PdsRecFReleasePage()
        {
            InitializeComponent();
            PdsRecFReleaseViewModel.ShowPdsRecCancelWindow = ShowPdsRecCancelWindow;
            PdsRecFReleaseViewModel.ShowAtcCodeGroupWindow = ShowAtcCodeGroupWindow;
            PdsRecFReleaseViewModel.ShowLstudWindow = ShowLstudWindow;
            PdsRecFReleaseViewModel.ShowPdsNoteEditWindow = ShowPdsNoteEditWindow;
            this.Loaded += PdsRecFRelease_Loaded;
        }

        private void PdsRecFRelease_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PdsRecFReleaseViewModel.LstCodeEnabled)
                this.LstCodeTextBox.Focus();
        }

        private PdsRecFReleaseViewModel PdsRecFReleaseViewModel =>
            this.DataContext as PdsRecFReleaseViewModel;

        private PdsRecCancelViewModel ShowPdsRecCancelWindow(Pds_recParam.CancelMode cancelMode)
        {
            var win = new PdsRecCancelWindow();
            var vm = new PdsRecCancelViewModel();
            vm.CancelMode = cancelMode;
            win.DataContext = vm;
            if ((bool)win.ShowDialog())
                return win.DataContext as PdsRecCancelViewModel;
            else
                return null;
        }

        private void ShowAtcCodeGroupWindow(List<Mi_micbcode> vm)
        {
            var win = new AtcCodeGroupWindow();
            win.DataContext = vm;
            win.ShowDialog();
        }

        private void ShowLstudWindow(ObservableCollection<Mr_lstud> lstudList)
        {
            var win = new LstudWindow();
            var vm = new LstudViewModel();
            vm.LstudList = lstudList;
            win.DataContext = vm;
            win.ShowDialog();
        }

        private bool ShowPdsNoteEditWindow(Pds_note note)
        {
            var win = new PdsNoteEditWindow();
            var vm = new PdsNoteEditViewModel();
            vm.Note = note;
            win.DataContext = vm;
            return (bool)win.ShowDialog();
        }

    }
}
