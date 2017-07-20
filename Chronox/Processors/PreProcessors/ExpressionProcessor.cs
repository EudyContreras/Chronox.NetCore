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
            expression = expression.RemoveWords(settings.Language.Vocabulary.Ignored).Trim();

            expression = expression.PadPunctuationExact(0, 1).ToLower();

            expression = expression.Replace("  ", " ", false);

            expression = expression.Pad(0, 1);

            return expression;
        }
    }
}
