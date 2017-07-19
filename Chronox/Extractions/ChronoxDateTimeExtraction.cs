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
    internal class ChronoxDateTimeExtraction : IChronoxExtraction, IComparable<ChronoxDateTimeExtraction>
    {

        public int Index { get; set; }

        public string Extraction { get; set; }

        public string Original { get; set; }

        public DateTime ReferenceDate { get; set; }

        private ChronoxDateTimeBuilder DateTime { get; set; }

        internal static ChronoxDateTimeExtraction EmptyExtraction { get; set; } = null;

        public ExtractionResultType ResultType => ExtractionResultType.DateTime;

        private ChronoxDateTimeExtraction() { }

        public ChronoxDateTimeExtraction(ChronoxSettings settings, DateTime referenceDate, int index, string extraction, string text) : this()
        {
            this.Index = index;
            this.Original = text;
            this.Extraction = extraction;
            this.ReferenceDate = referenceDate;
            this.DateTime = new ChronoxDateTimeBuilder(settings);
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
            return DateTime;
        }
    }
}
