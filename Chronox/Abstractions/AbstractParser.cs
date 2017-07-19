
using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chronox.Handlers;
using Chronox.Constants;
using Chronox.Interfaces;
using Enumerations;

namespace Chronox.Abstractions
{
    internal abstract class AbstractParser : IChronoxParser
    {

        protected abstract void Extract(String text, DateTime refDate, PatternSequence sequence, Match matcher, ChronoxSettings option, ChronoxBuildInformation information, ref IChronoxExtraction result);

        public List<IChronoxExtraction> Execute(string text, DateTime referenceDate, ChronoxSettings settings)
        {
            var results = new HashSet<IChronoxExtraction>();

            var information = new ChronoxBuildInformation(text, settings);

            var sequences = new List<PatternSequence>();

            return SelectSequences(text, referenceDate, settings, results, information, out sequences);
        }

        private List<IChronoxExtraction> SelectSequences(string text, DateTime referenceDate, ChronoxSettings settings, HashSet<IChronoxExtraction> results, ChronoxBuildInformation information, out List<PatternSequence> sequences)
        {
            switch (settings.Preferences.ParsingMode)
            {
                case ExtractionResultType.General:

                    sequences = PreselectSequences(settings.Language.AllRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.General, information, 1);

                case ExtractionResultType.TimeSpan:

                    sequences = PreselectSequences(settings.Language.DurationRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.TimeSpan, information, 1);

                case ExtractionResultType.DateTime:

                    sequences = PreselectSequences(settings.Language.DateTimeRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.DateTime, information, 1);

                case ExtractionResultType.TimeSet:

                    sequences = PreselectSequences(settings.Language.RepeaterRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.TimeSet, information, 1);

                case ExtractionResultType.TimeRange:

                    sequences = PreselectSequences(settings.Language.RangedRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.TimeRange, information, 1);

                default:

                    sequences = PreselectSequences(settings.Language.AllRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, ExtractionResultType.General, information, 1);
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

        private List<IChronoxExtraction> Execute(string text, DateTime referenceDate, ChronoxSettings settings, IEnumerable<PatternSequence> sequences,  HashSet<IChronoxExtraction> results, IChronoxExtraction last, ExtractionResultType type, ChronoxBuildInformation information, int passCount)
        {        
            var parts = information.ProcessedString.Split(' ');

            var matchesFound = FindAllMatches(settings, sequences, information, parts);

            var perfectMatch = MatchWrapper.Null;

            if (matchesFound.Count > 0)
            {
                perfectMatch = OrganizeMatches(matchesFound);
            }

            if (perfectMatch != null)
            {
                var chronoxResult = new ChronoxParsedResult();

                if(last != null)
                {
                    chronoxResult.Result = last;
                }
                else
                {
                    chronoxResult.Result = chronoxResult.Initialize(settings.Preferences.ParsingMode);

                    if(type == ExtractionResultType.General)
                    {
                        chronoxResult.Result = DetermineResultType(type, chronoxResult.Result, perfectMatch);
                    }
                }

                information.LatestMatch = perfectMatch;

                var result = chronoxResult.Result;

                Extract(information.ProcessedString, referenceDate, perfectMatch.Sequence, perfectMatch.Match, settings, information, ref result);

                chronoxResult.Result = result;

                var index = perfectMatch.Match.Index + perfectMatch.Match.Length;

                information.ProcessedString = text.Substring(index < text.Length - 1 ? index : text.Length - 1);

                if (chronoxResult.Result != null && !string.IsNullOrEmpty(chronoxResult.Result.Extraction))
                {
                    if (!results.Contains(chronoxResult.Result))
                    {
                        results.Add(chronoxResult.Result);

                        if (passCount < settings.SearchPassCount)
                        {
                            if (information.ProcessedString.Length >= settings.MinInputTextLength)
                            {
                                information.ClearFloaters();
                                information.ResetFlags();

                                var date = information.DateTime;

                                return Execute(information.ProcessedString, information.DateTime, settings, sequences, results, chronoxResult.Result, type, information, passCount + 1);
                            }
                        }
                    }
                }
            }
            return results.ToList();
        }

        private List<MatchWrapper> FindAllMatches(ChronoxSettings settings, IEnumerable<PatternSequence> sequences, ChronoxBuildInformation information,  string[] parts)
        {
            var matchesFound = new List<MatchWrapper>();

            foreach (var sequence in sequences)
            {
                if (sequence.PatternCount > parts.Length + 1) continue;

                var regex = new Regex(sequence.NormalizedPattern(settings.Language), RegexOptions.IgnoreCase | RegexOptions.Compiled);

                var match = regex.Match(information.ProcessedString);

                if (match.Success && !string.IsNullOrEmpty(match.Value))
                {
                    matchesFound.Add(new MatchWrapper(sequence, match));
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

            return perfectMatch;
        }

        //Create algorithm for preselecting and ordering the sequences that are most likely to match base on a score or something
        private List<PatternSequence> PreselectSequences(List<PatternSequence> sequences)
        {
            var regexes = sequences.OrderByDescending(s => s.Relevance).ToList();

            return regexes;
        }
    }
}
