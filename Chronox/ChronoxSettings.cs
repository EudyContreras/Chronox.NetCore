using Chronox.Constants;
using Chronox.Exceptions;
using Chronox.Handlers;
using Chronox.Interfaces;
using Chronox.Parsers;
using Chronox.Parsers.English;
using Chronox.Resolutions;
using Chronox.Resolutions.Resolvers;
using Chronox.Scanners;
using Chronox.Utilities.Extenssions;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox
{
    public class ChronoxSettings : IEquatable<ChronoxSettings>
    {
        private int searchPassCount = 3;

        private int minInputTextLength = 3;

        public DateTime ReferenceDate = DateTime.Now;

        public VocabularyHandler Language { get; private set; }

        public TimeRelationResolver TimeRelationResolver { get; set; } = TimeRelationResolver.Present;

        public AmbigousResultResolver AmbigousResultResolver { get; set; } = AmbigousResultResolver.ReturnNull;

        public NoFoundResultResolver NoFoundResultResolver { get; set; } = NoFoundResultResolver.ReturnNull;

        public ExtractionResultType ParsingMode { get; set; } = ExtractionResultType.DateTime;

        public PrefferedEndian PrefferedEndian { get; set; } = PrefferedEndian.LittleEndian;

        public PrefferedHolder PrefferedDay { get; set; } = PrefferedHolder.Current;

        public DayOfWeek StartOfWeek { get; set; } = DayOfWeek.Monday;

        public TimeParseType TimeParsing { get; set; } = TimeParseType.MilitaryTime;

        public DateParseType DateParsing { get; set; } = DateParseType.Standard;

        private string[] Languages { get; set; } = { "English" };

        private int year = int.MinValue;

        private int month = int.MinValue;

        private int day = int.MinValue;

        private int hour = 00;

        private int minute = 00;

        private int second = 00;

        private string timeZone = "UTC";

        internal int GetWeekStartOffset() => (int)StartOfWeek;

        public ChronoxSettings() : this(null) { }

        public ChronoxSettings(params string[] languages)
        {
            PrefferedLanguages = languages;

            this.Language = VocabularyHandler.GetInstance(this, Definitions.LangDataPath, PrefferedLanguages);
        }

        public int SearchPassCount
        {
            get
            {
                return searchPassCount;
            }
            internal set
            {
                if (searchPassCount >= 1)
                {
                    searchPassCount = value;
                }
                else
                {
                    throw new UnsupportedSearchPassException(value);
                }
            }
        }

        public int MinInputTextLength
        {
            get
            {
                return minInputTextLength;
            }
            internal set
            {
                if (minInputTextLength >= 3)
                {
                    minInputTextLength = value;
                }
            }
        }

        public string[] PrefferedLanguages
        {
            get { return Languages; }
            set
            {
                if (value != null)
                {
                    Languages = value;
                }
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

        public void SetLangDataSetFilePath(string fullPath)
        {
            Language.DestroyInstance();

            fullPath = fullPath.RemoveSubstrings(".txt", ".json");

            var parts = fullPath.Split('\\');

            var language = parts[parts.Length - 1];

            var path = fullPath.Replace(language, string.Empty).Trim();

            Language = VocabularyHandler.GetInstance(this, path, language);
        }


        //TODO: Implement equals effectively by invoking different properties
        public bool Equals(ChronoxSettings other)
        {
            return ChronoxSettings.Equals(this, other);
        }
    }
}
