using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Utilities
{
    internal class ChronoxTimeSpanUtility
    {

        public DateTime ReferenceDate { get; set; } = DateTime.Now;

        public static int MONTHS_IN_YEAR { get; private set; } = 12;

        public static int HOURS_IN_DAY { get; private set; } = 24;

        public static int MINUTES_IN_HOUR { get; private set; } = 60;

        public static int SECONDS_IN_MINUTE { get; private set; } = 60;

        public static int DAYS_IN_WEEK { get; private set; } = 7;

        public ChronoxTimeSpanUtility(DateTime referenceDate)
        {
            ReferenceDate = referenceDate;
        }

        public static int DAYS_IN_YEARS(DateTime referenceDate)
        {
            return DateTime.IsLeapYear(referenceDate.Year) ? 366 : 365;
        }

        public static int DAYS_IN_MONTH(DateTime referenceDate)
        {
            return ChronoxDateTimeUtility.DaysInMonth(referenceDate);
        }

        public int ConvertValue(DateTimeUnit source, DateTimeUnit target, int value) => (int) Convert(ReferenceDate, source, target, (double)value);


        public double ConvertValue(DateTimeUnit source, DateTimeUnit target, double value) => (int)Convert(ReferenceDate, source, target, value);


        public static int Convert(DateTime referenceDate, DateTimeUnit source, DateTimeUnit target, int value) => (int)Convert(referenceDate, source, target, (double)value);
        

        public static double Convert(DateTime referenceDate, DateTimeUnit source, DateTimeUnit target, double value)
        {
            switch (source)
            {
                case DateTimeUnit.Year:
                    return ConvertFromYearsTo(referenceDate, target, value);
                case DateTimeUnit.Month:
                    return ConvertFromMonthsTo(referenceDate, target, value);
                case DateTimeUnit.Day:
                    return ConvertFromDaysTo(referenceDate, target, value);
                case DateTimeUnit.Hour:
                    return ConvertFromHoursTo(referenceDate, target, value);
                case DateTimeUnit.Minute:
                    return ConvertFromMinutesTo(referenceDate, target, value);
                case DateTimeUnit.Second:
                    return ConvertFromSecondsTo(referenceDate, target, value);
            }

            return value;
        }

        private static double ConvertFromSecondsTo(DateTime referenceDate, DateTimeUnit target, double value)
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
            }

            return value;
        }

        private static double ConvertFromMinutesTo(DateTime referenceDate, DateTimeUnit target, double value)
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
            }
            return value;
        }

        private static double ConvertFromHoursTo(DateTime referenceDate, DateTimeUnit target, double value)
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
            }
            return value;
        }

        private static double ConvertFromDaysTo(DateTime referenceDate, DateTimeUnit target, double value)
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
            }
            return value;
        }

        private static double ConvertFromMonthsTo(DateTime referenceDate, DateTimeUnit target, double value)
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
            }
            return value;
        }

        private static double ConvertFromYearsTo(DateTime referenceDate, DateTimeUnit target, double value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return value;
                case DateTimeUnit.Month:
                    return value * MONTHS_IN_YEAR;
                case DateTimeUnit.Day:
                    return (value * DAYS_IN_YEARS(referenceDate));
                case DateTimeUnit.Hour:
                    return ((value * MONTHS_IN_YEAR) * DAYS_IN_MONTH(referenceDate)) * HOURS_IN_DAY;
                case DateTimeUnit.Minute:
                    return (((value * MONTHS_IN_YEAR) * DAYS_IN_MONTH(referenceDate)) * HOURS_IN_DAY) * MINUTES_IN_HOUR;
                case DateTimeUnit.Second:
                    return ((((value * MONTHS_IN_YEAR) * DAYS_IN_MONTH(referenceDate)) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE;
            }

            return value;
        }    
    }
}
