using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Components
{
    public class ChronoxTimeZone
    {
        public TimeSpan Offset { get; set; }

        public string UtcOffset { get; set; }

        public string Abbreviation { get; set; }

        public string StandardName { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"{StandardName}  {Offset}";
        }

        public ChronoxTimeZone SubstractOffset(ChronoxTimeZone timeZone){
            
            Offset = Offset.Subtract(timeZone.Offset);

            return this;
        }

        public ChronoxTimeZone AddOffset(ChronoxTimeZone timeZone)
        {

            Offset = Offset.Add(timeZone.Offset);

            return this;
        }

 
    }
}
