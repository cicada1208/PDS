using System.Collections.Generic;
using System.Windows;
using Lib;

namespace Models
{
    public class Ch_bcode_codegrp : BaseModel<Ch_bcode_codegrp>
    {
        private string _code_group;
        /// <summary>
        /// 條碼分組
        /// </summary>
        public string code_group
        {
            get => _code_group;
            set => Set(ref _code_group, value);
        }

        private string _bcode_code_rx_dtm;
        /// <summary>
        /// 服藥日期時間
        /// </summary>
        public string bcode_code_rx_dtm
        {
            get => _bcode_code_rx_dtm;
            set
            {
                Set(ref _bcode_code_rx_dtm, value);
                _bcode_code_rx_dtmVisibility = _bcode_code_rx_dtm.IsNullOrWhiteSpace() ?
                    Visibility.Collapsed : Visibility.Visible;
            }
        }

        private Visibility _bcode_code_rx_dtmVisibility = Visibility.Collapsed;
        /// <summary>
        /// 服藥日期時間：Visibility
        /// </summary>
        public Visibility bcode_code_rx_dtmVisibility
        {
            get => _bcode_code_rx_dtmVisibility;
            set => Set(ref _bcode_code_rx_dtmVisibility, value);
        }

        private List<Ch_bcode> _medList;
        /// <summary>
        /// 藥品清單
        /// </summary>
        public List<Ch_bcode> medList
        {
            get => _medList;
            set => Set(ref _medList, value);
        }

        private List<string> _ni_pic_urlList;
        /// <summary>
        /// 裸碇藥品圖片清單
        /// </summary>
        public List<string> ni_pic_urlList
        {
            get => _ni_pic_urlList;
            set => Set(ref _ni_pic_urlList, value);
        }

    }
}
