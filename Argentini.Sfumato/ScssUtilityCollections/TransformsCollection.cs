using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class TransformsCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllTransformsClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var sortSeed = 1200000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        await internalCollection.AddTransitionsGroupAsync();
        await internalCollection.AddAnimationGroupAsync();
        
        foreach (var group in internalCollection)
        {
            foreach (var item in group.Value.Classes)
                item.SortOrder = sortSeed++;
            
            collection.TryAdd(group.Key, group.Value);
        }
    }
    
    public static async Task AddTransitionsGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Transition
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "transition",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "transition: {value};");

        #endregion

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "color, background-color, border-color, text-decoration-color, fill, stroke, opacity, box-shadow, transform, filter, backdrop-filter",
                ["none"] = "none",
                ["all"] = "all",
                ["colors"] = "color, background-color, border-color, text-decoration-color, fill, stroke",
                ["opacity"] = "opacity",
                ["shadow"] = "box-shadow",
                ["transform"] = "transform"
            },
            """
            transition-property: {value};
            transition-timing-function: cubic-bezier(0.4, 0, 0.2, 1);
            transition-duration: 150ms;
            """
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
        
        #region Duration
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "duration",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("time", "transition-duration: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0s",
                ["75"] = "75ms",
                ["100"] = "100ms",
                ["150"] = "150ms",
                ["200"] = "200ms",
                ["300"] = "300ms",
                ["500"] = "500ms",
                ["700"] = "700ms",
                ["1000"] = "1000ms"
            },
            "transition-duration: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Timing
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ease",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "transition-timing-function: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["linear"] = "linear",
                ["in"] = "cubic-bezier(0.4, 0, 1, 1)",
                ["out"] = "cubic-bezier(0, 0, 0.2, 1)",
                ["in-out"] = "cubic-bezier(0.4, 0, 0.2, 1)",
            },
            "transition-timing-function: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Delay
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "delay",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("time", "transition-delay: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0s",
                ["75"] = "75ms",
                ["100"] = "100ms",
                ["150"] = "150ms",
                ["200"] = "200ms",
                ["300"] = "300ms",
                ["500"] = "500ms",
                ["700"] = "700ms",
                ["1000"] = "1000ms"
            },
            "transition-delay: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
    }
    
    public static async Task AddAnimationGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "animate",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "animation: {value};");

        #endregion

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none",
            },
            "animation: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["spin"] = "",
            },
            """
            animation: spin 1s linear infinite;

            @keyframes spin {
               from {
                   transform: rotate(0deg);
                }
                to {
                   transform: rotate(360deg);
                }
            }
            """
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["ping"] = "",
            },
            """
            animation: ping 1s cubic-bezier(0, 0, 0.2, 1) infinite;
            
             @keyframes ping {
                 75%, 100% {
                     transform: scale(2);
                     opacity: 0;
                 }
             }
            """
        );

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["pulse"] = "",
            },
            """
            animation: pulse 2s cubic-bezier(0.4, 0, 0.6, 1) infinite;
            
             @keyframes pulse {
                 0%, 100% {
                     opacity: 1;
                 }
                 50% {
                     opacity: .5;
                 }
             }
            """
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["bounce"] = "",
            },
            """
            animation: bounce 1s infinite;
            
             @keyframes bounce {
                 0%, 100% {
                     transform: translateY(-25%);
                     animation-timing-function: cubic-bezier(0.8, 0, 1, 1);
                 }
                 50% {
                     transform: translateY(0);
                     animation-timing-function: cubic-bezier(0, 0, 0.2, 1);
                 }
             }
            """
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }
}