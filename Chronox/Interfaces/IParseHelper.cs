using Chronox.Helpers;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Chronox.Interfaces
{
    internal interface IParseHelper
    {
        void ProcessRepeaterIndicator(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, DateRepeaterIndicator repeaterIndicator);

        void ProcessRepeaterExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeRepeater repeaterExpression);

        void ProcessDurationIndicator(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeDurationIndicator durationIndicator);

        void ProcessDurationExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeDurationExpression durationExpression);

        void ProcessProximityType(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, CertaintyType proximityType);

        void ProcessMonth(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessDay(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessDateBigEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, GroupWrapper GroupWrapper);

        void ProcessDateMiddleEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, GroupWrapper GroupWrapper);

        void ProcessDateLittleEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, GroupWrapper GroupWrapper);

        void ProcessMax5DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessMax4DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessMax2DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessInterpretedExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, DateTimeExpression interpretedExpression);

        void ProcessRangePointer(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeRangePointer rangePointer);

        void ProcessGrabberExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeRelation grabberExpression);

        void ProcessCasualExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, DateCasualExpression casualExpression);

        void ProcessDateTimeUnit(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, DateTimeUnit timeUnit);

        void ProcessNumericWord(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int numericValue);

        void ProcessNumericWordCardinal(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int numericWord);

        void ProcessNumericWordOrdinal(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int numericWord);

        void ProcessDayOffset(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int dayOffset);

        void ProcessTimeOfDay(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeRange timeOfDay);

        void ProcessMonthOfYear(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int monthOfYear);

        void ProcessDayOfWeek(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int dayOfWeek);

        void ProcessSeason(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int seasonOfYear);

        void ProcessYear(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessTimeExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, ChronoxTimeComponent time);

        void ProcessHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessMinutes(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessSeconds(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessDiscreteHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessDiscreteMinutes(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessDiscreteSeconds(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value);

        void ProcessTimeFraction(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeFraction timeFraction);

        void ProcessTimeConjointer(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeConjointer value);

        void ProcessRemaining(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information);

        void ProcessTimeMeridiam(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeMeridiam timeMeridiam);
    }
}