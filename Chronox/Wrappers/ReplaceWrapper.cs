using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Wrappers
{
    public class ReplaceWrapper
    {
 
        public IndexWrapper ReplacementPosition { get; set; }

        public IndexWrapper OriginalPosition { get; set; }  

        public string ReplacerTag { get; set; }

        public string TextOriginal { get; set; }

        public string TextReplacement { get; set; }

        public ReplaceWrapper(string replacerTag, int startIndex, int endIndex) : this(replacerTag, null, startIndex, endIndex) { }

        public ReplaceWrapper(string replacerTag, string text, int startIndex, int endIndex)
        {
            this.TextOriginal = text;
            this.ReplacerTag = replacerTag;

            OriginalPosition = new IndexWrapper(startIndex, endIndex);
            ReplacementPosition = new IndexWrapper(startIndex, endIndex);
        }

        public static bool Intercepts(int index, IndexWrapper wrapper)
        {
            return index >= wrapper.StartIndex && index <= wrapper.EndIndex;
        }

        public bool Intercepts(IndexWrapper other)
        {
            if(other.StartIndex>= ReplacementPosition.StartIndex && other.EndIndex <= ReplacementPosition.EndIndex)
            {
                return true;
            }
            else if (ReplacementPosition.StartIndex >= other.StartIndex && ReplacementPosition.EndIndex <= other.EndIndex)
            {
                return true;
            }
            return false;
        }
    }
}
