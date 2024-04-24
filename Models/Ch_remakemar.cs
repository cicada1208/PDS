using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ch_remakemar")]
    public class Ch_remakemar : BaseModel<Ch_remakemar>
    {
        private string _remake_orderid;
        [Key]
        public string remake_orderid
        {
            get => _remake_orderid;
            set => Set(ref _remake_orderid, value);
        }

        private string _remake_charttime;
        [Key]
        public string remake_charttime
        {
            get => _remake_charttime;
            set => Set(ref _remake_charttime, value);
        }

        private string _remake_marver;
        [Key]
        public string remake_marver
        {
            get => _remake_marver;
            set => Set(ref _remake_marver, value);
        }

        private string _remake_orderver;
        [Key]
        public string remake_orderver
        {
            get => _remake_orderver;
            set => Set(ref _remake_orderver, value);
        }

        private string _remake_ptencounterid;
        public string remake_ptencounterid
        {
            get => _remake_ptencounterid;
            set => Set(ref _remake_ptencounterid, value);
        }

        private int? _remake_ipd_date;
        public int? remake_ipd_date
        {
            get => _remake_ipd_date;
            set => Set(ref _remake_ipd_date, value);
        }

        private short? _remake_ipd_seq;
        public short? remake_ipd_seq
        {
            get => _remake_ipd_seq;
            set => Set(ref _remake_ipd_seq, value);
        }

        private int? _remake_ins_date;
        public int? remake_ins_date
        {
            get => _remake_ins_date;
            set => Set(ref _remake_ins_date, value);
        }

        private short? _remake_odr_seq;
        public short? remake_odr_seq
        {
            get => _remake_odr_seq;
            set => Set(ref _remake_odr_seq, value);
        }

        private short? _remake_fee_seq;
        public short? remake_fee_seq
        {
            get => _remake_fee_seq;
            set => Set(ref _remake_fee_seq, value);
        }

        private string _remake_pat_no;
        public string remake_pat_no
        {
            get => _remake_pat_no;
            set => Set(ref _remake_pat_no, value);
        }

        private string _remake_bed;
        public string remake_bed
        {
            get => _remake_bed;
            set => Set(ref _remake_bed, value);
        }

        private string _remake_ordertype;
        public string remake_ordertype
        {
            get => _remake_ordertype;
            set => Set(ref _remake_ordertype, value);
        }

        private string _remake_way;
        public string remake_way
        {
            get => _remake_way;
            set => Set(ref _remake_way, value);
        }

        private string _remake_maketype;
        public string remake_maketype
        {
            get => _remake_maketype;
            set => Set(ref _remake_maketype, value);
        }

        private int? _remake_origin_date;
        public int? remake_origin_date
        {
            get => _remake_origin_date;
            set => Set(ref _remake_origin_date, value);
        }

        private string _remake_reason;
        public string remake_reason
        {
            get => _remake_reason;
            set => Set(ref _remake_reason, value);
        }

        private string _remake_descause;
        public string remake_descause
        {
            get => _remake_descause;
            set => Set(ref _remake_descause, value);
        }

        private string _remake_medprs;
        public string remake_medprs
        {
            get => _remake_medprs;
            set => Set(ref _remake_medprs, value);
        }

        private string _remake_freq;
        public string remake_freq
        {
            get => _remake_freq;
            set => Set(ref _remake_freq, value);
        }

        private string _remake_route;
        public string remake_route
        {
            get => _remake_route;
            set => Set(ref _remake_route, value);
        }

        private string _remake_dose;
        public string remake_dose
        {
            get => _remake_dose;
            set => Set(ref _remake_dose, value);
        }

        private string _remake_dosede;
        public string remake_dosede
        {
            get => _remake_dosede;
            set => Set(ref _remake_dosede, value);
        }

        private string _remake_powder;
        public string remake_powder
        {
            get => _remake_powder;
            set => Set(ref _remake_powder, value);
        }

        private string _remake_emp_no;
        public string remake_emp_no
        {
            get => _remake_emp_no;
            set => Set(ref _remake_emp_no, value);
        }

        private int? _remake_tohis_date;
        public int? remake_tohis_date
        {
            get => _remake_tohis_date;
            set => Set(ref _remake_tohis_date, value);
        }

        private int? _remake_tohis_time;
        public int? remake_tohis_time
        {
            get => _remake_tohis_time;
            set => Set(ref _remake_tohis_time, value);
        }

        private string _remake_chg_yn;
        public string remake_chg_yn
        {
            get => _remake_chg_yn;
            set => Set(ref _remake_chg_yn, value);
        }

        private string _remake_chg_date;
        public string remake_chg_date
        {
            get => _remake_chg_date;
            set => Set(ref _remake_chg_date, value);
        }

        private string _remake_chg_time;
        public string remake_chg_time
        {
            get => _remake_chg_time;
            set => Set(ref _remake_chg_time, value);
        }

        private string _remake_chg_emp_no;
        public string remake_chg_emp_no
        {
            get => _remake_chg_emp_no;
            set => Set(ref _remake_chg_emp_no, value);
        }

        private string _remake_pha_yn;
        public string remake_pha_yn
        {
            get => _remake_pha_yn;
            set => Set(ref _remake_pha_yn, value);
        }

        private string _remake_pha_result;
        public string remake_pha_result
        {
            get => _remake_pha_result;
            set => Set(ref _remake_pha_result, value);
        }

        private string _remake_pha_reply;
        public string remake_pha_reply
        {
            get => _remake_pha_reply;
            set => Set(ref _remake_pha_reply, value);
        }

        private string _remake_pha_date;
        public string remake_pha_date
        {
            get => _remake_pha_date;
            set => Set(ref _remake_pha_date, value);
        }

        private string _remake_pha_time;
        public string remake_pha_time
        {
            get => _remake_pha_time;
            set => Set(ref _remake_pha_time, value);
        }

        private string _remake_pha_emp_no;
        public string remake_pha_emp_no
        {
            get => _remake_pha_emp_no;
            set => Set(ref _remake_pha_emp_no, value);
        }

        private short? _remake_odr_takeno;
        public short? remake_odr_takeno
        {
            get => _remake_odr_takeno;
            set => Set(ref _remake_odr_takeno, value);
        }

        private string _remake_status;
        public string remake_status
        {
            get => _remake_status;
            set => Set(ref _remake_status, value);
        }

        private string _remake_md_no;
        public string remake_md_no
        {
            get => _remake_md_no;
            set => Set(ref _remake_md_no, value);
        }

        private string _remake_md_dt;
        public string remake_md_dt
        {
            get => _remake_md_dt;
            set => Set(ref _remake_md_dt, value);
        }

        private string _remake_md_time;
        public string remake_md_time
        {
            get => _remake_md_time;
            set => Set(ref _remake_md_time, value);
        }

    }
}

