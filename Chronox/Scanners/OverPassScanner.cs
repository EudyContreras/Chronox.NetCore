using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Chronox.Wrappers;
using Chronox.Constants;
using Chronox.Utilities.Extenssions;
using Chronox.Interfaces;
using Enumerations;

namespace Chronox.Scanners
{
    public class OverPassScanner : IChronoxScanner
    {
        public ScanWrapper Scan(ChronoxSettings settings, string input)
        {

            var scanResult = new ScanWrapper();

            var number = new StringBuilder();

            var wordsToMask = new List<string>();

            var expression = input.PadPunctuationExact(1, 1, '-');

            switch (settings.ParsingMode)
            {
                case ExtractionResultType.General:

                    CollectAll(wordsToMask, settings);

                    break;
                case ExtractionResultType.TimeSpan:

                    wordsToMask.AddRange(settings.Language.Vocabulary.TimeSpanIgnored);

                    break;
                case ExtractionResultType.DateTime:

                    wordsToMask.AddRange(settings.Language.Vocabulary.DateTimeIgnored);

                    break;
                case ExtractionResultType.TimeSet:

                    wordsToMask.AddRange(settings.Language.Vocabulary.TimeSetIgnored);

                    break;
                case ExtractionResultType.TimeRange:

                    wordsToMask.AddRange(settings.Language.Vocabulary.TimeRangeIgnored);

                    break;
                default:

                    CollectAll(wordsToMask, settings);

                    break;
            }

            var containsWrapper = expression.Contains(wordsToMask);

            return new ScanWrapper();
        }

        private string PreProcessExpression(ChronoxSettings settings, string expression)
        {    
            expression = expression.PadPunctuationExact(0, 1, ',');

            expression = expression.Replace("  ", " ", false);

            expression = expression.Pad(0, 1);

            return expression;
        }

        private void CollectAll(List<string> words, ChronoxSettings settings)
        {
            words.AddRange(settings.Language.Vocabulary.DateTimeIgnored);
            words.AddRange(settings.Language.Vocabulary.TimeRangeIgnored);
            words.AddRange(settings.Language.Vocabulary.TimeSpanIgnored);
            words.AddRange(settings.Language.Vocabulary.TimeSetIgnored);;
        }
    }
}
