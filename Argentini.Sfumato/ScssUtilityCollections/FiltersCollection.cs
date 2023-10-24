using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class FiltersCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllFiltersClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var sortSeed = 400000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        await internalCollection.AddBlurGroupAsync();
        await internalCollection.AddBrightnessGroupAsync();
        await internalCollection.AddContrastGroupAsync();
        await internalCollection.AddDropShadowGroupAsync();
        await internalCollection.AddGrayscaleGroupAsync();
        await internalCollection.AddHueRotateGroupAsync();
        await internalCollection.AddInvertGroupAsync();
        await internalCollection.AddSaturateGroupAsync();
        await internalCollection.AddSepiaGroupAsync();
        await internalCollection.AddBackdropBlurGroupAsync();
        await internalCollection.AddBackdropBrightnessGroupAsync();
        await internalCollection.AddBackdropContrastGroupAsync();
        await internalCollection.AddBackdropGrayscaleGroupAsync();
        await internalCollection.AddBackdropHueRotateGroupAsync();
        await internalCollection.AddBackdropInvertGroupAsync();
        await internalCollection.AddBackdropSaturateGroupAsync();
        await internalCollection.AddBackdropSepiaGroupAsync();
        await internalCollection.AddBackdropOpacityGroupAsync();
        
        foreach (var group in internalCollection)
        {
            foreach (var item in group.Value.Classes)
                item.SortOrder = sortSeed++;
            
            collection.TryAdd(group.Key, group.Value);
        }
    }
    
    public static async Task AddBlurGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "blur"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "filter: blur({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = 8.PxToRem(),
                ["none"] = "0",
                ["sm"] = 4.PxToRem(),
                ["md"] = 12.PxToRem(),
                ["lg"] = 16.PxToRem(),
                ["xl"] = 24.PxToRem(),
                ["2xl"] = 40.PxToRem(),
                ["3xl"] = 64.PxToRem(),
            },
            "filter: blur({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBrightnessGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "brightness"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "filter: brightness({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "filter: brightness({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddContrastGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "contrast"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "filter: contrast({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "filter: contrast({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddDropShadowGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "drop-shadow"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "filter: drop-shadow({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = $"drop-shadow(0 1px {2.PxToRem()} rgb(0 0 0 / 0.1)) drop-shadow(0 1px 1px rgb(0 0 0 / 0.06))",
                ["sm"] = "drop-shadow(0 1px 1px rgb(0 0 0 / 0.05))",
                ["md"] = $"drop-shadow(0 {4.PxToRem()} {3.PxToRem()} rgb(0 0 0 / 0.07)) drop-shadow(0 {2.PxToRem()} {2.PxToRem()} rgb(0 0 0 / 0.06))",
                ["lg"] = $"drop-shadow(0 {10.PxToRem()} {8.PxToRem()} rgb(0 0 0 / 0.04)) drop-shadow(0 {4.PxToRem()} {3.PxToRem()} rgb(0 0 0 / 0.1))",
                ["xl"] = $"drop-shadow(0 {20.PxToRem()} {13.PxToRem()} rgb(0 0 0 / 0.03)) drop-shadow(0 {8.PxToRem()} {5.PxToRem()} rgb(0 0 0 / 0.08))",
                ["2xl"] = $"drop-shadow(0 {25.PxToRem()} {25.PxToRem()} rgb(0 0 0 / 0.15))",
                ["none"] = "drop-shadow(0 0 #0000)"
            },
            "filter: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddGrayscaleGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grayscale"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "filter: grayscale({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: grayscale({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddHueRotateGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "hue-rotate"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("angle", "filter: hue-rotate({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0deg",
                ["15"] = "15deg",
                ["30"] = "30deg",
                ["60"] = "60deg",
                ["90"] = "90deg",
                ["180"] = "180deg"
            },
            "filter: hue-rotate({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddInvertGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "invert"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "filter: invert({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: invert({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddSaturateGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "saturate"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "filter: saturate({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "filter: saturate({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddSepiaGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "sepia"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "filter: sepia({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: sepia({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropBlurGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-blur"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "backdrop-filter: blur({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = 8.PxToRem(),
                ["none"] = "0",
                ["sm"] = 4.PxToRem(),
                ["md"] = 12.PxToRem(),
                ["lg"] = 16.PxToRem(),
                ["xl"] = 24.PxToRem(),
                ["2xl"] = 40.PxToRem(),
                ["3xl"] = 64.PxToRem(),
            },
            "backdrop-filter: blur({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropBrightnessGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-brightness"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "backdrop-filter: brightness({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "backdrop-filter: brightness({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropContrastGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-contrast"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "backdrop-filter: contrast({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "backdrop-filter: contrast({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropGrayscaleGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-grayscale"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "backdrop-filter: grayscale({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: grayscale({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropHueRotateGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-hue-rotate"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("angle", "backdrop-filter: hue-rotate({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0deg",
                ["15"] = "15deg",
                ["30"] = "30deg",
                ["60"] = "60deg",
                ["90"] = "90deg",
                ["180"] = "180deg"
            },
            "backdrop-filter: hue-rotate({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropInvertGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-invert"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "backdrop-filter: invert({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: invert({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropSaturateGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-saturate"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "backdrop-filter: saturate({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "backdrop-filter: saturate({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropSepiaGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-sepia"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "backdrop-filter: sepia({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: sepia({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
    
    public static async Task AddBackdropOpacityGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-opacity"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "backdrop-filter: opacity({value});");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 100),
            "backdrop-filter: opacity({value});"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
}