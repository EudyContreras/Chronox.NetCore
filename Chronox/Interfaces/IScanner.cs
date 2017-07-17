using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Interfaces
{
    internal interface IScanner
    {
        ScanResult Scan(ChronoxOption option, string expression);
    }
}
