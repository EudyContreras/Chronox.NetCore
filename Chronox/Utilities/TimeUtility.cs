using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Utilities
{
    internal class TimeUtility
    {
        public static int DAYS_IN_YEARS { get; private set; } = 365;

        public static int DAYS_IN_MONTH { get; private set; } = 28 | 29 | 30 | 31;

        public static int MONTHS_IN_YEAR { get; private set; } = 12;

        public static int HOURS_IN_DAY { get; private set; } = 24;

        public static int MINUTES_IN_HOUR { get; private set; } = 60;

        public static int SECONDS_IN_MINUTE { get; private set; } = 60;

        public static int DAYS_IN_WEEK { get; private set; } = 7;

        public TimeUtility()
        {
            DAYS_IN_YEARS = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

            DAYS_IN_MONTH = DateTimeUtility.DaysInMonth(DateTime.Now);
        }

        static TimeUtility()
        {
            DAYS_IN_YEARS = DateTime.IsLeapYear(DateTime.Now.Year) ? 366 : 365;

            DAYS_IN_MONTH = DateTimeUtility.DaysInMonth(DateTime.Now);
        }

        public int ConvertValue(DateTimeUnit source, DateTimeUnit target, int value) => (int) Convert(source, target, (double)value);


        public double ConvertValue(DateTimeUnit source, DateTimeUnit target, double value) => (int)Convert(source, target, value);


        public static int Convert(DateTimeUnit source, DateTimeUnit target, int value) => (int)Convert(source, target, (double)value);
        

        public static double Convert(DateTimeUnit source, DateTimeUnit target, double value)
        {
            switch (source)
            {
                case DateTimeUnit.Year:
                    return ConvertFromYearsTo(target, value);
                case DateTimeUnit.Month:
                    return ConvertFromMonthsTo(target, value);
                case DateTimeUnit.Day:
                    return ConvertFromDaysTo(target, value);
                case DateTimeUnit.Hour:
                    return ConvertFromHoursTo(target, value);
                case DateTimeUnit.Minute:
                    return ConvertFromMinutesTo(target, value);
                case DateTimeUnit.Second:
                    return ConvertFromSecondsTo(target, value);
            }

            return value;
        }

        private static double ConvertFromSecondsTo(DateTimeUnit target, double value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return ((((value /SECONDS_IN_MINUTE)/ MINUTES_IN_HOUR) / HOURS_IN_DAY) / DAYS_IN_MONTH) / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return (((value / SECONDS_IN_MINUTE) / MINUTES_IN_HOUR) / HOURS_IN_DAY) / DAYS_IN_MONTH;
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

        private static double ConvertFromMinutesTo(DateTimeUnit target, double value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return (((value / MINUTES_IN_HOUR)/ HOURS_IN_DAY) / DAYS_IN_MONTH) / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return ((value / MINUTES_IN_HOUR) / HOURS_IN_DAY) / DAYS_IN_MONTH;
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

        private static double ConvertFromHoursTo(DateTimeUnit target, double value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return ((value / HOURS_IN_DAY) / DAYS_IN_MONTH) / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return (value / HOURS_IN_DAY) / DAYS_IN_MONTH;
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

        private static double ConvertFromDaysTo(DateTimeUnit target, double value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return (value / DAYS_IN_MONTH) / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return value / DAYS_IN_MONTH;
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

        private static double ConvertFromMonthsTo(DateTimeUnit target, double value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return value / MONTHS_IN_YEAR;
                case DateTimeUnit.Month:
                    return value;
                case DateTimeUnit.Day:
                    return (value * DAYS_IN_MONTH);
                case DateTimeUnit.Hour:
                    return (value * DAYS_IN_MONTH) * HOURS_IN_DAY;
                case DateTimeUnit.Minute:
                    return ((value * DAYS_IN_MONTH) * HOURS_IN_DAY) * MINUTES_IN_HOUR;
                case DateTimeUnit.Second:
                    return (((value * DAYS_IN_MONTH) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE;
            }
            return value;
        }

        private static double ConvertFromYearsTo(DateTimeUnit target, double value)
        {
            switch (target)
            {
                case DateTimeUnit.Year:
                    return value;
                case DateTimeUnit.Month:
                    return value * MONTHS_IN_YEAR;
                case DateTimeUnit.Day:
                    return (value * DAYS_IN_YEARS);
                case DateTimeUnit.Hour:
                    return ((value * MONTHS_IN_YEAR) * DAYS_IN_MONTH) * HOURS_IN_DAY;
                case DateTimeUnit.Minute:
                    return (((value * MONTHS_IN_YEAR) * DAYS_IN_MONTH) * HOURS_IN_DAY) * MINUTES_IN_HOUR;
                case DateTimeUnit.Second:
                    return ((((value * MONTHS_IN_YEAR) * DAYS_IN_MONTH) * HOURS_IN_DAY) * MINUTES_IN_HOUR) * SECONDS_IN_MINUTE;
            }

            return value;
        }    
    }
}
