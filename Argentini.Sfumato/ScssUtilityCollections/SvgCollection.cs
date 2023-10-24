using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class SvgCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllSvgClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var sortSeed = 1000000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        await internalCollection.AddSvgGroupAsync();
        
        foreach (var group in internalCollection)
        {
            foreach (var item in group.Value.Classes)
                item.SortOrder = sortSeed++;
            
            collection.TryAdd(group.Key, group.Value);
        }
    }
    
    public static async Task AddSvgGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Fill
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "fill"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "fill: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "fill: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "fill: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Stroke
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "stroke"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "stroke: {value};");
        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,integer,number,percentage", "stroke-width: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "stroke: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["1"] = "1",
                ["2"] = "2"
            },
            "stroke-width: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "stroke: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
    }
}