using Argentini.Sfumato.ScssUtilityCollections.Entities;

namespace Argentini.Sfumato.ScssUtilityCollections;

public static class InteractivityCollection
{
    /// <summary>
    /// Add all SCSS classes from this class.
    /// </summary>
    /// <param name="collection"></param>
    /// <param name="tasks"></param>
    public static void AddAllInteractivityClassesAsync(this ConcurrentDictionary<string,ScssUtilityClassGroup> collection, List<Task> tasks)
    {
        tasks.Add(collection.AddAccentGroupAsync());
        tasks.Add(collection.AddAppearanceGroupAsync());
        tasks.Add(collection.AddCursorGroupAsync());
        tasks.Add(collection.AddCaretGroupAsync());
        tasks.Add(collection.AddPointerEventsGroupAsync());
        tasks.Add(collection.AddResizeGroupAsync());
        tasks.Add(collection.AddScrollBehaviorGroupAsync());
        tasks.Add(collection.AddScrollMarginGroupAsync());
        tasks.Add(collection.AddScrollPaddingGroupAsync());
        tasks.Add(collection.AddScrollSnapGroupAsync());
        tasks.Add(collection.AddTouchSelectSnapGroupAsync());
        tasks.Add(collection.AddWillChangeSnapGroupAsync());
    }
    
    public static async Task AddAccentGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "accent"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("color", "accent-color: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            "accent-color: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }

    public static async Task AddAppearanceGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "appearance"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none"
            },
            "appearance: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }

    public static async Task AddCursorGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "cursor"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("raw", "cursor: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
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
            "cursor: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
    
    public static async Task AddCaretGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "caret"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("color", "caret-color: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            SfumatoScss.Colors,
            "caret-color: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
    
    public static async Task AddPointerEventsGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "pointer-events"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none",
                ["auto"] = "auto"
            },
            "pointer-events: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
    
    public static async Task AddResizeGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "resize"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                [""] = "both",
                ["none"] = "none",
                ["y"] = "vertical",
                ["x"] = "horizontal"
            },
            "resize: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
    
    public static async Task AddScrollBehaviorGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["smooth"] = "smooth"
            },
            "scroll-behavior: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
    
    public static async Task AddScrollMarginGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Scroll Margin
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-m"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-margin: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-mx"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            scroll-margin-left: {value};
            scroll-margin-right: {value};
            """
            );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            """
            scroll-margin-left: {value};
            scroll-margin-right: {value};
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            scroll-margin-left: {value};
            scroll-margin-right: {value};
            """
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-my"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Start
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-ms"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-inline-start: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-inline-start: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-inline-start: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin End
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-me"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-inline-end: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-inline-end: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-inline-end: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Top
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-mt"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-top: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-top: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-top: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Right
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-mr"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-right: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-right: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-right: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Bottom
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-mb"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-bottom: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-bottom: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-bottom: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Margin Left
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-ml"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-margin-left: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-margin-left: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-margin-left: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
    }    
    
    public static async Task AddScrollPaddingGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        #region Scroll Padding
        
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-p"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-padding: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding X
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-px"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            scroll-padding-left: {value};
            scroll-padding-right: {value};
            """
            );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            """
            scroll-padding-left: {value};
            scroll-padding-right: {value};
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            scroll-padding-left: {value};
            scroll-padding-right: {value};
            """
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Y
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-py"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync(
            "length,percentage",
            """
            scroll-padding-top: {value};
            scroll-padding-bottom: {value};
            """
        );

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            """
            scroll-padding-top: {value};
            scroll-padding-bottom: {value};
            """
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            """
            scroll-padding-top: {value};
            scroll-padding-bottom: {value};
            """
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Start
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-ps"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-inline-start: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-inline-start: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-inline-start: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding End
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pe"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-inline-end: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-inline-end: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-inline-end: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Top
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pt"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-top: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-top: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-top: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Right
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pr"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-right: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-right: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-right: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Bottom
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pb"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-bottom: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-bottom: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-bottom: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
        
        #region Scroll Padding Left
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "scroll-pl"
        };
    
        #region Arbitrary Value Options

        await scssUtilityClass.AddAbitraryValueClassAsync("length,percentage", "scroll-padding-left: {value};");

        #endregion
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["0"] = "0",
                ["px"] = "1px",
            },
            "scroll-padding-left: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            await CollectionBase.AddNumberedRemUnitsClassesAsync(0.5m, 96m),
            "scroll-padding-left: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        #endregion
    }    
    
    public static async Task AddScrollSnapGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "snap"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["start"] = "start",
                ["end"] = "end",
                ["center"] = "center",
                ["align-none"] = "none"
            },
            "scroll-snap-align: {value};"
        );
        
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["normal"] = "normal",
                ["always"] = "always"
            },
            "scroll-snap-stop: {value};"
        );

        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none",
                ["x"] = "x var(--sf-scroll-snap-strictness)",
                ["y"] = "y var(--sf-scroll-snap-strictness)",
                ["both"] = "both var(--sf-scroll-snap-strictness)"
            },
            "scroll-snap-type: {value};"
        );

        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        await ScssUtilityClassGroup.AddVanityClassGroups(
            collection,
            new Dictionary<string,string>
            {
                ["snap-mandatory"] = "mandatory",
                ["snap-proximity"] = "proximity"
            },
            "--sf-scroll-snap-strictness: {value};"
        );        
    }
    
    public static async Task AddTouchSelectSnapGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "touch"
        };
    
        await scssUtilityClass.AddClassesAsync(
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
            "touch-action: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
        
        scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "select"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["none"] = "none",
                ["text"] = "text",
                ["all"] = "all",
                ["auto"] = "auto"
            },
            "user-select: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
    
    public static async Task AddWillChangeSnapGroupAsync(this ConcurrentDictionary<string, ScssUtilityClassGroup> collection)
    {
        var scssUtilityClass = new ScssUtilityClassGroup
        {
            SelectorPrefix = "will-change"
        };
    
        await scssUtilityClass.AddClassesAsync(
            new Dictionary<string, string>
            {
                ["auto"] = "auto",
                ["scroll"] = "scroll-position",
                ["contents"] = "contents",
                ["transform"] = "transform"
            },
            "will-change: {value};"
        );
        
        if (collection.TryAdd(scssUtilityClass.SelectorPrefix, scssUtilityClass) == false) throw new Exception();
    }
}