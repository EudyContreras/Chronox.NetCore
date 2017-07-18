using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Interfaces
{
    internal interface IChronoxScanner
    {
        ScanResult Scan(ChronoxSettings option, string expression);
    }
}
