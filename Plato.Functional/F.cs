using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using Option;
using Unit = System.ValueTuple;

namespace Plato.Functional
{
    public static partial class F
    {
        public static Unit Unit() => default(Unit);

        public static R Using<T,R>(T disposable, Func<T,R> f) where T : IDisposable
        {
            using(var diss = disposable) return f(diss);
        }

        public static NoneType None => default;
        
        public static Option<T> Some<T>(T value) => new Option.Some<T>(value);
    }
}
