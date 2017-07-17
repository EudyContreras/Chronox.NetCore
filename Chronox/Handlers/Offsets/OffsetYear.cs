using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers.Offsets
{
    class OffsetYear : IOffset
    {
        public int Offset { get; set; } = int.MinValue;

        public OffsetYear(int offset)
        {
            this.Offset = offset;
        }
    }
}
