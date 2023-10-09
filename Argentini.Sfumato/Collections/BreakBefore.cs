namespace Argentini.Sfumato.Collections;

public sealed class BreakBefore
{
    public Dictionary<string, string> Breakbefores { get; } = new()
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

    public BreakBefore()
    {
        foreach (var item in Breakbefores)
        {
            Classes.Add($"break-before-{item.Key}", new ScssClass
            {
                Value = item.Value,
                Template = "break-before: {value};"
            });
        }
    }
    
    // ReSharper disable once CollectionNeverQueried.Global
    public Dictionary<string, ScssClass> Classes { get; } = new();
}