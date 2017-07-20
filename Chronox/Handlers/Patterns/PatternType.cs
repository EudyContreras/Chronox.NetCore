using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Patterns
{
    public enum PatternType
    {
        Combined,

        CombinedOptional,

        CombinedReversed,

        Group,

        Capture,

        GroupOptional,

        Optional,

        Filler,

        Ignored,

        Interpreted,

        Default = -1
    }
}
