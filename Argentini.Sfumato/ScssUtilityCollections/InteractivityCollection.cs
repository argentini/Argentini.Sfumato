using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class InteractivityCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static async Task AddAllInteractivityClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection)
    {
        var sortSeed = 600000;
        var internalCollection = new Dictionary<string, ScssUtilityClassGroup>();

        sortSeed = await internalCollection.AddAccentGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddAppearanceGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddCursorGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddCaretGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddPointerEventsGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddResizeGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddScrollBehaviorGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddScrollMarginGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddScrollPaddingGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddScrollSnapGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddTouchSelectSnapGroupAsync(sortSeed);
        sortSeed = await internalCollection.AddWillChangeSnapGroupAsync(sortSeed);
        
        foreach (var group in internalCollection)
            collection.TryAdd(group.Key, group.Value);
    }
    
    public static async Task<int> AddAccentGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "accent"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "accent-color: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "accent-color: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddAppearanceGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "appearance"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "appearance: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }

    public static async Task<int> AddCursorGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "cursor"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("raw", "cursor: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["alias"] = "alias",
                ["all-scroll"] = "all-scroll",
                ["auto"] = "auto",
                ["cell"] = "cell",
                ["context-menu"] = "context-menu",
                ["col-resize"] = "col-resize",
                ["copy"] = "copy",
                ["crosshair"] = "crosshair",
                ["default"] = "default",
                ["e-resize"] = "e-resize",
                ["ew-resize"] = "ew-resize",
                ["grab"] = "grab",
                ["grabbing"] = "grabbing",
                ["help"] = "help",
                ["move"] = "move",
                ["n-resize"] = "n-resize",
                ["ne-resize"] = "ne-resize",
                ["nesw-resize"] = "nesw-resize",
                ["ns-resize"] = "ns-resize",
                ["nw-resize"] = "nw-resize",
                ["nwse-resize"] = "nwse-resize",
                ["no-drop"] = "no-drop",
                ["none"] = "none",
                ["not-allowed"] = "not-allowed",
                ["pointer"] = "pointer",
                ["progress"] = "progress",
                ["row-resize"] = "row-resize",
                ["s-resize"] = "s-resize",
                ["se-resize"] = "se-resize",
                ["sw-resize"] = "sw-resize",
                ["text"] = "text",
                ["vertical-text"] = "vertical-text",
                ["w-resize"] = "w-resize",
                ["wait"] = "wait",
                ["zoom-in"] = "zoom-in",
                ["zoom-out"] = "zoom-out"
            },
            "cursor: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddCaretGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "caret"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("color", "caret-color: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            SfumatoScss.Colors,
            "caret-color: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddPointerEventsGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pointer-events"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none",
                ["auto"] = "auto"
            },
            "pointer-events: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddResizeGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "resize"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "both",
                ["none"] = "none",
                ["y"] = "vertical",
                ["x"] = "horizontal"
            },
            "resize: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddScrollBehaviorGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["smooth"] = "smooth"
            },
            "scroll-behavior: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddScrollMarginGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Scroll Margin
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-m"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-margin: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-mx"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            scroll-margin-left: {value};
            scroll-margin-right: {value};
            """,
            sortSeed
            );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            """
            scroll-margin-left: {value};
            scroll-margin-right: {value};
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            scroll-margin-left: {value};
            scroll-margin-right: {value};
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-my"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """,
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-ms"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-inline-start: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-inline-start: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-inline-start: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-me"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-inline-end: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-inline-end: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-inline-end: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Top
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-mt"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-top: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-top: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-top: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Right
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-mr"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-right: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-right: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-right: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Bottom
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-mb"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-bottom: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-bottom: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-bottom: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Left
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-ml"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-left: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-left: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-left: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }    
    
    public static async Task<int> AddScrollPaddingGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        #region Scroll Padding
        
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-p"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-padding: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding X
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-px"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            scroll-padding-left: {value};
            scroll-padding-right: {value};
            """,
            sortSeed
            );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            """
            scroll-padding-left: {value};
            scroll-padding-right: {value};
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            scroll-padding-left: {value};
            scroll-padding-right: {value};
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Y
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-py"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            scroll-padding-top: {value};
            scroll-padding-bottom: {value};
            """,
            sortSeed
        );

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            """
            scroll-padding-top: {value};
            scroll-padding-bottom: {value};
            """,
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            scroll-padding-top: {value};
            scroll-padding-bottom: {value};
            """,
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Start
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-ps"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-inline-start: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-inline-start: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-inline-start: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding End
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pe"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-inline-end: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-inline-end: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-inline-end: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Top
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pt"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-top: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-top: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-top: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Right
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pr"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-right: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-right: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-right: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Bottom
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pb"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-bottom: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-bottom: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-bottom: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Left
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pl"
        };
    
        #region Arbitrary Value Options

        sortSeed = await scssUtilityClassGroup.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-left: {value};", sortSeed);

        #endregion
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-left: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-left: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        #endregion
        
        return await Task.FromResult(sortSeed);
    }    
    
    public static async Task<int> AddScrollSnapGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "snap"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["align-none"] = "none"
            },
            "scroll-snap-align: {value};",
            false,
            sortSeed
        );
        
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["normal"] = "normal",
                ["always"] = "always"
            },
            "scroll-snap-stop: {value};",
            false,
            sortSeed
        );

        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none",
                ["x"] = "x var(--sf-scroll-snap-strictness)",
                ["y"] = "y var(--sf-scroll-snap-strictness)",
                ["both"] = "both var(--sf-scroll-snap-strictness)"
            },
            "scroll-snap-type: {value};",
            false,
            sortSeed
        );

        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        sortSeed = await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["snap-mandatory"] = "mandatory",
                ["snap-proximity"] = "proximity"
            },
            "--sf-scroll-snap-strictness: {value};",
            sortSeed
        );        
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddTouchSelectSnapGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "touch"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["none"] = "none",
                ["pan-x"] = "pan-x",
                ["pan-left"] = "pan-left",
                ["pan-right"] = "pan-right",
                ["pan-y"] = "pan-y",
                ["pan-up"] = "pan-up",
                ["pan-down"] = "pan-down",
                ["pinch-zoom"] = "pinch-zoom",
                ["manipulation"] = "manipulation"
            },
            "touch-action: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "select"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none",
                ["text"] = "text",
                ["all"] = "all",
                ["auto"] = "auto"
            },
            "user-select: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
    
    public static async Task<int> AddWillChangeSnapGroupAsync(this Dictionary<string,ScssUtilityClassGroup> collection, int sortSeed)
    {
        var scssUtilityClassGroup = new ScssUtilityClassGroup
        {
            SelectorPrefix = "will-change"
        };
    
        sortSeed = await scssUtilityClassGroup.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["scroll"] = "scroll-position",
                ["contents"] = "contents",
                ["transform"] = "transform"
            },
            "will-change: {value};",
            false,
            sortSeed
        );
        
        if (collection.TryAdd(scssUtilityClassGroup.SelectorPrefix, scssUtilityClassGroup) == false) throw new Exception();
        
        return await Task.FromResult(sortSeed);
    }
}