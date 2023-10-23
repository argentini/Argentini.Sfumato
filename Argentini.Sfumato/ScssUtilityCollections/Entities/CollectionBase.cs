namespace Argentini.Sfumato.ScssUtilityCollections.Entities;

public static class CollectionBase
{
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
}