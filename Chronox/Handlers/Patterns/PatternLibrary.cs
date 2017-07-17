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
    internal class PatternLibrary
    {

        internal readonly Dictionary<string, RegexPattern> Patterns = new Dictionary<string, RegexPattern>(StringComparer.OrdinalIgnoreCase);

        internal static readonly IDictionary<string,RegexPattern> HelperPatterns = new Dictionary<string,RegexPattern>(StringComparer.OrdinalIgnoreCase)
        {
           { Definitions.Patterns.OptionalSpace,  new RegexPattern(Definitions.Patterns.OptionalSpace, "\\s*\\,?\\s*") },

           { Definitions.Patterns.SpaceSeparator,  new RegexPattern(Definitions.Patterns.SpaceSeparator, "\\s*\\,?\\s") },

           { Definitions.Patterns.TimeSeparator, new RegexPattern(Definitions.Patterns.TimeSeparator,  "(\\.|\\:|\\：)?")},

           { Definitions.Patterns.DateSeparator, new RegexPattern(Definitions.Patterns.DateSeparator,  "(\\/|\\-|\\.|\\s*)?")},

           { Definitions.Patterns.NumberMax2Digits, new RegexPattern(Definitions.Patterns.NumberMax2Digits, "([0-9]{1,2})")},

           { Definitions.Patterns.NumberMax4Digits, new RegexPattern(Definitions.Patterns.NumberMax4Digits, "([0-9]{2,4})")},

           { Definitions.Patterns.NumberMax5Digits, new RegexPattern(Definitions.Patterns.NumberMax5Digits, "([0-9][0-9]{0,5})")},

           { Definitions.Patterns.WordStart, new RegexPattern(Definitions.Patterns.WordStart, "(?<=\\W|^)" )},

           { Definitions.Patterns.WordEnd,  new RegexPattern(Definitions.Patterns.WordEnd, "(?=\\W|$)" )}
        };

        internal static readonly IDictionary<string, RegexPattern> CommonDatePatterns = new Dictionary<string, RegexPattern>(StringComparer.OrdinalIgnoreCase)
        {
           { Definitions.Patterns.YearDiscrete, new RegexPattern(Definitions.Patterns.YearDiscrete, "([0-9]{4})") },

           { Definitions.Patterns.MonthDiscrete, new RegexPattern(Definitions.Patterns.MonthDiscrete, "([0-9]{1,2})")},

           { Definitions.Patterns.DayDiscrete, new RegexPattern(Definitions.Patterns.DayDiscrete, "([0-9]{1,2})?")}
        };

        internal static readonly IDictionary<string,RegexPattern> CommonTimePatterns = new Dictionary<string,RegexPattern>(StringComparer.OrdinalIgnoreCase)
        {
           { Definitions.Patterns.Hour, new RegexPattern(Definitions.Patterns.Hour, "([0-9]{1,2})") },

           { Definitions.Patterns.Minute, new RegexPattern(Definitions.Patterns.Minute, "((\\.|\\:|\\：)([0-9]{1,2}))?")},

           { Definitions.Patterns.Second, new RegexPattern(Definitions.Patterns.Second, "((\\.|\\:|\\：)([0-9]{1,2}))?")},

           { Definitions.Patterns.HourDiscrete, new RegexPattern(Definitions.Patterns.HourDiscrete, "([0-9]{1,2})") },

           { Definitions.Patterns.MinuteDiscrete, new RegexPattern(Definitions.Patterns.MinuteDiscrete, "([0-9]{1,2})")},

           { Definitions.Patterns.SecondDiscrete, new RegexPattern(Definitions.Patterns.SecondDiscrete, "([0-9]{1,2})")},

           { Definitions.Patterns.Millis, new RegexPattern(Definitions.Patterns.Millis, "((?:\\.|\\:|\\：)([0-9]{1,4}))?")},

           { Definitions.Patterns.ZoneOffset, new RegexPattern(Definitions.Patterns.ZoneOffset,"(?:Z|([+-]\\d{2}):?(\\d{2})?)?")},

        };

        internal static readonly IDictionary<string,RegexPattern> CommonYearPatterns = new Dictionary<string,RegexPattern>(StringComparer.OrdinalIgnoreCase)
        {
           { Definitions.Patterns.Year, new RegexPattern(Definitions.Patterns.Year, "([0-9]{4}(s)?|'[0-9]{2}(s)?)") },
        };    
    }
}
