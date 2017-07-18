using Chronox.Interfaces;
using Chronox.Processors;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Abstractions
{
    internal abstract class AbstractPreProcessor : IChronoxProcessor
    {
        public ProcessorType Type
        {
            get => ProcessorType.PreProcessor;

            set { }
        }

        protected abstract string ProcessExpression(ChronoxSettings settings, string expression);
    
        public string PreProcess(ChronoxSettings settings, string text)
        {
            return ProcessExpression(settings,text);
        }

        public List<ChronoxDateTimeExtraction> PostProcess(ChronoxSettings settings, List<ChronoxDateTimeExtraction> extractions, string text)
        {
            throw new NotImplementedException();
        }
    }
}
