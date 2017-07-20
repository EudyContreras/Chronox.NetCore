using Chronox.Helpers;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Chronox.Interfaces
{
    public interface IChronoxParseHelper<TResult>
    {
        void ProcessRepeaterIndicator(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateRepeaterIndicator repeaterIndicator);

        void ProcessRepeaterExpression(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRepeater repeaterExpression);

        void ProcessDurationIndicator(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationIndicator durationIndicator);

        void ProcessDurationExpression(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeDurationExpression durationExpression);

        void ProcessProximityType(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, CertaintyType proximityType);

        void ProcessMonth(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDay(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDateBigEndian(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper);

        void ProcessDateMiddleEndian(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper);

        void ProcessDateLittleEndian(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, GroupWrapper GroupWrapper);

        void ProcessMax5DigitNumber(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessMax4DigitNumber(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessMax2DigitNumber(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessInterpretedExpression(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeExpression interpretedExpression);

        void ProcessRangePointer(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRangePointer rangePointer);

        void ProcessGrabberExpression(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeRelation grabberExpression);

        void ProcessCasualExpression(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateCasualExpression casualExpression);

        void ProcessDateTimeUnit(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DateTimeUnit timeUnit);

        void ProcessNumericWord(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericValue);

        void ProcessNumericWordCardinal(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord);

        void ProcessNumericWordOrdinal(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int numericWord);

        void ProcessDayOffset(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOffset);

        void ProcessTimeOfDay(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, RangeWrapper timeOfDay);

        void ProcessMonthOfYear(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int monthOfYear);

        void ProcessDayOfWeek(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int dayOfWeek);

        void ProcessDayOfWeekType(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, DayOfWeekType dayOfWeekType);

        void ProcessSeason(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, int seasonOfYear);

        void ProcessYear(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessTimeExpression(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, ChronoxTime time);

        void ProcessHours(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessMinutes(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessSeconds(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDiscreteHours(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDiscreteMinutes(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessDiscreteSeconds(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, string value);

        void ProcessTimeFraction(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeFraction timeFraction);

        void ProcessTimeConjointer(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeConjointer value);

        void ProcessRemaining(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information);

        void ProcessTimeMeridiam(TResult result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxBuildInformation information, TimeMeridiam timeMeridiam);
    }
}