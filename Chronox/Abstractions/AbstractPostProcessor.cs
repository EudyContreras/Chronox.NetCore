using Chronox.Interfaces;
using Chronox.Processors;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Abstractions
{
    public abstract class AbstractPostProcessor : IChronoxProcessor
    {
        public ProcessorType Type
        {
            get => ProcessorType.PostProcessor;

            set { }
        }

        public abstract List<IChronoxExtraction> ProcessExpression(ChronoxSettings settings, List<IChronoxExtraction> extractions, string text);

        public string PreProcess(ChronoxSettings settings, string text)
        {
            return string.Empty;
        }

        public List<IChronoxExtraction> PostProcess(ChronoxSettings settings, List<IChronoxExtraction> extractions, string text)
        {
            return ProcessExpression(settings, extractions, text);
        }
    }
}
