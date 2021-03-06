﻿using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Interfaces
{
    public interface IChronox
    {

        ChronoxSettings Settings { get; set; }

        IEnumerable<IChronoxScanner> Scanners { get; }

        void RemoveScanner(IChronoxScanner scanner);

        void AddScanner(params IChronoxScanner[] scanners);

        ResultWrapper Parse(string input);


        ResultWrapper Parse(DateTime referenceDate, string input);
        

        bool TryParse(string input, out ResultWrapper result);


        bool TryParse(DateTime referenceDate, string input, out ResultWrapper result);


        IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(string input);


        IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(DateTime referenceDate, string input);


        IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(string input);


        IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(DateTime referenceDate, string input);


        IReadOnlyList<ChronoxTimeSpanExtraction> ParseTimeSpan(string input);


        IReadOnlyList<ChronoxTimeSetExtraction> ParseTimeSet(string input);

    }
}
