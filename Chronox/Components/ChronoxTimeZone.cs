using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Components
{
    public class ChronoxTimeZone
    {
        public TimeSpan Offset { get; set; }

        public string UtcOffset { get; set; }

        public string Abbreviation { get; set; }

        public string StandardName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{StandardName}  {Offset}";
        }
    }
}
