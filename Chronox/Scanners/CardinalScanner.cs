using Chronox.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Chronox.Wrappers;
using Chronox.Utilities.Extenssions;
using Chronox.Converters;
using Chronox.Constants;
using Enumerations;

namespace Chronox.Scanners
{
    public class CardinalScanner : IChronoxScanner
    {

        public string ScannerTag()
        {
            return GetType().Name;
        }

        public ScanWrapper Scan(ChronoxSettings option, string input)
        {
            var scanResult = new ScanWrapper(this);

            var number = new StringBuilder();

            var expression = input.Pad(0,1);

            var timeExpressions = option.Language.VocabularyBank.GetDictionary(Definitions.Property.InterpretedExpression);

            var grabberExpression = option.Language.VocabularyBank.GetDictionary(Definitions.Property.GrabberExpressions);

            var containsWrapper = expression.Contains(timeExpressions.Keys);

            containsWrapper.AddRange(expression.Contains(grabberExpression.Keys));

            var masks = new List<string>();

            var masker = "|^|";

            foreach (var wrapper in containsWrapper)
            {
                var mask = (wrapper.StartIndex + masker + wrapper.EndIndex).ToString();

                expression = expression.ReplaceWords(mask,wrapper.Text, true);

                masks.Add(mask);
            }

            var parts = expression.Split(' ');

            var dictionaryCardinals = option.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericWordCardinal);

            var dictionaryOrdinals = option.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericWordOrdinal);

            var dictionaryDimensionsCardinal = option.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericMagnitudeCardinal);

            var dictionaryDimensionsOrdinal = option.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericMagnitudeOrdinal);

            var ignored = option.Language.VocabularyBank.GetDictionary(Definitions.Property.LogicalOperator)
                .Where(e => Definitions.Converters.LOGICAL_OPERATOR[e.Value] == LogicalOperator.And)
                .Select(e => e.Key)
                .ToList();

            var startIndex = -1;

            foreach (var section in parts)
            {
                var part = section;

                var foundDigit = false;

                var sections = new string[] { };

                var translation = string.Empty;

                if (part.Contains('-'))
                {
                    sections = part.Split('-');
                }
                
                if (sections.Length > 1)
                {
                    foreach (var sect in sections)
                    {
                        translation = FindDigits(number, expression, dictionaryCardinals, dictionaryOrdinals, dictionaryDimensionsCardinal, dictionaryDimensionsOrdinal, ref startIndex, ref foundDigit, sect, true);
                    }

                }
                else
                {
                    translation = FindDigits(number, expression, dictionaryCardinals, dictionaryOrdinals, dictionaryDimensionsCardinal, dictionaryDimensionsOrdinal, ref startIndex, ref foundDigit, part, false);
                }

                if (!foundDigit)
                {
                    if (ignored.Any(i => string.Compare(section, i, true) != 0))
                    {
                        if (number.Length > 0)
                        {
                            CreateResult(scanResult, number, ref expression, ref startIndex, section);

                            number.Clear();
                        }
                    }
                    else
                    {
                        if(number.Length > 0)
                        {
                            number.Append(section).Append(" ");
                        }
                    }
                }
            }

            if (scanResult.NormalizedExpression == null)
            {
                scanResult.NormalizedExpression = expression;
            }

            foreach (var mask in masks)
            {
                var index = mask.Split(masker);

                var start = int.Parse(index[0]);

                var end = int.Parse(index[1]);

                var extraction = containsWrapper.Find(w => w.StartIndex == start && w.EndIndex == end);

                scanResult.NormalizedExpression = scanResult.NormalizedExpression.Replace(mask, extraction.Text, true);
            }

            return scanResult;
        }

        private void CreateResult(ScanWrapper scanResult, StringBuilder number, ref string expression, ref int startIndex, string section)
        {
            var endIndex = startIndex + (number.ToString().Trim().Length - 1);

            var cardinal = number.ToString().ReplaceLast(" and ", " ").ReplaceLast("-",string.Empty).Trim(); //Make language independent

            var digit = CardinalConverter.WordsToNumber(cardinal);

            var replacement = string.Empty;

            var wrapper = new ReplaceWrapper(ScannerTag(), cardinal, startIndex, endIndex);

            scanResult.ResultWrappers.Add(wrapper);

            scanResult.ScannedExpression = expression;

            if (startIndex >= 0)
            {
                
                switch (digit.Type)
                {
                    case NumberType.Cardinal:
                        replacement = digit.Value.ToString();
                        expression = expression.Replace(cardinal, replacement, true);
                        scanResult.NormalizedExpression = expression;
                        break;
                    case NumberType.Ordinal:
                        replacement = OrdinalConverter.Ordinal(digit.Value);
                        expression = expression.Replace(cardinal, replacement, true);
                        scanResult.NormalizedExpression = expression;
                        break;
                    case NumberType.FractionedOrdinal:
                        replacement = OrdinalConverter.Ordinal(digit.Value);
                        expression = expression.Replace(cardinal, replacement, true);
                        scanResult.NormalizedExpression = expression;
                        break;
                }

                wrapper.TextReplacement = replacement;

                wrapper.ReplacementPosition.StartIndex = wrapper.OriginalPosition.StartIndex;

                wrapper.ReplacementPosition.EndIndex = wrapper.ReplacementPosition.StartIndex + replacement.Length;

                startIndex = -1;
            }
            else
            {
                scanResult.NormalizedExpression = expression;
            }
        }

        private static string FindDigits(StringBuilder number, string expression, Dictionary<string, string> dictionaryCardinals, Dictionary<string, string> dictionaryOrdinals, Dictionary<string, string> dictionaryDimensionsCardinal, Dictionary<string, string> dictionaryDimensionsOrdinal, ref int startIndex, ref bool foundDigit, string sect, bool dashDivided)
        {
            var translation = string.Empty ;

            if (dictionaryCardinals.TryGetValue(sect, out translation) || dictionaryDimensionsCardinal.TryGetValue(sect, out translation))
            {
                if (CardinalConverter.CombinedNames().Contains(translation))
                {
                    foundDigit = true;

                    number.Append(translation);

                    if (dashDivided)
                    {
                        number.Append("-");
                    }
                    else
                    {
                        number.Append(" ");
                    }

                    if (startIndex == -1)
                    {
                        startIndex = expression.IndexOf(translation);
                    }
                }
            }
            else if (dictionaryOrdinals.TryGetValue(sect, out translation) || dictionaryDimensionsOrdinal.TryGetValue(sect, out translation))
            {
                if (OrdinalConverter.CombinedNamesOrdinal().Contains(translation) || OrdinalConverter.CombinedNames().Contains(translation))
                {
                    number.Append(translation);

                    if (dashDivided)
                    {
                        number.Append("-");
                    }
                    else
                    {
                        number.Append(" ");
                    }

                    foundDigit = true;

                    if (startIndex == -1)
                    {
                        startIndex = expression.IndexOf(translation);
                    }
                }
            }

            return translation;
        }
    }
}
