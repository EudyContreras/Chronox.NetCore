using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Components
{
    public class ChronoxDateTime
    {
        public ChronoxParser Parser { get; private set; }
        
        public DateTime? Date { get; private set; }

        public ChronoxDateTime(ChronoxParser parser, DateTime date)
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
