using System;
using System.Collections.Generic;

namespace Plato.Functional
{
    public static class ISetExtensions
    {
        public static ISet<B> Map<A,B>(this ISet<A> set, Func<A,B> f) 
        {
            var rs = new HashSet<B>();
            foreach (A a in set) rs.Add(f(a));
            return rs;        
        }
    }
}