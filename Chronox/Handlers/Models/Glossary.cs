using Chronox.Handlers.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Models
{
    public class Glossary
    {
        public string Language { get; set; } = "English";

        public bool AssumeSpace { get; set; } = true;

        public string PreferedEndianFormat { get; set; } = "M.D.Y";

        public List<string> Ignored { get; set; }

        public List<string> SupportedDateTimeFormats { get; set; }

        public List<string> SupportedTimeRangeFormats { get; set; }

        public List<string> SupportedTimeSpanFormats { get; set; }

        public List<string> SupportedTimeSetFormats { get; set; }

        public List<Section> Section { get; set; }

        public override string ToString()
        {
            return Language;
        }
    }
}
