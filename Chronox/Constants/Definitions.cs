using Chronox.Components;
using Chronox.Helpers;
using Chronox.Helpers.Offsets;
using Chronox.Utilities.Extenssions;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Constants
{
    internal static class Definitions
    {
        public const string TextLangDataPath = @"Resources\Languages\DataSets\TextFiles";
        public const string JsonLangDataPath = @"Resources\Languages\DataSets\JsonFiles";
        public const string TimeZoneFilePath = @"Resources\DataSets\TimeZoneInfo.txt";
        public const string FilePathDataSets = @"Resources\DataSets";

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

            public static readonly IReadOnlyList<string> CombinedProperties = Property.Dynamic.Values.Union(Patterns.Static.Values).ToList();
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
            public const string DayOfWeekType = "dayOfWeekType";

            public static readonly IReadOnlyDictionary<string, string> Dynamic = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {LogicalOperator,LogicalOperator},
                {ArithmeticOperator,ArithmeticOperator},
                {CasualExpressions,CasualExpressions},
                {GrabberExpressions,GrabberExpressions},
                {TimeExpressions,TimeExpressions},
                {TimeFractions,TimeFractions},
                {TimeConjointer,TimeConjointer},
                {InterpretedExpression,InterpretedExpression},
                {RangeIndicator,RangeIndicator},
                {RangeSeparator,RangeSeparator},
                {DecadeValues,DecadeValues},
                {TimePeriods,TimePeriods},
                {DayOffset,DayOffset},
                {Holidays,Holidays},
                {TimeOfDay,TimeOfDay},
                {DaysOfWeek,DaysOfWeek},
                {SeasonOfYear,SeasonOfYear},
                {MonthsOfYear,MonthsOfYear},
                {DateTimeUnits,DateTimeUnits },
                {NumericValue,NumericValue},
                {NumericWord,NumericWord},
                {NumericWordOrdinal,NumericWordOrdinal},
                {NumericWordCardinal,NumericWordCardinal},
                {TimeMeridiam,TimeMeridiam},
                {DateUnits,DateUnits},
                {TimeUnits,TimeUnits},
                {YearUnit,YearUnit},
                {MonthUnit,MonthUnit},
                {WeekUnit,WeekUnit},
                {DayUnit,DayUnit},
                {HourUnit,HourUnit},
                {MinuteUnit,MinuteUnit},
                {SecondUnit,SecondUnit},
                {Proximity,Proximity},
                {RepeaterExpressions,RepeaterExpressions},
                {DurationExpressions,DurationExpressions},
                {RepeaterIndicators,RepeaterIndicators},
                {DurationIndicators,DurationIndicators},
                {NumericMagnitudeCardinal,NumericMagnitudeCardinal},
                {NumericMagnitudeOrdinal,NumericMagnitudeOrdinal},
                {DayOfWeekType,DayOfWeekType},
            };
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
            public const string Millis = "dependentMilliseconds";
            public const string HourDiscrete = "discreteHour";
            public const string MinuteDiscrete = "discreteMinute";
            public const string SecondDiscrete = "discreteSecond";
            public const string TimeZone = "timeZone";
            public const string TimeZoneOffset = "timeZoneOffset";
            public const string TimeZoneAbbreviation = "timeZoneAbreviation";
            public const string TimeZoneCode = "timeZoneCode";

            public static readonly IReadOnlyDictionary<string, string> Static = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                {DateBigEndian,DateBigEndian},
                {DateLittleEndian,DateLittleEndian},
                {DateMiddleEndian,DateMiddleEndian},
                {SpaceSeparator,SpaceSeparator},
                {OptionalSpace,OptionalSpace},
                {SpaceRemover,SpaceRemover},
                {TimeSeparator,TimeSeparator},
                {DateSeparator,DateSeparator},
                {NumberMax2Digits,NumberMax2Digits},
                {NumberMax4Digits,NumberMax4Digits},
                {NumberMax5Digits,NumberMax5Digits},
                {WordStart,WordStart},
                {WordEnd,WordEnd},
                {Date,Date},
                {Time,Time},
                {Year,Year},
                {YearDiscrete,YearDiscrete},
                {MonthDiscrete,MonthDiscrete},
                {DayDiscrete,DayDiscrete},
                {Hour,Hour},
                {Minute,Minute},
                {Second,Second},
                {HourDiscrete,HourDiscrete},
                {MinuteDiscrete,MinuteDiscrete},
                {SecondDiscrete,SecondDiscrete},
                {Millis,Millis},
                {TimeZone, TimeZone},
                {TimeZoneOffset, TimeZoneOffset},
                {TimeZoneAbbreviation, TimeZoneAbbreviation},
                {TimeZoneCode, TimeZoneCode},

            };
        }

        public static class Converters
        {
            private static IReadOnlyDictionary<string, ChronoxTimeZone> timeZones = null;

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
                {"D.W.T",  Property.DaysOfWeek},
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
                {"N.M.2.D",Patterns.NumberMax2Digits},
                {"N.M.4.D",Patterns.NumberMax4Digits},
                {"N.M.5.D",Patterns.NumberMax5Digits},
            };

            public static readonly IReadOnlyDictionary<string, RangeWrapper> TIME_OF_DAY = new Dictionary<string, RangeWrapper>(StringComparer.OrdinalIgnoreCase)
            {
                {"morning", RangeConstants.MORNING_RANGE},
                {"afternoon", RangeConstants.AFTERNOON_RANGE},
                {"evening", RangeConstants.EVENING_RANGE},
                {"night", RangeConstants.NIGHT_RANGE},
            };

            public static readonly IReadOnlyDictionary<string, int> DAY_OFFSET = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                {"today", 0},
                {"yesterday", -1},
                {"tomorrow", 1},
                {"day after tomorrow",2},
                {"day before yesterday", -2},
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
                {"tonight",DateTimeExpression.Tonight },
                {"last night",DateTimeExpression.LastNight },
                {"now", DateTimeExpression.Now },
            };

            public static readonly IReadOnlyDictionary<string, ChronoxTime> TIME_EXPRESSION = new Dictionary<string, ChronoxTime>(StringComparer.OrdinalIgnoreCase)
            {
                {"midday",new ChronoxTime(12,0,0)},
                {"midnight", new ChronoxTime(0,0,0) },
            };

            public static readonly IReadOnlyDictionary<string, DayOfWeekType> WEEK_DAY_TYPE = new Dictionary<string, DayOfWeekType>(StringComparer.OrdinalIgnoreCase)
            {
                {"weekday",DayOfWeekType.Weekday},
                {"weekend", DayOfWeekType.Weekend},
            };

            public static readonly IReadOnlyDictionary<string, TimeOfDay> JULIAN_DAY = new Dictionary<string, TimeOfDay>(StringComparer.OrdinalIgnoreCase)
            {
                {"sunrise",TimeOfDay.Sunrise},
                {"sunset", TimeOfDay.Sunset},
            };

            public static readonly IReadOnlyDictionary<string, TimeRelation> RELATION_EXPRESSION = new Dictionary<string, TimeRelation>(StringComparer.OrdinalIgnoreCase)
            {
                {"last",TimeRelation.Past },
                {"next",TimeRelation.Future },
                {"this",TimeRelation.Present },
            };

            public static readonly IDictionary<string, DateCasualExpression> CASUAL_EXPRESSION = new Dictionary<string, DateCasualExpression>(StringComparer.OrdinalIgnoreCase)
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

            public static readonly IReadOnlyDictionary<string, DateTimeUnit> DATE_TIME_UNIT = new Dictionary<string, DateTimeUnit>(StringComparer.OrdinalIgnoreCase)
            {
                {"year", DateTimeUnit.Year},
                {"month", DateTimeUnit.Month},
                {"week", DateTimeUnit.Week},
                {"day", DateTimeUnit.Day},
                {"hour", DateTimeUnit.Hour},
                {"minute", DateTimeUnit.Minute},
                {"second", DateTimeUnit.Second},
                {"millisecond", DateTimeUnit.Millisecond },
                {"microsecond", DateTimeUnit.Microsecond},
                {"nanosecond", DateTimeUnit.Nanosecond},
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

            /*Ways to express Time Zones
            /* 
             * - 10:15 pm GMT+0000 (GMT)
             * - 10:15 pm GMT+0900 (JST)
             * - 10:15 pm +0500
             * - 10:15 pm +05:00
             * - 10:15 pm UTC-05:00
             * - 10:15 pm EST
             */

            /*
            Console.WriteLine("Count: " + Definitions.Converters.TIMEZONE_OFFSETS.Count);
            Console.WriteLine("Local TimeZone ID: "+ TimeZoneInfo.Local.Id);
            Console.WriteLine("Local TimeZone Name: " +TimeZoneInfo.Local.DisplayName);
            Console.WriteLine("Local Date UTC Offset: "+TimeZoneInfo.Local.GetUtcOffset(DateTime.Now));
            Console.WriteLine("Local Date: " + DateTime.Now);
            Console.WriteLine("New York Time: " +ChronoxDateTimeUtility.AddOffset(DateTime.Now, Definitions.Converters.TIMEZONE_OFFSETS["EDT"].Offset));
            Console.WriteLine("Australia Time: " + ChronoxDateTimeUtility.AddOffset(DateTime.Now, Definitions.Converters.TIMEZONE_OFFSETS["AEST"].Offset));
            */
            public static IReadOnlyDictionary<string, ChronoxTimeZone> TIMEZONE_OFFSETS
            {
                get
                {
                    if(timeZones == null)
                    {
                        timeZones = BuildTimeZones();

                        return timeZones;
                    }
                    else
                    {
                        return timeZones;
                    }
                }
            }
   
            public static Dictionary<string, ChronoxTimeZone> BuildTimeZones()
            {
                var lines = File.ReadAllLines(TimeZoneFilePath);

                var newLines = new Dictionary<string,ChronoxTimeZone>();

                foreach(var line in lines)
                {
                    var parts = line.Split('|');

                    var abbre = parts[0].Trim();

                    var offset = parts[1].Trim();

                    var name = parts[2].Trim();

                    var utc = parts[3].Trim();

                    var span = TimeSpan.FromMinutes(int.Parse(offset));

                    var timeZone = new ChronoxTimeZone
                    {
                        Abbreviation = abbre,
                        Offset = span,
                        StandardName = name,
                        UtcOffset = utc,
                    };

                    newLines.Add(abbre, timeZone);
                }

                return newLines;
            }
        }
    }
}
