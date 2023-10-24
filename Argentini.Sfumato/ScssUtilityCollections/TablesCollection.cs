using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class TablesCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllTableClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddTableGroupAsync());
    }
    
    public static async Task AddTableGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Border Collapse

        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string, string>
            {
                ["border-collapse"] = "collapse",
                ["border-separate"] = "separate",
            },
            "border-collapse: {value};"
        );

        #endregion
        
        #region Border Spacing
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "border-spacing",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "border-spacing: {value} {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "border-spacing: {value} {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "border-spacing: {value} {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Border Spacing X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "border-spacing-x",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "border-spacing: {value} var(--sf-border-spacing-y);");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "border-spacing: {value} var(--sf-border-spacing-y);"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "border-spacing: {value} var(--sf-border-spacing-y);"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Border Spacing Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "border-spacing-y",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "border-spacing: var(--sf-border-spacing-x) {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "border-spacing: var(--sf-border-spacing-x) {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "border-spacing: var(--sf-border-spacing-x) {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Table Layout
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "table"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["fixed"] = "fixed"
            },
            "table-layout: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Caption Side
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "caption"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["top"] = "top",
                ["bottom"] = "bottom"
            },
            "caption-side: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
    }
}