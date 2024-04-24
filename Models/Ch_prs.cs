using Params;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB2, "ch_prs")]
    public class Ch_prs : BaseModel<Ch_prs>
    {
        private string _chprs_mst_id;
        [Key]
        public string chprs_mst_id
        {
            get => _chprs_mst_id;
            set => Set(ref _chprs_mst_id, value);
        }

        private string _chprs_ins_id;
        public string chprs_ins_id
        {
            get => _chprs_ins_id;
            set => Set(ref _chprs_ins_id, value);
        }

        private string _chprs_stk_cnt;
        public string chprs_stk_cnt
        {
            get => _chprs_stk_cnt;
            set => Set(ref _chprs_stk_cnt, value);
        }

        private string _chprs_id_name;
        /// <summary>
        /// 處置名稱一
        /// </summary>
        public string chprs_id_name
        {
            get => _chprs_id_name;
            set => Set(ref _chprs_id_name, value);
        }

        private string _chprs_id_name2;
        public string chprs_id_name2
        {
            get => _chprs_id_name2;
            set => Set(ref _chprs_id_name2, value);
        }

        private string _chprs_id_name3;
        public string chprs_id_name3
        {
            get => _chprs_id_name3;
            set => Set(ref _chprs_id_name3, value);
        }

        private string _chprs_brf_id;
        public string chprs_brf_id
        {
            get => _chprs_brf_id;
            set => Set(ref _chprs_brf_id, value);
        }

        private string _chprs_fee_knd;
        public string chprs_fee_knd
        {
            get => _chprs_fee_knd;
            set => Set(ref _chprs_fee_knd, value);
        }

        private string _chprs_way_id;
        /// <summary>
        /// 藥品途徑識別 O:口服 E:外用 I:注射
        /// </summary>
        public string chprs_way_id
        {
            get => _chprs_way_id;
            set => Set(ref _chprs_way_id, value);
        }

        private string _chprs_typ_id;
        /// <summary>
        /// 藥品劑型識別 1:水劑,2:粉劑,3:錠劑,4:煎劑,5:塞劑,6:膏劑,7:噴劑
        /// </summary>
        public string chprs_typ_id
        {
            get => _chprs_typ_id;
            set => Set(ref _chprs_typ_id, value);
        }

        private string _chprs_alt2_1;
        public string chprs_alt2_1
        {
            get => _chprs_alt2_1;
            set => Set(ref _chprs_alt2_1, value);
        }

        private string _chprs_data1;
        public string chprs_data1
        {
            get => _chprs_data1;
            set => Set(ref _chprs_data1, value);
        }

        private string _chprs_data2;
        public string chprs_data2
        {
            get => _chprs_data2;
            set => Set(ref _chprs_data2, value);
        }

        private string _chprs_data3;
        public string chprs_data3
        {
            get => _chprs_data3;
            set => Set(ref _chprs_data3, value);
        }

        private string _chprs_data4;
        public string chprs_data4
        {
            get => _chprs_data4;
            set => Set(ref _chprs_data4, value);
        }

        private string _chprs_data5;
        public string chprs_data5
        {
            get => _chprs_data5;
            set => Set(ref _chprs_data5, value);
        }

        private string _chprs_data6;
        public string chprs_data6
        {
            get => _chprs_data6;
            set => Set(ref _chprs_data6, value);
        }

        private string _chprs_data7;
        public string chprs_data7
        {
            get => _chprs_data7;
            set => Set(ref _chprs_data7, value);
        }

        private string _chprs_data8;
        public string chprs_data8
        {
            get => _chprs_data8;
            set => Set(ref _chprs_data8, value);
        }

        private string _chprs_data9;
        public string chprs_data9
        {
            get => _chprs_data9;
            set => Set(ref _chprs_data9, value);
        }

        private string _chprs_data10;
        public string chprs_data10
        {
            get => _chprs_data10;
            set => Set(ref _chprs_data10, value);
        }

        private string _chprs_data11;
        public string chprs_data11
        {
            get => _chprs_data11;
            set => Set(ref _chprs_data11, value);
        }

        private string _chprs_id_name4;
        /// <summary>
        /// 中文藥名
        /// </summary>
        [NotMapped]
        public string chprs_id_name4
        {
            get => _chprs_id_name4;
            set => Set(ref _chprs_id_name4, value);
        }

        private string _chprs_way_name;
        [NotMapped]
        public string chprs_way_name
        {
            get => _chprs_way_name;
            set => Set(ref _chprs_way_name, value);
        }

        private string _chprs_orig_rehrig;
        /// <summary>
        /// 原包裝儲存方式：冷藏：Y
        /// </summary>
        [NotMapped]
        public string chprs_orig_rehrig
        {
            get => _chprs_orig_rehrig;
            set
            {
                Set(ref _chprs_orig_rehrig, value);
                chprs_orig_rehrigVisibility = (_chprs_orig_rehrig == "Y") ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _chprs_orig_rehrigVisibility = Visibility.Collapsed;
        /// <summary>
        /// 原包裝儲存方式：冷藏：Visibility
        /// </summary>
        [NotMapped]
        public Visibility chprs_orig_rehrigVisibility
        {
            get => _chprs_orig_rehrigVisibility;
            set => Set(ref _chprs_orig_rehrigVisibility, value);
        }

        private string _chprs_tube_feed;
        /// <summary>
        /// 管灌不宜：N
        /// </summary>
        [NotMapped]
        public string chprs_tube_feed
        {
            get => _chprs_tube_feed;
            set
            {
                Set(ref _chprs_tube_feed, value);
                chprs_tube_feedVisibility = (_chprs_tube_feed == "N") ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _chprs_tube_feedVisibility = Visibility.Collapsed;
        /// <summary>
        /// 管灌不宜：Visibility
        /// </summary>
        [NotMapped]
        public Visibility chprs_tube_feedVisibility
        {
            get => _chprs_tube_feedVisibility;
            set => Set(ref _chprs_tube_feedVisibility, value);
        }

        private string _chprs_tube_feed_note;
        /// <summary>
        /// 管灌不宜：說明
        /// </summary>
        [NotMapped]
        public string chprs_tube_feed_note
        {
            get => _chprs_tube_feed_note;
            set => Set(ref _chprs_tube_feed_note, value);
        }

        private string _chprs_liver_func;
        /// <summary>
        /// 肝功能調整：Y
        /// </summary>
        [NotMapped]
        public string chprs_liver_func
        {
            get => _chprs_liver_func;
            set
            {
                Set(ref _chprs_liver_func, value);
                chprs_liver_funcVisibility = (_chprs_liver_func == "Y") ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _chprs_liver_funcVisibility = Visibility.Collapsed;
        /// <summary>
        /// 肝功能調整：Visibility
        /// </summary>
        [NotMapped]
        public Visibility chprs_liver_funcVisibility
        {
            get => _chprs_liver_funcVisibility;
            set => Set(ref _chprs_liver_funcVisibility, value);
        }

        private string _chprs_liver_func_note;
        /// <summary>
        /// 肝功能調整：說明
        /// </summary>
        [NotMapped]
        public string chprs_liver_func_note
        {
            get => _chprs_liver_func_note;
            set => Set(ref _chprs_liver_func_note, value);
        }

        private string _chprs_renal_func;
        /// <summary>
        /// 腎功能調整：Y
        /// </summary>
        [NotMapped]
        public string chprs_renal_func
        {
            get => _chprs_renal_func;
            set
            {
                Set(ref _chprs_renal_func, value);
                chprs_renal_funcVisibility = (_chprs_renal_func == "Y") ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _chprs_renal_funcVisibility = Visibility.Collapsed;
        /// <summary>
        /// 腎功能調整：Visibility
        /// </summary>
        [NotMapped]
        public Visibility chprs_renal_funcVisibility
        {
            get => _chprs_renal_funcVisibility;
            set => Set(ref _chprs_renal_funcVisibility, value);
        }

        private string _chprs_renal_func_note;
        /// <summary>
        /// 腎功能調整：說明
        /// </summary>
        [NotMapped]
        public string chprs_renal_func_note
        {
            get => _chprs_renal_func_note;
            set => Set(ref _chprs_renal_func_note, value);
        }

        private string _chprs_multi_type;
        /// <summary>
        /// 多劑型：Y
        /// </summary>
        [NotMapped]
        public string chprs_multi_type
        {
            get => _chprs_multi_type;
            set
            {
                Set(ref _chprs_multi_type, value);
                chprs_multi_typeVisibility = _chprs_multi_type == "Y" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _chprs_multi_typeVisibility = Visibility.Collapsed;
        /// <summary>
        /// 多劑型：Visibility
        /// </summary>
        [NotMapped]
        public Visibility chprs_multi_typeVisibility
        {
            get => _chprs_multi_typeVisibility;
            set => Set(ref _chprs_multi_typeVisibility, value);
        }

        private string _chprs_multi_type_note;
        /// <summary>
        /// 多劑型：說明
        /// </summary>
        [NotMapped]
        public string chprs_multi_type_note
        {
            get => _chprs_multi_type_note;
            set => Set(ref _chprs_multi_type_note, value);
        }

        private string _chprs_multi_dose;
        /// <summary>
        /// 多劑量：Y
        /// </summary>
        [NotMapped]
        public string chprs_multi_dose
        {
            get => _chprs_multi_dose;
            set
            {
                Set(ref _chprs_multi_dose, value);
                chprs_multi_doseVisibility = _chprs_multi_dose == "Y" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        private Visibility _chprs_multi_doseVisibility = Visibility.Collapsed;
        /// <summary>
        /// 多劑量：Visibility
        /// </summary>
        [NotMapped]
        public Visibility chprs_multi_doseVisibility
        {
            get => _chprs_multi_doseVisibility;
            set => Set(ref _chprs_multi_doseVisibility, value);
        }

        private string _chprs_multi_dose_note;
        /// <summary>
        /// 多劑量：說明
        /// </summary>
        [NotMapped]
        public string chprs_multi_dose_note
        {
            get => _chprs_multi_dose_note;
            set => Set(ref _chprs_multi_dose_note, value);
        }

        private string _chprs_spec_pack1;
        /// <summary>
        /// 特殊包裝量：Y
        /// </summary>
        [NotMapped]
        public string chprs_spec_pack1
        {
            get => _chprs_spec_pack1;
            set => Set(ref _chprs_spec_pack1, value);
        }

        private string _chprs_spec_pack2;
        /// <summary>
        /// 特殊包裝量：數量
        /// </summary>
        [NotMapped]
        public string chprs_spec_pack2
        {
            get => _chprs_spec_pack2;
            set => Set(ref _chprs_spec_pack2, value);
        }

        private string _chprs_spec_pack3;
        /// <summary>
        /// 特殊包裝量：單位1
        /// </summary>
        [NotMapped]
        public string chprs_spec_pack3
        {
            get => _chprs_spec_pack3;
            set => Set(ref _chprs_spec_pack3, value);
        }

        private string _chprs_spec_pack4;
        /// <summary>
        /// 特殊包裝量：單位2
        /// </summary>
        [NotMapped]
        public string chprs_spec_pack4
        {
            get => _chprs_spec_pack4;
            set => Set(ref _chprs_spec_pack4, value);
        }

        private string _chprs_spec_pack;
        /// <summary>
        /// 特殊包裝量：A B/C
        /// </summary>
        [NotMapped]
        public string chprs_spec_pack
        {
            get => _chprs_spec_pack;
            set => Set(ref _chprs_spec_pack, value);
        }

        private string _chprs_spec_pack_speech;
        /// <summary>
        /// 特殊包裝量：每C A B (EX: 每盒 20 顆)
        /// </summary>
        [NotMapped]
        public string chprs_spec_pack_speech
        {
            get => _chprs_spec_pack_speech;
            set => Set(ref _chprs_spec_pack_speech, value);
        }

        private string _chprs_give_dilu;
        /// <summary>
        /// 另給稀釋液：Y
        /// </summary>
        [NotMapped]
        public string chprs_give_dilu
        {
            get => _chprs_give_dilu;
            set => Set(ref _chprs_give_dilu, value);
        }

        private string _chprs_give_dilu_note;
        /// <summary>
        /// 另給稀釋液：說明
        /// </summary>
        [NotMapped]
        public string chprs_give_dilu_note
        {
            get => _chprs_give_dilu_note;
            set => Set(ref _chprs_give_dilu_note, value);
        }

        private object _pic_url;
        /// <summary>
        /// 一般藥品圖片路徑
        /// </summary>
        [NotMapped]
        public object pic_url
        {
            get => _pic_url;
            set => Set(ref _pic_url, value);
        }

        private string _ni_pic_url;
        /// <summary>
        /// 裸碇藥品圖片路徑
        /// </summary>
        [NotMapped]
        public string ni_pic_url
        {
            get => _ni_pic_url;
            set => Set(ref _ni_pic_url, value);
        }

        private string _drug_news_url;
        /// <summary>
        /// 藥品異動路徑
        /// </summary>
        [NotMapped]
        public string drug_news_url
        {
            get => _drug_news_url;
            set => Set(ref _drug_news_url, value);
        }

        private string _drug_info_url;
        /// <summary>
        /// 藥品網頁資訊路徑
        /// </summary>
        [NotMapped]
        public string drug_info_url
        {
            get => _drug_info_url;
            set => Set(ref _drug_info_url, value);
        }

        private string _atc_code_prefix5;
        /// <summary>
        /// ATC CODE 前五碼
        /// </summary>
        [NotMapped]
        public string atc_code_prefix5
        {
            get => _atc_code_prefix5;
            set => Set(ref _atc_code_prefix5, value);
        }

    }
}
