using System;
using System.ComponentModel.DataAnnotations;

namespace Chronox
{
    public class ChronoxLangSettings
    {
        
        public int Priority { get; set; }

        public const int MAX_PRIORITY = -1;
        public const int MIN_PRIORITY = int.MaxValue;

        public static readonly ChronoxLangSettings Default = getDefault();

        public ChronoxLangSettings([Range(MAX_PRIORITY, MIN_PRIORITY)] int priority)
        {
            Priority = priority;
        }

        private static ChronoxLangSettings getDefault(){
            return new ChronoxLangSettings(MAX_PRIORITY);
        }
    }
}
