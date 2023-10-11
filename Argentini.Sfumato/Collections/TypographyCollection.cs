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
    
    
    
    
    
    
}