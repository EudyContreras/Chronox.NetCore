using Chronox.Constants;
using Chronox.Exceptions;
using Chronox.Handlers;
using Chronox.Interfaces;
using Chronox.Parsers;
using Chronox.Parsers.English;
using Chronox.Processors;
using Chronox.Processors.PreProcessors;
using Chronox.Resolutions;
using Chronox.Resolutions.Resolvers;
using Chronox.Scanners;
using Chronox.Utilities.Extenssions;
using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox
{
    internal class ChronoxOption
    {
        private int searchPassCount = 3;

        private int minInputTextLength = 3;

        private static ChronoxOption instance;

        public LanguageHandler Language { get; private set; }

        public ChronoxPreferences Preferences { get; private set; }

        private List<IProcessor> postProcessors = new List<IProcessor>();

        private List<IProcessor> preProcessors = new List<IProcessor>();

        private List<IScanner> scanners = new List<IScanner>();

        private List<IParser> parsers = new List<IParser>();

        public TimeParseType TimeParsing { get; set; } = TimeParseType.MilitaryTime;

        public DateParseType DateParsing { get; set; } = DateParseType.Standard;

        public ChronoxOption(string language) : this(language,TimeParseType.MilitaryTime,DateParseType.LongFormat) { }

        public ChronoxOption(string language, TimeParseType timeType, DateParseType dateType) : this(language, timeType, dateType,null,null) {}

        public ChronoxOption(string language, TimeParseType timeType, DateParseType dateType, List<IProcessor> processors) : this(language, timeType, dateType, null, processors) { }

        public ChronoxOption(string language, TimeParseType timeType, DateParseType dateType, List<IParser> parsers) : this(language, timeType,dateType,parsers,null) { }

        public ChronoxOption(string language, TimeParseType timeType, DateParseType dateType, List<IParser> parsers, List<IProcessor> processors)
        {
            this.TimeParsing= timeType;
            this.DateParsing = dateType;
            this.Preferences = new ChronoxPreferences(this);
            this.Language = !string.IsNullOrEmpty(language) ? LanguageHandler.GetInstance(this, Definitions.FilePath, language) : LanguageHandler.DefaultLanguage(this);
            this.parsers = parsers ?? new List<IParser>(StandardExpressionParsers());

            if(processors!= null)
            {
                foreach (var processor in processors)
                {
                    switch (processor.Type)
                    {
                        case ProcessorType.PreProcessor:
                            preProcessors.Add(processor);
                            break;
                        case ProcessorType.PostProcessor:
                            postProcessors.Add(processor);
                            break;
                    }
                }
            }
            else
            {
                AddProcessor(ProcessorType.PostProcessor, StandardPostProcessors().ToArray());

                AddProcessor(ProcessorType.PreProcessor, StandardPreProcessors().ToArray());

                AddScanner(StandarScanners().ToArray());
            }
        }

        public static ChronoxOption Standard
        {
            get
            {
                if (instance == null)
                {
                    instance = StandardOptions(); ;
                }
                return instance;
            }
            set { }
        }

        public static ChronoxOption Casual
        {
            get
            {
                if (instance == null)
                {
                    instance = CasualModeOptions(); ;
                }
                return instance;
            }
            set { }
        }

        public static ChronoxOption Strict
        {
            get
            {
                if (instance == null)
                {
                    instance = StrictModeOptions(); ;
                }
                return instance;
            }
            set { }
        }

        public int SearchPassCount
        {
            get
            {
                return searchPassCount;
            }
            internal set
            {
                if(searchPassCount >= 1)
                {
                    searchPassCount = value;
                }
                else
                {
                    throw new UnsupportedSearchPassException(value);
                }
            }
        }

        public int MinInputTextLength
        {
            get
            {
                return minInputTextLength;
            }
            internal set
            {
                if (minInputTextLength >= 3)
                {
                    minInputTextLength = value;
                }
            }
        }


        public void SetLanguageFilePath(string fullPath)
        {
            Language.DestroyInstance();

            fullPath = fullPath.RemoveSubstrings(".txt", ".json");

            var parts = fullPath.Split('\\');

            var language = parts[parts.Length - 1];

            var path = fullPath.Replace(language, string.Empty).Trim();

            Language = LanguageHandler.GetInstance(this, path, language);
        }

        public void DestroyInstance() => instance = null;

        private static ChronoxOption StandardOptions()
        {
            var options = new ChronoxOption(null);

            options.AddProcessor(ProcessorType.PostProcessor, options.StandardPostProcessors().ToArray());
            options.AddProcessor(ProcessorType.PreProcessor, options.StandardPreProcessors().ToArray());

            options.AddParser(options.CasualExpressionParsers().ToArray());
            options.AddScanner(options.StandarScanners().ToArray());

            return options;
        }

        private static ChronoxOption CasualModeOptions()
        {
            var options = new ChronoxOption(null);

            options.AddProcessor(ProcessorType.PostProcessor, options.StandardPostProcessors().ToArray());
            options.AddProcessor(ProcessorType.PreProcessor, options.StandardPreProcessors().ToArray());

            options.AddParser(options.StandardExpressionParsers().ToArray());
            options.AddScanner(options.StandarScanners().ToArray());

            return options;
        }

        private static ChronoxOption StrictModeOptions()
        {
            var options = new ChronoxOption(null);

            options.AddProcessor(ProcessorType.PostProcessor, options.StandardPostProcessors().ToArray());
            options.AddProcessor(ProcessorType.PreProcessor, options.StandardPreProcessors().ToArray());

            options.AddParser(options.StrictExpressionParsers().ToArray());
            options.AddScanner(options.StandarScanners().ToArray());

            return options;
        }

        private IEnumerable<IParser> StandardExpressionParsers()
        {
            var parsers = new List<IParser>();

            parsers.Add(new MasterParser());

            return parsers;
        }

        private IEnumerable<IParser> CasualExpressionParsers()
        {
            var parsers = new List<IParser>();

            parsers.Add(new MasterParser());

            return parsers;
        }

        private IEnumerable<IParser> StrictExpressionParsers()
        {
            var parsers = new List<IParser>();

            return parsers;
        }

        private IEnumerable<IScanner> StandarScanners()
        {
            var scanners = new List<IScanner>();

            scanners.Add(new HolidayScanner());
            scanners.Add(new NumberScanner());
            scanners.Add(new CardinalScanner());

            return scanners;
        }

        private IEnumerable<IProcessor> StandardPostProcessors()
        {
            var processors = new List<IProcessor>();

            return processors;
        }

        private IEnumerable<IProcessor> StandardPreProcessors()
        {
            var processors = new List<IProcessor>();

            processors.Add(new ExpressionProcessor());

            return processors;
        }

        public void AddParser(params IParser[] parser) => parsers.AddRange(parser);

        public void AddScanner(params IScanner[] scanner) => scanners.AddRange(scanner);

        public void RemoveParser(IParser parser) => parsers.Remove(parser);

        public void RemoveScanner(IScanner scanner) => scanners.Remove(scanner);

        public void AddProcessor(ProcessorType type, params IProcessor[] processors)
        {
            switch (type)
            {
                case ProcessorType.PreProcessor:
                    preProcessors.AddRange(processors);
                    break;
                case ProcessorType.PostProcessor:
                    postProcessors.AddRange(processors);
                    break;
            }
        }

        public void RemoveProcessor(ProcessorType type, IProcessor processor)
        {
            switch (type)
            {
                case ProcessorType.PreProcessor:
                    preProcessors.Remove(processor);
                    break;
                case ProcessorType.PostProcessor:
                    postProcessors.Remove(processor);
                    break;
            }
        }

        public IEnumerable<IParser> Parsers() => parsers;

        public IEnumerable<IScanner> Scanners() => scanners;

        public IEnumerable<IProcessor> Processors(ProcessorType type) => type == ProcessorType.PostProcessor ? postProcessors : preProcessors;

    }
}
