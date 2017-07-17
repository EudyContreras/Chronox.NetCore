using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers.Offsets
{
    class OffsetFraction : IOffset
    {
        public int Offset { get; set; } = int.MinValue;

        public OffsetFraction(int offset)
        {
            this.Offset = offset;
        }
    }
}

