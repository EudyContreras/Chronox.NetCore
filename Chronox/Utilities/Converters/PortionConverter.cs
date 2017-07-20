using Chronox.Utilities.Extenssions;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Chronox.Constants;
using Chronox.Wrappers;
using Enumerations;

namespace Chronox.Converters
{
    //One fifth
    //Half a dozen
    //One quater
    //One Half
    //One sixth
    public class PortionConverter
    {
        private static readonly string[] Suffixes = new string[] { "th", "st", "nd", "rd", "th", "th", "th", "th", "th", "th" };

        private static readonly HashSet<string> SuffixTypes = new HashSet<string> { "th", "st", "nd", "rd" };

        private static readonly List<string> Units = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        private static readonly List<string> Decades = new List<string>() { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        private static readonly List<string> CardinalUnits = new List<string>() { "zero", "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth", "nineth", "tenth", "eleventh", "twelveth", "thirteenth", "fourteenth", "fifteenth", "sixteenth", "seventeenth", "eighteenth", "nineteenth" };

        private static readonly List<string> CardinalDecades = new List<string>() { "zero", "tenth", "twentieth", "thirtieth", "fortieth", "fiftieth", "sixtieth", "seventieth", "eightieth", "ninetieth" };

        private static readonly Dictionary<string, decimal> Simple = new Dictionary<string, decimal>
        {
            { "zero", 0.0M },
            { "one", 1.0M },
            { "two", 2.0M },
            { "three", 3.0M },
            { "four", 4.0M },
            { "five", 5.0M },
            { "six", 6.0M },
            { "seven", 7.0M },
            { "eight", 8.0M },
            { "nine", 9.0M },
            { "ten", 10.0M },
            { "eleven", 11.0M },
            { "twelve", 12.0M },
            { "thirteen", 13.0M },
            { "fourteen", 14.0M },
            { "fifteen", 15.0M },
            { "sixteen", 16.0M },
            { "seventeen", 17.0M },
            { "eighteen", 18.0M },
            { "nineteen", 19.0M },
            { "twenty", 20.0M },
            { "thirty", 30.0M },
            { "forty", 40.0M },
            { "fifty", 50.0M },
            { "sixty", 60.0M },
            { "seventy", 70.0M },
            { "eighty", 80.0M },
            { "ninety", 90.0M }
        };

        private static readonly Dictionary<string, decimal> Magnitude = new Dictionary<string, decimal>
        {
            { Definitions.General.Thousand, 1000.0M },
            { Definitions.General.Million, 1000000.0M },
            { Definitions.General.Billion, 1000000000.0M },
            { Definitions.General.Trillion, 1000000000000.0M },
            { Definitions.General.Quadrillion, 1000000000000000.0M },
            { Definitions.General.Quintillion, 1000000000000000000M }
        };

        private static readonly Dictionary<string, decimal> SimpleOrdinal = new Dictionary<string, decimal>
        {
            { "zero", 0.0M },
            { "first", 1.0M },
            { "second", 2.0M },
            { "third", 3.0M },
            { "fourth", 4.0M },
            { "fifth", 5.0M },
            { "sixth", 6.0M },
            { "seventh", 7.0M },
            { "eighth", 8.0M },
            { "nineth", 9.0M },
            { "tenth", 10.0M },
            { "eleventh", 11.0M },
            { "twelveth", 12.0M },
            { "thirteenth", 13.0M },
            { "fourteenth", 14.0M },
            { "fifteenth", 15.0M },
            { "sixteenth", 16.0M },
            { "seventeenth", 17.0M },
            { "eighteenth", 18.0M },
            { "nineteenth", 19.0M },
            { "twentieth", 20.0M },
            { "thirtieth", 30.0M },
            { "fortieth", 40.0M },
            { "fiftieth", 50.0M },
            { "sixtieth", 60.0M },
            { "seventieth", 70.0M },
            { "eightieth", 80.0M },
            { "ninetieth", 90M }
        };

        private static readonly Dictionary<string, decimal> MiscMultipliers = new Dictionary<string, decimal>
        {
            { "quater",0.25M },
            { "half", 0.5M },
            { "whole", 1.0M },
            { "dozen", 12M }           
        };

        private static readonly Dictionary<string, decimal> MagnitudeOrdinal = new Dictionary<string, decimal>
        {
            { Definitions.General.Thousand + Suffixes[0], 1000.0M },
            { Definitions.General.Million + Suffixes[0], 1000000.0M },
            { Definitions.General.Billion + Suffixes[0], 1000000000.0M },
            { Definitions.General.Trillion + Suffixes[0], 1000000000000.0M },
            { Definitions.General.Quadrillion + Suffixes[0] ,1000000000000000.0M },
            { Definitions.General.Quintillion + Suffixes[0] ,1000000000000000000M }

        };

        public static HashSet<string> CombinedNames()
        {
            var combined = new HashSet<string>(Simple.Keys.ToArray());

            combined.Add(Definitions.General.Hundred);
            combined.AddRange(Magnitude.Keys.ToArray());

            return combined;
        }

        public static HashSet<string> CombinedNamesOrdinal()
        {
            var combined = new HashSet<string>(SimpleOrdinal.Keys.ToArray());

            combined.Add(string.Join(Definitions.General.Hundred,Suffixes[0]));
            combined.AddRange(MagnitudeOrdinal.Keys.ToArray());

            return combined;
        }

        
        /// <summary>
        /// Converts cardinal word based numbers into a 
        /// discrete numeric representation.
        /// </summary>
        /// <param name="cardinalNumber"></param>
        /// <returns></returns>
        public static NumberWrapper WordsToNumber(string cardinalNumber)
        {
            foreach (var suffix in SuffixTypes)
            {
                if (cardinalNumber.Contains(suffix))
                {
                    var parsed = cardinalNumber.Replace(suffix, string.Empty, true);

                    var result = 0M;

                    if (decimal.TryParse(parsed, out result))
                    {
                        return new NumberWrapper(result, NumberType.Ordinal);
                    }
                }
            }

            var multipliers = new List<decimal>(); 

            var parts = cardinalNumber.RemovePaddedPunctuation(0, 1).RemoveWords("and").RemoveTerminalCharacter('s').Split(' ', '-');

            var bigValue = 0M;

            var smallValue = 0M;

            var value = 0M;

            var negativeNumber = false;

            var integerPart = decimal.MinValue;

            var fractionalPart = 0M; ;

            if (string.Compare(parts[0], Definitions.General.Minus, true) == 0 || string.Compare(parts[0], Definitions.General.Negative, true) == 0)
            {
                negativeNumber = true;

                parts = string.Join(" ", parts).RemoveWords(Definitions.General.Minus, Definitions.General.Negative).Split(' ');
            }

            foreach (var part in parts)
            {
                var current = 0M;

                if (Simple.TryGetValue(part, out current))
                {
                    smallValue += current;

                    if (integerPart != decimal.MinValue)
                    {
                        fractionalPart += current;
                    }
                }
                else if(SimpleOrdinal.TryGetValue(part, out current))
                {
                    var cu = (1.0M/current); 

                    smallValue *= cu;

                    if (integerPart != decimal.MinValue)
                    {
                        fractionalPart *= (1.0M/current);
                    }
                }
                else if (part == Definitions.General.Hundred)
                {
                    smallValue *= 100M;

                    if (integerPart != decimal.MinValue)
                    {
                        fractionalPart *= 100M;
                    }
                }
                else if(part == string.Concat(Definitions.General.Hundred, Suffixes[0]))
                {
                    smallValue *= (1/100M);

                    if (integerPart != decimal.MinValue)
                    {
                        fractionalPart *= (1/100M);
                    }

                }
                else if (MiscMultipliers.ContainsKey(part))
                {
                    smallValue *= MiscMultipliers[part];
                }
                else if (part == Definitions.General.Point)
                {
                    integerPart = (bigValue + smallValue);
                }
                else
                {
                    if (Magnitude.TryGetValue(part, out current))
                    {
                        bigValue += (smallValue * current);

                        if (integerPart != decimal.MinValue)
                        {
                            fractionalPart += (smallValue * current);
                        }

                        smallValue = 0M;
                    }
                    else if (MagnitudeOrdinal.TryGetValue(part, out current))
                    {
                        bigValue *= (1/(smallValue * current));

                        if (integerPart != decimal.MinValue)
                        {
                            fractionalPart *= (1/(smallValue * current));
                        }

                        smallValue = 0M;
                    }
                    else
                    {
                        if (part != string.Empty)
                        {
                            throw new Exception("Unknown number: " + part);
                        }
                    }
                }
            }

            value = negativeNumber ? -(bigValue + smallValue) : (bigValue + smallValue);

            if (integerPart != decimal.MinValue)
            {
                integerPart = negativeNumber ? -integerPart : integerPart;

                value = decimal.Parse(string.Concat(integerPart.ToString(), ".", fractionalPart.ToString()));
            }

            if (negativeNumber && value < Magnitude[Definitions.General.Quintillion] * -1)
            {
                throw new Exception("Number too small");

            }

            if (value > Magnitude[Definitions.General.Quintillion])
            {
                throw new Exception("Number too large!");
            }

            return new NumberWrapper(value, NumberType.FractionedOrdinal); ;
        }
    }
}
