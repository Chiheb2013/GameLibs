using System.Collections.Generic;

namespace Mappy
{
    internal static class CollectionHelper
    {
        public static bool DictionaryContains<T>(IDictionary<string, T> dict, string name)
        {
            return dict.Keys.Contains(name);
        }

        public static bool DictionaryContains<T1, T2>(IDictionary<T1, T2> dict, T1 element)
        {
            return dict.Keys.Contains(element);
        }
    }
}
