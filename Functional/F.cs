using System;
namespace Functional
{
    public static class F
    {
      public static R Using<T,R>(T disposable, Func<T, R> f) where T : IDisposable
      {
        using(dis = disposable) return f(dis);
      }        
    }
}