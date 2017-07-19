using Chronox.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox
{
    internal class ChronoxDate : IComparable<ChronoxDate>
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public ChronoxDate(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public override string ToString() => $"{Year}-{Month}-{Day}";

        public int CompareTo(ChronoxDate other)
        {
            var compoundOne = Year + Month + Day;

            var compoundTwo = other.Year + other.Month + other.Day;

            return compoundOne - compoundTwo;
        }
    }
}
