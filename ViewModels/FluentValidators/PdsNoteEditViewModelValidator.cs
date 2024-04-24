using CommonServiceLocator;
using FluentValidation;
using Models;

namespace ViewModels.FluentValidators
{
    public class PdsNoteEditViewModelValidator : AbstractValidator<PdsNoteEditViewModel>
    {
        public PdsNoteEditViewModelValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(vm => vm.Note)
                .SetValidator(ServiceLocator.Current.GetInstance<IValidator<Pds_note>>());
        }

    }
}
