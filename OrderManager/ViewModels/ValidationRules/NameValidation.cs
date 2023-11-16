using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OrderManager.ViewModels.ValidationRules
{
    public class NameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string? name;
            try
            {
                name = value as string;
            }
            catch (Exception ex)
            {
                return new ValidationResult(false, $"Illegal characters or {ex.Message}");
            }

            if (string.IsNullOrEmpty(name)
                || !name.All(x => char.IsLetter(x) || char.IsWhiteSpace(x)))
            {
                return new ValidationResult(false, $"Only letters are allowed");
            }

            return ValidationResult.ValidResult;
        }
    }
}
