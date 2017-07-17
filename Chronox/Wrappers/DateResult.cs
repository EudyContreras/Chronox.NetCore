using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    internal class DateResult
    {
        public ChronoxParser Parser { get; private set; }
        
        public DateTime? Date { get; private set; }

        public DateResult(ChronoxParser parser, DateTime date)
        {
            Parser = parser;

            if(date != DateTime.MinValue)
            {
                Date = date;
            }
        }

        public override string ToString()
        {
            return Date.ToString();
        }
    }
}
