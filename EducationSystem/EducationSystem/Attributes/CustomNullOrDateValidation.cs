using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Attributes
{
    public class CustomNullOrDateValidation : ValidationAttribute
    {

        private const string _dateFormat = "dd.MM.yyyy H:mm:ss";
        public override bool IsValid(object value)
        {
            return value != null ? DateTime.TryParseExact(
                (string)value,
                _dateFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out DateTime result) : true;
        }
    }
}
