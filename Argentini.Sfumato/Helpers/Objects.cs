namespace Argentini.Sfumato.Helpers;

public static class Objects
{
    /// <summary>
    /// Get all types that inherit from a base type
    /// </summary>
    /// <param name="baseType"></param>
    /// <param name="excludeSystemAssemblies"></param>
    /// <returns></returns>
    public static IEnumerable<Type> GetInheritedTypes(this Type baseType, bool excludeSystemAssemblies = true)
    {
        var types = new List<Type>();
		
        foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies().Where(a => excludeSystemAssemblies == false || (excludeSystemAssemblies && (a.FullName?.StartsWith("System") ?? false) == false && (a.FullName?.StartsWith("Microsoft") ?? false) == false)))
        {
            var subtypes = assembly.GetTypes().Where(theType => theType is { IsClass: true, IsAbstract: false }).ToList();
            var ts = subtypes.Where(t => t != baseType && t.IsAssignableTo(baseType)).ToList();
            
            types.AddRange(ts);
        }
		
        return types;
    }

    #region Dictionaries
    
    /// <summary>
    /// Add or update a Dictionary with a given key/value pair.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="propertyTemplate"></param>
    /// <returns></returns>
    public static bool TryAddUpdate(this Dictionary<string,string> dictionary, string key, string value, string propertyTemplate = "")
    {
        if (dictionary.ContainsKey(key) == false)
            return dictionary.TryAdd(key, propertyTemplate == string.Empty ? value : propertyTemplate.Replace("{value}", value));
        
        dictionary[key] = propertyTemplate == string.Empty ? value : propertyTemplate.Replace("{value}", value);
        return true;
    }
    
    /// <summary>
    /// Add or update a Dictionary with a given key/value pair.
    /// </summary>
    /// <param name="dictionary"></param>
    /// <param name="kvp"></param>
    /// <param name="propertyTemplate"></param>
    /// <returns></returns>
    public static bool TryAddUpdate(this Dictionary<string,string> dictionary, KeyValuePair<string,string> kvp, string propertyTemplate = "")
    {
        if (dictionary.ContainsKey(kvp.Key) == false)
            return dictionary.TryAdd(kvp.Key, propertyTemplate == string.Empty ? kvp.Value : propertyTemplate.Replace("{value}", kvp.Value));
        
        dictionary[kvp.Key] = propertyTemplate == string.Empty ? kvp.Value : propertyTemplate.Replace("{value}", kvp.Value);
        return true;
    }

    public static void AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> other) where TKey : notnull
    {
        foreach (var kvp in other)
            dict.TryAdd(kvp.Key, kvp.Value);
    }
    
    /// <summary>
    /// Removes all entries for which the predicate returns true.
    /// </summary>
    public static void RemoveWhere<TKey, TValue>(this IDictionary<TKey, TValue>? dict, Func<TKey, TValue, bool>? predicate)
    {
        if (dict is null || predicate is null)
            return;

        var count = dict.Count;

        if (count == 0) 
            return;

        // Collect matching keys in one fast scan
        var keysToRemove = new TKey[count];
        var idx = 0;
        
        foreach (var kvp in dict)
        {
            if (predicate(kvp.Key, kvp.Value))
                keysToRemove[idx++] = kvp.Key;
        }

        // If nothing matched, bail out immediately
        if (idx == 0)
            return;

        // Remove exactly those entries
        for (var i = 0; i < idx; i++)
            dict.Remove(keysToRemove[i]);
    }
    
    #endregion
}