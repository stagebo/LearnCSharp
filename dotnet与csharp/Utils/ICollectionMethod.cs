using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseCSharp
{
    public static class ICollectionMethod
    {
        public static int[] StringListToIntList(this string[] str)
        {
            var result = str.Cast<string>().Select(x => {
                return int.Parse(x);
            }).ToList().ToArray(); 
            return result;
        }
    }
}
