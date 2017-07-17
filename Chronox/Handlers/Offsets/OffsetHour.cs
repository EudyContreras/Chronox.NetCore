using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers.Offsets
{
    class OffsetHour : IOffset
    {
        public int Offset { get; set; } = int.MinValue;

        public OffsetHour(int offset)
        {
            this.Offset = offset;
        }
    }
}
