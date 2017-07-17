using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    internal class ParsedResult
    {
        public ChronoxParser Parser { get; private set; }

        public List<ChronoxDateTimeExtraction> Results { get; set; }

        public ParsedResult(ChronoxParser parser, List<ChronoxDateTimeExtraction> results)
        {
            Parser = parser;
            Results = results;
        }
    }
}
