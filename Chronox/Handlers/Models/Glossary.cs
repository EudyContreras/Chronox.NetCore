using Chronox.Handlers.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Handlers.Models
{
    internal class Glossary
    {
        public string Language { get; set; } = "English";

        public bool AssumeSpace { get; set; } = true;

        public List<string> Ignored { get; set; }

        public List<string> SectionTypes { get; set; }

        public List<string> PropertyType { get; set; }

        public List<string> SupportedSectionAbreviations { get; set; }

        public List<string> SupportedDateFormats { get; set; }

        public List<Section> Section { get; set; }
    }
}
