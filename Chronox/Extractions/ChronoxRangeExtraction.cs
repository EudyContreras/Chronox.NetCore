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
    internal class ChronoxRangeExtraction : IDateTimeRangeExtraction, IComparable<ChronoxDateTimeExtraction>
    {

        public int Index { get; set; }

        public string Extraction { get; set; }

        public string Original { get; set; }

        public DateTime ReferenceDate { get; set; }

        private ChronoxDateBuilder Start { get; set; }

        private ChronoxDateBuilder End { get; set; }

        internal static ChronoxDateTimeExtraction EmptyExtraction { get; set; } = null;

        public ExtractionResultType ResultType => ExtractionResultType.DateTimeRange;

        internal DateRangePointer Pointer { get; set; } = DateRangePointer.Start;

        private ChronoxRangeExtraction() { }

        public ChronoxRangeExtraction(ChronoxSettings settings, DateTime referenceDate, int index, string extraction, string text) : this()
        {
            this.Index = index;
            this.Original = text;
            this.Extraction = extraction;
            this.ReferenceDate = referenceDate;
            this.Start = new ChronoxDateBuilder(settings);
        }

        public int CompareTo(ChronoxDateTimeExtraction other)
        {
            return this.Index - other.Index;
        }

        public override string ToString()
        {
            return Extraction;
        }

        internal ChronoxDateBuilder GetCurrent()
        {
            return Get(Pointer);
        }

        internal void Set(DateRangePointer pointer, ChronoxDateBuilder component)
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

        internal ChronoxDateBuilder Get(DateRangePointer pointer)
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
