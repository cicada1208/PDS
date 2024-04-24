using FluentValidation;
using Lib;

namespace Models.FluentValidators
{
    public class UsersValidator : AbstractValidator<Users>
    {
        public UsersValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(m => m.userTerseName)
                .NotEmpty()
                .WithName(m => m.GetPropertyDisplayName(nameof(m.userTerseName)));
        }
    }
}
