using Chronox.Helpers;
using Chronox.Utilities.Extenssions;
using Enumerations;
using System;
using System.Collections.Generic;


namespace Chronox.Wrappers
{
    public class ChronoxBuildInformation
    {
        private const string TagName = "ChronoxValue";

        public ChronoxParser Parser { get; private set; }

        public DateTime DateTime { get; internal set; }

        public DateTime CurrentDate { get; } = DateTime.Now;

        public ChronoxSettings Settings { get; set; }

        public MatchWrapper LatestMatch { get; set; }

        public GroupWrapper CurrentGroup { get; set; }

        public List<ScanWrapper> ScanWrappers { get; set; }

        public List<string> SearchBreakers { get; set; }   

        public Queue<int> GrabberOffsets { get; private set; }

        public Queue<int> NumericWords { get; private set; }

        public Queue<int> NumericValues { get; private set; }

        public Queue<decimal> DecimalValues { get; private set; }

        public Queue<int> NumericOrdinals { get; private set; }

        public Queue<int> FloatingYears { get; set; }

        public Queue<int> FloatingMonths { get; set; }

        public Queue<int> FloatingWeeks { get; set; }

        public Queue<int> FloatingDays { get; set; }

        public Queue<int> FloatingHours { get; set; }

        public Queue<int> FloatingMinutes { get; set; }

        public Queue<int> FloatingSeconds { get; set; }

        public Queue<int> FloatingWeekDays { get; set; }

        public Queue<int> FloatingDayOfWeek { get; set; }

        public Queue<int> FloatingGrabbers { get; set; }

        public Queue<RangeWrapper> FloatingTimeOfDay { get; set; }

        public Queue<DateTimeUnit> ProcessedTimeUnit { get; set; }

        public Queue<DateTimeUnit> FloatingTimeUnits { get; set; }

        public Queue<TimeConjointer> FloatingConjointer { get; internal set; }

        public DayOfWeek? PreferedDayOfWeek = null;

        public string ProcessedString { get; set; }

        public string OriginalString { get; set; }

        public string NormalizedString { get; set; }

        public bool HasDayOffset { get; internal set; }

        public bool HasOrdinalNumber = false;

        public bool HasCasualExpression = false;

        public bool HasGrabberExpression = false;

        public bool HasInterpretedExpression = false;

        public bool HasTimeExpression = false;

        public bool HasRangeIndicator = false;

        public bool HasRangeSeparator = false;

        public bool HasTimeOfDay = false;

        public bool HasDayOfWeek = false;

        public bool HasYear = false;

        public bool HasMonth = false;

        public bool HasTimeUnit = false;

        public bool HasTimeFraction = false;

        public bool HasHours = false;

        public bool HasMinutes = false;

        public bool HasSeconds = false;

        public bool HasTimeZone = false;

        public bool ProcessTime = false;

        public ChronoxBuildInformation(ChronoxParser parser, List<ScanWrapper> results, string original, string normalize, ChronoxSettings settings)
        {
            this.Parser = parser;
            this.Settings = settings;
            this.ScanWrappers = results;
            this.OriginalString = original;
            this.NormalizedString = normalize;
            this.ProcessedString = normalize;
            this.CurrentDate = settings.ReferenceDate;

            this.SearchBreakers = new List<string>();
            this.NumericWords = new Queue<int>();
            this.NumericValues = new Queue<int>();
            this.FloatingHours = new Queue<int>();
            this.FloatingMinutes = new Queue<int>();
            this.FloatingSeconds = new Queue<int>();
            this.FloatingYears = new Queue<int>();
            this.FloatingMonths = new Queue<int>();
            this.FloatingWeeks = new Queue<int>();
            this.FloatingDays = new Queue<int>();
            this.FloatingWeekDays = new Queue<int>();
            this.FloatingDayOfWeek = new Queue<int>();
            this.GrabberOffsets = new Queue<int>();
            this.NumericOrdinals = new Queue<int>();
            this.DecimalValues = new Queue<decimal>();
            this.FloatingGrabbers = new Queue<int>();
            this.FloatingTimeOfDay = new Queue<RangeWrapper>();
            this.FloatingTimeUnits = new Queue<DateTimeUnit>();
            this.ProcessedTimeUnit = new Queue<DateTimeUnit>();     
            this.FloatingConjointer = new Queue<TimeConjointer>();
            
        }

        public void ResetFlags()
        {
            PreferedDayOfWeek = null;
            ProcessTime = false;
            HasOrdinalNumber = false;
            HasRangeIndicator = false;
            HasRangeSeparator = false;
            HasGrabberExpression = false;
            HasInterpretedExpression = false;
            HasTimeExpression = false;
            HasTimeFraction = false;
            HasTimeOfDay = false;
            HasDayOffset = false;
            HasMonth = false;
            HasYear = false;
            HasTimeUnit = false;
            HasTimeZone = false;
            HasCasualExpression = false;
            HasDayOfWeek = false;
            HasHours = false;
            HasMinutes = false;
            HasSeconds = false;

        }

        public void ClearFloaters()
        {
            this.GrabberOffsets.Clear();
            this.ProcessedTimeUnit.Clear();
            this.NumericWords.Clear();
            this.NumericValues.Clear();
            this.DecimalValues.Clear();
            this.NumericOrdinals.Clear();
            this.FloatingConjointer.Clear();
            this.FloatingHours.Clear();
            this.FloatingMinutes.Clear();
            this.FloatingSeconds.Clear();
            this.FloatingYears.Clear();
            this.FloatingMonths.Clear();
            this.FloatingWeeks.Clear();
            this.FloatingDays.Clear();
            this.FloatingDayOfWeek.Clear();
            this.FloatingWeekDays.Clear();
            this.FloatingTimeUnits.Clear();
            this.FloatingGrabbers.Clear();
            this.FloatingTimeOfDay.Clear();
            this.SearchBreakers.Clear();
        }

        internal void PerformPass(string value)
        {
            ProcessedString = ProcessedString.Replace(value, TagName, true);
        }
    }
}
