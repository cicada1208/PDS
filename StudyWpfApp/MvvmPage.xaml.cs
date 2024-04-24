using Lib;
using System.Windows;
using System.Windows.Controls;
using ViewModels;

namespace StudyWpfApp
{
    /// <summary>
    /// MvvmPage.xaml 的互動邏輯
    /// </summary>
    public partial class MvvmPage : Page
    {
        public MvvmPage()
        {
            //// code-behind binding View DataContext
            //StudyViewModel studyViewModel = new StudyViewModel();
            //studyViewModel.FirstName = "嘉基";
            //this.DataContext = studyViewModel;

            InitializeComponent();

            //studyViewModel.FirstName = "test"; // 此行於 InitializeComponent() 之後，需有通知機制，才會更新於UI
            //// 改以 SetProperty 設定並通知，不用手動
            ////studyViewModel.OnPropertyChanged(nameof(StudyViewModel.FirstName));

            // code-behind 讀取資源
            string pageResStr = TryFindResource("PageResStr").NullableToStr();
            string appResStr = Application.Current.TryFindResource("AppResStr").NullableToStr();
            ResourceTextBox.Style = Application.Current.TryFindResource("TextBoxStyle") as Style;
        }

        private ValidationViewModel _validationViewModel;
        private ValidationViewModel ValidationViewModel =>
            _validationViewModel ?? (_validationViewModel = TryFindResource("ValidationViewModel") as ValidationViewModel);

        private MvvmViewModel MvvmViewModel =>
             this.DataContext as MvvmViewModel;


        /// <summary>
        /// DataGrid select cell value
        /// </summary>
        private void UsersDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) =>
            MvvmViewModel.OnSelectCell(e.AddedCells);

    }
}
