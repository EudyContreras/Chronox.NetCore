using Chronox.Handlers.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Models
{
    public class Glossary : IEquatable<Glossary> , IComparable<Glossary>, IEqualityComparer<Glossary>
    {
        public string Language { get; internal set; } = "English";

        public bool AssumeSpace { get; internal set; } = true;

        public List<string> CommonPunctuation { get; internal set; }

        public List<string> CommonDateSeparators { get; internal set; }

        public List<string> CommonTimeSeparators { get; internal set; }

        public List<string> DateTimeIgnored { get; internal set; }

        public List<string> TimeRangeIgnored { get; internal set; }

        public List<string> TimeSpanIgnored { get; internal set; }

        public List<string> TimeSetIgnored { get; internal set; }

        public List<string> OrdinalSuffixes { get; internal set; }

        public List<string> SupportedDateTimeFormats { get; internal set; }

        public List<string> SupportedTimeRangeFormats { get; internal set; }

        public List<string> SupportedTimeSpanFormats { get; internal set; }

        public List<string> SupportedTimeSetFormats { get; internal set; }

        public List<Section> Sections { get; internal set; }

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
