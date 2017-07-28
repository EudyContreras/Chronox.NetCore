using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Interfaces
{
    public interface IChronoxExtraction : IComparable<IChronoxExtraction>, IEquatable<IChronoxExtraction>, IEqualityComparer<IChronoxExtraction>
    {
        ExtractionResultType ResultType { get; }

        string ProcessedString { get; set; }

        string Original { get; set; }

        string Extraction { get; set; }

        int StartIndex { get; set; }

        int EndIndex { get; set; }
    }
}
