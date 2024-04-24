using FluentValidation;
using Lib;

namespace ViewModels.FluentValidators
{
    public class ValidationViewModelValidator : AbstractValidator<ValidationViewModel>
    {
        public ValidationViewModelValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(vm => vm.PhoneNumber)
                .NotEmpty()
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.PhoneNumber)));
        }
    }
}
