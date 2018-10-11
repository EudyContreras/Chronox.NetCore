
using Chronox.Wrappers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Chronox.Utilities.Extenssions
{
    internal static class StringExtenssions
    {
        private static readonly char[] Puntuation = new char[] { ',', '.', '!', ':', '?', ';', '"','\''};

        /// <summary>
        /// String extension method for calling contains on string and specifying
        /// the comparizon rule
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static bool Contains(this string source, string target, StringComparison comparisonType)
        {
            return source != null && target != null && source.IndexOf(target, comparisonType) >= 0;
        }

        /// <summary>
        /// Contains method which returns the start and end index of the 
        /// sub string given that it was found. This method also ignores found
        /// strings within other strings as it only works for words and not parts 
        /// of words. Useful for finding both simple and composite entities.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static ContainsWrapper Contains(this string source, string target, bool ignoreCase) => Contains(ignoreCase ? source.ToLower() : source, ignoreCase ? target.ToLower() : target);


        /// <summary>
        /// Contains method which returns the start and end index of the 
        /// sub string given that it was found. This method also ignores found
        /// strings within other strings as it only works for words and not parts 
        /// of words. Useful for finding both simple and composite entities.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static ContainsWrapper Contains(this string source, string target)
        {
            var result = Contains(source, new string[] { target });

            if(result.Count > 0)
            {
                return result[0];
            }

            return null;
        }

        /// <summary>
        /// Contains method which returns the start and end index of the 
        /// sub string given that it was found. This method also ignores found
        /// strings within other strings as it only works for words and not parts 
        /// of words. Useful for finding both simple and composite entities.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        /// 
        public static List<ContainsWrapper> Contains(this string source, params string[] targets) => Contains(source, targets, null);

        public static List<ContainsWrapper> Contains(this string source, IEnumerable<string> targets) => Contains(source, targets, null);

        public static List<ContainsWrapper> Contains(this string source, string target, params string[] ignore) => Contains(source, new string[] { target }, ignore);

        public static List<ContainsWrapper> Contains(this string source, string target, IEnumerable<string> ignore) => Contains(source, new string[] { target }, ignore);

        public static List<ContainsWrapper> Contains(this string source, IEnumerable<string> targets, params string[] ignore) => Contains(source, targets, ignore?.ToList());

        public static List<ContainsWrapper> Contains (this string source, IEnumerable<string> targets, IEnumerable<string> ignore)
        {
            var results = new List<ContainsWrapper>();

            var toIgnore = new List<ContainsWrapper>();

            var sourceLength = source.Length;

            var ignoring = false;

            if(ignore != null)
            {
               if(ignore.Any())
                {
                    toIgnore = Contains(source, ignore, null);

                    if (toIgnore.Count > 0)
                    {
                        ignoring = true;
                    }
                }
            }

            foreach (var target in targets)
            {
                var targetLength = target.Length;

                var counter = 0;

                var startIndex = 0;

                var lastSkipped = false;

                for (var i = 0; i < sourceLength; i++)
                {
                    if (ignoring)
                    {
                        if(toIgnore.Exists(e => ContainsWrapper.Intercepts(i, e.Position)))
                        {
                            lastSkipped = true;

                            continue;
                        }
                        if (lastSkipped)
                        {
                            lastSkipped = false;

                            continue;
                        }
                    }
                    if (char.ToLower(source[i]) == char.ToLower(target[counter]))
                    {
                        if (counter <= 0)
                        {
                            if (i > 0)
                            {
                                if (source[i - 1] == ' ')
                                {
                                    counter++;

                                    startIndex = i;
                                }
                            }
                            else
                            {
                                counter++;

                                startIndex = i;
                            }
                        }
                        else
                        {
                            counter++;
                        }
                    }
                    else
                    {
                        if (counter > 0)
                        {
                            i -= counter;
                        }
                        counter = 0;
                    }

                    if (counter == targetLength)
                    {
                        if (i < sourceLength - 1)
                        {
                            if (source[i + 1] == ' ' || char.IsPunctuation(source[i + 1]))
                            {
                                var current = new ContainsWrapper(true, target, startIndex, i)
                                {
                                    Length = (i - startIndex) + 1
                                };

                                var previous = results.Find(r => r.Intercepts(current.Position));

                                if (previous != null)
                                {
                                    if(previous.Text.Length < current.Text.Length)
                                    {
                                        results.Remove(previous);
                                        results.Add(current);
                                    }
                                }
                                else
                                {
                                    results.Add(current);
                                }                               
                                counter = 0;
                            }
                            else
                            {
                                counter = 0;
                            }
                        }
                        else
                        {
                            var current = new ContainsWrapper(true, target, startIndex, i)
                            {
                                Length = (i - startIndex) + 1
                            };

                            var previous = results.Find(r => r.Intercepts(current.Position));

                            if (previous != null)
                            {
                                if (previous.Text.Length < current.Text.Length)
                                {
                                    results.Remove(previous);
                                    results.Add(current);
                                }
                            }
                            else
                            {
                                results.Add(current);
                            }
                            counter = 0;
                        }
                    }
                }
            }

            return results;
        }

        private static bool MatchIntercepts(IndexWrapper a, IndexWrapper b)
        {
            if (a.StartIndex >= b.StartIndex && a.EndIndex <= b.EndIndex)
            {
                return true;
            }
            else if (b.StartIndex >= a.StartIndex && b.EndIndex <= a.EndIndex)
            {
                return true;
            }

           return false;
        }

        public static string[] Split(this string source, string sequence) => Split(source, sequence, false);


        public static string[] Split(this string source, string sequence, bool trim)
        {
            var parts = new List<string>();

            var mutable = source + sequence ;

            var index = 0;

            while(true)
            {
                var i = mutable.IndexOf(sequence, StringComparison.OrdinalIgnoreCase);

                if (string.IsNullOrEmpty(mutable))
                {
                    break;
                }
                var part = trim ? mutable.Substring(0, i).Trim() : mutable.Substring(0, i);

                if(!string.IsNullOrEmpty(part) && !string.IsNullOrWhiteSpace(part))
                {
                    parts.Add(part);
                }

                mutable = mutable.Substring(i + sequence.Length);

                index++;
            }

            return parts.ToArray();
        }

        public static List<ContainsWrapper> SearchString(string source, string pattern)
        {
            var results = new List<ContainsWrapper>();

            SearchString(results, source, pattern);

            return results;
        }

        public static List<ContainsWrapper> SearchString(string source, params string[] patterns)
        {
            var results = new List<ContainsWrapper>();

            var i = 0;

            for(i = 0; i<patterns.Length; i++)
            {
                SearchString(results, source, patterns[i]);
            }

            return results;
        }
        /// <summary>
        /// Naive pattern search implementation used
        /// for finding and returning the index index of the first pattern
        /// that matches the given pattern
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static void SearchString(List<ContainsWrapper> results, string source, string pattern)
        {
            var i = 0;

            var j = 0;

            var len = pattern.Length;

            for (i = 0, j = 0; i < source.Length - 1; i++, j++)
            {
                if (source[i] == pattern[j])
                {
                    if (j >= (len - 1))
                    {
                        var index = i - (len - 1);

                        results.Add(new ContainsWrapper(true,pattern, index, index + (len - 1)));

                        j = 0;
                    }
                }
                else
                {
                    j = 0;
                }
            }
        }


        public static List<int> SearchPattern(string expression, string pattern) => SearchPattern(expression, pattern, -1);

        public static List<int> SearchPattern(string expression, string pattern, int missingCount)
        {
            var results = new List<int>();

            var split = expression.Split(' ');

            SearchPattern(expression, split, results, pattern, missingCount < 0 ? split.Length : missingCount, 1, 5, 0);

            return results;
        }

        private static void SearchPattern(string expression, string[] splitExpression, List<int> results, string pattern, int missingColorCount, int start, int limit, int added)
        {
            var i = 0;

            for (i = 0; i < splitExpression.Length - start; i++)
            {
                var parts = new string[start];

                for (var j = 0; j < start; j++)
                {
                    parts[j] = splitExpression[i + j];
                }

                var element = string.Join(" ", parts).Trim();

                if(string.Compare(pattern, element) == 0)
                {
                    results.Add(expression.IndexOf(element, StringComparison.OrdinalIgnoreCase));

                    added++;
                }
            }
            if (added < missingColorCount)
            {
                if (start < limit)
                {
                    if ((start + 1) < splitExpression.Length)
                    {
                        SearchPattern(expression, splitExpression, results, pattern, missingColorCount, start + 1, limit, added);
                    }
                }
            }
        }

        /// <summary>
        /// Extension method for finding which of the words from 
        /// the target string are also found in the source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="separators"></param>
        /// <returns></returns>
        public static List<string> FindMatchingWords(this string source, string target, params char[] separators)
        {
            return source.Split(separators, StringSplitOptions.RemoveEmptyEntries).Intersect(target.Split(separators, StringSplitOptions.RemoveEmptyEntries), StringComparer.OrdinalIgnoreCase).ToList();
        }

        /// <summary>
        /// Method for creating a simple space
        /// </summary>
        /// <returns></returns>
        public static string Space() => " ";

        /// <summary>
        /// Returns a string composed of the specified
        /// amount of spaces.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string Spaces(int count)
        {
            string space = string.Empty;

            if(count <= 0)
            {
                return space;
            }
            else
            {
                for(var i = 0; i<count; i++)
                {
                    space += " ";
                }
            }
            return space;
        }

        /// <summary>
        /// Normalizes a string by adding the necessary padding and if specified
        /// removing all common punctuation
        /// </summary>
        /// <param name="source"></param>
        /// <param name="padLeft"></param>
        /// <param name="padRight"></param>
        /// <param name="removePunctuation"></param>
        /// <returns></returns>
        public static string Pad(this string source, int padLeft, int padRight)
        {
            var result = source;

            if (result[result.Length - 1] == ' ')
            {
                padRight = padRight - 1;
            }

            if (padLeft > 0)
            {
                result = Spaces(padLeft) + result;
            }
            if (padRight > 0)
            {
                result = result + Spaces(padRight);
            }
            return result;
        }
        /// <summary>
        /// Normalizes a string by adding the necessary padding and if specified
        /// removing all common punctuation
        /// </summary>
        /// <param name="source"></param>
        /// <param name="padLeft"></param>
        /// <param name="padRight"></param>
        /// <param name="removePunctuation"></param>
        /// <returns></returns>
        public static string Normalize(this string source, int padLeft, int padRight, bool removePunctuation)
        {
            var result = source;

            if(result[result.Length - 1] == ' ')
            {
                padRight = padRight - 1;
            }

            if(padLeft > 0)
            {
                result = Spaces(padLeft) + result;
            }
            if (padRight > 0)
            {
                result = result + Spaces(padRight);
            }
            if (removePunctuation)
            {
                return RemovePunctuation(result);
            }
            return result;
        }

        public static string PadPunctuation(this string source)
        {
            var result = new StringBuilder();

            for (var i = 0; i < source.Length; i++)
            {
                bool skip = false;

                foreach (var combo in Puntuation)
                {
                    if (source[i] == combo && source[i + (i < source.Length - 1 ? 1 : 0)] == ' ')
                    {
                        skip = true;

                        break;
                    }
                }
                if (!skip)
                {
                    result.Append(source[i]);
                }
                else
                {
                    result.Append(" ");
                    result.Append(source[i]);
                }
            }

            return result.ToString();
        }

        public static bool IsPunctuation(this char character, params char[] punctuation) => punctuation.Any(c => c == character);
        

        public static string PadPunctuationExact(this string source, int padLeft, int padRigth, params char[] punctuation)
        {
            var result = new StringBuilder();

            for (var i = 0; i < source.Length; i++)
            {
                bool skip = false;

                foreach (var combo in punctuation)
                {
                    if (source[i] == combo)
                    {
                        skip = true;

                        break;
                    }
                }
                if (!skip)
                {
                    result.Append(source[i]);
                }
                else
                {
                    result.Append(Spaces(padLeft));
                    result.Append(source[i]);
                    result.Append(Spaces(padRigth));
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Extension method which removes common terminal punctuation from 
        /// the specified string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemovePunctuation(this string source)
        {
            var result = new StringBuilder();

            for (var i = 0; i < source.Length; i++)
            {
                bool skip = false;

                foreach (var combo in Puntuation)
                {
                    if (source[i] == combo && source[i + (i < source.Length - 1 ? 1 : 0)] == ' ')
                    {
                        skip = true;
                        break;
                    }
                }
                if (!skip)
                {
                    result.Append(source[i]);
                }
            }
            return result.ToString();
        }


        /// <summary>
        /// Extension method which replaces common terminal punctuation with spaces 
        /// the specified string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string MaskPunctuation(this string source, params char[] punctuation)
        {
            var result = new StringBuilder();

            for (var i = 0; i < source.Length; i++)
            {
                bool skip = false;

                foreach (var combo in punctuation)
                {
                    if (source[i] == combo && source[i + (i < source.Length - 1 ? 1 : 0)] == ' ')
                    {
                        skip = true;

                        result.Append(" ");
                        break;
                    }
                }
                if (!skip)
                {
                    result.Append(source[i]);
                }
            }
            return result.ToString();
        }
        /// <summary>
        /// Extension method which removes padded punctuation from 
        /// the specified string
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string RemovePaddedPunctuation(this string source, int padLeft, int padRight)
        {
            var result = new StringBuilder(source);

            var exclude = new StringBuilder();

            foreach(var punctuation in Puntuation)
            {
                exclude.Append(Spaces(padLeft));
                exclude.Append(punctuation);
                exclude.Append(Spaces(padRight));

                result.Replace(exclude.ToString(), Space());

                exclude.Clear();
            }

            return result.ToString().Trim();
        }

        public static string ReplaceLast(this string source, string fromString, string toString)
        {
            var firsIndexOf = source.IndexOf(fromString,StringComparison.OrdinalIgnoreCase);

            var lastIndexOf = source.LastIndexOf(fromString,StringComparison.OrdinalIgnoreCase);

            if (lastIndexOf < 0 || firsIndexOf == lastIndexOf)
                return source;

            var leading = source.Substring(0, lastIndexOf);

            var charsToEnd = source.Length - (lastIndexOf + fromString.Length);

            var trailing = source.Substring(lastIndexOf + fromString.Length, charsToEnd);

            var result = leading + toString + trailing;

            return result;
        }

        public static string ReplaceFirst(this string source, string fromString, string toString)
        {
            var firsIndexOf = source.IndexOf(fromString,StringComparison.OrdinalIgnoreCase);

            var lastIndexOf = source.LastIndexOf(fromString, StringComparison.OrdinalIgnoreCase);

            if (firsIndexOf < 0 || firsIndexOf == lastIndexOf)
                return source;

            var leading = source.Substring(0, firsIndexOf);

            var charsToEnd = source.Length - (firsIndexOf + fromString.Length);

            var trailing = source.Substring(firsIndexOf + fromString.Length, charsToEnd);

            var result = leading + toString + trailing;

            return result;
        }

        /// <summary>
        /// Extension for removing the specified words from a string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wordsToRemove"></param>
        /// <returns></returns>
        public static string RemoveWords(this string source, params string[] wordsToRemove) => (wordsToRemove == null || wordsToRemove.Length <= 0) ? source :  RemoveWords(source, wordsToRemove.ToList());

        /// <summary>
        /// Extension for removing the specified words from a string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wordsToRemove"></param>
        /// <returns></returns>
        public static string RemoveWords(this string source, List<string> wordsToRemove) => (wordsToRemove == null || wordsToRemove.Count <= 0) ? source : RemoveWords(source.Split(' '), new HashSet<string>(wordsToRemove));

        /// <summary>
        /// Extension for removing the specified words from a string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wordsToRemove"></param>
        /// <returns></returns>
        public static string RemoveWords(this string[] source, HashSet<string> wordsToRemove)
        {
            var cleanSource = new StringBuilder();

            if (wordsToRemove == null || wordsToRemove.Count <= 0) return string.Join(" ", source);

            for (var i = 0; i < source.Length; i++)
            {
                var part = source[i];

                if (!wordsToRemove.Contains(part, StringComparer.OrdinalIgnoreCase))
                {
                    cleanSource.Append(part);
                    cleanSource.Append(" ");
                }
            }
            return cleanSource.ToString().Trim();
        }

        /// <summary>
        /// Extension for replacing the specified words in a string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wordsToReplace"></param>
        /// <returns></returns>
        public static string ReplaceWords(this string source, string replacement, string wordsToReplace, bool ignoreCase) => (wordsToReplace == null || wordsToReplace.Length <= 0) ? source : ReplaceWords(source, replacement, new List<string> { wordsToReplace }, ignoreCase);

        /// <summary>
        /// Extension for replacing the specified words in a string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wordsToReplace"></param>
        /// <returns></returns>
        public static string ReplaceWords(this string source, string replacement, List<string> wordsToReplace, bool ignoreCase) => (wordsToReplace == null || wordsToReplace.Count <= 0) ? source : ReplaceWords(source, replacement, new HashSet<string>(wordsToReplace), ignoreCase);

        /// <summary>
        /// Extension for replacing the specified words in a string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wordsToRemove"></param>
        /// <returns></returns>
        public static string ReplaceWords(this string source, string replacement, HashSet<string> wordsToReplace, bool ignoreCase)
        {
            var cleanSource = source;

            if (wordsToReplace == null || wordsToReplace.Count <= 0) return string.Join(" ", source);

            foreach(var word in wordsToReplace)
            {
                if (cleanSource.Contains(word))
                {
                    var start = cleanSource.IndexOf(word,ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) - 1;
                    var end = start + word.Length + 1;

                    if(start == -1 || end == cleanSource.Length)
                    {
                        if(start == -1)
                        {
                            if (cleanSource[end] == ' ' || char.IsPunctuation(cleanSource[end]))
                            {
                                cleanSource = cleanSource.Replace(word, replacement, ignoreCase);
                            }
                        }
                        else
                        {
                            if (cleanSource[start] == ' ')
                            {
                                cleanSource = cleanSource.Replace(word, replacement, ignoreCase);
                            }
                        }
                    }
                    else if(start == -1 && end == cleanSource.Length)
                    {
                        cleanSource = cleanSource.Replace(word, replacement, ignoreCase);
                    }
                    else
                    {
                        if (cleanSource[start] == ' ' && (cleanSource[end] == ' ' || char.IsPunctuation(cleanSource[end])))
                        {
                            cleanSource = cleanSource.Replace(word, replacement, ignoreCase);
                        }
                    }
                }
            }
            

            return cleanSource;
        }

        public static string RemoveTerminalCharacter(this string source, char characterToRemove)
        {
            var parts = source.Split(' ');

            var result = new StringBuilder();

            foreach(var part in parts)
            {
                var length = part.Length;

                var temp = part;

                if(char.ToLower(part[part.Length - 1]) == char.ToLower(characterToRemove))
                {
                    temp = part.Remove(part.Length - 1, 1);

                    result.Append(temp).Append(" ");
                }
                else
                {
                    result.Append(part).Append(" ");
                }
            }

            return result.ToString().Trim();
        }
        /// <summary>
        /// Extension for removing the specified words from a string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wordsToRemove"></param>
        /// <returns></returns>
        public static string RemoveSubstrings(this string source, params string[] wordsToRemove) => RemoveSubstrings(source, wordsToRemove.ToList());

    
        /// <summary>
        /// Extension for removing the specified words from a string 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="wordsToRemove"></param>
        /// <returns></returns>
        public static string RemoveSubstrings(this string source, IEnumerable<string> wordsToRemove)
        {
            foreach(var part in wordsToRemove)
            {
                source = Replace(source, part, string.Empty,true);
            }

            return source.Trim();
        }

        /// <summary>
        /// Converts the first letter of a string to uppercase
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string FirstLetterToUpper(this string source) => FirstLetterToUpper(source, false);

        /// <summary>
        /// Converts the first letter of a string to uppercase
        /// and if specified it will convert the rest to lowercase
        /// </summary>
        /// <param name="source"></param>
        /// <param name="restToLower"></param>
        /// <returns></returns>
        public static string FirstLetterToUpper(this string source, bool restToLower)
        {
            if (source == null)
                return null;

            if (restToLower)
            {
                if (source.Length > 1)
                    return char.ToUpper(source[0]) + source.ToLower().Substring(1);
            }
            else
            {
                if (source.Length > 1)
                    return char.ToUpper(source[0]) + source.Substring(1);
            }

            return source.ToUpper();
        }

        /// <summary>
        /// Replaces the specified pattern with the specified replacement
        /// using the specified comparizon method
        /// </summary>
        /// <param name="original"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static string Replace(string original, string pattern, string replacement, StringComparison comparisonType)
        {
            return Replace(original, pattern, replacement, comparisonType, -1);
        }

        /// <summary>
        /// Replaces the specified pattern with the specified replacement.
        /// allows case to be ignored
        /// </summary>
        /// <param name="original"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static string Replace(this string original, string pattern, string replacement, bool ignoreCase)
        {
            return Replace(original, pattern, replacement, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal, -1);
        }

        /// <summary>
        /// Replaces the specified pattern with the specified replacement
        /// using the specified comparizon method
        /// </summary>
        /// <param name="original"></param>
        /// <param name="pattern"></param>
        /// <param name="replacement"></param>
        /// <param name="comparisonType"></param>
        /// <param name="stringBuilderInitialSize"></param>
        /// <returns></returns>
        private static string Replace(string original, string pattern, string replacement, StringComparison comparisonType, int stringBuilderInitialSize)
        {
            if (original == null)
            {
                return null;
            }

            if (string.IsNullOrEmpty(pattern))
            {
                return original;
            }

            var posCurrent = 0;
            var bufferSize = 4096;
            var lenPattern = pattern.Length;
            var idxNext = original.IndexOf(pattern, comparisonType);

            var result = new StringBuilder(stringBuilderInitialSize < 0 ? Math.Min(bufferSize, original.Length) : stringBuilderInitialSize);

            while (idxNext >= 0)
            {
                result.Append(original, posCurrent, idxNext - posCurrent);
                result.Append(replacement);

                posCurrent = idxNext + lenPattern;

                idxNext = original.IndexOf(pattern, posCurrent, comparisonType);
            }

            result.Append(original, posCurrent, original.Length - posCurrent);

            return result.ToString();
        }
    }
}
