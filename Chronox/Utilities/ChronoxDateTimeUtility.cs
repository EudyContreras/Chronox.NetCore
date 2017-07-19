using Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Utilities
{
    public static class ChronoxDateTimeUtility
    {
        public static readonly int[] DaysInMonthInLeapYears = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public static readonly int[] DaysInMonthInRegularYears = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        public static readonly Dictionary<int, int[]> ShiftMatrix = new Dictionary<int, int[]>
            {
                {-6, new int []{1, 2, 3, 4, 5, 6, 0}},
                {-5, new int []{2, 3, 4, 5, 6, 0, 1}},
                {-4, new int []{3, 4, 5, 6, 0, 1, 2}},
                {-3, new int []{4, 5, 6, 0, 1, 2, 3}},
                {-2, new int []{5, 6, 0, 1, 2, 3, 4}},
                {-1, new int []{6, 0, 1, 2, 3, 4, 5}},
                { 0, new int []{0, 1, 2, 3, 4, 5, 6}},
                { 1, new int []{1, 2, 3, 4, 5, 6, 0}},
                { 2, new int []{2, 3, 4, 5, 6, 0, 1}},
                { 3, new int []{3, 4, 5, 6, 0, 1, 2}},
                { 4, new int []{4, 5, 6, 0, 1, 2, 3}},
                { 5, new int []{5, 6, 0, 1, 2, 3, 4}},
                { 6, new int []{6, 0, 1, 2, 3, 4, 5}},
            };

        public static DateTime CreateDateTime() => CreateDateTime(DateTime.Now);

        public static DateTime CreateDateTime(DateTime dateTime) => CreateDateTime(dateTime, 0, 0, 0, 0, 0, 0);

        public static DateTime CreateDateTime(DateTime dateTime, int dayOffset, int monthOffset, int yearOffset, int hourOffset, int minutesOffset, int secondsOffset)
        {
            return new DateTime(
                dateTime.Year + yearOffset,
                dateTime.Month + monthOffset,
                dateTime.Day + dayOffset,
                dateTime.Hour + hourOffset,
                dateTime.Minute + minutesOffset,
                dateTime.Second + secondsOffset,
                dateTime.Millisecond,
                dateTime.Kind);
        }

        public static DateTime AddWeeks(this DateTime dateTime, int numberOfWeeks) => dateTime.AddDays(numberOfWeeks * 7);

        public static DateTime SetWeekDay(this DateTime dateTime, DayOfWeek dayOfWeek) => SetWeekDay(dateTime, (int)dayOfWeek);

        public static DateTime SetYear(this DateTime dateTime, int year) => SetDate(dateTime, year, null, null, null, null, null);

        public static DateTime SetMonth(this DateTime dateTime, int month) => SetDate(dateTime, null, month, null, null, null, null);

        public static DateTime SetDay(this DateTime dateTime, int day) => SetDate(dateTime, null, null, day, null, null, null);

        public static DateTime SetHour(this DateTime dateTime, int hour) => SetDate(dateTime, null, null, null, hour, null, null);

        public static DateTime SetMinutes(this DateTime dateTime, int minute) => SetDate(dateTime, null, null, null, null, minute, null);

        public static DateTime SetSeconds(this DateTime dateTime, int seconds) => SetDate(dateTime, null, null, null, null, null, seconds);

        public static DateTime SetDate(this DateTime dateTime, int? years, int? months, int? days, int? hours, int? minutes, int? seconds)
        {
            if (years < 1) years = dateTime.Year;

            if (months > 12 || months < 1)  months = dateTime.Month;
            
            if (days > 28 || days < 1 )
            {
                var daysInMonth = DaysInMonth(years!=null ? years.Value : dateTime.Year, months!=null ? months.Value : dateTime.Month);

                if (days > daysInMonth)
                {
                    days = daysInMonth;
                }
            }

            if (hours > 24 || hours < 0) hours = dateTime.Hour;

            if (minutes > 59 || minutes < 0) minutes = dateTime.Minute;
            
            if (seconds > 59 || seconds < 0) seconds = dateTime.Second;

           return new DateTime(
                years != null ? years.Value : dateTime.Year,
                months != null ? months.Value : dateTime.Month,
                days != null ? days.Value : dateTime.Day,
                hours != null ? hours.Value : dateTime.Hour,
                minutes != null ? minutes.Value : dateTime.Minute,
                seconds != null ? seconds.Value : dateTime.Second,
                dateTime.Millisecond,
                dateTime.Kind);
        }

        public static DateTime SetDate(this DateTime dateTime, DateTime other)
        {
            return new DateTime(
                other.Year,
                other.Month,
                other.Day,
                other.Hour,
                other.Minute,
                other.Second,
                dateTime.Millisecond,
                dateTime.Kind);
        }

        public static DayOfWeek GetDayOfWeek(int dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case 0: return DayOfWeek.Sunday;
                case 1: return DayOfWeek.Monday;
                case 2: return DayOfWeek.Tuesday;
                case 3: return DayOfWeek.Wednesday;
                case 4: return DayOfWeek.Thursday;
                case 5: return DayOfWeek.Friday;
                case 6: return DayOfWeek.Saturday;
            }
            return DayOfWeek.Sunday;
        }

        public static DateTime SetWeekDay(this DateTime source, int weekDay)
        {
            var current = (int)source.DayOfWeek;

            var difference = Math.Abs(current - weekDay);

            if (current > weekDay)
            {
                return source.AddDays(-difference);
            }
            else if (current < weekDay)
            {
                return source.AddDays(difference);
            }

            return source;
        }

        public static int DaysInMonth(this DateTime source) => DaysInMonth(source.Year, source.Month);


        public static int DaysInMonth(int year, int month)
        {
            return DateTime.IsLeapYear(year) ? DaysInMonthInLeapYears[month - 1] : DaysInMonthInRegularYears[month - 1];
        }


        public static int RemainingDaysInYear(this DateTime source)
        {
            var lastDay = new DateTime(source.Year, 12, 31);

            var difference = lastDay - source;

            return (int)difference.TotalDays;
        }

        public static DateTime GetNextWeekday(this DateTime source, DayOfWeek dayOfWeek) => GetNextWeekday(source, (int)dayOfWeek);


        public static DateTime GetNextWeekday(this DateTime source, int day)
        {
            if (GetDayOfWeek(day) == source.DayOfWeek)
            {
                return source.AddDays(7);
            }
            return source.AddDays((day - (int)source.DayOfWeek + 7) % 7);
        }

        public static DateTime GetPreviousWeekday(this DateTime source, DayOfWeek dayOfWeek) => GetPreviousWeekday(source, (int)dayOfWeek);


        public static DateTime GetPreviousWeekday(this DateTime source, int day)
        {
            if(GetDayOfWeek(day) == source.DayOfWeek)
            {
                return source.AddDays(-7);
            }
            return source.AddDays((day - (int)source.DayOfWeek - 7) % 7);
        }

        public static bool HasDifferentDate(this DateTime source, DateTime target)
        {
            if(source.Day != target.Day || source.Month != target.Month || source.Year != target.Year)
            {
                return true;
            }
            return false;
        }

        public static int TotalWeekDays(this DateTime source, int month, DayOfWeek dayOfWeek) => TotalWeekDays(source, month, (int)dayOfWeek);


        public static int TotalWeekDays(this DateTime source, int month, int dayOfWeek)
        {
            var startDate = new DateTime(source.Year, source.Month, 1);

            var totalDays = startDate.AddMonths(1).Subtract(startDate).Days;

            return Enumerable.Range(1, totalDays)
                .Select(item => new DateTime(source.Year, source.Month, item))
                .Where(date => (int)date.DayOfWeek == dayOfWeek)
                .Count();
        }

        public static int GetWeeksInYear(this DateTime source, int year)
        {
            var formatInfo = DateTimeFormatInfo.CurrentInfo;

            var referenceDate = new DateTime(year, 12, 31);

            return formatInfo.Calendar.GetWeekOfYear(referenceDate, formatInfo.CalendarWeekRule, formatInfo.FirstDayOfWeek);
        }

        public static int DaysInYear(this DateTime source)
        {
            var start = new DateTime(source.Year, 1, 1);

            var end = new DateTime(source.Year, 12, 31);

            return (int)(end - start).TotalDays; 
        }

        public static int WeekNumber(this DateTime source) => WeekNumber(source, CultureInfo.CurrentCulture);


        public static int WeekNumber(this DateTime source, CultureInfo culture)
        {
            return culture.Calendar.GetWeekOfYear(source, culture.DateTimeFormat.CalendarWeekRule, culture.DateTimeFormat.FirstDayOfWeek);
        }

        internal static ChronoxDate GetDateComponent(this DateTime source) => new ChronoxDate(source.Year, source.Month, source.Day);

        internal static ChronoxTime GetTimeComponent(this DateTime source) => new ChronoxTime(source.Hour, source.Minute, source.Second);

        public static int GetSeason(this DateTime date, bool SouthHemisphere)
        {
            var hemisphereConst = (SouthHemisphere ? 2 : 0);

            Func<int, int> getReturn = (northern) => (northern + hemisphereConst) % 4;

            var value = date.Month + date.Day / 100f;

            if (value < 3.21 || value >= 12.22) return getReturn(3);

            if (value < 6.21) return getReturn(0); 

            if (value < 9.23) return getReturn(1);

            return getReturn(2);
        }

        public static DateSeasons GetSeason(this DateTime date)
        {
            var dayOfYear = date.DayOfYear - Convert.ToInt32((DateTime.IsLeapYear(date.Year)) && date.DayOfYear > 59);

            if (dayOfYear < 80 || dayOfYear >= 355) return DateSeasons.Winter;

            if (dayOfYear >= 80 && dayOfYear < 172) return DateSeasons.Spring;

            if (dayOfYear >= 172 && dayOfYear < 266) return DateSeasons.Summer;

            return DateSeasons.Autumn;
        }

        public static TimeMeridiam GetMeridiam(this DateTime date)
        {
            var hour = date.TimeOfDay.Hours;

            if (hour < 12 && hour >= 0) return TimeMeridiam.AM;

            return TimeMeridiam.PM;
        }

        public static int DayOfWeekShift(DayOfWeek dayOfWeek, int shiftAmount) => DayOfWeekShift((int)dayOfWeek, shiftAmount);

        public static int DayOfWeekShift(int dayOfWeek, int shiftAmount)
        {
            var value = ShiftMatrix[shiftAmount];

            for(var i = 0; i<value.Length; i++)
            {
                if(value[i] == dayOfWeek)
                {
                    return i;
                }
            }

            return dayOfWeek;
        }
    }
}
