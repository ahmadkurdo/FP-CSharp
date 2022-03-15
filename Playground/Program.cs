using System;
using Option;
using static Plato.Functional.F;
namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Enum.Parse("Monday"));
            
            
        }
    }
    public enum DayOfWeek
    {
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday,
        Saturday,
        Sunday
    }
    public static class Greeter 
    {
        public static string Greet(Option<string> greetee)
        {
            return greetee.Match(None: () => "Sorry who?", (name) => $"Hello {name}");
        }
    }
    public static class IntParser
    {
       public static Option<int> ParseInt(string val) => int.TryParse(val, out int result) ? Some(result) : None;
    }
    record Product(string Name, decimal Price,bool IsFood);
    record Adress(string Country);
    record UsAdress(string State) : Adress("us");
    record Order(Product Product, int Quantity)
    {
       public decimal NetPrice => Product.Price * Quantity; 
    }
    public static class VatCalculator
    {
        static decimal RateByCountry(string country)
        {
            return country switch
            {
                
                "it" => 0.22m,
                "jp" => 0.08m,
                _ => throw new ArgumentException($"No rate for {country}")
            };
        }

        static decimal RateByState(string state)
        {
            return state switch 
            {
                "ca" => 0.1m,
                "ma" => 0.0625m,
                "ny" => 0.085m,
                _ => throw new ArgumentException($"Missing rate for {state}")
            };
        }
        static decimal Vat(Adress Adress, Order Order) 
        {
            return Adress switch
            {
                {Country: "de"} => DeVat(Order),
                UsAdress(var state) => Vat(RateByState(state), Order),
                {Country: var c} => Vat(RateByCountry(c), Order),
            };
        }
        static decimal DeVat(Order order)=> order.NetPrice * (order.Product.IsFood ? 0.08m : 0.2m);
        static decimal Vat(decimal Rate, Order order) => order.NetPrice * Rate;
    }

}
