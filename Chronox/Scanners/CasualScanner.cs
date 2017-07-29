using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Chronox.Wrappers;
using Chronox.Constants;
using Chronox.Utilities.Extenssions;
using Chronox.Interfaces;

namespace Chronox.Scanners
{
    public class CasualScanner : IChronoxScanner
    {
        public string ScannerTag()
        {
            return GetType().Name;
        }
        public ScanWrapper Scan(ChronoxSettings option, string expression)
        {
           return new ScanWrapper(this);
        }
    }
}
