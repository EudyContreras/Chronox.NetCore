using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Exceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string input) : this(input, null){ }

        public InvalidInputException(string input, Exception inner) : base($"The ginven input( {input} ) does not comply with input rules", inner){ }
    }
}
