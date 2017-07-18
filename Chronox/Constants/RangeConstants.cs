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
        public static readonly TimeRange AM_RANGE = new TimeRange(0, 11);

        public static readonly TimeRange PM_RANGE = new TimeRange(12, 23);

        public static readonly TimeRange MORNING_RANGE = new TimeRange(6, 11);

        public static readonly TimeRange AFTERNOON_RANGE = new TimeRange(13, 17);

        public static readonly TimeRange EVENING_RANGE = new TimeRange(18, 20);

        public static readonly TimeRange NIGHT_RANGE = new TimeRange(21, 23);
    }
}
