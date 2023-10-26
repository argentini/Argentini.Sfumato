using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class SizingCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllSizingClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var sortSeed = 800000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddWidthGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddHeightGroupAsync(sortSeed);
        
        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddWidthGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Width
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "w"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "width: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto",
                ["screen"] = "100vw",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fit"] = "fit-content"
            },
            "width: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "width: {value};",
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "width: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Min Width
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "min-w"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "min-width: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["full"] = "100%",
                ["screen"] = "100vw",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fit"] = "fit-content"
            },
            "min-width: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Max Width
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "max-w"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "max-width: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["none"] = "none",
                ["xs"] = "20rem",
                ["sm"] = "24rem",
                ["md"] = "28rem",
                ["lg"] = "32rem",
                ["xl"] = "36rem",
                ["2xl"] = "42rem",
                ["3xl"] = "48rem",
                ["4xl"] = "56rem",
                ["5xl"] = "64rem",
                ["6xl"] = "72rem",
                ["7xl"] = "80rem",
                ["full"] = "100%",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fit"] = "fit-content",
                ["prose"] = "65ch",
                ["screen-zero"] = "calc(#{$phab-breakpoint} - 1px)",
                ["screen-phab"] = "#{$tabp-breakpoint}",
                ["screen-tabp"] = "#{$tabl-breakpoint}",
                ["screen-tabl"] = "#{$note-breakpoint}",
                ["screen-note"] = "#{$desk-breakpoint}",
                ["screen-desk"] = "#{$elas-breakpoint}",
                ["screen-elas"] = "#{$tabp-breakpoint}"
            },
            "max-width: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddHeightGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Height
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "h"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "height: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto",
                ["screen"] = "100vh",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fit"] = "fit-content"
            },
            "height: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "height: {value};",
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "height: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Min Height
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "min-h"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "min-height: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["full"] = "100%",
                ["screen"] = "100vh",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fit"] = "fit-content"
            },
            "min-height: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Max Height
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "max-h"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "max-height: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["none"] = "none",
                ["full"] = "100%",
                ["screen"] = "100vh",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fit"] = "fit-content"
            },
            "max-height: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "max-height: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
}