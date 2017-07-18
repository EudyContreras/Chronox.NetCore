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
    internal class ChronoxParser
    {

        public ChronoxSettings Settings { get; private set; } = ChronoxSettings.Standard;

        private static readonly ChronoxSettings standardsettings = ChronoxSettings.Standard;

        private static readonly ChronoxParser standardParser = new ChronoxParser(standardsettings);

        public ChronoxParser() : this(standardsettings) { }

        public ChronoxParser(ChronoxSettings settings)
        {
            settings = settings ?? settings;
        }

        private static ChronoxParser GetInstance(ChronoxSettings settings) => new ChronoxParser(settings);


        public static ParsedResult Parse(string input) => Parse(DateTime.MinValue, input);
        

        public static ParsedResult Parse(DateTime referenceDate, string input) => Parse(standardsettings, referenceDate, input);


        public static ParsedResult Parse(ChronoxSettings settings, string input) => Parse(settings, DateTime.MinValue, input);


        public static ParsedResult Parse(ChronoxSettings option, DateTime referenceDate, string input)
        {
            var instance = GetInstance(option);

            return new ParsedResult(instance, instance.ExtractDates(option, referenceDate, input));
        }

        public static DateResult ParseDate(string input) => ParseDate(DateTime.MinValue, input); 


        public static DateResult ParseDate(DateTime referenceDate, string input) => ParseDate(standardsettings, referenceDate, input);


        public static DateResult ParseDate(ChronoxSettings settings, string input) => ParseDate(settings, DateTime.MinValue, input);


        public static DateResult ParseDate(ChronoxSettings option, DateTime referenceDate, string input)
        {
            var instance = GetInstance(option);

            return new DateResult(instance, instance.ParseDateTime(option, referenceDate, input));
        }

        public static DateResult ParseTimeSpan(string input) => ParseTimeSpan(DateTime.MinValue, input);


        public static DateResult ParseTimeSpan(DateTime referenceDate, string input) => ParseTimeSpan(standardsettings, referenceDate, input);


        public static DateResult ParseTimeSpan(ChronoxSettings settings, string input) => ParseTimeSpan(settings, DateTime.MinValue, input);


        public static DateResult ParseTimeSpan(ChronoxSettings option, DateTime referenceDate, string input)
        {
            var instance = GetInstance(option);

            return new DateResult(instance, instance.ParseDateTime(option, referenceDate, input));
        }

        public static DateResult ParseTimeSet(string input) => ParseTimeSet(DateTime.MinValue, input);


        public static DateResult ParseTimeSet(DateTime referenceDate, string input) => ParseTimeSet(standardsettings, referenceDate, input);


        public static DateResult ParseTimeSet(ChronoxSettings settings, string input) => ParseTimeSet(settings, DateTime.MinValue, input);


        public static DateResult ParseTimeSet(ChronoxSettings option, DateTime referenceDate, string input)
        {
            var instance = GetInstance(option);

            return new DateResult(instance, instance.ParseDateTime(option, referenceDate, input));
        }

        public List<ChronoxDateTimeExtraction> ExtractDates(string input) => ExtractDates(DateTime.MinValue, input);
        

        public List<ChronoxDateTimeExtraction> ExtractDates(DateTime referenceDate, string input) => ExtractDates(Settings, referenceDate, input);


        public List<ChronoxDateTimeExtraction> ExtractDates(ChronoxSettings settings, string input) => ExtractDates(settings, DateTime.MinValue, input );


        public List<ChronoxDateTimeExtraction> ExtractDates(ChronoxSettings settings, DateTime referenceDate, string input )
        {
            var allResults = new List<ChronoxDateTimeExtraction>();

            var processed = PreProcessExpression(ProcessorType.PreProcessor, input);

            var scanResults = PerformExpressionScanAndReplace(processed);

            if(referenceDate != DateTime.MinValue)
            {
                settings.ReferencDate = referenceDate;
            }

            foreach (var parser in settings.Parsers())
            {
                var results = parser.Execute(scanResults.Key, settings.ReferencDate, settings);

                allResults.AddRange(results);
            }

            allResults = PostProcessResults(allResults, scanResults.Key, settings);

            allResults.Sort();

            return allResults;
        }

        public DateTime ParseDateTime(string input) => ParseDateTime(DateTime.MinValue, input);


        public DateTime ParseDateTime(DateTime referenceDate, string input) => ParseDateTime(Settings, referenceDate, input);


        public DateTime ParseDateTime(ChronoxSettings settings, string input) => ParseDateTime(settings, DateTime.MinValue, input);


        public DateTime ParseDateTime(ChronoxSettings settings, DateTime referenceDate, string input )
        {
            if (referenceDate != DateTime.MinValue)
            {
                settings.ReferencDate = referenceDate;
            }

            var results = ExtractDates(settings, settings.ReferencDate, input);

            if (results.Count > 0)
            {
                return results[0].GetCurrent().DateTime(); 
            }

            return DateTime.MinValue;
        }

        private string PreProcessExpression(ProcessorType type, string input)
        {
            if (Settings.Processors(type).Any())
            {
                foreach (var processor in Settings.Processors(type))
                {
                    input = processor.PreProcess(Settings,input);
                }
            }
            return input;
        }

        private PairWrapper<string,List<ScanResult>> PerformExpressionScanAndReplace(string input)
        {
            var results = new List<ScanResult>();

            var expression = input;

            foreach (var scanner in Settings.Scanners())
            {
                var result = scanner.Scan(Settings,expression);

                if (result.ResultWrappers.Count > 0)
                {
                    expression = result.NormalizedExpression;

                    results.Add(result);
                }
            }

            return new PairWrapper<string, List<ScanResult>>(expression,results);
        }

        private List<ChronoxDateTimeExtraction> PostProcessResults(List<ChronoxDateTimeExtraction> results, string input, ChronoxSettings settings)
        {
            if (!settings.Processors(ProcessorType.PostProcessor).Any()) return results;

            foreach (var processor in settings.Processors(ProcessorType.PostProcessor))
            {
                results = processor.PostProcess(settings,results, input);
            }

            return results;
        }
    }
}
