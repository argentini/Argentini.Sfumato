namespace Argentini.Sfumato.Collections;

public sealed class BreakAfter
{
    public Dictionary<string, string> BreakAfters { get; } = new()
    {
        ["auto"] = "auto",
        ["avoid"] = "avoid",
        ["all"] = "all",
        ["avoid-page"] = "avoid-page",
        ["page"] = "page",
        ["left"] = "left",
        ["right"] = "right",
        ["column"] = "column"
    };

    public BreakAfter()
    {
        foreach (var item in BreakAfters)
        {
            Classes.Add($"break-after-{item.Key}", new ScssClass
            {
                Value = item.Value,
                Template = "break-after: {value};"
            });
        }
    }
    
    // ReSharper disable once CollectionNeverQueried.Global
    public Dictionary<string, ScssClass> Classes { get; } = new();
}