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
    public class OrdinalConverter
    {
        private static readonly string[] Suffixes = new string[] { "th", "st", "nd", "rd", "th", "th", "th", "th", "th", "th" };

        private static readonly HashSet<string> SuffixTypes = new HashSet<string> { "th", "st", "nd", "rd" };

        private static readonly List<string> Units = new List<string>() { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        private static readonly List<string> Decades = new List<string>() { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        private static readonly List<string> CardinalUnits = new List<string>() { "zero", "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth", "nineth", "tenth", "eleventh", "twelfth", "thirteenth", "fourteenth", "fifteenth", "sixteenth", "seventeenth", "eighteenth", "nineteenth" };

        private static readonly List<string> CardinalDecades = new List<string>() { "zero", "tenth", "twentieth", "thirtieth", "fortieth", "fiftieth", "sixtieth", "seventieth", "eightieth", "ninetieth" };

        private static readonly Dictionary<string, long> Simple = new Dictionary<string, long>
        {
            { "zero", 0L },
            { "one", 1L },
            { "two", 2L },
            { "three", 3L },
            { "four", 4L },
            { "five", 5L },
            { "six", 6L },
            { "seven", 7L },
            { "eight", 8L },
            { "nine", 9L },
            { "ten", 10L },
            { "eleven", 11L },
            { "twelve", 12L },
            { "thirteen", 13L },
            { "fourteen", 14L },
            { "fifteen", 15L },
            { "sixteen", 16L },
            { "seventeen", 17L },
            { "eighteen", 18L },
            { "nineteen", 19L },
            { "twenty", 20L },
            { "thirty", 30L },
            { "forty", 40L },
            { "fifty", 50L },
            { "sixty", 60L },
            { "seventy", 70L },
            { "eighty", 80L },
            { "ninety", 90L }
        };

        private static readonly Dictionary<string, long> Magnitude = new Dictionary<string, long>
        {
            { Definitions.General.Thousand, 1000L },
            { Definitions.General.Million, 1000000L },
            { Definitions.General.Billion, 1000000000L },
            { Definitions.General.Trillion, 1000000000000L },
            { Definitions.General.Quadrillion, 1000000000000000L },
            { Definitions.General.Quintillion, 1000000000000000000L }
        };

        private static readonly Dictionary<string, long> SimpleOrdinal = new Dictionary<string, long>
        {
            { "zero", 0L },
            { "first", 1L },
            { "second", 2L },
            { "third", 3L },
            { "fourth", 4L },
            { "fifth", 5L },
            { "sixth", 6L },
            { "seventh", 7L },
            { "eighth", 8L },
            { "nineth", 9L },
            { "tenth", 10L },
            { "eleventh", 11L },
            { "twelfth", 12L },
            { "thirteenth", 13L },
            { "fourteenth", 14L },
            { "fifteenth", 15L },
            { "sixteenth", 16L },
            { "seventeenth", 17L },
            { "eighteenth", 18L },
            { "nineteenth", 19L },
            { "twentieth", 20L },
            { "thirtieth", 30L },
            { "fortieth", 40L },
            { "fiftieth", 50L },
            { "sixtieth", 60L },
            { "seventieth", 70L },
            { "eightieth", 80L },
            { "ninetieth", 90L }
        };

        private static readonly Dictionary<string, long> MagnitudeOrdinal = new Dictionary<string, long>
        {
            { Definitions.General.Thousand + Suffixes[0], 1000L },
            { Definitions.General.Million + Suffixes[0], 1000000L },
            { Definitions.General.Billion + Suffixes[0], 1000000000L },
            { Definitions.General.Trillion + Suffixes[0], 1000000000000L },
            { Definitions.General.Quadrillion + Suffixes[0] ,1000000000000000L },
            { Definitions.General.Quintillion + Suffixes[0] ,1000000000000000000L }

        };

        public static HashSet<string> CombinedNames()
        {
            var combined = new HashSet<string>(Simple.Keys.ToArray())
            {
                Definitions.General.Hundred
            };
            combined.AddRange(Magnitude.Keys.ToArray());

            return combined;
        }

        public static HashSet<string> CombinedNamesOrdinal()
        {
            var combined = new HashSet<string>(SimpleOrdinal.Keys.ToArray())
            {
                string.Join(Definitions.General.Hundred, Suffixes[0])
            };
            combined.AddRange(MagnitudeOrdinal.Keys.ToArray());

            return combined;
        }

        /// <summary>
        /// Converts a number to its suffix ordinal representation
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string Ordinal(decimal number)
        {
            if (number > Magnitude[Definitions.General.Quintillion])
            {
                throw new Exception("Number too large!");
            }
            switch (number % 100)
            {
                case 11:
                case 12:
                case 13:
                    return number + Suffixes[0];
                default:
                    return number + Suffixes[(long)number % 10L];
            }
        }

        /// <summary>
        /// Converts the given decimal to its ordinal word
        /// representation
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string DecimalToWords(decimal number)
        {
            if (number == 0M)
                return CardinalUnits[0];

            if (number < 0M)
                return Definitions.General.Minus.Pad(0,1) + DecimalToWords(Math.Abs(number));

            if (number > Magnitude[Definitions.General.Quintillion])
            {
                throw new Exception("Number too large!");
            }

            var longPortion = (long)number;

            var fraction = (number - longPortion) * 100M;

            var decPortion = (int)fraction;

            var words = ConvertNumberToWords(longPortion);

            var end = words.Split(' ').Reverse().FirstOrDefault();

            var parts = end.Split('-');

            var add = string.Empty;

            if (decPortion > 0)
            {
                words += Definitions.General.Point.Pad(1, 1);
                words += ConvertNumberToWords(decPortion);
            }
            else
            {
                if (parts.Length <= 1)
                {
                    if (Units.Contains(end))
                    {
                        add = CardinalUnits[Units.IndexOf(end)];
                        return words.Remove(words.LastIndexOf(end)) + add;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(end))
                        {
                            add = CardinalDecades[Decades.IndexOf(end)];
                            return words.Remove(words.LastIndexOf(end)) + add;
                        }
                    }
                }
                else
                {
                    add = CardinalUnits[Units.IndexOf(parts[1])];
                    return words.Remove(words.LastIndexOf(parts[1])) + add;
                }
            }
            return words;
        }

        /// <summary>
        /// Converts the given number to its word representation
        /// in ordinal form
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ConvertNumberToWords(long number)
        {
            if (number == 0L)
                return CardinalUnits[0];

            if (number < 0L)
                return Definitions.General.Minus.Pad(0,1) + ConvertNumberToWords(Math.Abs(number));

            if (number > Magnitude[Definitions.General.Quintillion])
            {
                throw new Exception("Number too large!");
            }

            var words = string.Empty;

            if ((number / 1000000000000000000L) > 0L)
            {
                words += ConvertNumberToWords(number / 1000000000000000000L) + Definitions.General.Quintillion.Pad(0,1);
                number %= 1000000000000000000L;
            }

            if ((number / 1000000000000000L) > 0L)
            {
                words += ConvertNumberToWords(number / 1000000000000000L) + Definitions.General.Quadrillion.Pad(0,1);
                number %= 1000000000000000L;
            }

            if ((number / 1000000000000L) > 0L)
            {
                words += ConvertNumberToWords(number / 1000000000000L) + Definitions.General.Trillion.Pad(0,1);
                number %= 1000000000000L;
            }

            if ((number / 1000000000L) > 0L)
            {
                words += ConvertNumberToWords(number / 1000000000L) + Definitions.General.Billion.Pad(0,1);
                number %= 1000000000L;
            }

            if ((number / 1000000L) > 0L)
            {
                words += ConvertNumberToWords(number / 1000000L) + Definitions.General.Million.Pad(0,1);
                number %= 1000000L;
            }

            if ((number / 1000L) > 0L)
            {
                words += ConvertNumberToWords(number / 1000L) + Definitions.General.Thousand.Pad(0,1);
                number %= 1000L;
            }

            if ((number / 100L) > 0L)
            {
                words += ConvertNumberToWords(number / 100L) + Definitions.General.Hundred.Pad(0,1);
                number %= 100L;
            }

            if (number > 0L)
            {
                if (words != string.Empty)
                    words += "and ";

                if (number < 20L)
                    words += Units[(int)number];
                else
                {
                    words += Decades[(int)number / 10];
                    if ((number % 10) > 0)
                        words += "-" + Units[(int)number % 10];
                }
            }
            return words;
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

                    if (decimal.TryParse(parsed, out decimal result))
                    {
                        return new NumberWrapper(result, NumberType.Ordinal);
                    }
                }
            }

            var parts = cardinalNumber.RemovePaddedPunctuation(0, 1).RemoveWords("and").Split(' ', '-');

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

            var lastPart = string.Empty;

            foreach (var part in parts)
            {

                if (Simple.TryGetValue(part, out long current))
                {
                    smallValue += current;

                    if (integerPart != decimal.MinValue)
                    {
                        fractionalPart += current;
                    }
                }
                else if (SimpleOrdinal.TryGetValue(part, out current))
                {
                    if (string.Compare(lastPart, Units[1]) == 0)
                    {
                        return PortionConverter.WordsToNumber(cardinalNumber);
                    }

                    smallValue += current;

                    if (integerPart != decimal.MinValue)
                    {
                        fractionalPart += current;
                    }
                }
                else if (part == Definitions.General.Hundred || part == string.Concat(Definitions.General.Hundred, Suffixes[0]))
                {
                    if (smallValue == 0)
                    {
                        smallValue = 1;
                    }

                    smallValue *= 100M;

                    if (integerPart != decimal.MinValue)
                    {
                        fractionalPart *= 100M;
                    }
                }
                else if (part == Definitions.General.Point)
                {
                    integerPart = (bigValue + smallValue);
                }
                else
                {
                    if (Magnitude.TryGetValue(part, out current) || MagnitudeOrdinal.TryGetValue(part, out current))
                    {
                        if (smallValue == 0)
                        {
                            smallValue = 1;
                        }

                        bigValue += (smallValue * current);

                        if (integerPart != decimal.MinValue)
                        {
                            fractionalPart += (smallValue * current);
                        }

                        smallValue = 0M;
                    }
                    else
                    {
                        if (part != string.Empty)
                        {
                            return PortionConverter.WordsToNumber(cardinalNumber);
                        }
                    }
                }

                lastPart = part;
            }

            value = negativeNumber ? -(bigValue + smallValue) : (bigValue + smallValue);

            if (integerPart != decimal.MinValue)
            {
                integerPart = negativeNumber ? -integerPart : integerPart;

                value = decimal.Parse(string.Concat(((int)integerPart).ToString(), ".", ((int)fractionalPart).ToString()));
            }

            if (negativeNumber && value < Magnitude[Definitions.General.Quintillion] * -1)
            {
                throw new Exception("Number too small");

            }

            if (value > Magnitude[Definitions.General.Quintillion])
            {
                throw new Exception("Number too large!");
            }

            return new NumberWrapper(value, NumberType.Ordinal); ;
        }
    }
}
