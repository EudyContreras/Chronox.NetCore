using Chronox.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Enumerations;
using Chronox.Wrappers;

namespace Chronox
{
    public class ChronoxTimeSpanExtraction : IChronoxExtraction
    {

        public string Original { get; set; }

        public string ProcessedString { get; set; }

        public string Extraction { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public ChronoxTimeSpan TimeSpan { get; set; }

        public ExtractionResultType ResultType => ExtractionResultType.TimeSpan;

        internal static ChronoxTimeSpanExtraction EmptyExtraction { get; set; } = null;

        private ChronoxTimeSpanExtraction() { }

        public ChronoxTimeSpanExtraction(ChronoxSettings settings, int index, string extraction, string text) : this()
        {
            this.StartIndex = index;
            this.EndIndex = index + extraction.Length;
            this.Original = text;
            this.Extraction = extraction;
            this.TimeSpan = new ChronoxTimeSpan();
        }

        public int CompareTo(IChronoxExtraction other)
        {
            return this.StartIndex - other.StartIndex;
        }

        public bool Equals(IChronoxExtraction x, IChronoxExtraction y)
        {
            return x.StartIndex.Equals(y.StartIndex);
        }

        public int GetHashCode(IChronoxExtraction obj)
        {
            return obj.StartIndex.GetHashCode();
        }

        public bool Equals(IChronoxExtraction other)
        {
            return StartIndex.Equals(other.StartIndex);
        }

        public override string ToString()
        {
            return Extraction;
        }
    }
}
