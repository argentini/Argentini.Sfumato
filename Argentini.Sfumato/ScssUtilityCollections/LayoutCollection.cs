using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class LayoutCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllLayoutClassesAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var sortSeed = 700000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddAspectRatioGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddContainerGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddColumnsGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBreakGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddBoxGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddDisplayGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddFloatGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddObjectGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddOverflowGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddPositionGroupAsync(sortSeed);
        
        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }

    public static async Task<int> AddAspectRatioGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "aspect"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("ratio", "aspect-ratio: {value};", sortSeed);

        #endregion

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["square"] = "1/1",
                ["video"] = "16/9",
                ["screen"] = "4/3"
            },
            "aspect-ratio: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddContainerGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string, string>
            {
                ["container"] = "",
            },
            """
            width: 100%;

            @include sf-media($from: phab) {
               max-width: $phab-breakpoint;
            }

            @include sf-media($from: tabp) {
               max-width: $tabp-breakpoint;
            }

            @include sf-media($from: tabl) {
               max-width: $tabl-breakpoint;
            }

            @include sf-media($from: note) {
               max-width: $note-breakpoint;
            }

            @include sf-media($from: desk) {
               max-width: $desk-breakpoint;
            }

            @include sf-media($from: elas) {
               max-width: $elas-breakpoint;
            }
            """,
            sortSeed
        );
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddColumnsGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "columns"
        };

        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage,integer", "columns: {value};", sortSeed);

        #endregion

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["3xs"] = "16rem",
                ["2xs"] = "18rem",
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
                ["7xl"] = "80rem"
            },
            "columns: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24),
            "columns: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddBreakGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Break After

        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "break-after"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["avoid"] = "avoid",
                ["all"] = "all",
                ["avoid-page"] = "avoid-page",
                ["page"] = "page",
                ["left"] = "left",
                ["right"] = "right",
                ["column"] = "column"
            },
            "break-after: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Break Before

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "break-before"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["avoid"] = "avoid",
                ["all"] = "all",
                ["avoid-page"] = "avoid-page",
                ["page"] = "page",
                ["left"] = "left",
                ["right"] = "right",
                ["column"] = "column"
            },
            "break-before: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Break Inside

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "break-inside"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["avoid"] = "avoid",
                ["avoid-page"] = "avoid-page",
                ["avoid-column"] = "avoid-column"
            },
            "break-inside: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Box Decoration Break

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "box-decoration"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["clone"] = "clone",
                ["slice"] = "slice"
            },
            "box-decoration-break: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddBoxGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "box"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["border"] = "border-box",
                ["content"] = "content-box"
            },
            "box-sizing: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddDisplayGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["block"] = "block",
                ["inline-block"] = "inline-block",
                ["inline"] = "inline",
                ["inline-flex"] = "inline-flex",
                ["table"] = "table",
                ["inline-table"] = "inline-table",
                ["table-caption"] = "table-caption",
                ["table-cell"] = "table-cell",
                ["table-column"] = "table-column",
                ["table-column-group"] = "table-column-group",
                ["table-footer-group"] = "table-footer-group",
                ["table-header-group"] = "table-header-group",
                ["table-row-group"] = "table-row-group",
                ["table-row"] = "table-row",
                ["flow-root"] = "flow-root",
                ["grid"] = "grid",
                ["inline-grid"] = "inline-grid",
                ["contents"] = "contents",
                ["list-item"] = "list-item",
                ["hidden"] = "none"
            },
            "display: {value};",
            sortSeed
        );        
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddFloatGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Float

        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "float"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["right"] = "right",
                ["left"] = "left",
                ["none"] = "none"
            },
            "float: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Clear

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "clear"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["right"] = "right",
                ["left"] = "left",
                ["both"] = "both",
                ["none"] = "none"
            },
            "clear: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddObjectGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Isolation

        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string, string>
            {
                ["isolate"] = "isolate",
                ["isolation-auto"] = "auto",
            },
            "isolation: {value};",
            sortSeed
        );
        
        #endregion

        #region Object

        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "object"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["contain"] = "contain",
                ["cover"] = "cover",
                ["fill"] = "fill",
                ["none"] = "none",
                ["scale-down"] = "scale-down"
            },
            "object-fit: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["bottom"] = "bottom",
                ["center"] = "center",
                ["left"] = "left",
                ["left-bottom"] = "left bottom",
                ["left-top"] = "left top",
                ["right"] = "right",
                ["right-bottom"] = "right bottom",
                ["right-top"] = "right top",
                ["top"] = "top"
            },
            "object-position: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddOverflowGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Overflow

        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overflow"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["hidden"] = "hidden",
                ["clip"] = "clip",
                ["visible"] = "visible",
                ["scroll"] = "scroll"
            },
            "overflow: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Overflow X

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overflow-x"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["hidden"] = "hidden",
                ["clip"] = "clip",
                ["visible"] = "visible",
                ["scroll"] = "scroll"
            },
            "overflow-x: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Overflow Y

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overflow-y"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["hidden"] = "hidden",
                ["clip"] = "clip",
                ["visible"] = "visible",
                ["scroll"] = "scroll"
            },
            "overflow-y: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Overscroll

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overscroll"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["contain"] = "contain",
                ["none"] = "none"
            },
            "overscroll-behavior: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Overscroll X

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overscroll-x"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["contain"] = "contain",
                ["none"] = "none"
            },
            "overscroll-behavior-x: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Overscroll Y

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overscroll-y"
        };

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["contain"] = "contain",
                ["none"] = "none"
            },
            "overscroll-behavior-y: {value};",
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddPositionGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Position

        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["static"] = "static",
                ["fixed"] = "fixed",
                ["absolute"] = "absolute",
                ["relative"] = "relative",
                ["sticky"] = "sticky"
            },
            "position: {value};",
            sortSeed
        );

        #endregion
        
        #region Top
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "top"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "top: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "top: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "top: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "top: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Right
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "right"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "right: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "right: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "right: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "right: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Bottom
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "bottom"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "bottom: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "bottom: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "bottom: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "bottom: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Left
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "left"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "left: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "left: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "left: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "left: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Inset
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "inset"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "inset: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "inset: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "inset: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "inset: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Inset X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "inset-x"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            left: {value};
            right: {value};
            """, sortSeed
            );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            """
            left: {value};
            right: {value};
            """,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            left: {value};
            right: {value};
            """,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            """
            left: {value};
            right: {value};
            """,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Inset Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "inset-y"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            top: {value};
            bottom: {value};
            """, sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            """
            top: {value};
            bottom: {value};
            """,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            top: {value};
            bottom: {value};
            """,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            """
            top: {value};
            bottom: {value};
            """,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "start"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "inset-inline-start: {value};", sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "inset-inline-start: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "inset-inline-start: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "inset-inline-start: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "end"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "inset-inline-end: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "inset-inline-end: {value};",
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "inset-inline-end: {value};",
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "inset-inline-end: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Visibility

        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string, string>
            {
                ["visible"] = "visible",
                ["invisible"] = "hidden",
                ["collapse"] = "collapse"
            },
            "visibility: {value};",
            sortSeed
        );
        
        #endregion
        
        #region Z-Index
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "z"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "integer",
            "z-index: {value};",
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["top"] = int.MaxValue.ToString(),
                ["bottom"] = int.MinValue.ToString(),
                ["0"] = "0",
                ["10"] = "10",
                ["20"] = "20",
                ["30"] = "30",
                ["40"] = "40",
                ["50"] = "50"
            },
            "z-index: {value};",
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }
}