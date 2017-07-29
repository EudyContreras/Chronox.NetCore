using System;
using System.Collections.Generic;
using System.Text;
using Enumerations;

namespace Chronox.Wrappers
{
    public class NumberWrapper
    {
        public decimal Value { get; private set; }

        public NumberType Type { get; private set; }

        public NumberWrapper(decimal value, NumberType type)
        {
            this.Value = value;
            this.Type = type;
        }

        public NumberWrapper()
        {
        }
    }
}
