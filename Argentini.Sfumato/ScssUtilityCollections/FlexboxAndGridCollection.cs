using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class FlexboxAndGridCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllFlexboxAndGridClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddFlexBasisGroupAsync());
        tasks.Add(collection.AddFlexGroupAsync());
        tasks.Add(collection.AddGridGroupAsync());
        tasks.Add(collection.AddJustifyGroupAsync());
        tasks.Add(collection.AddAlignGroupAsync());
        tasks.Add(collection.AddPlaceGroupAsync());
    }
    
    public static async Task AddFlexBasisGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "basis"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "flex-basis: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "flex-basis: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
    }

    public static async Task AddFlexGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Flex
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "flex"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "flex: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["row"] = "row",
                ["row-reverse"] = "row-reverse",
                ["col"] = "column",
                ["col-reverse"] = "column-reverse"
            },
            "flex-direction: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["wrap"] = "wrap",
                ["wrap-reverse"] = "wrap-reverse",
                ["nowrap"] = "nowrap"
            },
            "flex-wrap: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["1"] = "1 1 0%",
                ["auto"] = "1 1 auto",
                ["initial"] = "0 1 auto",
                ["none"] = "none"
            },
            "flex: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["grow"] = "1",
                ["grow-0"] = "0"
            },
            "flex-grow: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["shrink"] = "1",
                ["shrink-0"] = "0"
            },
            "flex-shrink: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grow
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grow"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("integer,number", "flex-grow: {value};");

        #endregion

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Shrink
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "shrink"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("integer,number", "flex-shrink: {value};");

        #endregion
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Order
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "order"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("integer", "order: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["first"] = int.MinValue.ToString(),
                ["last"] = int.MaxValue.ToString(),
                ["none"] = "0"
            },
            "order: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24),
            "order: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Order (Negative)
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "-order"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("integer", "order: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, true),
            "order: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);

        #endregion
    }

    public static async Task AddGridGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Grid Template Columns
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-cols"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "grid-template-columns: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "grid-template-columns: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "repeat({value}, minmax(0, 1fr))"),
            "grid-template-columns: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
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
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-span"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "grid-column: {value}");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["full"] = "1 / -1"
            },
            "grid-column: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "span {value} / span {value}"),
            "grid-column: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grid Column Start
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-start"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("integer", "grid-column-start: {value}");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-column-start: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-column-start: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grid Column End
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-end"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("integer", "grid-column-end: {value}");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-column-end: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-column-end: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grid Template Rows
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-rows"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "grid-template-rows: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "grid-template-rows: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "repeat({value}, minmax(0, 1fr))"),
            "grid-template-rows: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
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
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-span"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "grid-row: {value}");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["full"] = "1 / -1"
            },
            "grid-row: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "span {value} / span {value}"),
            "grid-row: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grid Row Start
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-start"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("integer", "grid-row-start: {value}");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-row-start: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-start: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grid Row End
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-end"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("integer", "grid-row-end: {value}");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-row-end: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-end: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grid Auto Flow
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-flow"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-end: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grid Auto Columns
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "auto-cols"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "grid-auto-columns: {value}");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fr"] = "minmax(0, 1fr)"
            },
            "grid-auto-columns: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Grid Auto Rows
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "auto-rows"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "grid-auto-rows: {value}");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fr"] = "minmax(0, 1fr)"
            },
            "grid-auto-rows: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Gap
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "gap: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "gap: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "gap: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Gap X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap-x"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "column-gap: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "column-gap: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "column-gap: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Gap Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap-y"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "row-gap: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "row-gap: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "row-gap: {value};"
        );
        
        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }

    public static async Task AddJustifyGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Justify Content
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Justify Items
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify-items"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["stretch"] = "stretch"
            },
            "justify-items: {value};"
        );

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Justify Self
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify-self"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }
    
    public static async Task AddAlignGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Align Content
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "content"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Align Items
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "items"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Align Self
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "self"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }

    public static async Task AddPlaceGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Place Content
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-content"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Place Items
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-items"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
        
        #region Place Self
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-self"
        };
    
        await scssUtilityClass.AddClassesAsync(
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

        collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass);
        
        #endregion
    }
}