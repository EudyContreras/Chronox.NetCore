using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Helpers.Offsets
{
    class OffsetDay : IOffset
    {
        public int Offset { get; set;} = int.MinValue;

        public OffsetDay(int offset)
        {
            this.Offset = offset;
        }
    }
}
