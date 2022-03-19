using System;
using System.Collections.Generic;
using System.Linq;
using Option;
using Plato.Functional;

namespace Plato.Playground
{
    internal static class Playground
    {
        public static void _main()
        {

        } 
        static Option<WorkPermit> GetWorkPermit(Dictionary<string, Employee> employees, string employeeId)
         => employees.LookUp(employeeId).Bind(emp => emp.WorkPermit);
        public static double AverageYearsWorkedAtTheCompany(List<Employee> employees)
        => employees
           .Bind(emp => emp.LeftOn.Map(leftOn => YearsBetween(emp.JoinedOn, leftOn)))
           .Average();
        static double YearsBetween(DateTime start, DateTime end)
         => (end - start).Days / 365d;
    }
   internal record WorkPermit
   (
      string Number,
      DateTime Expiry
   );
   internal record Employee
   (
      string Id,
      Option<WorkPermit> WorkPermit,
      DateTime JoinedOn,
      Option<DateTime> LeftOn
   );








}