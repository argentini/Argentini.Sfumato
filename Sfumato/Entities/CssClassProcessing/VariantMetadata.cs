// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable PropertyCanBeMadeInitOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sfumato.Entities.CssClassProcessing;

public sealed class VariantMetadata
{
    public int PrefixOrder { get; set; }
    public string PrefixType { get; set; } = string.Empty;
    public string Statement { get; set; } = string.Empty;
    public string SelectorPrefix { get; set; } = string.Empty;
    public string SelectorSuffix { get; set; } = string.Empty;
    public bool Inheritable { get; init; }
    public bool SpecialCase { get; init; }
    public bool CanHaveNumericSuffix { get; init; }
    public bool IsRazorSyntax { get; init; }
    public int PrioritySort { get; set; }

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
            IsRazorSyntax = IsRazorSyntax,
            CanHaveNumericSuffix = CanHaveNumericSuffix,
            PrioritySort = prioritySort ?? PrioritySort,
        };
    }
}