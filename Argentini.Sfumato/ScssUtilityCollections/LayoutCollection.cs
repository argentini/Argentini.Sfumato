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

        await internalCollection.AddAspectRatioGroupAsync();
        await internalCollection.AddContainerGroupAsync();
        await internalCollection.AddColumnsGroupAsync();
        await internalCollection.AddBreakGroupAsync();
        await internalCollection.AddBoxGroupAsync();
        await internalCollection.AddDisplayGroupAsync();
        await internalCollection.AddFloatGroupAsync();
        await internalCollection.AddObjectGroupAsync();
        await internalCollection.AddOverflowGroupAsync();
        await internalCollection.AddPositionGroupAsync();
        
        foreach (var group in internalCollection)
        {
            foreach (var item in group.Value.Classes)
                item.SortOrder = sortSeed++;
            
            collection.TryAdd(group.Key, group.Value);
        }
    }

    public static async Task AddAspectRatioGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "aspect"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("ratio", "aspect-ratio: {value};");

        #endregion

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["square"] = "1/1",
                ["video"] = "16/9",
                ["screen"] = "4/3"
            },
            "aspect-ratio: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }

    public static async Task AddContainerGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
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
            """
        );
    }

    public static async Task AddColumnsGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "columns"
        };

        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage,integer", "columns: {value};");

        #endregion

        await scssUtilityClassGroup.AddClassesAsync(
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
            "columns: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedClassesAsync(1, 24),
            "columns: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }

    public static async Task AddBreakGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Break After

        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "break-after"
        };

        await scssUtilityClassGroup.AddClassesAsync(
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
            "break-after: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion

        #region Break Before

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "break-before"
        };

        await scssUtilityClassGroup.AddClassesAsync(
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
            "break-before: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion

        #region Break Inside

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "break-inside"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["avoid"] = "avoid",
                ["avoid-page"] = "avoid-page",
                ["avoid-column"] = "avoid-column"
            },
            "break-inside: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion

        #region Box Decoration Break

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "box-decoration"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["clone"] = "clone",
                ["slice"] = "slice"
            },
            "box-decoration-break: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
    }

    public static async Task AddBoxGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "box"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["border"] = "border-box",
                ["content"] = "content-box"
            },
            "box-sizing: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
    }

    public static async Task AddDisplayGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        await ScssUtilityClassGroup.AddVanityClassGroups(
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
            "display: {value};"
        );        
    }

    public static async Task AddFloatGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Float

        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "float"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["right"] = "right",
                ["left"] = "left",
                ["none"] = "none"
            },
            "float: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion

        #region Clear

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "clear"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["right"] = "right",
                ["left"] = "left",
                ["both"] = "both",
                ["none"] = "none"
            },
            "clear: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
    }
    
    public static async Task AddObjectGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Isolation

        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string, string>
            {
                ["isolate"] = "isolate",
                ["isolation-auto"] = "auto",
            },
            "isolation: {value};"
        );
        
        #endregion

        #region Object

        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "object"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["contain"] = "contain",
                ["cover"] = "cover",
                ["fill"] = "fill",
                ["none"] = "none",
                ["scale-down"] = "scale-down"
            },
            "object-fit: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
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
            "object-position: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
    }
    
    public static async Task AddOverflowGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Overflow

        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overflow"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["hidden"] = "hidden",
                ["clip"] = "clip",
                ["visible"] = "visible",
                ["scroll"] = "scroll"
            },
            "overflow: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
        
        #region Overflow X

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overflow-x"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["hidden"] = "hidden",
                ["clip"] = "clip",
                ["visible"] = "visible",
                ["scroll"] = "scroll"
            },
            "overflow-x: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
        
        #region Overflow Y

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overflow-y"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["hidden"] = "hidden",
                ["clip"] = "clip",
                ["visible"] = "visible",
                ["scroll"] = "scroll"
            },
            "overflow-y: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
        
        #region Overscroll

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overscroll"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["contain"] = "contain",
                ["none"] = "none"
            },
            "overscroll-behavior: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
        
        #region Overscroll X

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overscroll-x"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["contain"] = "contain",
                ["none"] = "none"
            },
            "overscroll-behavior-x: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
        
        #region Overscroll Y

        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "overscroll-y"
        };

        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["contain"] = "contain",
                ["none"] = "none"
            },
            "overscroll-behavior-y: {value};"
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();

        #endregion
    }
    
    public static async Task AddPositionGroupAsync(this Dictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Position

        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["static"] = "static",
                ["fixed"] = "fixed",
                ["absolute"] = "absolute",
                ["relative"] = "relative",
                ["sticky"] = "sticky"
            },
            "position: {value};"
        );

        #endregion
        
        #region Top
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "top"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "top: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "top: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "top: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "top: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Right
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "right"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "right: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "right: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "right: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "right: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Bottom
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "bottom"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "bottom: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "bottom: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "bottom: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "bottom: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Left
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "left"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "left: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "left: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "left: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "left: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion

        #region Inset
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "inset"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "inset: {value};");

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "inset: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "inset: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "inset: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Inset X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "inset-x"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            left: {value};
            right: {value};
            """
            );

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            """
            left: {value};
            right: {value};
            """
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            left: {value};
            right: {value};
            """
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            """
            left: {value};
            right: {value};
            """
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Inset Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "inset-y"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            top: {value};
            bottom: {value};
            """
        );

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            """
            top: {value};
            bottom: {value};
            """
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            top: {value};
            bottom: {value};
            """
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            """
            top: {value};
            bottom: {value};
            """
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "start"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "inset-inline-start: {value};"
        );

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "inset-inline-start: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "inset-inline-start: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "inset-inline-start: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "end"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            "inset-inline-end: {value};"
        );

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0px",
                ["px"] = "1px",
                ["auto"] = "auto"
            },
            "inset-inline-end: {value};"
        );
        
        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "inset-inline-end: {value};"
        );

        await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddFractionsClassesAsync(),
            "inset-inline-end: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Visibility

        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string, string>
            {
                ["visible"] = "visible",
                ["invisible"] = "hidden",
                ["collapse"] = "collapse"
            },
            "visibility: {value};"
        );
        
        #endregion
        
        #region Z-Index
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "z"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "integer",
            "z-index: {value};"
        );

        #endregion
        
        await scssUtilityClassGroup.AddClassesAsync(
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
            "z-index: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
    }
}