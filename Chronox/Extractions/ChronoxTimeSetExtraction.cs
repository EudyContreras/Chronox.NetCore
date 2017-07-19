using Chronox.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Enumerations;

namespace Chronox
{
    internal class ChronoxTimeSetExtraction : IChronoxExtraction
    {
        public ExtractionResultType ResultType => ExtractionResultType.TimeSet;

        public string Extraction { get; set; }

        internal static ChronoxTimeSetExtraction EmptyExtraction { get; set; } = null;
    }
}
