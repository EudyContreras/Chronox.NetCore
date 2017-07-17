using Chronox.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox
{
    internal class ChronoxDateComponent : IComparable<ChronoxDateComponent>
    {
        public int Year { get; set; }

        public int Month { get; set; }

        public int Day { get; set; }

        public ChronoxDateComponent(int year, int month, int day)
        {
            Year = year;
            Month = month;
            Day = day;
        }

        public ChronoxDateComponent AddYears(int year)
        {
            this.Year += year;

            return this;
        }

        public ChronoxDateComponent AddMonths(int month)
        {
            this.Month += month;

            if (this.Month >= 12)
            {
                AddYears(1);

                Month = 0;
            }
            return this;
        }

        public ChronoxDateComponent AddWeeka(int week)
        {
            return AddDays(week * 7);
        }

        public ChronoxDateComponent AddDays(int day)
        {
            this.Day += day;

            if (this.Day >= DateTimeUtility.DaysInMonth(DateTime.Now))
            {
                AddMonths(1);

                Day = 0;
            }

            return this;
        }

        public override string ToString() => $"{Year}-{Month}-{Day}";

        public int CompareTo(ChronoxDateComponent other)
        {
            var compoundOne = Year + Month + Day;

            var compoundTwo = other.Year + other.Month + other.Day;

            return compoundOne - compoundTwo;
        }
    }
}
