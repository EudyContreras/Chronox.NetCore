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
    public class HolidayScanner : IChronoxScanner
    {
        public string ScannerTag()
        {
            return GetType().Name;
        }

        public ScanWrapper Scan(ChronoxSettings option, string expression)
        {
            var holidays = option.Language.VocabularyBank.GetDictionary(Definitions.Property.Holidays);

            var resultWrapper = expression.Contains(holidays.Keys);

            var result = new ScanWrapper(this);

            if(resultWrapper.Count > 0)
            {
                result.ResultWrappers = resultWrapper.Select(r => r.ToReplaceWrapper(ScannerTag())).ToList();

                result.ScannedExpression = expression;

                result.NormalizedExpression = expression;

                foreach (var wrapper in result.ResultWrappers)
                {
                    var holiday = option.Language.Holidays[holidays[wrapper.TextOriginal]];

                    var value = holiday;

                    if(holiday.Contains("####")){;
                        value = holiday.Replace("####", option.ReferenceDate.Year.ToString());
                    }

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
