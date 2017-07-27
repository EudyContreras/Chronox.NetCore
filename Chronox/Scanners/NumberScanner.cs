using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Chronox.Wrappers;
using Chronox.Constants;
using Chronox.Utilities.Extenssions;
using Chronox.Interfaces;

namespace Chronox.Scanners
{
    public class NumberScanner : IChronoxScanner
    {
        public ScanWrapper Scan(ChronoxSettings option, string expression)
        {
            var numbers = option.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericValue);

            var resultWrapper = expression.Contains(numbers.Keys.ToArray());

            var result = new ScanWrapper();

            if(resultWrapper.Count > 0)
            {
                result.ResultWrappers = resultWrapper;

                result.ScannedExpression = expression;

                result.NormalizedExpression = expression;

                foreach (var wrapper in resultWrapper)
                {
                    if (wrapper.Found)
                    {
                        var value = Definitions.Converters.NUMBERS[numbers[wrapper.Text]];

                        result.NormalizedExpression = result.NormalizedExpression.Replace(wrapper.Text, value.ToString(), true);
                    }
                }
            }
            return result;
        }
    }
}
