using System;
using System.Collections.Generic;
using Plato.Functional;
using static Plato.Functional.F;
using Unit = System.ValueTuple;
namespace Option
{
    public struct NoneType {}
    public struct Some<T>
    {
        internal T Value {get;}
        internal Some(T value)
        {
            if(value == null)
                throw new ArgumentNullException();
            Value = value;
        }
    }
    public struct Option<T> 
    {
        public readonly bool isSome;

        public readonly T value;
        
        internal Option(T value)
        {
            if(value == null)
                throw new ArgumentNullException();
            this.isSome = true;
            this.value = value;
        }
        public static implicit operator Option<T>(NoneType _) => new Option<T>();

        public static implicit operator Option<T>(Some<T> val) => new Option<T>(val.Value);

        public static implicit operator Option<T>(T val) => val is null ? None : Some(val);

        public R Match<R>(Func<R> None, Func<T,R> Some) => isSome ? Some(value) : None();

    }
    public static class OptionExtensions
    {
        public static Option<T> Where<T>(this Option<T> opt, Func<T,bool> f)
        => opt.Match(None: () => None, Some: (x) => f(x) ? opt : None);

        public static Option<Unit> ForEach<T>(this Option<T> opt, Action<T> action) => opt.Map(action.ToFunc());

        public static Option<R> Bind<T,R>(this Option<T> opt, Func<T,Option<R>> f) => opt.Match(None: () => None, Some: (x) => f(x));

        public static IEnumerable<R> Bind<T,R>(this Option<T> opt, Func<T, IEnumerable<R>> f) => opt.AsIEnumerable().Bind(f);

        public static Option<R> Map<T,R>(this Option<T> opt, Func<T,R> f) => opt.Bind(x => Some(f(x)));

        public static IEnumerable<T> AsIEnumerable<T>(this Option<T> opt)
        {
            if(opt.isSome) yield return opt.value;
        }
    }
}