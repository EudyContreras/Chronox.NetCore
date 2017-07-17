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

        public ChronoxOption Options { get; private set; } = ChronoxOption.Standard;

        private static readonly ChronoxOption standardOptions = ChronoxOption.Standard;

        private static readonly ChronoxParser standardParser = new ChronoxParser(standardOptions);

        public ChronoxParser() : this(standardOptions) { }

        public ChronoxParser(ChronoxOption options)
        {
            Options = options ?? Options;
        }

        private static ChronoxParser GetInstance(ChronoxOption options) => new ChronoxParser(options);


        public static ParsedResult Parse(string input) => Parse(DateTime.Now, input);
        

        public static ParsedResult Parse(DateTime referenceDate, string input) => Parse(standardOptions, referenceDate, input);


        public static ParsedResult Parse(ChronoxOption options, string input) => Parse(options, DateTime.Now, input);


        public static ParsedResult Parse(ChronoxOption option, DateTime referenceDate, string input)
        {
            var instance = GetInstance(option);

            return new ParsedResult(instance, instance.ExtractDates(option, referenceDate, input));
        }

        public static DateResult ParseDate(string input) => ParseDate(DateTime.Now, input); 


        public static DateResult ParseDate(DateTime referenceDate, string input) => ParseDate(standardOptions, referenceDate, input);


        public static DateResult ParseDate(ChronoxOption options, string input) => ParseDate(options, DateTime.Now, input);


        public static DateResult ParseDate(ChronoxOption option, DateTime referenceDate, string input)
        {
            var instance = GetInstance(option);

            return new DateResult(instance, instance.ParseDateTime(option, referenceDate, input));
        }

        public static DateResult ParseTimeSpan(string input) => ParseTimeSpan(DateTime.Now, input);


        public static DateResult ParseTimeSpan(DateTime referenceDate, string input) => ParseTimeSpan(standardOptions, referenceDate, input);


        public static DateResult ParseTimeSpan(ChronoxOption options, string input) => ParseTimeSpan(options, DateTime.Now, input);


        public static DateResult ParseTimeSpan(ChronoxOption option, DateTime referenceDate, string input)
        {
            var instance = GetInstance(option);

            return new DateResult(instance, instance.ParseDateTime(option, referenceDate, input));
        }

        public static DateResult ParseTimeSet(string input) => ParseTimeSet(DateTime.Now, input);


        public static DateResult ParseTimeSet(DateTime referenceDate, string input) => ParseTimeSet(standardOptions, referenceDate, input);


        public static DateResult ParseTimeSet(ChronoxOption options, string input) => ParseTimeSet(options, DateTime.Now, input);


        public static DateResult ParseTimeSet(ChronoxOption option, DateTime referenceDate, string input)
        {
            var instance = GetInstance(option);

            return new DateResult(instance, instance.ParseDateTime(option, referenceDate, input));
        }

        public List<ChronoxDateTimeExtraction> ExtractDates(string input) => ExtractDates(DateTime.Now, input);
        

        public List<ChronoxDateTimeExtraction> ExtractDates(DateTime referenceDate, string input) => ExtractDates(Options, referenceDate, input);


        public List<ChronoxDateTimeExtraction> ExtractDates(ChronoxOption options, string input) => ExtractDates(options, DateTime.Now, input );


        public List<ChronoxDateTimeExtraction> ExtractDates(ChronoxOption options, DateTime referenceDate, string input )
        {
            var allResults = new List<ChronoxDateTimeExtraction>();

            var processed = PreProcessExpression(ProcessorType.PreProcessor, input);

            var scanResults = PerformExpressionScanAndReplace(processed);

            foreach (var parser in options.Parsers())
            {
                var results = parser.Execute(scanResults.Key, referenceDate, options);

                allResults.AddRange(results);
            }

            allResults = PostProcessResults(allResults, scanResults.Key, options);

            allResults.Sort();

            return allResults;
        }

        public DateTime ParseDateTime(string input) => ParseDateTime(DateTime.Now,input);


        public DateTime ParseDateTime(DateTime referenceDate, string input) => ParseDateTime(Options, referenceDate, input);


        public DateTime ParseDateTime(ChronoxOption options, string input) => ParseDateTime(options, DateTime.Now, input);


        public DateTime ParseDateTime(ChronoxOption option, DateTime referenceDate, string input )
        { 
            var results = ExtractDates(option, referenceDate, input);

            if (results.Count > 0)
            {
                return results[0].Get(DateRangePointer.Start).DateTime(); 
            }

            return DateTime.MinValue;
        }

        private string PreProcessExpression(ProcessorType type, string input)
        {
            if (Options.Processors(type).Any())
            {
                foreach (var processor in Options.Processors(type))
                {
                    input = processor.PreProcess(Options,input);
                }
            }
            return input;
        }

        private PairWrapper<string,List<ScanResult>> PerformExpressionScanAndReplace(string input)
        {
            var results = new List<ScanResult>();

            var expression = input;

            foreach (var scanner in Options.Scanners())
            {
                var result = scanner.Scan(Options,expression);

                if (result.ResultWrappers.Count > 0)
                {
                    expression = result.NormalizedExpression;

                    results.Add(result);
                }
            }

            return new PairWrapper<string, List<ScanResult>>(expression,results);
        }

        private List<ChronoxDateTimeExtraction> PostProcessResults(List<ChronoxDateTimeExtraction> results, string input, ChronoxOption options)
        {
            if (!options.Processors(ProcessorType.PostProcessor).Any()) return results;

            foreach (var processor in options.Processors(ProcessorType.PostProcessor))
            {
                results = processor.PostProcess(options,results, input);
            }

            return results;
        }
    }
}
