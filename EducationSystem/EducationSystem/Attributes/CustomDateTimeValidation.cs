using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Attributes
{
    public class CustomDateTimeValidation : ValidationAttribute
    {
        
        private const string _dateFormat = "dd.MM.yyyy";
        public override bool IsValid(object value)
        {
            DateTime dt;
            return DateTime.TryParseExact((string)value, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result);           
        }
    }
}
