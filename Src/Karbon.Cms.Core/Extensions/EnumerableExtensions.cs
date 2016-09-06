using System;
using System.Collections.Generic;

namespace Karbon.Cms.Core
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Finds the index of the supplied item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public static int FindIndex<T>(this IEnumerable<T> list, T item)
        {
            var idx = 0;
            foreach (var l in list)
            {
                if (l.Equals(item))
                    return idx;
                idx++;
            }
            return -1;
        }

        /// <summary>
        /// Finds the index of the supplied item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="finder">The finder.</param>
        /// <returns></returns>
        public static int FindIndex<T>(this IEnumerable<T> list, Predicate<T> finder)
        {
            var idx = 0;
            foreach (var l in list)
            {
                if (finder(l))
                    return idx;
                idx++;
            }
            return -1;
        }
    }
}
