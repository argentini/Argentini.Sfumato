using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class FiltersCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllFiltersClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddBlurGroupAsync());
        tasks.Add(collection.AddBrightnessGroupAsync());
        tasks.Add(collection.AddContrastGroupAsync());
        tasks.Add(collection.AddDropShadowGroupAsync());
        tasks.Add(collection.AddGrayscaleGroupAsync());
        tasks.Add(collection.AddHueRotateGroupAsync());
        tasks.Add(collection.AddInvertGroupAsync());
        tasks.Add(collection.AddSaturateGroupAsync());
        tasks.Add(collection.AddSepiaGroupAsync());
        tasks.Add(collection.AddBackdropBlurGroupAsync());
        tasks.Add(collection.AddBackdropBrightnessGroupAsync());
        tasks.Add(collection.AddBackdropContrastGroupAsync());
        tasks.Add(collection.AddBackdropGrayscaleGroupAsync());
        tasks.Add(collection.AddBackdropHueRotateGroupAsync());
        tasks.Add(collection.AddBackdropInvertGroupAsync());
        tasks.Add(collection.AddBackdropSaturateGroupAsync());
        tasks.Add(collection.AddBackdropSepiaGroupAsync());
        tasks.Add(collection.AddBackdropOpacityGroupAsync());
    }
    
    public static async Task AddBlurGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "blur"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "filter: blur({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBrightnessGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "brightness"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "filter: brightness({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesOptions(0, 200),
            "filter: brightness({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddContrastGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "contrast"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "filter: contrast({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesOptions(0, 200),
            "filter: contrast({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddDropShadowGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "drop-shadow"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "filter: drop-shadow({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddGrayscaleGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grayscale"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "filter: grayscale({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: grayscale({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddHueRotateGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "hue-rotate"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("angle", "filter: hue-rotate({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddInvertGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "invert"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "filter: invert({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: invert({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddSaturateGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "saturate"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "filter: saturate({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesOptions(0, 200),
            "filter: saturate({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddSepiaGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "sepia"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "filter: sepia({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "filter: sepia({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropBlurGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-blur"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length", "backdrop-filter: blur({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropBrightnessGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-brightness"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "backdrop-filter: brightness({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesOptions(0, 200),
            "backdrop-filter: brightness({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropContrastGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-contrast"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "backdrop-filter: contrast({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesOptions(0, 200),
            "backdrop-filter: contrast({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropGrayscaleGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-grayscale"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "backdrop-filter: grayscale({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: grayscale({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropHueRotateGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-hue-rotate"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("angle", "backdrop-filter: hue-rotate({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropInvertGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-invert"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "backdrop-filter: invert({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: invert({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropSaturateGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-saturate"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "backdrop-filter: saturate({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesOptions(0, 200),
            "backdrop-filter: saturate({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropSepiaGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-sepia"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("percentage", "backdrop-filter: sepia({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "100%",
                ["0"] = "0"
            },
            "backdrop-filter: sepia({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
    
    public static async Task AddBackdropOpacityGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "backdrop-opacity"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "backdrop-filter: opacity({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddOneBasedPercentagesOptions(0, 100),
            "backdrop-filter: opacity({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
}