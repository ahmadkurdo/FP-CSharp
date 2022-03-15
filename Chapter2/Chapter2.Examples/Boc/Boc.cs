using System;
using System.Text.RegularExpressions;

namespace Chapter2.Examples.Boc
{
    public abstract class Command {}

    public sealed class MakeTransfer : Command 
    {
        public Guid DebitedAccountId { get; set; }
        public string Beneficiary { get; set; }
        public string Iban { get; set; }
        public string Bic { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
    public interface IValidator<T>
    {
        bool isValid(T t);
    }
    public sealed class BicFormatValidator : IValidator<MakeTransfer>
    {
        static readonly Regex regex = new Regex("^[A-Z]{6}[A-Z1-9]{5}$");
        public bool isValid(MakeTransfer t) => regex.IsMatch(t.Bic);
    }

    public sealed class DateNotPastValidator : IValidator<MakeTransfer>
    {
        public bool isValid(MakeTransfer t) => DateTime.UtcNow.Date <= t.Date.Date;
    }


    public struct Pair<T,U>
    {
        private (T,U) state; 
        public Pair(T t, U u)
        {
            this.state = (t, u);
        }
        public T Left => state.Item1;
        public U Right => state.Item2;
        public Pair<V,U> MapLeft<V>(Func<T,V> f) => new Pair<V, U>(f(state.Item1), state.Item2);
        public Pair<T,V> MapRight<V>(Func<U,V> f) => new Pair<T, V>(state.Item1, f(state.Item2));
        public Pair<U,T> Flip() => new Pair<U,T>(state.Item2,state.Item1);
        public R Combine<R>(Func<Pair<T,U>, R> f) => f(this);
    }

    public static class PairExtension
    {
        public static Pair<Pair<T,U>,U> Bind<T,U>(this Pair<T,U> x, Func<T, Pair<T,U>> f) => x.MapLeft(f);
        public static Pair<T,U> Join<T,U>(this Pair<Pair<T,U>,U> x, Func<Pair<T,U>,T> f) => x.MapLeft(f);
    }

    public  class Boc
    {
        public void _main()
        {
            var x = new Pair<string, int>("Ahmed", 25)
                    .MapLeft(x => new DateTime())
                    .MapRight(r => r.ToString())
                    .Flip()
                    .Bind(p => new Pair<string,DateTime>(p, DateTime.Now));
            
        }
    }
}