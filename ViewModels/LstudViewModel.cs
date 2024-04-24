using Models;
using System.Collections.ObjectModel;

namespace ViewModels
{
    public class LstudViewModel : BaseViewModel<LstudViewModel>
    {
        private ObservableCollection<Mr_lstud> _lstudList;
        /// <summary>
        /// 交互作用清單
        /// </summary>
        public ObservableCollection<Mr_lstud> LstudList
        {
            get => _lstudList;
            set => Set(ref _lstudList, value);
        }

        private Mr_lstud _selectedLstud;
        /// <summary>
        /// 交互作用選取
        /// </summary>
        public Mr_lstud SelectedLstud
        {
            get => _selectedLstud;
            set => Set(ref _selectedLstud, value);
        }

    }

}
