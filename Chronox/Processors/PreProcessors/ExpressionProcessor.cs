using System;
using System.Collections.Generic;
using Chronox.Utilities.Extenssions;
using System.Text;
using Chronox.Abstractions;

namespace Chronox.Processors.PreProcessors
{
    public class ExpressionProcessor : AbstractPreProcessor
    {
        protected override string ProcessExpression(ChronoxSettings settings, string expression)
        {
            switch (settings.Preferences.ParsingMode)
            {
                case Enumerations.ExtractionResultType.General:

                    expression = CleanExpression(expression, settings);

                    break;
                case Enumerations.ExtractionResultType.TimeSpan:

                    expression = expression.RemoveWords(settings.Language.Vocabulary.TimeSpanIgnored);

                    break;
                case Enumerations.ExtractionResultType.DateTime:

                    expression = expression.RemoveWords(settings.Language.Vocabulary.DateTimeIgnored);

                    break;
                case Enumerations.ExtractionResultType.TimeSet:

                    expression = expression.RemoveWords(settings.Language.Vocabulary.TimeSetIgnored);

                    break;
                case Enumerations.ExtractionResultType.TimeRange:

                    expression = expression.RemoveWords(settings.Language.Vocabulary.TimeRangeIgnored);

                    break;
                default:

                    expression = CleanExpression(expression, settings);

                    break;
            }

            expression = expression.PadPunctuationExact(0, 1).ToLower();

            expression = expression.Replace("  ", " ", false);

            expression = expression.Pad(0, 1);

            return expression;
        }

        private string CleanExpression(string expression, ChronoxSettings settings)
        {
            expression = expression.RemoveWords(settings.Language.Vocabulary.DateTimeIgnored);

            expression = expression.RemoveWords(settings.Language.Vocabulary.TimeRangeIgnored);

            expression = expression.RemoveWords(settings.Language.Vocabulary.TimeSpanIgnored);

            expression = expression.RemoveWords(settings.Language.Vocabulary.TimeSetIgnored);

            return expression;
        }
    }
}
