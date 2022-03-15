using System;
using System.Collections.Generic;
using Option;
using static Plato.Functional.F;

namespace Plato.Functional
{
    public static class IEnumerableExtensions
    {        
        public static Option<T> LookUp<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            foreach (T item in enumerable) if(predicate(item)) return Some(item);
            return None;
        }
    }
}