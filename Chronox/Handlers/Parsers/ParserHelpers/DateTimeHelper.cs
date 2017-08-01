using System;
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

using Chronox.Parsers.General.ParserHelpers;
using Chronox.Interfaces;
using Chronox.Parsers.English;
using Chronox.Components;

namespace Chronox.Parsers.General.ParserHelpers
{
    public class DateTimeHelper : IChronoxParseHelper<ChronoxDateTimeExtraction>
    {
        private MasterParser parser;

        public DateTimeHelper(MasterParser parser)
        {
            this.parser = parser;
        }

        public void ProcessMax5DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            information.NumericValues.Enqueue(int.Parse(value));
        }

        public void ProcessMax4DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            information.NumericValues.Enqueue(int.Parse(value));
        }

        public void ProcessMax2DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            information.NumericValues.Enqueue(int.Parse(value));
        }

        public void ProcessNumericWord(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue)
        {
            information.NumericValues.Enqueue(numericValue);
        }

        public void ProcessNumericWordCardinal(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue)
        {
            information.NumericValues.Enqueue(numericValue);
        }

        public void ProcessNumericWordOrdinal(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue)
        {
            information.NumericOrdinals.Enqueue(numericValue);

            information.HasOrdinalNumber = true;
        }

        public void ProcessInterpretedExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeExpression interpretedExpression)
        {
            switch (interpretedExpression)
            {
                case DateTimeExpression.Tonight:

                    dateTime = dateTime.SetHour((int)RangeConstants.NIGHT_RANGE.Start);
                    dateTime = dateTime.SetMinutes(0);
                    dateTime = dateTime.SetSeconds(0);

                    break;
                case DateTimeExpression.LastNight:

                    dateTime = dateTime.AddDays(-1);
                    dateTime = dateTime.SetHour((int)RangeConstants.NIGHT_RANGE.Start);
                    dateTime = dateTime.SetMinutes(0);
                    dateTime = dateTime.SetSeconds(0);

                    break;
            }

            result.Builder.AssignValue(DateTimeUnit.Day, dateTime.Day);

            result.Builder.ImplyValue(DateTimeUnit.Hour, dateTime.Hour);
            result.Builder.ImplyValue(DateTimeUnit.Minute, dateTime.Minute);
            result.Builder.ImplyValue(DateTimeUnit.Second, dateTime.Second);

            information.HasInterpretedExpression = true;
        }

        public void ProcessRangePointer(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRangePointer rangePointer)
        {
            switch (rangePointer)
            {
                case TimeRangePointer.Indicator:
                    break;
                case TimeRangePointer.Separator:
                    break;
            }
        }

        public void ProcessGrabberExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRelation grabberExpression)
        {
            var offset = 0;

            switch (grabberExpression)
            {
                case TimeRelation.Future:

                    offset = 1;

                    break;
                case TimeRelation.Past:

                    offset = -1;

                    break;
            }

            information.GrabberOffsets.Enqueue(offset);

            information.HasGrabberExpression = true;
        }

        public void ProcessDateTimeUnit(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit timeUnit)
        {
            var grabberOffset = information.GrabberOffsets.Count > 0 ? information.GrabberOffsets.Dequeue() : int.MinValue;

            var hasGrabber = false;

            var processUnit = true;

            if (grabberOffset == int.MinValue)
            {
                grabberOffset = information.NumericValues.Count > 0 ? information.NumericValues.Dequeue() : int.MinValue;

                if (grabberOffset == int.MinValue)
                {
                    information.FloatingTimeUnits.Enqueue(timeUnit);
                }
            }
            else
            {
                hasGrabber = true;

                information.ProcessedTimeUnit.Enqueue(timeUnit);
            }

            var conjointer = foundGroups.Find(g => g.Name == Definitions.Property.TimeConjointer);

            if (conjointer != null && !hasGrabber)
            {
                var timeConjointer = TranslationHandler.TimeConjointer(information.Settings, conjointer.Value.Trim());

                foundGroups.Find(g => g.Name == Definitions.Property.TimeConjointer).GroupUsed = true;

                if (timeConjointer == TimeConjointer.Ago || timeConjointer == TimeConjointer.To)
                {
                    grabberOffset = grabberOffset == int.MinValue ? -1 : -grabberOffset;
                }
                else if (timeConjointer == TimeConjointer.FromNow)
                {
                    grabberOffset = grabberOffset == int.MinValue ? 1 : grabberOffset;
                }

                var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                if (grabberExpression != null && !grabberExpression.GroupUsed && grabberExpression.Equals(foundGroups[foundGroups.Count - 1]))
                {
                    if(!foundGroups.Any(g => g.Name == Definitions.Property.DaysOfWeek))
                    {
                        var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim());

                        ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                        foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;

                        var offset = information.GrabberOffsets.Dequeue();

                        HandleDateTimeUnits(result, foundGroups, ref dateTime, information, timeUnit, offset, true);

                        hasGrabber = true;
                    }
                }

                if (!hasGrabber && !conjointer.Equals(foundGroups[foundGroups.Count -1]))
                {
                    information.FloatingConjointer.Enqueue(timeConjointer);

                    CheckForSuffixInfo(result, foundGroups, ref dateTime, information, timeUnit);
                }
            }
            else
            {
                if(information.NumericOrdinals.Count > 0)
                {
                    var floatingUnit = information.FloatingTimeUnits.Count > 0 ? information.FloatingTimeUnits.Dequeue() : timeUnit;

                    HandleScalarUnit(result, foundGroups, ref dateTime, information, floatingUnit, information.NumericOrdinals.Dequeue());
                }
            }

            if (!processUnit) return;

            grabberOffset = grabberOffset == int.MinValue ? 0 : grabberOffset;

            if (information.PreferedDayOfWeek != null && !result.Builder.IsValueCertain(DateTimeUnit.Day))
            {
                dateTime = dateTime.SetWeekDay(information.PreferedDayOfWeek.Value);

                result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
                result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
                result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);
            }

            HandleDateTimeUnits(result, foundGroups, ref dateTime, information, timeUnit, grabberOffset, hasGrabber);

            if (information.PreferedDayOfWeek != null && !result.Builder.IsValueCertain(DateTimeUnit.Day) && !parser.IsTimeUnit(timeUnit))
            {
                if (!hasGrabber && !DayHasChanged(timeUnit, grabberOffset))
                {
                    dateTime = dateTime.SetWeekDay(information.PreferedDayOfWeek.Value);

                    result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
                    result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
                    result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);
                }
            }

        }

        private bool DayHasChanged(DateTimeUnit unit, int grabberOffset)
        {
            return unit == DateTimeUnit.Day && grabberOffset != 0;
        }

        private void CheckForSuffixInfo(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit current)
        {
            var unitInfo = foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name));

            if (unitInfo != null)
            {
                var timeUnit = TranslationHandler.DateTimeUnit(information.Settings, unitInfo.Value.Trim());

                if (current != timeUnit || foundGroups.FindAll(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).Count >= 2)
                {
                    foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).GroupUsed = true;

                    var grabberInfo = foundGroups.FindLast(g => g.Name == Definitions.Property.GrabberExpressions);

                    var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberInfo.Value.Trim());

                    ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                    foundGroups.FindLast(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;

                    HandleDateTimeUnits(result, foundGroups, ref dateTime, information, timeUnit, information.GrabberOffsets.Dequeue(), true);

                    information.FloatingConjointer.Clear();
                    information.FloatingTimeUnits.Clear();
                }
                else
                {
                    CheckForTimeUnitSuffixInfo(result, foundGroups, ref dateTime, information, current);
                }
            }
            else
            {
                CheckForTimeUnitSuffixInfo(result, foundGroups, ref dateTime, information, current);
            }
        }

        private void CheckForTimeUnitSuffixInfo(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit current)
        {
            if (current == DateTimeUnit.Hour || current == DateTimeUnit.Minute || current == DateTimeUnit.Second)
            {
                var timeExpression = foundGroups.Find(g => g.Name == Definitions.Property.TimeExpressions);

                if (timeExpression != null)
                {
                    var time = TranslationHandler.TimeExpression(information.Settings, timeExpression.Value.Trim());

                    ProcessTimeExpression(result, foundGroups, ref dateTime, information, time);

                    foundGroups.Find(g => g.Name == Definitions.Property.TimeExpressions).GroupUsed = true;

                    result.Builder.UnAssignValue(DateTimeUnit.Hour);
                    result.Builder.UnAssignValue(DateTimeUnit.Minute);
                    result.Builder.UnAssignValue(DateTimeUnit.Second);
                }

                var dayOffsetExpression = foundGroups.Find(g => g.Name == Definitions.Property.DayOffset);

                if (dayOffsetExpression != null)
                {
                    var dayOffset = TranslationHandler.DayOffset(information.Settings, dayOffsetExpression.Value.Trim());

                    ProcessDayOffset(result, foundGroups, ref dateTime, information, dayOffset);
                    
                    foundGroups.Find(g => g.Name == Definitions.Property.DayOffset).GroupUsed = true;

                    if (dateTime.HasDifferentDate(information.CurrentDate) && !information.HasInterpretedExpression && !information.HasTimeExpression)
                    {
                        ProcessTimeExpression(result, foundGroups, ref dateTime, information, new ChronoxTime(0, 0, 0));
                    }

                    result.Builder.UnAssignValue(DateTimeUnit.Hour);
                    result.Builder.UnAssignValue(DateTimeUnit.Minute);
                    result.Builder.UnAssignValue(DateTimeUnit.Second);

                }

                var dayOfWeekExpression = foundGroups.Find(g => g.Name == Definitions.Property.DaysOfWeek);

                if (dayOfWeekExpression != null && !dayOfWeekExpression.GroupUsed)
                {
                    var dayOfWeek = ChronoxDateTimeUtility.DayOfWeekShift(TranslationHandler.DayOfWeek(information.Settings, dayOfWeekExpression.Value.Trim()), information.Settings.GetWeekStartOffset());

                    var today = ChronoxDateTimeUtility.DayOfWeekShift(dateTime.DayOfWeek, information.Settings.GetWeekStartOffset());

                    var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                    var offset = 0;

                    if (grabberExpression != null)
                    {
                        ProcessGrabberExpression(result, foundGroups, ref dateTime, information, TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim()));

                        offset = information.GrabberOffsets.Count > 0 ? information.GrabberOffsets.Dequeue() : 0;
                    }


                    information.FloatingTimeUnits.Clear();

                    HandleDayOfWeek(result, foundGroups, ref dateTime, information, dayOfWeek, today, offset);

                    foundGroups.Find(g => g.Name == Definitions.Property.DaysOfWeek).GroupUsed = true;

                    information.PreferedDayOfWeek = dateTime.DayOfWeek;

                    result.Builder.UnAssignValue(DateTimeUnit.Hour);
                    result.Builder.UnAssignValue(DateTimeUnit.Minute);
                    result.Builder.UnAssignValue(DateTimeUnit.Second);

                    if (dateTime.HasDifferentDate(information.CurrentDate) && !information.HasInterpretedExpression && !information.HasTimeExpression)
                    {
                        ProcessTimeExpression(result, foundGroups, ref dateTime, information, new ChronoxTime(0, 0, 0));
                    }
                }
            }
            else
            {
                var dayOfWeekExpression = foundGroups.Find(g => g.Name == Definitions.Property.DaysOfWeek);

                if (dayOfWeekExpression != null && !dayOfWeekExpression.GroupUsed)
                {
                    var dayOfWeek = ChronoxDateTimeUtility.DayOfWeekShift(TranslationHandler.DayOfWeek(information.Settings, dayOfWeekExpression.Value.Trim()), information.Settings.GetWeekStartOffset());

                    var today = ChronoxDateTimeUtility.DayOfWeekShift(dateTime.DayOfWeek, information.Settings.GetWeekStartOffset());

                    var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                    var offset = 0;

                    if(grabberExpression != null)
                    {
                        ProcessGrabberExpression(result, foundGroups, ref dateTime, information, TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim()));

                        offset = information.GrabberOffsets.Count > 0 ? information.GrabberOffsets.Dequeue() : 0;
                    }

                    information.FloatingTimeUnits.Clear();

                    HandleDayOfWeek(result, foundGroups, ref dateTime, information, dayOfWeek, today, offset);

                    foundGroups.Find(g => g.Name == Definitions.Property.DaysOfWeek).GroupUsed = true;

                    information.PreferedDayOfWeek = dateTime.DayOfWeek;
                
                    result.Builder.UnAssignValue(DateTimeUnit.Hour);
                    result.Builder.UnAssignValue(DateTimeUnit.Minute);
                    result.Builder.UnAssignValue(DateTimeUnit.Second);

                    if (dateTime.HasDifferentDate(information.CurrentDate) && !information.HasInterpretedExpression && !information.HasTimeExpression)
                    {
                        ProcessTimeExpression(result, foundGroups, ref dateTime, information, new ChronoxTime(0, 0, 0));
                    }
                }
            }
        }

        private void HandleScalarUnit(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit dateTimeUnit, int value)
        {
            switch (dateTimeUnit)
            {
                case DateTimeUnit.Year:

                    break;
                case DateTimeUnit.Month:
                    dateTime = dateTime.SetMonth(1);

                    dateTime = dateTime.AddMonths(value - 1);

                    result.Builder.AssignValue(DateTimeUnit.Month, dateTime.Month);
                    break;
                case DateTimeUnit.Week:
                    if (information.ProcessedTimeUnit.Count > 0)
                    {
                        ProcessWeekScalar(result, foundGroups, ref dateTime, information, information.ProcessedTimeUnit.Dequeue(), 0);
                    }
                    else if (information.FloatingTimeUnits.Count > 0)
                    {
                        ProcessWeekScalar(result, foundGroups, ref dateTime, information, information.FloatingTimeUnits.Dequeue(), 0);
                    }
                    else
                    {
                        var unitInfo = foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || g.Name == Definitions.Property.DateUnits);

                        if (unitInfo != null)
                        {
                            var timeUnit = TranslationHandler.DateTimeUnit(information.Settings, unitInfo.Value.Trim());

                            if (dateTimeUnit != timeUnit || foundGroups.FindAll(g => g.Name == Definitions.Property.DateUnits).Count >= 2)
                            {
                                if (information.GrabberOffsets.Count <= 0)
                                {
                                    var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                                    if (grabberExpression != null)
                                    {
                                        var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim());

                                        ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                                        foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;

                                        ProcessWeekScalar(result, foundGroups, ref dateTime, information, timeUnit, information.GrabberOffsets.Dequeue());

                                        foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).GroupUsed = true;
                                    }
                                    else
                                    {
                                        ProcessWeekScalar(result, foundGroups, ref dateTime, information, timeUnit, 0);

                                        foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).GroupUsed = true;
                                    }
                                }
                                else
                                {
                                    ProcessWeekScalar(result, foundGroups, ref dateTime, information, timeUnit, 0);

                                    foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || g.Name == Definitions.Property.DateUnits).GroupUsed = true;
                                }

                            }
                            else
                            {
                                var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                                if (grabberExpression != null)
                                {
                                    var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim());

                                    ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                                    foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;
                                }

                                var monthExpression = foundGroups.Find(g => g.Name == Definitions.Property.MonthsOfYear);

                                if (monthExpression != null)
                                {
                                    var month = TranslationHandler.Month(information.Settings, monthExpression.Value.Trim());

                                    ProcessMonthOfYear(result, foundGroups, ref dateTime, information, month);

                                    foundGroups.Find(g => g.Name == Definitions.Property.MonthsOfYear).GroupUsed = true;

                                    dateTime = dateTime.SetDay(1);
                                }
                            }
                        }
                        else
                        {
                            dateTime = dateTime.SetDay(1);
                        }
                    }

                    dateTime = dateTime.AddWeeks(value - 1);

                    result.Builder.AssignValue(DateTimeUnit.Day, dateTime.Day);

                    break;
                case DateTimeUnit.Day:

                    if (information.ProcessedTimeUnit.Count > 0)
                    {
                        ProcessDayScalar(result, foundGroups, ref dateTime, information, information.ProcessedTimeUnit.Dequeue(), 0);
                    }
                    else if (information.FloatingTimeUnits.Count > 0)
                    {
                        ProcessDayScalar(result, foundGroups, ref dateTime, information, information.FloatingTimeUnits.Dequeue(), 0);
                    }
                    else
                    {
                        var unitInfo = foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name));

                        if (unitInfo != null)
                        {
                            var timeUnit = TranslationHandler.DateTimeUnit(information.Settings, unitInfo.Value.Trim());

                            if (dateTimeUnit != timeUnit || foundGroups.FindAll(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).Count >= 2)
                            {
                                
                                if(information.GrabberOffsets.Count <= 0)
                                {
                                    var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                                    if (grabberExpression != null)
                                    {
                                        var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim());

                                        ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                                        foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;

                                        ProcessDayScalar(result, foundGroups, ref dateTime, information, timeUnit, information.GrabberOffsets.Dequeue());

                                        foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).GroupUsed = true;
                                    }
                                    else
                                    {
                                        ProcessDayScalar(result, foundGroups, ref dateTime, information, timeUnit, 0);

                                        foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).GroupUsed = true;
                                    }
                                }
                                else
                                {
                                    ProcessDayScalar(result, foundGroups, ref dateTime, information, timeUnit, 0);

                                    foundGroups.FindLast(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).GroupUsed = true;
                                }

                            }
                            else
                            {

                                var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                                if (grabberExpression != null)
                                {
                                    var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim());

                                    ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                                    foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;
                                }

                                var monthExpression = foundGroups.Find(g => g.Name == Definitions.Property.MonthsOfYear);

                                if (monthExpression != null)
                                {
                                    var month = TranslationHandler.Month(information.Settings, monthExpression.Value.Trim());

                                    ProcessMonthOfYear(result, foundGroups, ref dateTime, information, month);

                                    foundGroups.Find(g => g.Name == Definitions.Property.MonthsOfYear).GroupUsed = true;

                                    dateTime = dateTime.SetDay(1);
                                }
                            }
                        }
                        else
                        {
                            dateTime = dateTime.SetDay(1);
                        }
                       
                    }
                    if (value == int.MaxValue)
                    {
                        value = ChronoxDateTimeUtility.DaysInMonth(dateTime);
                    }

                    dateTime = dateTime.AddDays(value - 1);

                    result.Builder.AssignValue(DateTimeUnit.Day, dateTime.Day);

                    break;
                case DateTimeUnit.Hour:
                    break;
                case DateTimeUnit.Minute:
                    break;
                case DateTimeUnit.Second:
                    break;
                case DateTimeUnit.Meridiam:
                    break;
                case DateTimeUnit.Default:
                    break;
            }
        }

        private void ProcessDayScalar(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit dateTimeUnit, int offset)
        {
            switch (dateTimeUnit)
            {
                case DateTimeUnit.Year:

                    dateTime = dateTime.AddYears(offset);
                    dateTime = dateTime.SetMonth(1);
                    dateTime = dateTime.SetDay(1);

                    result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
                    result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);

                    break;
                case DateTimeUnit.Month:

                    dateTime = dateTime.AddMonths(offset);
                    dateTime = dateTime.SetDay(1);

                    result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
                    break;
                case DateTimeUnit.Week:

                    dateTime = dateTime.SetWeekDay(ChronoxDateTimeUtility.GetDayOfWeek(information.Settings.GetWeekStartOffset()));
                    dateTime = dateTime.AddWeeks(offset);

                    result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);

                    break;
            }

            if (dateTime.HasDifferentDate(information.CurrentDate) && !information.HasInterpretedExpression && !information.HasTimeExpression)
            {
                ProcessTimeExpression(result, foundGroups, ref dateTime, information, new ChronoxTime(0, 0, 0));
            }

        }

        private void ProcessWeekScalar(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit dateTimeUnit, int offset)
        {
            switch (dateTimeUnit)
            {
                case DateTimeUnit.Year:

                    dateTime = dateTime.AddYears(offset);
                    dateTime = dateTime.SetMonth(1);
                    dateTime = dateTime.SetDay(1);

                    result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
                    result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);

                    break;
                case DateTimeUnit.Month:

                    dateTime = dateTime.AddMonths(offset);
                    dateTime = dateTime.SetDay(1);

                    result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
                    break;
            }
        }

        private int GetConjointerOffset(List<GroupWrapper> foundGroups, ChronoxBuildInformation information)
        {
            var conjointer = foundGroups.Find(g => g.Name == Definitions.Property.TimeConjointer);

            if (conjointer != null)
            {
                var timeConjointer = TranslationHandler.TimeConjointer(information.Settings, conjointer.Value.Trim());

                foundGroups.Find(g => g.Name == Definitions.Property.TimeConjointer).GroupUsed = true;

                if (timeConjointer == TimeConjointer.Ago || timeConjointer == TimeConjointer.To)
                {
                    return -1;
                }
                else if (timeConjointer == TimeConjointer.FromNow || timeConjointer == TimeConjointer.Past)
                {
                    return 1;
                }
            }
            return 0;
        }

        private void HandleDateTimeUnits(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit timeUnit, int offset, bool grabber)
        {
            switch (timeUnit)
            {
                case DateTimeUnit.Year:

                    if (grabber)
                    {
                        dateTime = dateTime.AddYears(offset);

                        dateTime = dateTime.SetMonth(1);
                        dateTime = dateTime.SetDay(1);
                    }
                    else
                    {
                        dateTime = dateTime.AddYears(offset);
                    }

                    result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);

                    break;
                case DateTimeUnit.Month:

                    if (grabber)
                    {
                        dateTime = dateTime.AddMonths(offset);

                        dateTime = dateTime.SetDay(1);
                    }
                    else
                    {
                        dateTime = dateTime.AddMonths(offset);
                    }

                    result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
                    result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);

                    break;
                case DateTimeUnit.Week:

                    if (grabber && !information.HasDayOfWeek)
                    {
                        dateTime = dateTime.SetWeekDay(ChronoxDateTimeUtility.GetDayOfWeek(information.Settings.GetWeekStartOffset()));

                        dateTime = dateTime.AddWeeks(offset);
                    }
                    else
                    {
                        dateTime = dateTime.AddWeeks(offset);
                    }

                    break;
                case DateTimeUnit.Day:

                    if (offset == 0 && information.NumericOrdinals.Count <= 0 && !information.HasOrdinalNumber) //Maybe a better way to check if context is morning
                    {
                        ProcessTimeOfDay(result, foundGroups, ref dateTime, information, RangeConstants.MORNING_RANGE);
                    }
                    else
                    {
                        dateTime = dateTime.AddDays(offset);
                    }

                    break;
                case DateTimeUnit.Hour:

                    if (offset >= 24)
                    {
                        while (offset > 24)
                        {
                            offset -= 24;

                            dateTime = dateTime.AddDays(offset);
                        }
                    }

                    dateTime = dateTime.AddHours(offset);

                    result.Builder.ImplyValue(DateTimeUnit.Second, dateTime.Second);
                    result.Builder.ImplyValue(DateTimeUnit.Minute, dateTime.Minute);
                    result.Builder.AssignValue(DateTimeUnit.Hour, dateTime.Hour);

                    break;
                case DateTimeUnit.Minute:

                    if (offset >= 60)
                    {
                        while (offset > 60)
                        {
                            offset -= 60;

                            dateTime = dateTime.AddHours(offset);
                        }
                    }

                    dateTime = dateTime.AddMinutes(offset);

                    result.Builder.ImplyValue(DateTimeUnit.Second, dateTime.Second);
                    result.Builder.AssignValue(DateTimeUnit.Minute, dateTime.Minute);
                    result.Builder.ImplyValue(DateTimeUnit.Hour, dateTime.Hour);

                    break;
                case DateTimeUnit.Second:

                    if (offset >= 60)
                    {
                        while (offset > 60)
                        {
                            offset -= 60;

                            dateTime = dateTime.AddMinutes(offset);
                        }

                        result.Builder.ImplyValue(DateTimeUnit.Hour, dateTime.Hour);
                    }

                    dateTime = dateTime.AddSeconds(offset);

                    result.Builder.AssignValue(DateTimeUnit.Second, dateTime.Second);
                    result.Builder.ImplyValue(DateTimeUnit.Minute, dateTime.Minute);

                    break;
            }
           
            result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
            result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
            result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);

            information.HasTimeUnit = true;

        }

        public void ProcessDayOffset(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOffset)
        {
            dateTime = dateTime.AddDays(dayOffset);

            result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
            result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
        }     

        public void ProcessTimeOfDay(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, RangeWrapper timeOfDay)
        {
            result.Builder.TimeOfDay = timeOfDay.TimeOfDay;

            if (result.Builder.IsValueCertain(DateTimeUnit.Hour))
            {
                if (!result.Builder.IsValueCertain(DateTimeUnit.Meridiam))
                {
                    if (RangeConstants.PM_RANGE.Contains(timeOfDay))
                    {
                        ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.PM);
                    }
                    else
                    {
                        ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.AM);
                    }
                }

                information.FloatingDayOfWeek.Clear();

                result.Builder.ReAssignValue(DateTimeUnit.Hour, dateTime.Hour);
            }
            else
            {
                dateTime = dateTime.SetHour((int)timeOfDay.Start);

                result.Builder.AssignValue(DateTimeUnit.Hour, dateTime.Hour);

                result.Builder.ImplyValue(DateTimeUnit.Minute, 0);

                result.Builder.ImplyValue(DateTimeUnit.Second, 0);

                if (information.GrabberOffsets.Count <= 0)
                {
                    information.FloatingTimeOfDay.Enqueue(timeOfDay);
                }
            }         

            if(information.GrabberOffsets.Count > 0 && information.CurrentGroup.Equals(foundGroups[foundGroups.Count - 1]))
            {
                ProcessDayOffset(result, foundGroups, ref dateTime, information, information.GrabberOffsets.Dequeue());
            }
        }

        public void ProcessMonthOfYear(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int monthOfYear)
        {
            if (monthOfYear > 12 || monthOfYear < 1) return;

            if(information.GrabberOffsets.Count > 0)
            {
                var offset = information.GrabberOffsets.Dequeue();

                if(offset > 0)
                {
                    dateTime = dateTime.SetMonth(1);
                    dateTime = dateTime.AddYears(1);
                    dateTime = dateTime.SetDay(1);
                    dateTime = dateTime.AddMonths(monthOfYear-1);
                }
                else if(offset < 0)
                {
                    if(dateTime.Month > monthOfYear)
                    {
                        dateTime = dateTime.SetMonth(1);
                        dateTime = dateTime.SetDay(1);
                        dateTime = dateTime.AddMonths(monthOfYear-1);
                    }
                    else
                    {
                        dateTime = dateTime.SetMonth(1);
                        dateTime = dateTime.AddYears(-1);
                        dateTime = dateTime.SetDay(1);
                        dateTime = dateTime.AddMonths(monthOfYear-1);
                    }
                }
                else
                {
                    dateTime = dateTime.SetMonth(monthOfYear);

                    if (dateTime.Month != information.CurrentDate.Month)
                    {
                        dateTime = dateTime.SetDay(1);
                    }
                }
            }
            else
            {
                if (information.NumericValues.Count > 0)
                {
                    if (information.NumericValues.Peek() == int.MaxValue)
                    {
                        information.NumericValues.Dequeue();

                        if (dateTime.Month > monthOfYear)
                        {
                            dateTime = dateTime.SetMonth(1);
                            dateTime = dateTime.SetDay(1);
                            dateTime = dateTime.AddMonths(monthOfYear - 1);
                        }
                        else
                        {
                            dateTime = dateTime.SetMonth(1);
                            dateTime = dateTime.AddYears(-1);
                            dateTime = dateTime.SetDay(1);
                            dateTime = dateTime.AddMonths(monthOfYear - 1);
                        }
                    }
                    else
                    {
                        if (information.NumericValues.Count > 0)
                        {
                            if (!information.HasTimeUnit)
                            {
                                ProcessDayOfMonth(result, foundGroups, ref dateTime, information, information.NumericValues.Dequeue());
                            }
                        }
                        else if (information.NumericOrdinals.Count > 0)
                        {
                            if (!information.HasTimeUnit)
                            {
                                ProcessDayOfMonth(result, foundGroups, ref dateTime, information, information.NumericOrdinals.Dequeue());
                            }
                        }
                        dateTime = dateTime.SetMonth(monthOfYear);

                        if (dateTime.Month != information.CurrentDate.Month)
                        {
                            dateTime = dateTime.SetDay(1);
                        }
                    }
                }
                else
                {
                    if (information.NumericValues.Count > 0)
                    {
                        if (!information.HasTimeUnit)
                        {
                            ProcessDayOfMonth(result, foundGroups, ref dateTime, information, information.NumericValues.Dequeue());
                        }
                    }
                    else if (information.NumericOrdinals.Count > 0)
                    {
                        if (!information.HasTimeUnit)
                        {
                            ProcessDayOfMonth(result, foundGroups, ref dateTime, information, information.NumericOrdinals.Dequeue());
                        }
                    }

                    dateTime = dateTime.SetMonth(monthOfYear);

                    if (dateTime.Month != information.CurrentDate.Month)
                    {
                        dateTime = dateTime.SetDay(1);
                    }
                }
            }

            result.Builder.AssignValue(DateTimeUnit.Month, dateTime.Month);
            result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);
            result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);

            if (information.NumericValues.Count > 0)
            {
                ProcessDayOfMonth(result, foundGroups, ref dateTime, information, information.NumericValues.Dequeue());
            }
        }

        public void ProcessDayOfMonth(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfMonth)
        {
            if (dayOfMonth > 31 || dayOfMonth < 1)
            {
                if(dayOfMonth > 31)
                {
                    ProcessYear(result, foundGroups, ref dateTime, information, dayOfMonth.ToString());
                }

                return;
            }

            dateTime = dateTime.SetDay(dayOfMonth);

            result.Builder.AssignValue(DateTimeUnit.Day, dateTime.Day);
        }

        public void ProcessDayOfWeekType(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DayOfWeekType dayOfWeekType)
        {
            var grabberOffset = information.GrabberOffsets.Count > 0 ? information.GrabberOffsets.Dequeue() : int.MinValue;

            if (grabberOffset == int.MinValue)
            {
                var index = 0;

                if (foundGroups[index].Equals(information.CurrentGroup))
                {
                    var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                    if(grabberExpression != null)
                    {
                        if (foundGroups[index + 1].Equals(grabberExpression))
                        {
                            var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim());

                            ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                            foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;

                            grabberOffset = information.GrabberOffsets.Dequeue();
                        }
                    }
                }
            }

            if(grabberOffset == int.MinValue)
            {
                grabberOffset = 0;
            }

            if(grabberOffset < 0)
            {
                if(dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    dateTime = dateTime.AddWeeks(-1);
                    dateTime = dateTime.GetPreviousWeekday(DayOfWeek.Friday);
                }
                else
                {
                    dateTime = dateTime.GetPreviousWeekday(DayOfWeek.Friday);
                }
            }
            else if (grabberOffset > 0)
            {
                if((int)dateTime.DayOfWeek < (int)DayOfWeek.Friday)
                {
                    dateTime = dateTime.AddWeeks(1);
                    dateTime = dateTime.GetNextWeekday(DayOfWeek.Friday);
                }
            }
            else
            {
                dateTime = dateTime.GetNextWeekday(DayOfWeek.Friday);
            }
        }

        public void ProcessDayOfWeek(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek)
        {
            dayOfWeek = ChronoxDateTimeUtility.DayOfWeekShift(dayOfWeek, information.Settings.GetWeekStartOffset());

            var today = ChronoxDateTimeUtility.DayOfWeekShift(dateTime.DayOfWeek, information.Settings.GetWeekStartOffset());

            var grabberOffset = information.GrabberOffsets.Count > 0 ? information.GrabberOffsets.Dequeue() : 0;

            if (information.NumericValues.Count > 0 && grabberOffset == 0)
            {
                ProcessDayOfWeekCount(result, foundGroups, ref dateTime, information, dayOfWeek);

                information.HasDayOfWeek = true;

                return;
            }

            if (information.GrabberOffsets.Count <= 0 && information.FloatingConjointer.Count > 0)
            {
                information.FloatingDayOfWeek.Enqueue(ChronoxDateTimeUtility.DayOfWeekShift(dayOfWeek, -information.Settings.GetWeekStartOffset()));
            }
            else
            {
                var conjointer = foundGroups.Find(g => g.Name == Definitions.Property.TimeConjointer);

                if (conjointer != null)
                {
                    if (foundGroups.Any(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)))
                    {
                        //information.FloatingDayOfWeek.Enqueue(DateTimeUtility.DayOfWeekShift(dayOfWeek, -information.settings.GetWeekStartOffset()));

                        information.PreferedDayOfWeek = ChronoxDateTimeUtility.GetDayOfWeek(ChronoxDateTimeUtility.DayOfWeekShift(dayOfWeek, -information.Settings.GetWeekStartOffset()));
                    }
                    else
                    {
                        var timeConjointer = TranslationHandler.TimeConjointer(information.Settings, conjointer.Value.Trim());

                        foundGroups.Find(g => g.Name == Definitions.Property.TimeConjointer).GroupUsed = true;

                        if (timeConjointer == TimeConjointer.Ago || timeConjointer == TimeConjointer.To)
                        {
                            grabberOffset = -1;
                        }
                        else if (timeConjointer == TimeConjointer.FromNow)
                        {
                            grabberOffset = 1;
                        }

                        var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                        if (grabberExpression != null && !grabberExpression.GroupUsed)
                        {
                            var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim());

                            ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                            foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;

                            var offset = information.GrabberOffsets.Dequeue();

                            if (grabberOffset < offset)
                            {
                                if(dayOfWeek <= today)
                                {
                                    HandleDateTimeUnits(result, foundGroups, ref dateTime, information, DateTimeUnit.Week, offset, true);

                                    today = ChronoxDateTimeUtility.DayOfWeekShift(dateTime.DayOfWeek, information.Settings.GetWeekStartOffset());
                                }
                            }
                            else
                            {
                                if(today <= dayOfWeek)
                                {
                                    HandleDateTimeUnits(result, foundGroups, ref dateTime, information, DateTimeUnit.Week, offset, true);

                                    today = ChronoxDateTimeUtility.DayOfWeekShift(dateTime.DayOfWeek, information.Settings.GetWeekStartOffset());
                                }
                            }
                        }
                    }
                }
                else if(information.NumericOrdinals.Count > 0)
                {
                    HandleScalarDayOfWeek(result, foundGroups, ref dateTime, information, dayOfWeek, today);

                    information.HasDayOfWeek = true;

                    return;
                }
                else if(foundGroups.Any(g => g.Name == Definitions.Property.GrabberExpressions))
                {
                    information.FloatingDayOfWeek.Enqueue(ChronoxDateTimeUtility.DayOfWeekShift(dayOfWeek, -information.Settings.GetWeekStartOffset()));
                }
            }

            if (!information.HasDayOfWeek)
            {
                HandleDayOfWeek(result, foundGroups, ref dateTime, information, dayOfWeek, today, grabberOffset);

                information.HasDayOfWeek = true;
            }
        }

        private void HandleScalarDayOfWeek(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek, int today)
        {
            var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

            var dateTimeUnitExpression = foundGroups.Find(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name));

            var monthExpression = foundGroups.Find(g => g.Name == Definitions.Property.MonthsOfYear);

            var count = information.NumericOrdinals.Dequeue();

            if(grabberExpression != null)
            {
                var grabber = TranslationHandler.GrabberExpression(information.Settings, grabberExpression.Value.Trim());

                ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabber);

                foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;

            }

            if(monthExpression != null)
            {
                var normalDay = ChronoxDateTimeUtility.DayOfWeekShift(dayOfWeek, -information.Settings.GetWeekStartOffset());

                var month = TranslationHandler.Month(information.Settings, monthExpression.Value.Trim());

                ProcessMonthOfYear(result, foundGroups, ref dateTime, information, month);

                foundGroups.Find(g => g.Name == Definitions.Property.MonthsOfYear).GroupUsed = true;

                dateTime = dateTime.SetDay(1);

                dateTime = dateTime.GetNextWeekday(normalDay);

                var limit = dateTime.TotalWeekDays(dateTime.Month, normalDay) - 1;

                if (count == int.MaxValue || count == (limit + 1))
                {
                    count = limit;
                }
                else
                {
                    count = count - 1;
                }

                if (count > 0)
                {
                    dateTime = dateTime.AddWeeks(count);
                }

                result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
                result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
                result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);
            }
            else
            {   
                if(dateTimeUnitExpression != null)
                {
                    var normalDay = ChronoxDateTimeUtility.DayOfWeekShift(dayOfWeek, -information.Settings.GetWeekStartOffset());

                    var dateTimeUnit = TranslationHandler.DateTimeUnit(information.Settings, dateTimeUnitExpression.Value.Trim());

                    var offsetValue = information.GrabberOffsets.Count > 0 ? information.GrabberOffsets.Dequeue() : int.MinValue;

                    var grabber = false;

                    if (offsetValue == int.MinValue)
                    {
                        var numberExpression = foundGroups.Find(g => g.Name == Definitions.Patterns.NumberMax5Digits);

                        if(numberExpression != null)
                        {
                            ProcessMax5DigitNumber(result, foundGroups, ref dateTime, information, numberExpression.Value.Trim());

                            foundGroups.Find(g => g.Name == Definitions.Patterns.NumberMax5Digits).GroupUsed = true;

                            offsetValue = information.NumericValues.Dequeue();
                        }
                        else
                        {
                            offsetValue = 0;
                        }

                    }
                    else
                    {
                        grabber = true;
                    }

                    if (!dateTimeUnitExpression.GroupUsed)
                    {
                        HandleDateTimeUnits(result, foundGroups, ref dateTime, information, dateTimeUnit, offsetValue, grabber);

                        foundGroups.Find(g => g.Name == Definitions.Property.DateTimeUnits || parser.IsDateUnit(g.Name)).GroupUsed = true;
                    }

                    switch (dateTimeUnit)
                    {
                        case DateTimeUnit.Year:
                            dateTime = dateTime.SetDay(1);
                            dateTime = dateTime.SetMonth(1);

                            dateTime = dateTime.GetNextWeekday(normalDay);

                            var limit = dateTime.GetWeeksInYear(dateTime.Year) - 1;

                            var year = dateTime.Year;

                            if (count == int.MaxValue || count == (limit + 1))
                            {
                                count = limit;
                            }
                            else
                            {
                                count = count - 1;
                            }

                            if (count > 0)
                            {
                                dateTime = dateTime.AddWeeks(count);
                            }

                            while(dateTime.Year > year)
                            {
                                dateTime = dateTime.AddWeeks(-1);
                            }

                            while(dateTime.Year < year)
                            {
                                dateTime = dateTime.AddWeeks(1);
                            }

                            break;
                        case DateTimeUnit.Month:
                            dateTime = dateTime.SetDay(1);

                            if (ChronoxDateTimeUtility.GetDayOfWeek(normalDay) != dateTime.DayOfWeek)
                            {
                                dateTime = dateTime.GetNextWeekday(normalDay);
                            }

                            limit = dateTime.TotalWeekDays(dateTime.Month, normalDay)-1;

                            if (count == int.MaxValue || count == (limit + 1))
                            {
                                count = limit;
                            }
                            else
                            {
                                count = count - 1;
                            }

                            if(count > 0)
                            {            
                                dateTime = dateTime.AddWeeks(count);
                            }

                            break;
                        case DateTimeUnit.Week:

                            dateTime = dateTime.SetDay(1);

                            dateTime = dateTime.GetNextWeekday(normalDay);

                            break;

                    }

                    result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
                    result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
                    result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);

                    information.HasDayOfWeek = true;
                }
            }
        }

        private void HandleDayOfWeek(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek, int today, int grabberOffset)
        {
            var previous = today;

            var current = today;

            if (grabberOffset > 0)
            {
                if(current == dayOfWeek)
                {
                    dateTime = dateTime.AddWeeks(1);
                }
                else
                {
                    if(current > dayOfWeek)
                    {
                        dateTime = dateTime.AddDays(-Math.Abs(dayOfWeek - current));
                    }
                    else
                    {
                        dateTime = dateTime.AddDays(Math.Abs(dayOfWeek - current));
                    }

                    dateTime = dateTime.AddWeeks(1);
                }
            }
            else if (grabberOffset < 0)
            {
                if(current > dayOfWeek)
                {
                    dateTime = dateTime.AddDays(-(Math.Abs(dayOfWeek - current) + ChronoxTimeSpanUtility.DAYS_IN_WEEK));
                }
                else if(current < dayOfWeek)
                {
                    dateTime = dateTime.GetPreviousWeekday(ChronoxDateTimeUtility.DayOfWeekShift(dayOfWeek, -information.Settings.GetWeekStartOffset()));
                }
                else
                {
                    dateTime = dateTime.AddWeeks(-1);
                }
            }
            else
            {
                if (current < dayOfWeek)
                {
                    dateTime = dateTime.AddDays(Math.Abs(dayOfWeek - current));
                }
                else if (current > dayOfWeek)
                {
                    dateTime = dateTime.AddDays(-Math.Abs(dayOfWeek - current));
                }
                else
                {
                    //IF we say: Example: ill see you tuesday on a tuesday: 
                    //Resolution: Is it next tueday or the current:
                    //If we say: Last week on tuesday or next week on tuesday on a tuesday                  

                    var grabberExpression = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                    if (grabberExpression == null)
                    {
                        dateTime = dateTime.AddDays(7);
                    }
                }
            }

            result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
            result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
        }

        public void ProcessDayOfWeekCount(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek)
        {
            var current = ChronoxDateTimeUtility.DayOfWeekShift(dateTime.DayOfWeek, information.Settings.GetWeekStartOffset());

            var offset = 0;

            var numericOffset = information.NumericValues.Count > 0 ? information.NumericValues.Dequeue() : int.MinValue;

            if( numericOffset != int.MinValue)
            {
                var conjointer = foundGroups.Find(g => g.Name == Definitions.Property.TimeConjointer);

                if (conjointer != null)
                {
                    var timeConjointer = TranslationHandler.TimeConjointer(information.Settings, conjointer.Value.Trim());

                    foundGroups.Find(g => g.Name == Definitions.Property.TimeConjointer).GroupUsed = true;

                    switch (timeConjointer)
                    {
                        case TimeConjointer.Ago:
                        case TimeConjointer.To:
                            offset = -1;
                            break;
                        case TimeConjointer.FromNow:
                        case TimeConjointer.Past:
                            offset = 1;
                            break;
                    }

                    if(timeConjointer.Equals(foundGroups[foundGroups.Count - 1]))
                    {
                        
                    }
                }
                else
                {
                    var grabber = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);

                    if (grabber != null)
                    {
                        var grabberOffsets = TranslationHandler.GrabberExpression(information.Settings, grabber.Value.Trim());

                        ProcessGrabberExpression(result, foundGroups, ref dateTime, information, grabberOffsets);

                        numericOffset = information.GrabberOffsets.Dequeue();

                        foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;
                    }
                }

                var normalizedDay = ChronoxDateTimeUtility.DayOfWeekShift(dayOfWeek, -information.Settings.GetWeekStartOffset());

                if (current < dayOfWeek)
                {
                    if (offset < 0)
                    {
                        numericOffset = numericOffset - 1;

                        dateTime = dateTime.GetPreviousWeekday(normalizedDay);
                    }
                }

                for (var i = 0; i < numericOffset; i++)
                {
                    result.Builder.UnAssignValue(DateTimeUnit.Day);

                    //HandleDayOfWeek(result, foundGroups, ref dateTime, information, dayOfWeek, current, offset);

                    //current = dayOfWeek;

                    if(offset < 0)
                    {
                        dateTime = dateTime.GetPreviousWeekday(normalizedDay);
                    }
                    else
                    {
                        dateTime = dateTime.GetNextWeekday(normalizedDay);
                    }
                }

                result.Builder.AssignValue(DateTimeUnit.Day, dateTime.Day);
                result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
                result.Builder.ImplyValue(DateTimeUnit.Year, dateTime.Year);
            }
        }

        public void ProcessYear(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            value = value.RemovePaddedPunctuation(0,0);

            var year = int.Parse(value.RemoveSubstrings("s"));

            if (year > 0)
            {
                if (year < 100)
                {
                    if (year >= 20)
                    {
                        year = 1900 + year;
                    }
                    else if(year < 20)
                    {
                        year = 2000 + year;
                    }
                    else
                    {
                        year = dateTime.Year;
                    }
                }
                else
                {
                    if (year < 543)
                    {
                        year = dateTime.Year;
                    }
                }
            }

            if (!result.Builder.ContainsValue(DateTimeUnit.Day))
            {
                dateTime = dateTime.SetDay(1);

                result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
            }

            if (!result.Builder.ContainsValue(DateTimeUnit.Month))
            {
                dateTime = dateTime.SetMonth(1);

                result.Builder.ImplyValue(DateTimeUnit.Month, dateTime.Month);
            }

            dateTime = dateTime.SetYear(year);

            result.Builder.ReAssignValue(DateTimeUnit.Year, dateTime.Year);
        }

        public void ProcessTimeExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, ChronoxTime time)
        {
            var midnight = time.Hours == 0;

            if (!result.Builder.IsValueCertain(DateTimeUnit.Hour))
            {
                dateTime = dateTime.SetHour(time.Hours);
                dateTime = dateTime.SetMinutes(time.Minutes);
                dateTime = dateTime.SetSeconds(time.Seconds);

                result.Builder.ImplyValue(DateTimeUnit.Hour, dateTime.Hour);
                result.Builder.ImplyValue(DateTimeUnit.Minute, dateTime.Minute);
                result.Builder.ImplyValue(DateTimeUnit.Second, dateTime.Second);

                information.HasTimeExpression = true;
            }

            if(!dateTime.HasDifferentDate(information.CurrentDate) && midnight)
            {
                dateTime = dateTime.AddDays(1);

                result.Builder.ImplyValue(DateTimeUnit.Day, dateTime.Day);
            }
        }

        public void ProcessHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            ProcessHours(result, foundGroups, ref dateTime, information, int.Parse(value));
        }

        public void ProcessHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int hours)
        {
            var meridiam = foundGroups.Find(g => g.Name == Definitions.Property.TimeMeridiam);

            hours = hours == 24 ? 0 : hours;

            if (hours >= 0 && hours <= 23)
            {
                dateTime = dateTime.SetHour(hours);

                result.Builder.AssignValue(DateTimeUnit.Hour, dateTime.Hour);

                DetermineTimeMeridiam(foundGroups, result, meridiam, information, ref dateTime);

                dateTime = dateTime.SetMinutes(0);
                dateTime = dateTime.SetSeconds(0);

                result.Builder.AssignValue(DateTimeUnit.Hour, dateTime.Hour);

                if (!result.Builder.IsValueCertain(DateTimeUnit.Minute))
                {
                    result.Builder.ImplyValue(DateTimeUnit.Minute, dateTime.Minute);
                }
                if (!result.Builder.IsValueCertain(DateTimeUnit.Second))
                {
                    result.Builder.ImplyValue(DateTimeUnit.Second, dateTime.Second);
                }
            }
        }

        public void DetermineTimeMeridiam(List<GroupWrapper> foundGroups, ChronoxDateTimeExtraction result, GroupWrapper meridiam, ChronoxBuildInformation information, ref DateTime dateTime)
        {
            if (meridiam == null || !string.IsNullOrEmpty(meridiam.Value))
            {
                var timeOfDayGroup = foundGroups.Find(g => g.Name == Definitions.Property.TimeOfDay);

                if (timeOfDayGroup != null && !string.IsNullOrEmpty(timeOfDayGroup.Value))
                {
                    var timeOfDay = TranslationHandler.TimeOfDay(information.Settings, timeOfDayGroup.Value.Trim());

                    if (RangeConstants.PM_RANGE.Contains(timeOfDay))
                    {
                        ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.PM);
                    }
                    else
                    {
                        ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.AM);
                    }
                }
                else
                {
                    if(result.Builder.TimeOfDay != TimeOfDay.Default)
                    {
                        switch (result.Builder.TimeOfDay)
                        {
                            case TimeOfDay.Morning:
                                ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.AM);
                                break;                          
                            case TimeOfDay.Afternoon:
                            case TimeOfDay.Evening:
                            case TimeOfDay.Night:
                                ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.PM);
                                break;                       
                        }
                    }
                    else
                    {
                        if (dateTime.Hour >= 12)
                        {
                            ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.PM);
                        }
                        else
                        {
                            ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.AM);
                        }
                    }
                }
            }
        }

        public void ProcessMinutes(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            if(value!=null && value.Length > 0 && !string.IsNullOrWhiteSpace(value))
            {
                var minutes = int.Parse(value.RemoveSubstrings(":", ".", " "));

                if (minutes >= 0 && minutes <= 59)
                {
                    dateTime = dateTime.SetMinutes(minutes);
                    dateTime = dateTime.SetSeconds(0);

                    result.Builder.ReAssignValue(DateTimeUnit.Minute, dateTime.Minute);
                }
            }           
        }

        public void ProcessSeconds(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            if(value!=null && value.Length > 0 && !string.IsNullOrWhiteSpace(value))
            {
                var seconds = int.Parse(value.RemoveSubstrings(":", ".", " "));

                if (seconds >= 0 && seconds <= 59)
                {
                    dateTime = dateTime.SetSeconds(seconds);

                    result.Builder.ReAssignValue(DateTimeUnit.Second, dateTime.Second);
                }
            }
        }

        public void ProcessDiscreteHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            ProcessDiscreteHours(result, foundGroups, ref dateTime, information, int.Parse(value));
        }

        public void ProcessDiscreteHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int hours)
        {
            hours = hours == 24 ? 0 : hours;

            if (hours >= 0 && hours <= 23)
            {
                var meridiam = foundGroups.Find(g => g.Name == Definitions.Property.TimeMeridiam);

                dateTime = dateTime.SetHour(hours);

                DetermineTimeMeridiam(foundGroups,result, meridiam, information, ref dateTime);

                result.Builder.AssignValue(DateTimeUnit.Hour, dateTime.Hour);

                information.FloatingHours.Enqueue(hours);
            }
        }

        public void ProcessDiscreteMinutes(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            var minutes = int.Parse(value);

            if (minutes >= 0 && minutes <= 59)
            {
                dateTime = dateTime.SetMinutes(0);
                dateTime = dateTime.SetSeconds(0);

                result.Builder.ReAssignValue(DateTimeUnit.Minute, dateTime.Minute);

                information.FloatingMinutes.Enqueue(minutes);
            }
        }

        public void ProcessDiscreteSeconds(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            var seconds = int.Parse(value);

            if (seconds >= 0 && seconds <= 59)
            {
                dateTime = dateTime.SetSeconds(0);

                result.Builder.ReAssignValue(DateTimeUnit.Second, dateTime.Second);

                information.FloatingSeconds.Enqueue(seconds);
            }
        }

        public void ProcessTimeFraction(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeFraction timeFraction)
        {
            switch (timeFraction)
            {
                case TimeFraction.Quater:

                    ProcessDiscreteMinutes(result, foundGroups, ref dateTime, information, "15");

                    break;
                case TimeFraction.Half:

                    ProcessDiscreteMinutes(result, foundGroups, ref dateTime, information, "30");

                    break;
            }
        }

        public void ProcessTimeConjointer(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer value)
        {
            var floatingMinutes = int.MinValue;

            var hour = foundGroups.Find(g => g.Name == Definitions.Patterns.HourDiscrete);

            switch (value)
            {
                case TimeConjointer.To:
         
                    if(hour != null)
                    {
                        ProcessDiscreteHours(result, foundGroups, ref dateTime, information, hour.Value.Trim());

                        foundGroups.Find(g => g.Name == Definitions.Patterns.HourDiscrete).GroupUsed = true;
                    }
                    else
                    {
                        ProcessDiscreteHours(result, foundGroups, ref dateTime, information, (dateTime.Hour + 1).ToString());
                    }

                    floatingMinutes = information.FloatingMinutes.Count > 0 ? information.FloatingMinutes.Dequeue() : int.MinValue;

                    if (floatingMinutes != int.MinValue)
                    {
                        dateTime = dateTime.AddMinutes(-floatingMinutes);

                        result.Builder.ReAssignValue(DateTimeUnit.Minute, dateTime.Minute);
                        result.Builder.ReAssignValue(DateTimeUnit.Hour, dateTime.Hour);
                    }
                    break;
                case TimeConjointer.Past:

                    if (hour != null)
                    {
                        ProcessDiscreteHours(result, foundGroups, ref dateTime, information, hour.Value.Trim());

                        foundGroups.Find(g => g.Name == Definitions.Patterns.HourDiscrete).GroupUsed = true;
                    }

                    floatingMinutes = information.FloatingMinutes.Count > 0 ? information.FloatingMinutes.Dequeue() : 0;

                    if (floatingMinutes != int.MinValue)
                    {
                        dateTime = dateTime.AddMinutes(floatingMinutes);

                        result.Builder.ReAssignValue(DateTimeUnit.Minute, dateTime.Minute);
                        result.Builder.ReAssignValue(DateTimeUnit.Hour, dateTime.Hour);
                    }
                    break;
                case TimeConjointer.FromNow:
                    break;
                case TimeConjointer.From:
                    break;
                case TimeConjointer.Ago:
                    break;
            }

            ProcessRemainingTime(result, foundGroups, ref dateTime, information, value);
        }

        public void ProcessMonth(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            ProcessMonthOfYear(result, foundGroups, ref dateTime, information, int.Parse(value.Trim()));
        }

        public void ProcessDay(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            ProcessDayOfMonth(result, foundGroups, ref dateTime, information, int.Parse(value.Trim()));
        }

        public void ProcessTimeZoneInformation(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            var value = GroupWrapper.Value;

            var zoneCode = information.LatestMatch.Match.Groups[Definitions.Patterns.TimeZoneCode];
            var zoneOffset = information.LatestMatch.Match.Groups[Definitions.Patterns.TimeZoneOffset];
            var zoneAbbreviation = information.LatestMatch.Match.Groups[Definitions.Patterns.TimeZoneAbbreviation];

            if(zoneAbbreviation.Length > 0)
            {
                if(Definitions.Converters.TIMEZONE_OFFSETS.TryGetValue(zoneAbbreviation.Value, out ChronoxTimeZone timeZone))
                {
                    result.Builder.TimeZone = timeZone;
                    result.Builder.TimeOffset = timeZone.Offset;
                }
            }
            else
            {
                if(zoneOffset.Length > 0)
                {
                    if(zoneOffset.Value.StartsWith("-") || zoneOffset.Value.StartsWith("+"))
                    {
                        var arithmeticOperator = TranslationHandler.ArithmeticOperation(information.Settings, zoneOffset.Value[0].ToString());

                        var offset = int.MinValue;

                        if (zoneOffset.Value.Contains(":"))
                        {
                            var time = zoneOffset.Value.RemoveSubstrings("-", "+").Split(":");

                            var hours = time[0];

                            var minutes = time[1];

                            offset = int.Parse(hours) * 60 + int.Parse(minutes);
                        }
                        else
                        {
                            if(zoneOffset.Value.Length >= 5)
                            {
                                var time = zoneOffset.Value.RemoveSubstrings("-", "+");

                                var hours = new string(time.Take(2).ToArray());

                                var minutes = new string(time.Skip(2).ToArray());

                                offset = int.Parse(hours) * 60 + int.Parse(minutes);
                            }
                        }

                        if(offset != int.MinValue)
                        {
                            if (arithmeticOperator == ArithmeticOperation.Substract)
                            {
                                offset = -offset;
                            }

                            result.Builder.TimeOffset = TimeSpan.FromMinutes(offset);
                        }                    
                    }
                    else
                    {
                        if(zoneOffset.Value == "Z")
                        {
                            if (Definitions.Converters.TIMEZONE_OFFSETS.TryGetValue(zoneOffset.Value, out ChronoxTimeZone timeZone))
                            {
                                result.Builder.TimeZone = timeZone;
                                result.Builder.TimeOffset = timeZone.Offset;
                            }
                        }
                    }
                }
            }
        }

        public void ProcessDateBigEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            var value = GroupWrapper.Value;

            var year = information.LatestMatch.Match.Groups[Definitions.Patterns.YearDiscrete];
            var month = information.LatestMatch.Match.Groups[Definitions.Patterns.MonthDiscrete];
            var day = information.LatestMatch.Match.Groups[Definitions.Patterns.DayDiscrete];

            if (year.Length > 0)
            {
                ProcessYear(result, foundGroups, ref dateTime, information, year.Value.Trim());

                foundGroups.Find(g => g.Name == Definitions.Patterns.YearDiscrete).GroupUsed = true;
            }
            if (month.Length > 0)
            {
                ProcessMonth(result, foundGroups, ref dateTime, information, month.Value.Trim());

                foundGroups.Find(g => g.Name == Definitions.Patterns.MonthDiscrete).GroupUsed = true;
            }
            if (day.Length > 0)
            {
                ProcessDay(result, foundGroups, ref dateTime, information, day.Value.Trim());

                foundGroups.Find(g => g.Name == Definitions.Patterns.DayDiscrete).GroupUsed = true;
            }
        }

        public void ProcessDateMiddleEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            //Too ambigous. Maybe only support base on cultural information
        }

        public void ProcessDateLittleEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            var value = GroupWrapper.Value;

            var year = information.LatestMatch.Match.Groups[Definitions.Patterns.YearDiscrete];
            var month = information.LatestMatch.Match.Groups[Definitions.Patterns.MonthDiscrete];
            var day = information.LatestMatch.Match.Groups[Definitions.Patterns.DayDiscrete];

            var middleEndian = false;

            if (year.Length > 0)
            {
                ProcessYear(result, foundGroups, ref dateTime, information, year.Value.Trim());

                foundGroups.Find(g => g.Name == Definitions.Patterns.YearDiscrete).GroupUsed = true;
            }
            if (month.Length > 0)
            {
                if (int.TryParse(month.Value.Trim(), out int monthOfYear))
                {
                    if (monthOfYear > 0 && monthOfYear <= 12)
                    {
                        ProcessMonth(result, foundGroups, ref dateTime, information, month.Value.Trim());

                        foundGroups.Find(g => g.Name == Definitions.Patterns.MonthDiscrete).GroupUsed = true;
                    }
                    else
                    {
                        if (monthOfYear > 12)
                        {
                            ProcessDay(result, foundGroups, ref dateTime, information, month.Value.Trim());

                            foundGroups.Find(g => g.Name == Definitions.Patterns.DayDiscrete).GroupUsed = true;

                            middleEndian = true;
                        }
                    }
                }
            }
            if (day.Length > 0)
            {
                if (middleEndian)
                {
                    ProcessMonth(result, foundGroups, ref dateTime, information, day.Value.Trim());

                    foundGroups.Find(g => g.Name == Definitions.Patterns.MonthDiscrete).GroupUsed = true;
                }
                else
                {
                    ProcessDay(result, foundGroups, ref dateTime, information, day.Value.Trim());

                    foundGroups.Find(g => g.Name == Definitions.Patterns.DayDiscrete).GroupUsed = true;
                }
            }
        }

        public void ProcessRemaining(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information)
        {
            if (dateTime.HasDifferentDate(information.CurrentDate ) && !information.HasInterpretedExpression && !information.HasTimeExpression)
            {
                ProcessTimeExpression(result, foundGroups, ref dateTime, information, new ChronoxTime(0, 0, 0));
            }

            if (information.NumericValues.Count > 0)
            {
                if (PossibleDayExpression(result, foundGroups, ref dateTime, information) && !information.ProcessTime)
                {
                    var dayOfWeek = foundGroups.Find(g => g.Name == Definitions.Property.DaysOfWeek);

                    if (dayOfWeek == null)
                    {
                        var numericOffset = information.NumericValues.Dequeue();

                        ProcessDayOfMonth(result, foundGroups, ref dateTime, information, numericOffset);
                    }
                    else
                    {
                        var numericOffset = information.NumericValues.Dequeue();

                        ProcessDayOfMonth(result, foundGroups, ref dateTime, information, numericOffset);
                        //TODO:
                        //Process base on resolution
                        //Example: "Monday the third"
                        //If the third of the month of the reference date is not a monday: "Wrong date"
                        //Resolution: Pick the discrete day and not the week day
                    }

                    if(information.NumericValues.Count > 0)
                    {
                        information.ProcessTime = true;

                        ProcessRemaining(result, foundGroups, ref dateTime, information);
                    }
                }
                else if (PossibleTimeExpression(result, foundGroups, ref dateTime, information) || information.ProcessTime)
                {
                    var meridiam = foundGroups.Find(g => g.Name == Definitions.Property.TimeMeridiam);

                    dateTime = dateTime.SetHour(information.NumericValues.Peek());

                    result.Builder.ImplyValue(DateTimeUnit.Hour, dateTime.Hour);

                    if(meridiam == null || meridiam.Group.Length <= 0)
                    {
                        var timeOfDayExpression = foundGroups.Find(g => g.Name == Definitions.Property.TimeOfDay);

                        if(timeOfDayExpression != null)
                        {
                            var timeOfDay = TranslationHandler.TimeOfDay(information.Settings, timeOfDayExpression.Value.Trim());

                            if (RangeConstants.PM_RANGE.Contains(timeOfDay))
                            {
                                ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.PM);
                            }
                            else
                            {
                                ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.AM);
                            }

                        }
                        else
                        {
                            if (information.NumericValues.Peek() >= 12)
                            {
                                ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.PM);
                            }
                            else
                            {
                                ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, TimeMeridiam.AM);
                            }
                        }
                    }
                    else
                    {
                        var meridianValue = TranslationHandler.TimeMeridiam(information.Settings, meridiam.Value.Trim());

                        ProcessTimeMeridiam(result, foundGroups, ref dateTime, information, meridianValue);
                    }

                    information.NumericValues.Dequeue();
                }                
            }

            if (information.GrabberOffsets.Count > 0)
            {
                if(information.FloatingTimeUnits.Count > 0)
                {
                    result.Builder.UnAssignValue(DateTimeUnit.Day);

                    ProcessDateTimeUnit(result, foundGroups, ref dateTime, information, information.FloatingTimeUnits.Dequeue());
                }

                if(information.FloatingDayOfWeek.Count > 0 && information.GrabberOffsets.Count > 0)
                {
                    result.Builder.UnAssignValue(DateTimeUnit.Day);

                    ProcessDayOfWeek(result, foundGroups, ref dateTime, information, information.FloatingDayOfWeek.Dequeue());
                }

                if(information.FloatingTimeOfDay.Count > 0 && information.GrabberOffsets.Count > 0)
                {
                    result.Builder.UnAssignValue(DateTimeUnit.Day);

                    ProcessDayOfWeek(result, foundGroups, ref dateTime, information, dateTime.Day + information.GrabberOffsets.Peek());
                }
            }
            else
            {
                if (information.FloatingDayOfWeek.Count > 0 && information.NumericOrdinals.Count <= 0)
                {
                    if (!result.Builder.IsValueCertain(DateTimeUnit.Day))
                    {
                        ProcessDayOfWeek(result, foundGroups, ref dateTime, information, information.FloatingDayOfWeek.Dequeue());
                    }
                }
            }

            if (information.NumericOrdinals.Count > 0)
            {
                if(information.FloatingTimeUnits.Count > 0)
                {
                    HandleScalarUnit(result, foundGroups, ref dateTime, information, information.FloatingTimeUnits.Dequeue(), information.NumericOrdinals.Dequeue());
                }
                else if(information.FloatingDayOfWeek.Count > 0)
                {
                    ProcessDayOfMonth(result, foundGroups, ref dateTime, information, information.NumericOrdinals.Dequeue());
                }
                else
                {
                    ProcessDayOfMonth(result, foundGroups, ref dateTime, information, information.NumericOrdinals.Dequeue());
                }
            }
            if(information.FloatingConjointer.Count > 0)
            {
                if(information.FloatingTimeUnits.Count > 0)
                {
                    ProcessPrefixExpression(result, foundGroups, ref dateTime, information, information.FloatingConjointer.Dequeue(), information.FloatingTimeUnits.Dequeue());
                }
            }
        }

        private void ProcessPrefixExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer timeConjointer, DateTimeUnit dateTimeUnit)
        {
            var offset = 0;

            if (timeConjointer == TimeConjointer.Ago || timeConjointer == TimeConjointer.To)
            {
                offset = -1;
            }
            else if (timeConjointer == TimeConjointer.FromNow || timeConjointer == TimeConjointer.Past)
            {
                offset = 1;
            }

            HandleDateTimeUnits(result, foundGroups, ref dateTime, information, dateTimeUnit, offset, false);
        }


        private bool PossibleDayExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information)
        {
            var ordinalWord = foundGroups.Find(g => g.Name == Definitions.Property.Ordinal);

            if(ordinalWord == null)
            {
                ordinalWord = foundGroups.Find(g => g.Name == Definitions.Property.Number);
            }

            if(ordinalWord != null)
            {
                if(int.TryParse(ordinalWord.Value.RemoveSubstrings(information.Settings.Language.Vocabulary.OrdinalSuffixes).Trim(), out int value))
                {
                    return true;
                }
                else
                {
                    //Is ordinal or else
                    //Non-Conventional in "Most cultures
                }
            }
            else
            {
                var number = foundGroups.Find(g => g.Name == Definitions.Patterns.NumberMax2Digits);

                if(number != null)
                {
                    return true;
                }
                else
                {
                    if (information.NumericOrdinals.Count > 1)
                    {
                        return true;
                    }
                }

            }

            return false;
        }

        private bool PossibleTimeExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information)
        {
            var cardinalWord = foundGroups.Find(g => g.Name == Definitions.Property.Number);

            if (cardinalWord != null)
            {
                if (int.TryParse(cardinalWord.Value.Trim(), out int value))
                {
                    return true;
                }
                else
                {
                    //Is ordinal or else
                }
            }
            return false;
        }

        private bool ContainsDiscreteTimeExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information)
        {
            var hour = foundGroups.Find(g => g.Name == Definitions.Patterns.Hour);

            if (hour == null)
            {
                hour = foundGroups.Find(g => g.Name == Definitions.Patterns.HourDiscrete);
            }

            if (hour != null)
            {
                var numericValue = TranslationHandler.NumericWordCardinal(information.Settings, hour.Value.Trim());

                if (numericValue != int.MinValue)
                {

                }
                else
                {
                    //Is ordinal or else
                }
            }
            return false;
        }

        private void ProcessRemainingTime(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer conjointer)
        {
            var dayOffset = foundGroups.Find(g => g.Name == Definitions.Property.DayOffset);

            if (dayOffset != null)
            {
                HandleDayOffsetExpression(result, foundGroups, ref dateTime, information, conjointer, dayOffset);
            }
            else
            {
                var timeExpression = foundGroups.Find(g => g.Name == Definitions.Property.TimeExpressions);

                if (timeExpression != null)
                {
                    HandleTimeExpression(result, foundGroups,ref dateTime, information, conjointer, timeExpression);
                }
                else
                {
                    var dayOfWeek = foundGroups.Find(g => g.Name == Definitions.Property.DaysOfWeek);

                    if (dayOfWeek != null)
                    {
                        var dayOfWeekValue = TranslationHandler.DayOfWeek(information.Settings, dayOfWeek.Value.Trim());
                    }
                    else
                    {
                        var timeUnit = foundGroups.Find(g => g.Name == Definitions.Property.DateTimeUnits);

                        if (timeUnit != null)
                        {                       
                            HandleTimeUnitExpression(result, foundGroups, ref dateTime, information, conjointer, timeUnit);
                        }
                    }
                }
            }
        }

        private void HandleDayOffsetExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer conjointer, GroupWrapper dayOffset)
        {
            var dayOffsetValue = TranslationHandler.DayOffset(information.Settings, dayOffset.Value.Trim());

        }

        private static void HandleTimeExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer conjointer, GroupWrapper timeExpression)
        {
            var timeExpressionValue = TranslationHandler.TimeExpression(information.Settings, timeExpression.Value.Trim());

            var hourOffset = 0;

            if (information.NumericValues.Count > 0)
            {
                switch (conjointer)
                {
                    case TimeConjointer.To:
                    case TimeConjointer.From:
                        hourOffset = -Math.Abs(information.NumericValues.Dequeue());
                        break;
                    case TimeConjointer.Past:
                        hourOffset = Math.Abs(information.NumericValues.Dequeue());
                        break;
                }
            }

            if (timeExpressionValue.Hours == 0)
            {
                var offset = (24 - dateTime.Hour) + hourOffset;

                dateTime = dateTime.SetSeconds(0);
                dateTime = dateTime.SetMinutes(0);
                dateTime = dateTime.AddHours(offset);
            }
            else
            {
                var offset = (timeExpressionValue.Hours - dateTime.Hour) + hourOffset;

                //dateTime = dateTime.SetSeconds(0);
                //dateTime = dateTime.SetMinutes(0);
                dateTime = dateTime.AddHours(offset);
            }

            //result.Builder.ReAssignValue(DateTimeUnit.Second, dateTime.Second);
            //result.Builder.ReAssignValue(DateTimeUnit.Minute, dateTime.Minute);
            result.Builder.ReAssignValue(DateTimeUnit.Hour, dateTime.Hour);

            foundGroups.Find(g => g.Name == Definitions.Property.TimeExpressions).GroupUsed = true;
        }

        public void HandleTimeUnitExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer conjointer, GroupWrapper timeUnit)
        {
            var timeUnitExpression = TranslationHandler.DateTimeUnit(information.Settings, timeUnit.Value.Trim());

            var grabberOffset = 0;

            if(information.GrabberOffsets.Count > 0)
            {
                grabberOffset = information.GrabberOffsets.Dequeue();
            }
            else
            {
                var grabber = foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions);         

                if (grabber != null)
                {
                    ProcessGrabberExpression(result, foundGroups, ref dateTime, information, TranslationHandler.GrabberExpression(information.Settings, grabber.Value.Trim()));

                    grabberOffset = information.GrabberOffsets.Dequeue();

                    foundGroups.Find(g => g.Name == Definitions.Property.GrabberExpressions).GroupUsed = true;
                }
            }
        }

        public void ProcessCasualExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateCasualExpression casualExpression)
        {
            //Unsure if it will be supported
        }

        public void ProcessSeason(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int seasonOfYear)
        {
        
        }

        public void ProcessRepeaterIndicator(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateRepeaterIndicator repeaterIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRepeater repeaterExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationIndicator(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationIndicator durationIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationExpression durationExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessProximityType(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, CertaintyType proximityType)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeMeridiam(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeMeridiam timeMeridiam)
        {
            var certain = foundGroups.Any(g => g.Name == Definitions.Property.TimeMeridiam);

            switch (timeMeridiam)
            {
                case TimeMeridiam.AM:

                    if (certain)
                    {
                        result.Builder.AssignValue(DateTimeUnit.Meridiam, 0);
                    }
                    else
                    {
                        result.Builder.ImplyValue(DateTimeUnit.Meridiam, 0);
                    }

                    dateTime = dateTime.SetHour(dateTime.Hour == 12 ? 0 : dateTime.Hour);

                    break;
                case TimeMeridiam.PM:

                    if (certain)
                    {
                        result.Builder.AssignValue(DateTimeUnit.Meridiam, 1);
                    }
                    else
                    {
                        result.Builder.ImplyValue(DateTimeUnit.Meridiam, 1);
                    }

                    dateTime = dateTime.SetHour(dateTime.Hour != 12 ? dateTime.Hour + 12 : dateTime.Hour);

                    break;
            }

            result.Builder.ReAssignValue(DateTimeUnit.Hour, dateTime.Hour);
        }

        public void BreakExecution(List<GroupWrapper> foundGroups, ChronoxDateTimeExtraction result, bool returnNull)
        {
            foundGroups.ForEach(g => g.GroupUsed = true);
        }
    }
}
