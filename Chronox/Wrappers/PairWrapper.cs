using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    public class PairWrapper<TKey,TValue>
    {
        public TKey Key { get; private set; }

        public TValue Value { get; private set; }

        public PairWrapper(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
