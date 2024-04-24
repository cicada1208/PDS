using FluentValidation;
using Lib;

namespace Models.FluentValidators
{
    public class Pds_noteValidator : AbstractValidator<Pds_note>
    {
        public Pds_noteValidator()
        {
            RuleFor(m => m.pds_note_type)
                .NotEmpty()
                .WithName(m => m.GetPropertyDisplayName(nameof(m.pds_note_type)));
        }

    }
}
