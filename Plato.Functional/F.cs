using System;

namespace Plato.Functional
{
    public static partial class F
    {

        public static R Using<T,R>(T disposable, Func<T,R> f) where T : IDisposable
        {
            using(var diss = disposable) return f(diss);
        }
    }
}
