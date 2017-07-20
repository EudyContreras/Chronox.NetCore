using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Wrappers
{
    public class Section
    {
        public string Label { get; set; }

        public string Type { get; set; }

        public List<Property> Properties { get; set; }

        public override string ToString()
        {
            return Label;
        }
    }
}
