using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Wrappers
{
    public class Property : IEquatable<Property>, IComparable<Property>, IEqualityComparer<Property>
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }

        public string Pattern { get; set; }

        public static Property Comparer = new Property();

        public List<string> Variations { get; set; }

        public int CompareTo(Property other)
        {
            return string.Compare(Key, other.Key, true);
        }

        public bool Equals(Property other)
        {
            return Key.Equals(other.Key, StringComparison.OrdinalIgnoreCase);
        }

        public bool Equals(Property x, Property y)
        {
            return x.Key.Equals(y.Key, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(Property obj)
        {
            return obj.Key.GetHashCode();
        }

        public override string ToString()
        {
            return Key;
        }
    }
}
