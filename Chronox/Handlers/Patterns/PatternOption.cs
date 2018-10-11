using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Patterns
{
   
    public class PatternOption
    {
        public static PatternOption Standard = GetStandard();

        private static PatternOption GetStandard()
        {
            return new PatternOption();
        }

        public class PatternCreationType
        {
            public static readonly string Combined = "combined";

            public static readonly string CombinedOptional = "combinedOptional";

            public static readonly string CombinedReversed = "combinedReversed";

            public static readonly string Group = "group";

            public static readonly string Capture = "group";

            public static readonly string GroupOptional = "groupOptional";

            public static readonly string Ignored = "ignored";

            public static readonly string Optional = "optional";

            public static readonly string Filler = "filler";

            public static readonly string Interpreted = "interpreted";

            public static readonly string None = "n/a";
        }

    }
}
