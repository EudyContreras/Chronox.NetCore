using Chronox.Handlers.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Models
{
    public class Glossary : IEquatable<Glossary> , IComparable<Glossary>, IEqualityComparer<Glossary>
    {
        public string Language { get; set; } = "English";

        public bool AssumeSpace { get; set; } = true;

        public string PreferedEndianFormat { get; set; } = "M.D.Y";

        public List<string> CommonPunctuation { get; set; }

        public List<string> CommonDateSeparators { get; set; }

        public List<string> CommonTimeSeparators { get; set; }

        public List<string> DateTimeIgnored { get; set; }

        public List<string> TimeRangeIgnored { get; set; }

        public List<string> TimeSpanIgnored { get; set; }

        public List<string> TimeSetIgnored { get; set; }

        public List<string> SupportedDateTimeFormats { get; set; }

        public List<string> SupportedTimeRangeFormats { get; set; }

        public List<string> SupportedTimeSpanFormats { get; set; }

        public List<string> SupportedTimeSetFormats { get; set; }

        public List<Section> Sections { get; set; }

        public int CompareTo(Glossary other)
        {
            return string.Compare(Language, other.Language, true);
        }

        public bool Equals(Glossary other)
        {
            return Language.Equals(other.Language, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Glossary x, Glossary y)
        {
            return x.Language.Equals(y.Language);
        }

        public int GetHashCode(Glossary obj)
        {
           return  obj.Language.GetHashCode();
        }

        public override string ToString()
        {
            return Language;
        }
    }
}
