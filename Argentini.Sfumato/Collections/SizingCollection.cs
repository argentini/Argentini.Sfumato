using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass Width { get; } = new()
    {
        SelectorPrefix = "w",
        PropertyName = "width",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto",
            ["screen"] = "100vw",
            ["min"] = "min-content",
            ["max"] = "max-content",
            ["fit"] = "fit-content"
        }
    };
    
    public ScssBaseClass MinWidth { get; } = new()
    {
        SelectorPrefix = "min-w",
        PropertyName = "min-width",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["full"] = "100%",
            ["screen"] = "100vw",
            ["min"] = "min-content",
            ["max"] = "max-content",
            ["fit"] = "fit-content"
        }
    };

    public ScssBaseClass MaxWidth { get; } = new()
    {
        SelectorPrefix = "max-w",
        PropertyName = "max-width",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["none"] = "none",
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
            ["7xl"] = "80rem",
            ["full"] = "100%",
            ["min"] = "min-content",
            ["max"] = "max-content",
            ["fit"] = "fit-content",
            ["prose"] = "65ch",
            ["screen-zero"] = "calc(#{$phab-breakpoint} - 1px)",
            ["screen-phab"] = "#{$tabp-breakpoint}",
            ["screen-tabp"] = "#{$tabl-breakpoint}",
            ["screen-tabl"] = "#{$note-breakpoint}",
            ["screen-note"] = "#{$desk-breakpoint}",
            ["screen-desk"] = "#{$elas-breakpoint}",
            ["screen-elas"] = "#{$tabp-breakpoint}"
        }
    };
    
    public ScssBaseClass Height { get; } = new()
    {
        SelectorPrefix = "h",
        PropertyName = "height",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        AddFractionOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["auto"] = "auto",
            ["screen"] = "100vh",
            ["min"] = "min-content",
            ["max"] = "max-content",
            ["fit"] = "fit-content"
        }
    };
    
    public ScssBaseClass MinHeight { get; } = new()
    {
        SelectorPrefix = "min-h",
        PropertyName = "min-height",
        PrefixValueTypes = "length,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["full"] = "100%",
            ["screen"] = "100vh",
            ["min"] = "min-content",
            ["max"] = "max-content",
            ["fit"] = "fit-content"
        }
    };

    public ScssBaseClass MaxHeight { get; } = new()
    {
        SelectorPrefix = "max-h",
        PropertyName = "max-height",
        PrefixValueTypes = "length,percentage",
        AddNumberedRemUnitsOptionsMinValue = 0.5m,
        AddNumberedRemUnitsOptionsMaxValue = 96m,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0px",
            ["px"] = "1px",
            ["none"] = "none",
            ["full"] = "100%",
            ["screen"] = "100vh",
            ["min"] = "min-content",
            ["max"] = "max-content",
            ["fit"] = "fit-content"
        }
    };
}