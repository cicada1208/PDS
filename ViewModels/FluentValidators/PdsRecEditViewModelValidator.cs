using CommonServiceLocator;
using FluentValidation;
using Models;

namespace ViewModels.FluentValidators
{
    public class PdsRecEditViewModelValidator : AbstractValidator<PdsRecEditViewModel>
    {
        public PdsRecEditViewModelValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(vm => vm.Rec)
                .SetValidator(ServiceLocator.Current.GetInstance<IValidator<Pds_rec>>());
        }

    }
}
