using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers
{
    internal class TimeRange
    {
        public static readonly TimeRange Default = DefaultRange();

        public long Start { get; private set; }

        public long End { get; private set; }

        public TimeRange(long start, long end)
        {
            Start = start;
            End = end;
        }

        private static TimeRange DefaultRange()
        {
            return new TimeRange(-1, -1);
        }

        public bool Contains(long point) => Start <= point && End >= point;

        public bool Contains(TimeRange range) => (Start <= range.Start && End >= range.End);

    }
}
