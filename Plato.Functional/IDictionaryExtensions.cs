using System;
using System.Collections.Generic;
using Option;
using static Plato.Functional.F;

namespace Plato.Functional
{
    public static class IDictionaryExtensions
    {
        // Dictionary<K,A> -> (A -> B) -> Dictionary<K,B>
        public static Dictionary<K,B> Map<A,B,K>(this Dictionary<K,A> dict, Func<A,B> f)
        {
            Dictionary<K,B> res = new Dictionary<K,B>();
            foreach(KeyValuePair<K,A> pair in dict) res[pair.Key] = f(pair.Value);
            return res;
        }
        
        public static Option<A> LookUp<K,A>(this Dictionary<K,A> dict, K key) => dict.TryGetValue(key, out A a) ? Some(a) : None;
    }
}