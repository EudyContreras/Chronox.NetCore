using Chronox.Constants;
using Chronox.Helpers.Offsets;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers.Interpreters
{
    internal class ConversionHandler
    {
        public static int DayOfWeek(ChronoxSettings settings, string dayOfWeek)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DaysOfWeek);

            if (dictionary.TryGetValue(dayOfWeek, out key))
            {
                return (int)Definitions.Converters.DAY_OF_WEEK[key];
            }

            return int.MinValue;
        }

        public static int Month(ChronoxSettings settings, string month)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.MonthsOfYear);

            if (dictionary.TryGetValue(month, out key))
            {
                return Definitions.Converters.MONTHS[key];
            }

            return int.MinValue;
        }

        public static int Season(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.SeasonOfYear);

            if (dictionary.TryGetValue(input, out key))
            {
                return (int)Definitions.Converters.SEASONS[key];
            }

            return int.MinValue;
        }

        public static int NumericWord(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericWord);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.NUMBERS_WORDS[key];
            }

            return int.MinValue;            
        }

        public static int NumericWordOrdinal(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericWordOrdinal);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.NUMBERS_WORDS_ORDINAL[key];
            }

            return int.MinValue;
        }

        public static int NumericWordCardinal(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericWordCardinal);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.NUMBERS_WORDS_CARDINAL[key];
            }

            return int.MinValue;
        }

        public static long NumericMagnitude(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericMagnitudeCardinal);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.NUMERIC_MAGNITUDES_CARDINAL[key];
            }

            return long.MinValue;
        }

        public static int NumericValue(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericValue);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.NUMBERS[key];
            }

            return int.MinValue;
        }

        public static int DayOffset(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DayOffset);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.DAY_OFFSET[key];
            }

            return int.MinValue;
        }

        public static ChronoxTimeComponent TimeExpression(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimeExpressions);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_EXPRESSION[key] ;
            }

            return null;
        }

        public static TimeRange TimeOfDay(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimeOfDay);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_OF_DAY[key];
            }

            return null;
        }

        public static int DecadeValues(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DecadeValues);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.DECADE_VALUES[key];
            }

            return int.MinValue;
        }

        public static LogicalOperator LogicalOperator(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.LogicalOperator);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.LOGICAL_OPERATOR[key];
            }

            return Enumerations.LogicalOperator.Default;
        }

        public static TimeRepeater RepeaterExpression(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.RepeaterExpressions);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.REPERATER_EXPRESSIONS[key];
            }

            return TimeRepeater.Default;
        }

        public static DateRepeaterIndicator RepeaterIndicator(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.RepeaterExpressions);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.REPEATER_INDICATOR[key];
            }

            return DateRepeaterIndicator.Default;
        }

        public static TimeDurationExpression DurationExpression(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DurationExpressions);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.DURATION_EXPRESSIONS[key];
            }

            return TimeDurationExpression.Default;
        }

        public static TimeDurationIndicator DurationIndicator(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DurationIndicators);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.DURATION_INDICATOR[key];
            }

            return TimeDurationIndicator.Default;
        }

        public static CertaintyType ProximityType(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.Proximity);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.PROXIMITY[key];
            }

            return Enumerations.CertaintyType.Default;
        }

        public static TimePeriod TimePeriod<T>(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimePeriods);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_PERIODS[key];
            }

            return Enumerations.TimePeriod.Default;
        }

        public static DateArithmeticOperation ArithmeticOperation(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.ArithmeticOperator);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.ARITHMETIC_OPERATION[key];
            }

            return Enumerations.DateArithmeticOperation.Default;
        }

        public static DateCasualExpression CasualExpression(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.CasualExpressions);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.CASUAL_EXPRESSION[key];
            }

            return DateCasualExpression.Default;
        }

        public static DateTimeUnit DateTimeUnit(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DateTimeUnits);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_UNIT[key];
            }

            return Enumerations.DateTimeUnit.Default;
        }

        public static DateTimeUnit DateUnit(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DateUnits);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_UNIT[key];
            }

            return Enumerations.DateTimeUnit.Default;
        }

        public static DateTimeUnit TimeUnit(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimeUnits);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_UNIT[key];
            }

            return Enumerations.DateTimeUnit.Default;
        }

        public static TimeRelation GrabberExpression(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.GrabberExpressions);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.GRABBER_EXPRESSION[key];
            }

            return TimeRelation.Default;
        }

        public static TimeFraction Fraction(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimeFractions);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_FRACTIONS[key];
            }

            return TimeFraction.Default;
        }

        public static TimeConjointer TimeConjointer(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimeConjointer);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_CONJOINTER[key];
            }

            return Enumerations.TimeConjointer.Default;
        }

        public static TimeMeridiam TimeMeridiam(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimeMeridiam);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.TIME_MERIDIAM[key];
            }

            return Enumerations.TimeMeridiam.Default;
        }

        public static DateTimeExpression InterpretedExpression(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.InterpretedExpression);

            if (dictionary.TryGetValue(input, out key))
            {
                return Definitions.Converters.DATE_TIME_EXPRESSION[key];
            }

            return DateTimeExpression.Default;
        }

        public static TimeRangePointer RangePointer(ChronoxSettings settings, string input)
        {
            var key = string.Empty;

            var dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.RangeIndicator);

            if (dictionary.TryGetValue(input, out key))
            {
                return TimeRangePointer.Indicator;
            }

            dictionary = settings.Language.VocabularyBank.GetDictionary(Definitions.Property.RangeSeparator);

            if (dictionary.TryGetValue(input, out key))
            {
                return TimeRangePointer.Separator;
            }

            return TimeRangePointer.Default;
        }
    }
}
