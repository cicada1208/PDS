using FluentValidation;
using Lib;
using Params;

namespace ViewModels.FluentValidators
{
    public class PdsRecCancelViewModelValidator : AbstractValidator<PdsRecCancelViewModel>
    {
        public PdsRecCancelViewModelValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(vm => vm.pds_rec_reason)
                .NotEmpty()
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.pds_rec_reason)));

            RuleFor(vm => vm.pds_rec_reason_oth)
                .NotEmpty()
                // ApplyConditionTo.CurrentValidator 只應用於前一個 validator NotEmpty
                .When(vm => vm.pds_rec_reason == Pds_recParam.Rec_reason.C99,
                ApplyConditionTo.CurrentValidator)
                .MaxLen(vm => vm.GetPropertyMaxLength(nameof(vm.pds_rec_reason_oth)))
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.pds_rec_reason_oth)));

            RuleFor(vm => vm.pds_rec_md_qty)
                .NotEmpty()
                .When(vm => vm.pds_rec_reason == Pds_recParam.Rec_reason.C04 ||
                vm.pds_rec_reason == Pds_recParam.Rec_reason.C08,
                ApplyConditionTo.CurrentValidator)
                .MaxLen(vm => vm.GetPropertyMaxLength(nameof(vm.pds_rec_md_qty)))
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.pds_rec_md_qty)));

            //RuleFor(vm => vm.pds_rec_md_qty)
            //    .Must((vm, qty) => !(qty.NullableToStr() == "" && vm.pds_rec_md_way1.NullableToStr() == "")) // return true 才不會有訊息
            //    .When(vm => vm.pds_rec_reason == Pds_recParam.Rec_reason.C04 ||
            //    vm.pds_rec_reason == Pds_recParam.Rec_reason.C08,
            //    ApplyConditionTo.CurrentValidator)
            //    .WithMessage("總量/總包與頻次需填寫其一");

            RuleFor(vm => vm.pds_rec_md_qty)
                .Must(qty => qty.IsNumeric())
                .When(vm => vm.pds_rec_md_qty.NullableToStr() != string.Empty,
                ApplyConditionTo.CurrentValidator)
                .WithMessage("總量/總包需為數值");

            RuleFor(vm => vm.pds_rec_md_way1)
                .NotEmpty()
                .When(vm => vm.pds_rec_reason == Pds_recParam.Rec_reason.C04 ||
                vm.pds_rec_reason == Pds_recParam.Rec_reason.C08 ||
                vm.pds_rec_reason == Pds_recParam.Rec_reason.C11,
                ApplyConditionTo.CurrentValidator)
                .MaxLen(vm => vm.GetPropertyMaxLength(nameof(vm.pds_rec_md_way1)))
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.pds_rec_md_way1)));

            RuleFor(vm => vm.pds_recd_err_mst_id)
                .NotEmpty()
                .When(vm => vm.pds_rec_reason == Pds_recParam.Rec_reason.C06,
                ApplyConditionTo.CurrentValidator)
                .MaxLen(vm => vm.GetPropertyMaxLength(nameof(vm.pds_recd_err_mst_id)))
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.pds_recd_err_mst_id)));

            RuleFor(vm => vm.chprs_id_name)
                .NotEmpty()
                .When(vm => vm.pds_rec_reason == Pds_recParam.Rec_reason.C06,
                ApplyConditionTo.CurrentValidator)
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.chprs_id_name)));

            RuleFor(vm => vm.pds_recd_err_qty)
                .NotEmpty()
                .When(vm => vm.pds_rec_reason == Pds_recParam.Rec_reason.C07,
                ApplyConditionTo.CurrentValidator)
                .MaxLen(vm => vm.GetPropertyMaxLength(nameof(vm.pds_recd_err_qty)))
                .WithName(vm => vm.GetPropertyDisplayName(nameof(vm.pds_recd_err_qty)));

            RuleFor(vm => vm.pds_recd_err_qty)
                .Must(qty => qty.IsNumeric())
                .When(vm => vm.pds_recd_err_qty.NullableToStr() != string.Empty,
                ApplyConditionTo.CurrentValidator)
                .WithMessage("錯誤總量/總包需為數值");
        }

    }
}
