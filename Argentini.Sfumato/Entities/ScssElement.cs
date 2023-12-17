namespace Argentini.Sfumato.Entities;

public sealed class ScssElement
{
    public string Selector => $".{string.Join(", .", Classes.Select(c => c.EscapedSelector))}"; // e.g. .bg-white, .my-bg-color
    public List<CssSelector> Classes { get; } = [];
    public int Level => VariantsPath.Split(':', StringSplitOptions.RemoveEmptyEntries).Length;
    public string VariantsPath => Classes.Count != 0 ? Classes[0].VariantSegment : string.Empty; // e.g. dark:sm:
    public List<string> AllVariants => Classes.Count != 0 ? Classes[0].AllVariants : [];
    public List<string> MediaQueryVariants => Classes.Count != 0 ? Classes[0].MediaQueryVariants : [];
    public List<string> PseudoClassVariants => Classes.Count != 0 ? Classes[0].PseudoClassVariants : [];
    public string Styles { get; set; } = string.Empty;
    public string CompactStyles { get; set; } = string.Empty;
}
