using System;


namespace Chronox.Components
{
    public class ChronoxDateTime : IComparable<ChronoxDateTime>
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

        public int CompareTo(ChronoxDateTime other)
        {
            var dateCompareValue = Date.CompareTo(other.Date);

            if(dateCompareValue == 0 ){

                return Time.CompareTo(other.Time);
            }

            return dateCompareValue;
        }
    }
}
