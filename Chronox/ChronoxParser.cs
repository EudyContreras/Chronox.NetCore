﻿using Chronox.Interfaces;
using Chronox.Parsers;
using Chronox.Parsers.English;
using Chronox.Scanners;
using Chronox.Utilities.Extenssions;
using Chronox.Wrappers;
using Enumerations;
using System;
using System.Collections.Generic;
using Chronox.Exceptions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chronox.Handlers.Models;

namespace Chronox
{
    public class ChronoxParser : IChronox
    {
        private List<IChronoxScanner> scanners;

        private MasterParser MasterParser = new MasterParser();

        private static ChronoxSettings StandardSettings = new ChronoxSettings();

        private ChronoxParser(ChronoxSettings settings)
        {
            Settings = settings ?? StandardSettings;
            scanners = StandardScanners().ToList();
        }

        private static IChronox Instance = null;

        public static IChronox GetInstance() => GetInstance(StandardSettings);

        public static IChronox GetInstance(ChronoxSettings settings)
        {
            if (Instance == null)
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

        private ChronoxSettings settings = new ChronoxSettings(new Language("English",ChronoxLangSettings.Default));

        public ChronoxSettings Settings
        {
            get
            {
                return settings;
            }
            set
            {
                if (value != null)
                {
                    settings = value;
                }
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

            if(!InputCompliesWithSettings(settings, input)){
                
                switch(settings.InvalidInputResolver){
                    case InvalidInputResolver.Empty:
                        return ResultWrapper.EmptyResult(this);
                    case InvalidInputResolver.Exception:
                        throw new InvalidInputException(input);
                    case InvalidInputResolver.Null:
                        return null;
                }
            }

            var allResults = new List<IChronoxExtraction>();

            var scanResults = PerformExpressionScanAndReplace(input);

            var information = new ChronoxBuildInformation(this, scanResults.Value, input, scanResults.Key, settings);

            var preProcessed = PreProcessExpression(settings, scanResults.Key);

            if (referenceDate == DateTime.MinValue)
            {
                referenceDate = DateTime.Now;
            }

            settings.ReferenceDate = referenceDate;

            allResults.AddRange(MasterParser.ComputeResult(information.ProcessedString = preProcessed, settings.ReferenceDate, settings,information));

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
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            Settings.ParsingMode = ExtractionResultType.DateTime;

            return Parse(referenceDate, input)?.Results.Cast<ChronoxDateTimeExtraction>().ToList();
        }


        IReadOnlyList<ChronoxTimeRangeExtraction> IChronox.ParseTimeRange(string input) => ParseTimeRange(DateTime.MinValue, input);


        IReadOnlyList<ChronoxTimeRangeExtraction> IChronox.ParseTimeRange(DateTime referenceDate, string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            Settings.ParsingMode = ExtractionResultType.TimeRange;

            return Instance.Parse(referenceDate, input)?.Results.Cast<ChronoxTimeRangeExtraction>().ToList();
        }


        IReadOnlyList<ChronoxTimeSpanExtraction> IChronox.ParseTimeSpan(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            Settings.ParsingMode = ExtractionResultType.TimeSpan;

            return Instance.Parse(input)?.Results.Cast<ChronoxTimeSpanExtraction>().ToList();
        }


        IReadOnlyList<ChronoxTimeSetExtraction> IChronox.ParseTimeSet(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            Settings.ParsingMode = ExtractionResultType.TimeSet;

            return Instance.Parse(DateTime.MinValue, input)?.Results.Cast<ChronoxTimeSetExtraction>().ToList();
        }

        private string PreProcessExpression(ChronoxSettings settings, string expression)
        {
            switch (settings.ParsingMode)
            {
                case ExtractionResultType.General:

                    expression = CleanExpression(expression, settings);

                    break;
                case ExtractionResultType.TimeSpan:

                    expression = expression.RemoveWords(settings.Language.Vocabulary.TimeSpanIgnored);

                    break;
                case ExtractionResultType.DateTime:

                    expression = expression.RemoveWords(settings.Language.Vocabulary.DateTimeIgnored);

                    break;
                case ExtractionResultType.TimeSet:

                    expression = expression.RemoveWords(settings.Language.Vocabulary.TimeSetIgnored);

                    break;
                case ExtractionResultType.TimeRange:

                    expression = expression.RemoveWords(settings.Language.Vocabulary.TimeRangeIgnored);

                    break;
                default:

                    expression = CleanExpression(expression, settings);

                    break;
            }

            expression = expression.Contains("  ") ? expression.Replace("  ", " ", false) : expression;

            expression = expression[expression.Length-1] != ' ' ? expression.Pad(0, 1) : expression;;

            return expression;
        }

        private string CleanExpression(string expression, ChronoxSettings settings)
        {
            expression = expression.RemoveWords(settings.Language.Vocabulary.DateTimeIgnored);
            expression = expression.RemoveWords(settings.Language.Vocabulary.TimeRangeIgnored);
            expression = expression.RemoveWords(settings.Language.Vocabulary.TimeSpanIgnored);
            expression = expression.RemoveWords(settings.Language.Vocabulary.TimeSetIgnored);

            return expression;
        }


        public IEnumerable<IChronoxScanner> Scanners {
            get{
                return scanners;
            }
        }


        private IReadOnlyList<IChronoxScanner> StandardScanners()
        {
            return new List<IChronoxScanner>
            {
                new PunctuationScanner(),
                new HolidayScanner(),
                new NumberScanner(),
                new CardinalScanner()
            };
        }

        public void AddScanner(params IChronoxScanner[] scanner) => scanners.AddRange(scanner);


        public void RemoveScanner(IChronoxScanner scanner) => scanners.Remove(scanner);


        private KeyValuePair<string, List<ScanWrapper>> PerformExpressionScanAndReplace(string input)
        {
            var results = new List<ScanWrapper>();

            var expression = input;

            foreach (var scanner in Scanners)
            {
                var result = scanner.Scan(Settings, expression);

                if (result.ResultWrappers.Count > 0)
                {
                    expression = result.NormalizedExpression;

                    results.Add(result);
                }
            }
            return new KeyValuePair<string, List<ScanWrapper>>(expression, results);
        }

        private bool InputCompliesWithSettings(ChronoxSettings settings, string input){

            if (input.Length < settings.MinInputTextLength) return false;

            if (string.IsNullOrEmpty(input)) return false;

            return true;
        }

        private bool ResultCompliesWithSettings(ChronoxSettings settings, ResultWrapper result){

            return true;
        }
    }
}