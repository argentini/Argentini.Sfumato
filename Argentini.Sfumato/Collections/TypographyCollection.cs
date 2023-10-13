using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass FontFamily { get; } = new()
    {
        SelectorPrefix = "font",
        PropertyName = "font-family",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["sans"] = "ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, \"Aptos\", \"Segoe UI\", Roboto, \"Helvetica Neue\", Arial, \"Noto Sans\", sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\"",
            ["serif"] = "ui-serif, Georgia, Cambria, \"Times New Roman\", Times, serif",
            ["mono"] = "ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"JetBrains Mono\", \"Liberation Mono\", \"Courier New\", monospace"
        }
    };
    
    public TextSizeClass TextSizeClass { get; } = new();

    public ScssUtilityBaseClass Antialiased { get; } = new()
    {
        Selector = "antialiased",
        Template = """
                   -webkit-font-smoothing: antialiased;
                   -moz-osx-font-smoothing: grayscale;
                   """
    };
    
    public ScssUtilityBaseClass SubpixelAntialiased { get; } = new()
    {
        Selector = "subpixel-antialiased",
        Template = """
                   -webkit-font-smoothing: auto;
                   -moz-osx-font-smoothing: auto;
                   """
    };
    
    public ScssBaseClass FontStyle { get; } = new()
    {
        PropertyName = "font-style",
        Options = new Dictionary<string, string>
        {
            ["italic"] = "italic",
            ["not-italic"] = "normal"
        }
    };
    
    public ScssBaseClass FontWeight { get; } = new()
    {
        SelectorPrefix = "font",
        PropertyName = "font-weight",
        PrefixValueTypes = "integer",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["thin"] = "100",
            ["extralight"] = "200",
            ["light"] = "300",
            ["normal"] = "400",
            ["medium"] = "500",
            ["semibold"] = "600",
            ["bold"] = "700",
            ["extrabold"] = "800",
            ["black"] = "900"
        }
    };
    
    public ScssBaseClass FontVariantNumeric { get; } = new()
    {
        PropertyName = "font-variant-numeric",
        Options = new Dictionary<string, string>
        {
            ["normal-nums"] = "normal",
            ["ordinal"] = "ordinal",
            ["slashed-zero"] = "slashed-zero",
            ["lining-sums"] = "lining-sums",
            ["oldstyle-nums"] = "oldstyle-nums",
            ["proportional-nums"] = "proportional-nums",
            ["tabular-nums"] = "tabular-nums",
            ["diagonal-fractions"] = "diagonal-fractions",
            ["stacked-fractions"] = "stacked-fractions"
        }
    };
    
    public ScssBaseClass LetterSpacing { get; } = new()
    {
        SelectorPrefix = "tracking",
        PropertyName = "letter-spacing",
        PrefixValueTypes = "length",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["tighter"] = "-0.05em",
            ["tight"] = "-0.025em",
            ["normal"] = "0em",
            ["wide"] = "0.025em",
            ["wider"] = "0.05em",
            ["widest"] = "0.1em"
        }
    };
    
    public ScssBaseClass LineClamp { get; } = new()
    {
        SelectorPrefix = "line-clamp",
        PropertyName = "-webkit-line-clamp",
        PrefixValueTypes = "integer",
        AddNumberedOptionsMinValue = 1,
        AddNumberedOptionsMaxValue = 6,
        AddNumberedOptionsValueTemplate = "{value}; overflow: hidden; display: -webkit-box; -webkit-box-orient: vertical;",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssUtilityBaseClass LineClampNone { get; } = new()
    {
        Selector = "line-clamp-none",
        Template = """
                   overflow: visible;
                   display: block;
                   -webkit-box-orient: horizontal;
                   -webkit-line-clamp: none;
                   """
    };
    
    public ScssBaseClass LineHeight { get; } = new()
    {
        SelectorPrefix = "leading",
        PropertyName = "line-height",
        PrefixValueTypes = "length,integer",
        AddNumberedRemUnitsOptionsMinValue = 3,
        AddNumberedRemUnitsOptionsMaxValue = 10,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["none"] = "1",
            ["tight"] = "1.25",
            ["snug"] = "1.375",
            ["normal"] = "1.5",
            ["relaxed"] = "1.625",
            ["loose"] = "2"
        }
    };
    
    public ScssBaseClass ListStyleImage { get; } = new()
    {
        SelectorPrefix = "list-image",
        PropertyName = "list-style-image",
        PrefixValueTypes = "url",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass ListStylePosition { get; } = new()
    {
        PropertyName = "list-style-position",
        Options = new Dictionary<string, string>
        {
            ["list-inside"] = "inside",
            ["list-outside"] = "outside"
        }
    };
    
    public ScssBaseClass ListStyleType { get; } = new()
    {
        SelectorPrefix = "list",
        PropertyName = "list-style-type",
        Options = new Dictionary<string, string>
        {
            ["-"] = "",
            ["none"] = "none",
            ["disc"] = "disc",
            ["decimal"] = "decimal"
        }
    };
    
    public ScssBaseClass TextAlign { get; } = new()
    {
        SelectorPrefix = "text",
        PropertyName = "text-align",
        Options = new Dictionary<string, string>
        {
            ["left"] = "left",
            ["center"] = "center",
            ["right"] = "right",
            ["justify"] = "justify",
            ["start"] = "start",
            ["end"] = "end"
        }
    };
    
    public ScssBaseClass TextColor { get; } = new()
    {
        SelectorPrefix = "text",
        PropertyName = "color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass TextDecoration { get; } = new()
    {
        PropertyName = "text-decoration-line",
        Options = new Dictionary<string, string>
        {
            ["underline"] = "underline",
            ["overline"] = "overline",
            ["line-through"] = "line-through",
            ["no-underline"] = "none"
        }
    };
    
    public ScssBaseClass TextDecorationColor { get; } = new()
    {
        SelectorPrefix = "decoration",
        PropertyName = "text-decoration-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass TextDecorationStyle { get; } = new()
    {
        SelectorPrefix = "decoration",
        PropertyName = "text-decoration-style",
        Options = new Dictionary<string, string>
        {
            ["solid"] = "solid",
            ["double"] = "double",
            ["dotted"] = "dotted",
            ["dashed"] = "dashed",
            ["wavy"] = "wavy"
        }
    };

    public ScssBaseClass TextDecorationThickness { get; } = new()
    {
        SelectorPrefix = "decoration",
        PropertyName = "text-decoration-thickness",
        PrefixValueTypes = "length",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto",
            ["from-font"] = "from-font",
            ["0"] = "0px",
            ["1"] = "1px",
            ["2"] = 2.PxToRem(),
            ["4"] = 4.PxToRem(),
            ["8"] = 8.PxToRem()
        }
    };    
    
    public ScssBaseClass TextUnderlineOffset { get; } = new()
    {
        SelectorPrefix = "underline-offset",
        PropertyName = "text-underline-offset",
        PrefixValueTypes = "length",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["auto"] = "auto",
            ["0"] = "0px",
            ["1"] = "1px",
            ["2"] = 2.PxToRem(),
            ["4"] = 4.PxToRem(),
            ["8"] = 8.PxToRem()
        }
    };
    
    public ScssBaseClass TextTransform { get; } = new()
    {
        PropertyName = "text-transform",
        Options = new Dictionary<string, string>
        {
            ["uppercase"] = "uppercase",
            ["lowercase"] = "lowercase",
            ["capitalize"] = "capitalize",
            ["normal-case"] = "none"
        }
    };
    
    public ScssUtilityBaseClass TextOverflowTruncate { get; } = new()
    {
        Selector = "truncate",
        Template = """
                   overflow: hidden;
                   text-overflow: ellipsis;
                   white-space: nowrap;
                   """
    };

    public ScssBaseClass TextOverflow { get; } = new()
    {
        PropertyName = "text-overflow",
        Options = new Dictionary<string, string>
        {
            ["text-ellipsis"] = "ellipsis",
            ["text-clip"] = "clip"
        }
    };
    
    public ScssBaseClass TextIndent { get; } = new()
    {
        SelectorPrefix = "indent",
        PropertyName = "text-indent",
        PrefixValueTypes = "length",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass VerticalAlign { get; } = new()
    {
        SelectorPrefix = "align",
        PropertyName = "vertical-align",
        PrefixValueTypes = "length",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["baseline"] = "baseline",
            ["top"] = "top",
            ["middle"] = "middle",
            ["bottom"] = "bottom",
            ["text-top"] = "text-top",
            ["text-bottom"] = "text-bottom",
            ["sub"] = "sub",
            ["super"] = "super"
        }
    };
    
    public ScssBaseClass Whitespace { get; } = new()
    {
        SelectorPrefix = "whitespace",
        PropertyName = "white-space",
        Options = new Dictionary<string, string>
        {
            ["normal"] = "normal",
            ["nowrap"] = "nowrap",
            ["pre"] = "pre",
            ["pre-line"] = "pre-line",
            ["pre-wrap"] = "pre-wrap",
            ["break-spaces"] = "break-spaces"
        }
    };

    public ScssUtilityBaseClass WordBreakNormal { get; } = new()
    {
        Selector = "break-normal",
        Template = """
                   overflow-wrap: normal;
                   word-break: normal;
                   """
    };

    public ScssUtilityBaseClass WordBreakWords { get; } = new()
    {
        Selector = "break-words",
        Template = """
                   overflow-wrap: break-word;
                   """
    };
    
    public ScssBaseClass WordBreak { get; } = new()
    {
        SelectorPrefix = "break",
        PropertyName = "word-break",
        Options = new Dictionary<string, string>
        {
            ["all"] = "break-all",
            ["keep"] = "keep-all"
        }
    };
    
    public ScssBaseClass Hyphens { get; } = new()
    {
        SelectorPrefix = "hyphens",
        PropertyName = "hyphens",
        Options = new Dictionary<string, string>
        {
            ["none"] = "none",
            ["manual"] = "manual",
            ["auto"] = "auto"
        }
    };
    
    public ScssBaseClass Content { get; } = new()
    {
        SelectorPrefix = "content",
        PropertyName = "content",
        PrefixValueTypes = "string",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["none"] = "none"
        }
    };
}