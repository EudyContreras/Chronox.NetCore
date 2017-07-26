
using Chronox.Constants;
using Chronox.Constants.Banks;
using Chronox.Handlers.Banks;
using Chronox.Handlers.Models;
using Chronox.Handlers.Patterns;
using Chronox.Handlers.Wrappers;
using Chronox.Helpers.Patterns;
using Chronox.Utilities.Extenssions;
using Chronox.Wrappers;
using Enumerations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronox.Handlers
{
    public class VocabularyHandler
    {
        private static VocabularyHandler instance;

        public static VocabularyHandler DefaultLanguage(ChronoxSettings settings) => GetInstance(settings, Definitions.TextLangDataPath, Definitions.DefaultLanguage);

        public Glossary Vocabulary { get; private set; }

        public PatternLibrary PatternLibrary { get; private set; }
  
        public GlossaryBank VocabularyBank { get; set; }

        public SequenceBank SequenceLibrary { get; private set; }

        public List<PatternSequence> DateTimeRegexSequences { get; private set; }

        public List<PatternSequence> DurationRegexSequences { get; private set; }

        public List<PatternSequence> RepeaterRegexSequences { get; private set; }

        public List<PatternSequence> RangedRegexSequences { get; private set; }

        public List<PatternSequence> AllRegexSequences { get; private set; }

        public Dictionary<string,string> Holidays { get; set; }

        internal List<string> SequenceRepresentations { get; private set; }

        internal PatternHandler PatternHandler { get; private set; }

        internal SequenceHandler SequenceHandler { get; private set; }

        private ChronoxSettings Settings { get; set; }

        private VocabularyHandler(ChronoxSettings settings, string directory, params string[] languages)
        {
            this.Settings = settings;

            Vocabulary = Load(directory, languages);

            VocabularyBank = new GlossaryBank();

            PatternHandler = new PatternHandler();

            PatternLibrary = new PatternLibrary();

            SequenceHandler = new SequenceHandler(this);

            SequenceLibrary = new SequenceBank();

            DateTimeRegexSequences = new List<PatternSequence>();

            Holidays = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

            SequenceLibrary.SequencesDateTimeCombinations.AddRange(SequenceBank.DateTimeSequences.ToArray());

            SequenceLibrary.SequencesRepeaterCombinations.AddRange(SequenceBank.RepeaterSequences.ToArray());

            SequenceLibrary.SequencesDurationCombinations.AddRange(SequenceBank.DurationSequences.ToArray());

            SequenceLibrary.SequencesRangeCombinations.AddRange(SequenceBank.TimeRangeSequences.ToArray());

            SequenceLibrary.SequencesAllCombinations.AddRange(SequenceBank.DateTimeSequences.ToArray());
            SequenceLibrary.SequencesAllCombinations.AddRange(SequenceBank.DurationSequences.ToArray());
            SequenceLibrary.SequencesAllCombinations.AddRange(SequenceBank.RepeaterSequences.ToArray());
            SequenceLibrary.SequencesAllCombinations.AddRange(SequenceBank.TimeRangeSequences.ToArray());

            /*
            SequenceRepresentations = SequenceLibrary.SequencesDateTimeCombinations
                .Select(s => s.Representation.Split('|'))
                .SelectMany(s => s)
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrEmpty(s) && !string.IsNullOrWhiteSpace(s))
                .Select(s => string.Concat("\"",s,"\","))
                .ToList();
            */

            CreateVocabulary();

            CreatePatterns();

            ExtractSequences();

            CreateSequences();

        }

        public static VocabularyHandler GetInstance(ChronoxSettings settings, string directory, params string[] language)
        {
            if (instance == null)
            {
                instance = new VocabularyHandler(settings, directory, language);
            }
            return instance;
        }

        public void DestroyInstance() => instance = null;

        private Glossary Load(string directory, string[] languages)
        {
            var glossaries = new HashSet<Glossary>();

            foreach(var language in languages)
            {
                var fileName = new StringBuilder(language.FirstLetterToUpper(true));

                var langFileHandler = new LangDataHandler();

                var path = Path.Combine(directory, fileName.Append(".txt").ToString());

                if (File.Exists(path))
                {
                    glossaries.Add(langFileHandler.CreateGlossary(path));
                }
                else
                {
                    path = Path.Combine(Definitions.TextLangDataPath, fileName.ToString());

                    if (File.Exists(path))
                    {
                        glossaries.Add(langFileHandler.CreateGlossary(path));
                    }
                    else
                    {
                        path = Path.Combine(directory, fileName.Replace(".txt", string.Empty).Append(".json").ToString());

                        if (File.Exists(path))
                        {
                            glossaries.Add(LoadJsonFile(path));
                        }
                        else
                        {
                            path = Path.Combine(Definitions.JsonLangDataPath, fileName.ToString());

                            if (File.Exists(path))
                            {
                                glossaries.Add(LoadJsonFile(path));
                            }
                        }
                    }
                }

                if(glossaries.Count <= 0)
                {
                    fileName = new StringBuilder(Definitions.DefaultLanguage).Append(".txt");

                    path = Path.Combine(Definitions.TextLangDataPath, fileName.ToString());

                    glossaries.Add(langFileHandler.CreateGlossary(path));
                }
            }

            return MergeGlossaries(glossaries.ToArray());
        }

        private static Glossary LoadJsonFile(string path)
        {
            var textFile = File.OpenText(path);

            var reader = new JsonTextReader(textFile);

            var jsonObject = (JObject)JToken.ReadFrom(reader);

            return JsonConvert.DeserializeObject<Glossary>(jsonObject.ToString());
        }

        public void AddGlossary(Glossary glossary)
        {
            throw new NotImplementedException();
        }

        public void RemoveGlossary(Glossary glossary)
        {
            throw new NotImplementedException();
        }

        public Glossary MergeGlossaries(params Glossary[] glossaries)
        {
            if(glossaries.Length > 1)
            {
                var mergedGlossary = new Glossary();

                mergedGlossary.Language = string.Join("|", glossaries.Select(g => g.Language));

                mergedGlossary.DateTimeIgnored = new HashSet<string>(glossaries.SelectMany(g => g.DateTimeIgnored),StringComparer.OrdinalIgnoreCase).ToList();
                mergedGlossary.TimeRangeIgnored = new HashSet<string>(glossaries.SelectMany(g => g.TimeRangeIgnored), StringComparer.OrdinalIgnoreCase).ToList();
                mergedGlossary.TimeSpanIgnored = new HashSet<string>(glossaries.SelectMany(g => g.TimeSpanIgnored), StringComparer.OrdinalIgnoreCase).ToList();
                mergedGlossary.TimeSetIgnored = new HashSet<string>(glossaries.SelectMany(g => g.TimeSetIgnored), StringComparer.OrdinalIgnoreCase).ToList();

                mergedGlossary.SupportedDateTimeFormats = new HashSet<string>(glossaries.SelectMany(g => g.SupportedDateTimeFormats), StringComparer.OrdinalIgnoreCase).ToList();
                mergedGlossary.SupportedTimeRangeFormats = new HashSet<string>(glossaries.SelectMany(g => g.SupportedTimeRangeFormats), StringComparer.OrdinalIgnoreCase).ToList();
                mergedGlossary.SupportedTimeSpanFormats = new HashSet<string>(glossaries.SelectMany(g => g.SupportedTimeSpanFormats), StringComparer.OrdinalIgnoreCase).ToList();
                mergedGlossary.SupportedTimeSetFormats = new HashSet<string>(glossaries.SelectMany(g => g.SupportedTimeSetFormats), StringComparer.OrdinalIgnoreCase).ToList();

                mergedGlossary.Section = glossaries[0].Section;

                foreach (var section in mergedGlossary.Section)
                {
                    if (section.Label.CompareTo(Definitions.Property.Holidays) == 0)
                    {
                        section.Properties = new HashSet<Property>(glossaries
                            .SelectMany(g => g.Section)
                            .Where(g => g.Label.CompareTo(section.Label) == 0)
                            .SelectMany(g => g.Properties), Property.Comparer)
                            .ToList();
                    }
                    else
                    {
                        foreach (var property in section.Properties)
                        {
                            var properties = glossaries
                                   .SelectMany(g => g.Section)
                                   .Where(g => g.Label.CompareTo(section.Label) == 0)
                                   .SelectMany(g => g.Properties)
                                   .Where(g => g.Key.CompareTo(property.Key) == 0);

                            property.Pattern = string.Join("|", properties.Select(p => p.Pattern));

                            property.Variations = properties.SelectMany(g => g.Variations).ToList();
                        }
                    }
                }

                return mergedGlossary;
            }
            else
            {
                return glossaries[0];
            }
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
            PopulateGlossary(Definitions.Property.DayOfWeekType);
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
            CreatePattern(Definitions.Property.DayOfWeekType);

            PatternLibrary.Patterns.AddAll(PatternLibrary.CommonDatePatterns);

            PatternLibrary.Patterns.AddAll(PatternLibrary.CommonTimePatterns);

            PatternLibrary.Patterns.AddAll(PatternLibrary.CommonYearPatterns);
        }

        private void CreatePattern(string label)
        {
            var combined = PatternHandler.GeneratePattern(PatternOption.Standard, Vocabulary, label);

            PatternLibrary.Patterns.Add(label, new PatternRegex(label, combined));
        }

        private void ExtractSequences()
        {
            //WriteSequenceCodes(SequenceBank.DateTimeSequences.ToList(), "Sequences.txt");

            SequenceHandler.ExtractStandAlonePatterns();

            SequenceHandler.ExtractPatternSequences(Vocabulary, Vocabulary.SupportedDateTimeFormats, SequenceType.DateTime);

            SequenceHandler.ExtractPatternSequences(Vocabulary, Vocabulary.SupportedTimeRangeFormats, SequenceType.TimeRange);

            SequenceHandler.ExtractPatternSequences(Vocabulary, Vocabulary.SupportedTimeSpanFormats, SequenceType.TimeSpan);

            SequenceHandler.ExtractPatternSequences(Vocabulary, Vocabulary.SupportedTimeSetFormats, SequenceType.TimeSet);

        }

        private void CreateSequences()
        {
            AllRegexSequences = new List<PatternSequence>();

            DateTimeRegexSequences = SequenceHandler.BuildPatternSequences(Settings, SequenceLibrary.SequencesDateTimeCombinations, PatternLibrary.Patterns);
            DateTimeRegexSequences.ForEach(s => s.ComputeRelevance(DateTimeRegexSequences.OrderByDescending(p => s.PatternCount).FirstOrDefault().PatternCount));

            DurationRegexSequences = SequenceHandler.BuildPatternSequences(Settings, SequenceLibrary.SequencesDurationCombinations, PatternLibrary.Patterns);        
            DurationRegexSequences.ForEach(s => s.ComputeRelevance(DurationRegexSequences.OrderByDescending(p => s.PatternCount).FirstOrDefault().PatternCount));

            RepeaterRegexSequences = SequenceHandler.BuildPatternSequences(Settings, SequenceLibrary.SequencesRepeaterCombinations, PatternLibrary.Patterns);
            RepeaterRegexSequences.ForEach(s => s.ComputeRelevance(RepeaterRegexSequences.OrderByDescending(p => s.PatternCount).FirstOrDefault().PatternCount));

            RangedRegexSequences = SequenceHandler.BuildPatternSequences(Settings, SequenceLibrary.SequencesRangeCombinations, PatternLibrary.Patterns);
            RangedRegexSequences.ForEach(s => s.ComputeRelevance(RangedRegexSequences.OrderByDescending(p => s.PatternCount).FirstOrDefault().PatternCount));

            AllRegexSequences.AddRange(DateTimeRegexSequences);
            AllRegexSequences.AddRange(DurationRegexSequences);
            AllRegexSequences.AddRange(RepeaterRegexSequences);
            AllRegexSequences.AddRange(RangedRegexSequences);
        }

        internal void WriteSequenceCodes(List<Sequence> sequences, string filePath)
        {
            var abbreSequences = sequences.Select(s => s.AbbreviatedSequence).ToList();

            File.WriteAllLines(filePath, abbreSequences);
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
