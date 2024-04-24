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
    /// PdsRecFAdjustPage.xaml 的互動邏輯
    /// </summary>
    public partial class PdsRecFAdjustPage : Page
    {
        public PdsRecFAdjustPage()
        {
            InitializeComponent();
            PdsRecFAdjustViewModel.ShowPdsRecCancelWindow = ShowPdsRecCancelWindow;
            PdsRecFAdjustViewModel.ShowAtcCodeGroupWindow = ShowAtcCodeGroupWindow;
            PdsRecFAdjustViewModel.ShowLstudWindow = ShowLstudWindow;
            PdsRecFAdjustViewModel.ShowPdsNoteEditWindow = ShowPdsNoteEditWindow;
            this.Loaded += PdsRecFAdjust_Loaded;
        }

        private void PdsRecFAdjust_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if ( PdsRecFAdjustViewModel.LstCodeEnabled)
                this.LstCodeTextBox.Focus();
        }

        private PdsRecFAdjustViewModel PdsRecFAdjustViewModel =>
            this.DataContext as PdsRecFAdjustViewModel;

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
