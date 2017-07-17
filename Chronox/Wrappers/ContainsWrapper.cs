using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Wrappers
{
    internal class ContainsWrapper
    {
        public string Text { get; set; }

        public bool Found { get; set; }

        public IndexWrapper Position { get; set; }

        public ContainsWrapper(bool found, int startIndex, int endIndex) : this(found, null, startIndex, endIndex) { }

        public ContainsWrapper(bool found, string text, int startIndex, int endIndex)
        {
            this.Found = found;
            this.Text = text;
            Position = new IndexWrapper(startIndex, endIndex);

        }

        public int StartIndex
        {
            get
            {
                return Position.StartIndex;
            }
        }

        public int EndIndex
        {
            get
            {
                return Position.EndIndex;
            }
        }

        public bool Intercepts(IndexWrapper other)
        {
            if(other.StartIndex>= Position.StartIndex && other.EndIndex <= Position.EndIndex)
            {
                return true;
            }
            else if (Position.StartIndex >= other.StartIndex && Position.EndIndex <= other.EndIndex)
            {
                return true;
            }
            return false;
        }
    }
}
