using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Wrappers
{
    internal class Property
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string Type { get; set; }

        public string Pattern { get; set; }

        public List<string> Variations { get; set; }
    }
}
