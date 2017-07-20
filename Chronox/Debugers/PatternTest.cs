using Chronox.Constants;
using Chronox.Helpers.Patterns;
using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using Chronox;

namespace Tests
{
    public class PatternTest
    {
        public Dictionary<string, PatternRegex> Patterns = new Dictionary<string, PatternRegex>();

        public ChronoxSettings settings = ChronoxSettings.Standard;

        public PatternTest()
        {
            this.Patterns = settings.Language.PatternLibrary.Patterns;
        }

        public void TestDayOfWeek()
        {
            var pattern =  Patterns[Definitions.Property.DaysOfWeek].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DaysOfWeek).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestMonthOfYear()
        {
            var pattern = Patterns[Definitions.Property.MonthsOfYear].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.MonthsOfYear).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestNumericWord()
        {
            var pattern = Patterns[Definitions.Property.NumericWord].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericWord).Keys.ToArray())
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestNumericValue()
        {
            var pattern = Patterns[Definitions.Property.NumericValue].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.NumericValue).Keys.ToArray())
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestTimeUnits()
        {
            var pattern = Patterns[Definitions.Property.DateTimeUnits].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DateTimeUnits).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestGrabberExpressions()
        {
            var pattern = Patterns[Definitions.Property.GrabberExpressions].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.GrabberExpressions).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestDayOffset()
        {
            var pattern = Patterns[Definitions.Property.DayOffset].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.DayOffset).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestTimeOfDay()
        {
            var pattern = Patterns[Definitions.Property.TimeOfDay].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimeOfDay).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestSeasonOfYear()
        {
            var pattern = Patterns[Definitions.Property.SeasonOfYear].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.SeasonOfYear).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestTimeExpressions()
        {
            var pattern = Patterns[Definitions.Property.TimeExpressions].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.TimeExpressions).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }

        public void TestCasualExpressions()
        {
            var pattern = Patterns[Definitions.Property.CasualExpressions].Value;

            var wordStart = PatternLibrary.HelperPatterns[Definitions.Patterns.WordStart].Value;

            var wordEnd = PatternLibrary.HelperPatterns[Definitions.Patterns.WordEnd].Value;

            pattern = wordStart + pattern + wordEnd;

            Console.WriteLine(pattern);
            Console.WriteLine();

            var regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            foreach (var match in settings.Language.VocabularyBank.GetDictionary(Definitions.Property.CasualExpressions).Keys)
            {
                var matcher = regex.Match(match);

                if (matcher.Success)
                {
                    Console.WriteLine("Match: " + matcher.Value);
                }
            }
        }
    }
}
