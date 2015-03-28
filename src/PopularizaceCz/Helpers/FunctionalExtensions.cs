using System;
using System.Collections.Generic;

namespace PopularizaceCz.Helpers
{
    public static class FunctionalExtensions
    {
        public static HashSet<T> ToSet<T>(this IEnumerable<T> e)
        {
            return new HashSet<T>(e);
        }
    }
}