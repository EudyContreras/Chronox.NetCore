using Chronox.Handlers.Wrappers;
using System;
using Chronox.Constants;
using System.Collections.Generic;

namespace Chronox.Handlers.Models
{
    public class Glossary : IEquatable<Glossary> , IComparable<Glossary>, IEqualityComparer<Glossary>
    {
        public Language Language { get; internal set; } = Definitions.DefaultLanguage;

        public bool AssumeSpace { get; internal set; } = true;

        public List<Tuple<char, char>> CommonPunctuation { get; internal set; }

        public List<char> CommonDateSeparators { get; internal set; }

        public List<char> CommonTimeSeparators { get; internal set; }

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
            return string.Compare(Language.LanguageName, other.Language.LanguageName, true);
        }

        public bool Equals(Glossary other)
        {
            return Equals(other.Language, StringComparison.OrdinalIgnoreCase);
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
            return Language.LanguageName;
        }
    }
}
