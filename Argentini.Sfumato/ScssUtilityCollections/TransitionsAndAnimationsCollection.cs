using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class TransitionsAndAnimationsCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllTransitionsAndAnimationsClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var sortSeed = 1300000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddTransformsGroupAsync(sortSeed);
        
        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddTransformsGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Scale
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scale",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "transform: scale({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "transform: scale({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Scale X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scale-x",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "transform: scaleX({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "transform: scaleX({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scale Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scale-y",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("number", "transform: scaleY({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "transform: scaleY({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Rotate
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "rotate",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("angle", "transform: rotate({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "transform: rotate({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Translate X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "translate-x",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "transform: translateX({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "transform: translateX({value});",
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "transform: translateX({value});",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "transform: translateX({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Translate Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "translate-y",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "transform: translateY({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "transform: translateY({value});",
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "transform: translateY({value});",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "transform: translateY({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Skew X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "skew-x",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("angle", "transform: skewX({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "transform: skewX({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Skew Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "skew-y",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("angle", "transform: skewY({value});", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "transform: skewY({value});",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Origin
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "origin",
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "transform-origin: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "transform-origin: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
}