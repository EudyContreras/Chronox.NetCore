using Chronox.Interfaces;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Processors
{
    public interface IChronoxProcessor
    {
        string PreProcess(ChronoxSettings settings, string text);

        List<IChronoxExtraction> PostProcess(ChronoxSettings settings, List<IChronoxExtraction> extractions, string text);

        ProcessorType Type { get; set; }
    }
}
