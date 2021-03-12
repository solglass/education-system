using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EducationSystem.API.Utils
{
    public class Converters
    {
        private const string _dateFormat = "dd.MM.yyyy";
        private const string _periodDateFormat = "yyyy.MM";
        public static DateTime StrToDateTime(string strDate)
        {
            var date = DateTime
                .ParseExact(strDate, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return date;
        }

        public static string DateTimeToStr(DateTime date)
        {
            return date.ToString(_dateFormat);
        }
        public static string StrToDateTimePeriod(string period)
        {
            DateTime.TryParseExact(period, _periodDateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime periodParsed);
            return PeriodDateToStr(periodParsed);
        }
        private static string PeriodDateToStr(DateTime period)
        {
            return (period.ToString(_periodDateFormat));
        }
    }
}
