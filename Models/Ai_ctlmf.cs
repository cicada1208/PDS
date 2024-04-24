using Params;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Lib.Attributes.Table(DBParam.DBType.SYBASE, DBParam.DBName.SYB1, "ai_ctlmf")]
    public class Ai_ctlmf : BaseModel<Ai_ctlmf>
    {
        private int? _ai_ctlmf_pat_no;
        [Key]
        public int? ai_ctlmf_pat_no
        {
            get => _ai_ctlmf_pat_no;
            set => Set(ref _ai_ctlmf_pat_no, value);
        }

        private string _ai_ctlmf_id_no;
        public string ai_ctlmf_id_no
        {
            get => _ai_ctlmf_id_no;
            set => Set(ref _ai_ctlmf_id_no, value);
        }

        private decimal? _ai_ctlmf_body_height;
        public decimal? ai_ctlmf_body_height
        {
            get => _ai_ctlmf_body_height;
            set => Set(ref _ai_ctlmf_body_height, value);
        }

        private decimal? _ai_ctlmf_body_weight;
        public decimal? ai_ctlmf_body_weight
        {
            get => _ai_ctlmf_body_weight;
            set => Set(ref _ai_ctlmf_body_weight, value);
        }

        private string _ai_ctlmf_hd_capd;
        public string ai_ctlmf_hd_capd
        {
            get => _ai_ctlmf_hd_capd;
            set => Set(ref _ai_ctlmf_hd_capd, value);
        }

        private int? _ai_ctlmf_update_da;
        public int? ai_ctlmf_update_da
        {
            get => _ai_ctlmf_update_da;
            set => Set(ref _ai_ctlmf_update_da, value);
        }

        private int? _ai_ctlmf_update_tm;
        public int? ai_ctlmf_update_tm
        {
            get => _ai_ctlmf_update_tm;
            set => Set(ref _ai_ctlmf_update_tm, value);
        }

        private string _ai_ctlmf_update_user;
        public string ai_ctlmf_update_user
        {
            get => _ai_ctlmf_update_user;
            set => Set(ref _ai_ctlmf_update_user, value);
        }

        private string _ai_ctlmf_filler;
        public string ai_ctlmf_filler
        {
            get => _ai_ctlmf_filler;
            set => Set(ref _ai_ctlmf_filler, value);
        }

    }
}
