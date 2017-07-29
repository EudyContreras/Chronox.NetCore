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
        public string ScannerTag()
        {
            return GetType().Name;
        }
        public ScanWrapper Scan(ChronoxSettings option, string expression)
        {
            var numbers = option.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericValue);

            var resultWrapper = expression.Contains(numbers.Keys, Definitions.Converters.NUMBERS.Keys);

            var result = new ScanWrapper(this);

            if(resultWrapper.Count > 0)
            {
                result.ResultWrappers = resultWrapper.Select(r => r.ToReplaceWrapper(ScannerTag())).ToList();

                result.ScannedExpression = expression;

                result.NormalizedExpression = expression;

                foreach (var wrapper in result.ResultWrappers)
                {
                    var value = Definitions.Converters.NUMBERS[numbers[wrapper.TextOriginal]].ToString();

                    wrapper.TextReplacement = value;

                    wrapper.ReplacementPosition.StartIndex = wrapper.OriginalPosition.StartIndex;

                    wrapper.ReplacementPosition.EndIndex = wrapper.ReplacementPosition.StartIndex + value.Length;

                    result.NormalizedExpression = result.NormalizedExpression.Replace(wrapper.TextOriginal, value, true);
                }
            }
            return result;
        }
    }
}
