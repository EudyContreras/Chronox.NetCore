﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Chronox.Wrappers;
using Chronox.Utilities;
using Chronox.Constants;
using Enumerations;
using Chronox.Helpers.Interpreters;
using Chronox.Utilities.Extenssions;
using Chronox.Parsers.General.ParserHelpers;
using Chronox.Interfaces;

namespace Chronox.Parsers.English
{
    public class MasterParser
    {
        private IChronoxParser<ChronoxDateTimeExtraction> helperA = null;

        private IChronoxParser<ChronoxTimeRangeExtraction> helperB = null;

        private IChronoxParser<ChronoxTimeSpanExtraction> helperC = null;

        private IChronoxParser<ChronoxTimeSetExtraction> helperD = null;



        public List<IChronoxExtraction> ComputeResult(string text, DateTime referenceDate, ChronoxSettings settings, ChronoxBuildInformation information)
        {
            var results = new HashSet<IChronoxExtraction>();

            var sequences = new List<PatternSequence>();

            return SelectSequences(information.ProcessedString, referenceDate, settings, results, information, out sequences);
        }

        private List<IChronoxExtraction> SelectSequences(string text, DateTime referenceDate, ChronoxSettings settings, HashSet<IChronoxExtraction> results, ChronoxBuildInformation information, out List<PatternSequence> sequences)
        {
            switch (settings.ParsingMode)
            {
                case ExtractionResultType.General:

                    sequences = PreselectSequences(settings.Language.AllRegexSequences);

                    return Compute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.General, information, 1);

                case ExtractionResultType.TimeSpan:

                    sequences = PreselectSequences(settings.Language.DurationRegexSequences);

                    return Compute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.TimeSpan, information, 1);

                case ExtractionResultType.DateTime:

                    sequences = PreselectSequences(settings.Language.DateTimeRegexSequences);

                    return Compute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.DateTime, information, 1);

                case ExtractionResultType.TimeSet:

                    sequences = PreselectSequences(settings.Language.RepeaterRegexSequences);

                    return Compute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.TimeSet, information, 1);

                case ExtractionResultType.TimeRange:

                    sequences = PreselectSequences(settings.Language.RangedRegexSequences);

                    return Compute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.TimeRange, information, 1);

                default:

                    sequences = PreselectSequences(settings.Language.AllRegexSequences);

                    return Compute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.General, information, 1);
            }
        }

        private static IChronoxExtraction DetermineResultType(ExtractionResultType type, IChronoxExtraction result, MatchWrapper perfectMatch)
        {
            if (type == ExtractionResultType.General)
            {
                switch (perfectMatch.Sequence.SequenceType)
                {
                    case SequenceType.DateTime:
                        result = result.ResultType != ExtractionResultType.DateTime ? ChronoxDateTimeExtraction.EmptyExtraction : result;
                        break;
                    case SequenceType.TimeRange:
                        result = result.ResultType != ExtractionResultType.TimeRange ? ChronoxTimeRangeExtraction.EmptyExtraction : result;
                        break;
                    case SequenceType.TimeSpan:
                        result = result.ResultType != ExtractionResultType.TimeSpan ? ChronoxTimeSpanExtraction.EmptyExtraction : result;
                        break;
                    case SequenceType.TimeSet:
                        result = result.ResultType != ExtractionResultType.TimeSet ? ChronoxTimeSetExtraction.EmptyExtraction : result;
                        break;
                }
            }
            return result;
        }

        private List<IChronoxExtraction> Compute(string text, DateTime referenceDate, ChronoxSettings settings, IEnumerable<PatternSequence> sequences, HashSet<IChronoxExtraction> results, IChronoxExtraction last, ExtractionResultType type, ChronoxBuildInformation information, int passCount)
        {
            var parts = information.ProcessedString.Split(' ');

            var perfectMatch = GetPerfectMatch(settings, sequences, information, parts);

            var chronoxResult = new ChronoxResult();

            if (perfectMatch != null)
            {
                if (last != null)
                {
                    chronoxResult.Result = last;
                }
                else
                {
                    chronoxResult.Result = chronoxResult.Initialize(settings.ParsingMode);

                    if (type == ExtractionResultType.General)
                    {
                        chronoxResult.Result = DetermineResultType(type, chronoxResult.Result, perfectMatch);
                    }
                }

                information.LatestMatch = perfectMatch;

                var result = chronoxResult.Result;

                return HandleMatch(text, referenceDate, settings, sequences, results, type, information, passCount, perfectMatch, chronoxResult, ref result);

            }
            return AdjustResults(information.OriginalString, settings, information, results.ToList());
        }

        private MatchWrapper GetPerfectMatch(ChronoxSettings settings, IEnumerable<PatternSequence> sequences, ChronoxBuildInformation information, string[] parts)
        {
            var matchesFound = FindAllMatches(settings, sequences, information, parts);

            var perfectMatch = MatchWrapper.Null;

            if (matchesFound.Count > 0)
            {
                perfectMatch = OrganizeMatches(matchesFound);
            }

            return perfectMatch;
        }

        private List<IChronoxExtraction> HandleMatch(string text, DateTime referenceDate, ChronoxSettings settings, IEnumerable<PatternSequence> sequences, HashSet<IChronoxExtraction> results, ExtractionResultType type, ChronoxBuildInformation information, int passCount, MatchWrapper perfectMatch, ChronoxResult chronoxResult, ref IChronoxExtraction result)
        {
            if (passCount > 1)
            {
                if (!EmptySpace(text.Substring(0, perfectMatch.Match.Index)) || SingleExtraction(result.Extraction) )
                {
                    return Compute(text, information.Settings.ReferenceDate, settings, sequences, results, null, type, information, 1);
                }
            }

            ExtractChronoxInformation(information.ProcessedString, referenceDate, perfectMatch.Sequence, perfectMatch.Match, settings, information, ref result);

            var index = perfectMatch.Match.Index + perfectMatch.Match.Length;

            if(string.Compare(result.Extraction,perfectMatch.Match.Value) != 0)
            {
                result.Extraction = string.Join(" ", result.Extraction, perfectMatch.Match.Value);
            }

            chronoxResult.Result = result;

            information.ProcessedString = text.Substring(index < text.Length - 1 ? index : text.Length - 1).TrimStart();

            if (chronoxResult.Result != null && !string.IsNullOrEmpty(chronoxResult.Result.Extraction))
            {
                if (!results.Contains(chronoxResult.Result) || MissingTimeInformation((ChronoxDateTimeExtraction)chronoxResult.Result))
                {
                    results.Remove(chronoxResult.Result);
                    results.Add(chronoxResult.Result);

                    if (passCount < settings.SearchPassCount)
                    {
                        if (information.ProcessedString.Length >= settings.MinInputTextLength)
                        {
                            information.ClearFloaters();
                            information.ResetFlags();

                            var date = information.DateTime;

                            return Compute(information.ProcessedString, information.DateTime, settings, sequences, results, chronoxResult.Result, type, information, passCount + 1);
                        }
                    }
                }
                else
                {
                    return Compute(information.ProcessedString, information.Settings.ReferenceDate, settings, sequences, results, null, type, information, 1);
                }
            }
            return AdjustResults(information.OriginalString, settings, information, results.ToList());
        }


        private List<IChronoxExtraction> AdjustResults(string input, ChronoxSettings settings, ChronoxBuildInformation information, List<IChronoxExtraction> results)
        {
            var ignoredWords = GetIgnoredWords(settings);

            var scanResults = information.ScanWrappers.SelectMany(s => s.ResultWrappers).ToArray();

            foreach (var result in results)
            {
                var resultWrappers = result.Original.Contains(result.Extraction.Trim(), ignoredWords);

                if(resultWrappers.Count > 0)
                {
                    var wrapper = resultWrappers[0];

                    result.Extraction = result.Original.Substring(wrapper.StartIndex, wrapper.Length);

                    result.ProcessedString = result.Original.Remove(wrapper.StartIndex,wrapper.Length);

                    result.StartIndex = wrapper.StartIndex;

                    result.EndIndex = wrapper.EndIndex;

                }
                else
                {
                    foreach (var scan in scanResults)
                    {
                        if (result.Extraction.Contains(scan.TextReplacement))
                        {
                            var wrappers = result.Original.Contains(result.Extraction.Replace(scan.TextReplacement, scan.TextOriginal, true).Trim(), ignoredWords);

                            if (wrappers.Count > 0)
                            {
                                var wrapper = wrappers[0];

                                result.Extraction = result.Original.Substring(wrapper.StartIndex, wrapper.Length);

                                result.ProcessedString = result.Original.Remove(wrapper.StartIndex, wrapper.Length);

                                result.StartIndex = wrapper.StartIndex;

                                result.EndIndex = wrapper.EndIndex;
                            }
                        }
                    }
                }
            }
            return results;
        }

        private List<string> GetIgnoredWords(ChronoxSettings settings)
        {
            var ignoredWords = new List<string>();

            switch (settings.ParsingMode)
            {
                case ExtractionResultType.General:

                    CollectAll(ignoredWords, settings);

                    break;
                case ExtractionResultType.TimeSpan:

                    ignoredWords = settings.Language.Vocabulary.TimeSpanIgnored;

                    break;
                case ExtractionResultType.DateTime:

                    ignoredWords = settings.Language.Vocabulary.DateTimeIgnored;

                    break;
                case ExtractionResultType.TimeSet:

                    ignoredWords = settings.Language.Vocabulary.TimeSetIgnored;

                    break;
                case ExtractionResultType.TimeRange:

                    ignoredWords = settings.Language.Vocabulary.TimeRangeIgnored;

                    break;
                default:

                    CollectAll(ignoredWords, settings);

                    break;
            }

            return ignoredWords;
        }


        private void CollectAll(List<string> words, ChronoxSettings settings)
        {
            words.AddRange(settings.Language.Vocabulary.DateTimeIgnored);
            words.AddRange(settings.Language.Vocabulary.TimeRangeIgnored);
            words.AddRange(settings.Language.Vocabulary.TimeSpanIgnored);
            words.AddRange(settings.Language.Vocabulary.TimeSetIgnored); ;
        }

        private bool EmptySpace(string text)
        {
            return string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text);
        }

        private bool SingleExtraction(string text)
        {
            return !text.Contains(StringExtenssions.Spaces(0));
        }

        private bool MissingTimeInformation(IChronoxExtraction result)
        {
            if(result.ResultType  == ExtractionResultType.DateTime)
            {
                var date = (ChronoxDateTimeExtraction)result;

                var missing = !date.Builder.IsValueCertain(DateTimeUnit.Hour)
                || date.Builder.TimeZone == null
                || date.Builder.TimeOffset == null;

                return missing;
            }

            return false;
        }

        private string timeZone;

        //Find a way to minimize amount of checks and reduce loop count!

        private List<MatchWrapper> FindAllMatches(ChronoxSettings settings, IEnumerable<PatternSequence> sequences, ChronoxBuildInformation information, string[] parts)
        {
            var matchesFound = new List<MatchWrapper>();

            if(timeZone == null){
                timeZone = Definitions.Converters.ABBREVIATIONS[Definitions.Patterns.TimeZone];
            }
         
            foreach (var sequence in sequences)
            {
              
                if (sequence.PatternCount > parts.Length) 
                    continue;

                var match = sequence.RegexMatcher.Match(information.ProcessedString);


                if (match.Success && !string.IsNullOrEmpty(match.Value))
                {
                    if(sequence.AbbreviatedSequence == timeZone)
                    {
                        if(match.Value.All(s => char.IsUpper(s)))
                        {
                            matchesFound.Add(new MatchWrapper(sequence, match));
                        }
                    }

                    else
                    {
                        matchesFound.Add(new MatchWrapper(sequence, match));             
                    }
                }
            }
            return matchesFound;
        }

        private static MatchWrapper OrganizeMatches(List<MatchWrapper> matchesFound)
        {
            var perfectMatch = matchesFound
                                .GroupBy(m => m.Match.Index)
                                .OrderBy(g => g.Key)
                                .FirstOrDefault()
                                .OrderByDescending(m => m.Match.Length)
                                .FirstOrDefault();

            if (!DiscreteDate(perfectMatch)) return perfectMatch;

            if (perfectMatch.Sequence.Patterns.Any(p => string.Compare(p.Label, Definitions.Patterns.DateBigEndian) == 0))
            {
                perfectMatch = matchesFound
                    .Where(p => p.Sequence.Patterns
                    .Any(s => string.Compare(s.Label, Definitions.Patterns.DateBigEndian) == 0))
                    .OrderByDescending(p => p.Sequence.PatternCount)
                    .FirstOrDefault();
            }
            else if (perfectMatch.Sequence.Patterns.Any(p => string.Compare(p.Label, Definitions.Patterns.DateLittleEndian) == 0))
            {
                perfectMatch = matchesFound
                    .Where(p => p.Sequence.Patterns
                    .Any(s => string.Compare(s.Label, Definitions.Patterns.DateLittleEndian) == 0))
                    .OrderByDescending(p => p.Sequence.PatternCount)
                    .FirstOrDefault();
            }
            else if (perfectMatch.Sequence.Patterns.Any(p => string.Compare(p.Label, Definitions.Patterns.DateMiddleEndian) == 0))
            {
                perfectMatch = matchesFound
                    .Where(p => p.Sequence.Patterns
                    .Any(s => string.Compare(s.Label, Definitions.Patterns.DateLittleEndian) == 0))
                    .OrderByDescending(p => p.Sequence.PatternCount)
                    .FirstOrDefault();
            }

            return perfectMatch;
        }

        private static bool DiscreteDate(MatchWrapper perfectMatch)
        {
            return perfectMatch.Sequence.Patterns.Any(p => 
                string.Compare(p.Label, Definitions.Patterns.DateBigEndian) == 0 ||
                string.Compare(p.Label, Definitions.Patterns.DateMiddleEndian) == 0 ||
                string.Compare(p.Label, Definitions.Patterns.DateLittleEndian) == 0);
        }

        private List<PatternSequence> PreselectSequences(List<PatternSequence> sequences)
        {
            var regexes = sequences.OrderByDescending(s => s.Relevance).ToList();

            return regexes;
        }

        private void ExtractChronoxInformation(string text, DateTime refDate, PatternSequence sequence, Match match, ChronoxSettings settings, ChronoxBuildInformation information, ref IChronoxExtraction result)
        {
            var dateTime = ChronoxDateTimeUtility.CreateDateTime(refDate);

            var sequenceControll = false;

            if (result == null)
            {
                switch (settings.ParsingMode)
                {
                    case ExtractionResultType.TimeSpan:
                        result = new ChronoxTimeSpanExtraction(settings, match.Index, match.Value, information.OriginalString);
                        break;
                    case ExtractionResultType.DateTime:
                        result = new ChronoxDateTimeExtraction(settings, dateTime, match.Index, match.Value, information.OriginalString);
                        break;
                    case ExtractionResultType.TimeSet:
                        break;
                    case ExtractionResultType.TimeRange:
                        break;
                    case ExtractionResultType.General:

                        sequenceControll = true;

                        break;
                }
            }

            var parseResult = new ChronoxResult(InitializeFromSequenceType(text, sequence, match, settings, result, dateTime, sequenceControll));

            HandleResultType(match, settings, information, parseResult, ref dateTime);

            result = parseResult.Result;
        }

        private IChronoxExtraction InitializeFromSequenceType(string text, PatternSequence sequence, Match match, ChronoxSettings settings, IChronoxExtraction result, DateTime dateTime, bool sequenceControll)
        {
            switch (sequence.SequenceType)
            {
                case SequenceType.DateTime:
                    helperA = helperA ?? new DateTimeHelper(this, settings);
                    result = (sequenceControll && result == null) ? new ChronoxDateTimeExtraction(settings, dateTime, match.Index, match.Value, text) : result;
                    break;
                case SequenceType.TimeRange:
                    helperB = helperB ?? new TimeRangeHelper();
                    break;
                case SequenceType.TimeSpan:
                    helperC = helperC ?? new TimeSpanHelper();
                    result = (sequenceControll && result == null) ? new ChronoxTimeSpanExtraction(settings, match.Index, match.Value, text) : result;
                    break;
                case SequenceType.TimeSet:
                    helperD = helperD ?? new TimeSetHelper();
                    break;
            }

            return result;
        }

        private void HandleResultType(Match match, ChronoxSettings settings, ChronoxBuildInformation information, ChronoxResult result, ref DateTime dateTime)
        {
            var groups = ProcessGroups(match, ref dateTime, information, settings);

            switch (result.ResultType)
            {
                case ExtractionResultType.DateTime:
                    result.DateTimeExtraction = HandleDateTimeExtraction(match, settings, information, result.DateTimeExtraction, groups, ref dateTime);
                    break;
                case ExtractionResultType.TimeRange:
                    result.TimeRangeExtraction = HandleTimeRangeExtraction(match, settings, information, result.TimeRangeExtraction, groups, ref dateTime);
                    break;
                case ExtractionResultType.TimeSpan:
                    result.TimeSpanExtraction = HandleTimeSpanExtraction(match, settings, information, result.TimeSpanExtraction, groups, ref dateTime);
                    break;
                case ExtractionResultType.TimeSet:
                    result.TimeSetExtraction = HandleTimeSetExtraction(match, settings, information, result.TimeSetExtraction, groups, ref dateTime);
                    break;
            }
        }

        private ChronoxDateTimeExtraction HandleDateTimeExtraction(Match match, ChronoxSettings settings, ChronoxBuildInformation information, ChronoxDateTimeExtraction result, List<GroupWrapper> groups, ref DateTime dateTime)
        {
            var now = settings.ReferenceDate;

            result = PerformInstructions(groups, ref dateTime, result, information, settings, helperA);

            result.Builder.NormalizeDateValues(now, dateTime, information.Settings);
            result.Builder.NormalizeTimeValues(now, dateTime, information.Settings);

            information.DateTime = dateTime;

            return result;
        }

        private ChronoxTimeRangeExtraction HandleTimeRangeExtraction(Match match, ChronoxSettings settings, ChronoxBuildInformation information, ChronoxTimeRangeExtraction result, List<GroupWrapper> groups, ref DateTime dateTime)
        {
            result = PerformInstructions(groups, ref dateTime, result, information, settings, helperB);

            return null;
        }

        private ChronoxTimeSpanExtraction HandleTimeSpanExtraction(Match match, ChronoxSettings settings, ChronoxBuildInformation information, ChronoxTimeSpanExtraction result, List<GroupWrapper> groups, ref DateTime dateTime)
        {
            result = PerformInstructions(groups, ref dateTime, result, information, settings, helperC);

            return result;
        }

        private ChronoxTimeSetExtraction HandleTimeSetExtraction(Match match, ChronoxSettings settings, ChronoxBuildInformation information, ChronoxTimeSetExtraction result, List<GroupWrapper> groups, ref DateTime dateTime)
        {
            result = PerformInstructions(groups, ref dateTime, result, information, settings, helperD);

            return null;
        }

        private List<GroupWrapper> ProcessGroups(Match match, ref DateTime dateTime, ChronoxBuildInformation information, ChronoxSettings settings)
        {
            var matches = new HashSet<GroupWrapper>();

            foreach (var property in Definitions.General.CombinedProperties)
            {
                var group = match.Groups[property];

                foreach (object capture in group.Captures)
                {
                    if (capture is Group)
                    {
                        matches.Add(new GroupWrapper((Group)capture));
                    }
                    else if (capture is Capture)
                    {
                        var captureWrapper = new GroupWrapper((Capture)capture)
                        {
                            Name = group.Name,
                            Group = group
                        };
                        matches.Add(captureWrapper);
                    }
                }
            }

            var filterd = matches.Where(m => !string.IsNullOrEmpty(m.Value) && !string.IsNullOrWhiteSpace(m.Value));

            return filterd.OrderBy(g => g.Index).ThenByDescending(g => g.Value.Length).ToList();
        }

        private TResult PerformInstructions<TResult>(List<GroupWrapper> foundGroups, ref DateTime dateTime, TResult result, ChronoxBuildInformation information, ChronoxSettings settings, IChronoxParser<TResult> helper)
        {
            var date = dateTime;

            foreach (var group in foundGroups)
            {
                if (group.GroupUsed)
                {
                    continue;
                }

                information.CurrentGroup = group;

                if (group.Name == Definitions.Property.CasualExpressions)
                {
                    var casualExpression = TranslationHandler.CasualExpression(settings, group.Value.Trim());

                    helper.ProcessCasualExpression(result, foundGroups, ref dateTime, information, casualExpression);
                }
                else if (group.Name == Definitions.Property.GrabberExpressions)
                {
                    var grabberExpression = TranslationHandler.GrabberExpression(settings, group.Value.Trim());

                    helper.ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabberExpression);
                }
                else if (group.Name == Definitions.Property.InterpretedExpression)
                {
                    var interpretedExpression = TranslationHandler.InterpretedExpression(settings, group.Value.Trim());

                    helper.ProcessInterpretedExpression(result, foundGroups, ref dateTime, information, interpretedExpression);
                }
                else if (group.Name == Definitions.Property.RepeaterIndicators)
                {
                    var repeaterIndicator = TranslationHandler.RepeaterIndicator(settings, group.Value.Trim());

                    helper.ProcessRepeaterIndicator(result, foundGroups, ref dateTime, information, repeaterIndicator);
                }
                else if (group.Name == Definitions.Property.RepeaterExpressions)
                {
                    var repeaterExpression = TranslationHandler.RepeaterExpression(settings, group.Value.Trim());

                    helper.ProcessRepeaterExpression(result, foundGroups, ref dateTime, information, repeaterExpression);
                }
                else if (group.Name == Definitions.Property.DurationIndicators)
                {
                    var durationIndicator = TranslationHandler.DurationIndicator(settings, group.Value.Trim());

                    helper.ProcessDurationIndicator(result, foundGroups, ref dateTime, information, durationIndicator);
                }
                else if (group.Name == Definitions.Property.DurationExpressions)
                {
                    var durationExpression = TranslationHandler.DurationExpression(settings, group.Value.Trim());

                    helper.ProcessDurationExpression(result, foundGroups, ref dateTime, information, durationExpression);
                }
                else if (group.Name == Definitions.Property.Proximity)
                {
                    var proximityType = TranslationHandler.ProximityType(settings, group.Value.Trim());

                    helper.ProcessProximityType(result, foundGroups, ref dateTime, information, proximityType);
                }
                else if (group.Name == Definitions.Patterns.YearDiscrete)
                {
                    helper.ProcessYear(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.MonthDiscrete)
                {
                    helper.ProcessMonth(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.DayDiscrete)
                {
                    helper.ProcessDay(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.DateBigEndian)
                {
                    helper.ProcessDateBigEndian(result, foundGroups, ref dateTime, information, group);
                }
                else if (group.Name == Definitions.Patterns.DateMiddleEndian)
                {
                    helper.ProcessDateMiddleEndian(result, foundGroups, ref dateTime, information, group);
                }
                else if (group.Name == Definitions.Patterns.DateLittleEndian)
                {
                    helper.ProcessDateLittleEndian(result, foundGroups, ref dateTime, information, group);
                }
                else if (group.Name == Definitions.Patterns.TimeZone)
                {
                    helper.ProcessTimeZoneInformation(result, foundGroups, ref dateTime, information, group);
                }
                else if (group.Name == Definitions.Property.DateTimeUnits || IsDateUnit(group.Name) || IsTimeUnit(group.Name))
                {
                    var timeUnit = TranslationHandler.DateTimeUnit(settings, group.Value.Trim());

                    helper.ProcessDateTimeUnit(result, foundGroups, ref dateTime, information, timeUnit);
                }
                else if (group.Name == Definitions.Property.RangeIndicator || group.Name == Definitions.Property.RangeSeparator)
                {
                    var rangePointer = TranslationHandler.RangePointer(settings, group.Value.Trim());

                    helper.ProcessRangePointer(result, foundGroups, ref dateTime, information, rangePointer);
                }
                else if (group.Name == Definitions.Property.SeasonOfYear)
                {
                    var seasonOfYear = TranslationHandler.DayOffset(settings, group.Value.Trim());

                    helper.ProcessSeason(result, foundGroups, ref dateTime, information, seasonOfYear);
                }
                else if (group.Name == Definitions.Property.DayOfWeekType)
                {
                    var dayOfWeekType = TranslationHandler.DayOfWeekType(settings, group.Value.Trim());

                    helper.ProcessDayOfWeekType(result, foundGroups, ref dateTime, information, dayOfWeekType);
                }
                else if (group.Name == Definitions.Property.DaysOfWeek)
                {
                    var dayOfWeek = TranslationHandler.DayOfWeek(settings, group.Value.Trim());

                    helper.ProcessDayOfWeek(result, foundGroups, ref dateTime, information, dayOfWeek);
                }
                else if (group.Name == Definitions.Property.MonthsOfYear)
                {
                    var monthOfYear = TranslationHandler.Month(settings, group.Value.Trim());

                    helper.ProcessMonthOfYear(result, foundGroups, ref dateTime, information, monthOfYear);
                }
                else if (group.Name == Definitions.Property.DayOffset)
                {
                    var dayOffset = TranslationHandler.DayOffset(settings, group.Value.Trim());

                    helper.ProcessDayOffset(result, foundGroups, ref dateTime, information, dayOffset);
                }
                else if (group.Name == Definitions.Property.TimeOfDay)
                {
                    var timeOfDay = TranslationHandler.TimeOfDay(settings, group.Value.Trim());

                    helper.ProcessTimeOfDay(result, foundGroups, ref dateTime, information, timeOfDay);
                }
                else if (group.Name == Definitions.Property.TimeMeridiam)
                {
                    var timeMeridiam = TranslationHandler.TimeMeridiam(settings, group.Value.Trim());

                    helper.ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, timeMeridiam);
                }
                else if (group.Name == Definitions.Property.Number)
                {
                    var value = group.Value.RemoveSubstrings(settings.Language.Vocabulary.OrdinalSuffixes);

                    if(int.TryParse(value.Trim(), out int number))
                    {
                        helper.ProcessNumericWord(result, foundGroups, ref dateTime, information, number);
                    }
                    else
                    {
                        var numericValue = TranslationHandler.NumericWordOrdinal(settings, group.Value.Trim());

                        helper.ProcessNumericWordOrdinal(result, foundGroups, ref dateTime, information, numericValue);
                    }
                    
                }
                else if (group.Name == Definitions.Property.Ordinal)
                {
                    var value = group.Value.RemoveSubstrings(settings.Language.Vocabulary.OrdinalSuffixes);

                    if (int.TryParse(value.Trim(), out int number))
                    {
                        helper.ProcessNumericWordOrdinal(result, foundGroups, ref dateTime, information, number);
                    }
                    else
                    {
                        var numericValue = TranslationHandler.NumericWordOrdinal(settings, group.Value.Trim());

                        helper.ProcessNumericWordOrdinal(result, foundGroups, ref dateTime, information, numericValue);
                    }
                }
                else if (group.Name == Definitions.Patterns.DecimalNumber)
                {
                    helper.ProcessDecimalNumber(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Property.TimeExpressions)
                {
                    var timeExpression = TranslationHandler.TimeExpression(settings, group.Value.Trim());

                    helper.ProcessTimeExpression(result, foundGroups, ref dateTime, information, timeExpression);
                }
                else if (group.Name == Definitions.Property.TimeFractions)
                {
                    var timeFraction = TranslationHandler.Fraction(settings, group.Value.Trim());

                    helper.ProcessTimeFraction(result, foundGroups, ref dateTime, information, timeFraction);
                }
                else if (group.Name == Definitions.Property.TimeConjointer)
                {
                    var timeConjointer = TranslationHandler.TimeConjointer(settings, group.Value.Trim());

                    helper.ProcessTimeConjointer(result, foundGroups, ref dateTime, information, timeConjointer);
                }
                else if (group.Name == Definitions.Patterns.Year)
                {
                    helper.ProcessYear(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.Hour)
                {
                    helper.ProcessHours(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.Minute)
                {
                    helper.ProcessMinutes(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.Second)
                {
                    helper.ProcessSeconds(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.HourDiscrete)
                {
                    helper.ProcessDiscreteHours(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.MinuteDiscrete)
                {
                    helper.ProcessDiscreteMinutes(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.SecondDiscrete)
                {
                    helper.ProcessDiscreteSeconds(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.NumberMax2Digits)
                {
                    helper.ProcessMax2DigitNumber(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.NumberMax4Digits)
                {
                    helper.ProcessMax4DigitNumber(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.NumberMax5Digits)
                {
                    helper.ProcessMax5DigitNumber(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }

                group.GroupUsed = true;
            }

            helper.ProcessRemaining(result, foundGroups, ref dateTime, information);

            return result;
        }

        public bool IsDateUnit(string label)
        {
            return label == Definitions.Property.DateUnits
                || label == Definitions.Property.YearUnit
                || label == Definitions.Property.MonthUnit
                || label == Definitions.Property.WeekUnit
                || label == Definitions.Property.DayUnit;

        }

        public bool IsTimeUnit(string label)
        {
            return label == Definitions.Property.TimeUnits
                || label == Definitions.Property.HourUnit
                || label == Definitions.Property.MinuteUnit
                || label == Definitions.Property.SecondUnit;
        }

        public bool IsDateUnit(DateTimeUnit unit)
        {
            return unit == DateTimeUnit.Year
                || unit == DateTimeUnit.Month
                || unit == DateTimeUnit.Week
                || unit == DateTimeUnit.Day;
        }

        public bool IsTimeUnit(DateTimeUnit unit)
        {
            return unit == DateTimeUnit.Hour
                || unit == DateTimeUnit.Minute
                || unit == DateTimeUnit.Second;
        }
    }
}