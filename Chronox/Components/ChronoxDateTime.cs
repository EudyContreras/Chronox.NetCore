using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Components
{
    public class ChronoxDateTime
    {
        public ChronoxTime Time { get; private set; }
        
        public ChronoxDate Date { get; private set; }

        public ChronoxDateTime(ChronoxDate date, ChronoxTime time)
        {
            Date = date;
            Time = time;
        }

        public DateTime ToDateTime()
        {
            return new DateTime(Date.Year, Date.Month, Date.Day, Time.Hours, Time.Minutes, Time.Seconds);
        }

        public override string ToString()
        {
            return $"{ Date.ToString()} {Time.ToString()}";
        }
    }
}
