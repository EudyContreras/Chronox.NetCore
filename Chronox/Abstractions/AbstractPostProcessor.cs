using Chronox.Interfaces;
using Chronox.Processors;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Abstractions
{
    internal abstract class AbstractPostProcessor : IProcessor
    {
        public ProcessorType Type
        {
            get => ProcessorType.PostProcessor;

            set { }
        }

        public abstract List<ChronoxDateTimeExtraction> ProcessExpression(ChronoxOption options, List<ChronoxDateTimeExtraction> extractions, string text);

        public string PreProcess(ChronoxOption options, string text)
        {
            return string.Empty;
        }

        public List<ChronoxDateTimeExtraction> PostProcess(ChronoxOption options, List<ChronoxDateTimeExtraction> extractions, string text)
        {
            return ProcessExpression(options, extractions, text);
        }
    }
}
