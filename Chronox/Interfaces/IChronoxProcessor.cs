using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Processors
{
    internal interface IChronoxProcessor
    {
        string PreProcess(ChronoxSettings settings, string text);

        List<ChronoxDateTimeExtraction> PostProcess(ChronoxSettings settings, List<ChronoxDateTimeExtraction> extractions, string text);

        ProcessorType Type { get; set; }
    }
}
