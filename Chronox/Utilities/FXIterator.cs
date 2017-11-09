using System;
using System.Collections.Generic;
using System.Text;

namespace Chronox.Debugers
{
    public static class FXIterator
    {
        public delegate void FXIteration<T>(T current, int index);

        public static void Iterate<T>(List<T> items, FXIteration<T> iterator)
        {
            for (int i = 0; i < items.Count; iterator(items[i], i), i++) ;
        }

        public static void Iterate<T>(List<T> items, int start, int end, FXIteration<T> iterator)
        {
            start = start >= 0 ? start : 0;

            end = end >= 0 && end <= items.Count ? end : items.Count;

            for (int i = start; i < end; iterator(items[i], i), i++) ;
        }

        public static void Iterate<T>(T[] items, FXIteration<T> iterator)
        {
            for (int i = 0; i < items.Length; iterator(items[i], i), i++) ;
        }

        public static void Iterate<T>(T[] items, int start, int end, FXIteration<T> iterator)
        {
            start = start >= 0 ? start : 0;

            end = end >= 0 && end <= items.Length ? end : items.Length;

            for (int i = start; i < end; iterator(items[i], i), i++) ;
        }
    }
}
