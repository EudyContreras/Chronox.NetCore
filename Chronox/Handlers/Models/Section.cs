
using System.Collections.Generic;

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
