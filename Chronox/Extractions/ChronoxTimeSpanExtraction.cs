using Chronox.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Enumerations;

namespace Chronox
{
    public class ChronoxTimeSpanExtraction : IChronoxExtraction
    {
        public ExtractionResultType ResultType => ExtractionResultType.TimeSpan;

        public string Extraction { get; set; }

        internal static ChronoxTimeSpanExtraction EmptyExtraction { get; set; } = null;

        public int CompareTo(IChronoxExtraction other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IChronoxExtraction other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(IChronoxExtraction x, IChronoxExtraction y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(IChronoxExtraction obj)
        {
            throw new NotImplementedException();
        }
    }
}
