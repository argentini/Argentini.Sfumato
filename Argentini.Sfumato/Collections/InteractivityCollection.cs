using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass AccentColor { get; } = new()
    {
        SelectorPrefix = "accent",
        PropertyName = "accent-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssUtilityBaseClass Appearance { get; } = new()
    {
        Selector = "appearance-none",
        Template = "appearance: none;"
    };

    public ScssBaseClass Cursor { get; } = new()
    {
        SelectorPrefix = "cursor",
        PropertyName = "cursor",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
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
        }
    };
    
    public ScssBaseClass CaretColor { get; } = new()
    {
        SelectorPrefix = "caret",
        PropertyName = "caret-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass PointerEvents { get; } = new()
    {
        SelectorPrefix = "pointer-events",
        PropertyName = "pointer-events",
        Options = new Dictionary<string, string>
        {
            ["none"] = "none",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass Resize { get; } = new()
    {
        SelectorPrefix = "resize",
        PropertyName = "resize",
        Options = new Dictionary<string, string>
        {
            [""] = "both",
            ["none"] = "none",
            ["y"] = "vertical",
            ["x"] = "horizontal"
        }
    };
    
    public ScssBaseClass ScrollBehavior { get; } = new()
    {
        SelectorPrefix = "scroll",
        PropertyName = "scroll-behavior",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["smooth"] = "smooth"
        }
    };
    
    #region Scroll Margin
    
    public ScssBaseClass ScrollMargin { get; } = new()
    {
        SelectorPrefix = "scroll-m",
        PropertyName = "scroll-margin",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollMarginX { get; } = new()
    {
        SelectorPrefix = "scroll-mx",
        PropertyName = "scroll-margin-left,scroll-margin-right",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollMarginY { get; } = new()
    {
        SelectorPrefix = "scroll-my",
        PropertyName = "scroll-margin-top,scroll-margin-bottom",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollMarginStart { get; } = new()
    {
        SelectorPrefix = "scroll-ms",
        PropertyName = "scroll-margin-inline-start",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollMarginEnd { get; } = new()
    {
        SelectorPrefix = "scroll-me",
        PropertyName = "scroll-margin-inline-end",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollMarginTop { get; } = new()
    {
        SelectorPrefix = "scroll-mt",
        PropertyName = "scroll-margin-top",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollMarginRight { get; } = new()
    {
        SelectorPrefix = "scroll-mr",
        PropertyName = "scroll-margin-right",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollMarginBottom { get; } = new()
    {
        SelectorPrefix = "scroll-mb",
        PropertyName = "scroll-margin-bottom",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollMarginLeft { get; } = new()
    {
        SelectorPrefix = "scroll-ml",
        PropertyName = "scroll-margin-left",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    #endregion
    
    #region Scroll Padding
    
    public ScssBaseClass ScrollPadding { get; } = new()
    {
        SelectorPrefix = "scroll-p",
        PropertyName = "scroll-padding",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollPaddingX { get; } = new()
    {
        SelectorPrefix = "scroll-px",
        PropertyName = "scroll-padding-left,scroll-padding-right",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollPaddingY { get; } = new()
    {
        SelectorPrefix = "scroll-py",
        PropertyName = "scroll-padding-top,scroll-padding-bottom",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollPaddingStart { get; } = new()
    {
        SelectorPrefix = "scroll-ps",
        PropertyName = "scroll-padding-inline-start",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollPaddingEnd { get; } = new()
    {
        SelectorPrefix = "scroll-pe",
        PropertyName = "scroll-padding-inline-end",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollPaddingTop { get; } = new()
    {
        SelectorPrefix = "scroll-pt",
        PropertyName = "scroll-padding-top",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollPaddingRight { get; } = new()
    {
        SelectorPrefix = "scroll-pr",
        PropertyName = "scroll-padding-right",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollPaddingBottom { get; } = new()
    {
        SelectorPrefix = "scroll-pb",
        PropertyName = "scroll-padding-bottom",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass ScrollPaddingLeft { get; } = new()
    {
        SelectorPrefix = "scroll-pl",
        PropertyName = "scroll-padding-left",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    #endregion
    
    public ScssBaseClass ScrollSnapAlign { get; } = new()
    {
        SelectorPrefix = "snap",
        PropertyName = "scroll-snap-align",
        Options = new Dictionary<string, string>
        {
            ["start"] = "start",
            ["end"] = "end",
            ["center"] = "center",
            ["align-none"] = "none"
        }
    };
    
    public ScssBaseClass ScrollSnapStop { get; } = new()
    {
        SelectorPrefix = "snap",
        PropertyName = "scroll-snap-stop",
        Options = new Dictionary<string, string>
        {
            ["normal"] = "normal",
            ["always"] = "always"
        }
    };
  
    public ScssBaseClass ScrollSnapType { get; } = new()
    {
        SelectorPrefix = "snap",
        PropertyName = "scroll-snap-type",
        Options = new Dictionary<string, string>
        {
            ["none"] = "none",
            ["x"] = "x var(--sf-scroll-snap-strictness)",
            ["y"] = "y var(--sf-scroll-snap-strictness)",
            ["both"] = "both var(--sf-scroll-snap-strictness)"
        }
    };
    
    public ScssUtilityBaseClass SnapMandatory { get; } = new()
    {
        Selector = "snap-mandatory",
        Template = "--sf-scroll-snap-strictness: mandatory;"
    };
    
    public ScssUtilityBaseClass SnapProximity { get; } = new()
    {
        Selector = "snap-proximity",
        Template = "--sf-scroll-snap-strictness: proximity;"
    };
    
    public ScssBaseClass TouchAction { get; } = new()
    {
        SelectorPrefix = "touch",
        PropertyName = "touch-action",
        Options = new Dictionary<string, string>
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
        }
    };
    
    public ScssBaseClass UserSelect { get; } = new()
    {
        SelectorPrefix = "select",
        PropertyName = "user-select",
        Options = new Dictionary<string, string>
        {
            ["none"] = "none",
            ["text"] = "text",
            ["all"] = "all",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass WillChange { get; } = new()
    {
        SelectorPrefix = "will-change",
        PropertyName = "will-change",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["scroll"] = "scroll-position",
            ["contents"] = "contents",
            ["transform"] = "transform"
        }
    };
}