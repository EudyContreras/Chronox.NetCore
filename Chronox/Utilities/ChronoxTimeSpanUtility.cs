using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Utilities
{
    public class ChronoxTimeSpanUtility
    {

        public static int MONTHS_IN_YEAR { get; private set; } = 12;


        public static int DAYS_IN_WEEK { get; private set; } = 7;


        public static int HOURS_IN_DAY { get; private set; } = 24;


        public static int MINUTES_IN_HOUR { get; private set; } = 60;


        public static int SECONDS_IN_MINUTE { get; private set; } = 60;


        public static int MILLIS_IN_SECOND { get; private set; } = 1000;


        public static int DAYS_IN_YEAR() => 365;

   
        public static int DAYS_IN_MONTH() => 30;


        public static int DAYS_IN_YEAR(DateTime? referenceDate) => referenceDate == null ? DAYS_IN_YEAR() : DateTime.IsLeapYear(referenceDate.Value.Year) ? 366 : 365;
        

        public static int DAYS_IN_MONTH(DateTime? referenceDate) => referenceDate == null ? DAYS_IN_MONTH() : ChronoxDateTimeUtility.DaysInMonth(referenceDate.Value);


        public static int Convert(DateTime referenceDate, DateTimeUnit source, DateTimeUnit target, int value) => (int)Convert(referenceDate, source, target, (decimal)value);


        public static decimal Convert(DateTimeUnit source, DateTimeUnit target, decimal value) => Convert(null, source, target,value);


        public static decimal Convert(DateTime? referenceDate, DateTimeUnit source, DateTimeUnit target, decimal value)
        {
            switch (source)
            {
                case DateTimeUnit.Year:
                    return ConvertFromYearsTo(referenceDate, target, value);
                case DateTimeUnit.Month:
                    return ConvertFromMonthsTo(referenceDate, target, value);
                case DateTimeUnit.Week:
                    return ConvertFromWeeksTo(referenceDate, target, value);
                case DateTimeUnit.Day:
                    return ConvertFromDaysTo(referenceDate, target, value);
                case DateTimeUnit.Hour:
                    return ConvertFromHoursTo(referenceDate, target, value);
                case DateTimeUnit.Minute:
                    return ConvertFromMinutesTo(referenceDate, target, value);
                case DateTimeUnit.Second:
                    return ConvertFromSecondsTo(referenceDate, target, value);
                case DateTimeUnit.Millisecond:
                    return ConvertToMillis(referenceDate, target, value);
            }

            return value;
        }

        private static decimal ConvertToMillis(DateTimeUnit unit, decimal value) => ConvertToMillis(null, unit, value);
        

        private static decimal ConvertToMillis(DateTime? referenceDate, DateTimeUnit unit, decimal value)
        {
            switch (unit)
            {
                case DateTimeUnit.Year:
                    return ((((value / DAYS_IN_YEAR(referenceDate)) / HOURS_IN_DAY) / MINUTES_IN_HOUR) / SECONDS_IN_MINUTE) / MILLIS_IN_SECOND;
                case DateTimeUnit.Month:
                    return ((((value / DAYS_IN_MONTH(referenceDate)) / HOURS_IN_DAY) / MINUTES_IN_HOUR) / SECONDS_IN_MINUTE) / MILLIS_IN_SECOND;
                case DateTimeUnit.Week:
                    return ((((value / DAYS_IN_WEEK) / HOURS_IN_DAY) / MINUTES_IN_HOUR) / SECONDS_IN_MINUTE) / MILLIS_IN_SECOND;
                case DateTimeUnit.Day:
                    return (((value / HOURS_IN_DAY) / MINUTES_IN_HOUR) / SECONDS_IN_MINUTE) / MILLIS_IN_SECOND;
                case DateTimeUnit.Hour:
                    return ((value / MINUTES_IN_HOUR) / SECONDS_IN_MINUTE) / MILLIS_IN_SECOND;
                case DateTimeUnit.Minute:
                    return (value / SECONDS_IN_MINUTE) / MILLIS_IN_SECOND;
                case DateTimeUnit.Second:
                    return value / MILLIS_IN_SECOND;
            }
            return value;
        }


        private static decimal ConvertFromSecondsTo(DateTimeUnit target, decimal value) => ConvertFromSecondsTo(null, target, value);


        private static decimal ConvertFromSecondsTo(DateTime? referenceDate, DateTimeUnit target, decimal value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return ((((value /SECONDS_IN_MINUTE)/ MINUTES_IN_HOUR) / HOURS_IN_DAY) / DAYS_IN_MONTH(referenceDate)) / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return (((value / SECONDS_IN_MINUTE) / MINUTES_IN_HOUR) / HOURS_IN_DAY) / DAYS_IN_MONTH(referenceDate);
                case DateTimeUnit.Day:
                    return ((value / SECONDS_IN_MINUTE) / MINUTES_IN_HOUR) / HOURS_IN_DAY;
                case DateTimeUnit.Hour:
                    return (value / SECONDS_IN_MINUTE) / MINUTES_IN_HOUR;
                case DateTimeUnit.Minute:
                    return value / SECONDS_IN_MINUTE;
                case DateTimeUnit.Second:
                    return value;
                case DateTimeUnit.Millisecond:
                    return value * MILLIS_IN_SECOND;
            }

            return value;
        }


        private static decimal ConvertFromMinutesTo(DateTimeUnit target, decimal value) => ConvertFromMinutesTo(null, target, value);


        private static decimal ConvertFromMinutesTo(DateTime? referenceDate, DateTimeUnit target, decimal value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return (((value / MINUTES_IN_HOUR)/ HOURS_IN_DAY) / DAYS_IN_MONTH(referenceDate)) / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return ((value / MINUTES_IN_HOUR) / HOURS_IN_DAY) / DAYS_IN_MONTH(referenceDate);
                case DateTimeUnit.Day:
                    return (value / MINUTES_IN_HOUR) / HOURS_IN_DAY;
                case DateTimeUnit.Hour:
                    return value / MINUTES_IN_HOUR;
                case DateTimeUnit.Minute:
                    return value;
                case DateTimeUnit.Second:
                    return value * SECONDS_IN_MINUTE;
                case DateTimeUnit.Millisecond:
                    return (value * SECONDS_IN_MINUTE) * MILLIS_IN_SECOND;
            }
            return value;
        }


        private static decimal ConvertFromHoursTo(DateTimeUnit target, decimal value) => ConvertFromHoursTo(null, target, value);


        private static decimal ConvertFromHoursTo(DateTime? referenceDate, DateTimeUnit target, decimal value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return ((value / HOURS_IN_DAY) / DAYS_IN_MONTH(referenceDate)) / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return (value / HOURS_IN_DAY) / DAYS_IN_MONTH(referenceDate);
                case DateTimeUnit.Day:
                    return value / HOURS_IN_DAY;
                case DateTimeUnit.Hour:
                    return value;
                case DateTimeUnit.Minute:
                    return value * MINUTES_IN_HOUR;
                case DateTimeUnit.Second:
                    return (value * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE;
                case DateTimeUnit.Millisecond:
                    return ((value * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE) * MILLIS_IN_SECOND;
            }
            return value;
        }


        private static decimal ConvertFromDaysTo(DateTimeUnit target, decimal value) => ConvertFromDaysTo(null, target, value);


        private static decimal ConvertFromDaysTo(DateTime? referenceDate, DateTimeUnit target, decimal value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return (value / DAYS_IN_MONTH(referenceDate)) / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return value / DAYS_IN_MONTH(referenceDate);
                case DateTimeUnit.Day:
                    return value;
                case DateTimeUnit.Hour:
                    return value * HOURS_IN_DAY;
                case DateTimeUnit.Minute:
                    return (value * HOURS_IN_DAY) * MINUTES_IN_HOUR;
                case DateTimeUnit.Second:
                    return ((value * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE;
                case DateTimeUnit.Millisecond:
                    return (((value * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE) * MILLIS_IN_SECOND;
            }
            return value;
        }


        private static decimal ConvertFromWeeksTo(DateTimeUnit target, decimal value) => ConvertFromWeeksTo(null, target, value);


        private static decimal ConvertFromWeeksTo(DateTime? referenceDate, DateTimeUnit target, decimal value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return (value * DAYS_IN_WEEK) / DAYS_IN_YEAR(referenceDate) ;
                case DateTimeUnit.Month:
                    return (value * DAYS_IN_WEEK) / DAYS_IN_MONTH(referenceDate);
                case DateTimeUnit.Day:
                    return (value * DAYS_IN_WEEK);
                case DateTimeUnit.Hour:
                    return (value * DAYS_IN_WEEK) * HOURS_IN_DAY;
                case DateTimeUnit.Minute:
                    return ((value * DAYS_IN_WEEK) * HOURS_IN_DAY) * MINUTES_IN_HOUR;
                case DateTimeUnit.Second:
                    return (((value * DAYS_IN_WEEK) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE;
                case DateTimeUnit.Millisecond:
                    return ((((value * DAYS_IN_WEEK) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE) * MILLIS_IN_SECOND;
            }
            return value;
        }

        private static decimal ConvertFromMonthsTo(DateTimeUnit target, decimal value) => ConvertFromMonthsTo(null, target, value);


        private static decimal ConvertFromMonthsTo(DateTime? referenceDate, DateTimeUnit target, decimal value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return value / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return value;
                case DateTimeUnit.Day:
                    return (value * DAYS_IN_MONTH(referenceDate));
                case DateTimeUnit.Hour:
                    return (value * DAYS_IN_MONTH(referenceDate)) * HOURS_IN_DAY;
                case DateTimeUnit.Minute:
                    return ((value * DAYS_IN_MONTH(referenceDate)) * HOURS_IN_DAY) * MINUTES_IN_HOUR;
                case DateTimeUnit.Second:
                    return (((value * DAYS_IN_MONTH(referenceDate)) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE;
                case DateTimeUnit.Millisecond:
                    return ((((value * DAYS_IN_MONTH(referenceDate)) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE) * MILLIS_IN_SECOND;
            }
            return value;
        }


        private static decimal ConvertFromYearsTo(DateTimeUnit target, decimal value) => ConvertFromYearsTo(null, target, value);


        private static decimal ConvertFromYearsTo(DateTime? referenceDate, DateTimeUnit target, decimal value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return value;
                case DateTimeUnit.Month:
                    return value * MONTHS_IN_YEAR;
                case DateTimeUnit.Day:
                    return (value * DAYS_IN_YEAR(referenceDate));
                case DateTimeUnit.Hour:
                    return (value * DAYS_IN_YEAR(referenceDate)) * HOURS_IN_DAY;
                case DateTimeUnit.Minute:
                    return ((value * DAYS_IN_YEAR(referenceDate)) * HOURS_IN_DAY) * MINUTES_IN_HOUR;
                case DateTimeUnit.Second:
                    return (((value * DAYS_IN_YEAR(referenceDate)) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE;
                case DateTimeUnit.Millisecond:
                    return ((((value * DAYS_IN_YEAR(referenceDate)) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE) * MILLIS_IN_SECOND;
            }
            return value;
        }
    }
}
