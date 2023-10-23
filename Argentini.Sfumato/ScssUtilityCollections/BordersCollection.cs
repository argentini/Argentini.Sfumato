using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class BordersCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllBordersClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddRoundedGroupAsync());
    }
    
    public static async Task AddRoundedGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "rounded"
        };
        
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "border-radius: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.RoundedOptions,
            "border-radius: {value};"
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "s",
            """
                border-start-start-radius: {value};
                border-end-start-radius: {value};
                """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "e",
            """
            border-start-end-radius: {value};
            border-end-end-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "t",
            """
            border-top-left-radius: {value};
            border-top-right-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "r",
            """
            border-top-right-radius: {value};
            border-bottom-right-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "b",
            """
            border-bottom-right-radius: {value};
            border-bottom-left-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "l",
            """
            border-top-left-radius: {value};
            border-bottom-left-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "ss",
            """
            border-start-start-radius: {value};
            """
        );

        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "se",
            """
            border-start-end-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "ee",
            """
            border-end-end-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "es",
            """
            border-end-start-radius: {value};
            """
        );

        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "tl",
            """
            border-top-left-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "tr",
            """
            border-top-right-radius: {value};
            """
        );

        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "br",
            """
            border-bottom-right-radius: {value};
            """
        );
        
        await scssUtilityClass.AddClassesWithPrefixAsync(
            SfumatoScss.RoundedOptions,
            "bl",
            """
            border-bottom-left-radius: {value};
            """
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }
}