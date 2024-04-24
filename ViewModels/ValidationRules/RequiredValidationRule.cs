using System.Globalization;
using System.Windows.Controls;

namespace ViewModels.ValidationRules
{
    public class RequiredValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (string.IsNullOrWhiteSpace(value as string))
                return new ValidationResult(false, "The value must not be empty.");
            else
                return new ValidationResult(true, null);
        }
    }
}
