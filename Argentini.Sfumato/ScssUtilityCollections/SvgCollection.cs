using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class SvgCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllSvgClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddSvgGroupAsync());
    }
    
    public static async Task AddSvgGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Fill
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "fill"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("color", "fill: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "fill: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            "fill: {value};"
        );

        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Stroke
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "stroke"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("color", "stroke: {value};");
        await scssUtilityClass.AddAbitraryValueClassAsync("length,integer,number,percentage", "stroke-width: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "stroke: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["1"] = "1",
                ["2"] = "2"
            },
            "stroke-width: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            "stroke: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
    }
}