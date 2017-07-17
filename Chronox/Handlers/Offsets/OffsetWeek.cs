using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers.Offsets
{
    class OffsetWeek : IOffset
    {
        public int Offset { get; set; } = int.MinValue;

        public OffsetWeek(int offset)
        {
            this.Offset = offset;
        }
    }
}

