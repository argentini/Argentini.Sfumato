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

        sortSeed = await internalCollection.AddBlurGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBrightnessGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddContrastGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddDropShadowGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddGrayscaleGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddHueRotateGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddInvertGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddSaturateGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddSepiaGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropBlurGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropBrightnessGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropContrastGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropGrayscaleGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropHueRotateGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropInvertGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropSaturateGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropSepiaGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBackdropOpacityGroupAsync(sortSeed);
        
        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddBlurGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "blur"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "filter: blur({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "filter: blur({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBrightnessGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "brightness"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "filter: brightness({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "filter: brightness({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddContrastGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "contrast"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "filter: contrast({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "filter: contrast({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddDropShadowGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "drop-shadow"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "filter: drop-shadow({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "filter: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddGrayscaleGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grayscale"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "filter: grayscale({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: grayscale({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddHueRotateGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "hue-rotate"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("angle", "filter: hue-rotate({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0deg",
                ["15"] = "15deg",
                ["30"] = "30deg",
                ["60"] = "60deg",
                ["90"] = "90deg",
                ["180"] = "180deg"
            },
            "filter: hue-rotate({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddInvertGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "invert"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "filter: invert({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: invert({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddSaturateGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "saturate"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "filter: saturate({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "filter: saturate({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddSepiaGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "sepia"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "filter: sepia({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: sepia({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropBlurGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-blur"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length", "backdrop-filter: blur({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "backdrop-filter: blur({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropBrightnessGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-brightness"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "backdrop-filter: brightness({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "backdrop-filter: brightness({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropContrastGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-contrast"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "backdrop-filter: contrast({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "backdrop-filter: contrast({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropGrayscaleGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-grayscale"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "backdrop-filter: grayscale({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: grayscale({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropHueRotateGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-hue-rotate"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("angle", "backdrop-filter: hue-rotate({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0deg",
                ["15"] = "15deg",
                ["30"] = "30deg",
                ["60"] = "60deg",
                ["90"] = "90deg",
                ["180"] = "180deg"
            },
            "backdrop-filter: hue-rotate({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropInvertGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-invert"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "backdrop-filter: invert({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: invert({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropSaturateGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-saturate"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "backdrop-filter: saturate({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 200),
            "backdrop-filter: saturate({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropSepiaGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-sepia"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("percentage", "backdrop-filter: sepia({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: sepia({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddBackdropOpacityGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-opacity"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "backdrop-filter: opacity({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesClassesAsync(0, 100),
            "backdrop-filter: opacity({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
}