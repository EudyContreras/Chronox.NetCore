using Chronox.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Enumerations;

namespace Chronox
{
    internal class ChronoxDurationExtraction : IChronoxExtraction
    {
        public ExtractionResultType ResultType => ExtractionResultType.Duration;
    }
}
