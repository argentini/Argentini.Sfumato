using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    #region Box Shadow
    
    // 1px = 0.0625rem
    // 2px = 0.125rem
    // 3px = 0.1875rem
    // 4px = 0.25rem
    // 5px = 0.3125rem
    // 6px = 0.375rem
    // 8px = 0.5rem
    // 10px = 0.62rem
    // 12px = 0.745rem
    // 20px = 1.24rem
    // 25px = 1.5525rem
    // 50px = 3.105rem
    
    public ScssBaseClass BoxShadow { get; } = new()
    {
        SelectorPrefix = "shadow",
        PropertyName = "box-shadow",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = "0 1px 0.1875rem 0 rgb(0 0 0 / 0.1), 0 1px 0.125rem -1px rgb(0 0 0 / 0.1)",
            ["sm"] = "0 1px 0.125rem 0 rgb(0 0 0 / 0.05)",
            ["md"] = "0 0.25rem 0.375rem -1px rgb(0 0 0 / 0.1), 0 0.125rem 0.25rem -0.125rem rgb(0 0 0 / 0.1)",
            ["lg"] = "0 0.62rem 0.95rem -0.1875rem rgb(0 0 0 / 0.1), 0 0.25rem 0.375rem -0.25rem rgb(0 0 0 / 0.1)",
            ["xl"] = "0 1.24rem 1.5525rem -0.3125rem rgb(0 0 0 / 0.1), 0 0.5rem 0.62rem -0.375rem rgb(0 0 0 / 0.1)",
            ["2xl"] = "0 1.5525rem 3.105rem -0.745rem rgb(0 0 0 / 0.25)",
            ["inner"] = "inset 0 0.125rem 0.25rem 0 rgb(0 0 0 / 0.05)",
            ["none"] = "0 0 #0000"
        }
    };
    
    public ScssBaseClass BoxShadowColor { get; } = new()
    {
        SelectorPrefix = "shadow",
        PrefixValueTypes = "--sf-shadow-color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    #endregion
    
    public ScssBaseClass Opacity { get; } = new()
    {
        SelectorPrefix = "opacity",
        PropertyName = "opacity",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 100,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    #region Blend Modes

    private static Dictionary<string, string> BlendModeOptions => new()
    {
        ["normal"] = "normal",
        ["multiply"] = "multiply",
        ["screen"] = "multiply",
        ["overlay"] = "overlay",
        ["darken"] = "darken",
        ["lighten"] = "lighten",
        ["color-dodge"] = "color-dodge",
        ["color-burn"] = "color-burn",
        ["hard-light"] = "hard-light",
        ["soft-light"] = "soft-light",
        ["difference"] = "difference",
        ["exclusion"] = "exclusion",
        ["hue"] = "hue",
        ["saturation"] = "saturation",
        ["color"] = "color",
        ["luminosity"] = "luminosity",
        ["plus-lighter"] = "plus-lighter"
    };
    
    public ScssBaseClass MixBlendMode { get; } = new()
    {
        SelectorPrefix = "mix-blend",
        PropertyName = "mix-blend-mode",
        Options = BlendModeOptions
    };
    
    public ScssBaseClass BackgroundBlendMode { get; } = new()
    {
        SelectorPrefix = "bg-blend",
        PropertyName = "background-blend-mode",
        Options = BlendModeOptions
    };
    
    #endregion
}