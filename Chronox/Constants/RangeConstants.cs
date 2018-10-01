using Chronox.Helpers;
using Enumerations;

namespace Chronox.Constants
{
    public class RangeConstants
    {
        public static readonly RangeWrapper AM_RANGE = new RangeWrapper(TimeOfDay.Default, 0, 11);

        public static readonly RangeWrapper PM_RANGE = new RangeWrapper(TimeOfDay.Default, 12, 23);

        public static readonly RangeWrapper MORNING_RANGE = new RangeWrapper(TimeOfDay.Morning, 6, 11);

        public static readonly RangeWrapper AFTERNOON_RANGE = new RangeWrapper(TimeOfDay.Afternoon, 13, 17);

        public static readonly RangeWrapper EVENING_RANGE = new RangeWrapper(TimeOfDay.Evening, 18, 20);

        public static readonly RangeWrapper NIGHT_RANGE = new RangeWrapper(TimeOfDay.Night, 21, 23);
    }
}
