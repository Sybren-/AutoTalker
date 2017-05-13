using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FunProject.Validation
{
    class RequiredValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var content = value as string;

            if (string.IsNullOrWhiteSpace(content))
            {
                return new ValidationResult(false, "Required content");
            }

            return ValidationResult.ValidResult;
        }
    }
}
