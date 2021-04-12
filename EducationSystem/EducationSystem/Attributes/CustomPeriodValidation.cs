using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace EducationSystem.API.Attributes
{
    public class CustomPeriodValidation : ValidationAttribute
    {
        private const string _dateFormat = "MMM yyyy";
        public override bool IsValid(object value)
        {
            return DateTime.TryParseExact(
                (string)value,
                _dateFormat,
                CultureInfo.GetCultureInfo("ru-ru"),
                DateTimeStyles.None,
                out _);
        }
    }
}
