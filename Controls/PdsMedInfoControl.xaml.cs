using Lib;
using Models;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ViewModels;

namespace Controls
{
    /// <summary>
    /// PdsMedInfoControl.xaml 的互動邏輯
    /// </summary>
    public partial class PdsMedInfoControl : UserControl
    {
        public PdsMedInfoControl()
        {
            InitializeComponent();

            this.DataContextChanged += (sender, args) =>
            {
                var vm = args.NewValue as PdsMedInfo;
                if (vm != null && vm.ShowPdsNoteEditWindow == null)
                    vm.ShowPdsNoteEditWindow = ShowPdsNoteEditWindow;

                this.DrugInfoButton.IsEnabled = !string.IsNullOrWhiteSpace(vm?.ch_prs?.drug_info_url);
                this.ExpDataButton.IsEnabled = !string.IsNullOrWhiteSpace(vm?.exp_data);
                this.DrugNewsButton.IsEnabled = !string.IsNullOrWhiteSpace(vm?.ch_prs?.drug_news_url);
            };
        }

        private PdsMedInfo PdsMedInfo =>
             this.DataContext as PdsMedInfo;

        private void DrugInfoButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PdsMedInfo?.ch_prs?.drug_info_url))
            {
                //var startInfo = new ProcessStartInfo("Chrome.exe", PdsMedInfo.ch_prs.drug_info_url);
                //startInfo.CreateNoWindow = true;
                //Process p = Process.Start(startInfo);

                //var startInfo = new ProcessStartInfo();
                //startInfo.FileName = "Chrome.exe";
                //startInfo.Arguments = PdsMedInfo.ch_prs.drug_info_url;
                //startInfo.WindowStyle = ProcessWindowStyle.Maximized;
                //Process p = Process.Start(startInfo);

                Process.Start(PdsMedInfo.ch_prs.drug_info_url);
            }
        }

        private void ExpDataButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PdsMedInfo?.exp_data))
            {
                var win = new MsgWindow();
                win.Title = "健保規範";
                win.DataContext = PdsMedInfo.exp_data;
                win.ShowDialog();
            }
        }

        private void DrugNewsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PdsMedInfo?.ch_prs?.drug_news_url))
                Process.Start(PdsMedInfo.ch_prs.drug_news_url);
        }

        private void DrugImage_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (PdsMedInfo?.ch_prs?.pic_url?.GetType() != typeof(Byte[]) &&
                (!string.IsNullOrWhiteSpace(PdsMedInfo?.ch_prs?.pic_url.ToString())) &&
                (!PdsMedInfo.ch_prs.pic_url.IsBase64String()))
                Process.Start(PdsMedInfo.ch_prs.pic_url.ToString());
            e.Handled = true;
        }

        /// <summary>
        /// Button Click 後顯示 ToolTip，再次點選關閉 ToolTip
        /// </summary>
        private void ToolTipButton_Click(object sender, RoutedEventArgs e)
        {
            //e.Handled = true;
            var fe = sender as FrameworkElement;
            var toolTip = (fe.ToolTip as ToolTip);
            if (toolTip == null) return;
            if (toolTip.PlacementTarget == null)
                toolTip.PlacementTarget = fe;
            toolTip.IsOpen = !toolTip.IsOpen;
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
