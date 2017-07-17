using Chronox.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Wrappers
{
    public class RegexPattern : IComparable<RegexPattern>
    {
        public string Value { get; set; }

        public string Label { get; set; }

        public static RegexPattern Null { get; private set; }

        public RegexPattern(string pattern) : this(string.Empty, pattern) { }

        public RegexPattern(string label, string pattern)
        {
            this.Label = label;
            this.Value = pattern;
        }

        public override string ToString()
        {
            return Value;
        }

        public int CompareTo(RegexPattern other)
        {
            return string.Compare(Label, other.Label, true);
        }

        internal void LabelValue()
        {
            if (PatternHandler.MissingLabel(Value))
            {
                Value = PatternHandler.LabelWrapp(Label, Value);
            }
        }
    }
}
