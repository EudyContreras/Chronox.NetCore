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

        public int PartCount { get; private set; }

        public MatchWrapper(PatternSequence sequence, Match match, int count)
        {
            Sequence = sequence;
            Match = match;
            PartCount = count;
        }

        public MatchWrapper(PatternSequence sequence, Match match) : this(sequence,match, match.Value.Trim().Split(' ').Length){}


        public override string ToString()
        {
            return Match.ToString();
        }
    }
}
