using CommonServiceLocator;
using FluentValidation;
using Models;

namespace ViewModels.FluentValidators
{
    public class PdsPrsCodeViewModelValidator : AbstractValidator<PdsPrsCodeViewModel>
    {
        public PdsPrsCodeViewModelValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(vm => vm.SelectedCode)
                .SetValidator(ServiceLocator.Current.GetInstance<IValidator<Ch_prs_code>>());

        }
    }
}
