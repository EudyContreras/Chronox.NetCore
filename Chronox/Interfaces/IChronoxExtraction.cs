using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Interfaces
{
    public interface IChronoxExtraction
    {
        ExtractionResultType ResultType { get; }

        string Extraction { get; set; }
    }
}
