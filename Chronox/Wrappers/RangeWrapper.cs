using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers
{
    internal class RangeWrapper
    {
        public static readonly RangeWrapper Default = DefaultRange();

        public long Start { get; private set; }

        public long End { get; private set; }

        public RangeWrapper(long start, long end)
        {
            Start = start;
            End = end;
        }

        private static RangeWrapper DefaultRange()
        {
            return new RangeWrapper(-1, -1);
        }

        public bool Contains(long point) => Start <= point && End >= point;

        public bool Contains(RangeWrapper range) => (Start <= range.Start && End >= range.End);

    }
}
