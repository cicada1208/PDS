using FluentValidation;
using Lib;

namespace Models.FluentValidators
{
    public class Ch_prs_codeValidator : AbstractValidator<Ch_prs_code>
    {
        public Ch_prs_codeValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(m => m.prscode_mst_id)
                .NotEmpty()
                .MaxLen(m => m.GetPropertyMaxLength(nameof(m.prscode_mst_id)))
                .WithName(m => m.GetPropertyDisplayName(nameof(m.prscode_mst_id)));

            RuleFor(m => m.chprs_id_name)
                .NotEmpty()
                .WithName(m => m.GetPropertyDisplayName(nameof(m.chprs_id_name)));

            RuleFor(m => m.prscode_brand)
                .NotEmpty()
                .MaxLen(m => m.GetPropertyMaxLength(nameof(m.prscode_brand)))
                .WithName(m => m.GetPropertyDisplayName(nameof(m.prscode_brand)));

            RuleFor(m => m.prscode_code)
                .NotEmpty()
                .MaxLen(m => m.GetPropertyMaxLength(nameof(m.prscode_code)))
                .WithName(m => m.GetPropertyDisplayName(nameof(m.prscode_code)));
        }
    }
}
