using Chronox.Interfaces;
using Chronox.Processors;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Abstractions
{
    internal abstract class AbstractPreProcessor : IProcessor
    {
        public ProcessorType Type
        {
            get => ProcessorType.PreProcessor;

            set { }
        }

        protected abstract string ProcessExpression(ChronoxOption options, string expression);
    
        public string PreProcess(ChronoxOption options, string text)
        {
            return ProcessExpression(options,text);
        }

        public List<ChronoxDateTimeExtraction> PostProcess(ChronoxOption options, List<ChronoxDateTimeExtraction> extractions, string text)
        {
            throw new NotImplementedException();
        }
    }
}
