using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SerbiaRailway.services
{

    public class TimeValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string input = value.ToString();
            try
            {
                TimeSpan.Parse(input);
            }
            catch
            {
                return new ValidationResult(false, "Invalid input.");
            }
            return new ValidationResult(true, null);
        }
    }

    public class PriceValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            string input = value.ToString();
            try
            {
                int.Parse(input);
            }
            catch
            {
                return new ValidationResult(false, "Invalid input.");
            }
            return new ValidationResult(true, null);
        }
    }
}
