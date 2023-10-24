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
                result.AddRange(group.Classes.Where(c => c.Selector == cssSelector.RootSegment));
        
        else if (string.IsNullOrEmpty(cssSelector.CustomValueType) == false && string.IsNullOrEmpty(cssSelector.CustomValueType) == false)
            foreach (var (_, group) in groups)
                result.AddRange(group.Classes.Where(c => c.Selector == cssSelector.RootClass && c.ArbitraryValueTypes.Contains(cssSelector.CustomValueType)));

        else if (string.IsNullOrEmpty(cssSelector.CustomValueType))
            foreach (var (_, group) in groups)
                result.AddRange(group.Classes.Where(c => c.Selector == cssSelector.RootClass && c.ArbitraryValueTypes.Contains("raw")));
        
        return await Task.FromResult(result);
    }
    
    /// <summary>
    /// Return numbered rem size classes (e.g. basis-0.5, basis-1.5, etc.).
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

    /// <summary>
    /// Return classes for incremental whole numbers where that translate to fractional values from zero to one (e.g. ["opacity-5"] => "opacity: 0.05;").
    /// Used by inherited classes.
    /// </summary>
    public static async Task<Dictionary<string,string>> AddOneBasedPercentagesClassesAsync(decimal minValue, decimal maxValue, string valueTemplate = "")
    {
        var result = new Dictionary<string, string>();

        for (var x = minValue; x <= maxValue; x++)
        {
            var percentage = x / 100m;
            var value = $"{percentage}";
            
            if (string.IsNullOrEmpty(valueTemplate) == false)
                value = valueTemplate.Replace("{value}", value);
            
            result.Add($"{x}", value);
        }

        return await Task.FromResult(result);
    }

    /// <summary>
    /// Return classes for incremental whole numbers where the prefix and value arte the same (e.g. ["order-1"] => "order: 1;").
    /// Used by inherited classes.
    /// </summary>
    public static async Task<Dictionary<string,string>> AddNumberedClassesAsync(int minValue, int maxValue, bool isNegative = false, string valueTemplate = "")
    {
        var result = new Dictionary<string, string>();

        for (var x = minValue; x <= maxValue; x++)
        {
            var value = (isNegative ? x * -1 : x).ToString();
            
            if (string.IsNullOrEmpty(valueTemplate) == false)
                value = valueTemplate.Replace("{value}", value);
            
            result.Add(x.ToString(), value);
        }

        return await Task.FromResult(result);
    }
    
    /// <summary>
    /// Add options for fractions from 1/2 up through 11/12, and "full" =&gt; 100%.
    /// Used by inherited classes.
    /// </summary>
    public static async Task<Dictionary<string,string>> AddFractionsClassesAsync()
    {
        var result = new Dictionary<string, string>();

        foreach (var percentage in SfumatoScss.Fractions)
            result.TryAdd(percentage.Key, percentage.Value);
        
        return await Task.FromResult(result);
    }
}