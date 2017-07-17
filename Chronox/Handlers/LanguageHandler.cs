
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Chronox.Utilities.Extenssions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using Chronox.Constants;
using Chronox.Handlers.Models;
using Chronox.Helpers.Patterns;
using Chronox.Wrappers;
using Chronox.Handlers.Patterns;
using Chronox.Handlers.Banks;
using Chronox.Constants.Banks;
using Chronox.Handlers.Wrappers;

namespace Chronox.Handlers
{
    internal class LanguageHandler
    {
        private static LanguageHandler instance;

        public static LanguageHandler DefaultLanguage(ChronoxOption options) => GetInstance(options, Definitions.FilePath, Definitions.DefaultLanguage);

        public Glossary Vocabulary { get; private set; }

        public PatternLibrary PatternLibrary { get; private set; }
  
        public GlossaryBank VocabularyBank { get; set; }

        public SequenceBank SequenceLibrary { get; private set; }

        public List<RegexSequence> DateTimeRegexSequences { get; private set; }

        public List<RegexSequence> DurationRegexSequences { get; private set; }

        public List<RegexSequence> RepeaterRegexSequences { get; private set; }

        public List<RegexSequence> RangedRegexSequences { get; private set; }

        public List<RegexSequence> AllRegexSequences { get; private set; }

        public Dictionary<string,string> Holidays { get; set; }

        internal List<string> SequenceRepresentations { get; private set; }

        internal PatternHandler PatternHandler { get; private set; }

        internal SequenceHandler SequenceHandler { get; private set; }

        private ChronoxOption options { get; set; }

        private LanguageHandler(ChronoxOption options, string directory, string language)
        {
            this.options = options;

            Vocabulary = !string.IsNullOrEmpty(language) ? Load(directory,language) : DefaultLanguage(options).Vocabulary;

            VocabularyBank = new GlossaryBank();

            PatternHandler = new PatternHandler();

            PatternLibrary = new PatternLibrary();

            SequenceHandler = new SequenceHandler(this);

            SequenceLibrary = new SequenceBank();

            DateTimeRegexSequences = new List<RegexSequence>();

            Holidays = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            SequenceLibrary.SequencesDateTimeCombinations.AddRange(SequenceBank.DateTimeSequences.Distinct());

            SequenceLibrary.SequencesRepeaterCombinations.AddRange(SequenceBank.RepeaterSequences.Distinct());

            SequenceLibrary.SequencesDurationCombinations.AddRange(SequenceBank.DurationSequences.Distinct());

            SequenceLibrary.SequencesRangeCombinations.AddRange(SequenceBank.TimeRangeSequences.Distinct());

            SequenceLibrary.SequencesAllCombinations.AddRange(SequenceBank.DateTimeSequences.Distinct());
            SequenceLibrary.SequencesAllCombinations.AddRange(SequenceBank.DurationSequences.Distinct());
            SequenceLibrary.SequencesAllCombinations.AddRange(SequenceBank.RepeaterSequences.Distinct());
            SequenceLibrary.SequencesAllCombinations.AddRange(SequenceBank.TimeRangeSequences.Distinct());

            SequenceRepresentations = SequenceLibrary.SequencesDateTimeCombinations
                .Select(s => s.Representation.Split('|'))
                .SelectMany(s => s)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s))
                .Select(s => string.Concat("\"",s,"\","))
                .ToList();

            CreateVocabulary();

            CreatePatterns();

            CreateSequences();

        }

        public static LanguageHandler GetInstance(ChronoxOption options, string directory, string language)
        {
            if (instance == null)
            {
                instance = new LanguageHandler(options, directory, language);
            }
            return instance;
        }

        public void DestroyInstance() => instance = null;

        private Glossary Load(string directory, string language)
        {
            var fileName = new StringBuilder(language.FirstLetterToUpper(true)).Append(".json");

            var path = Path.Combine(directory, fileName.ToString());

            var textFile = File.OpenText(path);

            var reader = new JsonTextReader(textFile);

            var jsonObject = (JObject)JToken.ReadFrom(reader);

            return JsonConvert.DeserializeObject<Glossary>(jsonObject.ToString());
        }

        private void CreateVocabulary()
        {

            PopulateGlossary(Definitions.Property.LogicalOperator);
            PopulateGlossary(Definitions.Property.CasualExpressions);       
            PopulateGlossary(Definitions.Property.GrabberExpressions);  
            PopulateGlossary(Definitions.Property.DayOffset);
            PopulateGlossary(Definitions.Property.TimeExpressions);
            PopulateGlossary(Definitions.Property.TimeFractions);
            PopulateGlossary(Definitions.Property.TimeConjointer);
            PopulateGlossary(Definitions.Property.InterpretedExpression);
            PopulateGlossary(Definitions.Property.ArithmeticOperator);
            PopulateGlossary(Definitions.Property.RangeIndicator);
            PopulateGlossary(Definitions.Property.RangeSeparator);
            PopulateGlossary(Definitions.Property.RepeaterExpressions);
            PopulateGlossary(Definitions.Property.RepeaterIndicators);
            PopulateGlossary(Definitions.Property.DurationExpressions);
            PopulateGlossary(Definitions.Property.DurationIndicators);
            PopulateGlossary(Definitions.Property.Proximity);
            PopulateGlossary(Definitions.Property.Holidays);
            PopulateGlossary(Definitions.Property.TimePeriods);
            PopulateGlossary(Definitions.Property.DecadeValues);
            PopulateGlossary(Definitions.Property.TimeOfDay);
            PopulateGlossary(Definitions.Property.DaysOfWeek);
            PopulateGlossary(Definitions.Property.SeasonOfYear);
            PopulateGlossary(Definitions.Property.MonthsOfYear);
            PopulateGlossary(Definitions.Property.DateTimeUnits);
            PopulateGlossary(Definitions.Property.TimeUnits);
            PopulateGlossary(Definitions.Property.DateUnits);
            PopulateGlossary(Definitions.Property.YearUnit);
            PopulateGlossary(Definitions.Property.MonthUnit);
            PopulateGlossary(Definitions.Property.WeekUnit);
            PopulateGlossary(Definitions.Property.DayUnit);
            PopulateGlossary(Definitions.Property.NumericValue);
            PopulateGlossary(Definitions.Property.NumericMagnitudeCardinal);
            PopulateGlossary(Definitions.Property.NumericMagnitudeOrdinal);
            PopulateGlossary(Definitions.Property.NumericWord);
            PopulateGlossary(Definitions.Property.NumericWordOrdinal);
            PopulateGlossary(Definitions.Property.NumericWordCardinal);
            PopulateGlossary(Definitions.Property.TimeMeridiam);
        }

        private void PopulateGlossary(string label)
        {
            var section = Vocabulary.Section.Find(s => string.Compare(s.Label, label, true) == 0);

            if(section == null)
            {
                var dateTimeUnit = Vocabulary.Section.Find(s => string.Compare(s.Label, Definitions.Property.DateTimeUnits) == 0); 

                switch (label)
                {
                    case Definitions.Property.DateUnits:
                        section = new Section
                        {
                            Label = label,

                            Type = dateTimeUnit.Type,

                            Properties = dateTimeUnit.Properties.Where(p => IsDateUnit(p.Key)).ToList()                            
                        };

                        Vocabulary.Section.Add(section);

                        break;
                    case Definitions.Property.TimeUnits:
                        section = new Section
                        {
                            Label = label,

                            Type = dateTimeUnit.Type,

                            Properties = dateTimeUnit.Properties.Where(p => IsTimeUnit(p.Key)).ToList()
                        };

                        Vocabulary.Section.Add(section);

                        break;
                    default:

                        section = CreateSeparateUnits(label, dateTimeUnit);

                        if(section != null)
                        {
                            Vocabulary.Section.Add(section);
                        }

                        break;
                }
            }
            var isHoliday = false;

            if (string.Compare(section.Label, Definitions.Property.Holidays) == 0)
            {
                isHoliday = true;
            }

            foreach (var property in section.Properties)
            {
                if (isHoliday)
                {
                    Holidays.Add(property.Key, property.Value);
                }
                foreach (var variation in property.Variations)
                {
                    if (!VocabularyBank.GetDictionary(section.Label).ContainsKey(variation))
                    {
                        VocabularyBank.GetDictionary(section.Label).Add(variation, property.Key);
                    }
                }
            }
        }

        private Section CreateSeparateUnits(string label, Section section)
        {
            switch (label)
            {
                case Definitions.Property.DayUnit:
                    return new Section
                    {
                        Label = Definitions.Property.DayUnit,

                        Type = PatternOption.PatternCreationType.None,

                        Properties = section.Properties.Where(p => string.Compare(p.Key, Definitions.General.Day) == 0).ToList()
                    };
                case Definitions.Property.YearUnit:
                    return new Section
                    {
                        Label = Definitions.Property.YearUnit,

                        Type = PatternOption.PatternCreationType.None,

                        Properties = section.Properties.Where(p => string.Compare(p.Key, Definitions.General.Year) == 0).ToList()
                    };
                case Definitions.Property.WeekUnit:
                    return new Section
                    {
                        Label = Definitions.Property.WeekUnit,

                        Type = PatternOption.PatternCreationType.None,

                        Properties = section.Properties.Where(p => string.Compare(p.Key, Definitions.General.Week) == 0).ToList()
                    };
                case Definitions.Property.MonthUnit:
                    return new Section
                    {
                        Label = Definitions.Property.MonthUnit,

                        Type = PatternOption.PatternCreationType.None,

                        Properties = section.Properties.Where(p => string.Compare(p.Key, Definitions.General.Month) == 0).ToList()
                    };         
                }

            return null;
        }

        private void CreatePatterns()
        {
            foreach(var section in Vocabulary.Section)
            {
                foreach(var property in section.Properties)
                {
                    if(string.IsNullOrEmpty(property.Pattern) || string.IsNullOrWhiteSpace(property.Pattern))
                    {
                        property.Pattern = PatternHandler.ComputePattern(property.Variations);
                    }
                }
            }

            CreatePattern(Definitions.Property.LogicalOperator);
            CreatePattern(Definitions.Property.CasualExpressions);
            CreatePattern(Definitions.Property.GrabberExpressions);
            CreatePattern(Definitions.Property.DayOffset);
            CreatePattern(Definitions.Property.TimeExpressions);
            CreatePattern(Definitions.Property.TimeFractions);
            CreatePattern(Definitions.Property.TimeConjointer);
            CreatePattern(Definitions.Property.InterpretedExpression);
            CreatePattern(Definitions.Property.ArithmeticOperator);
            CreatePattern(Definitions.Property.RangeIndicator);
            CreatePattern(Definitions.Property.RangeSeparator);
            CreatePattern(Definitions.Property.RepeaterExpressions);
            CreatePattern(Definitions.Property.RepeaterIndicators);
            CreatePattern(Definitions.Property.DurationExpressions);
            CreatePattern(Definitions.Property.DurationIndicators);
            CreatePattern(Definitions.Property.Proximity);
            CreatePattern(Definitions.Property.Holidays);
            CreatePattern(Definitions.Property.TimePeriods);
            CreatePattern(Definitions.Property.DecadeValues);
            CreatePattern(Definitions.Property.TimeOfDay);
            CreatePattern(Definitions.Property.DaysOfWeek);
            CreatePattern(Definitions.Property.SeasonOfYear);
            CreatePattern(Definitions.Property.MonthsOfYear);
            CreatePattern(Definitions.Property.DateTimeUnits);
            CreatePattern(Definitions.Property.TimeUnits);
            CreatePattern(Definitions.Property.DateUnits);
            CreatePattern(Definitions.Property.YearUnit);
            CreatePattern(Definitions.Property.MonthUnit);
            CreatePattern(Definitions.Property.WeekUnit);
            CreatePattern(Definitions.Property.DayUnit);
            CreatePattern(Definitions.Property.NumericValue);
            CreatePattern(Definitions.Property.NumericMagnitudeCardinal);
            CreatePattern(Definitions.Property.NumericMagnitudeOrdinal);
            CreatePattern(Definitions.Property.NumericWord);
            CreatePattern(Definitions.Property.NumericWordOrdinal);
            CreatePattern(Definitions.Property.NumericWordCardinal);
            CreatePattern(Definitions.Property.TimeMeridiam);

            PatternLibrary.Patterns.AddAll(PatternLibrary.CommonDatePatterns);

            PatternLibrary.Patterns.AddAll(PatternLibrary.CommonTimePatterns);

            PatternLibrary.Patterns.AddAll(PatternLibrary.CommonYearPatterns);
        }

        private void CreatePattern(string label)
        {
            var combined = PatternHandler.GeneratePattern(PatternOption.Standard, Vocabulary, label);

            PatternLibrary.Patterns.Add(label, new RegexPattern(label, combined));
        }

        private void CreateSequences()
        {
            SequenceHandler.ExtractStandAlonePatterns();

            SequenceHandler.ExtractPatternSequences(Vocabulary);

            AllRegexSequences = new List<RegexSequence>();

            DateTimeRegexSequences = SequenceHandler.BuildPatternSequences(options, SequenceLibrary.SequencesDateTimeCombinations, PatternLibrary.Patterns);
            DateTimeRegexSequences.ForEach(s => s.ComputeRelevance(DateTimeRegexSequences.OrderByDescending(p => s.PatternCount).FirstOrDefault().PatternCount));

            DurationRegexSequences = SequenceHandler.BuildPatternSequences(options, SequenceLibrary.SequencesDurationCombinations, PatternLibrary.Patterns);        
            DurationRegexSequences.ForEach(s => s.ComputeRelevance(DurationRegexSequences.OrderByDescending(p => s.PatternCount).FirstOrDefault().PatternCount));

            RepeaterRegexSequences = SequenceHandler.BuildPatternSequences(options, SequenceLibrary.SequencesRepeaterCombinations, PatternLibrary.Patterns);
            RepeaterRegexSequences.ForEach(s => s.ComputeRelevance(RepeaterRegexSequences.OrderByDescending(p => s.PatternCount).FirstOrDefault().PatternCount));

            RangedRegexSequences = SequenceHandler.BuildPatternSequences(options, SequenceLibrary.SequencesRangeCombinations, PatternLibrary.Patterns);
            RangedRegexSequences.ForEach(s => s.ComputeRelevance(RangedRegexSequences.OrderByDescending(p => s.PatternCount).FirstOrDefault().PatternCount));

            AllRegexSequences.AddRange(DateTimeRegexSequences);
            AllRegexSequences.AddRange(DurationRegexSequences);
            AllRegexSequences.AddRange(RepeaterRegexSequences);
            AllRegexSequences.AddRange(RangedRegexSequences);
        }

        public bool IsDateUnit(string key)
        {
            return !IsTimeUnit(key);
        }

        public bool IsTimeUnit(string key)
        {
            if (string.Compare(key, Definitions.General.Hour) == 0) return true;

            if (string.Compare(key, Definitions.General.Minute) == 0) return true;

            if (string.Compare(key, Definitions.General.Second) == 0) return true;

            return false;
        }
    }
}
