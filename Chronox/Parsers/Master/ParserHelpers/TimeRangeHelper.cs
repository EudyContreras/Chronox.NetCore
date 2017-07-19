using Chronox.Abstractions;
using Chronox.Interfaces;
using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Chronox.Helpers;
using Enumerations;

namespace Chronox.Parsers.General.ParserHelpers
{
    internal class TimeRangeHelper : IChronoxParseHelper<ChronoxTimeRangeExtraction>
    {
        public void DetermineTimeMeridiam(List<GroupWrapper> foundGroups, GroupWrapper meridiam, ChronoxBuildInformation information, ref DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeMeridiam(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeMeridiam timeMeridiam)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWord(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWordCardinal(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWordOrdinal(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord)
        {
            throw new NotImplementedException();
        }

        public void ProcessCasualExpression(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateCasualExpression casualExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateBigEndian(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateLittleEndian(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateMiddleEndian(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDay(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOffset(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOffset)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOfWeekType(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DayOfWeekType dayOfWeekType)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOfWeek(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteHours(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteMinutes(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteSeconds(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationExpression(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationExpression durationExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationIndicator(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationIndicator durationIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessGrabberExpression(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRelation grabberExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessHours(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessInterpretedExpression(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeExpression interpretedExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax2DigitNumber(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax4DigitNumber(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax5DigitNumber(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMinutes(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMonth(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMonthOfYear(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int monthOfYear)
        {
            throw new NotImplementedException();
        }

        public void ProcessProximityType(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, CertaintyType proximityType)
        {
            throw new NotImplementedException();
        }

        public void ProcessRangePointer(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRangePointer rangePointer)
        {
            throw new NotImplementedException();
        }

        public void ProcessRemaining(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterExpression(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRepeater repeaterExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterIndicator(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateRepeaterIndicator repeaterIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessSeason(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int seasonOfYear)
        {
            throw new NotImplementedException();
        }

        public void ProcessSeconds(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeConjointer(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeFraction(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeFraction timeFraction)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeOfDay(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, RangeWrapper timeOfDay)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateTimeUnit(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit timeUnit)
        {
            throw new NotImplementedException();
        }

        public void ProcessYear(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeExpression(ChronoxTimeRangeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, ChronoxTime time)
        {
            throw new NotImplementedException();
        }
    }
}
