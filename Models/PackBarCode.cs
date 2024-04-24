using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PackBarCode : BaseModel<PackBarCode>
    {
        private string _barcode;
        public string barcode
        {
            get => _barcode;
            set => Set(ref _barcode, value);
        }

        private string _mst_id;
        public string mst_id
        {
            get => _mst_id;
            set => Set(ref _mst_id, value);
        }

        private string _uqty1;
        public string uqty1
        {
            get => _uqty1;
            set => Set(ref _uqty1, value);
        }

        private string _uqty2;
        public string uqty2
        {
            get => _uqty2;
            set => Set(ref _uqty2, value);
        }

        private string _expdt;
        /// <summary>
        /// 效期 yyyyMMdd
        /// </summary>
        public string expdt
        {
            get => _expdt;
            set => Set(ref _expdt, value);
        }

        private string _expdt_fmt;
        /// <summary>
        /// 效期 yyyy/MM/dd
        /// </summary>
        public string expdt_fmt
        {
            get => _expdt_fmt;
            set => Set(ref _expdt_fmt, value);
        }

    }
}
