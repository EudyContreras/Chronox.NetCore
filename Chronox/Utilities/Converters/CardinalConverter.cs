using Chronox.Constants;
using Chronox.Utilities.Extenssions;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronox.Converters
{
    public class CardinalConverter
    {
        private static readonly List<string> Numbers = new List<string>();

        private static readonly string[] UnitsText = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };

        private static readonly string[] DecadesText = { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

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

        public static HashSet<string> CombinedNames()
        {
            var combined = new HashSet<string>(Simple.Keys.ToArray())
            {
                Definitions.General.Hundred
            };
            combined.AddRange(Magnitude.Keys.ToArray());

            return combined;
        }

        /// <summary>
        /// Converts a decimal number to its cardinal representation
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string DecimalToWords(decimal number)
        {
            if (number == 0M)
                return UnitsText[0];

            if (number < 0M)
                return Definitions.General.Minus.Pad(0,1) + DecimalToWords(Math.Abs(number));

            if (number > Magnitude[Definitions.General.Quintillion])
            {
                throw new Exception("Number too large!");
            }

            var longPortion = (long)number;

            var fraction = (number - longPortion) * 100M;

            var decPortion = (int)fraction;

            var words = NumberToWords(longPortion);

            if (decPortion > 0)
            {
                words +=Definitions.General.Point.Pad(1,1);
                words += NumberToWords(decPortion);
            }

            return words.Trim();
        }

        public static string NumberToWords(long number) => ConvertNumberToWords(number).Trim();

        /// <summary>
        /// Converts the given number to its word representation
        /// in cardinal form
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string ConvertNumberToWords(long number)
        {
            if (number == 0L)
                return UnitsText[0];

            if (number < 0L)
                return Definitions.General.Minus.Pad(0,1) + ConvertNumberToWords(Math.Abs(number));

            if (number > Magnitude[Definitions.General.Quintillion])
            {
                throw new Exception("Number too large!");
            }

            string words = string.Empty;

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
                    words += UnitsText[number] + " ";
                else
                {
                    words += DecadesText[number / 10];

                    if ((number % 10L) > 0L)
                    {
                        words += "-" + UnitsText[number % 10] + " ";
                    }
                    else
                    {
                        words += " ";
                    }

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
            var parts = cardinalNumber.RemovePaddedPunctuation(0, 1).RemoveWords("and").Split(' ', '-');

            var bigValue = 0M;

            var smallValue = 0M;

            var value = 0M;

            var last = decimal.MinValue;

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

                if (Simple.TryGetValue(part, out long current))
                {
                    if (last > 0 && last <= 9)
                    {
                        integerPart = last;

                    }
                    smallValue += current;

                    if (integerPart != decimal.MinValue)
                    {
                        fractionalPart += current;
                    }

                    last = smallValue;
                }
                else if (part == Definitions.General.Hundred)
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
                    if (Magnitude.TryGetValue(part, out current))
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

                        smallValue = 0L;
                    }
                    else
                    {
                        if (part != string.Empty)
                        {
                            return OrdinalConverter.WordsToNumber(cardinalNumber);
                        }
                    }
                }
            }

            value = negativeNumber ? -(bigValue + smallValue) : (bigValue + smallValue);

            if(integerPart != decimal.MinValue)
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

            return new NumberWrapper(value, NumberType.Cardinal);
        }
    }
}
