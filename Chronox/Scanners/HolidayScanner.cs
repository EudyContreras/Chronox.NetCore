using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Chronox.Utilities.Extenssions;
using Chronox.Wrappers;
using Chronox.Constants;
using Chronox.Interfaces;

namespace Chronox.Scanners
{
    internal class HolidayScanner : IScanner
    {
        public ScanResult Scan(ChronoxOption option, string expression)
        {
            var holidays = option.Language.VocabularyBank.GetDictionary(Definitions.Property.Holidays);

            var resultWrapper = expression.Contains(holidays.Keys.ToArray());

            var result = new ScanResult();

            if(resultWrapper.Count > 0)
            {
                result.ResultWrappers = resultWrapper;

                result.ScannedExpression = expression;

                result.NormalizedExpression = expression;

                foreach (var wrapper in resultWrapper)
                {
                    if (wrapper.Found)
                    {
                        var value = option.Language.Holidays[holidays[wrapper.Text]];

                        result.NormalizedExpression = result.NormalizedExpression.Replace(wrapper.Text, value,true);
                    }
                }
            }

            return result;
        }
    }
}
