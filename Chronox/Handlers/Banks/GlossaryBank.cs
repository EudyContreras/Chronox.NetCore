using Chronox.Constants;
using Chronox.Helpers.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Banks
{
    internal class GlossaryBank
    {
        public string WordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

        public string WordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

        private Dictionary<string, string> LogicalOperator = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> CasualExpressions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> GrabberExpressions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DayOffset = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> TimeExpressions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> TimeFractions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> TimeConjointers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> ArithmeticOperators = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> InterpretedExpressions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> RepeaterIndicators = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> RepeaterExpressions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DurationIndicator = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DurationExpressions = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> Certainty = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> RangeIndicator = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> RangeSeparator = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> Holidays = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DecadeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> TimePeriods = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> TimeOfDay = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DaysOfWeek = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> SeasonOfYear = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> MonthsOfYear = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DateTimeUnits = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> TimeUnits = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DateUnits = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> YearUnits = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> MonthUnits = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> WeekUnits = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DayUnits = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> NumericValue = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> NumericMagnitudesCardinal = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> NumericMagnitudesOrdinal = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> NumericWord= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> NumericWordOrdinal = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> NumericWordCardinal = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> TimeMeridiam = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        private Dictionary<string, string> DayOfWeekType = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public Dictionary<string, string> GetDictionary(string dictionary)
        {
            if (dictionary.Equals(Definitions.Property.LogicalOperator)) return LogicalOperator;

            if (dictionary.Equals(Definitions.Property.CasualExpressions)) return CasualExpressions;

            if (dictionary.Equals(Definitions.Property.GrabberExpressions)) return GrabberExpressions;

            if (dictionary.Equals(Definitions.Property.DayOffset)) return DayOffset;

            if (dictionary.Equals(Definitions.Property.TimeExpressions)) return TimeExpressions;

            if (dictionary.Equals(Definitions.Property.TimeFractions)) return TimeFractions;

            if (dictionary.Equals(Definitions.Property.ArithmeticOperator)) return ArithmeticOperators;

            if (dictionary.Equals(Definitions.Property.TimeConjointer)) return TimeConjointers;

            if (dictionary.Equals(Definitions.Property.InterpretedExpression)) return InterpretedExpressions;

            if (dictionary.Equals(Definitions.Property.RangeIndicator)) return RangeIndicator;

            if (dictionary.Equals(Definitions.Property.RangeSeparator)) return RangeSeparator;

            if (dictionary.Equals(Definitions.Property.RepeaterExpressions)) return RepeaterExpressions;

            if (dictionary.Equals(Definitions.Property.RepeaterIndicators)) return RepeaterIndicators;

            if (dictionary.Equals(Definitions.Property.DurationIndicators)) return DurationIndicator;

            if (dictionary.Equals(Definitions.Property.DurationExpressions)) return DurationExpressions;

            if (dictionary.Equals(Definitions.Property.Proximity)) return Certainty;

            if (dictionary.Equals(Definitions.Property.Holidays)) return Holidays;

            if (dictionary.Equals(Definitions.Property.DecadeValues)) return DecadeValues;

            if (dictionary.Equals(Definitions.Property.TimePeriods)) return TimePeriods;

            if (dictionary.Equals(Definitions.Property.TimeOfDay)) return TimeOfDay;

            if (dictionary.Equals(Definitions.Property.DaysOfWeek)) return DaysOfWeek;

            if (dictionary.Equals(Definitions.Property.SeasonOfYear)) return SeasonOfYear;

            if (dictionary.Equals(Definitions.Property.MonthsOfYear)) return MonthsOfYear;

            if (dictionary.Equals(Definitions.Property.DateTimeUnits)) return DateTimeUnits;

            if (dictionary.Equals(Definitions.Property.TimeUnits)) return TimeUnits;

            if (dictionary.Equals(Definitions.Property.DateUnits)) return DateUnits;

            if (dictionary.Equals(Definitions.Property.YearUnit)) return YearUnits;

            if (dictionary.Equals(Definitions.Property.MonthUnit)) return MonthUnits;

            if (dictionary.Equals(Definitions.Property.WeekUnit)) return WeekUnits;

            if (dictionary.Equals(Definitions.Property.DayUnit)) return DayUnits;

            if (dictionary.Equals(Definitions.Property.NumericValue)) return NumericValue;

            if (dictionary.Equals(Definitions.Property.NumericMagnitudeCardinal)) return NumericMagnitudesCardinal;

            if (dictionary.Equals(Definitions.Property.NumericMagnitudeOrdinal)) return NumericMagnitudesOrdinal;

            if (dictionary.Equals(Definitions.Property.NumericWord)) return NumericWord;

            if (dictionary.Equals(Definitions.Property.NumericWordOrdinal)) return NumericWordOrdinal;

            if (dictionary.Equals(Definitions.Property.NumericWordCardinal)) return NumericWordCardinal;

            if (dictionary.Equals(Definitions.Property.TimeMeridiam)) return TimeMeridiam;

            if (dictionary.Equals(Definitions.Property.DayOfWeekType)) return DayOfWeekType;

            return null;
        }
    }
}
