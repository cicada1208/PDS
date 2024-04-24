using System.Collections.Generic;

namespace Models
{
    public class Ch_bcode_daygrp : BaseModel<Ch_bcode_daygrp>
    {
        private string _day_group;
        /// <summary>
        /// 第幾天用藥分組
        /// </summary>
        public string day_group
        {
            get => _day_group;
            set => Set(ref _day_group, value);
        }

        private bool _isChecked =false;
        /// <summary>
        /// 確認選取
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set => Set(ref _isChecked, value);
        }

        private List<Ch_bcode_codegrp> _codeList;
        /// <summary>
        /// 條碼清單
        /// </summary>
        public List<Ch_bcode_codegrp> codeList
        {
            get => _codeList;
            set => Set(ref _codeList, value);
        }

    }
}
