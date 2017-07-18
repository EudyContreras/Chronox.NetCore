using Chronox.Interfaces;
using Chronox.Processors;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Abstractions
{
    internal abstract class AbstractPostProcessor : IChronoxProcessor
    {
        public ProcessorType Type
        {
            get => ProcessorType.PostProcessor;

            set { }
        }

        public abstract List<ChronoxDateTimeExtraction> ProcessExpression(ChronoxSettings settings, List<ChronoxDateTimeExtraction> extractions, string text);

        public string PreProcess(ChronoxSettings settings, string text)
        {
            return string.Empty;
        }

        public List<ChronoxDateTimeExtraction> PostProcess(ChronoxSettings settings, List<ChronoxDateTimeExtraction> extractions, string text)
        {
            return ProcessExpression(settings, extractions, text);
        }
    }
}
