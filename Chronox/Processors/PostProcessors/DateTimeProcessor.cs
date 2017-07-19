﻿using Chronox.Abstractions;
using Chronox.Interfaces;
using Chronox.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Processors.PostProcessors
{
    internal class StandardPostProcessor : AbstractPostProcessor
    {
        public override List<IChronoxExtraction> ProcessExpression(ChronoxSettings settings, List<IChronoxExtraction> extractions, string text)
        {
            var referenceDate = settings.ReferencDate.GetDateComponent();

            foreach(var extraction in extractions)
            {
                //if(extraction.GetCurrent().Date().CompareTo(referenceDate) != 0)
                //{

                //}
            }

            return extractions;
        }
    }
}
