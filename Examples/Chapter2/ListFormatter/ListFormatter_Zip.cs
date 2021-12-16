using System.Collections.Generic;
using System.Linq;
namespace Chapter2.ListFormatter
{
    public class ListFormatter_Zip
    {
        public List<string> Formar__ (List<string> list)
        => list.Select(StringExt.ToSentenceCase).Zip(Enumerable.Range(1,list.Count), (s,i) => $"{s} {i}").ToList();
        public List<string> Format(List<string> list)
        {
         var left = list.Select(StringExt.ToSentenceCase);
         var right = Enumerable.Range(1,list.Count);
         var zipped = Enumerable.Zip(left, right, (l,r) => $"{left} {right}").ToList();
         return zipped;
        }
    }
}