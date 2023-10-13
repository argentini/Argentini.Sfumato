using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    #region Box Shadow
    
    public ScssBaseClass BoxShadow { get; } = new()
    {
        SelectorPrefix = "shadow",
        PropertyName = "box-shadow",
        GlobalGrouping = "shadow",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = $"0 1px {3.PxToRem()} 0 rgb(0 0 0 / 0.1), 0 1px {2.PxToRem()} -1px rgb(0 0 0 / 0.1)",
            ["sm"] = $"0 1px {2.PxToRem()} 0 rgb(0 0 0 / 0.05)",
            ["md"] = $"0 {4.PxToRem()} {6.PxToRem()} -1px rgb(0 0 0 / 0.1), 0 {2.PxToRem()} {4.PxToRem()} -{2.PxToRem()} rgb(0 0 0 / 0.1)",
            ["lg"] = $"0 {10.PxToRem()} {15.PxToRem()} -{3.PxToRem()} rgb(0 0 0 / 0.1), 0 {4.PxToRem()} {6.PxToRem()} -{4.PxToRem()} rgb(0 0 0 / 0.1)",
            ["xl"] = $"0 {20.PxToRem()} {25.PxToRem()} -{5.PxToRem()} rgb(0 0 0 / 0.1), 0 {8.PxToRem()} {10.PxToRem()} -{6.PxToRem()} rgb(0 0 0 / 0.1)",
            ["2xl"] = $"0 {25.PxToRem()} {50.PxToRem()} -{12.PxToRem()} rgb(0 0 0 / 0.25)",
            ["inner"] = $"inset 0 {2.PxToRem()} {4.PxToRem()} 0 rgb(0 0 0 / 0.05)",
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