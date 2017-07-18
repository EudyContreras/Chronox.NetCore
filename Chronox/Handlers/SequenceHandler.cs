using Chronox.Constants;
using Chronox.Handlers.Models;
using Chronox.Wrappers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Chronox.Helpers.Patterns;
using Chronox.Constants.Banks;
using Chronox.Utilities.Extenssions;
using Enumerations;

namespace Chronox.Handlers
{
    internal class SequenceHandler
    {
     
        internal LanguageHandler LanguageHandler { get; private set; }

        internal RegexPattern TimePattern { get; set; }

        internal RegexPattern DatePatternBigEndian { get; set; }

        internal RegexPattern DatePatternMediumEndian { get; set; }

        internal RegexPattern DatePatternLittleEndian { get; set; }

        internal RegexPattern HoursDiscretePattern { get; set; }

        internal RegexPattern MinutesDiscretePattern { get; set; }

        internal RegexPattern SecondsDiscretePattern { get; set; }

        public SequenceHandler(LanguageHandler languageHandler)
        {   
            LanguageHandler = languageHandler;
        }

        internal void ExtractStandAlonePatterns()
        {
            DatePatternBigEndian = CreateDatePattern(DateTimeEndian.Big);

            DatePatternMediumEndian = CreateDatePattern(DateTimeEndian.Middle);

            DatePatternLittleEndian = CreateDatePattern(DateTimeEndian.Little);

            HoursDiscretePattern = CreateHoursPattern();

            MinutesDiscretePattern = CreateMinutesPattern();

            SecondsDiscretePattern = CreateSecondsPattern();

            TimePattern = CreateTimePattern();
        }

        public void AddSequence(SequenceType type, string label, params string[] properties) => AddSequence(type, label, null, properties);

        public void AddSequence(SequenceType type, string label, string representation, params string[] properties) => AddSequence(new Sequence(type, representation, properties));

        public void AddSequence(Sequence sequence)
        {
            if (SequenceOK(sequence))
            {
                switch (sequence.SequenceType)
                {
                    case SequenceType.DateTime:
                        LanguageHandler.SequenceLibrary.SequencesDateTimeCombinations.Add(sequence);
                        break;
                    case SequenceType.DateRange:
                        LanguageHandler.SequenceLibrary.SequencesRangeCombinations.Add(sequence);
                        break;
                    case SequenceType.Duration:
                        LanguageHandler.SequenceLibrary.SequencesDurationCombinations.Add(sequence);
                        break;
                    case SequenceType.Repeater:
                        LanguageHandler.SequenceLibrary.SequencesRepeaterCombinations.Add(sequence);
                        break;
                }
            }
        }

        private bool HasDuplicates<T>(List<T> myList)
        {
            var hashSet = new HashSet<T>();

            for (var i = 0; i < myList.Count; ++i)
            {
                if (!hashSet.Add(myList[i])) return true;
            }
            return false;
        }

        public List<RegexSequence> BuildPatternSequences(ChronoxSettings settings, List<Sequence> sequences, Dictionary<string,RegexPattern> patterns)
        {
            var regexSequences = new List<RegexSequence>();

            var separator = PatternLibrary.HelperPatterns[Definitions.Patterns.SpaceSeparator];

            var optionalSeparator = PatternLibrary.HelperPatterns[Definitions.Patterns.OptionalSpace];

            foreach (var sequence in sequences)
            {
                var builder = new StringBuilder();

                var regexSequence = new RegexSequence(sequence.SequenceType);

                foreach(var property in sequence.DateProperties)
                {
                    var pattern = RegexPattern.Null;

                    pattern = patterns.ContainsKey(property) ? patterns[property] : null;

                    pattern = CheckPattern(property, pattern);

                    if (pattern == null)
                    {
                       PatternLibrary.HelperPatterns.TryGetValue(property,out pattern);                      

                        if (pattern == null)
                        {   
                            if(!IdentifyAndAssign(property, ref pattern))
                            {
                                continue;
                            }
                        }
                        else
                        {
                            pattern.LabelValue();
                        }
                    }

                    regexSequence.Patterns.Add(pattern);      

                    if (NotSeparator(pattern.Label))
                    {
                        if(pattern.Label == Definitions.Patterns.Time || pattern.Label == Definitions.Patterns.HourDiscrete)
                        {
                            regexSequence.Patterns.Add(optionalSeparator);
                        }
                        else
                        {
                            regexSequence.Patterns.Add(separator);
                        }

                        regexSequence.PatternCount += 1;
                    }

                    builder.Append(pattern.Value);

                    if (NotSeparator(pattern.Label))
                    {
                        if (pattern.Label == Definitions.Patterns.Time || pattern.Label == Definitions.Patterns.HourDiscrete)
                        {
                            builder.Append(optionalSeparator.Value);
                        }
                        else
                        {
                            builder.Append(separator.Value);
                        }
                    }
                }

                var result = builder.ToString();

                if (result.EndsWith(separator.Value))
                {
                    result = result.ReplaceLast(separator.Value, string.Empty);
                }

                if (regexSequence.Patterns[regexSequence.Patterns.Count - 1].Label == Definitions.Patterns.SpaceSeparator)
                {
                    regexSequence.Patterns.RemoveAt(regexSequence.Patterns.Count - 1);
                }

                regexSequence.CombinedPattern = result;

                regexSequences.Add(regexSequence);

                builder.Clear();
            }

            return regexSequences;
        }

        private bool IdentifyAndAssign(string property, ref RegexPattern pattern)
        {
            var identify = true;

            if (string.Compare(property, Definitions.Patterns.Time, true) == 0)
            {
                pattern = TimePattern;
            }
            else if (string.Compare(property, Definitions.Patterns.DateBigEndian, true) == 0)
            {
                pattern = DatePatternBigEndian;
            }
            else if (string.Compare(property, Definitions.Patterns.DateMiddleEndian, true) == 0)
            {
                pattern = DatePatternMediumEndian;
            }
            else if (string.Compare(property, Definitions.Patterns.DateLittleEndian, true) == 0)
            {
                pattern = DatePatternLittleEndian;
            }
            else if (string.Compare(property, Definitions.Patterns.HourDiscrete) == 0)
            {
                pattern = HoursDiscretePattern;
            }
            else if (string.Compare(property, Definitions.Patterns.MinuteDiscrete) == 0)
            {
                pattern = MinutesDiscretePattern;
            }
            else if (string.Compare(property, Definitions.Patterns.SecondDiscrete) == 0)
            {
                pattern = SecondsDiscretePattern;
            }
            else
            {
                identify =  false;
            }

            return identify;
        }

        private RegexPattern CheckPattern(string property,RegexPattern pattern)
        {
            if (string.Compare(property, Definitions.Patterns.Year) == 0)
            {
                var year = PatternLibrary.CommonYearPatterns[Definitions.Patterns.Year];

               return new RegexPattern(year.Label, PatternHandler.LabelWrapp(year.Label, year.Value));
            }
            else if (string.Compare(property, Definitions.Patterns.HourDiscrete) == 0)
            {
                var hour = new RegexPattern(Definitions.Patterns.HourDiscrete, PatternHandler.LabelWrapp(Definitions.Patterns.HourDiscrete, pattern.Value));

                return hour;
            }
            else if (string.Compare(property, Definitions.Patterns.MinuteDiscrete) == 0)
            {
                var minute = new RegexPattern(Definitions.Patterns.MinuteDiscrete, PatternHandler.LabelWrapp(Definitions.Patterns.MinuteDiscrete, pattern.Value));

                return minute;
            }
            else if(string.Compare(property, Definitions.Patterns.SecondDiscrete) == 0)
            {
                var second = new RegexPattern(Definitions.Patterns.SecondDiscrete, PatternHandler.LabelWrapp(Definitions.Patterns.SecondDiscrete, pattern.Value));

                return second;
            }
            else
            {
                return pattern;
            }
        }

        private RegexPattern CreateDatePattern(DateTimeEndian endian)
        {
            var year = PatternLibrary.CommonDatePatterns[Definitions.Patterns.YearDiscrete];

            var month = PatternLibrary.CommonDatePatterns[Definitions.Patterns.MonthDiscrete];

            var day = PatternLibrary.CommonDatePatterns[Definitions.Patterns.DayDiscrete];

            var separator = PatternLibrary.HelperPatterns[Definitions.Patterns.DateSeparator].Value;

            var labeledYear = PatternHandler.LabelWrapp(year.Label, year.Value);

            var labeledMonth = PatternHandler.LabelWrapp(month.Label, month.Value);

            var labeledDay = PatternHandler.LabelWrapp(day.Label, day.Value);

            switch (endian)
            {
                case DateTimeEndian.Little:

                    return DatePatternLittleEndian = new RegexPattern(Definitions.Patterns.DateLittleEndian, PatternHandler.LabelWrapp(Definitions.Patterns.DateLittleEndian, string.Concat(labeledDay,separator, labeledMonth, separator, labeledYear)));
                  
                case DateTimeEndian.Middle:

                    return DatePatternMediumEndian = new RegexPattern(Definitions.Patterns.DateMiddleEndian, PatternHandler.LabelWrapp(Definitions.Patterns.DateMiddleEndian, string.Concat(labeledMonth, separator, labeledDay, separator, labeledYear)));
                    
                case DateTimeEndian.Big:

                    return DatePatternBigEndian = new RegexPattern(Definitions.Patterns.DateBigEndian, PatternHandler.LabelWrapp(Definitions.Patterns.DateBigEndian, string.Concat(labeledYear, separator, labeledMonth, separator, labeledDay)));
              
            }

            return null;
        }

        private RegexPattern CreateTimePattern()
        {
            var hours = PatternLibrary.CommonTimePatterns[Definitions.Patterns.Hour];

            var minutes = PatternLibrary.CommonTimePatterns[Definitions.Patterns.Minute];

            var seconds = PatternLibrary.CommonTimePatterns[Definitions.Patterns.Second];

            var millis = PatternLibrary.CommonTimePatterns[Definitions.Patterns.Millis];

            var zone = PatternLibrary.CommonTimePatterns[Definitions.Patterns.ZoneOffset];

            var labeledHour = PatternHandler.LabelWrapp(hours.Label, hours.Value);

            var labeledMinute = PatternHandler.LabelWrapp(minutes.Label, minutes.Value);

            var labeledSecond = PatternHandler.LabelWrapp(seconds.Label, seconds.Value);

            var labeledMillis = PatternHandler.LabelWrapp(millis.Label, millis.Value);

            var labeledZone = PatternHandler.LabelWrapp(zone.Label, zone.Value);

            return new RegexPattern(Definitions.Patterns.Time, PatternHandler.GroupWrapp(string.Concat(labeledHour, labeledMinute, labeledSecond, labeledMillis)));
        }

        private RegexPattern CreateHoursPattern()
        {
            var hours = PatternLibrary.CommonTimePatterns[Definitions.Patterns.HourDiscrete];

            var labeledHours = PatternHandler.LabelWrapp(hours.Label, hours.Value);

            var oclockSuffixes = LanguageHandler.VocabularyBank.GetDictionary(Definitions.Property.CasualExpressions).Where(e => Definitions.Converters.CASUAL_EXPRESSION[e.Value] == DateCasualExpression.Oclock).Select(e => e.Key).ToList();

            var pattern = PatternHandler.OptionalWrapp(LanguageHandler.PatternHandler.ComputePattern(oclockSuffixes));

            var hourPattern = string.Concat(labeledHours,PatternLibrary.HelperPatterns[Definitions.Patterns.OptionalSpace], pattern);

            return new RegexPattern(Definitions.Patterns.HourDiscrete, hourPattern);
        }

        private RegexPattern CreateMinutesPattern()
        {
            var minutes = PatternLibrary.CommonTimePatterns[Definitions.Patterns.MinuteDiscrete];

            var labeledMinutes = PatternHandler.LabelWrapp(minutes.Label, minutes.Value);

            return new RegexPattern(Definitions.Patterns.MinuteDiscrete, labeledMinutes);
        }

        private RegexPattern CreateSecondsPattern()
        {
            var seconds = PatternLibrary.CommonTimePatterns[Definitions.Patterns.SecondDiscrete];

            var labeledSeconds = PatternHandler.LabelWrapp(seconds.Label, seconds.Value);

            return new RegexPattern(Definitions.Patterns.SecondDiscrete, labeledSeconds);
        }

        public void ExtractPatternSequences(Glossary glossary)
        {
            foreach (var format in glossary.SupportedDateFormats)
            {
                var parts = format.Split(':');

                var type = GetSequenceType(parts[0]);

                if (parts.Length > 1)
                {
                    var properties = parts[1].RemoveSubstrings("(", ")").Split('|');

                    var normalProperties = new List<string>();

                    var last = string.Empty;

                    for (var i = 0; i < properties.Length; i++)
                    {
                        var property = Definitions.Converters.PROPERTIES[properties[i]];

                        normalProperties.Add(property);

                        if (glossary.AssumeSpace)
                        {
                            if (i < properties.Length - 1 && property != Definitions.Patterns.SpaceSeparator)
                            {
                                if (properties[i + 1] != Definitions.Converters.PROPERTIES.FirstOrDefault(e => e.Value == Definitions.Patterns.SpaceSeparator).Key)
                                {
                                    normalProperties.Add(Definitions.Patterns.SpaceSeparator);
                                }
                            }
                        }
                    }

                    var sequence = new Sequence(type, normalProperties.ToArray());

                    AddSequence(sequence);
                }
            }
        }

        private SequenceType GetSequenceType(string type)
        {
            if (string.Compare(type, Definitions.General.DateTime) == 0) return SequenceType.DateTime;

            if (string.Compare(type, Definitions.General.TimeRange) == 0) return SequenceType.DateRange;

            if (string.Compare(type, Definitions.General.TimeSet) == 0) return SequenceType.Repeater;

            if (string.Compare(type, Definitions.General.TimeSpan) == 0) return SequenceType.Duration;

            return SequenceType.DateTime;
        }

        private bool NotSeparator(string label)
        {
            if (string.Compare(label, Definitions.Patterns.SpaceSeparator, true) == 0 || string.Compare(label, Definitions.Patterns.OptionalSpace) == 0) return false;

            return true;
        }

        private bool SequenceOK(Sequence sequence)
        {
            if (sequence == null) return false;

            if (sequence.DateProperties == null) return false;

            if (sequence.DateProperties.Count <= 0) return false;

            return true;
        }
    }
}
