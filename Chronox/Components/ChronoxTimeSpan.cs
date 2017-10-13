using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    public class ChronoxTimeSpan
    {

        internal long Days { get; set; }

        internal long Hours { get; set; }

        internal long Minutes { get; set; }

        internal long Seconds { get; set; }

        public long MilliSeconds { get; set; }

        public long TotalMilliseconds { get; set; }

        public TimeSpan ToTimeSpan()
        {
            return TimeSpan.FromMilliseconds(TotalMilliseconds);
        }

        public override string ToString()
        {
            return TotalMilliseconds.ToString();
        }
    }
}
