using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class TransitionsAndAnimationsCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllTransitionsAndAnimationsClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        //tasks.Add(collection.Add());
    }
    
    public static async Task AddTransformsGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Scale
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scale",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "transform: scale({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["50"] = "0.5",
                ["75"] = "0.75",
                ["90"] = "0.90",
                ["95"] = "0.95",
                ["100"] = "1.0",
                ["105"] = "1.05",
                ["110"] = "1.1",
                ["125"] = "1.25",
                ["150"] = "1.5"
            },
            "transform: scale({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion

        #region Scale X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scale-x",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "transform: scaleX({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["50"] = "0.5",
                ["75"] = "0.75",
                ["90"] = "0.90",
                ["95"] = "0.95",
                ["100"] = "1.0",
                ["105"] = "1.05",
                ["110"] = "1.1",
                ["125"] = "1.25",
                ["150"] = "1.5"
            },
            "transform: scaleX({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Scale Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scale-y",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("number", "transform: scaleY({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["50"] = "0.5",
                ["75"] = "0.75",
                ["90"] = "0.90",
                ["95"] = "0.95",
                ["100"] = "1.0",
                ["105"] = "1.05",
                ["110"] = "1.1",
                ["125"] = "1.25",
                ["150"] = "1.5"
            },
            "transform: scaleY({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Rotate
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "rotate",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("angle", "transform: rotate({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0deg",
                ["1"] = "1deg",
                ["2"] = "2deg",
                ["3"] = "3deg",
                ["6"] = "6deg",
                ["12"] = "12deg",
                ["45"] = "45deg",
                ["90"] = "90deg",
                ["180"] = "180deg"
            },
            "transform: rotate({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Translate X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "translate-x",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "transform: translateX({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "transform: translateX({value});"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "transform: translateX({value});"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "transform: translateX({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Translate Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "translate-y",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "transform: translateY({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "transform: translateY({value});"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "transform: translateY({value});"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "transform: translateY({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Skew X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "skew-x",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("angle", "transform: skewX({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0deg",
                ["1"] = "1deg",
                ["2"] = "2deg",
                ["3"] = "3deg",
                ["6"] = "6deg",
                ["12"] = "12deg",
                ["45"] = "45deg",
                ["90"] = "90deg",
                ["180"] = "180deg"
            },
            "transform: skewX({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Skew Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "skew-y",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("angle", "transform: skewY({value});");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0deg",
                ["1"] = "1deg",
                ["2"] = "2deg",
                ["3"] = "3deg",
                ["6"] = "6deg",
                ["12"] = "12deg",
                ["45"] = "45deg",
                ["90"] = "90deg",
                ["180"] = "180deg"
            },
            "transform: skewY({value});"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Origin
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "origin",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "transform-origin: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["center"] = "center",
                ["top"] = "top",
                ["top-right"] = "top right",
                ["right"] = "right",
                ["bottom-right"] = "bottom right",
                ["bottom"] = "bottom",
                ["bottom-left"] = "bottom left",
                ["left"] = "left",
                ["top-left"] = "top left"
            },
            "transform-origin: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }
}