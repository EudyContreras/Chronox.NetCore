
using System;
using System.Collections.Generic;
using System.Text;
using Chronox.Wrappers;
using System.Text.RegularExpressions;
using Chronox.Interfaces;
using Chronox.Helpers;
using Enumerations;
using Chronox.Utilities;

namespace Chronox.Parsers.General.ParserHelpers
{
    public class TimeSpanHelper : IChronoxParser<ChronoxTimeSpanExtraction>
    {
        public void DetermineTimeMeridiam(List<GroupWrapper> foundGroups, GroupWrapper meridiam, ChronoxBuildInformation information, ref DateTime dateTime)
        {
           
        }

        public void ProcessTimeMeridiam(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeMeridiam timeMeridiam)
        {
           
        }

        public void ProcessNumericWord(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue)
        {
           
        }

        public void ProcessNumericWordCardinal(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord)
        {
           
        }

        public void ProcessNumericWordOrdinal(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord)
        {
           
        }

        public void ProcessCasualExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateCasualExpression casualExpression)
        {
           
        }

        public void ProcessDateBigEndian(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
           
        }

        public void ProcessDateLittleEndian(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
           
        }

        public void ProcessDateMiddleEndian(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
           
        }

        public void ProcessDay(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessDayOffset(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOffset)
        {
           
        }

        public void ProcessDayOfWeekType(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DayOfWeekType dayOfWeekType)
        {
           
        }

        public void ProcessDayOfWeek(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek)
        {
           
        }

        public void ProcessDiscreteHours(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessDiscreteMinutes(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessDiscreteSeconds(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessDurationExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationExpression durationExpression)
        {
           
        }

        public void ProcessDurationIndicator(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationIndicator durationIndicator)
        {
           
        }

        public void ProcessGrabberExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRelation grabberExpression)
        {
           
        }

        public void ProcessHours(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessInterpretedExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeExpression interpretedExpression)
        {
           
        }

        public void ProcessMax2DigitNumber(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessMax4DigitNumber(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessMax5DigitNumber(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessMinutes(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessMonth(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessMonthOfYear(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int monthOfYear)
        {
           
        }

        public void ProcessProximityType(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, CertaintyType proximityType)
        {
           
        }

        public void ProcessRangePointer(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRangePointer rangePointer)
        {
           
        }

        public void ProcessRemaining(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information)
        {
           
        }

        public void ProcessRepeaterExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRepeater repeaterExpression)
        {
           
        }

        public void ProcessRepeaterIndicator(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateRepeaterIndicator repeaterIndicator)
        {
           
        }

        public void ProcessSeason(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int seasonOfYear)
        {
           
        }

        public void ProcessSeconds(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessTimeConjointer(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer value)
        {
           
        }

        public void ProcessTimeFraction(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeFraction timeFraction)
        {
           
        }

        public void ProcessTimeOfDay(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, RangeWrapper timeOfDay)
        {
           
        }

        public void ProcessDateTimeUnit(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit timeUnit)
        {
            var value = 1M;

            if(information.DecimalValues.Count > 0)
            {
                value = information.DecimalValues.Dequeue();
            }

            var conversion = (long)ChronoxTimeSpanUtility.Convert(timeUnit, DateTimeUnit.Millisecond, value);

            result.TimeSpan.TotalMilliseconds += conversion;
        }

        public void ProcessDecimalNumber(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
            if(decimal.TryParse(value, out decimal decimalValue))
            {
                information.DecimalValues.Enqueue(decimalValue);
            }
        }

        public void ProcessYear(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value)
        {
           
        }

        public void ProcessTimeExpression(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, ChronoxTime time)
        {
           
        }

        public void ProcessTimeZoneInformation(ChronoxTimeSpanExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper)
        {
           
        }
    }
}
