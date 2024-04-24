using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class FstAvg : BaseModel<FstAvg>
    {
        private int? _icfcode_prt_dt_begin;
        public int? icfcode_prt_dt_begin
        {
            get => _icfcode_prt_dt_begin;
            set => Set(ref _icfcode_prt_dt_begin, value);
        }

        private int? _icfcode_prt_dt_end;
        public int? icfcode_prt_dt_end
        {
            get => _icfcode_prt_dt_end;
            set => Set(ref _icfcode_prt_dt_end, value);
        }

        private string _icfcode_prt_dt_begin_fmt;
        public string icfcode_prt_dt_begin_fmt
        {
            get => _icfcode_prt_dt_begin_fmt;
            set => Set(ref _icfcode_prt_dt_begin_fmt, value);
        }

        private string _icfcode_prt_dt_end_fmt;
        public string icfcode_prt_dt_end_fmt
        {
            get => _icfcode_prt_dt_end_fmt;
            set => Set(ref _icfcode_prt_dt_end_fmt, value);
        }

        private string _pds_rec_op_name;
        /// <summary>
        /// 階段
        /// </summary>
        [Display(Name = "階段")]
        public string pds_rec_op_name
        {
            get => _pds_rec_op_name;
            set => Set(ref _pds_rec_op_name, value);
        }

        private string _icfcode_lst_type;
        /// <summary>
        /// 配藥單類型
        /// </summary>
        public string icfcode_lst_type
        {
            get => _icfcode_lst_type;
            set => Set(ref _icfcode_lst_type, value);
        }

        private string _dt;
        /// <summary>
        /// 日期 yyyy/MM/dd
        /// </summary>
        [Display(Name = "日期")]
        public string dt
        {
            get => _dt;
            set => Set(ref _dt, value);
        }

        private string _dayofWeek;
        /// <summary>
        /// 星期
        /// </summary>
        [Display(Name = "星期")]
        public string dayofWeek
        {
            get => _dayofWeek;
            set => Set(ref _dayofWeek, value);
        }


        private double? _hr0000;
        /// <summary>
        /// 區間 00:00:00 ＜＝ x ＜ 00:30:00
        /// </summary>
        [Display(Name = "00:00-00:30")]
        public double? hr0000
        {
            get => _hr0000;
            set => Set(ref _hr0000, value);
        }

        private double? _hr0030;
        [Display(Name = "00:30-01:00")]
        public double? hr0030
        {
            get => _hr0030;
            set => Set(ref _hr0030, value);
        }

        private double? _hr0100;
        [Display(Name = "01:00-01:30")]
        public double? hr0100
        {
            get => _hr0100;
            set => Set(ref _hr0100, value);
        }

        private double? _hr0130;
        [Display(Name = "01:30-02:00")]
        public double? hr0130
        {
            get => _hr0130;
            set => Set(ref _hr0130, value);
        }

        private double? _hr0200;
        [Display(Name = "02:00-02:30")]
        public double? hr0200
        {
            get => _hr0200;
            set => Set(ref _hr0200, value);
        }

        private double? _hr0230;
        [Display(Name = "02:30-03:00")]
        public double? hr0230
        {
            get => _hr0230;
            set => Set(ref _hr0230, value);
        }

        private double? _hr0300;
        [Display(Name = "03:00-03:30")]
        public double? hr0300
        {
            get => _hr0300;
            set => Set(ref _hr0300, value);
        }

        private double? _hr0330;
        [Display(Name = "03:30-04:00")]
        public double? hr0330
        {
            get => _hr0330;
            set => Set(ref _hr0330, value);
        }

        private double? _hr0400;
        [Display(Name = "04:00-04:30")]
        public double? hr0400
        {
            get => _hr0400;
            set => Set(ref _hr0400, value);
        }

        private double? _hr0430;
        [Display(Name = "04:30-05:00")]
        public double? hr0430
        {
            get => _hr0430;
            set => Set(ref _hr0430, value);
        }

        private double? _hr0500;
        [Display(Name = "05:00-05:30")]
        public double? hr0500
        {
            get => _hr0500;
            set => Set(ref _hr0500, value);
        }

        private double? _hr0530;
        [Display(Name = "05:30-06:00")]
        public double? hr0530
        {
            get => _hr0530;
            set => Set(ref _hr0530, value);
        }

        private double? _hr0600;
        [Display(Name = "06:00-06:30")]
        public double? hr0600
        {
            get => _hr0600;
            set => Set(ref _hr0600, value);
        }

        private double? _hr0630;
        [Display(Name = "06:30-07:00")]
        public double? hr0630
        {
            get => _hr0630;
            set => Set(ref _hr0630, value);
        }

        private double? _hr0700;
        [Display(Name = "07:00-07:30")]
        public double? hr0700
        {
            get => _hr0700;
            set => Set(ref _hr0700, value);
        }

        private double? _hr0730;
        [Display(Name = "07:30-08:00")]
        public double? hr0730
        {
            get => _hr0730;
            set => Set(ref _hr0730, value);
        }

        private double? _hr0800;
        [Display(Name = "08:00-08:30")]
        public double? hr0800
        {
            get => _hr0800;
            set => Set(ref _hr0800, value);
        }

        private double? _hr0830;
        [Display(Name = "08:30-09:00")]
        public double? hr0830
        {
            get => _hr0830;
            set => Set(ref _hr0830, value);
        }

        private double? _hr0900;
        [Display(Name = "09:00-09:30")]
        public double? hr0900
        {
            get => _hr0900;
            set => Set(ref _hr0900, value);
        }

        private double? _hr0930;
        [Display(Name = "09:30-10:00")]
        public double? hr0930
        {
            get => _hr0930;
            set => Set(ref _hr0930, value);
        }

        private double? _hr1000;
        [Display(Name = "10:00-10:30")]
        public double? hr1000
        {
            get => _hr1000;
            set => Set(ref _hr1000, value);
        }

        private double? _hr1030;
        [Display(Name = "10:30-11:00")]
        public double? hr1030
        {
            get => _hr1030;
            set => Set(ref _hr1030, value);
        }

        private double? _hr1100;
        [Display(Name = "11:00-11:30")]
        public double? hr1100
        {
            get => _hr1100;
            set => Set(ref _hr1100, value);
        }

        private double? _hr1130;
        [Display(Name = "11:30-12:00")]
        public double? hr1130
        {
            get => _hr1130;
            set => Set(ref _hr1130, value);
        }

        private double? _hr1200;
        [Display(Name = "12:00-12:30")]
        public double? hr1200
        {
            get => _hr1200;
            set => Set(ref _hr1200, value);
        }

        private double? _hr1230;
        [Display(Name = "12:30-13:00")]
        public double? hr1230
        {
            get => _hr1230;
            set => Set(ref _hr1230, value);
        }

        private double? _hr1300;
        [Display(Name = "13:00-13:30")]
        public double? hr1300
        {
            get => _hr1300;
            set => Set(ref _hr1300, value);
        }

        private double? _hr1330;
        [Display(Name = "13:30-14:00")]
        public double? hr1330
        {
            get => _hr1330;
            set => Set(ref _hr1330, value);
        }

        private double? _hr1400;
        [Display(Name = "14:00-14:30")]
        public double? hr1400
        {
            get => _hr1400;
            set => Set(ref _hr1400, value);
        }

        private double? _hr1430;
        [Display(Name = "14:30-15:00")]
        public double? hr1430
        {
            get => _hr1430;
            set => Set(ref _hr1430, value);
        }

        private double? _hr1500;
        [Display(Name = "15:00-15:30")]
        public double? hr1500
        {
            get => _hr1500;
            set => Set(ref _hr1500, value);
        }

        private double? _hr1530;
        [Display(Name = "15:30-16:00")]
        public double? hr1530
        {
            get => _hr1530;
            set => Set(ref _hr1530, value);
        }

        private double? _hr1600;
        [Display(Name = "16:00-16:30")]
        public double? hr1600
        {
            get => _hr1600;
            set => Set(ref _hr1600, value);
        }

        private double? _hr1630;
        [Display(Name = "16:30-17:00")]
        public double? hr1630
        {
            get => _hr1630;
            set => Set(ref _hr1630, value);
        }

        private double? _hr1700;
        [Display(Name = "17:00-17:30")]
        public double? hr1700
        {
            get => _hr1700;
            set => Set(ref _hr1700, value);
        }

        private double? _hr1730;
        [Display(Name = "17:30-18:00")]
        public double? hr1730
        {
            get => _hr1730;
            set => Set(ref _hr1730, value);
        }

        private double? _hr1800;
        [Display(Name = "18:00-18:30")]
        public double? hr1800
        {
            get => _hr1800;
            set => Set(ref _hr1800, value);
        }

        private double? _hr1830;
        [Display(Name = "18:30-19:00")]
        public double? hr1830
        {
            get => _hr1830;
            set => Set(ref _hr1830, value);
        }

        private double? _hr1900;
        [Display(Name = "19:00-19:30")]
        public double? hr1900
        {
            get => _hr1900;
            set => Set(ref _hr1900, value);
        }

        private double? _hr1930;
        [Display(Name = "19:30-20:00")]
        public double? hr1930
        {
            get => _hr1930;
            set => Set(ref _hr1930, value);
        }

        private double? _hr2000;
        [Display(Name = "20:00-20:30")]
        public double? hr2000
        {
            get => _hr2000;
            set => Set(ref _hr2000, value);
        }

        private double? _hr2030;
        [Display(Name = "20:30-21:00")]
        public double? hr2030
        {
            get => _hr2030;
            set => Set(ref _hr2030, value);
        }

        private double? _hr2100;
        [Display(Name = "21:00-21:30")]
        public double? hr2100
        {
            get => _hr2100;
            set => Set(ref _hr2100, value);
        }

        private double? _hr2130;
        [Display(Name = "21:30-22:00")]
        public double? hr2130
        {
            get => _hr2130;
            set => Set(ref _hr2130, value);
        }

        private double? _hr2200;
        [Display(Name = "22:00-22:30")]
        public double? hr2200
        {
            get => _hr2200;
            set => Set(ref _hr2200, value);
        }

        private double? _hr2230;
        [Display(Name = "22:30-23:00")]
        public double? hr2230
        {
            get => _hr2230;
            set => Set(ref _hr2230, value);
        }

        private double? _hr2300;
        [Display(Name = "23:00-23:30")]
        public double? hr2300
        {
            get => _hr2300;
            set => Set(ref _hr2300, value);
        }

        private double? _hr2330;
        [Display(Name = "23:30-00:00")]
        public double? hr2330
        {
            get => _hr2330;
            set => Set(ref _hr2330, value);
        }

    }
}
