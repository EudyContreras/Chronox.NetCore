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
        public ScanWrapper Scan(ChronoxSettings option, string input)
        {
            var scanResult = new ScanWrapper();

            var number = new StringBuilder();

            var expression = input.PadPunctuationExact(1, 1, '-');

            var timeExpressions = option.Language.VocabularyBank.GetDictionary(Definitions.Property.InterpretedExpression);

            var grabberExpression = option.Language.VocabularyBank.GetDictionary(Definitions.Property.GrabberExpressions);

            var containsWrapper = expression.Contains(timeExpressions.Keys.ToArray());

            containsWrapper.AddRange(expression.Contains(grabberExpression.Keys.ToArray()));

            var masks = new List<string>();

            var masker = "|^|";

            foreach (var wrapper in containsWrapper)
            {
                var mask = (wrapper.StartIndex + masker + wrapper.EndIndex).ToString();

                expression = expression.Replace(wrapper.Text, mask, true);

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

            foreach (var part in parts)
            {
                var translation = string.Empty;

                if (dictionaryCardinals.TryGetValue(part, out translation) || dictionaryDimensionsCardinal.TryGetValue(part, out translation))
                {
                    if (CardinalConverter.CombinedNames().Contains(translation))
                    {
                        number.Append(translation).Append(" ");

                        if (startIndex == -1)
                        {
                            startIndex = expression.IndexOf(part);
                        }
                    }                    
                }
                else if(dictionaryOrdinals.TryGetValue(part, out translation) || dictionaryDimensionsOrdinal.TryGetValue(part, out translation))
                {
                    if (OrdinalConverter.CombinedNamesOrdinal().Contains(translation))
                    {
                        number.Append(translation).Append(" ");

                        if (startIndex == -1)
                        {
                            startIndex = expression.IndexOf(part);
                        }
                    }
                    else if (OrdinalConverter.CombinedNames().Contains(translation))
                    {
                        number.Append(translation).Append(" ");

                        if (startIndex == -1)
                        {
                            startIndex = expression.IndexOf(part);
                        }
                    }
                }
                else
                {
                    if (ignored.Any(i => string.Compare(part, i, true) != 0) && string.Compare(part, "-", true) != 0)
                    {
                        if (number.Length > 0)
                        {
                            var endIndex = expression.LastIndexOf(part);

                            var cardinal = number.ToString().ReplaceLast(" and ", " ").Replace(" - ", "-").Trim(); //Make language independent

                            var digit = CardinalConverter.WordsToNumber(cardinal);

                            var wrapper = new ContainsWrapper(true, cardinal, startIndex, endIndex);

                            scanResult.ResultWrappers.Add(wrapper);

                            scanResult.ScannedExpression = expression;

                            if(startIndex >= 0)
                            {
                                switch (digit.Type)
                                {
                                    case NumberType.Cardinal:
                                        expression = expression.Replace(expression.Substring(startIndex, endIndex - startIndex), digit.Value.ToString().Normalize(0, 1, false), true);
                                        expression = expression.Replace(" - ", "-", true);
                                        scanResult.NormalizedExpression = expression;
                                        break;
                                    case NumberType.Ordinal:
                                        expression = expression.Replace(expression.Substring(startIndex, endIndex - startIndex), OrdinalConverter.Ordinal(digit.Value).Normalize(0, 1, false), true);
                                        expression = expression.Replace(" - ", "-", true);
                                        scanResult.NormalizedExpression = expression;
                                        break;
                                    case NumberType.FractionedOrdinal:
                                        scanResult.NormalizedExpression = expression.Replace(expression.Substring(startIndex, endIndex - startIndex), OrdinalConverter.Ordinal(digit.Value).Normalize(0, 1, false), true);
                                        scanResult.NormalizedExpression = scanResult.NormalizedExpression.Replace(" - ", "-", true);
                                        expression = scanResult.NormalizedExpression;
                                        break;
                                }

                                startIndex = -1;
                            }
                            else
                            {
                                expression = expression.Replace(" - ", "-", true);
                                scanResult.NormalizedExpression = expression;
                            }

                            number.Clear();
                        }
                    }
                    else
                    {
                        number.Append(part).Append(" ");
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
    }
}
