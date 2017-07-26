using Chronox.Constants;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronox.Handlers.Models
{
    public class Sequence : IEquatable<Sequence>, IComparable<Sequence>, IEqualityComparer<Sequence>
    {
        public string Representation { get; set; }

        public string AbbreviatedSequence { get; set; }

        public decimal FrequencyScore { get; set; }

        public SequenceType SequenceType { get; private set; }

        public static Sequence Comapararer = new Sequence();

        public List<string> DateProperties { get;set; }

        public Sequence() { }

        public Sequence(SequenceType type, params string[] properties) : this(type, null, properties) { }

        public Sequence(SequenceType type, string representation, params string[] properties) : this(type, representation, 0, properties) { }

        public Sequence(SequenceType type, string representation, decimal frequencyScore, params string[] properties)
        {
            SequenceType = type;
            FrequencyScore = frequencyScore;
            Representation = representation;
            AbbreviatedSequence = ToAbbreviatedSequence(properties);
            DateProperties = new List<string>(properties);
        }

        private string ToAbbreviatedSequence(string[] properties)
        {
            var builder = new List<string>();

            foreach(var property in properties)
            {
                builder.Add(Definitions.Converters.ABBREVIATIONS[property]);
            }

            return string.Join("|", builder);
        }

        public int CompareTo(Sequence other)
        {
            return string.Compare(AbbreviatedSequence, other.AbbreviatedSequence, true);
        }

        public bool Equals(Sequence other)
        {
            return AbbreviatedSequence.Equals(other.AbbreviatedSequence, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Sequence x, Sequence y)
        {
            return x.AbbreviatedSequence.Equals(y.AbbreviatedSequence, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Sequence obj)
        {
            return obj.AbbreviatedSequence.GetHashCode();
        }
    }
}
