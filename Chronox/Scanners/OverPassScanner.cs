using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Chronox.Wrappers;
using Chronox.Constants;
using Chronox.Utilities.Extenssions;
using Chronox.Interfaces;
using Enumerations;

namespace Chronox.Scanners
{
    public class OverPassScanner : IChronoxScanner
    {
        public string ScannerTag()
        {
            return GetType().Name;
        }

        public ScanWrapper Scan(ChronoxSettings settings, string input)
        {
            return new ScanWrapper(this);
        }
    }
}
