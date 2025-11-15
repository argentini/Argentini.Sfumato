namespace Sfumato.Helpers;

public static class Dictionaries
{
    public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
    {
        foreach (var kvp in other)
            dict.TryAdd(kvp.Key, kvp.Value);
    }
}