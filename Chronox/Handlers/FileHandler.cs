using Chronox.Constants;
using Chronox.Handlers.Models;
using Chronox.Handlers.Wrappers;
using Chronox.Utilities.Extenssions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chronox.Handlers
{
    public class FileHandler
    {
        private const string Language = "language";
        private const string Ignored = "ignored";
        private const string AssumeSpace = "assumespace";
        private const string PreferedEndianFormat = "preferedendianformat";
        private const string SupportedDateTimeFormats = "supporteddatetimeformats";
        private const string SupportedTimeRangeFormats = "supportedtimerangeformats";
        private const string SupportedTimeSpanFormats = "supportedtimespanformats";
        private const string SupportedTimeSetFormats = "supportedtimesetformats";

        private const string End = "<----------------->";
        private const string Key = "key";
        private const string Value = "value";
        private const string Type = "type";
        private const string Pattern = "pattern";
        private const string Variations = "variations";

        private string GetPath(string directory, string langName, string extension)
        {
            var fileName = new StringBuilder(langName.FirstLetterToUpper(true)).Append(extension);

            return Path.Combine(directory, fileName.ToString());
        }

        public void NormalizeFile(string directory, string langName)
        {
            var lines = ReadFile(GetPath(directory, langName, ".txt")).Select(s => s.TrimEnd());

            var newLines = new List<string>();

            var cleanLines = new List<string>();

            var lastLine = ".";

            foreach(var line in lines)
            {
                if (EmptyLine(lastLine))
                {
                    if (!EmptyLine(line))
                    {
                        newLines.Add(line);
                    }
                }
                else
                {
                    newLines.Add(line);
                }

                lastLine = line;
            }

            foreach(var line in newLines)
            {
                var cleanLine = line;

                if (!EmptyLine(cleanLine))
                {
                    if (cleanLine[cleanLine.Length - 1] == ',')
                    {
                        cleanLine = new string( cleanLine.Take(cleanLine.Length - 1).ToArray());
                    }
                }

                cleanLines.Add(cleanLine.TrimEnd());
            }

            File.WriteAllLines(GetPath(directory, langName, ".txt"), cleanLines);
        }

        public void GenerateTemplateTxt(string directory, string langName)
        {
            var lines = ReadFile(GetPath(directory, langName, ".txt")).Select(s => s.TrimEnd());

            var newLines = new List<string>();

            foreach(var line in lines)
            {
                var part = line.Split(':');

                var key = part[0];

                var clean = line;

                if (!EmptyLine(key))
                {
                    if(string.Compare(key.Trim(), Pattern, true) == 0)
                    {
                        clean = key + ":";
                    }
                    else if(string.Compare(key.Trim(), Variations, true) == 0)
                    {
                        clean = key + ":";
                    }
                }

                newLines.Add(clean);
            }

            File.WriteAllLines(@"Languages\Files\Template.txt", newLines);
        }

        public void GenerateTemplateJson(string directory, string langName)
        {
            var lines = ReadFile(GetPath(directory, langName, ".json")).Select(s => s.TrimEnd());

            var newLines = new List<string>();

            foreach (var line in lines)
            {
                var part = line.Split(':');

                var key = part[0];

                var clean = line;

                if (!EmptyLine(key))
                {
                    if (string.Compare(key.RemoveSubstrings("\"").Trim(), Pattern, true) == 0)
                    {
                        clean = string.Concat(key, ":", " \"\",");
                    }
                    else if (string.Compare(key.RemoveSubstrings("\"").Trim(), Variations, true) == 0)
                    {
                        clean = string.Concat(key, ":"," \"\"");
                    }
                }

                newLines.Add(clean);
            }

            File.WriteAllLines(@"Languages\Files\Template.json", newLines);
        }

        private List<string> ReadFile(string filePath)
        {
            return File.ReadLines(filePath).ToList();
        }

        public Glossary CreateGlossary(string directory, string langName)
        {
            return CreateGlossary(ReadFile(GetPath(directory, langName, ".txt")));
        }

        public Glossary CreateGlossary(string path)
        {
            return CreateGlossary(ReadFile(path));
        }

        private Glossary CreateGlossary(List<string> rawLines)
        {
            var sections = new List<Section>();

            var glossary = new Glossary();

            var lines = new List<string>();

            RemoveComments(rawLines, lines);

            for (var i = 0; i < lines.Count; i++)
            {
                var line = lines[i];

                var parts = line.Split(':');

                var attribute = parts[0].Trim().ToLower();

                var value = parts.Length > 1 ? parts[1].Trim().ToLower() : null;

                if (!EmptyLine(attribute))
                {
                    switch (attribute)
                    {
                        case Language:

                            if (value != null)
                            {
                                glossary.Language = value;
                            }
                            break;
                        case Ignored:

                            if (value != null)
                            {
                                glossary.Ignored = value.Split(',').Where(v => !EmptyLine(v)).Select(v => v.Trim()).ToList();
                            }

                            break;
                        case AssumeSpace:

                            if (value != null)
                            {
                                glossary.AssumeSpace = bool.Parse(value);
                            }
                            break;
                        case PreferedEndianFormat:

                            if (value != null)
                            {
                                glossary.PreferedEndianFormat = value.ToUpper();
                            }
                            break;
                        case SupportedDateTimeFormats:

                            glossary.SupportedDateTimeFormats = ExtractFormats(lines, SupportedTimeRangeFormats, i);

                            break;
                        case SupportedTimeRangeFormats:

                            glossary.SupportedTimeRangeFormats = ExtractFormats(lines, SupportedTimeSpanFormats, i);

                            break;
                        case SupportedTimeSpanFormats:

                            glossary.SupportedTimeSpanFormats = ExtractFormats(lines, SupportedTimeSetFormats, i);

                            break;
                        case SupportedTimeSetFormats:

                            glossary.SupportedTimeSetFormats = ExtractFormats(lines, Definitions.Property.CasualExpressions.ToLower(), i);

                            break;
                        default:

                            HandleSections(lines, sections, attribute, value, i);

                            break;
                    }
                }
            }

            glossary.Section = sections;

            return glossary;
        }

        private static void RemoveComments(List<string> rawLines, List<string> lines)
        {
            foreach (var line in rawLines)
            {
                if (line.Contains("//*") && line.Contains("*//"))
                {
                    var parts = line.Split("//*");

                    if (parts.Length > 1)
                    {
                        if (!line.StartsWith("//*"))
                        {
                            lines.Add(parts[0]);
                        }
                    }
                }
                else
                {
                    lines.Add(line);
                }

            }

            foreach(var line in lines)
            {
                if (line.Contains("//*") || line.Contains("*//"))
                {
                    throw new Exception("Please review the comments written and make sure the follow the right format");
                }
            }
        }

        private List<string> ExtractFormats(List<string> lines, string endFormat, int index)
        {
            var formats = lines.Skip(index + 1).Select(s => s.ToLower()).ToList();

            var dateTimeFormats = new List<string>();

            foreach (var format in formats)
            {
                if (format.Contains(endFormat))
                {
                    break;
                }
                if (EmptyLine(format)) continue;

                var dateTimeFormat = format.ToUpper().Split("//*")[0].Trim();

                dateTimeFormats.Add(dateTimeFormat);
            }

            return dateTimeFormats;
        }

        private void HandleSections(List<string> lines, List<Section> sections, string attribute, string value, int index)
        {
            foreach (var propertyName in Definitions.Property.Dynamic.Values)
            {
                if(string.Compare(propertyName, attribute, true) == 0)
                {
                    var section = new Section();

                    section.Label = propertyName;

                    if (value != null)
                    {
                        section.Type = value;
                    }

                    section.Properties = HandleProperties(lines, index);

                    sections.Add(section);
                }
            }
        }

        private List<Property> HandleProperties(List<string> lines, int index)
        {
            var properties = new List<Property>();

            var sectAtributes = lines.Skip(index + 1).ToList();

            var propAttributes = sectAtributes.TakeWhile(s => string.Compare(s.Trim(), End, true) != 0).Select(s => s.ToLower().Trim()).ToList();

            var property = new Property();

            foreach (var propAttribute in propAttributes)
            {
                if (EmptyLine(propAttribute)) continue;

                var parts = propAttribute.Split(':');

                var key = parts[0].Trim();

                var val = string.Join(":", parts.Skip(1)).Trim();

                switch (key)
                {
                    case Key:
                        property = new Property();
                        property.Key = val;
                        break;
                    case Value:
                        property.Value = val;
                        break;
                    case Type:
                        property.Type = val;
                        break;
                    case Pattern:
                        property.Pattern = val;
                        break;
                    case Variations:
                        var variations = val.Split(',').Select(v => v.Trim()).ToList();

                        property.Variations = variations;
                        properties.Add(property);
                        break;
                }
            }
            return properties;
        }

        private bool EmptyLine(string line)
        {
            return string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line);
        }
    }
}
