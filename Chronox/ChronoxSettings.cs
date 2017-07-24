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
    public class ChronoxSettings : IEquatable<ChronoxSettings>
    {
        private int searchPassCount = 3;

        private int minInputTextLength = 3;

        private static ChronoxSettings instance;

        public DateTime ReferenceDate = DateTime.Now;

        public VocabularyHandler Language { get; private set; }

        public ChronoxPreferences Preferences { get; private set; }

        private List<IChronoxProcessor> postProcessors = new List<IChronoxProcessor>();

        private List<IChronoxProcessor> preProcessors = new List<IChronoxProcessor>();

        private List<IChronoxScanner> scanners = new List<IChronoxScanner>();

        private List<IChronoxParser> parsers = new List<IChronoxParser>();

        public ChronoxSettings() : this(new ChronoxPreferences()) { }

        public ChronoxSettings(ChronoxPreferences preferences) : this(preferences, null) { }

        public ChronoxSettings(ChronoxPreferences preferences, List<IChronoxProcessor> processors)
        {
            this.Preferences = preferences ?? new ChronoxPreferences();

            this.Language = SetLanguagePreferences(preferences);

            AddParser(StandardExpressionParsers().ToArray());

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

        private VocabularyHandler SetLanguagePreferences(ChronoxPreferences preferences)
        {
            return VocabularyHandler.GetInstance(this, Definitions.TextLangDataPath, preferences.Languages[0]);
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

        public void SetLangDataSetFilePath(string fullPath)
        {
            Language.DestroyInstance();

            fullPath = fullPath.RemoveSubstrings(".txt", ".json");

            var parts = fullPath.Split('\\');

            var language = parts[parts.Length - 1];

            var path = fullPath.Replace(language, string.Empty).Trim();

            Language = VocabularyHandler.GetInstance(this, path, language);
        }

        public void DestroyInstance() => instance = null;

        private static ChronoxSettings Standardsettings()
        {
            var settings = new ChronoxSettings();

            return settings;
        }

        private static ChronoxSettings CasualModesettings()
        {
            var settings = new ChronoxSettings();

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
            var parsers = new List<IChronoxParser>
            {
                new MasterParser()
            };
            return parsers;
        }

        private IEnumerable<IChronoxParser> StrictExpressionParsers()
        {
            var parsers = new List<IChronoxParser>();

            return parsers;
        }

        private IEnumerable<IChronoxScanner> StandarScanners()
        {
            var scanners = new List<IChronoxScanner>
            {
                new HolidayScanner(),
                new NumberScanner(),
                new CardinalScanner()
            };
            return scanners;
        }

        private IEnumerable<IChronoxProcessor> StandardPostProcessors()
        {
            var processors = new List<IChronoxProcessor>();

            return processors;
        }

        private IEnumerable<IChronoxProcessor> StandardPreProcessors()
        {
            var processors = new List<IChronoxProcessor>
            {
                new ExpressionProcessor()
            };
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

        //TODO: Implement equals effectively by invoking different properties
        public bool Equals(ChronoxSettings other)
        {
            return ChronoxSettings.Equals(this, other);
        }
    }
}
