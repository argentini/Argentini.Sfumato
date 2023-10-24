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

        sortSeed = await internalCollection.AddFlexBasisGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddFlexGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddGridGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddJustifyGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddAlignGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddPlaceGroupAsync(sortSeed);
        
        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddFlexBasisGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "basis"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "flex-basis: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "flex-basis: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "flex-basis: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddFlexGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Flex
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "flex"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "flex: {value};", sortSeed);

        #endregion

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "",
            },
            "display: flex;",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["row"] = "row",
                ["row-reverse"] = "row-reverse",
                ["col"] = "column",
                ["col-reverse"] = "column-reverse"
            },
            "flex-direction: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["wrap"] = "wrap",
                ["wrap-reverse"] = "wrap-reverse",
                ["nowrap"] = "nowrap"
            },
            "flex-wrap: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["1"] = "1 1 0%",
                ["auto"] = "1 1 auto",
                ["initial"] = "0 1 auto",
                ["none"] = "none"
            },
            "flex: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["grow"] = "1",
                ["grow-0"] = "0"
            },
            "flex-grow: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["shrink"] = "1",
                ["shrink-0"] = "0"
            },
            "flex-shrink: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grow
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grow"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer,number", "flex-grow: {value};", sortSeed);

        #endregion

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Shrink
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "shrink"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer,number", "flex-shrink: {value};", sortSeed);

        #endregion
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Order
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "order"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "order: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["first"] = int.MinValue.ToString(),
                ["last"] = int.MaxValue.ToString(),
                ["none"] = "0"
            },
            "order: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24),
            "order: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Order (Negative)
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "-order"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "order: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, true),
            "order: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddGridGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Grid Template Columns
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-cols"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-template-columns: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "grid-template-columns: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "repeat({value}, minmax(0, 1fr))"),
            "grid-template-columns: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["col-auto"] = "auto"
            },
            "grid-column: {value};",
            sortSeed
        );        
        
        #region Grid Column Span
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-span"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-column: {value}", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["full"] = "1 / -1"
            },
            "grid-column: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "span {value} / span {value}"),
            "grid-column: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Column Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-start"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "grid-column-start: {value}", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-column-start: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-column-start: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Column End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "col-end"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "grid-column-end: {value}", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-column-end: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-column-end: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Template Rows
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-rows"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-template-rows: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "grid-template-rows: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "repeat({value}, minmax(0, 1fr))"),
            "grid-template-rows: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["row-auto"] = "auto"
            },
            "grid-row: {value};",
            sortSeed
        );        
        
        #region Grid Row Span
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-span"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-row: {value}", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["full"] = "1 / -1"
            },
            "grid-row: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24, false, "span {value} / span {value}"),
            "grid-row: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Row Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-start"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "grid-row-start: {value}", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-row-start: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-start: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Row End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "row-end"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("integer", "grid-row-end: {value}", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto"
            },
            "grid-row-end: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-end: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Auto Flow
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "grid-flow"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["row"] = "row",
                ["col"] = "column",
                ["dense"] = "dense",
                ["row-dense"] = "row dense",
                ["col-dense"] = "column dense"
            },
            "grid-auto-flow: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 25),
            "grid-row-end: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Auto Columns
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "auto-cols"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-auto-columns: {value}", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fr"] = "minmax(0, 1fr)"
            },
            "grid-auto-columns: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Grid Auto Rows
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "auto-rows"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "grid-auto-rows: {value}", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["min"] = "min-content",
                ["max"] = "max-content",
                ["fr"] = "minmax(0, 1fr)"
            },
            "grid-auto-rows: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Gap
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "gap: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "gap: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "gap: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Gap X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap-x"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "column-gap: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "column-gap: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "column-gap: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Gap Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "gap-y"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "row-gap: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "row-gap: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "row-gap: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddJustifyGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Justify Content
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "justify-content: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Justify Items
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify-items"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["stretch"] = "stretch"
            },
            "justify-items: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Justify Self
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "justify-self"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["stretch"] = "stretch"
            },
            "justify-self: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddAlignGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Align Content
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "content"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "align-content: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Align Items
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "items"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "flex-start",
                ["end"] = "flex-end",
                ["center"] = "center",
                ["baseline"] = "baseline",
                ["stretch"] = "stretch"
            },
            "align-items: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Align Self
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "self"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["start"] = "flex-start",
                ["end"] = "flex-end",
                ["center"] = "center",
                ["baseline"] = "baseline",
                ["stretch"] = "stretch"
            },
            "align-self: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddPlaceGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Place Content
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-content"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
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
            "place-content: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Place Items
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-items"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["baseline"] = "baseline",
                ["stretch"] = "stretch"
            },
            "place-items: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Place Self
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "place-self"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["stretch"] = "stretch"
            },
            "place-self: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
}