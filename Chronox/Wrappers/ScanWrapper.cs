using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    public class ScanWrapper
    {
        public List<ContainsWrapper> ResultWrappers { get; set; } = new List<ContainsWrapper>();

        public string ScannedExpression { get; set; } 

        public string NormalizedExpression { get; set; }
    }
}
