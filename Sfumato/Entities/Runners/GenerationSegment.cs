namespace Sfumato.Entities.Runners;

public sealed class GenerationSegment
{
    public StringBuilder Content { get; set; } = new();
    public Dictionary<string, string> UsedCssCustomProperties { get; } = new(StringComparer.Ordinal);
    public Dictionary<string, string> UsedCss { get; } = new(StringComparer.Ordinal);
}