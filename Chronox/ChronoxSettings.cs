using Chronox.Constants;
using Chronox.Exceptions;
using Chronox.Handlers;
using Chronox.Handlers.Models;
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

        public TimeRange TimeRange { get; private set; } = new TimeRange();

        internal VocabularyHandler Language { get; private set; }

        public List<SequenceCollection> ChronoxSequenceCollections { get; set; } = new List<SequenceCollection>();

        public TimeRelationResolver TimeRelationResolver { get; set; } = TimeRelationResolver.Present;

        public AmbigousResultResolver AmbigousResultResolver { get; set; } = AmbigousResultResolver.Null;

        public NoFoundResultResolver NoFoundResultResolver { get; set; } = NoFoundResultResolver.Null;

        public InvalidInputResolver InvalidInputResolver { get; set; } = InvalidInputResolver.Null;

        public ExtractionResultType ParsingMode { get; set; } = ExtractionResultType.DateTime;

        public ExpressionRelaxLevel RelaxLevel { get; set; } = ExpressionRelaxLevel.Casual;

        public PrefferedEndian PrefferedEndian { get; set; } = PrefferedEndian.LittleEndian;

        public PrefferedHolder PrefferedDay { get; set; } = PrefferedHolder.Current;

        public DayOfWeek StartOfWeek { get; set; } = DayOfWeek.Monday;

        public Language[] Languages { get; private set; } = { new Language("English", ChronoxLangSettings.Default) };

        private int year = int.MinValue;

        private int month = int.MinValue;

        private int day = int.MinValue;

        private int hour = 00;

        private int minute = 00;

        private int second = 00;

        private string timeZone = "UTC";

        internal int GetWeekStartOffset() => (int)StartOfWeek;

        public ChronoxSettings() : this(Definitions.DefaultLanguage) { }

        public ChronoxSettings(params Language[] languages)
        {
            PrefferedLanguages = languages;

            this.Language = VocabularyHandler.GetInstance(this, Definitions.LangDataPath, PrefferedLanguages);
        }

        public ChronoxSettings(SequenceCollection chronoxSequences, params Language[] languages)
        {
            PrefferedLanguages = languages;

            ChronoxSequenceCollections = new List<SequenceCollection>() { chronoxSequences };

            this.Language = VocabularyHandler.GetInstance(this, Definitions.LangDataPath, PrefferedLanguages);
        }

        public ChronoxSettings(List<SequenceCollection> chronoxSequences, params Language[] languages)
        {
            PrefferedLanguages = languages;

            ChronoxSequenceCollections = chronoxSequences;

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
                searchPassCount = value.LimitToRange(1, 10);
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
                minInputTextLength = value.LimitToRange(3, int.MaxValue);
            }
        }

        public Language[] PrefferedLanguages
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
                year = value.LimitToRange(0,10000);
            }
        }

        public int PreferedMonth
        {
            get { return month; }
            set
            {
                month = value.LimitToRange(1, 12);;
            }
        }

        public int PreferedDay
        {
            get { return day; }
            set
            {
                day = value.LimitToRange(1, 31);;
            }
        }

        public int PreferedHour
        {
            get { return hour; }
            set
            {
                hour = value.LimitToRange(1, 24);;
            }
        }

        public int PreferedMinute
        {
            get { return minute; }
            set
            {
                minute = value.LimitToRange(1, 60);;
            }
        }

        public int PreferedSecond
        {
            get { return second; }
            set
            {
                second = value.LimitToRange(1, 60);;
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

        //public void SetLangDataSetFilePath(string fullPath)
        //{
        //    Language.DestroyInstance();

        //    fullPath = fullPath.RemoveSubstrings(".txt", ".json");

        //    var parts = fullPath.Split('\\');

        //    var language = parts[parts.Length - 1];

        //    var path = fullPath.Replace(language, string.Empty).Trim();

        //    Language = VocabularyHandler.GetInstance(this, path, language);
        //}


        //TODO: Implement equals effectively by invoking different properties
        public bool Equals(ChronoxSettings other)
        {
            return ChronoxSettings.Equals(this, other);
        }
    }
}
