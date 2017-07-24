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
    public class ChronoxPreferences
    {
        public TimeRelationResolver TimeRelationResolver { get; set; } = TimeRelationResolver.Present;

        public AmbigousResultResolver AmbigousResultResolver { get; set; } = AmbigousResultResolver.ReturnNull;

        public NoFoundResultResolver NoFoundResultResolver { get; set; } = NoFoundResultResolver.ReturnNull;

        public ExtractionResultType ParsingMode { get; set; } = ExtractionResultType.DateTime;

        public PrefferedEndian PrefferedEndian { get; set; } = PrefferedEndian.LittleEndian;

        public PrefferedHolder PrefferedDay { get; set; } = PrefferedHolder.Current;

        public DayOfWeek StartOfWeek { get; set; } = DayOfWeek.Monday;

        public TimeParseType TimeParsing { get; set; } = TimeParseType.MilitaryTime;

        public DateParseType DateParsing { get; set; } = DateParseType.Standard;

        public string[] Languages { get; set; } = { "English" };

        private int year = int.MinValue;

        private int month = int.MinValue;

        private int day = int.MinValue;

        private int hour = 00;

        private int minute = 00;

        private int second = 00;

        private string timeZone = "UTC";

        public ChronoxPreferences(){}


        public string[] PrefferedLanguages
        {
            get { return Languages; }
            set
            {
                Languages = value;
            }
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
