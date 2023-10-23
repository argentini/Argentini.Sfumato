namespace Argentini.Sfumato.ScssUtilityCollections.Entities;

public static class CollectionBase
{
    /// <summary>
    /// Return matching core utility classes.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="cssSelector"></param>
    /// <returns></returns>
    public static async Task<List<ScssUtilityClass>> GetMatchingClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection, CssSelector cssSelector)
    {
        var result = new List<ScssUtilityClass>();
        var groups = collection.Where(g => cssSelector.RootClass.StartsWith(g.Key, StringComparison.Ordinal)).ToList();

        if (string.IsNullOrEmpty(cssSelector.CustomValueSegment))
            foreach (var (_, group) in groups)
                result.AddRange(group.Classes.Where(c => c.Selector == cssSelector.RootClass));
        
        else if (string.IsNullOrEmpty(cssSelector.CustomValueType) == false)
            foreach (var (_, group) in groups)
                result.AddRange(group.Classes.Where(c => c.Selector == cssSelector.RootClass && c.ArbitraryValueTypes.Contains(cssSelector.CustomValueType)));

        else if (string.IsNullOrEmpty(cssSelector.CustomValueType))
            foreach (var (_, group) in groups)
                result.AddRange(group.Classes.Where(c => c.Selector == cssSelector.RootClass && c.ArbitraryValueTypes.Contains("raw")));
        
        return await Task.FromResult(result);
    }
    
    /// <summary>
    /// Return numbered rem size options (e.g. basis-0.5, basis-1.5, etc.).
    /// </summary>
    public static async Task<Dictionary<string,string>> AddNumberedRemUnitsClassesAsync(decimal minValue, decimal maxValue, decimal step = 0.5m)
    {
        var result = new Dictionary<string, string>();

        for (var x = minValue; x <= maxValue; x += step)
        {
            if (x == 4)
                step = 1;
            
            result.Add($"{x:0.#}", $"{x / 4:0.###}rem");
        }

        return await Task.FromResult(result);
    }

}