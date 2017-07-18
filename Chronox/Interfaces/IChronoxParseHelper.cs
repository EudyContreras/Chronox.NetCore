using Chronox.Helpers;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Chronox.Interfaces
{
    internal interface IChronoxParseHelper
    {
        void ProcessRepeaterIndicator(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateRepeaterIndicator repeaterIndicator);

        void ProcessRepeaterExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRepeater repeaterExpression);

        void ProcessDurationIndicator(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationIndicator durationIndicator);

        void ProcessDurationExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationExpression durationExpression);

        void ProcessProximityType(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, CertaintyType proximityType);

        void ProcessMonth(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDay(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDateBigEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper);

        void ProcessDateMiddleEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper);

        void ProcessDateLittleEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper);

        void ProcessMax5DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessMax4DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessMax2DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessInterpretedExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeExpression interpretedExpression);

        void ProcessRangePointer(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRangePointer rangePointer);

        void ProcessGrabberExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRelation grabberExpression);

        void ProcessCasualExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateCasualExpression casualExpression);

        void ProcessDateTimeUnit(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit timeUnit);

        void ProcessNumericWord(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue);

        void ProcessNumericWordCardinal(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord);

        void ProcessNumericWordOrdinal(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord);

        void ProcessDayOffset(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOffset);

        void ProcessTimeOfDay(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRange timeOfDay);

        void ProcessMonthOfYear(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int monthOfYear);

        void ProcessDayOfWeek(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek);

        void ProcessSeason(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int seasonOfYear);

        void ProcessYear(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessTimeExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, ChronoxTimeComponent time);

        void ProcessHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessMinutes(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessSeconds(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDiscreteHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDiscreteMinutes(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDiscreteSeconds(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessTimeFraction(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeFraction timeFraction);

        void ProcessTimeConjointer(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer value);

        void ProcessRemaining(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information);

        void ProcessTimeMeridiam(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeMeridiam timeMeridiam);
    }
}