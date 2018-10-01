
using Enumerations;

namespace Chronox.Components
{
    public class ChronoxTimeSet
    {
        public DateRepeaterIndicator indicator { get; set; }

        public TimeRepeater Frequency { get; set; }

        public DateTimeUnit TimeUnit { get; set; }

        public ChronoxTime Time { get; set; }

    }
}
