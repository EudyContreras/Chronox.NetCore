
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

        protected abstract void Extract(String text, DateTime refDate, RegexSequence sequence, Match matcher, ChronoxSettings option, ChronoxBuildInformation information, ref ChronoxDateTimeExtraction result);

        public List<ChronoxDateTimeExtraction> Execute(string text, DateTime referenceDate, ChronoxSettings settings)
        {
            var results = new HashSet<ChronoxDateTimeExtraction>();

            var information = new ChronoxBuildInformation(text, settings);

            var sequences = new List<RegexSequence>();

            switch (settings.Preferences.ParsingMode)
            {
                case ExtractionResultType.General:

                    sequences = PreselectSequences(settings.Language.AllRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, information, 1);

                case ExtractionResultType.Duration:

                    sequences = PreselectSequences(settings.Language.DurationRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, information, 1);

                case ExtractionResultType.DateTime:

                    sequences = PreselectSequences(settings.Language.DateTimeRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, information, 1);

                case ExtractionResultType.Repeater:

                    sequences = PreselectSequences(settings.Language.RepeaterRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, information, 1);

                case ExtractionResultType.DateTimeRange:

                    sequences = PreselectSequences(settings.Language.RangedRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, information, 1);

                default:

                    sequences = PreselectSequences(settings.Language.AllRegexSequences);

                    return Execute(text, referenceDate, settings, sequences, results, null, information, 1);
            }
        }
           
        private List<ChronoxDateTimeExtraction> Execute(string text, DateTime referenceDate, ChronoxSettings settings, IEnumerable<RegexSequence> sequences,  HashSet<ChronoxDateTimeExtraction> results, ChronoxDateTimeExtraction last, ChronoxBuildInformation information, int passCount)
        {
            var matchesFound = new List<MatchWrapper>();

            var result = last == null ? ChronoxDateTimeExtraction.EmptyExtraction : last;

            var parts = information.ProcessedString.Split(' ');

            foreach (var sequence in sequences)
            {
                if (sequence.PatternCount > parts.Length + 1) continue;

                var regex = new Regex(sequence.NormalizedPattern(settings.Language), RegexOptions.IgnoreCase | RegexOptions.Compiled);

                var match = regex.Match(information.ProcessedString);

                if(match.Success && !string.IsNullOrEmpty(match.Value))
                {
                    matchesFound.Add(new MatchWrapper(sequence,match));
                }              
            }

            var perfectMatch = MatchWrapper.Null;

            if(matchesFound.Count > 0)
            {
                perfectMatch = OrganizeMatches(matchesFound);
            }

            if (perfectMatch != null)
            {
                information.LatestMatch = perfectMatch;

                Extract(information.ProcessedString, referenceDate, perfectMatch.Sequence, perfectMatch.Match, settings, information, ref result);

                var index = perfectMatch.Match.Index + perfectMatch.Match.Length;

                information.ProcessedString = text.Substring(index < text.Length - 1 ? index : text.Length - 1);

                if (result != null && !string.IsNullOrEmpty(result.Extraction))
                {
                    if (!results.Contains(result))
                    {
                        results.Add(result);

                        if(passCount < settings.SearchPassCount)
                        {
                            if(information.ProcessedString.Length >= settings.MinInputTextLength)
                            {
                                information.ClearFloaters();
                                information.ResetFlags();

                                var date = information.DateTime;

                                return Execute(information.ProcessedString, information.DateTime, settings, sequences, results, result, information, passCount + 1);
                            }
                        }
                    }
                }
            }

            return results.ToList();
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
        private List<RegexSequence> PreselectSequences(List<RegexSequence> sequences)
        {
            var regexes = sequences.OrderByDescending(s => s.Relevance).ToList();

            return regexes;
        }
    }
}
