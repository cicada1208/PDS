using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_hdb")]
    public class Ch_hdb : BaseModel<Ch_hdb>
    {
        private int? _chhdb_hd_dt;
        [Key]
        public int? chhdb_hd_dt
        {
            get => _chhdb_hd_dt;
            set => Set(ref _chhdb_hd_dt, value);
        }

        private int? _chhdb_pat_no;
        [Key]
        public int? chhdb_pat_no
        {
            get => _chhdb_pat_no;
            set => Set(ref _chhdb_pat_no, value);
        }

        private int? _chhdb_hd_dt_v;
        public int? chhdb_hd_dt_v
        {
            get => _chhdb_hd_dt_v;
            set => Set(ref _chhdb_hd_dt_v, value);
        }

        private int? _chhdb_original_dt;
        public int? chhdb_original_dt
        {
            get => _chhdb_original_dt;
            set => Set(ref _chhdb_original_dt, value);
        }

        private string _chhdb_hd_reason;
        public string chhdb_hd_reason
        {
            get => _chhdb_hd_reason;
            set => Set(ref _chhdb_hd_reason, value);
        }

        private string _chhdb_hd_other;
        public string chhdb_hd_other
        {
            get => _chhdb_hd_other;
            set => Set(ref _chhdb_hd_other, value);
        }

        private string _chhdb_va_yn;
        public string chhdb_va_yn
        {
            get => _chhdb_va_yn;
            set => Set(ref _chhdb_va_yn, value);
        }

        private string _chhdb_va;
        public string chhdb_va
        {
            get => _chhdb_va;
            set => Set(ref _chhdb_va, value);
        }

        private int? _chhdb_on_va_dt;
        public int? chhdb_on_va_dt
        {
            get => _chhdb_on_va_dt;
            set => Set(ref _chhdb_on_va_dt, value);
        }

        private short? _chhdb_pd;
        public short? chhdb_pd
        {
            get => _chhdb_pd;
            set => Set(ref _chhdb_pd, value);
        }

        private int? _chhdb_on_pd_dt;
        public int? chhdb_on_pd_dt
        {
            get => _chhdb_on_pd_dt;
            set => Set(ref _chhdb_on_pd_dt, value);
        }

        private int? _chhdb_change_pd_dt;
        public int? chhdb_change_pd_dt
        {
            get => _chhdb_change_pd_dt;
            set => Set(ref _chhdb_change_pd_dt, value);
        }

        private string _chhdb_fst_hd_place;
        public string chhdb_fst_hd_place
        {
            get => _chhdb_fst_hd_place;
            set => Set(ref _chhdb_fst_hd_place, value);
        }

        private string _chhdb_mjdr_id;
        public string chhdb_mjdr_id
        {
            get => _chhdb_mjdr_id;
            set => Set(ref _chhdb_mjdr_id, value);
        }

        private string _chhdb_mjdr_name;
        public string chhdb_mjdr_name
        {
            get => _chhdb_mjdr_name;
            set => Set(ref _chhdb_mjdr_name, value);
        }

        private int? _chhdb_create_dt;
        public int? chhdb_create_dt
        {
            get => _chhdb_create_dt;
            set => Set(ref _chhdb_create_dt, value);
        }

        private int? _chhdb_create_time;
        public int? chhdb_create_time
        {
            get => _chhdb_create_time;
            set => Set(ref _chhdb_create_time, value);
        }

        private string _chhdb_create_id;
        public string chhdb_create_id
        {
            get => _chhdb_create_id;
            set => Set(ref _chhdb_create_id, value);
        }

        private int? _chhdb_modify_dt;
        public int? chhdb_modify_dt
        {
            get => _chhdb_modify_dt;
            set => Set(ref _chhdb_modify_dt, value);
        }

        private int? _chhdb_modify_time;
        public int? chhdb_modify_time
        {
            get => _chhdb_modify_time;
            set => Set(ref _chhdb_modify_time, value);
        }

        private string _chhdb_modify_id;
        public string chhdb_modify_id
        {
            get => _chhdb_modify_id;
            set => Set(ref _chhdb_modify_id, value);
        }

        private string _chhdb_upload_emr;
        public string chhdb_upload_emr
        {
            get => _chhdb_upload_emr;
            set => Set(ref _chhdb_upload_emr, value);
        }

        private int? _chhdb_1st_emr_dt;
        public int? chhdb_1st_emr_dt
        {
            get => _chhdb_1st_emr_dt;
            set => Set(ref _chhdb_1st_emr_dt, value);
        }

        private string _chhdb_1st_emr_id;
        public string chhdb_1st_emr_id
        {
            get => _chhdb_1st_emr_id;
            set => Set(ref _chhdb_1st_emr_id, value);
        }

        private int? _chhdb_last_emr_dt;
        public int? chhdb_last_emr_dt
        {
            get => _chhdb_last_emr_dt;
            set => Set(ref _chhdb_last_emr_dt, value);
        }

        private string _chhdb_last_emr_id;
        public string chhdb_last_emr_id
        {
            get => _chhdb_last_emr_id;
            set => Set(ref _chhdb_last_emr_id, value);
        }

        private int? _chhdb_turn_out_dt;
        public int? chhdb_turn_out_dt
        {
            get => _chhdb_turn_out_dt;
            set => Set(ref _chhdb_turn_out_dt, value);
        }

        private string _chhdb_turn_out_dr;
        public string chhdb_turn_out_dr
        {
            get => _chhdb_turn_out_dr;
            set => Set(ref _chhdb_turn_out_dr, value);
        }

        private string _chhdb_filler;
        public string chhdb_filler
        {
            get => _chhdb_filler;
            set => Set(ref _chhdb_filler, value);
        }

    }
}

