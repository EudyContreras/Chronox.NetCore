using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox
{
    public class ChronoxTime : IComparable<ChronoxTime>
    {
        public int Hours { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public int MilliSecond { get; set; }

        public ChronoxTime() : this(0, 0, 0) { }

        public ChronoxTime(int hour, int minute, int second) : this(hour, minute, second, 0) { }

        public ChronoxTime(int hour, int minute, int second, int milliSecond)
        {
            Hours = hour;
            Minutes = minute;
            Seconds = second;
            MilliSecond = milliSecond;
        }

        public ChronoxTime AddHour(int hour)
        {
            for(var i = 0; i<Math.Abs(hour); i++)
            {
                if(Hours == 0)
                {
                    Hours = 24;
                }
                this.Hours =  hour < 0 ? Hours -1 : Hours + 1;

                if (this.Hours >= 24)
                {
                    Hours = 0;
                }
            }

            return this;
        }


        public ChronoxTime AddMinute(int minute)
        {
            this.Minutes += minute;

            if (this.Minutes >= 60)
            {
                AddHour(1);

                Minutes = 0;
            }
            return this;
        }

        public ChronoxTime AddSecond(int second)
        {
            this.Seconds += second;

            if (this.Seconds >= 60)
            {
                AddMillisecond(1);

                Seconds = 0;
            }

            return this;
        }

        public ChronoxTime AddMillisecond(int milli)
        {
            this.MilliSecond += milli;

            if(this.MilliSecond >= 1000)
            {
                AddSecond(1);

                MilliSecond = 0;
            }
            return this;
        }


        public override string ToString() => $"{Hours}:{Minutes}:{Seconds}:{MilliSecond}";

        public int CompareTo(ChronoxTime other)
        {
            return Hours != other.Hours ? Hours - other.Hours : Minutes != other.Minutes ? Minutes - Minutes : Seconds != other.Seconds ? Seconds - other.Seconds : MilliSecond != other.MilliSecond ? MilliSecond - other.MilliSecond : 0;
        }
    }
}
