using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass Blur { get; } = new()
    {
        SelectorPrefix = "blur",
        PropertyTemplate = "filter: blur({value});",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = 8.PxToRem(),
            ["none"] = "0",
            ["sm"] = 4.PxToRem(),
            ["md"] = 12.PxToRem(),
            ["lg"] = 16.PxToRem(),
            ["xl"] = 24.PxToRem(),
            ["2xl"] = 40.PxToRem(),
            ["3xl"] = 64.PxToRem(),
        }
    };
    
    public ScssBaseClass Brightness { get; } = new()
    {
        SelectorPrefix = "brightness",
        PropertyTemplate = "filter: brightness({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 200,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass Contrast { get; } = new()
    {
        SelectorPrefix = "contrast",
        PropertyTemplate = "filter: contrast({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 200,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass DropShadowPresets { get; } = new()
    {
        SelectorPrefix = "drop-shadow",
        PropertyName = "filter",
        Options = new Dictionary<string, string>
        {
            [""] = $"drop-shadow(0 1px {2.PxToRem()} rgb(0 0 0 / 0.1)) drop-shadow(0 1px 1px rgb(0 0 0 / 0.06))",
            ["sm"] = "drop-shadow(0 1px 1px rgb(0 0 0 / 0.05))",
            ["md"] = $"drop-shadow(0 {4.PxToRem()} {3.PxToRem()} rgb(0 0 0 / 0.07)) drop-shadow(0 {2.PxToRem()} {2.PxToRem()} rgb(0 0 0 / 0.06))",
            ["lg"] = $"drop-shadow(0 {10.PxToRem()} {8.PxToRem()} rgb(0 0 0 / 0.04)) drop-shadow(0 {4.PxToRem()} {3.PxToRem()} rgb(0 0 0 / 0.1))",
            ["xl"] = $"drop-shadow(0 {20.PxToRem()} {13.PxToRem()} rgb(0 0 0 / 0.03)) drop-shadow(0 {8.PxToRem()} {5.PxToRem()} rgb(0 0 0 / 0.08))",
            ["2xl"] = $"drop-shadow(0 {25.PxToRem()} {25.PxToRem()} rgb(0 0 0 / 0.15))",
            ["none"] = "drop-shadow(0 0 #0000)"
        }
    };

    public ScssBaseClass DropShadow { get; } = new()
    {
        SelectorPrefix = "drop-shadow",
        PropertyTemplate = "filter: drop-shadow({value});",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass Grayscale { get; } = new()
    {
        SelectorPrefix = "grayscale",
        PropertyTemplate = "filter: grayscale({value});",
        PrefixValueTypes = "percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = "100%",
            ["0"] = "0"
        }
    };
    
    public ScssBaseClass HueRotate { get; } = new()
    {
        SelectorPrefix = "hue-rotate",
        PropertyTemplate = "filter: hue-rotate({value});",
        PrefixValueTypes = "angle",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0deg",
            ["15"] = "15deg",
            ["30"] = "30deg",
            ["60"] = "60deg",
            ["90"] = "90deg",
            ["180"] = "180deg"
        }
    };
    
    public ScssBaseClass Invert { get; } = new()
    {
        SelectorPrefix = "invert",
        PropertyTemplate = "filter: invert({value});",
        PrefixValueTypes = "percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = "100%",
            ["0"] = "0"
        }
    };
 
    public ScssBaseClass Saturate { get; } = new()
    {
        SelectorPrefix = "saturate",
        PropertyTemplate = "filter: saturate({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 200,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass Sepia { get; } = new()
    {
        SelectorPrefix = "sepia",
        PropertyTemplate = "filter: sepia({value});",
        PrefixValueTypes = "percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = "100%",
            ["0"] = "0"
        }
    };
    
    public ScssBaseClass BackdropBlur { get; } = new()
    {
        SelectorPrefix = "backdrop-blur",
        PropertyTemplate = "backdrop-filter: blur({value});",
        PrefixValueTypes = "length",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = 8.PxToRem(),
            ["none"] = "0",
            ["sm"] = 4.PxToRem(),
            ["md"] = 12.PxToRem(),
            ["lg"] = 16.PxToRem(),
            ["xl"] = 24.PxToRem(),
            ["2xl"] = 40.PxToRem(),
            ["3xl"] = 64.PxToRem()
        }
    };
    
    public ScssBaseClass BackdropBrightness { get; } = new()
    {
        SelectorPrefix = "backdrop-brightness",
        PropertyTemplate = "backdrop-filter: brightness({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 200,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BackdropContrast { get; } = new()
    {
        SelectorPrefix = "backdrop-contrast",
        PropertyTemplate = "backdrop-filter: contrast({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 200,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BackdropGrayscale { get; } = new()
    {
        SelectorPrefix = "backdrop-grayscale",
        PropertyTemplate = "backdrop-filter: grayscale({value});",
        PrefixValueTypes = "percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = "100%",
            ["0"] = "0"
        }
    };
    
    public ScssBaseClass BackdropHueRotate { get; } = new()
    {
        SelectorPrefix = "backdrop-hue-rotate",
        PropertyTemplate = "backdrop-filter: hue-rotate({value});",
        PrefixValueTypes = "angle",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0deg",
            ["15"] = "15deg",
            ["30"] = "30deg",
            ["60"] = "60deg",
            ["90"] = "90deg",
            ["180"] = "180deg"
        }
    };
    
    public ScssBaseClass BackdropInvert { get; } = new()
    {
        SelectorPrefix = "backdrop-invert",
        PropertyTemplate = "backdrop-filter: invert({value});",
        PrefixValueTypes = "percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = "100%",
            ["0"] = "0"
        }
    };

    public ScssBaseClass BackdropOpacity { get; } = new()
    {
        SelectorPrefix = "backdrop-opacity",
        PropertyTemplate = "backdrop-filter: opacity({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 100,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BackdropSaturate { get; } = new()
    {
        SelectorPrefix = "backdrop-saturate",
        PropertyTemplate = "backdrop-filter: saturate({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 200,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
    
    public ScssBaseClass BackdropSepia { get; } = new()
    {
        SelectorPrefix = "backdrop-sepia",
        PropertyTemplate = "backdrop-filter: sepia({value});",
        PrefixValueTypes = "percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            [""] = "100%",
            ["0"] = "0"
        }
    };
    
}