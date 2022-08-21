using System;
using System.Collections.Generic;

namespace TerraNova.Common.Utils
{
    public static class Comparer
    {
        public static ReverseComparer<T> Reverse<T>()
        {
            return ReverseComparer<T>.Default;
        }

        public static ReverseComparer<T> Reverse<T>(this IComparer<T> comparer)
        {
            return new ReverseComparer<T>(comparer);
        }
    }

    public sealed class ReverseComparer<T> : IComparer<T>
    {
        public static readonly ReverseComparer<T> Default = new ReverseComparer<T>(Comparer<T>.Default);

        private readonly IComparer<T> comparer = Default;

        public ReverseComparer(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public int Compare(T x, T y)
        {
            return comparer.Compare(y, x);
        }
    }
}