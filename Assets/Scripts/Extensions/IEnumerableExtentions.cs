using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Extensions
{
    public static class IEnumerableExtentions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => enumerable == null || !enumerable.Any();
    }
}