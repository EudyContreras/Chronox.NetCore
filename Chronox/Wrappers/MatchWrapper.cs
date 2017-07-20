using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Chronox.Wrappers
{
    public class MatchWrapper
    {
        public PatternSequence Sequence { get; set; }

        public static MatchWrapper Null {get;}

        public Match Match { get; set; }

        public MatchWrapper(PatternSequence sequence, Match match)
        {
            this.Sequence = sequence;
            this.Match = match;
        }

        public override string ToString()
        {
            return Match.ToString();
        }
    }
}
