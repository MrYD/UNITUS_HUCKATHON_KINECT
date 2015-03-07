using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public static class UtilityExtension
    {
        public static void Each<T>(this IEnumerable<T> obj,Action<T> act)
        {
            foreach (var value in obj)
            {
                act(value);
            }
        }
    }
}
