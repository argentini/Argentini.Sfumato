using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class EffectsCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllEffectsClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddShadowGroupAsync());
        tasks.Add(collection.AddOpacityGroupAsync());
        tasks.Add(collection.AddBlendModeGroupAsync());
    }
    
    public static async Task AddShadowGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "shadow"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "box-shadow: {value};");
        await scssUtilityClass.AddAbitraryValueClassAsync("color", "--sf-shadow-color: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
            "box-shadow: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            "--sf-shadow-color: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
    
    public static async Task AddOpacityGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "opacity"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "opacity: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 100),
            "opacity: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
    
    public static async Task AddBlendModeGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "mix-blend"
        };
    
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.BlendModeOptions,
            "mix-blend-mode: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "bg-blend"
        };
    
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.BlendModeOptions,
            "background-blend-mode: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
}