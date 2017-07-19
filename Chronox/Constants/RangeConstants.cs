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
        public static readonly RangeWrapper AM_RANGE = new RangeWrapper(0, 11);

        public static readonly RangeWrapper PM_RANGE = new RangeWrapper(12, 23);

        public static readonly RangeWrapper MORNING_RANGE = new RangeWrapper(6, 11);

        public static readonly RangeWrapper AFTERNOON_RANGE = new RangeWrapper(13, 17);

        public static readonly RangeWrapper EVENING_RANGE = new RangeWrapper(18, 20);

        public static readonly RangeWrapper NIGHT_RANGE = new RangeWrapper(21, 23);
    }
}
