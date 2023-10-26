using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class EffectsCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllEffectsClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var sortSeed = 300000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddShadowGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddOpacityGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBlendModeGroupAsync(sortSeed);

        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddShadowGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "shadow",
            Category = "shadow"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "box-shadow: {value};", sortSeed);
        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "--sf-shadow-color: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = $"0 1px {3.PxToRem()} 0 rgb(0 0 0 / 0.1), 0 1px {2.PxToRem()} -1px rgb(0 0 0 / 0.1)",
                ["xs"] = "0 0 0 1px rgb(0 0 0 / 0.05)",
                ["sm"] = $"0 1px {2.PxToRem()} 0 rgb(0 0 0 / 0.05)",
                ["md"] = $"0 {4.PxToRem()} {6.PxToRem()} -1px rgb(0 0 0 / 0.1), 0 {2.PxToRem()} {4.PxToRem()} -{2.PxToRem()} rgb(0 0 0 / 0.1)",
                ["lg"] = $"0 {10.PxToRem()} {15.PxToRem()} -{3.PxToRem()} rgb(0 0 0 / 0.1), 0 {4.PxToRem()} {6.PxToRem()} -{4.PxToRem()} rgb(0 0 0 / 0.1)",
                ["xl"] = $"0 {20.PxToRem()} {25.PxToRem()} -{5.PxToRem()} rgb(0 0 0 / 0.1), 0 {8.PxToRem()} {10.PxToRem()} -{6.PxToRem()} rgb(0 0 0 / 0.1)",
                ["2xl"] = $"0 {25.PxToRem()} {50.PxToRem()} -{12.PxToRem()} rgb(0 0 0 / 0.25)",
                ["inner"] = $"inset 0 {2.PxToRem()} {4.PxToRem()} 0 rgb(0 0 0 / 0.05)",
                ["none"] = "0 0 #0000"
            },
            "box-shadow: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "--sf-shadow-color: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddOpacityGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "opacity"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "opacity: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 100),
            "opacity: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBlendModeGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mix-blend"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.BlendModeOptions,
            "mix-blend-mode: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "bg-blend"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.BlendModeOptions,
            "background-blend-mode: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
}