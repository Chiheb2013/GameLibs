using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mappy
{
    internal static class CollectionHelper<T>
    {
        public static bool DictionnaryContains(IDictionary<string, T> dict, string name)
        {
            return dict.Keys.Contains(name);
        }
    }
}
