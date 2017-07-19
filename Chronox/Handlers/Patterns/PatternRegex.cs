using Chronox.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Wrappers
{
    public class PatternRegex : IComparable<PatternRegex>
    {
        public string Value { get; set; }

        public string Label { get; set; }

        public static PatternRegex Null { get; private set; }

        public PatternRegex(string pattern) : this(string.Empty, pattern) { }

        public PatternRegex(string label, string pattern)
        {
            this.Label = label;
            this.Value = pattern;
        }

        public override string ToString()
        {
            return Value;
        }

        public int CompareTo(PatternRegex other)
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
