using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Option;
using static Plato.Functional.F;
using Unit = System.ValueTuple;

namespace Plato.Functional
{
    public static class IEnumerableExtensions
    {        
        public static Option<A> LookUp<A>(this IEnumerable<A> enumerable, Func<A, bool> predicate)
        {
            foreach (A item in enumerable) if(predicate(item)) return Some(item);
            return None;
        }

        public static IEnumerable<B> Map<A,B>(this IEnumerable<A> enumerable, Func<A,B> f) => enumerable.Bind(x => LiftToList(f(x)));

        public static IEnumerable<Unit> FoBEach<A>(this IEnumerable<A> enumerable, Action<A> action) => enumerable.Map(action.ToFunc()).ToImmutableList();

        public static IEnumerable<B> Bind<A,B>(this IEnumerable<A> enumrable, Func<A, IEnumerable<B>> f)
        {
            foreach(A t in enumrable) foreach(B b in f(t)) yield return b;
        }

        public static IEnumerable<B> Bind<A,B>(this IEnumerable<A> enumrable, Func<A, Option<B>> f) => enumrable.Bind(x => f(x).AsIEnumerable());
        
        public static IEnumerable<A> LiftToList<A>(params A[] items) => items.ToImmutableList();
    }
}