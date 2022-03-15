using System;

namespace Plato.Playground
{
    internal static class Playground
    {
        public static void _main()
        {

        }
        
    }
    internal struct NoneType {}
    internal record None<T> : Option<T> {}
    internal abstract record Option<T>
    {
        public static implicit operator Option<T>(NoneType _) => new None<T>();
        public static implicit operator Option<T>(T value) => value is null ? new None<T>() : new Some<T>(value);
    }
    internal record Some<T> : Option<T>
    {
    private T Value { get; }
    public Some(T value)
        => Value = value ?? throw new ArgumentNullException();
    
    public void Deconstruct(out T value)
        => value = Value;
    }
}