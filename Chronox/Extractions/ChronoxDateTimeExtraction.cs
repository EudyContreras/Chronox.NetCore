using Chronox.Components;
using Chronox.Interfaces;
using Chronox.Utilities.Extenssions;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox
{
    public class ChronoxDateTimeExtraction : IChronoxExtraction
    {
        public string Original { get; set; }

        public int StartIndex { get; set; }

        public int EndIndex { get; set; }

        public string DatelessString { get; set; }

        public string Extraction { get; set; }

        public bool ValidDate { get; set; }

        public TimeSpan TimeOffset { get; set; }

        public DateTime ReferenceDate { get; set; }

        public ChronoxTimeZone TimeZone { get; set; }

        internal ChronoxDateTimeBuilder Builder { get; set; }

        internal static ChronoxDateTimeExtraction EmptyExtraction { get; set; } = null;

        public ExtractionResultType ResultType => ExtractionResultType.DateTime;

        private ChronoxDateTimeExtraction() { }

        public ChronoxDateTimeExtraction(ChronoxSettings settings, DateTime referenceDate, int index, string extraction, string text) : this()
        {
            this.StartIndex = index;
            this.EndIndex = index + extraction.Length;
            this.Original = text;
            this.Extraction = extraction;
            this.DatelessString = text.RemoveSubstrings(extraction);
            this.ReferenceDate = referenceDate;
            this.Builder = new ChronoxDateTimeBuilder(this);
        }

        public ChronoxDateTime DateTime
        {
            get
            {
                return Builder.DateTime;
            }
        }

        public int CompareTo(IChronoxExtraction other)
        {
            return this.StartIndex - ((ChronoxDateTimeExtraction)other).StartIndex;
        }

        public bool Equals(IChronoxExtraction x, IChronoxExtraction y)
        {
            return ((ChronoxDateTimeExtraction)x).StartIndex.Equals(((ChronoxDateTimeExtraction)y).StartIndex);
        }

        public int GetHashCode(IChronoxExtraction obj)
        {
            return ((ChronoxDateTimeExtraction)obj).StartIndex.GetHashCode();
        }

        public bool Equals(IChronoxExtraction other)
        {
            return StartIndex.Equals(((ChronoxDateTimeExtraction)other).StartIndex);
        }

        public override string ToString()
        {
            return Extraction;
        }   
    }
}
