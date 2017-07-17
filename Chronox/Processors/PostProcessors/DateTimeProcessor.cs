using Chronox.Abstractions;
using Chronox.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Processors.PostProcessors
{
    internal class StandardPostProcessor : AbstractPostProcessor
    {
        public override List<ChronoxDateTimeExtraction> ProcessExpression(ChronoxOption options, List<ChronoxDateTimeExtraction> extractions, string text)
        {
            var referenceDate = DateTime.Now.GetDateComponent();

            foreach(var extraction in extractions)
            {
                if(extraction.Get(Enumerations.DateRangePointer.Start).Date().CompareTo(referenceDate) != 0)
                {

                }
            }

            return extractions;
        }
    }
}
