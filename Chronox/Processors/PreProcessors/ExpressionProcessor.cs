using System;
using System.Collections.Generic;
using Chronox.Utilities.Extenssions;
using System.Text;
using Chronox.Abstractions;

namespace Chronox.Processors.PreProcessors
{
    internal class ExpressionProcessor : AbstractPreProcessor
    {
        protected override string ProcessExpression(ChronoxOption options, string expression)
        {
            expression = expression.RemoveWords(options.Language.Vocabulary.Ignored).Trim();

            expression = expression.PadPunctuationExact(0, 1).ToLower();

            expression = expression.Replace("  ", " ", false);

            expression = expression.Pad(0, 1);

            return expression;
        }
    }
}
