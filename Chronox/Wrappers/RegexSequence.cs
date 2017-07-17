using Chronox.Handlers;
using Chronox.Helpers.Patterns;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Chronox.Constants;
using Enumerations;

namespace Chronox.Wrappers
{
    internal class RegexSequence
    {

        public readonly List<RegexPattern> Patterns = new List<RegexPattern>();

        public string CombinedPattern { get; set; }

        public int PatternCount { get; set; }

        public int Relevance { get; set; }

        public SequenceType SequenceType { get; private set; }

        public RegexSequence(SequenceType type)
        {
            SequenceType = type;
        }

        public string NormalizedPattern(LanguageHandler handler)
        {
            return string.Concat(handler.VocabularyBank.WordStart,CombinedPattern, handler.VocabularyBank.WordEnd);
        }

        public void ComputeRelevance(int maxCount)
        {
            if(Patterns.Any(p => p.Label == Definitions.Property.InterpretedExpression))
            {
                Relevance = (maxCount + PatternCount);
            }
            else
            {
                Relevance = PatternCount;
            }
        }

        public bool ContainsProperty(string propertyName)
        {
            return Patterns.Any(p => string.Compare(p.Label, propertyName, true) == 0);
        }
    }
}
