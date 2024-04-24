using Params;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace Models
{
    public class Function : BaseModel<Function>
    {
        private string _Name;
        /// <summary>
        /// 唯一識別名稱
        /// </summary>
        public string Name
        {
            get => _Name;
            set => Set(ref _Name, value);
        }

        private string _Title;
        /// <summary>
        /// 描述
        /// </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }

        private FunctionParam.FuncType _Type;
        /// <summary>
        /// 功能類型
        /// </summary>
        public FunctionParam.FuncType Type
        {
            get => _Type;
            set => Set(ref _Type, value);
        }

        private object _Content;
        /// <summary>
        /// 內容位址
        /// </summary>
        public object Content
        {
            get => _Content;
            set => Set(ref _Content, value);
        }

        private object _ContentInstance;
        /// <summary>
        /// 內容實例
        /// </summary>
        public object ContentInstance
        {
            //get
            //{
            //    if (_ContentInstance == null)
            //    {
            //        if (!string.IsNullOrWhiteSpace(Content as string))
            //            switch (Type)
            //            {
            //                case FunctionParam.FuncType.None:
            //                    break;
            //                case FunctionParam.FuncType.InnerPage:
            //                    Frame frame = new Frame();
            //                    frame.Navigate(new Uri($"{Content}.xaml", UriKind.RelativeOrAbsolute));
            //                    _ContentInstance = frame;
            //                    break;
            //            }
            //    }
            //    return _ContentInstance;
            //}
            get => _ContentInstance;
            set => Set(ref _ContentInstance, value);
        }

        /// <summary>
        /// 釋放內容實例
        /// </summary>
        public void DisposeContentInstance() =>
            _ContentInstance = null;

        /// <summary>
        /// 群組子功能
        /// </summary>
        public List<Function> Functions { get; set; }
    }
}
