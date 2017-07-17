using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Interfaces
{
    internal interface IParser
    {
        List<ChronoxDateTimeExtraction> Execute(string text, DateTime referenceDate, ChronoxOption options);
    }
}
