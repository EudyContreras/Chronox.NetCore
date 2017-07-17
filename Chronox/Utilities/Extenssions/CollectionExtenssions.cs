using Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chronox.Utilities.Extenssions
{
    internal static class CollectionExtenssions
    {
        public static void AddAll<TKey, TValue>(this Dictionary<TKey, TValue> source, IDictionary<TKey, TValue> dictionary)
        {
            foreach (var entry in dictionary)
            {
                source.Add(entry.Key, entry.Value);
            }
        }

        public static void AddAll(this Dictionary<DateTimeUnit, int> source, Dictionary<DateTimeUnit, int?> dictionary)
        {
            foreach (var entry in dictionary)
            {
                var value = entry.Value == null ? default(int) : entry.Value;

                source.Add(entry.Key, entry.Value.Value);
            }
        }

        public static void AddRange<TValue>(this HashSet<TValue> source, TValue[] array)
        {
            foreach (var value in array)
            {
                source.Add(value);
            }
        }

        public static void AddRange<TValue>(this HashSet<TValue> source, List<TValue> list)
        {
            foreach (var value in list)
            {
                source.Add(value);
            }
        }

        public static void AddRange<TValue>(this HashSet<TValue> source, HashSet<TValue> set)
        {
            foreach (var value in set)
            {
                source.Add(value);
            }
        }
    }
}
