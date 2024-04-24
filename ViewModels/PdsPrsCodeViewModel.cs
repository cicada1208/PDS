using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfLib;

namespace ViewModels
{
    public class PdsPrsCodeViewModel : BaseViewModel<PdsPrsCodeViewModel>
    {
        private Ch_prs_code _selectedCode;
        /// <summary>
        /// 藥品條碼選取編輯
        /// </summary>
        public Ch_prs_code SelectedCode
        {
            get
            {
                if (_selectedCode == null)
                {
                    _selectedCode = new Ch_prs_code
                    {
                        prscode_invalid_dt = string.Empty,
                        prscode_st = string.Empty // 狀態：作用中
                    };
                }
                IsInsertEnabled = string.IsNullOrWhiteSpace(_selectedCode.prscode_no) ? false : true;
                IsUpdateEnabled = string.IsNullOrWhiteSpace(_selectedCode.prscode_no) ? true : false;
                return _selectedCode;
            }
            set => Set(ref _selectedCode, value);
        }

        private Ch_prs_code _filteredCode;
        /// <summary>
        /// 藥品條碼篩選條件
        /// </summary>
        public Ch_prs_code FilteredCode
        {
            get
            {
                if (_filteredCode == null)
                {
                    _filteredCode = new Ch_prs_code
                    {
                        prscode_invalid_dt = DateTime.Now.ToString("yyyy/MM/dd"),
                        prscode_st = string.Empty // 狀態：作用中
                    };
                }
                return _filteredCode;
            }
            set => Set(ref _filteredCode, value);
        }

        private ObservableCollection<Ch_prs_code> _codesList;
        /// <summary>
        /// 藥品條碼清單
        /// </summary>
        public ObservableCollection<Ch_prs_code> CodesList
        {
            get
            {
                if (_codesList == null)
                {
                    string service = RouteParam.Service();
                    string requestUri = RouteParam.Ch_prs_code.QueryCh_prs_code; // 4: 依非空值篩選
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Ch_prs_code>>>(
                        service, requestUri, FilteredCode, new { option = 4 });
                    _codesList = new ObservableCollection<Ch_prs_code>(result.Data);
                }
                return _codesList;
            }
            set => Set(ref _codesList, value);
        }

        private ObservableCollection<Ch_prs> _prsList;
        /// <summary>
        /// 處置藥品檔
        /// </summary>
        public ObservableCollection<Ch_prs> PrsList
        {
            get
            {
                if (_prsList == null)
                {
                    string service = RouteParam.Service();
                    string requestUri = RouteParam.Ch_prs.QueryCh_prs; // 2: 依處置類別藥品，查詢處置代碼、處置名稱一
                    var result = ApiUtil.HttpClientEx<ApiResult<List<Ch_prs>>>(
                        service, requestUri, queryParams: new { option = 2 });
                    result.Data = result.Data.OrderBy(p => p.chprs_mst_id).ToList();
                    _prsList = new ObservableCollection<Ch_prs>(result.Data);
                }
                return _prsList;
            }
            set => Set(ref _prsList, value);
        }

        private ObservableCollection<RecSt> _recStList;
        /// <summary>
        /// 記錄狀態
        /// </summary>
        public ObservableCollection<RecSt> RecStList
        {
            get
            {
                if (_recStList == null)
                {
                    _recStList = new ObservableCollection<RecSt>();
                    RecSt st = new RecSt();
                    st.Status = string.Empty; st.StatusName = "作用中"; _recStList.Add(st);
                    st = new RecSt();
                    st.Status = "D"; st.StatusName = "刪除"; _recStList.Add(st);
                }
                return _recStList;
            }
            set => Set(ref _recStList, value);
        }

        private bool _isInsertEnabled;
        /// <summary>
        /// 操作新增：是否Enabled
        /// </summary>
        public bool IsInsertEnabled
        {
            get => _isInsertEnabled;
            set => Set(ref _isInsertEnabled, value);
        }

        private bool _isUpdateEnabled;
        /// <summary>
        /// 操作修改：是否Enabled
        /// </summary>
        public bool IsUpdateEnabled
        {
            get => _isUpdateEnabled;
            set => Set(ref _isUpdateEnabled, value);
        }

        protected override string ExportTitle =>
            $"藥品條碼設定檔 {DateTime.Now.ToString("yyyyMMddHHmmss")}";


        private DelegateCommand<Ch_prs_code> _selectPrsCommand;
        public DelegateCommand<Ch_prs_code> SelectPrsCommand =>
            _selectPrsCommand ?? (_selectPrsCommand = new DelegateCommand<Ch_prs_code>
            (OnSelectPrs));
        /// <summary>
        /// 選取藥品代碼，顯示藥品名稱
        /// </summary>
        public void OnSelectPrs(Ch_prs_code code)
        {
            var prs = PrsList.FirstOrDefault(p => p.chprs_mst_id == code.prscode_mst_id);
            code.chprs_id_name = prs?.chprs_id_name ?? string.Empty;
        }

        private DelegateCommand<Ch_prs_code> _saveCommand;
        public DelegateCommand<Ch_prs_code> SaveCommand =>
            _saveCommand ?? (_saveCommand = new DelegateCommand<Ch_prs_code>
            (OnSave, param => Validate().IsValid));
        /// <summary>
        /// 存檔
        /// </summary>
        private void OnSave(Ch_prs_code code)
        {
            if (!Validate().IsValid) return;

            string service = RouteParam.Service();
            string requestUri = RouteParam.Ch_prs_code.SaveCh_prs_code;
            SelectedCode.prscode_md_man = LoginViewModel.LoginUser.UserId;
            SelectedCode.prscode_md_name = LoginViewModel.LoginUser.UserName;
            SelectedCode.prscode_md_pc = HostUtil.GetHostNameAndAddress();
            SelectedCode.prscode_md_ver = Application.Current.TryFindResource("AppVersion").NullableToStr();
            var result = ApiUtil.HttpClientEx<ApiResult<Ch_prs_code>>(
                service, requestUri, SelectedCode);

            MessageBox.Show(result.Msg, MsgParam.TitlePrompt);

            if (!string.IsNullOrWhiteSpace(SelectedCode.prscode_no))
            { // Update
                OnQuery(null);
            }
            else
            { //  Insert：prscode_no、prscode_code保持空白且其他欄位資訊保留，以便繼續新增
                SelectedCode.prscode_code = string.Empty;
                Ch_prs_code selectedCodeCopy = SelectedCode;
                OnQuery(null);
                SelectedCode = selectedCodeCopy;
            }
        }

        private DelegateCommand<Ch_prs_code> _insertCommand;
        public DelegateCommand<Ch_prs_code> InsertCommand =>
            _insertCommand ?? (_insertCommand = new DelegateCommand<Ch_prs_code>
            (OnInsert));
        /// <summary>
        /// 新增
        /// </summary>
        private void OnInsert(Ch_prs_code code) =>
            SelectedCode = null;

        private DelegateCommand<Ch_prs_code> _queryCommand;
        public DelegateCommand<Ch_prs_code> QueryCommand =>
            _queryCommand ?? (_queryCommand = new DelegateCommand<Ch_prs_code>
            (OnQuery));
        /// <summary>
        /// 查詢
        /// </summary>
        private void OnQuery(Ch_prs_code code) =>
            CodesList = null;

    }
}
