using Chronox.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Wrappers
{
    public struct DateExtraction
    {
        public ChronoxDateTime DateTime { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public string DatelessString { get; set; }

        public string DateString { get; set; }

        public bool ValidDate { get; set; }
    }
}
