using Chronox.Helpers;
using Enumerations;

namespace Chronox.Constants
{
    public class TimeRange
    {
        
        public RangeWrapper AM_RANGE = new RangeWrapper(TimeOfDay.Default, 0, 11);

        public RangeWrapper PM_RANGE = new RangeWrapper(TimeOfDay.Default, 12, 23);

        public RangeWrapper MORNING_RANGE = new RangeWrapper(TimeOfDay.Morning, 6, 11);

        public RangeWrapper AFTERNOON_RANGE = new RangeWrapper(TimeOfDay.Afternoon, 13, 17);

        public RangeWrapper EVENING_RANGE = new RangeWrapper(TimeOfDay.Evening, 18, 20);

        public RangeWrapper NIGHT_RANGE = new RangeWrapper(TimeOfDay.Night, 21, 23);

        public RangeWrapper GetRange(TimeOfDay timeOfDay){
            
            switch(timeOfDay){
                case TimeOfDay.Morning:
                    return MORNING_RANGE;
                case TimeOfDay.Afternoon:
                    return AFTERNOON_RANGE;
                case TimeOfDay.Evening:
                    return EVENING_RANGE;
                case TimeOfDay.Night:
                    return NIGHT_RANGE;
                default:
                    return AM_RANGE;

            }
        }
    }
}
