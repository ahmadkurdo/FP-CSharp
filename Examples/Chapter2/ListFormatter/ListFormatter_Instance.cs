using System.Collections.Generic;
using System.Linq;


namespace Chapter2.ListFormatter
{
    public class ListFormatter_Instance
    {
      int counter;

      string PrependCounter(string s) => $"{++counter}. {s}";
      
      public List<string> Format(List<string> list)
         => list
            .Select(StringExt.ToSentenceCase)
            .Select(PrependCounter)
            .ToList();
    }
}