using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Exceptions
{
    public class UnsupportedSearchPassException : Exception
    {
        public UnsupportedSearchPassException(int count) : this(count, null){ }

        public UnsupportedSearchPassException(int count, Exception inner) : base($"An unsupported count has been give: {count} Please issue a count between 1 and 5", inner){ }
    }
}
