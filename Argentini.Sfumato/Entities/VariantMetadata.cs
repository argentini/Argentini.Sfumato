// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato.Entities;

public class VariantMetadata
{
    public int PrefixOrder { get; init; }
    public int Priority { get; init; }
    public string PrefixType { get; init; } = string.Empty;
    public string Statement { get; init; } = string.Empty;
}