using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfLib;

namespace ViewModels
{
    public class LoginViewModel : BaseViewModel<LoginViewModel>
    {
        /// <summary>
        /// Static Property 變更通知機制，通知 Binding 更新，此為委派事件
        /// </summary>
        public static event PropertyChangedEventHandler StaticPropertyChanged;
        /// <summary>
        ///  賦值並通知 Static Property 已經修改 Binding 更新，Static Property 同步方法，
        ///  需置於 Static Property 所在 class
        /// </summary>
        private static bool SetStatic<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(field, newValue))
            {
                field = newValue;
                StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }

        private static Mg_mnid _loginUser;
        public static Mg_mnid LoginUser
        {
            get => _loginUser;
            set => SetStatic(ref _loginUser, value);
        }

        private string _userId;
        [Display(Name = "使用者帳號")]
        public string UserId
        {
            get => _userId;
            set
            {
                Set(ref _userId, value);
                //LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private List<SysParameter> _sysParam;
        public List<SysParameter> SysParam
        {
            get
            {
                if (_sysParam == null)
                {
                    string service = RouteParam.Service();
                    string requestUri = RouteParam.SysParameter.QuerySysParameter; // 2: 依參數查詢(多筆)
                    SysParameter param = new SysParameter();
                    param.parameterName = "'CloseAD','SysAdmKey'";
                    var result = ApiUtil.HttpClientEx<ApiResult<List<SysParameter>>>(service, requestUri, param, new { option = 2 });
                    _sysParam = result.Data;
                }
                return _sysParam;
            }
            set => Set(ref _sysParam, value);
        }

        /// <summary>
        /// 登入成功，顯示主畫面
        /// </summary>
        public Action ShowMainWindow;


        private DelegateCommand<PasswordBox> _loginCommand;
        public DelegateCommand<PasswordBox> LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand<PasswordBox>
            (OnLogin, param => Validate().IsValid));
        private void OnLogin(PasswordBox passwordBox)
        {
            bool loginSucc;
            string loginMsg = string.Empty;
            Mg_mnid user = null;

            if (!Validate().IsValid) return;

            string closeAD = SysParam?.Find(p => p.parameterName == "CloseAD")?.value;
            string sysAdmKey = SysParam?.Find(p => p.parameterName == "SysAdmKey")?.value;
            bool passAD = (closeAD == "1") || (sysAdmKey == passwordBox.Password);

            loginSucc = passAD || ServiceUtil.ADProxy.VerifyUser(UserId);
            if (!loginSucc)
                loginMsg = MsgParam.LoginErrorId;
            else
            {
                loginSucc = passAD || ServiceUtil.ADProxy.Verify(UserId, passwordBox.Password);
                if (!loginSucc)
                    loginMsg = MsgParam.LoginErrorPw;
                else
                {
                    string server = RouteParam.Service();
                    string requestUri = RouteParam.Mg_mnid.QueryUser; // 1: 依員編，查詢人員資料
                    user = new Mg_mnid();
                    user.UserId = UserId;
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Mg_mnid>>>(server, requestUri, user, new { option = 1 });
                    user = result.Data?.FirstOrDefault();
                    if (!result.Succ)
                        loginMsg = result.Msg;
                    else if (user == null)
                        loginMsg = MsgParam.LoginNoData;
                    else if (user.Dimission == "Y")
                        loginMsg = MsgParam.LoginDimission;
                }
            }
            if (loginMsg != string.Empty)
                MessageBox.Show(loginMsg, MsgParam.TitlePrompt);
            else
            {
                LoginUser = user;
                ShowMainWindow?.Invoke();
            }
        }

        public void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            if (sender is PasswordBox)
                OnLogin(sender as PasswordBox);
            else
                CtrlUtil.KeyEnterMoveFocus(e);
        }

    }
}
