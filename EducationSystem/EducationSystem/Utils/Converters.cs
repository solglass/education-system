using System;
using System.Globalization;

namespace EducationSystem.API.Utils
{
    public class Converters
    {
        private const string _dateFormat = "dd.MM.yyyy";
        private const string _inputOutputPeriodDateFormat = "MMM yyyy";   // янв 2021, мар 2020
        private const string _dateWithTimeFormat = "dd.MM.yyyy H:mm:ss";
        private const string _periodDateFormat = "yyyy.MM";

        public static DateTime StrToDateTime(string strDate)
        {
            var date = DateTime
                .ParseExact(strDate, _dateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return date;
        }

        public static DateTime StrToDateTimeWithTime(string strDate)
        {
            var date = DateTime
                .ParseExact(strDate, _dateWithTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None);
            return date;
        }

        public static string DateTimeToStr(DateTime date)
        {
            return date.ToString(_dateFormat);
        }

        public static string StrToDateTimePeriod(string period)
        {
            DateTime.TryParseExact(period, _inputOutputPeriodDateFormat, CultureInfo.GetCultureInfo("ru-ru"), DateTimeStyles.None, out DateTime periodParsed);
            return PeriodDateToStr(periodParsed, _periodDateFormat);
        }

        public static string StrToStrOutputPeriod(string period)
        {
            DateTime.TryParseExact(period, _periodDateFormat, CultureInfo.GetCultureInfo("ru-ru"), DateTimeStyles.None, out DateTime periodParsed);
            return PeriodDateToStr(periodParsed, _inputOutputPeriodDateFormat);
        }

        private static string PeriodDateToStr(DateTime period, string format)
        {
            return (period.ToString(format));
        }
    }
}
