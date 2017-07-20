﻿using Chronox.Interfaces;
using Chronox.Utilities.Extenssions;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox
{
    public class ChronoxTimeRangeExtraction : IChronoxExtraction, IComparable<ChronoxDateTimeExtraction>
    {

        public int Index { get; set; }

        public string Extraction { get; set; }

        public string Original { get; set; }

        public DateTime ReferenceDate { get; set; }

        private ChronoxDateTimeBuilder Start { get; set; }

        private ChronoxDateTimeBuilder End { get; set; }

        internal static ChronoxTimeRangeExtraction EmptyExtraction { get; set; } = null;

        public ExtractionResultType ResultType => ExtractionResultType.TimeRange;

        internal DateRangePointer Pointer { get; set; } = DateRangePointer.Start;

        private ChronoxTimeRangeExtraction() { }

        public ChronoxTimeRangeExtraction(ChronoxSettings settings, DateTime referenceDate, int index, string extraction, string text) : this()
        {
            this.Index = index;
            this.Original = text;
            this.Extraction = extraction;
            this.ReferenceDate = referenceDate;
            this.Start = new ChronoxDateTimeBuilder(settings);
        }

        public int CompareTo(ChronoxDateTimeExtraction other)
        {
            return this.Index - other.Index;
        }

        public override string ToString()
        {
            return Extraction;
        }

        internal ChronoxDateTimeBuilder GetCurrent()
        {
            return Get(Pointer);
        }

        internal void Set(DateRangePointer pointer, ChronoxDateTimeBuilder component)
        {
            switch (pointer)
            {
                case DateRangePointer.Start:

                    Start = component;

                    break;

                case DateRangePointer.End:

                    End = component;

                    break;
            }
        }

        internal ChronoxDateTimeBuilder Get(DateRangePointer pointer)
        {
            switch (pointer)
            {
                case DateRangePointer.Start:

                    return Start;

                case DateRangePointer.End:

                    return End;

                default:

                    return Start;
            }
        }
    }
}
