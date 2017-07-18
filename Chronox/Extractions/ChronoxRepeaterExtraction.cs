using Chronox.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Enumerations;

namespace Chronox
{
    internal class ChronoxRepeaterExtraction : IChronoxExtraction
    {
        public ExtractionResultType ResultType => ExtractionResultType.Repeater;
    }
}
