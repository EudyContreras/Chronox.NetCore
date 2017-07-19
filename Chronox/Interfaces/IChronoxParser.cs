using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Interfaces
{
    internal interface IChronoxParser
    {
        List<IChronoxExtraction> Execute(string text, DateTime referenceDate, ChronoxSettings settings);
    }
}
