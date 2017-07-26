using Chronox.Interfaces;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox
{
    public class ChronoxResult
    {
        public ExtractionResultType ResultType { get; private set; }

        public ChronoxTimeRangeExtraction TimeRangeExtraction { get; set; }

        public ChronoxTimeSpanExtraction TimeSpanExtraction { get; set; }

        public ChronoxTimeSetExtraction TimeSetExtraction { get; set; }

        public ChronoxDateTimeExtraction DateTimeExtraction { get; set; }

        public ChronoxResult() { }

        public ChronoxResult(IChronoxExtraction extraction)
        {
            Initialize(extraction);
        }

        public IChronoxExtraction Result
        {
            get
            {
                return GetResult(ResultType);
            }
            set
            {
                SetResult(value);
            }
        }

        public IChronoxExtraction Initialize(IChronoxExtraction extraction)
        {
            ResultType = extraction.ResultType;

            switch (extraction.ResultType)
            {
                case ExtractionResultType.TimeSpan:
                    return TimeSpanExtraction = (ChronoxTimeSpanExtraction)extraction;
                case ExtractionResultType.DateTime:
                    return DateTimeExtraction = (ChronoxDateTimeExtraction)extraction;
                case ExtractionResultType.TimeSet:
                    return TimeSetExtraction = (ChronoxTimeSetExtraction)extraction;
                case ExtractionResultType.TimeRange:
                    return TimeRangeExtraction = (ChronoxTimeRangeExtraction)extraction;
            }
            return null;
        }

        public IChronoxExtraction Initialize(ExtractionResultType type)
        {
            ResultType = type;

            switch (type)
            {
                case ExtractionResultType.TimeSpan:
                    return TimeSpanExtraction = ChronoxTimeSpanExtraction.EmptyExtraction;
                case ExtractionResultType.DateTime:
                    return DateTimeExtraction = ChronoxDateTimeExtraction.EmptyExtraction;
                case ExtractionResultType.TimeSet:
                    return TimeSetExtraction = ChronoxTimeSetExtraction.EmptyExtraction;
                case ExtractionResultType.TimeRange:
                    return TimeRangeExtraction = ChronoxTimeRangeExtraction.EmptyExtraction;
            }
            return null;
        }

        public IChronoxExtraction GetResult(ExtractionResultType resultType)
        {
            switch (resultType)
            {
                case ExtractionResultType.TimeSpan:
                    return TimeSpanExtraction;
                case ExtractionResultType.DateTime:
                    return DateTimeExtraction;
                case ExtractionResultType.TimeSet:
                    return TimeSetExtraction;
                case ExtractionResultType.TimeRange:
                    return TimeRangeExtraction;
            }

            return null;
        }

        public void SetResult(IChronoxExtraction extraction)
        {
            if (extraction == null) return;

            ResultType = extraction.ResultType;

            switch (extraction.ResultType)
            {
                case ExtractionResultType.TimeSpan:
                    TimeSpanExtraction = (ChronoxTimeSpanExtraction)extraction; ;
                    break;
                case ExtractionResultType.DateTime:
                    DateTimeExtraction = (ChronoxDateTimeExtraction)extraction; ;
                    break;
                case ExtractionResultType.TimeSet:
                    TimeSetExtraction = (ChronoxTimeSetExtraction)extraction; ;
                    break;
                case ExtractionResultType.TimeRange:
                    TimeRangeExtraction = (ChronoxTimeRangeExtraction)extraction; ;
                    break;
            }
        }
    }
}
