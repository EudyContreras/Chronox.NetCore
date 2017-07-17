using Chronox.Handlers.Models;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Constants.Banks
{
    internal class SequenceBank
    {
        public readonly List<Sequence> SequencesAllCombinations = new List<Sequence>();

        public readonly List<Sequence> SequencesDateTimeCombinations = new List<Sequence>();

        public readonly List<Sequence> SequencesRangeCombinations = new List<Sequence>();

        public readonly List<Sequence> SequencesDurationCombinations = new List<Sequence>();

        public readonly List<Sequence> SequencesRepeaterCombinations = new List<Sequence>();

        public static readonly IEnumerable<Sequence> DateTimeSequences = new List<Sequence>()
        {
            new Sequence(SequenceType.DateTime, "1900s",
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "the day",
                    Definitions.Property.DayUnit
                ),
            new Sequence(SequenceType.DateTime, "at noon | at midnight",
                    Definitions.Property.TimeExpressions
                ),
            new Sequence(SequenceType.DateTime, "4pm | 4:00:00 | 4:00 a.m",
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "10pm next tuesday",
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DaysOfWeek
                ),
            new Sequence(SequenceType.DateTime, "12:30 on friday",
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam,
                    Definitions.Property.DaysOfWeek
                ),
            new Sequence(SequenceType.DateTime, "10 to 8 | 10 past 8",
                    Definitions.Patterns.MinuteDiscrete,
                    Definitions.Property.TimeConjointer,
                    Definitions.Patterns.HourDiscrete,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "half past 2 | quater to 2 | half to 2",
                    Definitions.Property.TimeFractions,
                    Definitions.Property.TimeConjointer,
                    Definitions.Patterns.HourDiscrete,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "one thirty am",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "one oclock pm",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "6 in the morning | 8pm in the evening",
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "6 tuesday morning | 8pm on monday evening",
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "this sat the 7th in the evening |last monday the first in the morning", //Prone to error if day of month does not match the day of week!
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.NumericWord,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "sat 7 in the evening | monday 10:30 in the morning",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "next sat 7 in the evening | last monday 10:30 in the morning",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "sat 7am | saturday at 20:40.0000",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "this sat 7am | next saturday at 20:40.0000",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "yesterday | tomorrow | today | day after tomorrow | etc",
                    Definitions.Property.DayOffset
                ),
            new Sequence(SequenceType.DateTime, "afternoon yesterday || evening yesterday", //Should it be supported by default!
                    Definitions.Property.TimeOfDay,
                    Definitions.Property.DayOffset
                ),
            new Sequence(SequenceType.DateTime, "yesterday afternoon || tomorrow evening",
                    Definitions.Property.DayOffset,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "last week | next month | this year",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.DateTime, "thursday | this thursday | next thrurday | last thursday ",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DaysOfWeek
                ),
            new Sequence(SequenceType.DateTime, "next monday the third",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.NumericWordOrdinal
                ),
            new Sequence(SequenceType.DateTime, "monday the third",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.NumericWordOrdinal
                ),
            new Sequence(SequenceType.DateTime, "november",
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "this november | last december",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "last winter | this winter | spring",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.SeasonOfYear
                ),
            new Sequence(SequenceType.DateTime, "morning",
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "this morning | afternoon | night",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "last night | right now | tonight",
                    Definitions.Property.InterpretedExpression
                ),
            new Sequence(SequenceType.DateTime, "the third week of december | the third day of july",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateUnits,
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "the third week of this year | the third week of this month",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateUnits,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateUnits
                ),
            new Sequence(SequenceType.DateTime, "the third day of the week",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateUnits,
                    Definitions.Property.DateUnits
                ),
            new Sequence(SequenceType.DateTime, "the third week of 2010",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateUnits,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "yesterday at 4:00 | tomorrow at 10pm",
                    Definitions.Property.DayOffset,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "last week on tuesday | next week on monday",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.WeekUnit,
                    Definitions.Property.DaysOfWeek
                ),
            new Sequence(SequenceType.DateTime, "last week on tuesday afternoon | next week on monday morning",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.WeekUnit,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "thursday last week | monday next week",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.WeekUnit
                ),
            new Sequence(SequenceType.DateTime, "the day after tuesday",
                    Definitions.Property.DayUnit,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.DaysOfWeek
                ),
            new Sequence(SequenceType.DateTime, "the day after next twesday",
                    Definitions.Property.DayUnit,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DaysOfWeek
                ),
            new Sequence(SequenceType.DateTime, "the day after next month",
                    Definitions.Property.DateUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateUnits
                ),
            new Sequence(SequenceType.DateTime, "last day of the following month",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateUnits,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateUnits
                ),
            new Sequence(SequenceType.DateTime, "last day of the following month at 10pm",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateUnits,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateUnits,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "last day of the following month at noon",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateUnits,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateUnits,
                    Definitions.Property.TimeExpressions
                ),
            new Sequence(SequenceType.DateTime, "last monday of the month",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.DateUnits
                ),
            new Sequence(SequenceType.DateTime, "last monday of the previous month",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateUnits
                ),
            new Sequence(SequenceType.DateTime, "last monday of the previous month at 10pm",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateUnits,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "last monday of the previous month at midnight",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateUnits,
                    Definitions.Property.TimeExpressions
                ),
            new Sequence(SequenceType.DateTime, "second friday of october",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.MonthsOfYear
                ),

            new Sequence(SequenceType.DateTime, "second friday of next october",
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "week third of december",
                    Definitions.Property.DateUnits,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "week third of next december",
                    Definitions.Property.DateUnits,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "third of june | fifth of may",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.DateTime, "third of june 2017 | fifth of may 2017",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "third of june 2017 at 10 PM | fifth of may 2017 at 20:00",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time
                ),
            new Sequence(SequenceType.DateTime, "third of june 2017 at 10 PM | fifth of may 2017 at 20:00",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time
                ),
            new Sequence(SequenceType.DateTime, "sunday november 26th",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.NumericWord
                ),
            new Sequence(SequenceType.DateTime, "sunday november 26",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.NumberMax2Digits
                ),
            new Sequence(SequenceType.DateTime, "sunday november 26 in 2017",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "sunday november 26th in 2017",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.NumericWord,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "december 31",
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.NumberMax2Digits
                ),
            new Sequence(SequenceType.DateTime, "31 december",
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "31st of december",
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "december 31, 2017",
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "31 december 2017",
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "december 2017",
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "december 31st | january twenty-third",
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.NumericWord
                ),
            new Sequence(SequenceType.DateTime, "twenty third of june | 31st of december",
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear
                ),
           new Sequence(SequenceType.DateTime, "december 31st 2017 | january twenty-third 2017",
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.NumericWord,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "twenty third of june 2017| 31st of december 2017",
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "monday the 3rd of June of the year 2017 at 20pm",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "monday the 3rd of June 2017 at 20pm",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "monday the 3rd of June 2017",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "monday the 3 of June 2017",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "the 3rd of June 2017 at 10pm",
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "june third 2017 at 10pm",
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.NumericWord,                 
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "the 3 of June 2017 at 10pm",
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "the 3 of June 2017",
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "the 31st of december of the year 2017",
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.YearUnit,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "the 31 of december of the year 2017",
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.YearUnit,
                    Definitions.Patterns.Year
                ),
            new Sequence(SequenceType.DateTime, "fourteenth of june 2010 at eleven o'clock in the evening",
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "may seventh '97 at three in the morning",
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.NumericWord,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "the thirty first of december of the year 2017 at 12 am",
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.YearUnit,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "the 31 of december of the year 2017 at 10:31 pm",
                    Definitions.Patterns.NumberMax2Digits,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Property.YearUnit,
                    Definitions.Patterns.Year,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "4 weeks | 3 hours",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.DateTime, "4 weeks from today | 3 days from today",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.DayOffset
                ),
            new Sequence(SequenceType.DateTime, "4 weeks from now | 3 days from now",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer
                ),
             new Sequence(SequenceType.DateTime, "four weeks from now | three days from now",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer
                ),
             new Sequence(SequenceType.DateTime, "five weeks ago on saturday",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.DaysOfWeek
                ),
             new Sequence(SequenceType.DateTime, "saturday five week ago",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer
                ),
             new Sequence(SequenceType.DateTime, "ten months ago on saturday at 6pm", //Is that valid?
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.Time
                ),
             new Sequence(SequenceType.DateTime, "4 days before next week",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateTimeUnits
                ),
             new Sequence(SequenceType.DateTime, "4 weeks ago on saturday",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.DaysOfWeek
                ),
             new Sequence(SequenceType.DateTime, "saturday 4 weeks ago",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer                    
                ),
             new Sequence(SequenceType.DateTime, "first friday in 2 months", //Is that valid?
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.MonthUnit
                ),
             new Sequence(SequenceType.DateTime, "first friday in 2 years", //Is that valid?
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.YearUnit
                ),
             new Sequence(SequenceType.DateTime, "in two months on the first friday", //Is that valid?
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateUnits,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DaysOfWeek
                ),
             new Sequence(SequenceType.DateTime, "the month after the next", //Is that valid?
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.GrabberExpressions
                ),
             new Sequence(SequenceType.DateTime, "the monday after the next", //Is that valid?
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.GrabberExpressions
                ),
             new Sequence(SequenceType.DateTime, "third week of next july", //Is that valid?
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.MonthsOfYear
                ),
             new Sequence(SequenceType.DateTime, "third week of next july at 10:00", //Is that valid?
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.MonthsOfYear,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
             new Sequence(SequenceType.DateTime, "4 months ago on saturday at 6pm", //Is that valid?
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
             new Sequence(SequenceType.DateTime, "saturday 4 months ago at 6pm", //Is that valid?
                    Definitions.Property.DaysOfWeek,
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer,                   
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "2 hours before noon",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.TimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.TimeExpressions
                ),
            new Sequence(SequenceType.DateTime, "2 hours before tomorrow",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.TimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.DayOffset
                ),
            new Sequence(SequenceType.DateTime, "2 hours before monday",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.TimeUnits,
                    Definitions.Property.TimeConjointer,
                    Definitions.Property.DaysOfWeek
                ),
            new Sequence(SequenceType.DateTime, "2 fridays from now | 3 saturdays ago",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.TimeConjointer
                ),
            new Sequence(SequenceType.DateTime, "the monday after | the tuesday before",
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions
                ),
            new Sequence(SequenceType.DateTime, "next monday evening",
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "the day before",
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.GrabberExpressions
                ),
            new Sequence(SequenceType.DateTime, "fifth of may",
                    Definitions.Property.NumericWord,
                    Definitions.Property.MonthsOfYear
                ),
            new Sequence(SequenceType.DateTime, "2 hours ago",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeConjointer
                ),
            new Sequence(SequenceType.DateTime, "tonight at 10pm",
                    Definitions.Property.InterpretedExpression,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "2017-10-23 at 10:pm",
                    Definitions.Patterns.DateBigEndian,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "10-23-2017 at 10:pm",
                    Definitions.Patterns.DateLittleEndian,
                    Definitions.Patterns.Time,
                    Definitions.Property.TimeMeridiam
                ),
            new Sequence(SequenceType.DateTime, "2017-10-23",
                    Definitions.Patterns.DateBigEndian
                ),
            new Sequence(SequenceType.DateTime, "23-10-2017",
                    Definitions.Patterns.DateLittleEndian
                ),
                    
        };

        public static readonly IEnumerable<Sequence> RepeaterSequences = new List<Sequence>()
        {
            new Sequence(SequenceType.Repeater, "every other day | every other week | every year | every week",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Repeater, "every monday | every sunday",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.DaysOfWeek
                ),
            new Sequence(SequenceType.Repeater, "every afternoon | every morning",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.Repeater, "daily | weekly | monthly ",
                    Definitions.Property.RepeaterExpressions
                ),
            new Sequence(SequenceType.Repeater, "every 2 days | every 30 seconds",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Repeater, "every first day | every 2nd month",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Repeater, "every first day of the year | every fifth day of the year",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DayUnit,
                    Definitions.Property.YearUnit
                ),
            new Sequence(SequenceType.Repeater, "every first day of the month | every 2nd day of the month",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DayUnit,
                    Definitions.Property.MonthUnit
                ),
            new Sequence(SequenceType.Repeater, "every first day of the week | every 2nd day of the week",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DayUnit,
                    Definitions.Property.WeekUnit
                ),
            new Sequence(SequenceType.Repeater, "every first week of the month | every 2nd week of the month",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.WeekUnit,
                    Definitions.Property.MonthUnit
                ),
            new Sequence(SequenceType.Repeater, "every first week of the year | every 2nd week of the year",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.WeekUnit,
                    Definitions.Property.YearUnit
                ),
            new Sequence(SequenceType.Repeater, "every first month of the year| every 2nd month of the year",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.MonthUnit,
                    Definitions.Property.YearUnit
                ),
            new Sequence(SequenceType.Repeater, "every first day of the month| every 2nd day of the month",
                    Definitions.Property.RepeaterIndicators,
                    Definitions.Property.NumericWord,
                    Definitions.Property.WeekUnit,
                    Definitions.Property.YearUnit
                ),

        };

        public static readonly IEnumerable<Sequence> DurationSequences = new List<Sequence>()
        {        
            new Sequence(SequenceType.Duration, "for aproximately five seconds | for exactly ten years",
                    Definitions.Property.DurationIndicators,
                    Definitions.Property.Proximity,
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Duration, "for aproximately 5 seconds | for exactly 10 years",
                    Definitions.Property.DurationIndicators,
                    Definitions.Property.Proximity,
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Duration, "five seconds long | ten years long",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Duration, "5 seconds long | 5 years long",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Duration, "five decades | five millenniums",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.TimePeriods
                ),
            new Sequence(SequenceType.Duration, "5 decades | 5 millenniums",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.TimePeriods
                ),
            new Sequence(SequenceType.Duration, "a half a minute | two quaters of two minutes",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DurationExpressions,
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.TimePeriods
                ),
            new Sequence(SequenceType.Duration, "a half minute | a quater minute",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DurationExpressions,
                    Definitions.Property.TimePeriods
                ),
            new Sequence(SequenceType.Duration, "one fifth of a second | two thirds of a year",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.NumericWordOrdinal,
                    Definitions.Property.DurationExpressions,
                    Definitions.Property.TimePeriods
                ),
            new Sequence(SequenceType.Duration, "45 minutes and 3 seconds",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.LogicalOperator,
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Duration, "forty five minutes and three seconds",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.LogicalOperator,
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Duration, "45 minutes and three seconds",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.LogicalOperator,
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Duration, "forty five minutes and 3 seconds",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.LogicalOperator,
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits
                ),
            new Sequence(SequenceType.Duration, "a week | a month | two weeks",
                    Definitions.Property.NumericWordCardinal,
                    Definitions.Property.DateTimeUnits
                ),
        };

        public static readonly IEnumerable<Sequence> TimeRangeSequences = new List<Sequence>()
        {

        };

        public static readonly IEnumerable<Sequence> ComplexTestSequences = new List<Sequence>()
        {
            new Sequence(SequenceType.DateTime, "first friday of the following month at noon",
                    Definitions.Property.NumericWord,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.GrabberExpressions,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.TimeExpressions
                ),
            new Sequence(SequenceType.DateTime, "in 2 months on the first friday in the afternoon",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.NumericWord,
                    Definitions.Property.DaysOfWeek,
                    Definitions.Property.TimeOfDay
                ),
            new Sequence(SequenceType.DateTime, "in 2 months on the first friday",
                    Definitions.Patterns.NumberMax5Digits,
                    Definitions.Property.DateTimeUnits,
                    Definitions.Property.NumericWord,
                    Definitions.Property.DaysOfWeek
                ),         
        };
    }
}
