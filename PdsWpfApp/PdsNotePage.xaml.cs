using Controls;
using Models;
using System.Windows.Controls;
using ViewModels;

namespace PdsWpfApp
{
    /// <summary>
    /// PdsNotePage.xaml 的互動邏輯
    /// </summary>
    public partial class PdsNotePage : Page
    {
        public PdsNotePage()
        {
            InitializeComponent();
            PdsNoteViewModel.ShowPdsNoteEditWindow = ShowPdsNoteEditWindow;
        }

        private PdsNoteViewModel PdsNoteViewModel =>
            this.DataContext as PdsNoteViewModel;

        private bool ShowPdsNoteEditWindow(Pds_note note)
        {
            var win = new PdsNoteEditWindow();
            var vm = new PdsNoteEditViewModel();
            vm.SetNote(note);
            win.DataContext = vm;
            return (bool)win.ShowDialog();
        }

    }
}
