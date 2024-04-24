using FluentValidation;
using Lib;

namespace ViewModels.FluentValidators
{
    public class PdsUdrecChkClinicalViewModelValidator : AbstractValidator<PdsUdrecChkClinicalViewModel>
    {
        public PdsUdrecChkClinicalViewModelValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(vm => vm.SelectedClinical)
                .NotEmpty()
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.SelectedClinical)));

        }
    }

}
