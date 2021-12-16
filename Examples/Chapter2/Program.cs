using System;
using System.Collections.Generic;
using Chapter2.ListFormatter;
using static System.Console;

namespace Chapter2
{
    class Program
    {
        static void Main(string[] args)
        {
         var shoppingList = new List<string> { "coffee beans", "BANANAS", "Dates" };

         new ListFormatter_Instance()
            .Format(shoppingList)
            .ForEach(WriteLine);

         Read();        
         }
    }
}
