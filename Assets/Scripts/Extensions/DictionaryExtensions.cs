using System.Collections.Generic;

namespace Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key,
            TValue fallback = default) => dictionary.ContainsKey(key) ? dictionary[key] : fallback;
    }
}