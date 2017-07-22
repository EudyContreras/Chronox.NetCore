using Chronox.Handlers;
using Chronox.Helpers.Patterns;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Chronox.Constants;
using Enumerations;
using System.Text.RegularExpressions;

namespace Chronox.Wrappers
{
    public class PatternSequence
    {

        public readonly List<PatternRegex> Patterns = new List<PatternRegex>();

        public Regex RegexMatcher { get; set; }

        public string CombinedPattern { get; set; }

        public int PatternCount { get; set; }

        public int Relevance { get; set; }

        public SequenceType SequenceType { get; private set; }

        public PatternSequence(SequenceType type)
        {
            SequenceType = type;
        }

        public Regex InitRegexMatcher(VocabularyHandler handler)
        {
            var pattern = string.Concat(handler.VocabularyBank.WordStart, CombinedPattern, handler.VocabularyBank.WordEnd);

            return new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);
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
