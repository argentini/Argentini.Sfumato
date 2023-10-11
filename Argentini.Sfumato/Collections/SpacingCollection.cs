using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass Padding { get; } = new()
    {
        SelectorPrefix = "p",
        PropertyName = "padding",
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
    
    public ScssBaseClass PaddingX { get; } = new()
    {
        SelectorPrefix = "px",
        PropertyName = "padding-left,padding-right",
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
    
    public ScssBaseClass PaddingY { get; } = new()
    {
        SelectorPrefix = "py",
        PropertyName = "padding-top,padding-bottom",
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
    
    public ScssBaseClass PaddingInlineStart { get; } = new()
    {
        SelectorPrefix = "ps",
        PropertyName = "padding-inline-start",
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
    
    public ScssBaseClass PaddingInlineEnd { get; } = new()
    {
        SelectorPrefix = "pe",
        PropertyName = "padding-inline-end",
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
    
    public ScssBaseClass PaddingTop { get; } = new()
    {
        SelectorPrefix = "pt",
        PropertyName = "padding-top",
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
    
    public ScssBaseClass PaddingRight { get; } = new()
    {
        SelectorPrefix = "pr",
        PropertyName = "padding-right",
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
    
    public ScssBaseClass PaddingBottom { get; } = new()
    {
        SelectorPrefix = "pb",
        PropertyName = "padding-bottom",
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
    
    public ScssBaseClass PaddingLeft { get; } = new()
    {
        SelectorPrefix = "pl",
        PropertyName = "padding-left",
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

    public ScssBaseClass Margin { get; } = new()
    {
        SelectorPrefix = "m",
        PropertyName = "margin",
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
    
    public ScssBaseClass MarginX { get; } = new()
    {
        SelectorPrefix = "mx",
        PropertyName = "margin-left,margin-right",
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
    
    public ScssBaseClass MarginY { get; } = new()
    {
        SelectorPrefix = "my",
        PropertyName = "margin-top,margin-bottom",
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
    
    public ScssBaseClass MarginInlineStart { get; } = new()
    {
        SelectorPrefix = "ms",
        PropertyName = "margin-inline-start",
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
    
    public ScssBaseClass MarginInlineEnd { get; } = new()
    {
        SelectorPrefix = "me",
        PropertyName = "margin-inline-end",
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
    
    public ScssBaseClass MarginTop { get; } = new()
    {
        SelectorPrefix = "mt",
        PropertyName = "margin-top",
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
    
    public ScssBaseClass MarginRight { get; } = new()
    {
        SelectorPrefix = "mr",
        PropertyName = "margin-right",
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
    
    public ScssBaseClass MarginBottom { get; } = new()
    {
        SelectorPrefix = "mb",
        PropertyName = "margin-bottom",
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
    
    public ScssBaseClass MarginLeft { get; } = new()
    {
        SelectorPrefix = "ml",
        PropertyName = "margin-left",
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

    public ScssBaseClass SpaceX { get; } = new()
    {
        SelectorPrefix = "space-x",
        PropertyName = "margin-left",
        PrefixValueTypes = "length,percentage",
        ChildSelector = "& > * + *",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
    
    public ScssBaseClass SpaceY { get; } = new()
    {
        SelectorPrefix = "space-y",
        PropertyName = "margin-top",
        PrefixValueTypes = "length,percentage",
        ChildSelector = "& > * + *",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px"
        }
    };
}