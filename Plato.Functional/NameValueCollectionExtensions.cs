using System.Collections.Specialized;
using Option;

namespace Plato.Functional
{
    public static class NameValueCollectionExtensions
    {
        public static Option<string> LookUp(this NameValueCollection collection, string key) => collection[key];
        
    }
}