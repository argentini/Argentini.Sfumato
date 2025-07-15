// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato.Entities.CssClassProcessing;

public sealed class VariantMetadata
{
    public int PrefixOrder { get; init; }
    public string PrefixType { get; init; } = string.Empty;
    public string Statement { get; init; } = string.Empty;
    public string SelectorPrefix { get; init; } = string.Empty;
    public string SelectorSuffix { get; init; } = string.Empty;
    public bool Inheritable { get; init; }
    public int PrioritySort { get; init; }

    public VariantMetadata CreateNewPseudoClass(string suffix)
    {
        return new VariantMetadata()
        {
            PrefixType = "pseudoclass",
            SelectorSuffix = suffix,
            Inheritable = Inheritable,
            PrioritySort = PrioritySort,
        };
    }
}