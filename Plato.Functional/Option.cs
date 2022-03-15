using System;
using static Plato.Functional.F;
namespace Option
{
    public struct Option<T> 
    {
        readonly bool isSome;
        readonly T value;
        internal Option(T value)
        {
            if(value == null)
                throw new ArgumentNullException();
            this.isSome = true;
            this.value = value;
        }
        public static implicit operator Option<T>(NoneType _) => new Option<T>();
        public static implicit operator Option<T>(Some<T> val) => new Option<T>(val.Value);
        public static implicit operator Option<T>(T val) => val is null? None : Some(val);
        public R Match<R>(Func<R> None, Func<T,R> Some) => isSome ? Some(value) : None();
    }
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
}