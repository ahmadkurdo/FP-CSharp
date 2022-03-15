using System;
using System.Collections.Generic;
using System.Linq;

namespace Chapter2.Examples
{
    public class MutatingArguments
    {
      decimal RecomputeTotal_MutatingArguments(Order order, List<OrderLine> linesToDelete)
      {
         var result = 0m;
         foreach (var line in order.OrderLines)
            if (line.Quantity == 0) linesToDelete.Add(line);
            else result += line.Product.Price * line.Quantity;
         return result;
      }

      (decimal, List<OrderLine>) RecomputeTotal_MutatingArguments(Order order) => 
      (order.OrderLines.Sum(ol => ol.Product.Price * ol.Quantity), 
       order.OrderLines.Where(ol => ol.Quantity == 0).ToList());
    }
      class Product
      {
         public decimal Price { get; }
      }

      class OrderLine
      {
         public Product Product { get; }
         public int Quantity { get; }
      }

      class Order
      {
         public IEnumerable<OrderLine> OrderLines { get; } 
      }
}
