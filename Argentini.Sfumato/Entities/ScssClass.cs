namespace Argentini.Sfumato.Entities;

public sealed class ScssClass
{
    public List<string> Selectors { get; } = [];
    public string PseudoclassSuffix { get; set; } = string.Empty; // e.g. '::after:hover'
    public string ScssProperties { get; set; } = string.Empty; // SCSS property and value
    public string CompactScssProperties { get; set; } = string.Empty; // SCSS property and value compacted for comparisons
    public int Depth { get; set; }

    public string GetScssMarkup()
    {
        var needsLeadingDots = Selectors.Any(s => s.StartsWith('.') == false);

        if (needsLeadingDots)
        {
            var oldList = Selectors.ToList();

            Selectors.Clear();

            foreach (var selector in oldList)
                Selectors.Add($"{(selector.StartsWith("html") ? string.Empty : ".")}{selector.TrimStart('.')}");
        }
        
        return
            $$"""
            {{string.Join($"{PseudoclassSuffix}, ", Selectors)}}{{PseudoclassSuffix}} {
            {{ScssProperties.Indent(4)}}
            }{{Environment.NewLine}}
            """.Indent(Depth * 4);
    }
}
