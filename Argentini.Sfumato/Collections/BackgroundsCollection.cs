using Argentini.Sfumato.Collections.Entities;

namespace Argentini.Sfumato.Collections;

public partial class ScssClassCollection
{
    public ScssBaseClass BgColor { get; } = new()
    {
        SelectorPrefix = "bg",
        PropertyName = "background-color",
        PrefixValueTypes = "color",
        AddColorOptions = true,
        Options = new Dictionary<string, string>
        {
            ["-"] = string.Empty
        }
    };
}