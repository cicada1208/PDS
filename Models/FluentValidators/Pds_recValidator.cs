using FluentValidation;
using Lib;

namespace Models.FluentValidators
{
    public class Pds_recValidator : AbstractValidator<Pds_rec>
    {
        public Pds_recValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(m => m.pds_rec_nondeliver)
                .MaxLen(m => m.GetPropertyMaxLength(nameof(m.pds_rec_nondeliver)))
                .WithName(m => m.GetPropertyDisplayName(nameof(m.pds_rec_nondeliver)));

            RuleFor(m => m.pds_rec_note)
                .MaxLen(m => m.GetPropertyMaxLength(nameof(m.pds_rec_note)))
                .WithName(m => m.GetPropertyDisplayName(nameof(m.pds_rec_note)));
        }
    }
}
