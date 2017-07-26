using Chronox.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Chronox.Wrappers;
using Chronox.Utilities.Extenssions;
using Chronox.Handlers.Patterns;
using Chronox.Handlers.Models;

namespace Chronox.Handlers
{
    public class PatternHandler
    {
        public string GeneratePattern(PatternOption option, Glossary glossary, string label)
        {
            var patternBuilder = new StringBuilder();

            var section = glossary.Section.Find(s => string.Compare(s.Label, label, true) == 0);

            var patterns = section.Properties.Select(p => p.Pattern).ToList();

            switch (GetPatternType(section.Type))
            {
                case PatternType.Combined:

                    patternBuilder.Append(LabelWrapp(label, GroupWrapp(string.Join("|", patterns))));

                    break;
                case PatternType.CombinedOptional:

                    patternBuilder.Append(LabelWrapp(label, OptionalGroupWrapp(string.Join("|", patterns))));

                    break;
                case PatternType.CombinedReversed:

                    patterns.Reverse();

                    patternBuilder.Append(LabelWrapp(label, GroupWrapp(string.Join("|", patterns))));

                    break;
                case PatternType.Default:

                    for (var i = 0; i < section.Properties.Count; i++)
                    {
                        var property = section.Properties[i];

                        switch (GetPatternType(property.Type))
                        {
                            case PatternType.Group:

                                patternBuilder.Append(LabelWrapp(label, GroupWrapp(property.Pattern)));

                                break;
                            case PatternType.Interpreted:

                                patternBuilder.Append(LabelWrapp(label, GroupWrapp(property.Pattern)));

                                break;
                            case PatternType.GroupOptional:

                                patternBuilder.Append(LabelWrapp(label, OptionalGroupWrapp(property.Pattern)));

                                break;
                            case PatternType.Filler:
                            case PatternType.Default:

                                patternBuilder.Append(property.Pattern);

                                break;
                            case PatternType.Optional:
                            case PatternType.Ignored:

                                patternBuilder.Append(LabelWrapp(label, OptionalWrapp(property.Pattern)));

                                break;
                        }

                        if(i < section.Properties.Count - 1)
                        {
                            patternBuilder.Append("|");
                        }
                    }

                    break;
            }

            return patternBuilder.ToString();
        }

        internal string ComputePattern(List<string> variations)
        {
            var patternBuilder = new StringBuilder();

            var values = new List<string>();

            foreach(var variation in variations.OrderByDescending(v => v.Length))
            {
                if (variation.Contains(" ") || variation.Contains(". "))
                {
                    values.Add(StringExtenssions.Replace(StringExtenssions.Replace(variation, " ", "\\s*", true), ". ", "\\. ", true));
                }
                else
                {
                    values.Add(variation);
                }
            }

            patternBuilder.Append(string.Join("|", values.OrderByDescending(v => v.Length)));

            return patternBuilder.ToString();
        }

        public static string LabelWrapp(string label, string value)
        {
            return $"(?<{label}>{value})";
        }

        public static string GroupWrapp(string value)
        {
            return $"({value})";
        }

        public static string OrGroupWrapp(params string[] values)
        {
            return $"({string.Join("|",values)})";
        }

        public static string CaptureWrapp(string value)
        {
            return $"[{value}]";
        }

        public static string OptionalGroupWrapp(string value)
        {
            return $"(?:({value}))?";
        }

        public static string OrOptionalGroupWrapp(params string[] values)
        {
            return $"(?:({string.Join("|",values)}))?";
        }

        public static string OptionalWrapp(string value)
        {
            return $"(?:{value})?";
        }

        internal static bool MissingLabel(string value)
        {
            if(!value.Contains("<") && !value.Contains(">"))
            {
                return true;
            }

            return false;
        }

        public static PatternType GetPatternType(string type)
        {
            if (string.Compare(type, PatternOption.PatternCreationType.Combined, true) == 0) return PatternType.Combined;

            if (string.Compare(type, PatternOption.PatternCreationType.CombinedReversed, true) == 0) return PatternType.CombinedReversed;

            if (string.Compare(type, PatternOption.PatternCreationType.CombinedOptional, true) == 0) return PatternType.CombinedOptional;

            if (string.Compare(type, PatternOption.PatternCreationType.Group, true) == 0) return PatternType.Group;

            if (string.Compare(type, PatternOption.PatternCreationType.Capture, true) == 0) return PatternType.Capture;

            if (string.Compare(type, PatternOption.PatternCreationType.GroupOptional, true) == 0) return PatternType.GroupOptional;

            if (string.Compare(type, PatternOption.PatternCreationType.Filler, true) == 0) return PatternType.Filler;

            if (string.Compare(type, PatternOption.PatternCreationType.Optional, true) == 0) return PatternType.Optional;

            if (string.Compare(type, PatternOption.PatternCreationType.Interpreted, true) == 0) return PatternType.Interpreted;

            if (string.Compare(type, PatternOption.PatternCreationType.Ignored, true) == 0) return PatternType.Ignored;

            return PatternType.Default;
        }
    }
}
