using CommonServiceLocator;
using FluentValidation;
using Lib;
using Models;

namespace ViewModels.FluentValidators
{
    public class MvvmViewModelValidator : AbstractValidator<MvvmViewModel>
    {
        public MvvmViewModelValidator()
        {
            // To set the cascade mode for all rules inside a single validator class.
            // Stop - stops executing a rule as soon as a validator fails.
            CascadeMode = CascadeMode.Stop;

            RuleFor(vm => vm.Title)
                .NotEmpty()
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.Title)));
            //.WithName(vm => ReflectionUtil.GetPropertyDisplayName<MvvmViewModel>(nameof(vm.Title)));
            //.WithName(vm => ReflectionUtil.GetPropertyDisplayName<MvvmViewModel>(i => i.Title));

            //RuleFor(vm => vm.SelectedUser).SetValidator(new UsersValidator());
            RuleFor(vm => vm.SelectedUser)
                .SetValidator(ServiceLocator.Current.GetInstance<IValidator<Users>>());
        }
    }
}
