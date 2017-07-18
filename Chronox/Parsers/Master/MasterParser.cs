﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Chronox.Wrappers;
using Chronox.Utilities;
using Chronox.Handlers;
using Chronox.Constants;
using Enumerations;
using Chronox.Helpers.Interpreters;
using Chronox.Helpers;
using Chronox.Utilities.Extenssions;
using Chronox.Abstractions;
using Chronox.Parsers.General.ParserHelpers;
using Chronox.Interfaces;

namespace Chronox.Parsers.English
{
    internal class MasterParser : AbstractParser
    {
        private IChronoxParseHelper helper = new DateTimeHelper(null);

        protected override void Extract(string text, DateTime refDate, RegexSequence sequence, Match match, ChronoxSettings settings, ChronoxBuildInformation information, ref ChronoxDateTimeExtraction result)
        {
            var dateTime = ChronoxDateTimeUtility.CreateDateTime(refDate);
          
            if(result == null)
            {
                result = new ChronoxDateTimeExtraction(settings, dateTime, match.Index, match.Value, text);
            }

            switch (sequence.SequenceType)
            {
                case SequenceType.DateTime:
                    helper = new DateTimeHelper(this);
                    break;
                case SequenceType.DateRange:
                    helper = new TimeRangeHelper();
                    break;
                case SequenceType.Duration:
                    helper = new DurationHelper();
                    break;
                case SequenceType.Repeater:
                    helper = new RepeaterHelper();
                    break;
            }

            var now = settings.ReferencDate;

            result = AssignGroups(match, ref dateTime, result, information, settings);

            result.GetCurrent().NormalizeDateValues(now, dateTime, information.Settings);
            result.GetCurrent().NormalizeTimeValues(now, dateTime, information.Settings);

            information.DateTime = dateTime;
        }

        private ChronoxDateTimeExtraction AssignGroups(Match match, ref DateTime dateTime, ChronoxDateTimeExtraction result, ChronoxBuildInformation information, ChronoxSettings settings)
        {
            var matches = new HashSet<GroupWrapper>();

            foreach(var property in Definitions.Properties.Dynamic.Union(Definitions.Properties.Static))
            {
                var group = match.Groups[property];

                foreach(object capture in group.Captures)
                {
                    if(capture is  Group)
                    {
                        matches.Add(new GroupWrapper((Group)capture));
                    }
                    else if(capture is Capture)
                    {
                        var cap = new GroupWrapper((Capture)capture);

                        cap.Name = group.Name;
                        cap.Group = group;

                        matches.Add(cap);
                    }
                }
            }
            var filterd = matches.Where(m => !string.IsNullOrEmpty(m.Value) && !string.IsNullOrWhiteSpace(m.Value));

            var ordered = filterd.OrderBy(g => g.Index).ThenByDescending(g => g.Value.Length).ToList();

            return ComputeInstructions(ordered, ref dateTime, result, information, settings);
        }

        private ChronoxDateTimeExtraction ComputeInstructions(List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeExtraction result, ChronoxBuildInformation information, ChronoxSettings settings)
        {
            var date = dateTime;

            foreach (var group in foundGroups)
            {
                if (group.GroupUsed)
                {
                    continue;
                }

                information.CurrentGroup = group;

                if (group.Name == Definitions.Property.CasualExpressions)
                {
                    var casualExpression = ConversionHandler.CasualExpression(settings, group.Value.Trim());

                    helper.ProcessCasualExpression(result, foundGroups, ref dateTime, information, casualExpression);
                }
                else if (group.Name == Definitions.Property.GrabberExpressions)
                {
                    var grabberExpression = ConversionHandler.GrabberExpression(settings, group.Value.Trim());

                    helper.ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabberExpression);
                }
                else if (group.Name == Definitions.Property.InterpretedExpression)
                {
                    var interpretedExpression = ConversionHandler.InterpretedExpression(settings, group.Value.Trim());

                    helper.ProcessInterpretedExpression(result, foundGroups, ref dateTime, information, interpretedExpression);
                }
                else if (group.Name == Definitions.Property.RepeaterIndicators)
                {
                    var repeaterIndicator = ConversionHandler.RepeaterIndicator(settings, group.Value.Trim());

                    helper.ProcessRepeaterIndicator(result, foundGroups, ref dateTime, information, repeaterIndicator);
                }
                else if (group.Name == Definitions.Property.RepeaterExpressions)
                {
                    var repeaterExpression = ConversionHandler.RepeaterExpression(settings, group.Value.Trim());

                    helper.ProcessRepeaterExpression(result, foundGroups, ref dateTime, information, repeaterExpression);
                }
                else if (group.Name == Definitions.Property.DurationIndicators)
                {
                    var durationIndicator = ConversionHandler.DurationIndicator(settings, group.Value.Trim());

                    helper.ProcessDurationIndicator(result, foundGroups, ref dateTime, information, durationIndicator);
                }
                else if (group.Name == Definitions.Property.DurationExpressions)
                {
                    var durationExpression = ConversionHandler.DurationExpression(settings, group.Value.Trim());

                    helper.ProcessDurationExpression(result, foundGroups, ref dateTime, information, durationExpression);
                }
                else if (group.Name == Definitions.Property.Proximity)
                {
                    var proximityType = ConversionHandler.ProximityType(settings, group.Value.Trim());

                    helper.ProcessProximityType(result, foundGroups, ref dateTime, information, proximityType);
                }
                else if (group.Name == Definitions.Patterns.YearDiscrete)
                {
                    helper.ProcessYear(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.MonthDiscrete)
                {
                    helper.ProcessMonth(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.DayDiscrete)
                {
                    helper.ProcessDay(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.DateBigEndian)
                {
                    helper.ProcessDateBigEndian(result, foundGroups, ref dateTime, information, group);
                }
                else if (group.Name == Definitions.Patterns.DateMiddleEndian)
                {
                    helper.ProcessDateMiddleEndian(result, foundGroups, ref dateTime, information, group);
                }
                else if (group.Name == Definitions.Patterns.DateLittleEndian)
                {
                    helper.ProcessDateLittleEndian(result, foundGroups, ref dateTime, information, group);
                }
                else if (group.Name == Definitions.Property.DateTimeUnits || IsDateUnit(group.Name)|| IsTimeUnit(group.Name))
                {
                    var timeUnit = ConversionHandler.DateTimeUnit(settings, group.Value.Trim());

                    helper.ProcessDateTimeUnit(result, foundGroups, ref dateTime, information, timeUnit);
                }
                else if (group.Name == Definitions.Property.RangeIndicator || group.Name == Definitions.Property.RangeSeparator)
                {
                    var rangePointer = ConversionHandler.RangePointer(settings, group.Value.Trim());

                    helper.ProcessRangePointer(result, foundGroups, ref dateTime, information, rangePointer);
                }
                else if (group.Name == Definitions.Property.SeasonOfYear)
                {
                    var seasonOfYear = ConversionHandler.DayOffset(settings, group.Value.Trim());

                    helper.ProcessSeason(result, foundGroups, ref dateTime, information, seasonOfYear);
                }
                else if (group.Name == Definitions.Property.DaysOfWeek)
                {
                    var dayOfWeek = ConversionHandler.DayOfWeek(settings, group.Value.Trim());

                    helper.ProcessDayOfWeek(result, foundGroups, ref dateTime, information, dayOfWeek);
                }
                else if (group.Name == Definitions.Property.MonthsOfYear)
                {
                    var monthOfYear = ConversionHandler.Month(settings, group.Value.Trim());

                    helper.ProcessMonthOfYear(result, foundGroups, ref dateTime, information, monthOfYear);
                }
                else if (group.Name == Definitions.Property.DayOffset)
                {
                    var dayOffset = ConversionHandler.DayOffset(settings, group.Value.Trim());

                    helper.ProcessDayOffset(result, foundGroups, ref dateTime, information, dayOffset);
                }
                else if (group.Name == Definitions.Property.TimeOfDay)
                {
                    var timeOfDay = ConversionHandler.TimeOfDay(settings, group.Value.Trim());

                    helper.ProcessTimeOfDay(result, foundGroups, ref dateTime, information, timeOfDay);
                }
                else if (group.Name == Definitions.Property.TimeMeridiam)
                {
                    var timeMeridiam = ConversionHandler.TimeMeridiam(settings, group.Value.Trim());

                    helper.ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, timeMeridiam);
                }
                else if (group.Name == Definitions.Property.NumericWord)
                {
                    var numericWord = ConversionHandler.NumericWord(settings, group.Value.Trim());

                    helper.ProcessNumericWord(result, foundGroups, ref dateTime, information, numericWord);
                }
                else if (group.Name == Definitions.Property.NumericWordCardinal)
                {
                    var numericWord = ConversionHandler.NumericWordCardinal(settings, group.Value.Trim());

                    helper.ProcessNumericWordCardinal(result, foundGroups, ref dateTime, information, numericWord);
                }
                else if (group.Name == Definitions.Property.NumericWordOrdinal)
                {
                    var numericWord = ConversionHandler.NumericWordOrdinal(settings, group.Value.Trim());

                    helper.ProcessNumericWordOrdinal(result, foundGroups, ref dateTime, information, numericWord);
                }
                else if (group.Name == Definitions.Property.TimeExpressions)
                {
                    var timeExpression = ConversionHandler.TimeExpression(settings, group.Value.Trim());

                    helper.ProcessTimeExpression(result, foundGroups, ref dateTime, information, timeExpression);
                }
                else if (group.Name == Definitions.Property.TimeFractions)
                {
                    var timeFraction = ConversionHandler.Fraction(settings, group.Value.Trim());

                    helper.ProcessTimeFraction(result, foundGroups, ref dateTime, information, timeFraction);
                }
                else if (group.Name == Definitions.Property.TimeConjointer)
                {
                    var timeConjointer = ConversionHandler.TimeConjointer(settings, group.Value.Trim());

                    helper.ProcessTimeConjointer(result, foundGroups, ref dateTime, information, timeConjointer);
                }
                else if (group.Name == Definitions.Patterns.Year)
                {
                    helper.ProcessYear(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.Hour)
                {
                    helper.ProcessHours(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.Minute)
                {
                    helper.ProcessMinutes(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.Second)
                {
                    helper.ProcessSeconds(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.HourDiscrete)
                {
                    helper.ProcessDiscreteHours(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.MinuteDiscrete)
                {
                    helper.ProcessDiscreteMinutes(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.SecondDiscrete)
                {
                    helper.ProcessDiscreteSeconds(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.NumberMax2Digits)
                {
                    helper.ProcessMax2DigitNumber(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.NumberMax4Digits)
                {
                    helper.ProcessMax4DigitNumber(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }
                else if (group.Name == Definitions.Patterns.NumberMax5Digits)
                {
                    helper.ProcessMax5DigitNumber(result, foundGroups, ref dateTime, information, group.Value.Trim());
                }

                group.GroupUsed = true;
            }

            helper.ProcessRemaining(result, foundGroups, ref dateTime, information);

            return result;
        }

        public bool IsDateUnit(string label)
        {
            return label == Definitions.Property.DateUnits
                || label == Definitions.Property.YearUnit
                || label == Definitions.Property.MonthUnit
                || label == Definitions.Property.WeekUnit
                || label == Definitions.Property.DayUnit;

        }

        public bool IsTimeUnit(string label)
        {
            return label == Definitions.Property.TimeUnits
                || label == Definitions.Property.HourUnit
                || label == Definitions.Property.MinuteUnit
                || label == Definitions.Property.SecondUnit;
        }

        public bool IsDateUnit(DateTimeUnit unit)
        {
            return unit == DateTimeUnit.Year
                || unit == DateTimeUnit.Month
                || unit == DateTimeUnit.Week
                || unit == DateTimeUnit.Day;
        }

        public bool IsTimeUnit(DateTimeUnit unit)
        {
            return unit == DateTimeUnit.Hour
                || unit == DateTimeUnit.Minute
                || unit == DateTimeUnit.Second;
        }
    }
}