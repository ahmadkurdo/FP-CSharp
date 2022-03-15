using Option;
using static Plato.Functional.F;

namespace Plato.Functional
{
    public static class Enum
    {
        public static Option<T> Parse<T>(this string s) where T : struct
         => System.Enum.TryParse(s, out T t) ? Some(t) : None ;
        
    }
}