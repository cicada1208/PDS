using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_tra")]
    public class Ch_tra : BaseModel<Ch_tra>
    {
        private decimal? _tra_ipd_no;
        [Key]
        public decimal? tra_ipd_no
        {
            get => _tra_ipd_no;
            set => Set(ref _tra_ipd_no, value);
        }

        private decimal? _tra_odr_no;
        [Key]
        public decimal? tra_odr_no
        {
            get => _tra_odr_no;
            set => Set(ref _tra_odr_no, value);
        }

        private short? _tra_fee_no;
        [Key]
        public short? tra_fee_no
        {
            get => _tra_fee_no;
            set => Set(ref _tra_fee_no, value);
        }

        private int? _tra_exe_date;
        [Key]
        public int? tra_exe_date
        {
            get => _tra_exe_date;
            set => Set(ref _tra_exe_date, value);
        }

        private short? _tra_exe_time;
        [Key]
        public short? tra_exe_time
        {
            get => _tra_exe_time;
            set => Set(ref _tra_exe_time, value);
        }

        private string _tra_fee_prs;
        public string tra_fee_prs
        {
            get => _tra_fee_prs;
            set => Set(ref _tra_fee_prs, value);
        }

        private string _tra_fee_name;
        public string tra_fee_name
        {
            get => _tra_fee_name;
            set => Set(ref _tra_fee_name, value);
        }

        private string _tra_chemo_odr_no;
        public string tra_chemo_odr_no
        {
            get => _tra_chemo_odr_no;
            set => Set(ref _tra_chemo_odr_no, value);
        }

        private string _tra_chemo_fee_no;
        public string tra_chemo_fee_no
        {
            get => _tra_chemo_fee_no;
            set => Set(ref _tra_chemo_fee_no, value);
        }

        private string _tra_bed_room;
        public string tra_bed_room
        {
            get => _tra_bed_room;
            set => Set(ref _tra_bed_room, value);
        }

        private string _tra_bed_no;
        public string tra_bed_no
        {
            get => _tra_bed_no;
            set => Set(ref _tra_bed_no, value);
        }

        private string _tra_trn_ward;
        public string tra_trn_ward
        {
            get => _tra_trn_ward;
            set => Set(ref _tra_trn_ward, value);
        }

        private string _tra_barcode;
        public string tra_barcode
        {
            get => _tra_barcode;
            set => Set(ref _tra_barcode, value);
        }

        private string _tra_cbn_barcode;
        public string tra_cbn_barcode
        {
            get => _tra_cbn_barcode;
            set => Set(ref _tra_cbn_barcode, value);
        }

        private int? _tra_pat_no;
        public int? tra_pat_no
        {
            get => _tra_pat_no;
            set => Set(ref _tra_pat_no, value);
        }

        private string _tra_pat_name;
        public string tra_pat_name
        {
            get => _tra_pat_name;
            set => Set(ref _tra_pat_name, value);
        }

        private short? _tra_pill_no;
        public short? tra_pill_no
        {
            get => _tra_pill_no;
            set => Set(ref _tra_pill_no, value);
        }

        private string _tra_prs_type;
        public string tra_prs_type
        {
            get => _tra_prs_type;
            set => Set(ref _tra_prs_type, value);
        }

        private string _tra_reserve;
        public string tra_reserve
        {
            get => _tra_reserve;
            set => Set(ref _tra_reserve, value);
        }

        private string _tra_dr_no;
        public string tra_dr_no
        {
            get => _tra_dr_no;
            set => Set(ref _tra_dr_no, value);
        }

        private int? _tra_odr_date;
        public int? tra_odr_date
        {
            get => _tra_odr_date;
            set => Set(ref _tra_odr_date, value);
        }

        private short? _tra_odr_time;
        public short? tra_odr_time
        {
            get => _tra_odr_time;
            set => Set(ref _tra_odr_time, value);
        }

        private int? _tra_prn_date;
        public int? tra_prn_date
        {
            get => _tra_prn_date;
            set => Set(ref _tra_prn_date, value);
        }

        private short? _tra_prn_time;
        public short? tra_prn_time
        {
            get => _tra_prn_time;
            set => Set(ref _tra_prn_time, value);
        }

        private string _tra_fin_no;
        public string tra_fin_no
        {
            get => _tra_fin_no;
            set => Set(ref _tra_fin_no, value);
        }

        private string _tra_fin_name;
        public string tra_fin_name
        {
            get => _tra_fin_name;
            set => Set(ref _tra_fin_name, value);
        }

        private int? _tra_fin_date;
        public int? tra_fin_date
        {
            get => _tra_fin_date;
            set => Set(ref _tra_fin_date, value);
        }

        private short? _tra_fin_time;
        public short? tra_fin_time
        {
            get => _tra_fin_time;
            set => Set(ref _tra_fin_time, value);
        }

        private string _tra_chk_no;
        public string tra_chk_no
        {
            get => _tra_chk_no;
            set => Set(ref _tra_chk_no, value);
        }

        private string _tra_chk_name;
        public string tra_chk_name
        {
            get => _tra_chk_name;
            set => Set(ref _tra_chk_name, value);
        }

        private int? _tra_chk_date;
        public int? tra_chk_date
        {
            get => _tra_chk_date;
            set => Set(ref _tra_chk_date, value);
        }

        private short? _tra_chk_time;
        public short? tra_chk_time
        {
            get => _tra_chk_time;
            set => Set(ref _tra_chk_time, value);
        }

        private string _tra_drug_no;
        public string tra_drug_no
        {
            get => _tra_drug_no;
            set => Set(ref _tra_drug_no, value);
        }

        private string _tra_drug_name;
        public string tra_drug_name
        {
            get => _tra_drug_name;
            set => Set(ref _tra_drug_name, value);
        }

        private int? _tra_drug_date;
        public int? tra_drug_date
        {
            get => _tra_drug_date;
            set => Set(ref _tra_drug_date, value);
        }

        private short? _tra_drug_time;
        public short? tra_drug_time
        {
            get => _tra_drug_time;
            set => Set(ref _tra_drug_time, value);
        }

        private string _tra_carry_no;
        public string tra_carry_no
        {
            get => _tra_carry_no;
            set => Set(ref _tra_carry_no, value);
        }

        private string _tra_carry_name;
        public string tra_carry_name
        {
            get => _tra_carry_name;
            set => Set(ref _tra_carry_name, value);
        }

        private int? _tra_carry_date;
        public int? tra_carry_date
        {
            get => _tra_carry_date;
            set => Set(ref _tra_carry_date, value);
        }

        private short? _tra_carry_time;
        public short? tra_carry_time
        {
            get => _tra_carry_time;
            set => Set(ref _tra_carry_time, value);
        }

        private int? _tra_arrive_date;
        public int? tra_arrive_date
        {
            get => _tra_arrive_date;
            set => Set(ref _tra_arrive_date, value);
        }

        private short? _tra_arrive_time;
        public short? tra_arrive_time
        {
            get => _tra_arrive_time;
            set => Set(ref _tra_arrive_time, value);
        }

        private string _tra_st_no;
        public string tra_st_no
        {
            get => _tra_st_no;
            set => Set(ref _tra_st_no, value);
        }

        private string _tra_st_name;
        public string tra_st_name
        {
            get => _tra_st_name;
            set => Set(ref _tra_st_name, value);
        }

        private int? _tra_st_date;
        public int? tra_st_date
        {
            get => _tra_st_date;
            set => Set(ref _tra_st_date, value);
        }

        private short? _tra_st_time;
        public short? tra_st_time
        {
            get => _tra_st_time;
            set => Set(ref _tra_st_time, value);
        }

        private string _tra_give_no;
        public string tra_give_no
        {
            get => _tra_give_no;
            set => Set(ref _tra_give_no, value);
        }

        private string _tra_give_name;
        public string tra_give_name
        {
            get => _tra_give_name;
            set => Set(ref _tra_give_name, value);
        }

        private int? _tra_give_date;
        public int? tra_give_date
        {
            get => _tra_give_date;
            set => Set(ref _tra_give_date, value);
        }

        private short? _tra_give_time;
        public short? tra_give_time
        {
            get => _tra_give_time;
            set => Set(ref _tra_give_time, value);
        }

        private string _tra_memo;
        public string tra_memo
        {
            get => _tra_memo;
            set => Set(ref _tra_memo, value);
        }

        private string _tra_filler;
        public string tra_filler
        {
            get => _tra_filler;
            set => Set(ref _tra_filler, value);
        }

    }
}

