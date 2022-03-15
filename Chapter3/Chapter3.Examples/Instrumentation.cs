using System;
using System.Diagnostics;
using Plato.Functional;

namespace Chapter3.Examples
{
    public static class Instrumentation
    {
        public static T Time<T>(string op, Func<T> f)
        {
            var sw = new Stopwatch();
            sw.Start();
            T t = f();
            sw.Stop();
            return t;
        }
        public static void Time(string op, Action act) => Time(op, act.ToFunc());
    }
}