using Lib;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WpfLib;

namespace Models
{
    public class PdsMedInfo : BaseModel<PdsMedInfo>
    {
        private PdsMedInfoParam.OpMode _opMode;
        /// <summary>
        /// 操作模式
        /// </summary>
        public PdsMedInfoParam.OpMode opMode
        {
            get => _opMode;
            set
            {
                Set(ref _opMode, value);
                SetMode();
            }
        }

        private bool _chkMode = false;
        /// <summary>
        /// 核對模式：核對 true；非核對 false
        /// </summary>
        public bool chkMode
        {
            get => _chkMode;
            set => Set(ref _chkMode, value);
        }

        private Mi_micbcode _mi_micbcode;
        public Mi_micbcode mi_micbcode
        {
            get => _mi_micbcode ?? (_mi_micbcode = new Mi_micbcode());
            set => Set(ref _mi_micbcode, value);
        }

        private Ch_prs _ch_prs;
        public Ch_prs ch_prs
        {
            get => _ch_prs ?? (_ch_prs = new Ch_prs());
            set => Set(ref _ch_prs, value);
        }

        private string _exp_data;
        /// <summary>
        /// 院內藥典 健保規範
        /// </summary>
        public string exp_data
        {
            get => _exp_data;
            set => Set(ref _exp_data, value);
        }

        private Pds_recd _mdInfo;
        /// <summary>
        /// 最新處方修改資訊
        /// </summary>
        public Pds_recd mdInfo
        {
            get => _mdInfo;
            set => Set(ref _mdInfo, value);
        }

        private string _icbcode_rx_way1_final;
        /// <summary>
        /// 頻次顯示(修改後)
        /// </summary>
        public string icbcode_rx_way1_final
        {
            get => _icbcode_rx_way1_final;
            set => Set(ref _icbcode_rx_way1_final, value);
        }

        private string _icbcode_rx_qty_final;
        /// <summary>
        /// 總量顯示(修改後)
        /// </summary>
        public string icbcode_rx_qty_final
        {
            get => _icbcode_rx_qty_final;
            set => Set(ref _icbcode_rx_qty_final, value);
        }

        private string _icbcode_pack_final;
        /// <summary>
        /// 總包顯示(磨粉分包，修改後)
        /// </summary>
        public string icbcode_pack_final
        {
            get => _icbcode_pack_final;
            set => Set(ref _icbcode_pack_final, value);
        }

        private bool _chprs_id_nameChecked = false;
        /// <summary>
        /// 處置名稱一：Checked
        /// </summary>
        public bool chprs_id_nameChecked
        {
            get => _chprs_id_nameChecked;
            set => Set(ref _chprs_id_nameChecked, value);
        }

        private bool _icbcode_rx_uqtyChecked = false;
        /// <summary>
        /// 次劑量：Checked
        /// </summary>
        public bool icbcode_rx_uqtyChecked
        {
            get => _icbcode_rx_uqtyChecked;
            set => Set(ref _icbcode_rx_uqtyChecked, value);
        }

        private bool _chprs_multi_typeChecked = false;
        /// <summary>
        /// 多劑型：Checked
        /// </summary>
        public bool chprs_multi_typeChecked
        {
            get => _chprs_multi_typeChecked;
            set => Set(ref _chprs_multi_typeChecked, value);
        }

        private bool _chprs_multi_doseChecked = false;
        /// <summary>
        ///  多劑量：Checked
        /// </summary>
        public bool chprs_multi_doseChecked
        {
            get => _chprs_multi_doseChecked;
            set => Set(ref _chprs_multi_doseChecked, value);
        }

        private bool _icbcode_rx_qtyChecked = false;
        /// <summary>
        /// 加總總量：Checked
        /// </summary>
        public bool icbcode_rx_qtyChecked
        {
            get => _icbcode_rx_qtyChecked;
            set => Set(ref _icbcode_rx_qtyChecked, value);
        }

        private bool _icbcode_packChecked = false;
        /// <summary>
        /// 加總包數(磨粉分包)：Checked
        /// </summary>
        public bool icbcode_packChecked
        {
            get => _icbcode_packChecked;
            set => Set(ref _icbcode_packChecked, value);
        }

        private bool _qty_packEnabled = false;
        /// <summary>
        /// 加總總量 ＆ 加總包數(磨粉分包) ToggleButton：Enabled
        /// </summary>
        public bool qty_packEnabled
        {
            get => _qty_packEnabled;
            set => Set(ref _qty_packEnabled, value);
        }

        private string _qty_packText;
        /// <summary>
        /// 加總總量 ＆ 加總包數(磨粉分包) TextBox：Text
        /// </summary>
        public string qty_packText
        {
            get => _qty_packText;
            set => Set(ref _qty_packText, value?.Trim());
        }

        private Brush _qty_packColor;
        /// <summary>
        /// 加總總量 ＆ 加總包數(磨粉分包) TextBox：Color
        /// </summary>
        public Brush qty_packColor
        {
            get => _qty_packColor ?? (_qty_packColor = Application.Current?.TryFindResource("Color.ToggleButton.Enabled.Background") as Brush);
            set => Set(ref _qty_packColor, value);
        }

        private Visibility _qty_packTextVisibility = Visibility.Collapsed;
        /// <summary>
        /// 加總總量 ＆ 加總包數(磨粉分包) TextBox：Visibility
        /// </summary>
        public Visibility qty_packTextVisibility
        {
            get => _qty_packTextVisibility;
            set => Set(ref _qty_packTextVisibility, value);
        }

        private Visibility _icbcode_rx_qty_valVisibility = Visibility.Collapsed;
        /// <summary>
        /// 加總總量：Visibility
        /// </summary>
        public Visibility icbcode_rx_qty_valVisibility
        {
            get => _icbcode_rx_qty_valVisibility;
            set => Set(ref _icbcode_rx_qty_valVisibility, value);
        }

        private Visibility _icbcode_pack_valVisibility = Visibility.Collapsed;
        /// <summary>
        /// 加總包數(磨粉分包)：Visibility
        /// </summary>
        public Visibility icbcode_pack_valVisibility
        {
            get => _icbcode_pack_valVisibility;
            set => Set(ref _icbcode_pack_valVisibility, value);
        }


        private void SetMode()
        {
            switch (opMode)
            {
                case PdsMedInfoParam.OpMode.A:
                    chkMode = false;
                    ch_prs.chprs_multi_typeVisibility = Visibility.Collapsed;
                    ch_prs.chprs_multi_doseVisibility = Visibility.Collapsed;
                    qty_packEnabled = chkMode;
                    qty_packTextVisibility = Visibility.Collapsed;
                    break;
                case PdsMedInfoParam.OpMode.C:
                    chkMode = true;
                    ch_prs.chprs_multi_typeVisibility = ch_prs.chprs_multi_type == "Y" ? Visibility.Visible : Visibility.Collapsed;
                    ch_prs.chprs_multi_doseVisibility = ch_prs.chprs_multi_dose == "Y" ? Visibility.Visible : Visibility.Collapsed;
                    // 藥車核對改成同首日量核對，總量手輸檢核
                    //qty_packEnabled = chkMode;
                    //qty_packTextVisibility = Visibility.Collapsed;
                    qty_packEnabled = false;
                    qty_packTextVisibility = qty_packEnabled ? Visibility.Collapsed : Visibility.Visible;
                    break;
                case PdsMedInfoParam.OpMode.FA:
                case PdsMedInfoParam.OpMode.FR:
                    chkMode = false;
                    ch_prs.chprs_multi_typeVisibility = ch_prs.chprs_multi_type == "Y" ? Visibility.Visible : Visibility.Collapsed;
                    ch_prs.chprs_multi_doseVisibility = ch_prs.chprs_multi_dose == "Y" ? Visibility.Visible : Visibility.Collapsed;
                    qty_packEnabled = chkMode;
                    qty_packTextVisibility = Visibility.Collapsed;
                    break;
                case PdsMedInfoParam.OpMode.FC:
                    chkMode = true;
                    ch_prs.chprs_multi_typeVisibility = ch_prs.chprs_multi_type == "Y" ? Visibility.Visible : Visibility.Collapsed;
                    ch_prs.chprs_multi_doseVisibility = ch_prs.chprs_multi_dose == "Y" ? Visibility.Visible : Visibility.Collapsed;
                    qty_packEnabled = false;
                    qty_packTextVisibility = qty_packEnabled ? Visibility.Collapsed : Visibility.Visible;
                    break;
                case PdsMedInfoParam.OpMode.NONE:
                    chkMode = false;
                    qty_packEnabled = chkMode;
                    qty_packTextVisibility = Visibility.Collapsed;
                    break;
            }

            mi_micbcode.icbcode_rx_uqtyEnabled = (chkMode && mi_micbcode.icbcode_med_type_p) ? true : false;
            mi_micbcode.icbcode_rx_qtyVisibility = mi_micbcode.icbcode_med_type_p ? Visibility.Collapsed : Visibility.Visible;
            mi_micbcode.icbcode_packVisibility = mi_micbcode.icbcode_med_type_p ? Visibility.Visible : Visibility.Collapsed;
            icbcode_rx_qty_valVisibility = mi_micbcode.icbcode_rx_qtyVisibility;
            icbcode_pack_valVisibility = mi_micbcode.icbcode_packVisibility;
            // 藥車核對改成同首日量核對，總量手輸檢核
            if (opMode == PdsMedInfoParam.OpMode.FC ||
                opMode == PdsMedInfoParam.OpMode.C)
            {
                icbcode_rx_qty_valVisibility = Visibility.Collapsed;
                icbcode_pack_valVisibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 核對藥品儲存
        /// </summary>
        public Action SaveMEDV;

        /// <summary>
        /// 顯示通報畫面
        /// </summary>
        public Func<Pds_note, bool> ShowPdsNoteEditWindow;

        private DelegateCommand _qtyPackTextCommand;
        public DelegateCommand QtyPackTextCommand =>
            _qtyPackTextCommand ?? (_qtyPackTextCommand = new DelegateCommand
            (OnQtyPackText));
        private void OnQtyPackText()
        {
            string qty_pack = string.Empty;
            string msgTitle = string.Empty;

            if (mi_micbcode.icbcode_med_type_p)
            {
                qty_pack = icbcode_pack_final;
                msgTitle = "總包";
                icbcode_packChecked = qty_packText == qty_pack;
            }
            else
            {
                qty_pack = icbcode_rx_qty_final;
                msgTitle = "總量";
                icbcode_rx_qtyChecked = qty_packText == qty_pack;
            }

            if (qty_packText != qty_pack)
            {
                qty_packColor = Application.Current?.TryFindResource("Color.ToggleButton.Enabled.Background") as Brush;
                Util.Media.MsgSpeech($"{msgTitle}輸入不符。"); // ，應為{qty_pack}。
            }
            else
            {
                qty_packColor = Application.Current?.TryFindResource("Color.ToggleButton.Checked.Background") as Brush;
                OnToggle();
            }
        }

        private DelegateCommand _toggleCommand;
        public DelegateCommand ToggleCommand =>
            _toggleCommand ?? (_toggleCommand = new DelegateCommand
            (OnToggle));
        private void OnToggle()
        {
            if (!chkMode) return;
            if (mi_micbcode.icbcode_code.IsNullOrWhiteSpace()) return;
            if (!chprs_id_nameChecked) return;
            if (mi_micbcode.icbcode_med_type_p)
            {
                if (!icbcode_rx_uqtyChecked) return;
                if (!icbcode_packChecked) return;
            }
            else
                if (!icbcode_rx_qtyChecked) return;

            if (ch_prs.chprs_multi_type == "Y" && (!chprs_multi_typeChecked)) return;
            if (ch_prs.chprs_multi_dose == "Y" && (!chprs_multi_doseChecked)) return;

            SaveMEDV?.Invoke();
        }

        private DelegateCommand _noticeCommand;
        public DelegateCommand NoticeCommand =>
            _noticeCommand ?? (_noticeCommand = new DelegateCommand
            (OnNotice, () => !mi_micbcode.icbcode_code.IsNullOrWhiteSpace()));
        /// <summary>
        /// 通報(藥袋)
        /// </summary>
        private void OnNotice()
        {
            string pds_note_op = "";
            string pds_note_bed = "", pds_note_clinical = "";
            int? pds_note_send_dt = null;
            string pds_note_lst_code = "";

            switch (opMode)
            {
                case PdsMedInfoParam.OpMode.A:
                case PdsMedInfoParam.OpMode.C:
                    pds_note_op = Pds_noteParam.Op.UD;

                    var ch_udrec = ApiUtil.HttpClientEx<ApiResult<List<Ch_udrec>>>(
                     RouteParam.Service(),
                     RouteParam.Ch_udrec.QueryCh_udrec,
                     new Ch_udrec
                     {
                         chudrec_date = mi_micbcode.icbcode_send_dt,
                         chudrec_ipd_no = mi_micbcode.icbcode_ipd_no
                     },
                     new { option = 2 } // 2: 依傳送日期、住院序號，查詢調劑 & 核對護理站床位
                     ).Data.FirstOrDefault();

                    pds_note_bed = chkMode ? ch_udrec?.chudrecchk_bed : ch_udrec?.chudrec_bed;
                    pds_note_clinical = chkMode ? ch_udrec?.chudrecchk_bed_unit : ch_udrec?.chudrec_bed_unit;
                    pds_note_send_dt = mi_micbcode.icbcode_send_dt;
                    pds_note_lst_code = mi_micbcode.icbcode_send_dt.NullableToStr().PadLeft(7, '0') + mi_micbcode.icbcode_ipd_no;
                    break;
                case PdsMedInfoParam.OpMode.FA:
                case PdsMedInfoParam.OpMode.FC:
                case PdsMedInfoParam.OpMode.FR:
                    pds_note_op = Pds_noteParam.Op.FST;
                    pds_note_bed = mi_micbcode.icfcode_bed;
                    pds_note_clinical = mi_micbcode.icfcode_clinical;
                    pds_note_send_dt = mi_micbcode.icfcode_prt_dt;
                    pds_note_lst_code = mi_micbcode.icfcode_prt_dt.NullableToStr().PadLeft(7, '0') + mi_micbcode.icfcode_id.PadLeft(1, ' ') + mi_micbcode.icfcode_pill_no.NullableToStr().PadLeft(4, '0');
                    break;
            }

            ShowPdsNoteEditWindow?.Invoke(new Pds_note()
            {
                pds_note_op = pds_note_op,
                pds_note_bag_code = mi_micbcode.icbcode_code,
                pds_note_lst_code = pds_note_lst_code,
                pds_note_send_dt = pds_note_send_dt,
                pds_note_ipd_no = mi_micbcode.icbcode_ipd_no,
                pds_note_pat_no = mi_micbcode.icbcode_pat_no,
                pds_note_bed = pds_note_bed,
                pds_note_clinical = pds_note_clinical,
                pds_note_st = Pds_noteParam.St.U
            });
        }

    }
}
