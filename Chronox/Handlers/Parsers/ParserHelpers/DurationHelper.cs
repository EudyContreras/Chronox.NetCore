using Chronox.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Chronox.Wrappers;
using System.Text.RegularExpressions;
using Chronox.Interfaces;
using Chronox.Helpers;
using Enumerations;

namespace Chronox.Parsers.General.ParserHelpers
{
    public class TimeSpanHelper : IChronoxParseHelper<ChronoxTimeSpanExtraction>
    {
        public void DetermineTimeMeridiam(List<GroupWrapper> foundGroups, GroupWrapper meridiam, ChronoxBuildInformation information, ref DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeMeridiam(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeMeridiam timeMeridiam)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWord(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWordCardinal(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWordOrdinal(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord)
        {
            throw new NotImplementedException();
        }

        public void ProcessCasualExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateCasualExpression casualExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateBigEndian(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateLittleEndian(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateMiddleEndian(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDay(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOffset(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOffset)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOfWeekType(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DayOfWeekType dayOfWeekType)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOfWeek(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteHours(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteMinutes(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteSeconds(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationExpression durationExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationIndicator(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationIndicator durationIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessGrabberExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRelation grabberExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessHours(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessInterpretedExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeExpression interpretedExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax2DigitNumber(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax4DigitNumber(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax5DigitNumber(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMinutes(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMonth(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMonthOfYear(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int monthOfYear)
        {
            throw new NotImplementedException();
        }

        public void ProcessProximityType(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, CertaintyType proximityType)
        {
            throw new NotImplementedException();
        }

        public void ProcessRangePointer(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRangePointer rangePointer)
        {
            throw new NotImplementedException();
        }

        public void ProcessRemaining(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRepeater repeaterExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterIndicator(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateRepeaterIndicator repeaterIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessSeason(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int seasonOfYear)
        {
            throw new NotImplementedException();
        }

        public void ProcessSeconds(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeConjointer(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeFraction(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeFraction timeFraction)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeOfDay(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, RangeWrapper timeOfDay)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateTimeUnit(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit timeUnit)
        {
            throw new NotImplementedException();
        }

        public void ProcessYear(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, ChronoxTime time)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeZoneInformation(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }
    }
}
