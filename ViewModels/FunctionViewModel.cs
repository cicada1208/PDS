using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using WpfLib;

namespace ViewModels
{
    public class FunctionViewModel : BaseViewModel<FunctionViewModel>
    {
        private ObservableCollection<Function> _funcList;
        public ObservableCollection<Function> FuncList
        {
            get
            {
                if (_funcList == null)
                {
                    List<Function> funcList = new List<Function>();
                    Function funcGroup;

                    funcGroup = new Function();
                    funcGroup.Title = "藥車作業";
                    funcGroup.Functions = new List<Function>();
                    AddFunc(funcGroup, "藥車調劑", "PdsRecAdjustPage", "PdsRecAdjustPage");
                    AddFunc(funcGroup, "PSY 藥車啟動核對", "Controls.PdsUdrecChkInsertPsyWindow",
                        "Controls.PdsUdrecChkInsertPsyWindow", FunctionParam.FuncType.InnerWin);
                    AddFunc(funcGroup, "藥車啟動核對", "Controls.PdsUdrecChkInsertWindow",
                        "Controls.PdsUdrecChkInsertWindow", FunctionParam.FuncType.InnerWin);
                    AddFunc(funcGroup, "藥車核對", "PdsRecCheckPage", "PdsRecCheckPage");
                    AddFunc(funcGroup, "藥車配藥單未完成明細", "PdsRecLstuPage", "PdsRecLstuPage");
                    AddFunc(funcGroup, "藥車藥袋未完成明細", "PdsRecBaguPage", "PdsRecBaguPage");
                    funcList.Add(funcGroup);

                    funcGroup = new Function();
                    funcGroup.Title = "首日量作業";
                    funcGroup.Functions = new List<Function>();
                    AddFunc(funcGroup, "首日量調劑", "PdsRecFAdjustPage", "PdsRecFAdjustPage");
                    AddFunc(funcGroup, "首日量核發", "PdsRecFCheckPage", "PdsRecFCheckPage"); // 首日量核對
                    // 2024.01.24 因藥局人力不足暫將發藥併入首日量核對作業
                    //AddFunc(funcGroup, "首日量發藥", "PdsRecFReleasePage", "PdsRecFReleasePage");
                    AddFunc(funcGroup, "首日量藥袋未完成明細", "PdsRecFBaguPage", "PdsRecFBaguPage");
                    funcList.Add(funcGroup);

                    funcGroup = new Function();
                    funcGroup.Title = "藥師個人筆記本";
                    funcGroup.Type = FunctionParam.FuncType.InnerPage;
                    funcGroup.Content ="PdsNotePage";
                    funcGroup.Name = "PdsNotePage";
                    funcList.Add(funcGroup);

                    funcGroup = new Function();
                    funcGroup.Title = "報表";
                    funcGroup.Functions = new List<Function>();
                    AddFunc(funcGroup, "錯誤/取消明細", "PdsRecdStncPage", "PdsRecdStncPage");
                    AddFunc(funcGroup, "統計明細", "PdsRecStatisticPage", "PdsRecStatisticPage");
                    AddFunc(funcGroup, "首日量平均時間", "PdsRecFstAvgPage", "PdsRecFstAvgPage");
                    funcList.Add(funcGroup);

                    funcGroup = new Function();
                    funcGroup.Title = "設定維護作業";
                    funcGroup.Functions = new List<Function>();
                    AddFunc(funcGroup, "藥品條碼建置作業", "PdsPrsCodePage", "PdsPrsCodePage");
                    funcList.Add(funcGroup);

                    _funcList = new ObservableCollection<Function>(funcList);
                }
                return _funcList;
            }
            set => Set(ref _funcList, value);
        }

        private ObservableCollection<Function> _contentFuncList;
        public ObservableCollection<Function> ContentFuncList
        {
            get => _contentFuncList ?? (_contentFuncList = new ObservableCollection<Function>());
            set => Set(ref _contentFuncList, value);
        }

        private Function _selectedFunc;
        public Function SelectedFunc
        {
            get => _selectedFunc;
            set => Set(ref _selectedFunc, value);
        }


        private void AddFunc(Function funcGroup, string title, object content, string name,
            FunctionParam.FuncType type = Params.FunctionParam.FuncType.InnerPage)
        {
            Function func = new Function();
            func.Title = title;
            func.Type = type;
            func.Content = content;
            func.Name = name;
            funcGroup.Functions.Add(func);
        }

        ///// <summary>
        ///// 顯示畫面
        ///// </summary>
        //public Func<string, bool?> ShowWindow;

        private DelegateCommand<Function> _selectFuncCommand;
        public DelegateCommand<Function> SelectFuncCommand =>
            _selectFuncCommand ?? (_selectFuncCommand = new DelegateCommand<Function>(OnSelectFunc));
        private void OnSelectFunc(Function selectedFunc = null)
        {
            if (selectedFunc == null) return;
            if (ContentFuncList.Contains(selectedFunc))
                SelectedFunc = selectedFunc;
            else
            {
                if (selectedFunc.Type == FunctionParam.FuncType.None ||
                    selectedFunc.Content.NullableToStr() == string.Empty) return;
                switch (selectedFunc.Type)
                {
                    case FunctionParam.FuncType.InnerPage:
                        Frame frame = new Frame();
                        frame.Navigate(new Uri($"{selectedFunc.Content}.xaml", UriKind.RelativeOrAbsolute));
                        selectedFunc.ContentInstance = frame;
                        ContentFuncList.Add(selectedFunc);
                        SelectedFunc = selectedFunc;
                        break;
                    case FunctionParam.FuncType.InnerWin:
                        //ShowWindow?.Invoke(selectedFunc.Content.NullableToStr());
                        Window win = Util.Ctrl.GetWindow(selectedFunc.Content.NullableToStr(), selectedFunc.Title);
                        win?.ShowDialog();
                        win?.Close();
                        break;
                    default:
                        // 未實做
                        break;
                }
            }
        }

        private DelegateCommand<Function> _closeFuncCommand;
        public DelegateCommand<Function> CloseFuncCommand =>
             _closeFuncCommand ?? (_closeFuncCommand = new DelegateCommand<Function>(OnCloseFunc));
        private void OnCloseFunc(Function selectedFunc = null)
        {
            if (selectedFunc == null) return;
            selectedFunc.DisposeContentInstance();
            ContentFuncList.Remove(selectedFunc);
        }

    }
}
