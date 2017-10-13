
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
    public class TimeSetHelper : IChronoxParseHelper<ChronoxTimeSetExtraction>
    {
        public void DetermineTimeMeridiam(List<GroupWrapper> foundGroups, GroupWrapper meridiam, ChronoxBuildInformation information, ref DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeMeridiam(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeMeridiam timeMeridiam)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWord(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWordCardinal(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWordOrdinal(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord)
        {
            throw new NotImplementedException();
        }

        public void ProcessDecimalNumber(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessCasualExpression(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateCasualExpression casualExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateBigEndian(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateLittleEndian(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateMiddleEndian(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDay(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOffset(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOffset)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOfWeekType(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DayOfWeekType dayOfWeekType)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOfWeek(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteHours(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteMinutes(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteSeconds(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationExpression(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationExpression durationExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationIndicator(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationIndicator durationIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessGrabberExpression(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRelation grabberExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessHours(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessInterpretedExpression(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeExpression interpretedExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax2DigitNumber(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax4DigitNumber(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax5DigitNumber(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMinutes(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMonth(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMonthOfYear(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int monthOfYear)
        {
            throw new NotImplementedException();
        }

        public void ProcessProximityType(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, CertaintyType proximityType)
        {
            throw new NotImplementedException();
        }

        public void ProcessRangePointer(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRangePointer rangePointer)
        {
            throw new NotImplementedException();
        }

        public void ProcessRemaining(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterExpression(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRepeater repeaterExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterIndicator(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateRepeaterIndicator repeaterIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessSeason(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int seasonOfYear)
        {
            throw new NotImplementedException();
        }

        public void ProcessSeconds(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeConjointer(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeFraction(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeFraction timeFraction)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeOfDay(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, RangeWrapper timeOfDay)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateTimeUnit(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit timeUnit)
        {
            throw new NotImplementedException();
        }

        public void ProcessYear(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeExpression(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, ChronoxTime time)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeZoneInformation(ChronoxTimeSetExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }
    }
}
