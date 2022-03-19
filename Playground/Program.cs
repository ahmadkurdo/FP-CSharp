using System;
using System.Collections.Generic;
using Option;
using Plato.Functional;
using static Plato.Functional.F;
using Pet = System.String;

namespace Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            var neighbors = new Neighbor[]
            {
               new (Name: "John", Pets: new Pet[] {"Fluffy", "Thor"}),
               new (Name: "Tim",  Pets: new Pet[] {}),
               new (Name: "Carl", Pets: new Pet[] {"Sybil"}),
            };

         IEnumerable<IEnumerable<Pet>> nested = neighbors.Map(n => n.Pets);
         IEnumerable<Pet> flat = neighbors.Bind(n => n.Pets);
            //foreach(T t in enumrable) foreach(R r in f(t)) yield return r;


            IEnumerable<IEnumerable<Pet>> pets = neighbors.Map(n => n.Pets);
            IEnumerable<Pet>  c = neighbors.Bind(x => x.Pets);
        }
        record Neighbor(string Name, IEnumerable<Pet> Pets);

        public static string Prompt(string message) 
        {
            Console.WriteLine(message);
            return Console.ReadLine();
            
        }
        public static Option<Age> ReadAge(int retries) =>  
        retries <= 0 ? None :
        Age.ParseAge(Prompt("Please enter your age")).Match(None: () => ReadAge(--retries), Some: (age) => age);
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

    public struct Age
   {
        public static Func<string, Option<Age>> ParseAge = (val) => IntParser.ParseInt(val).Bind(val => Create(val));
        public int Value { get; }
        public static Option<Age> Create(int age) => IsValid(age) ? Some(new Age(age)) : None;      
        private Age(int value) => Value = value;
        private static bool IsValid(int age) => 0 <= age && age < 120;
        public static bool operator <(Age l, Age r) => l.Value < r.Value;
        public static bool operator >(Age l, Age r) => l.Value > r.Value;
        public static bool operator <(Age l, int r) => l < new Age(r);
        public static bool operator >(Age l, int r) => l > new Age(r); 
        public override string ToString() => Value.ToString();
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
