using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers
{
    public class RangeWrapper
    {
        public static readonly RangeWrapper Default = DefaultRange();

        public TimeOfDay TimeOfDay { get; private set; }

        public long Start { get; private set; }

        public long End { get; private set; }

        public RangeWrapper(TimeOfDay timeOfDay, long start, long end)
        {
            TimeOfDay = timeOfDay;
            Start = start;
            End = end;
        }

        private static RangeWrapper DefaultRange()
        {
            return new RangeWrapper(TimeOfDay.Default, -1, -1);
        }

        public bool Contains(long point) => Start <= point && End >= point;

        public bool Contains(RangeWrapper range) => (Start <= range.Start && End >= range.End);

    }
}
