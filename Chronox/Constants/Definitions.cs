using Chronox.Helpers;
using Chronox.Helpers.Offsets;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Constants
{
    internal static class Definitions
    {
        public const string FilePath = @"Languages\Files";

        public const string DefaultLanguage = "English";

        public static class General
        {
            public const string Year = "year";
            public const string Month = "month";
            public const string Week = "week";
            public const string Day = "day";
            public const string Hour = "hour";
            public const string Minute = "minute";
            public const string Second = "second";


            public const string Half = "half";
            public const string Quater = "quater";
            public const string Dozen = "dozen";
            public const string Point = "point";
            public const string Minus = "minus";
            public const string Negative = "negative";
            public const string Hundred = "hundred";
            public const string Thousand = "thousand";
            public const string Million = "million";
            public const string Billion = "billion";
            public const string Trillion = "trillion";
            public const string Quadrillion = "quadrillion";
            public const string Quintillion = "quintillion";

            public const string DateTime = "dateTime";
            public const string TimeSet = "timeSet";
            public const string TimeSpan = "timeSpan";
            public const string TimeRange = "timeRange";
        }

        public static class DynamicIndex
        {

            public const int LogicalOperator = 0;
            public const int ArithmeticOperator = 1;
            public const int CasualExpressions = 2;
            public const int GrabberExpressions = 3;
            public const int TimeExpressions = 4;
            public const int TimeFractions = 5;
            public const int TimeConjointer = 6;
            public const int InterpretedExpression = 7;
            public const int RangeIndicator = 8;
            public const int RangeSeparator = 9;
            public const int DecadeValues = 10;
            public const int TimePeriods = 11;
            public const int DayOffset = 12;
            public const int Holidays = 13;
            public const int TimeOfDay = 14;
            public const int DaysOfWeek = 15;
            public const int SeasonOfYear = 16;
            public const int MonthsOfYear = 17;
            public const int DateTimeUnits = 18;
            public const int NumericValue = 19;
            public const int NumericWord = 20;
            public const int NumericWordOrdinal = 21;
            public const int NumericWordCardinal = 22;
            public const int TimeMeridiam = 23;
            public const int DateUnits = 24;
            public const int TimeUnits = 25;
            public const int YearUnit = 26;
            public const int MonthUnit = 27;
            public const int WeekUnit = 28;
            public const int DayUnit = 29;
            public const int HourUnit = 30;
            public const int MinuteUnit = 31;
            public const int SecondUnit = 32;
            public const int Proximity = 33;
            public const int RepeaterExpressions = 34;
            public const int DurationExpressions = 35;
            public const int RepeaterIndicators = 36;
            public const int DurationIndicators = 37;
            public const int NumericMagnitudeCardinal = 38;
            public const int NumericMagnitudeOrdinal = 39;
        }


        public static class StaticIndex
        {
            public const int DateBigEndian = 0;
            public const int DateLittleEndian = 1;
            public const int DateMiddleEndian = 2;
            public const int SpaceSeparator = 3;
            public const int OptionalSpace = 4;
            public const int SpaceRemover = 5;
            public const int TimeSeparator = 6;
            public const int DateSeparator = 7;
            public const int NumberMax2Digits = 8;
            public const int NumberMax4Digits = 9;
            public const int NumberMax5Digits = 10;
            public const int WordStart = 11;
            public const int WordEnd = 12;
            public const int Date = 13;
            public const int Time = 14;
            public const int Year = 15;
            public const int YearDiscrete = 16;
            public const int MonthDiscrete = 17;
            public const int DayDiscrete = 18;
            public const int Hour = 19;
            public const int Minute = 20;
            public const int Second = 21;
            public const int HourDiscrete = 22;
            public const int MinuteDiscrete = 23;
            public const int SecondDiscrete = 24;
            public const int Millis = 25;
            public const int ZoneOffset = 26;
        }

        //TODO: Need to switch to this acessing method!
        public static class Properties
        {

            public static readonly string[] Dynamic =
            {
                "logicalOperator",
                "arithmeticOperator",
                "casualExpressions",
                "grabberExpressions",
                "timeExpressions",
                "timeFractions",
                "timeConjointer",
                "interpretedExpressions",
                "rangeIndicators",
                "rangeSeparators",
                "decadeValues",
                "timePeriods",
                "dayOffsets",
                "holidays",
                "timesOfDay",
                "daysOfWeek",
                "seasonsOfYear",
                "monthsOfYear",
                "dateTimeUnits",
                "numericValues",
                "numericWords",
                "numericWordsOrdinal",
                "numericWordsCardinal",
                "timeMeridiam",
                "dateUnit",
                "timeUnit",
                "yearUnit",
                "monthUnit",
                "weekUnit",
                "dayUnit",
                "hourUnit",
                "minuteUnit",
                "secondUnit",
                "proximity",
                "repeaterExpressions",
                "durationExpressions",
                "repeaterIndicators",
                "durationIndicators",
                "numericMagnitudeCardinal",
                "numericMagnitudeOrdinal",
            };
            
            public static string[] Static =
            {
                "discreteDateBigEndian",
                "discreteDateLittleEndian",
                "discreteDateMediumEndian",
                "whiteSpace",
                "optionalSpace",
                "whiteSpaceRemover",
                "discreteTimeSeparator",
                "discreteDateSeparator",
                "numberMax2Digits",
                "numberMax4Digits",
                "numberMax5Digits",
                "wordStart",
                "wordEnd",
                "discreteDate",
                "discreteTime",
                "dependentYear",
                "discreteYear",
                "discreteMonth",
                "discreteDay",
                "dependentHour",
                "dependentMinute",
                "dependentSecond",
                "discreteHour",
                "discreteMinute",
                "discreteSecond",
                "dependentMilliseconds",
                "discreteZone",
            };
        }

        public static class Property
        {
            public const string LogicalOperator = "logicalOperator";
            public const string ArithmeticOperator = "arithmeticOperator";
            public const string CasualExpressions = "casualExpressions";
            public const string GrabberExpressions = "grabberExpressions";
            public const string TimeExpressions = "timeExpressions";
            public const string TimeFractions = "timeFractions";
            public const string TimeConjointer = "timeConjointer";
            public const string InterpretedExpression = "interpretedExpressions";
            public const string RangeIndicator = "rangeIndicators";
            public const string RangeSeparator = "rangeSeparators";
            public const string DecadeValues = "decadeValues";
            public const string TimePeriods = "timePeriods";
            public const string DayOffset = "dayOffsets";
            public const string Holidays = "holidays";
            public const string TimeOfDay = "timesOfDay";
            public const string DaysOfWeek = "daysOfWeek";
            public const string SeasonOfYear = "seasonsOfYear";
            public const string MonthsOfYear = "monthsOfYear";
            public const string DateTimeUnits = "dateTimeUnits";
            public const string NumericValue = "numericValues";
            public const string NumericWord = "numericWords";
            public const string NumericWordOrdinal = "numericWordsOrdinal";
            public const string NumericWordCardinal = "numericWordsCardinal";
            public const string TimeMeridiam = "timeMeridiam";
            public const string DateUnits = "dateUnit";
            public const string TimeUnits = "timeUnit";
            public const string YearUnit = "yearUnit";
            public const string MonthUnit = "monthUnit";
            public const string WeekUnit = "weekUnit";
            public const string DayUnit = "dayUnit";
            public const string HourUnit = "hourUnit";
            public const string MinuteUnit = "minuteUnit";
            public const string SecondUnit = "secondUnit";
            public const string Proximity = "proximity";
            public const string RepeaterExpressions = "repeaterExpressions";
            public const string DurationExpressions = "durationExpressions";
            public const string RepeaterIndicators = "repeaterIndicators";
            public const string DurationIndicators = "durationIndicators";
            public const string NumericMagnitudeCardinal = "numericMagnitudeCardinal";
            public const string NumericMagnitudeOrdinal = "numericMagnitudeOrdinal";
        }

        public static class Patterns
        {
            public const string DateBigEndian = "discreteDateBigEndian";
            public const string DateLittleEndian = "discreteDateLittleEndian";
            public const string DateMiddleEndian = "discreteDateMediumEndian";
            public const string SpaceSeparator = "whiteSpace";
            public const string OptionalSpace = "optionalSpace";
            public const string SpaceRemover = "whiteSpaceRemover";
            public const string TimeSeparator = "discreteTimeSeparator";
            public const string DateSeparator = "discreteDateSeparator";
            public const string NumberMax2Digits = "numberMax2Digits";
            public const string NumberMax4Digits = "numberMax4Digits";
            public const string NumberMax5Digits = "numberMax5Digits";
            public const string WordStart = "wordStart";
            public const string WordEnd = "wordEnd";
            public const string Date = "discreteDate";
            public const string Time = "discreteTime";
            public const string Year = "dependentYear";
            public const string YearDiscrete = "discreteYear";
            public const string MonthDiscrete = "discreteMonth";
            public const string DayDiscrete = "discreteDay";
            public const string Hour = "dependentHour";
            public const string Minute = "dependentMinute";
            public const string Second = "dependentSecond";
            public const string HourDiscrete = "discreteHour";
            public const string MinuteDiscrete = "discreteMinute";
            public const string SecondDiscrete = "discreteSecond";
            public const string Millis = "dependentMilliseconds";
            public const string ZoneOffset = "discreteZone";
        }

        public static class Converters
        {

            public static readonly IReadOnlyDictionary<string, string> PROPERTIES = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {"C.E",    Property.CasualExpressions},
                {"G.E",    Property.GrabberExpressions},
                {"D.O",    Property.DayOffset},
                {"T.E",    Property.TimeExpressions},
                {"T.F",    Property.TimeFractions},
                {"T.C",    Property.TimeConjointer},
                {"I.E",    Property.InterpretedExpression },
                {"T.M",    Property.GrabberExpressions},
                {"R.I",    Property.RangeIndicator},
                {"R.S",    Property.RangeSeparator},
                {"N.V",    Property.NumericValue},
                {"N.M.C",  Property.NumericMagnitudeCardinal},
                {"N.M.O",  Property.NumericMagnitudeOrdinal},
                {"N.W",    Property.NumericWord},
                {"N.W.C",  Property.NumericWordCardinal},
                {"N.W.O",  Property.NumericWordOrdinal},
                {"T.O.D",  Property.TimeOfDay},
                {"D.O.W",  Property.DaysOfWeek},
                {"S.O.Y",  Property.SeasonOfYear},
                {"M.O.Y",  Property.MonthsOfYear},
                {"C.Y",    Property.MonthsOfYear},
                {"R.E.I",  Property.RepeaterIndicators},
                {"D.E.I",  Property.RepeaterIndicators},
                {"R.E",    Property.RepeaterExpressions},
                {"D.E",    Property.DurationExpressions},
                {"P.T",    Property.Proximity},
                {"T.U",    Property.DateTimeUnits},
                {"T.P",    Property.TimePeriods },
                {"D.V",    Property.DecadeValues },
                {"A.O",    Property.ArithmeticOperator},
                {"L.O",    Property.LogicalOperator},
                {"D.D",    Patterns.Date},
                {"D.T",    Patterns.Time},
                {"D.Y",    Patterns.Year},
                {"Y",      Patterns.YearDiscrete},
                {"M",      Patterns.MonthDiscrete},
                {"D",      Patterns.DayDiscrete},
                {"H.D",    Patterns.HourDiscrete},
                {"D.D.B",  Patterns.DateBigEndian},
                {"D.D.L",  Patterns.DateLittleEndian},
                {"D.D.M",  Patterns.DateMiddleEndian},
                {"M.D",    Patterns.MinuteDiscrete},
                {"S.D",    Patterns.SecondDiscrete},
                {"W.S",    Patterns.SpaceSeparator},
                {"D.U.B",  Property.DateUnits },
                {"T.U.B",  Property.TimeUnits },
                {"Y.U",    Property.YearUnit },
                {"M.U",    Property.MonthUnit },
                {"W.U",    Property.WeekUnit },
                {"D.U",    Property.DayUnit },
                {"H.U",    Property.HourUnit },
                {"M.I.U",  Property.MinuteUnit },
                {"S.U",    Property.SecondUnit },
                {"D.T.S",  Patterns.TimeSeparator},
                {"D.D.S",  Patterns.DateSeparator},
                {"N.M.2.D", Patterns.NumberMax2Digits},
                {"N.M.4.D", Patterns.NumberMax4Digits},
                {"N.M.5.D", Patterns.NumberMax5Digits},
            };

            public static readonly IReadOnlyDictionary<string, TimeRange> TIME_OF_DAY = new Dictionary<string, TimeRange>(StringComparer.OrdinalIgnoreCase)
            {
                {"Morning", RangeConstants.MORNING_RANGE},
                {"Afternoon", RangeConstants.AFTERNOON_RANGE},
                {"Evening", RangeConstants.EVENING_RANGE},
                {"Night", RangeConstants.NIGHT_RANGE},
            };

            public static readonly IReadOnlyDictionary<string, int> DAY_OFFSET = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"Today", 0},
                {"Yesterday", -1},
                {"Tomorrow", 1},
                {"Day After Tomorrow",2},
                {"Day Before Yesterday", -2},
            };

            public static readonly IReadOnlyDictionary<string, int> DECADE_VALUES = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"tens", 1910},
                {"twenties", 1920},
                {"thirties", 1930},
                {"forties", 1940},
                {"fifties", 1950},
                {"sixties", 1960},
                {"seventies", 1970},
                {"eighties", 1980},
                {"nineties", 1990},
            };

            public static readonly IReadOnlyDictionary<string, TimePeriod> TIME_PERIODS = new Dictionary<string, TimePeriod>(StringComparer.OrdinalIgnoreCase)
            {
                {"decade", TimePeriod.Decade},
                {"century", TimePeriod.Century},
                {"millennium", TimePeriod.Millennium}
            };

            public static readonly IReadOnlyDictionary<string, DateRepeaterIndicator> REPEATER_INDICATOR = new Dictionary<string, DateRepeaterIndicator>(StringComparer.OrdinalIgnoreCase)
            {
                {"every", DateRepeaterIndicator.Every},
                {"every other", DateRepeaterIndicator.EveryOther }
            };

            public static readonly IReadOnlyDictionary<string, TimeDurationIndicator> DURATION_INDICATOR = new Dictionary<string, TimeDurationIndicator>(StringComparer.OrdinalIgnoreCase)
            {
                {"for", TimeDurationIndicator.For},
            };

            public static readonly IReadOnlyDictionary<string, DateTimeIndicator> TIME_INDICATOR = new Dictionary<string, DateTimeIndicator>(StringComparer.OrdinalIgnoreCase)
            {
                {"in", DateTimeIndicator.In},
            };

            public static readonly IReadOnlyDictionary<string, TimeFraction> TIME_FRACTIONS = new Dictionary<string, TimeFraction>(StringComparer.OrdinalIgnoreCase)
            {
                {"quater",TimeFraction.Quater},
                {"half", TimeFraction.Half }
            };

            public static readonly IReadOnlyDictionary<string, TimeConjointer> TIME_CONJOINTER = new Dictionary<string, TimeConjointer>(StringComparer.OrdinalIgnoreCase)
            {
                {"to",TimeConjointer.To},
                {"past", TimeConjointer.Past },
                {"ago",TimeConjointer.Ago },
                {"from now",TimeConjointer.FromNow },
                {"from",TimeConjointer.From },
            };

            public static readonly IReadOnlyDictionary<string, TimeMeridiam> TIME_MERIDIAM = new Dictionary<string, TimeMeridiam>(StringComparer.OrdinalIgnoreCase)
            {
                {"am",TimeMeridiam.AM},
                {"pm", TimeMeridiam.PM },
            };

            public static readonly IReadOnlyDictionary<string, DateArithmeticOperation> ARITHMETIC_OPERATION = new Dictionary<string, DateArithmeticOperation>(StringComparer.OrdinalIgnoreCase)
            {
                {"-",DateArithmeticOperation.Substract},
                {"+", DateArithmeticOperation.Add }
            };

            public static readonly IReadOnlyDictionary<string, DateTimeExpression> DATE_TIME_EXPRESSION = new Dictionary<string, DateTimeExpression>(StringComparer.OrdinalIgnoreCase)
            {
                {"Tonight",DateTimeExpression.Tonight },
                {"Last Night",DateTimeExpression.LastNight },
                {"Now", DateTimeExpression.Now },
            };

            public static readonly IReadOnlyDictionary<string, ChronoxTimeComponent> TIME_EXPRESSION = new Dictionary<string, ChronoxTimeComponent>(StringComparer.OrdinalIgnoreCase)
            {
                {"midday",new ChronoxTimeComponent(12,0,0)},
                {"midnight", new ChronoxTimeComponent(0,0,0) }
            };

            public static readonly IReadOnlyDictionary<string, TimeRelation> GRABBER_EXPRESSION = new Dictionary<string, TimeRelation>(StringComparer.OrdinalIgnoreCase)
            {
                {"last",TimeRelation.Past },
                {"next",TimeRelation.Future },
                {"this",TimeRelation.Present },
            };

            public static readonly IReadOnlyDictionary<string, DateCasualExpression> CASUAL_EXPRESSION = new Dictionary<string, DateCasualExpression>(StringComparer.OrdinalIgnoreCase)
            {        
                {"on",DateCasualExpression.On },
                {"oclock",DateCasualExpression.Oclock },
                {"in",DateCasualExpression.In },
                {"of", DateCasualExpression.Of },
                {"during",DateCasualExpression.During },
                {"and",DateCasualExpression.And }
            };

            public static readonly IReadOnlyDictionary<string, LogicalOperator> LOGICAL_OPERATOR = new Dictionary<string, LogicalOperator>(StringComparer.OrdinalIgnoreCase)
            {
                {"and",LogicalOperator.And },
                {"or",LogicalOperator.Or }
            };

            public static readonly IReadOnlyDictionary<string, DateTimeUnit> TIME_UNIT = new Dictionary<string, DateTimeUnit>(StringComparer.OrdinalIgnoreCase)
            {
                {"year", DateTimeUnit.Year},
                {"month", DateTimeUnit.Month},
                {"week", DateTimeUnit.Week},
                {"day", DateTimeUnit.Day},
                {"hour", DateTimeUnit.Hour},
                {"minute", DateTimeUnit.Minute},
                {"second", DateTimeUnit.Second},
            };

            public static readonly IReadOnlyDictionary<string, DayOfWeek> DAY_OF_WEEK = new Dictionary<string, DayOfWeek>(StringComparer.OrdinalIgnoreCase)
            {
                {"sunday", DayOfWeek.Sunday},
                {"monday", DayOfWeek.Monday},
                {"tuesday", DayOfWeek.Tuesday},
                {"wednesday", DayOfWeek.Wednesday},
                {"thursday", DayOfWeek.Thursday},
                {"friday", DayOfWeek.Friday},
                {"saturday", DayOfWeek.Saturday},
            };

            public static readonly IReadOnlyDictionary<string, DateSeasons> SEASONS = new Dictionary<string, DateSeasons>(StringComparer.OrdinalIgnoreCase)
            {
                {"spring", DateSeasons.Spring},
                {"summer", DateSeasons.Summer},
                {"autumn", DateSeasons.Autumn},
                {"winter", DateSeasons.Winter},
            };

            public static readonly IReadOnlyDictionary<string, int> MONTHS = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"january", 1},
                {"february", 2},
                {"march", 3},
                {"april", 4},
                {"may", 5},
                {"june", 6},
                {"july", 7},
                {"august", 8},
                {"september", 9},
                {"october", 10},
                {"november", 11},
                {"december", 12},
            };

            public static readonly IDictionary<string, CertaintyType> PROXIMITY = new Dictionary<string, CertaintyType>(StringComparer.OrdinalIgnoreCase)
            {
                {"approximate", CertaintyType.Aproximation},
                {"exact", CertaintyType.Certainty},
            };

            public static readonly IDictionary<string, TimeRepeater> REPERATER_EXPRESSIONS = new Dictionary<string, TimeRepeater>(StringComparer.OrdinalIgnoreCase)
            {
                {"secondly", TimeRepeater.Secondly},
                {"minutely", TimeRepeater.Minutely},
                {"hourly", TimeRepeater.Hourly},
                {"daily", TimeRepeater.Dayly},
                {"weekly", TimeRepeater.Weekly},
                {"monthly", TimeRepeater.Monthly},
                {"yearly", TimeRepeater.Yearly}
            };

            public static readonly IDictionary<string, TimeDurationExpression> DURATION_EXPRESSIONS = new Dictionary<string, TimeDurationExpression>(StringComparer.OrdinalIgnoreCase)
            {
                {"whole",TimeDurationExpression.Whole},
                {"half",TimeDurationExpression.Half},
                {"quater",TimeDurationExpression.Quater}
            };

            public static readonly IReadOnlyDictionary<string, int> NUMBERS = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"0", 0},
                {"1", 1},
                {"2", 2},
                {"3", 3},
                {"4", 4},
                {"5", 5},
                {"6", 6},
                {"7", 7},
                {"8", 8},
                {"9", 9}
            };

            public static readonly IReadOnlyDictionary<string, long> NUMERIC_MAGNITUDES_CARDINAL = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase)
            {
                {"hundred",  100},
                {"thousand", 1000},
                {"million",  1000000},
                {"billion",  1000000000},
                {"trillion", 1000000000000},
                {"quadrillion", 1000000000000000},
                {"quintillion", 1000000000000000000},
            };

            public static readonly IReadOnlyDictionary<string, long> NUMERIC_MAGNITUDES_ORDINAL = new Dictionary<string, long>(StringComparer.OrdinalIgnoreCase)
            {
                {"hundredth", 100},
                {"thousandth",1000},
                {"millionth", 1000000},
                {"billionth", 1000000000},
                {"trillionth", 1000000000000},
                {"quadrillionth", 1000000000000000},
                {"quintillionth", 1000000000000000000},
            };

            public static readonly IReadOnlyDictionary<string, int> NUMBERS_WORDS = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"one", 1},
                {"two", 2},
                {"three", 3},
                {"four", 4},
                {"five", 5},
                {"six", 6},
                {"seven", 7},
                {"eight", 8},
                {"nine", 9},
                {"ten", 10},
                {"eleven", 11},
                {"twelve", 12},
                {"thirteen", 13},
                {"fourteen", 14},
                {"fifteen", 15},
                {"sixteen", 16},
                {"seventeen", 17},
                {"eighteen", 18},
                {"nineteen", 19},
                {"twenty", 20},
                {"twenty one", 21},
                {"twenty two", 22},
                {"twenty three", 23},
                {"twenty four", 24},
                {"twenty five", 25},
                {"twenty six", 26},
                {"twenty seven", 27},
                {"twenty eight", 28},
                {"twenty nine", 29},
                {"thirty", 30},
                {"thirty one", 31},
            };

            public static readonly IReadOnlyDictionary<string, int> NUMBERS_WORDS_CARDINAL = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"one", 1},
                {"two", 2},
                {"three", 3},
                {"four", 4},
                {"five", 5},
                {"six", 6},
                {"seven", 7},
                {"eight", 8},
                {"nine", 9},
                {"ten", 10},
                {"eleven", 11},
                {"twelve", 12},
                {"thirteen", 13},
                {"fourteen", 14},
                {"fifteen", 15},
                {"sixteen", 16},
                {"seventeen", 17},
                {"eighteen", 18},
                {"nineteen", 19},
                {"twenty", 20},
                {"twenty one", 21},
                {"twenty two", 22},
                {"twenty three", 23},
                {"twenty four", 24},
                {"twenty five", 25},
                {"twenty six", 26},
                {"twenty seven", 27},
                {"twenty eight", 28},
                {"twenty nine", 29},
                {"thirty", 30},
                {"thirty one", 31},
                {"forty", 40},
                {"fifty", 50},
                {"sixty", 60},
                {"seventy", 70},
                {"eighty", 80},
                {"ninety", 90},

            };

            public static readonly IReadOnlyDictionary<string, int> NUMBERS_WORDS_ORDINAL = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"start",0 },
                {"first", 1},
                {"second", 2},
                {"third", 3},
                {"fourth", 4},
                {"fifth", 5},
                {"sixth", 6},
                {"seventh", 7},
                {"eighth", 8},
                {"ninth", 9},
                {"tenth", 10},
                {"eleventh", 11},
                {"twelfth", 12},
                {"thirteenth", 13},
                {"fourteenth", 14},
                {"fifteenth", 15},
                {"sixteenth", 16},
                {"seventeenth", 17},
                {"eighteenth", 18},
                {"nineteenth", 19},
                {"twentieth", 20},
                {"twenty first", 21},
                {"twenty second", 22},
                {"twenty third", 23},
                {"twenty fourth", 24},
                {"twenty fifth", 25},
                {"twenty sixth", 26},
                {"twenty seventh", 27},
                {"twenty eighth", 28},
                {"twenty ninth", 29},
                {"thirtieth", 30},
                {"thirty first", 31},
                {"fortieth", 40},
                {"fiftiety", 50},
                {"sixtieth", 60},
                {"seventieth", 70},
                {"eightieth", 80},
                {"ninetieth", 90},
                {"last", int.MaxValue }
            };

            public static readonly IReadOnlyDictionary<string, TimeSpan> TIMEZONE_OFFSETS = new Dictionary<string, TimeSpan>(StringComparer.OrdinalIgnoreCase)
            {
                {"A",TimeSpan.FromMinutes(60)},
                {"ACDT",TimeSpan.FromMinutes(630)},
                {"ACST",TimeSpan.FromMinutes(570)},
                {"ADT",TimeSpan.FromMinutes(-180)},
                {"AEDT",TimeSpan.FromMinutes(660)},
                {"AEST",TimeSpan.FromMinutes(600)},
                {"AFT",TimeSpan.FromMinutes(270)},
                {"AKDT",TimeSpan.FromMinutes(-480)},
                {"AKST",TimeSpan.FromMinutes(-540)},
                {"ALMT",TimeSpan.FromMinutes(360)},
                {"AMST",TimeSpan.FromMinutes(-180)},
                {"AMT",TimeSpan.FromMinutes(-240)},
                {"ANAST",TimeSpan.FromMinutes(720)},
                {"ANAT",TimeSpan.FromMinutes(720)},
                {"AQTT",TimeSpan.FromMinutes(300)},
                {"ART",TimeSpan.FromMinutes(-180)},
                {"AST",TimeSpan.FromMinutes(-240)},
                {"AWDT",TimeSpan.FromMinutes(540)},
                {"AWST",TimeSpan.FromMinutes(480)},
                {"AZOST",TimeSpan.FromMinutes(0)},
                {"AZOT",TimeSpan.FromMinutes(-60)},
                {"AZST",TimeSpan.FromMinutes(300)},
                {"AZT",TimeSpan.FromMinutes(240)},
                {"B",TimeSpan.FromMinutes(120)},
                {"BNT",TimeSpan.FromMinutes(480)},
                {"BOT",TimeSpan.FromMinutes(-240)},
                {"BRST",TimeSpan.FromMinutes(-120)},
                {"BRT",TimeSpan.FromMinutes(-180)},
                {"BST",TimeSpan.FromMinutes(60)},
                {"BTT",TimeSpan.FromMinutes(360)},
                {"C",TimeSpan.FromMinutes(180)},
                {"CAST",TimeSpan.FromMinutes(480)},
                {"CAT",TimeSpan.FromMinutes(120)},
                {"CCT",TimeSpan.FromMinutes(390)},
                {"CDT",TimeSpan.FromMinutes(-300)},
                {"CEST",TimeSpan.FromMinutes(120)},
                {"CET",TimeSpan.FromMinutes(60)},
                {"CHADT",TimeSpan.FromMinutes(825)},
                {"CHAST",TimeSpan.FromMinutes(765)},
                {"CKT",TimeSpan.FromMinutes(-600)},
                {"CLST",TimeSpan.FromMinutes(-180)},
                {"CLT",TimeSpan.FromMinutes(-240)},
                {"COT",TimeSpan.FromMinutes(-300)},
                {"CST",TimeSpan.FromMinutes(-360)},
                {"CVT",TimeSpan.FromMinutes(-60)},
                {"CXT",TimeSpan.FromMinutes(420)},
                {"ChST",TimeSpan.FromMinutes(600)},
                {"D",TimeSpan.FromMinutes(240)},
                {"DAVT",TimeSpan.FromMinutes(420)},
                {"E",TimeSpan.FromMinutes(300)},
                {"EASST",TimeSpan.FromMinutes(-300)},
                {"EAST",TimeSpan.FromMinutes(-360)},
                {"EAT",TimeSpan.FromMinutes(180)},
                {"ECT",TimeSpan.FromMinutes(-300)},
                {"EDT",TimeSpan.FromMinutes(-240)},
                {"EEST",TimeSpan.FromMinutes(180)},
                {"EET",TimeSpan.FromMinutes(120)},
                {"EGST",TimeSpan.FromMinutes(0)},
                {"EGT",TimeSpan.FromMinutes(-60)},
                {"EST",TimeSpan.FromMinutes(-300)},
                {"ET",TimeSpan.FromMinutes(-300)},
                {"F",TimeSpan.FromMinutes(360)},
                {"FJST",TimeSpan.FromMinutes(780)},
                {"FJT",TimeSpan.FromMinutes(720)},
                {"FKST",TimeSpan.FromMinutes(-180)},
                {"FKT",TimeSpan.FromMinutes(-240)},
                {"FNT",TimeSpan.FromMinutes(-120)},
                {"G",TimeSpan.FromMinutes(420)},
                {"GALT",TimeSpan.FromMinutes(-360)},
                {"GAMT",TimeSpan.FromMinutes(-540)},
                {"GET",TimeSpan.FromMinutes(240)},
                {"GFT",TimeSpan.FromMinutes(-180)},
                {"GILT",TimeSpan.FromMinutes(720)},
                {"GMT",TimeSpan.FromMinutes(0)},
                {"GST",TimeSpan.FromMinutes(240)},
                {"GYT",TimeSpan.FromMinutes(-240)},
                {"H",TimeSpan.FromMinutes(480)},
                {"HAA",TimeSpan.FromMinutes(-180)},
                {"HAC",TimeSpan.FromMinutes(-300)},
                {"HADT",TimeSpan.FromMinutes(-540)},
                {"HAE",TimeSpan.FromMinutes(-240)},
                {"HAP",TimeSpan.FromMinutes(-420)},
                {"HAR",TimeSpan.FromMinutes(-360)},
                {"HAST",TimeSpan.FromMinutes(-600)},
                {"HAT",TimeSpan.FromMinutes(-90)},
                {"HAY",TimeSpan.FromMinutes(-480)},
                {"HKT",TimeSpan.FromMinutes(480)},
                {"HLV",TimeSpan.FromMinutes(-210)},
                {"HNA",TimeSpan.FromMinutes(-240)},
                {"HNC",TimeSpan.FromMinutes(-360)},
                {"HNE",TimeSpan.FromMinutes(-300)},
                {"HNP",TimeSpan.FromMinutes(-480)},
                {"HNR",TimeSpan.FromMinutes(-420)},
                {"HNT",TimeSpan.FromMinutes(-150)},
                {"HNY",TimeSpan.FromMinutes(-540)},
                {"HOVT",TimeSpan.FromMinutes(420)},
                {"I",TimeSpan.FromMinutes(540)},
                {"ICT",TimeSpan.FromMinutes(420)},
                {"IDT",TimeSpan.FromMinutes(180)},
                {"IOT",TimeSpan.FromMinutes(360)},
                {"IRDT",TimeSpan.FromMinutes(270)},
                {"IRKST",TimeSpan.FromMinutes(540)},
                {"IRKT",TimeSpan.FromMinutes(540)},
                {"IRST",TimeSpan.FromMinutes(210)},
                {"IST",TimeSpan.FromMinutes(60)},
                {"JST",TimeSpan.FromMinutes(540)},
                {"K",TimeSpan.FromMinutes(600)},
                {"KGT",TimeSpan.FromMinutes(360)},
                {"KRAST",TimeSpan.FromMinutes(480)},
                {"KRAT",TimeSpan.FromMinutes(480)},
                {"KST",TimeSpan.FromMinutes(540)},
                {"KUYT",TimeSpan.FromMinutes(240)},
                {"L",TimeSpan.FromMinutes(660)},
                {"LHDT",TimeSpan.FromMinutes(660)},
                {"LHST",TimeSpan.FromMinutes(630)},
                {"LINT",TimeSpan.FromMinutes(840)},
                {"M",TimeSpan.FromMinutes(720)},
                {"MAGST",TimeSpan.FromMinutes(720)},
                {"MAGT",TimeSpan.FromMinutes(720)},
                {"MART",TimeSpan.FromMinutes(-510)},
                {"MAWT",TimeSpan.FromMinutes(300)},
                {"MDT",TimeSpan.FromMinutes(-360)},
                {"MESZ",TimeSpan.FromMinutes(120)},
                {"MEZ",TimeSpan.FromMinutes(60)},
                {"MHT",TimeSpan.FromMinutes(720)},
                {"MMT",TimeSpan.FromMinutes(390)},
                {"MSD",TimeSpan.FromMinutes(240)},
                {"MSK",TimeSpan.FromMinutes(240)},
                {"MST",TimeSpan.FromMinutes(-420)},
                {"MUT",TimeSpan.FromMinutes(240)},
                {"MVT",TimeSpan.FromMinutes(300)},
                {"MYT",TimeSpan.FromMinutes(480)},
                {"N",TimeSpan.FromMinutes(-60)},
                {"NCT",TimeSpan.FromMinutes(660)},
                {"NDT",TimeSpan.FromMinutes(-90)},
                {"NFT",TimeSpan.FromMinutes(690)},
                {"NOVST",TimeSpan.FromMinutes(420)},
                {"NOVT",TimeSpan.FromMinutes(360)},
                {"NPT",TimeSpan.FromMinutes(345)},
                {"NST",TimeSpan.FromMinutes(-150)},
                {"NUT",TimeSpan.FromMinutes(-660)},
                {"NZDT",TimeSpan.FromMinutes(780)},
                {"NZST",TimeSpan.FromMinutes(720)},
                {"O",TimeSpan.FromMinutes(-120)},
                {"OMSST",TimeSpan.FromMinutes(420)},
                {"OMST",TimeSpan.FromMinutes(420)},
                {"P",TimeSpan.FromMinutes(-180)},
                {"PDT",TimeSpan.FromMinutes(-420)},
                {"PET",TimeSpan.FromMinutes(-300)},
                {"PETST",TimeSpan.FromMinutes(720)},
                {"PETT",TimeSpan.FromMinutes(720)},
                {"PGT",TimeSpan.FromMinutes(600)},
                {"PHOT",TimeSpan.FromMinutes(780)},
                {"PHT",TimeSpan.FromMinutes(480)},
                {"PKT",TimeSpan.FromMinutes(300)},
                {"PMDT",TimeSpan.FromMinutes(-120)},
                {"PMST",TimeSpan.FromMinutes(-180)},
                {"PONT",TimeSpan.FromMinutes(660)},
                {"PST",TimeSpan.FromMinutes(-480)},
                {"PT",TimeSpan.FromMinutes(-480)},
                {"PWT",TimeSpan.FromMinutes(540)},
                {"PYST",TimeSpan.FromMinutes(-180)},
                {"PYT",TimeSpan.FromMinutes(-240)},
                {"Q",TimeSpan.FromMinutes(-240)},
                {"R",TimeSpan.FromMinutes(-300)},
                {"RET",TimeSpan.FromMinutes(240)},
                {"S",TimeSpan.FromMinutes(-360)},
                {"SAMT",TimeSpan.FromMinutes(240)},
                {"SAST",TimeSpan.FromMinutes(120)},
                {"SBT",TimeSpan.FromMinutes(660)},
                {"SCT",TimeSpan.FromMinutes(240)},
                {"SGT",TimeSpan.FromMinutes(480)},
                {"SRT",TimeSpan.FromMinutes(-180)},
                {"SST",TimeSpan.FromMinutes(-660)},
                {"T",TimeSpan.FromMinutes(-420)},
                {"TAHT",TimeSpan.FromMinutes(-600)},
                {"TFT",TimeSpan.FromMinutes(300)},
                {"TJT",TimeSpan.FromMinutes(300)},
                {"TKT",TimeSpan.FromMinutes(780)},
                {"TLT",TimeSpan.FromMinutes(540)},
                {"TMT",TimeSpan.FromMinutes(300)},
                {"TVT",TimeSpan.FromMinutes(720)},
                {"U",TimeSpan.FromMinutes(-480)},
                {"ULAT",TimeSpan.FromMinutes(480)},
                {"UTC",TimeSpan.FromMinutes(0)},
                {"UYST",TimeSpan.FromMinutes(-120)},
                {"UYT",TimeSpan.FromMinutes(-180)},
                {"UZT",TimeSpan.FromMinutes(300)},
                {"V",TimeSpan.FromMinutes(-540)},
                {"VET",TimeSpan.FromMinutes(-210)},
                {"VLAST",TimeSpan.FromMinutes(660)},
                {"VLAT",TimeSpan.FromMinutes(660)},
                {"VUT",TimeSpan.FromMinutes(660)},
                {"W",TimeSpan.FromMinutes(-600)},
                {"WAST",TimeSpan.FromMinutes(120)},
                {"WAT",TimeSpan.FromMinutes(60)},
                {"WEST",TimeSpan.FromMinutes(60)},
                {"WESZ",TimeSpan.FromMinutes(60)},
                {"WET",TimeSpan.FromMinutes(0)},
                {"WEZ",TimeSpan.FromMinutes(0)},
                {"WFT",TimeSpan.FromMinutes(720)},
                {"WGST",TimeSpan.FromMinutes(-120)},
                {"WGT",TimeSpan.FromMinutes(-180)},
                {"WIB",TimeSpan.FromMinutes(420)},
                {"WIT",TimeSpan.FromMinutes(540)},
                {"WITA",TimeSpan.FromMinutes(480)},
                {"WST",TimeSpan.FromMinutes(780)},
                {"WT",TimeSpan.FromMinutes(0)},
                {"X",TimeSpan.FromMinutes(-660)},
                {"Y",TimeSpan.FromMinutes(-720)},
                {"YAKST",TimeSpan.FromMinutes(600)},
                {"YAKT",TimeSpan.FromMinutes(600)},
                {"YAPT",TimeSpan.FromMinutes(600)},
                {"YEKST",TimeSpan.FromMinutes(360)},
                {"YEKT",TimeSpan.FromMinutes(360)},
                {"Z",TimeSpan.FromMinutes(0)},
            };
        }
    }
}
