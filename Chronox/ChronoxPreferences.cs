using Chronox.Resolutions;
using Chronox.Resolutions.Resolvers;
using Chronox.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Enumerations;
using Chronox.Constants;
using System.Globalization;

namespace Chronox
{
    internal class ChronoxPreferences
    {
        public TimeRelationResolver TimeRelationResolver { get; set; } = TimeRelationResolver.Present;

        public AmbigousResultResolver AmbigousResultResolver { get; set; } = AmbigousResultResolver.ReturnNull;

        public NoFoundResultResolver NoFoundResultResolver { get; set; } = NoFoundResultResolver.ReturnNull;

        public ExtractionResultType ParsingMode { get; set; } = ExtractionResultType.DateTime;

        public PrefferedEndian PrefferedEndian { get; set; } = PrefferedEndian.LittleEndian;

        public PrefferedHolder PrefferedDay { get; set; } = PrefferedHolder.Current;

        public DayOfWeek StartOfWeek { get; set; } = DayOfWeek.Monday;

        private int year;

        private int month;

        private int day;

        private int hour;

        private int minute;

        private int second;

        private string timeZone;

        public ChronoxPreferences(ChronoxOption options)
        {
            var dateTime = DateTime.Now;

            year = dateTime.Year;
            month = dateTime.Month;
            day = dateTime.Day;
            hour = (int)RangeConstants.MORNING_RANGE.Start;
            minute = 00;
            second = 00;
            timeZone = TimeZoneInfo.Local.DisplayName;
            StartOfWeek = DayOfWeek.Monday;
        }

        public void ChangeStartOfWeek(DayOfWeek dayOfWeek)
        {
            var culture = (CultureInfo)CultureInfo.CurrentCulture.Clone();

            var uiCulture = (CultureInfo)CultureInfo.CurrentUICulture.Clone();

            culture.DateTimeFormat.FirstDayOfWeek = dayOfWeek;

            uiCulture.DateTimeFormat.FirstDayOfWeek = dayOfWeek;

            CultureInfo.CurrentCulture = culture;

            CultureInfo.CurrentUICulture = uiCulture;
        }

        public int PreferedYear
        {
            get { return year; }
            set
            {
                year = value;
            }
        }

        public int PreferedMonth
        {
            get { return month; }
            set
            {
                month = value;
            }
        }

        public int PreferedDay
        {
            get { return day; }
            set
            {
                day = value;
            }
        }

        public int PreferedHour
        {
            get { return hour; }
            set
            {
                hour = value;
            }
        }

        public int PreferedMinute
        {
            get { return minute; }
            set
            {
                minute = value;
            }
        }

        public int PreferedSecond
        {
            get { return second; }
            set
            {
                second = value;
            }
        }

        public string PreferedTimeZone
        {
            get { return timeZone; }
            set
            {
                timeZone = value;
            }
        }

        internal int GetWeekStartOffset() => (int)StartOfWeek;
    }
}
