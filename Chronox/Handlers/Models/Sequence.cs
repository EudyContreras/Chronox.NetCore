using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronox.Handlers.Models
{
    public class Sequence : IEquatable<Sequence>, IComparable<Sequence>, IEqualityComparer<Sequence>
    {
        public string DateSequence { get; set; }

        public string Representation { get; set; }

        public decimal FrequencyScore { get; set; }

        public SequenceType SequenceType { get; private set; }

        public List<string> DateProperties { get;set; }

        public Sequence(SequenceType type, params string[] properties) : this(type, null, properties) { }

        public Sequence(SequenceType type, string representation, params string[] properties) : this(type, representation, 0, properties) { }

        public Sequence(SequenceType type, string representation, decimal frequencyScore, params string[] properties)
        {
            SequenceType = type;
            FrequencyScore = frequencyScore;
            Representation = representation;
            DateProperties = new List<string>(properties);
        }

        public int CompareTo(Sequence other)
        {
            var sourceSequnce = string.Join("|", DateProperties);

            var otherSequence = string.Join("|", other.DateProperties);

            return sourceSequnce.CompareTo(otherSequence);
        }

        public bool Equals(Sequence other)
        {
            return CompareTo(other) == 0;
        }

        public bool Equals(Sequence x, Sequence y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Sequence obj)
        {
            var sourceSequnce = string.Join("|", DateProperties);

            return sourceSequnce.GetHashCode();
        }
    }
}
