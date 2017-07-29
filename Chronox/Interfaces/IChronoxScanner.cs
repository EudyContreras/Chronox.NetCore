using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Interfaces
{
    public interface IChronoxScanner
    {
        string ScannerTag();

        ScanWrapper Scan(ChronoxSettings option, string expression);
    }
}
