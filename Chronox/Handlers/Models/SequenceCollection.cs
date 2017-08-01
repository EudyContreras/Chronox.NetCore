using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronox.Handlers.Models
{
    public class SequenceCollection
    {

        public SequenceType SequenceType { get; private set; } = SequenceType.DateTime;

        public List<string> Sequences { get; set; } = new List<string>();

        public SequenceCollection(SequenceType type, params string[] sequences)
        {
            SequenceType = type;
            Sequences = sequences.ToList();
        }

        public SequenceCollection(SequenceType type, List<string> sequences)
        {
            SequenceType = type;
            Sequences = sequences;
        }
    }
}
