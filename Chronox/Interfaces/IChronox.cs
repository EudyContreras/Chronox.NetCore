using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Interfaces
{
    internal interface IChronox
    {

        ChronoxSettings Settings { get; set; }


        ResultWrapper Parse(string input);


        ResultWrapper Parse(DateTime referenceDate, string input);


        ResultWrapper Parse(ChronoxSettings settings, string input);


        ResultWrapper Parse(ChronoxSettings settings, DateTime referenceDate, string input);


        bool TryParse(string input, out ResultWrapper result);


        bool TryParse(DateTime referenceDate, string input, out ResultWrapper result);


        bool TryParse(ChronoxSettings settings, string input, out ResultWrapper result);


        bool TryParse(ChronoxSettings settings, DateTime referenceDate, string input, out ResultWrapper result);


        IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(string input);


        IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(DateTime referenceDate, string input);


        IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(ChronoxSettings settings, string input);


        IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(ChronoxSettings settings, DateTime referenceDate, string input);


        IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(string input);


        IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(DateTime referenceDate, string input);


        IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(ChronoxSettings settings, string input);


        IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(ChronoxSettings settings, DateTime referenceDate, string input);


        IReadOnlyList<ChronoxTimeSpanExtraction> ParseTimeSpan(string input);


        IReadOnlyList<ChronoxTimeSpanExtraction> ParseTimeSpan(ChronoxSettings settings, string input);


        IReadOnlyList<ChronoxTimeSetExtraction> ParseTimeSet(string input);


        IReadOnlyList<ChronoxTimeSetExtraction> ParseTimeSet(ChronoxSettings settings, string input);
    }
}
