//using GalaSoft.MvvmLight;
//using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfLib;

namespace ViewModels
{
    public class MvvmViewModel : BaseViewModel<MvvmViewModel>
    {
        //public string FirstName { get; set; }

        //public event PropertyChangedEventHandler PropertyChanged;

        // 通知 Property 已經修改，此為委派事件對應的方法，參數是要更新的屬性名稱
        //public void OnPropertyChanged(string propertyName) =>
        //            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// A token for a messaging channel.
        /// </summary>
        public static readonly Guid MessageToken = Guid.NewGuid();

        private string _title;
        [Display(Name = "標題")]
        public string Title
        {
            get => _title;
            set => Set(ref _title, value);
        }

        private ObservableCollection<Users> _userList;
        public ObservableCollection<Users> UserList
        {
            get
            {
                if (_userList == null)
                {
                    string service = RouteParam.Service();
                    string requestUri = RouteParam.Users.QueryUser; // 3: 依部門、狀態做用中、未離職
                    Users user = new Users();
                    user.employeeDept = "3240";
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Users>>>(
                        service, requestUri, user, new { option = 3 });
                    _userList = new ObservableCollection<Users>(result.Data);
                }
                return _userList;
            }
            set => Set(ref _userList, value);
        }

        private Users _selectedUser;
        public Users SelectedUser
        {
            get => _selectedUser;
            set => Set(ref _selectedUser, value, true);  // Send Message
        }


        //// 加入 Command 機制
        //public ICommand ChangeNameCommand { get; }

        private DelegateCommand<Users> _updateNameCommand;
        public DelegateCommand<Users> UpdateNameCommand =>
            _updateNameCommand ?? (_updateNameCommand =
            new DelegateCommand<Users>(OnUpdateName, CanUpdateName));
        private bool CanUpdateName(Users selectedUser = null)
        {
            return true;
            //return !string.IsNullOrWhiteSpace(selectedUser?.userId);
        }
        private void OnUpdateName(Users selectedUser = null)
        {
            // DataGrid SelectedValue:
            ////CommandParameter="{Binding SelectedValue, ElementName=UsersDataGrid}"
            ////<DataGrid SelectionMode="Single"  SelectionUnit="FullRow" SelectedValuePath="userName"

            if (selectedUser == null) return;
            string server = RouteParam.Service();
            string requestUri = RouteParam.Users.UpdateUser; // 1: 依參數自動組建 // 2: 儲存userTerseName、systemDt
            selectedUser.systemDt = System.DateTime.Now;
            var result = ApiUtil.HttpClientEx<ApiResult<Users>>(server, requestUri, selectedUser, new { option = 2 });
            MessageBox.Show(result.Msg);

            // Send Message
            MessengerInstance.Send(new NotificationMessage(this, "Send Message!"), MessageToken);
            MessengerInstance.Send<NotificationMessage<string>>(new NotificationMessage<string>(this, "Generic Value Content", "Notification Message"), MessageToken);
        }


        private DelegateCommand<IList<DataGridCellInfo>> _selectCellCommand;
        public DelegateCommand<IList<DataGridCellInfo>> SelectCellCommand =>
            _selectCellCommand ?? (_selectCellCommand =
            new DelegateCommand<IList<DataGridCellInfo>>(OnSelectCell));
        /// <summary>
        /// DataGrid select cell value
        /// </summary>
        public void OnSelectCell(IList<DataGridCellInfo> selectedCell = null)
        {
            if (selectedCell == null) return;
            DataGridCellInfo cellInfo = selectedCell.FirstOrDefault();
            if (!cellInfo.IsValid) return;
            FrameworkElement cellContent = cellInfo.Column.GetCellContent(cellInfo.Item);
            var text = (cellContent as TextBlock)?.Text;
            MessageBox.Show(text);
        }

        private DelegateCommand<SelectedCellsChangedEventArgs> _selectCellEventArgsCommand;
        public DelegateCommand<SelectedCellsChangedEventArgs> SelectCellEventArgsCommand =>
            _selectCellEventArgsCommand ?? (_selectCellEventArgsCommand =
            new DelegateCommand<SelectedCellsChangedEventArgs>(OnSelectCellEventArgs));
        /// <summary>
        /// DataGrid select cell value
        /// </summary>
        private void OnSelectCellEventArgs(SelectedCellsChangedEventArgs e = null) =>
            OnSelectCell(e?.AddedCells);

        /// <summary>
        /// DataGrid select cell value
        /// </summary>
        public void UsersDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e) =>
            OnSelectCell(e?.AddedCells);

    }
}
