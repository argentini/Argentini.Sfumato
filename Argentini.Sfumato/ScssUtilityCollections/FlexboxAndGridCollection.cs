using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class FlexboxAndGridCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllFlexboxAndGridClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var sortSeed = 500000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        await internalCollection.AddFlexBasisGroupAsync();
        await internalCollection.AddFlexGroupAsync();
        await internalCollection.AddGridGroupAsync();
        await internalCollection.AddJustifyGroupAsync();
        await internalCollection.AddAlignGroupAsync();
        await internalCollection.AddPlaceGroupAsync();
        
        foreach (var group in internalCollection)
        {
            foreach (var item in group.Value.Classes)
                item.SortOrder = sortSeed++;
            
            collection.TryAdd(group.Key, group.Value);
        }
    }
    
    public static async Task AddFlexBasisGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "basis"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "flex-basis: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["px"] = "1px",
                ["auto"] = "auto",
                ["0.5"] = "0.125rem",
                ["1.5"] = "0.375rem",
                ["2.5"] = "0.625rem",
                ["3.5"] = "0.875rem",
                ["1/2"] = "50%",
                ["1/3"] = "33.333333%",
                ["2/3"] = "66.666667%",
                ["1/4"] = "25%",
                ["2/4"] = "50%",
                ["3/4"] = "75%",
                ["1/5"] = "20%",
                ["2/5"] = "40%",
                ["3/5"] = "60%",
                ["4/5"] = "80%",
                ["1/6"] = "16.666667%",
                ["2/6"] = "33.333333%",
                ["3/6"] = "50%",
                ["4/6"] = "66.666667%",
                ["5/6"] = "83.333333%",
                ["1/12"] = "8.333333%",
                ["2/12"] = "16.666667%",
                ["3/12"] = "25%",
                ["4/12"] = "33.333333%",
                ["5/12"] = "41.666667%",
                ["6/12"] = "50%",
                ["7/12"] = "58.333333%",
                ["8/12"] = "66.666667%",
                ["9/12"] = "75%",
                ["10/12"] = "83.333333%",
                ["11/12"] = "91.666667%",
                ["full"] = "100%"
            },
            "flex-basis: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "flex-basis: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }

    public static async Task AddFlexGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Flex
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "flex"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "flex: {value};");

        #endregion

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "",
            },
            "display: flex;"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["row"] = "row",
                ["row-reverse"] = "row-reverse",
                ["col"] = "column",
                ["col-reverse"] = "column-reverse"
            },
            "flex-direction: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["wrap"] = "wrap",
                ["wrap-reverse"] = "wrap-reverse",
                ["nowrap"] = "nowrap"
            },
            "flex-wrap: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["1"] = "1 1 0%",
                ["auto"] = "1 1 auto",
                ["initial"] = "0 1 auto",
                ["none"] = "none"
            },
            "flex: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["grow"] = "1",
                ["grow-0"] = "0"
            },
            "flex-grow: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["shrink"] = "1",
                ["shrink-0"] = "0"
            },
            "flex-shrink: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grow
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grow"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer,number", "flex-grow: {value};");

        #endregion

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Shrink
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "shrink"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer,number", "flex-shrink: {value};");

        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Order
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "order"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "order: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["first"] = int.MinValue.ToString(),
                ["last"] = int.MaxValue.ToString(),
                ["none"] = "0"
            },
            "order: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24),
            "order: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Order (Negative)
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "-order"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "order: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, true),
            "order: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
    }

    public static async Task AddGridGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Grid Template Columns
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-cols"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-template-columns: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "grid-template-columns: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "repeat({value}, minmax(0, 1fr))"),
            "grid-template-columns: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["col-auto"] = "auto"
            },
            "grid-column: {value};"
        );        
        
        #region Grid Column Span
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-span"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-column: {value}");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["full"] = "1 / -1"
            },
            "grid-column: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "span {value} / span {value}"),
            "grid-column: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Column Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-start"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "grid-column-start: {value}");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-column-start: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-column-start: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Column End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-end"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "grid-column-end: {value}");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-column-end: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-column-end: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Template Rows
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-rows"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-template-rows: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "grid-template-rows: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "repeat({value}, minmax(0, 1fr))"),
            "grid-template-rows: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["row-auto"] = "auto"
            },
            "grid-row: {value};"
        );        
        
        #region Grid Row Span
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-span"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-row: {value}");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["full"] = "1 / -1"
            },
            "grid-row: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "span {value} / span {value}"),
            "grid-row: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Row Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-start"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "grid-row-start: {value}");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-row-start: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-start: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Row End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-end"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "grid-row-end: {value}");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-row-end: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-end: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Auto Flow
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-flow"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["row"] = "row",
                ["col"] = "column",
                ["dense"] = "dense",
                ["row-dense"] = "row dense",
                ["col-dense"] = "column dense"
            },
            "grid-auto-flow: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-end: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Auto Columns
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "auto-cols"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-auto-columns: {value}");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fr"] = "minmax(0, 1fr)"
            },
            "grid-auto-columns: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Auto Rows
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "auto-rows"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-auto-rows: {value}");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fr"] = "minmax(0, 1fr)"
            },
            "grid-auto-rows: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Gap
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "gap: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "gap: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "gap: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Gap X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap-x"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "column-gap: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "column-gap: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "column-gap: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Gap Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap-y"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "row-gap: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "row-gap: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "row-gap: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
    }

    public static async Task AddJustifyGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Justify Content
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["normal"] = "normal",
                ["start"] = "flex-start",
                ["end"] = "flex-end",
                ["center"] = "center",
                ["between"] = "space-between",
                ["around"] = "space-around",
                ["evenly"] = "space-evenly",
                ["stretch"] = "stretch"
            },
            "justify-content: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Justify Items
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify-items"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["stretch"] = "stretch"
            },
            "justify-items: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Justify Self
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify-self"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["stretch"] = "stretch"
            },
            "justify-self: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
    }
    
    public static async Task AddAlignGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Align Content
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "content"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["normal"] = "normal",
                ["center"] = "center",
                ["start"] = "flex-start",
                ["end"] = "flex-end",
                ["between"] = "space-between",
                ["around"] = "space-around",
                ["evenly"] = "space-evenly",
                ["baseline"] = "baseline",
                ["stretch"] = "stretch"
            },
            "align-content: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Align Items
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "items"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "flex-start",
                ["end"] = "flex-end",
                ["center"] = "center",
                ["baseline"] = "baseline",
                ["stretch"] = "stretch"
            },
            "align-items: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Align Self
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "self"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["start"] = "flex-start",
                ["end"] = "flex-end",
                ["center"] = "center",
                ["baseline"] = "baseline",
                ["stretch"] = "stretch"
            },
            "align-self: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
    }

    public static async Task AddPlaceGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Place Content
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-content"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["center"] = "center",
                ["start"] = "start",
                ["end"] = "end",
                ["between"] = "space-between",
                ["around"] = "space-around",
                ["evenly"] = "space-evenly",
                ["baseline"] = "baseline",
                ["stretch"] = "stretch"
            },
            "place-content: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Place Items
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-items"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["baseline"] = "baseline",
                ["stretch"] = "stretch"
            },
            "place-items: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Place Self
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-self"
        };
    
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["stretch"] = "stretch"
            },
            "place-self: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
    }
}