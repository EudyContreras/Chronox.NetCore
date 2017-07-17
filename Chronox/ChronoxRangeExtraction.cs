using Chronox.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Enumerations;

namespace Chronox
{
    internal class ChronoxRangeExtraction : IExtraction
    {
        public ExtractionResultType ResultType => ExtractionResultType.DateRange;
    }
}
