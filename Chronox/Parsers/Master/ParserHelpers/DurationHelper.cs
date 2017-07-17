﻿using Chronox.Abstractions;
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
    internal class DurationHelper : IParseHelper
    {
        public void DetermineTimeMeridiam(List<GroupWrapper> foundGroups, GroupWrapper meridiam, ChronoxDateTimeInformation information, ref DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeMeridiam(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeMeridiam timeMeridiam)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWord(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int numericValue)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWordCardinal(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int numericWord)
        {
            throw new NotImplementedException();
        }

        public void ProcessNumericWordOrdinal(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int numericWord)
        {
            throw new NotImplementedException();
        }

        public void ProcessCasualExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, DateCasualExpression casualExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateBigEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateLittleEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateMiddleEndian(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, GroupWrapper GroupWrapper)
        {
            throw new NotImplementedException();
        }

        public void ProcessDay(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOffset(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int dayOffset)
        {
            throw new NotImplementedException();
        }

        public void ProcessDayOfWeek(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int dayOfWeek)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteMinutes(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDiscreteSeconds(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeDurationExpression durationExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessDurationIndicator(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeDurationIndicator durationIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessGrabberExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeRelation grabberExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessHours(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessInterpretedExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, DateTimeExpression interpretedExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax2DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax4DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMax5DigitNumber(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMinutes(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMonth(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessMonthOfYear(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int monthOfYear)
        {
            throw new NotImplementedException();
        }

        public void ProcessProximityType(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, CertaintyType proximityType)
        {
            throw new NotImplementedException();
        }

        public void ProcessRangePointer(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeRangePointer rangePointer)
        {
            throw new NotImplementedException();
        }

        public void ProcessRemaining(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeRepeater repeaterExpression)
        {
            throw new NotImplementedException();
        }

        public void ProcessRepeaterIndicator(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, DateRepeaterIndicator repeaterIndicator)
        {
            throw new NotImplementedException();
        }

        public void ProcessSeason(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, int seasonOfYear)
        {
            throw new NotImplementedException();
        }

        public void ProcessSeconds(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeConjointer(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeConjointer value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeFraction(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeFraction timeFraction)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeOfDay(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, TimeRange timeOfDay)
        {
            throw new NotImplementedException();
        }

        public void ProcessDateTimeUnit(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, DateTimeUnit timeUnit)
        {
            throw new NotImplementedException();
        }

        public void ProcessYear(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, string value)
        {
            throw new NotImplementedException();
        }

        public void ProcessTimeExpression(ChronoxDateTimeExtraction result, List<GroupWrapper> foundGroups, ref DateTime dateTime, ChronoxDateTimeInformation information, ChronoxTimeComponent time)
        {
            throw new NotImplementedException();
        }
    }
}
