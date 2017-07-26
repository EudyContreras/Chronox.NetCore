﻿using Chronox.Utilities;
using Chronox.Utilities.Extenssions;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox
{
    public class ChronoxTimeSetBuilder
    {

        private ChronoxSettings settings;

        public DateExtraction Wrapper { get; set; }

        private ChronoxDateTimeExtraction ParentExtraction { get; set; }

        private readonly Dictionary<DateTimeUnit, int> knownValues = new Dictionary<DateTimeUnit, int>();

        private readonly Dictionary<DateTimeUnit, int> impliedValues = new Dictionary<DateTimeUnit, int>();

        public DateTime DateTime() => Merge(Date(), Time());

        public ChronoxTimeSetBuilder(ChronoxSettings settings)
        {
            this.settings = settings;
        }

        internal void ImplyDefault()
        {
            var dateTime = settings.ReferenceDate;

            ImplyValue(DateTimeUnit.Year, dateTime.Year);
            ImplyValue(DateTimeUnit.Month, dateTime.Month);
            ImplyValue(DateTimeUnit.Day, dateTime.Day);
            ImplyValue(DateTimeUnit.Hour, dateTime.Hour);
            ImplyValue(DateTimeUnit.Minute, dateTime.Minute);
            ImplyValue(DateTimeUnit.Second, dateTime.Second);
        }

        internal void NormalizeDateValues(DateTime now, DateTime date, ChronoxSettings settings)
        {
            if (!impliedValues.ContainsKey(DateTimeUnit.Year) && !knownValues.ContainsKey(DateTimeUnit.Year))
            {
                impliedValues.Add(DateTimeUnit.Year, date.Year);
            }
            if (!impliedValues.ContainsKey(DateTimeUnit.Month) && !knownValues.ContainsKey(DateTimeUnit.Month))
            {
                impliedValues.Add(DateTimeUnit.Month, date.Month);
            }
            if (!impliedValues.ContainsKey(DateTimeUnit.Day) && !knownValues.ContainsKey(DateTimeUnit.Day))
            {
                impliedValues.Add(DateTimeUnit.Day, date.Day);
            }
        }

        internal void NormalizeTimeValues(DateTime now, DateTime date, ChronoxSettings settings)
        {
            if (!impliedValues.ContainsKey(DateTimeUnit.Hour) && !knownValues.ContainsKey(DateTimeUnit.Hour))
            {
                if(now.Day != date.Day || now.Month != date.Month || now.Year != date.Year)
                {
                    impliedValues.Add(DateTimeUnit.Hour, settings.PreferedHour);
                }
                else
                {
                    impliedValues.Add(DateTimeUnit.Hour, date.Hour);
                }
            }
            if (!impliedValues.ContainsKey(DateTimeUnit.Minute) && !knownValues.ContainsKey(DateTimeUnit.Minute))
            {
                if (now.Day != date.Day || now.Month != date.Month || now.Year != date.Year)
                {
                    impliedValues.Add(DateTimeUnit.Minute, settings.PreferedMinute);
                }
                else
                {
                    impliedValues.Add(DateTimeUnit.Minute, date.Minute);
                }
            }
            if (!impliedValues.ContainsKey(DateTimeUnit.Second) && !knownValues.ContainsKey(DateTimeUnit.Second))
            {
                if (now.Day != date.Day || now.Month != date.Month || now.Year != date.Year)
                {
                    impliedValues.Add(DateTimeUnit.Second, settings.PreferedSecond);
                }
                else
                {
                    impliedValues.Add(DateTimeUnit.Second, date.Second);
                }
            }
        }

        public ChronoxDate Date()
        {
            var dateValues = new Dictionary<DateTimeUnit, int>();

            dateValues.AddAll(this.impliedValues);

            dateValues.AddAll(this.knownValues);

            var date = new ChronoxDate(
                    dateValues[DateTimeUnit.Year],
                    dateValues[DateTimeUnit.Month],
                    dateValues[DateTimeUnit.Day]);

            return date;
        }

        private DateTime Merge(ChronoxDate date, ChronoxTime time) => new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        

        public ChronoxTime Time()
        {
            var timeValues = new Dictionary<DateTimeUnit, int>();

            timeValues.AddAll(this.impliedValues);

            timeValues.AddAll(this.knownValues);

            var time = new ChronoxTime(
                    timeValues[DateTimeUnit.Hour],
                    timeValues[DateTimeUnit.Minute],
                    timeValues[DateTimeUnit.Second]);

            return time;
        }

        internal int GetValue(DateTimeUnit unit)
        {
            if (knownValues.ContainsKey(unit))
            {
                return knownValues[unit];
            }

            if (this.impliedValues.ContainsKey(unit))
            {
                return impliedValues[unit];
            }

            return int.MinValue;
        }

        internal bool IsValueCertain(DateTimeUnit unit) => knownValues.ContainsKey(unit);


        internal bool IsValueImplied(DateTimeUnit unit) => impliedValues.ContainsKey(unit);


        internal bool ContainsValue(DateTimeUnit unit) => IsValueCertain(unit) || IsValueImplied(unit);
        

        internal void ImplyValue(DateTimeUnit unit, int value)
        {
            if (!knownValues.ContainsKey(unit))
            {
                if (impliedValues.ContainsKey(unit))
                {
                    impliedValues[unit] = value;
                }
                else
                {
                    impliedValues.Add(unit, value);
                }
            }
        }

        internal void AssignValue(DateTimeUnit unit, int value)
        {
            if (!knownValues.ContainsKey(unit))
            {
                knownValues.Add(unit, value);
            }
 
            impliedValues.Remove(unit);
        }

        internal void ReAssignValue(DateTimeUnit unit, int value)
        {
            if (!knownValues.ContainsKey(unit))
            {
                knownValues.Add(unit, value);
            }
            else
            {
                knownValues[unit] = value;
            }

            impliedValues.Remove(unit);
        }

        internal void UnAssignValue(DateTimeUnit unit)
        {
            if (knownValues.ContainsKey(unit))
            {
                ImplyValue(unit, knownValues[unit]);
                
                knownValues.Remove(unit);
            }      
        }
    }
}
