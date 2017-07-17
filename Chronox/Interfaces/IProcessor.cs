using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Processors
{
    internal interface IProcessor
    {
        string PreProcess(ChronoxOption options, string text);

        List<ChronoxDateTimeExtraction> PostProcess(ChronoxOption options, List<ChronoxDateTimeExtraction> extractions, string text);

        ProcessorType Type { get; set; }
    }
}
