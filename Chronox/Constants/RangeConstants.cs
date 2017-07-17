using Chronox.Helpers;
using Chronox.Utilities;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Constants
{
    internal class RangeConstants
    {
        public static readonly TimeRange AM_RANGE = new TimeRange(
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 0), 
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 11)); // 12am-12pm

        public static readonly TimeRange PM_RANGE = new TimeRange(
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 12), 
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 23)); // 12pm-12am

        public static readonly TimeRange MORNING_RANGE = new TimeRange(
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 6), 
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 11)); // 6am-12pm

        public static readonly TimeRange AFTERNOON_RANGE = new TimeRange(
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 13), 
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 17)); // 12pm-5pm

        public static readonly TimeRange EVENING_RANGE = new TimeRange(
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 18), 
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 20)); // 5pm-8pm

        public static readonly TimeRange NIGHT_RANGE = new TimeRange(
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 21), 
            TimeUtility.Convert(DateTimeUnit.Hour, DateTimeUnit.Hour, 23)); // 8pm-12am
    }
}
