using Chronox.Interfaces;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox
{
    internal class ChronoxResult
    {
        public ExtractionResultType ResultType { get; private set; }

        private ChronoxDurationExtraction dateTimeDurationExtraction { get; set; }

        private ChronoxRangeExtraction dateTimeRangeExtraction { get; set; }

        private ChronoxRepeaterExtraction dateTimeRepeaterExtraction { get; set; }

        private ChronoxDateTimeExtraction dateTimeExtraction { get; set; }

        public ChronoxResult(IExtraction result)
        {
            ResultType = result.ResultType;
        }

        public IExtraction GetResult()
        {
            switch (ResultType)
            {
                case ExtractionResultType.Duration:
                    return dateTimeDurationExtraction;
                case ExtractionResultType.DateTime:
                    return dateTimeExtraction;
                case ExtractionResultType.Repeater:
                    return dateTimeRepeaterExtraction;
                case ExtractionResultType.DateRange:
                    return dateTimeRangeExtraction;
            }

            return null;
        }
    }
}
