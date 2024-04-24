using Controls;
using Models;
using Params;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// PdsRecCheckPage.xaml 的互動邏輯
    /// </summary>
    public partial class PdsRecCheckPage : Page
    {
        public PdsRecCheckPage()
        {
            InitializeComponent();
            PdsRecCheckViewModel.ShowPdsRecCancelWindow = ShowPdsRecCancelWindow;
            PdsRecCheckViewModel.ShowAtcCodeGroupWindow = ShowAtcCodeGroupWindow;
            PdsRecCheckViewModel.ShowLstudWindow = ShowLstudWindow;
            PdsRecCheckViewModel.ShowPdsNoteEditWindow = ShowPdsNoteEditWindow;
            this.Loaded += PdsRecCheck_Loaded;
        }

        private void PdsRecCheck_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (PdsRecCheckViewModel.LstCodeEnabled)
                this.LstCodeTextBox.Focus();
        }

        private PdsRecCheckViewModel PdsRecCheckViewModel =>
            this.DataContext as PdsRecCheckViewModel;

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

        private bool MedInfExpand = false;

        private void MedInfGroupBox_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MedInfExpand = !MedInfExpand;
            if (!MedInfExpand)
            {
                ClinicalRow.Height = new GridLength(1, GridUnitType.Auto);
                LstCodeRow.Height = new GridLength(1, GridUnitType.Auto);
                BagCodeRow.Height = new GridLength(1, GridUnitType.Auto);
                PatInfRow.Height = new GridLength(1, GridUnitType.Auto);
                OrderListColumn.Width = new GridLength(0.15, GridUnitType.Star);
                MedInfColumn.Width = new GridLength(0.7, GridUnitType.Star);
                ToolColumn.Width = new GridLength(0.15, GridUnitType.Star);
            }
            else
            {
                ClinicalRow.Height = new GridLength(0, GridUnitType.Pixel);
                LstCodeRow.Height = new GridLength(0, GridUnitType.Pixel);
                BagCodeRow.Height = new GridLength(1, GridUnitType.Auto);
                PatInfRow.Height = new GridLength(0, GridUnitType.Pixel);
                OrderListColumn.Width = new GridLength(0, GridUnitType.Pixel);
                ToolColumn.Width = new GridLength(0, GridUnitType.Pixel);
            }
        }

    }
}
