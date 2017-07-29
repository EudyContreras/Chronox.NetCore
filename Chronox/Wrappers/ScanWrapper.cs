using Chronox.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Wrappers
{
    public class ScanWrapper
    {
        public List<ReplaceWrapper> ResultWrappers { get; set; } = new List<ReplaceWrapper>();

        public string ScannedExpression { get; set; } 

        public string NormalizedExpression { get; set; }

        public string ScannerTag { get; set; }

        public ScanWrapper(IChronoxScanner scanner)
        {
            ScannerTag = scanner.ScannerTag();
        }
    }
}
