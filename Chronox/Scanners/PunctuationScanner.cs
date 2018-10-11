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
    public class PunctuationScanner : IChronoxScanner
    {
        public string ScannerTag()
        {
            return GetType().Name;
        }

        public ScanWrapper Scan(ChronoxSettings option, string input)
        {
            var commonPunctuation = option.Language.Vocabulary.CommonPunctuation;

            var indexes = new List<int>();

            var result = new ScanWrapper(this);

            var expression = input;

            for (var i = 0; i < commonPunctuation.Count; i++)
            {
                var start = 0;

                var noMoreFound = false;

                while(!noMoreFound){
                    
                    var index = expression.IndexOf(commonPunctuation[i].Item1,start, expression.Length-start);

                    if(index == -1 || index == expression.Length-1){
                        noMoreFound = true;
                        continue;
                    }

                    indexes.Add(index);
                    start = index+1;
                }

                if(commonPunctuation[i].Item1 != commonPunctuation[i].Item2)
                {
                     expression = expression.Replace(string.Concat(commonPunctuation[i].Item1.ToString()," "), string.Concat(commonPunctuation[i].Item2.ToString(), " "));
                }
            }

            if(indexes.Count > 0){

                var replaceWrappers = new List<ReplaceWrapper>();


                foreach(int index in indexes)
                {

                    var hasProceedingSpace = (expression[index + 1] == ' ');

                    if (hasProceedingSpace)
                    {

                        var replacement = new ReplaceWrapper(ScannerTag(), index, index);

                        replacement.TextOriginal = expression[index].ToString();
                        replacement.TextReplacement =  string.Concat(StringExtenssions.Spaces(1), replacement.TextOriginal);

                        replaceWrappers.Add(replacement);           //IF shift some formats will suffer, and if removed it will be harder to find multiple dates in.
                    }
                }

                result.ResultWrappers = replaceWrappers;

                result.ScannedExpression = expression;

                result.NormalizedExpression = expression;

                foreach (var wrapper in result.ResultWrappers)
                {
                    wrapper.ReplacementPosition.StartIndex = wrapper.OriginalPosition.StartIndex;

                    wrapper.ReplacementPosition.EndIndex = wrapper.ReplacementPosition.StartIndex + wrapper.TextReplacement.Length;

                    result.NormalizedExpression = result.NormalizedExpression.Replace(wrapper.TextOriginal, wrapper.TextReplacement, true);
                 }
            }
       
            return result;
        }
    }
}
