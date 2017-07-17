using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers.Offsets
{
    class OffsetMinute : IOffset
    {
        public int Offset { get; set; } = int.MinValue;

        public OffsetMinute(int offset)
        {
            this.Offset = offset;
        }
    }
}
