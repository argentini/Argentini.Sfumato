using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass Scale { get; } = new()
    {
        SelectorPrefix = "scale",
        PropertyTemplate = "transform: scale({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 150,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass ScaleX { get; } = new()
    {
        SelectorPrefix = "scale-x",
        PropertyTemplate = "transform: scaleX({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 150,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass ScaleY { get; } = new()
    {
        SelectorPrefix = "scale-y",
        PropertyTemplate = "transform: scaleY({value});",
        PrefixValueTypes = "number",
        AddNumberedOptionsMinValue = 0,
        AddNumberedOptionsMaxValue = 150,
        AddOneBasedPercentageOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };

    public ScssBaseClass Rotate { get; } = new()
    {
        SelectorPrefix = "rotate",
        PropertyTemplate = "transform: rotate({value});",
        PrefixValueTypes = "angle",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0deg",
            ["1"] = "1deg",
            ["2"] = "2deg",
            ["3"] = "3deg",
            ["6"] = "6deg",
            ["12"] = "12deg",
            ["45"] = "45deg",
            ["90"] = "90deg",
            ["180"] = "180deg"
        }
    };

    public ScssBaseClass TranslateX { get; } = new()
    {
        SelectorPrefix = "translate-x",
        PropertyTemplate = "transform: translateX({value});",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass TranslateY { get; } = new()
    {
        SelectorPrefix = "translate-y",
        PropertyTemplate = "transform: translateY({value});",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass SkewX { get; } = new()
    {
        SelectorPrefix = "skew-x",
        PropertyTemplate = "transform: skewX({value});",
        PrefixValueTypes = "angle",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0deg",
            ["1"] = "1deg",
            ["2"] = "2deg",
            ["3"] = "3deg",
            ["6"] = "6deg",
            ["12"] = "12deg",
            ["45"] = "45deg",
            ["90"] = "90deg",
            ["180"] = "180deg"
        }
    };
    
    public ScssBaseClass SkewY { get; } = new()
    {
        SelectorPrefix = "skew-y",
        PropertyTemplate = "transform: skewY({value});",
        PrefixValueTypes = "angle",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0deg",
            ["1"] = "1deg",
            ["2"] = "2deg",
            ["3"] = "3deg",
            ["6"] = "6deg",
            ["12"] = "12deg",
            ["45"] = "45deg",
            ["90"] = "90deg",
            ["180"] = "180deg"
        }
    };
    
    public ScssBaseClass TransformOrigin { get; } = new()
    {
        SelectorPrefix = "origin",
        PropertyTemplate = "transform-origin: {value};",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["center"] = "center",
            ["top"] = "top",
            ["top-right"] = "top right",
            ["right"] = "right",
            ["bottom-right"] = "bottom right",
            ["bottom"] = "bottom",
            ["bottom-left"] = "bottom left",
            ["left"] = "left",
            ["top-left"] = "top left"
        }
    };
}