using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass AspectRatio { get; } = new()
    {
        SelectorPrefix = "aspect",
        PropertyName = "aspect-ratio",
        PrefixValueTypes = "ratio",        
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto",
            ["square"] = "1/1",
            ["video"] = "16/9",
            ["screen"] = "4/3"
        }
    };
    
    public ScssUtilityBaseClass Container { get; } = new()
    {
        Selector = "container",
        Template = """
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
    };

    public ScssBaseClass Columns { get; } = new()
    {
        SelectorPrefix = "columns",
        PropertyName = "columns",
        PrefixValueTypes = "length,percentage,integer",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 24,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
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
        }
    };
    
    public ScssBaseClass BreakAfter { get; } = new()
    {
        SelectorPrefix = "break-after",
        PropertyName = "break-after",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["all"] = "all",
            ["avoid-page"] = "avoid-page",
            ["page"] = "page",
            ["left"] = "left",
            ["right"] = "right",
            ["column"] = "column"
        }
    };

    public ScssBaseClass BreakBefore { get; } = new()
    {
        SelectorPrefix = "break-before",
        PropertyName = "break-before",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["all"] = "all",
            ["avoid-page"] = "avoid-page",
            ["page"] = "page",
            ["left"] = "left",
            ["right"] = "right",
            ["column"] = "column"
        }
    };

    public ScssBaseClass BreakInside { get; } = new()
    {
        SelectorPrefix = "break-inside",
        PropertyName = "break-inside",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["avoid"] = "avoid",
            ["avoid-page"] = "avoid-page",
            ["avoid-column"] = "avoid-column"
        }
    };
    
    public ScssBaseClass BoxDecorationBreak { get; } = new()
    {
        SelectorPrefix = "box-decoration",
        PropertyName = "box-decoration-break",
        Options = new Dictionary<string, string>
        {
            ["clone"] = "clone",
            ["slice"] = "slice"
        }
    };

    public ScssBaseClass BoxSizing { get; } = new()
    {
        SelectorPrefix = "box",
        PropertyName = "box-sizing",
        Options = new Dictionary<string, string>
        {
            ["border"] = "border-box",
            ["content"] = "content-box"
        }
    };
    
    public ScssBaseClass Display { get; } = new()
    {
        PropertyName = "display",
        Options = new Dictionary<string, string>
        {
            ["block"] = "block",
            ["inline-block"] = "inline-block",
            ["inline"] = "inline",
            ["flex"] = "flex",
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
        }
    };
    
    public ScssBaseClass Floats { get; } = new()
    {
        SelectorPrefix = "float",
        PropertyName = "float",
        Options = new Dictionary<string, string>
        {
            ["right"] = "right",
            ["left"] = "left",
            ["none"] = "none"
        }
    };

    public ScssBaseClass Clear { get; } = new()
    {
        SelectorPrefix = "clear",
        PropertyName = "clear",
        Options = new Dictionary<string, string>
        {
            ["right"] = "right",
            ["left"] = "left",
            ["both"] = "both",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass Isolation { get; } = new()
    {
        PropertyName = "isolation",
        Options = new Dictionary<string, string>
        {
            ["isolate"] = "isolate",
            ["isolation-auto"] = "auto"
        }
    };
    
    public ScssBaseClass ObjectFit { get; } = new()
    {
        SelectorPrefix = "object",
        PropertyName = "object-fit",
        Options = new Dictionary<string, string>
        {
            ["contain"] = "contain",
            ["cover"] = "cover",
            ["fill"] = "fill",
            ["none"] = "none",
            ["scale-down"] = "scale-down"
        }
    };

    public ScssBaseClass ObjectBottom { get; } = new()
    {
        SelectorPrefix = "object",
        PropertyName = "object-position",
        Options = new Dictionary<string, string>
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
        }
    };
    
    public ScssBaseClass Overflow { get; } = new()
    {
        SelectorPrefix = "overflow",
        PropertyName = "overflow",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssBaseClass OverflowX { get; } = new()
    {
        SelectorPrefix = "overflow-x",
        PropertyName = "overflow-x",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssBaseClass OverflowY { get; } = new()
    {
        SelectorPrefix = "overflow-y",
        PropertyName = "overflow-y",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["hidden"] = "hidden",
            ["clip"] = "clip",
            ["visible"] = "visible",
            ["scroll"] = "scroll"
        }
    };
    
    public ScssBaseClass OverscrollBehavior { get; } = new()
    {
        SelectorPrefix = "overscroll",
        PropertyName = "overscroll-behavior",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass OverscrollBehaviorX { get; } = new()
    {
        SelectorPrefix = "overscroll-x",
        PropertyName = "overscroll-behavior-x",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass OverscrollBehaviorY { get; } = new()
    {
        SelectorPrefix = "overscroll-y",
        PropertyName = "overscroll-behavior-y",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["contain"] = "contain",
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass Position { get; } = new()
    {
        PropertyName = "position",
        Options = new Dictionary<string, string>
        {
            ["static"] = "static",
            ["fixed"] = "fixed",
            ["absolute"] = "absolute",
            ["relative"] = "relative",
            ["sticky"] = "sticky"
        }
    };

    public ScssBaseClass Top { get; } = new()
    {
        SelectorPrefix = "top",
        PropertyName = "top",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };

    public ScssBaseClass Right { get; } = new()
    {
        SelectorPrefix = "right",
        PropertyName = "right",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };

    public ScssBaseClass Bottom { get; } = new()
    {
        SelectorPrefix = "bottom",
        PropertyName = "bottom",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass Left { get; } = new()
    {
        SelectorPrefix = "left",
        PropertyName = "left",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass Inset { get; } = new()
    {
        SelectorPrefix = "inset",
        PropertyName = "inset",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass InsetX { get; } = new()
    {
        SelectorPrefix = "inset-x",
        PropertyName = "left,right",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass InsetY { get; } = new()
    {
        SelectorPrefix = "inset-y",
        PropertyName = "top,bottom",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass Start { get; } = new()
    {
        SelectorPrefix = "start",
        PropertyName = "inset-inline-start",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass End { get; } = new()
    {
        SelectorPrefix = "end",
        PropertyName = "inset-inline-end",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass Visibility { get; } = new()
    {
        PropertyName = "visibility",
        Options = new Dictionary<string, string>
        {
            ["visible"] = "visible",
            ["invisible"] = "hidden",
            ["collapse"] = "collapse"
        }
    };
    
    public ScssBaseClass Zindex { get; } = new()
    {
        SelectorPrefix = "z",
        PropertyName = "z-index",
        PrefixValueTypes = "integer",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto",
            ["top"] = int.MaxValue.ToString(),
            ["bottom"] = int.MinValue.ToString(),
            ["0"] = "0",
            ["10"] = "10",
            ["20"] = "20",
            ["30"] = "30",
            ["40"] = "40",
            ["50"] = "50"
        }
    };
}