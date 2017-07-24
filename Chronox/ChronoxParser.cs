using Chronox.Interfaces;
using Chronox.Parsers;
using Chronox.Processors;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox
{
    public class ChronoxParser : IChronox
    {

        private static IChronox Instance = null;

        public ChronoxSettings Settings { get; set; }

        private static ChronoxSettings StandardSettings = ChronoxSettings.Standard;

        private ChronoxParser(ChronoxSettings settings)
        {
            Settings = settings ?? ChronoxSettings.Standard;
        }

        public static IChronox GetInstance() => GetInstance(StandardSettings);


        public static IChronox GetInstance(ChronoxSettings settings)
        {
            if(Instance == null)
            {
                Instance = new ChronoxParser(settings);

                return Instance;
            }
            else
            {
                if (!Instance.Settings.Equals(settings))
                {
                    Instance.Settings = settings;
                }

                return Instance;
            }
        }


        public static ResultWrapper Parse(string input) => Parse(DateTime.MinValue, input);
        

        public static ResultWrapper Parse(DateTime referenceDate, string input) => Parse(StandardSettings, referenceDate, input);


        public static ResultWrapper Parse(ChronoxSettings settings, string input) => Parse(settings, DateTime.MinValue, input);


        public static ResultWrapper Parse(ChronoxSettings settings, DateTime referenceDate, string input) => GetInstance(settings).Parse(referenceDate, input);
        

        public static bool TryParse(string input, out ResultWrapper result) => TryParse(DateTime.MinValue, input, out result);


        public static bool TryParse(DateTime referenceDate, string input, out ResultWrapper result) => TryParse(Instance.Settings, referenceDate, input, out result);


        public static bool TryParse(ChronoxSettings settings, string input, out ResultWrapper result) => TryParse(settings, DateTime.MinValue, input, out result);


        public static bool TryParse(ChronoxSettings settings, DateTime referenceDate, string input, out ResultWrapper result) => GetInstance(settings).TryParse(referenceDate, input, out result);
        

        public static IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(string input) => ParseDateTime(DateTime.MinValue, input); 


        public static IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(DateTime referenceDate, string input) => ParseDateTime(StandardSettings, referenceDate, input);


        public static IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(ChronoxSettings settings, string input) => ParseDateTime(settings, DateTime.MinValue, input);


        public static IReadOnlyList<ChronoxDateTimeExtraction> ParseDateTime(ChronoxSettings settings, DateTime referenceDate, string input) => GetInstance(settings).ParseDateTime(referenceDate, input);
       

        public static IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(string input) => ParseTimeRange(DateTime.MinValue, input);


        public static IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(DateTime referenceDate, string input) => ParseTimeRange(StandardSettings, referenceDate, input);


        public static IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(ChronoxSettings settings, string input) => ParseTimeRange(settings, DateTime.MinValue, input);


        public static IReadOnlyList<ChronoxTimeRangeExtraction> ParseTimeRange(ChronoxSettings settings, DateTime referenceDate, string input) => GetInstance(settings).ParseTimeRange(referenceDate, input);
        

        public static IReadOnlyList<ChronoxTimeSpanExtraction> ParseTimeSpan(string input) => ParseTimeSpan(StandardSettings, input);


        public static IReadOnlyList<ChronoxTimeSpanExtraction> ParseTimeSpan(ChronoxSettings settings, string input) => GetInstance(settings).ParseTimeSpan(input);
        

        public static IReadOnlyList<ChronoxTimeSetExtraction> ParseTimeSet(string input) => ParseTimeSet(StandardSettings, input);


        public static IReadOnlyList<ChronoxTimeSetExtraction> ParseTimeSet(ChronoxSettings settings, string input) => GetInstance(settings).ParseTimeSet(input);


        ResultWrapper IChronox.Parse(string input) => Parse(Settings, input);


        ResultWrapper IChronox.Parse(DateTime referenceDate, string input)
        {
            var allResults = new List<IChronoxExtraction>();

            var processed = PreProcessExpression(ProcessorType.PreProcessor, input);

            var scanResults = PerformExpressionScanAndReplace(processed);

            if (referenceDate == DateTime.MinValue)
            {
                referenceDate = DateTime.Now;
            }

            Settings.ReferenceDate = referenceDate;

            foreach (var parser in Settings.Parsers())
            {
                var results = parser.Execute(scanResults.Key, Settings.ReferenceDate, Settings);

                allResults.AddRange(results);
            }

            allResults = PostProcessResults(allResults, scanResults.Key, Settings);

            allResults.Sort();

            if (allResults.Count > 0)
            {
                return new ResultWrapper(this, allResults);
            }

            return null;
        }


        bool IChronox.TryParse(string input, out ResultWrapper result) => TryParse(DateTime.MinValue, input, out result);


        bool IChronox.TryParse(DateTime referenceDate, string input, out ResultWrapper result)
        {
            var parsedResult = Parse(referenceDate, input);

            if (parsedResult != null)
            {
                result = parsedResult;

                return true;
            }
            else
            {
                result = null;

                return false;
            }
        }

        IReadOnlyList<ChronoxDateTimeExtraction> IChronox.ParseDateTime(string input) => ParseDateTime(DateTime.MinValue, input);


        IReadOnlyList<ChronoxDateTimeExtraction> IChronox.ParseDateTime(DateTime referenceDate, string input)
        {
            Settings.Preferences.ParsingMode = ExtractionResultType.DateTime;

            return Parse(referenceDate, input)?.Results.Cast<ChronoxDateTimeExtraction>().ToList();
        }
   

        IReadOnlyList<ChronoxTimeRangeExtraction> IChronox.ParseTimeRange(string input) => ParseTimeRange(DateTime.MinValue, input);


        IReadOnlyList<ChronoxTimeRangeExtraction> IChronox.ParseTimeRange(DateTime referenceDate, string input)
        {
            Settings.Preferences.ParsingMode = ExtractionResultType.TimeRange;

            return Parse(referenceDate, input)?.Results.Cast<ChronoxTimeRangeExtraction>().ToList();
        }

        IReadOnlyList<ChronoxTimeSpanExtraction> IChronox.ParseTimeSpan(string input)
        {
            Settings.Preferences.ParsingMode = ExtractionResultType.TimeSpan;

            return Parse(DateTime.MinValue, input)?.Results.Cast<ChronoxTimeSpanExtraction>().ToList();
        }


        IReadOnlyList<ChronoxTimeSetExtraction> IChronox.ParseTimeSet(string input)
        {
            Settings.Preferences.ParsingMode = ExtractionResultType.TimeSet;

            return Parse(DateTime.MinValue, input)?.Results.Cast<ChronoxTimeSetExtraction>().ToList();
        }


        private string PreProcessExpression(ProcessorType type, string input)
        {
            if (Settings.Processors(type).Any())
            {
                foreach (var processor in Settings.Processors(type))
                {
                    input = processor.PreProcess(Settings, input);
                }
            }
            return input;
        }


        private PairWrapper<string, List<ScanWrapper>> PerformExpressionScanAndReplace(string input)
        {
            var results = new List<ScanWrapper>();

            var expression = input;

            foreach (var scanner in Settings.Scanners())
            {
                var result = scanner.Scan(Settings, expression);

                if (result.ResultWrappers.Count > 0)
                {
                    expression = result.NormalizedExpression;

                    results.Add(result);
                }
            }

            return new PairWrapper<string, List<ScanWrapper>>(expression, results);
        }


        private List<IChronoxExtraction> PostProcessResults(List<IChronoxExtraction> results, string input, ChronoxSettings settings)
        {
            if (!settings.Processors(ProcessorType.PostProcessor).Any()) return results;

            foreach (var processor in settings.Processors(ProcessorType.PostProcessor))
            {
                results = processor.PostProcess(settings, results, input);
            }

            return results;
        }
    }
}
