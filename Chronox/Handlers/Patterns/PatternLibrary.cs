using Chronox.Constants;
using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chronox.Helpers.Patterns
{
    public class PatternLibrary
    {
        internal readonly Dictionary<string, PatternRegex> Patterns = new Dictionary<string, PatternRegex>(StringComparer.OrdinalIgnoreCase);

        internal static readonly IDictionary<string,PatternRegex> HelperPatterns = new Dictionary<string,PatternRegex>(StringComparer.OrdinalIgnoreCase)
        {
           { Definitions.Patterns.OptionalSpace, new PatternRegex(Definitions.Patterns.OptionalSpace, "\\s*\\,?\\s*") },

           { Definitions.Patterns.SpaceSeparator, new PatternRegex(Definitions.Patterns.SpaceSeparator, "\\s*\\,?\\s") },

           { Definitions.Patterns.TimeSeparator, new PatternRegex(Definitions.Patterns.TimeSeparator,  "(\\.|\\:|\\：)?")},

           { Definitions.Patterns.DateSeparator, new PatternRegex(Definitions.Patterns.DateSeparator,  "(\\/|\\-|\\s*)?")},

           { Definitions.Patterns.NumberMax2Digits, new PatternRegex(Definitions.Patterns.NumberMax2Digits, "([0-9]{1,2})")},

           { Definitions.Patterns.NumberMax4Digits, new PatternRegex(Definitions.Patterns.NumberMax4Digits, "([0-9]{2,4})")},

           { Definitions.Patterns.NumberMax5Digits, new PatternRegex(Definitions.Patterns.NumberMax5Digits, "([0-9][0-9]{0,5})")},

           { Definitions.Patterns.NumberRangedOrdinal, new PatternRegex(Definitions.Patterns.NumberRangedOrdinal, "^([3-3][1-9]|[4-9][0-9]|[1-9][0-9][0-9])(?:st|nd|rd|th)?$") },

           { Definitions.Patterns.WordStart, new PatternRegex(Definitions.Patterns.WordStart, "(?<=\\W|^)" )},

           { Definitions.Patterns.WordEnd, new PatternRegex(Definitions.Patterns.WordEnd, "(?=\\W|$)" )}
        };

        internal static readonly IDictionary<string, PatternRegex> CommonDatePatterns = new Dictionary<string, PatternRegex>(StringComparer.OrdinalIgnoreCase)
        {
           { Definitions.Patterns.YearDiscrete, new PatternRegex(Definitions.Patterns.YearDiscrete, "([0-9]{4})") },

           { Definitions.Patterns.MonthDiscrete, new PatternRegex(Definitions.Patterns.MonthDiscrete, "([0-9]{1,2})")},

           { Definitions.Patterns.DayDiscrete, new PatternRegex(Definitions.Patterns.DayDiscrete, "([0-9]{1,2})?")}
        };

        internal static readonly IDictionary<string,PatternRegex> CommonTimePatterns = new Dictionary<string,PatternRegex>(StringComparer.OrdinalIgnoreCase)
        {
           { Definitions.Patterns.Hour, new PatternRegex(Definitions.Patterns.Hour, "([0-9]{1,2})") },

           { Definitions.Patterns.Minute, new PatternRegex(Definitions.Patterns.Minute, "((\\.|\\:|\\：)([0-9]{1,2}))?")},

           { Definitions.Patterns.Second, new PatternRegex(Definitions.Patterns.Second, "((\\.|\\:|\\：)([0-9]{1,2}))?")},

           { Definitions.Patterns.HourDiscrete, new PatternRegex(Definitions.Patterns.HourDiscrete, "([0-9]{1,2})") },

           { Definitions.Patterns.MinuteDiscrete, new PatternRegex(Definitions.Patterns.MinuteDiscrete, "([0-9]{1,2})")},

           { Definitions.Patterns.SecondDiscrete, new PatternRegex(Definitions.Patterns.SecondDiscrete, "([0-9]{1,2})")},

           { Definitions.Patterns.Millis, new PatternRegex(Definitions.Patterns.Millis, "((?:\\.|\\:|\\：)([0-9]{1,4}))?")},
        };

        internal static readonly IDictionary<string, PatternRegex> CommonTimeZonePatterns = new Dictionary<string, PatternRegex>(StringComparer.OrdinalIgnoreCase)
        {
            { Definitions.Patterns.TimeZoneCode, new PatternRegex(Definitions.Patterns.TimeZoneCode,"(GMT|UTC)?")},

            { Definitions.Patterns.TimeZoneOffset, new PatternRegex(Definitions.Patterns.TimeZoneOffset,"(?:Z|[+-](?:2[0-3]|[01][0-9]):?[0-5][0-9])")},

            { Definitions.Patterns.TimeZoneAbbreviation, new PatternRegex(Definitions.Patterns.TimeZoneAbbreviation, $"({string.Join("|",Definitions.Converters.TIMEZONE_OFFSETS.Keys)})")},
        };

        internal static readonly IDictionary<string,PatternRegex> CommonYearPatterns = new Dictionary<string,PatternRegex>(StringComparer.OrdinalIgnoreCase)
        {
           { Definitions.Patterns.Year, new PatternRegex(Definitions.Patterns.Year, "([0-9]{4}(s)?|'[0-9]{2}(s)?)") },
        };
    }
}
