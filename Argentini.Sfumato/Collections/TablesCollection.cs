using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass BorderCollapse { get; } = new()
    {
        PropertyName = "border-collapse",
        Options = new Dictionary<string, string>
        {
            ["border-collapse"] = "collapse",
            ["border-separate"] = "separate"
        }
    };
    
    public ScssBaseClass BorderSpacing { get; } = new()
    {
        SelectorPrefix = "border-spacing",
        PropertyTemplate = "border-spacing: {value} {value};",
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

    public ScssBaseClass BorderSpacingX { get; } = new()
    {
        SelectorPrefix = "border-spacing-x",
        PropertyTemplate = "border-spacing: {value} var(--tw-border-spacing-y);",
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
    
    public ScssBaseClass BorderSpacingY { get; } = new()
    {
        SelectorPrefix = "border-spacing-y",
        PropertyTemplate = "border-spacing: var(--tw-border-spacing-x) {value};",
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
    
    public ScssBaseClass TableLayout { get; } = new()
    {
        SelectorPrefix = "table",
        PropertyName = "table-layout",
        Options = new Dictionary<string, string>
        {
            ["auto"] = "auto",
            ["fixed"] = "fixed"
        }
    };
   
    public ScssBaseClass CaptionSide { get; } = new()
    {
        SelectorPrefix = "caption",
        PropertyName = "caption-side",
        Options = new Dictionary<string, string>
        {
            ["top"] = "top",
            ["bottom"] = "bottom"
        }
    };
}