using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class TransformsCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllTransformsClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddTransitionsGroupAsync());
        tasks.Add(collection.AddAnimationGroupAsync());
    }
    
    public static async Task AddTransitionsGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Transition
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "transition",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "transition: {value};");

        #endregion

        await scssUtilityClass.AddClassesAsync(
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
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);

        #endregion
        
        #region Duration
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "duration",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("time", "transition-duration: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Timing
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "ease",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "transition-timing-function: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["linear"] = "linear",
                ["in"] = "cubic-bezier(0.4, 0, 1, 1)",
                ["out"] = "cubic-bezier(0, 0, 0.2, 1)",
                ["in-out"] = "cubic-bezier(0.4, 0, 0.2, 1)",
            },
            "transition-timing-function: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Delay
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "delay",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("time", "transition-delay: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }
    
    public static async Task AddAnimationGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "animate",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "animation: {value};");

        #endregion

        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none",
            },
            "animation: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
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
        
        await scssUtilityClass.AddClassesAsync(
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

        await scssUtilityClass.AddClassesAsync(
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
        
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
}