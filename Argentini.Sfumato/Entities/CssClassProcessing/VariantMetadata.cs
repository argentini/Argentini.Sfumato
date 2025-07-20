// ReSharper disable UnusedAutoPropertyAccessor.Global

namespace Argentini.Sfumato.Entities.CssClassProcessing;

public sealed class VariantMetadata
{
    public int PrefixOrder { get; init; }
    public string PrefixType { get; init; } = string.Empty;
    public string Statement { get; set; } = string.Empty;
    public string SelectorPrefix { get; set; } = string.Empty;
    public string SelectorSuffix { get; set; } = string.Empty;
    public bool Inheritable { get; init; }
    public bool SpecialCase { get; init; }
    public bool CanHaveNumericSuffix { get; init; }
    public int PrioritySort { get; init; }

    public VariantMetadata CreateNewVariant(string? prefixType = null, int? prefixOrder = null, string? statement = null, string? prefix = null, string? suffix = null, int? prioritySort = null)
    {
        return new VariantMetadata
        {
            PrefixOrder = prefixOrder ?? PrefixOrder,
            PrefixType = prefixType ?? PrefixType,
            Statement = statement ?? Statement,
            SelectorPrefix = prefix ?? SelectorPrefix,
            SelectorSuffix = suffix ?? SelectorSuffix,
            Inheritable = Inheritable,
            SpecialCase = SpecialCase,
            PrioritySort = prioritySort ?? PrioritySort,
        };
    }
}