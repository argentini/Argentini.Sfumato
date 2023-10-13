using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass Fill { get; } = new()
    {
        SelectorPrefix = "fill",
        PropertyName = "fill",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["none"] = "none"
        }
    };

    public ScssBaseClass Stroke { get; } = new()
    {
        SelectorPrefix = "stroke",
        PropertyName = "stroke",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["none"] = "none"
        }
    };
    
    public ScssBaseClass StrokeWidth { get; } = new()
    {
        SelectorPrefix = "stroke",
        PropertyName = "stroke-width",
        PrefixValueTypes = "length,integer,number,percentage",
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty,
            ["0"] = "0",
            ["1"] = "1",
            ["2"] = "2"
        }
    };
}