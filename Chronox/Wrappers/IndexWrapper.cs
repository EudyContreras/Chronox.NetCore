using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    public class IndexWrapper
    {
        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public IndexWrapper(int start, int end)
        {
            StartIndex = start;
            EndIndex = end;
        }
    }
}
