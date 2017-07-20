﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    public class ChronoxTimeSpan
    {
        public ChronoxParser Parser { get; private set; }
        
        public DateTime? Date { get; private set; }

        public ChronoxTimeSpan(ChronoxParser parser, DateTime date)
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
