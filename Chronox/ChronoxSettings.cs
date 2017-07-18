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
    internal class ChronoxSettings
    {
        private int searchPassCount = 3;

        private int minInputTextLength = 3;

        private static ChronoxSettings instance;

        public DateTime ReferencDate { get; set; } = DateTime.Now;

        public LanguageHandler Language { get; private set; }

        public ChronoxPreferences Preferences { get; private set; }

        private List<IChronoxProcessor> postProcessors = new List<IChronoxProcessor>();

        private List<IChronoxProcessor> preProcessors = new List<IChronoxProcessor>();

        private List<IChronoxScanner> scanners = new List<IChronoxScanner>();

        private List<IChronoxParser> parsers = new List<IChronoxParser>();

        public TimeParseType TimeParsing { get; set; } = TimeParseType.MilitaryTime;

        public DateParseType DateParsing { get; set; } = DateParseType.Standard;

        public ChronoxSettings(string language) : this(language,TimeParseType.MilitaryTime,DateParseType.LongFormat) { }

        public ChronoxSettings(string language, TimeParseType timeType, DateParseType dateType) : this(language, timeType, dateType,null,null) {}

        public ChronoxSettings(string language, TimeParseType timeType, DateParseType dateType, List<IChronoxProcessor> processors) : this(language, timeType, dateType, null, processors) { }

        public ChronoxSettings(string language, TimeParseType timeType, DateParseType dateType, List<IChronoxParser> parsers) : this(language, timeType,dateType,parsers,null) { }

        public ChronoxSettings(string language, TimeParseType timeType, DateParseType dateType, List<IChronoxParser> parsers, List<IChronoxProcessor> processors)
        {
            this.TimeParsing= timeType;
            this.DateParsing = dateType;
            this.Preferences = new ChronoxPreferences(this);
            this.Language = !string.IsNullOrEmpty(language) ? LanguageHandler.GetInstance(this, Definitions.FilePath, language) : LanguageHandler.DefaultLanguage(this);
            this.parsers = parsers ?? new List<IChronoxParser>(StandardExpressionParsers());

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

        public static ChronoxSettings Standard
        {
            get
            {
                if (instance == null)
                {
                    instance = Standardsettings(); ;
                }
                return instance;
            }
            set { }
        }

        public static ChronoxSettings Casual
        {
            get
            {
                if (instance == null)
                {
                    instance = CasualModesettings(); ;
                }
                return instance;
            }
            set { }
        }

        public static ChronoxSettings Strict
        {
            get
            {
                if (instance == null)
                {
                    instance = StrictModesettings(); ;
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

        private static ChronoxSettings Standardsettings()
        {
            var settings = new ChronoxSettings(null);

            settings.AddProcessor(ProcessorType.PostProcessor, settings.StandardPostProcessors().ToArray());
            settings.AddProcessor(ProcessorType.PreProcessor, settings.StandardPreProcessors().ToArray());

            settings.AddParser(settings.CasualExpressionParsers().ToArray());
            settings.AddScanner(settings.StandarScanners().ToArray());

            return settings;
        }

        private static ChronoxSettings CasualModesettings()
        {
            var settings = new ChronoxSettings(null);

            settings.AddProcessor(ProcessorType.PostProcessor, settings.StandardPostProcessors().ToArray());
            settings.AddProcessor(ProcessorType.PreProcessor, settings.StandardPreProcessors().ToArray());

            settings.AddParser(settings.StandardExpressionParsers().ToArray());
            settings.AddScanner(settings.StandarScanners().ToArray());

            return settings;
        }

        private static ChronoxSettings StrictModesettings()
        {
            var settings = new ChronoxSettings(null);

            settings.AddProcessor(ProcessorType.PostProcessor, settings.StandardPostProcessors().ToArray());
            settings.AddProcessor(ProcessorType.PreProcessor, settings.StandardPreProcessors().ToArray());

            settings.AddParser(settings.StrictExpressionParsers().ToArray());
            settings.AddScanner(settings.StandarScanners().ToArray());

            return settings;
        }

        private IEnumerable<IChronoxParser> StandardExpressionParsers()
        {
            var parsers = new List<IChronoxParser>();

            parsers.Add(new MasterParser());

            return parsers;
        }

        private IEnumerable<IChronoxParser> CasualExpressionParsers()
        {
            var parsers = new List<IChronoxParser>();

            parsers.Add(new MasterParser());

            return parsers;
        }

        private IEnumerable<IChronoxParser> StrictExpressionParsers()
        {
            var parsers = new List<IChronoxParser>();

            return parsers;
        }

        private IEnumerable<IChronoxScanner> StandarScanners()
        {
            var scanners = new List<IChronoxScanner>();

            scanners.Add(new HolidayScanner());
            scanners.Add(new NumberScanner());
            scanners.Add(new CardinalScanner());

            return scanners;
        }

        private IEnumerable<IChronoxProcessor> StandardPostProcessors()
        {
            var processors = new List<IChronoxProcessor>();

            return processors;
        }

        private IEnumerable<IChronoxProcessor> StandardPreProcessors()
        {
            var processors = new List<IChronoxProcessor>();

            processors.Add(new ExpressionProcessor());

            return processors;
        }

        public void AddParser(params IChronoxParser[] parser) => parsers.AddRange(parser);

        public void AddScanner(params IChronoxScanner[] scanner) => scanners.AddRange(scanner);

        public void RemoveParser(IChronoxParser parser) => parsers.Remove(parser);

        public void RemoveScanner(IChronoxScanner scanner) => scanners.Remove(scanner);

        public void AddProcessor(ProcessorType type, params IChronoxProcessor[] processors)
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

        public void RemoveProcessor(ProcessorType type, IChronoxProcessor processor)
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

        public IEnumerable<IChronoxParser> Parsers() => parsers;

        public IEnumerable<IChronoxScanner> Scanners() => scanners;

        public IEnumerable<IChronoxProcessor> Processors(ProcessorType type) => type == ProcessorType.PostProcessor ? postProcessors : preProcessors;

    }
}
