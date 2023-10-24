using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class TablesCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllTableClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var sortSeed = 1100000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        await internalCollection.AddTableGroupAsync();
        
        foreach (var group in internalCollection)
        {
            foreach (var item in group.Value.Classes)
                item.SortOrder = sortSeed++;
            
            collection.TryAdd(group.Key, group.Value);
        }
    }
    
    public static async Task AddTableGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
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
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "border-spacing",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "border-spacing: {value} {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "border-spacing: {value} {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "border-spacing: {value} {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Border Spacing X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "border-spacing-x",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "border-spacing: {value} var(--sf-border-spacing-y);");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "border-spacing: {value} var(--sf-border-spacing-y);"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "border-spacing: {value} var(--sf-border-spacing-y);"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Border Spacing Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "border-spacing-y",
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "border-spacing: var(--sf-border-spacing-x) {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px"
            },
            "border-spacing: var(--sf-border-spacing-x) {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "border-spacing: var(--sf-border-spacing-x) {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Table Layout
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "table"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["fixed"] = "fixed"
            },
            "table-layout: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Caption Side
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "caption"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["top"] = "top",
                ["bottom"] = "bottom"
            },
            "caption-side: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
    }
}