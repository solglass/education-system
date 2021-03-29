using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Attributes
{
    public class CustomPeriodValidation : ValidationAttribute
    {

        private const string _dateFormat = "yyyy.MM";
        public override bool IsValid(object value)
        {
            return DateTime.TryParseExact(
                (string)value,
                _dateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result);
        }
    }
}
