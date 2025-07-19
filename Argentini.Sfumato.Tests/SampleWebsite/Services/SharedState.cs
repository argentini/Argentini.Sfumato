using System.Text.RegularExpressions;

namespace UmbracoCms.Services;

public partial class SharedState
{
    public Dictionary<string,string> Colors { get; set; } = new(StringComparer.Ordinal);
    public List<ExportItem> ExportItems { get; set; } = [];
    public Dictionary<string,string> CssCustomProperties { get; set; } = new(StringComparer.Ordinal);
    public Dictionary<string,VariantMetadata> Variants { get; set; } = new(StringComparer.Ordinal);
    public Regex CssCustomPropsRegex { get; } = StaticCssCustomPropsRegex();
    public string RowMarkup => 
        """
        <tr id="{id}" class="{rowClasses}col-span-2 grid grid-cols-subgrid border-t border-line/75 dark:border-dark-line/75">
            <td class="align-top font-mono text-xs/6 font-medium {classClasses}">
                <pre><code>{0}</code><span class="inline-block ml-2" onclick="{onclick}"><i id="{icon}" class="cursor-pointer fa-light fa-chevron-up select-none text-canvas-text dark:text-dark-canvas-text" title="{tooltip}"></i></span></pre>
            </td>
            <td class="align-top font-mono text-xs/6 {styleClasses}">
                <pre><code>{1}</code></pre>
            </td>
        </tr>
        """;

    public string BasicRowMarkup3Column => 
        """
        <tr class="col-span-full grid grid-cols-subgrid border-t border-line/75 dark:border-dark-line/75">
            <td class="align-top font-mono text-xs/6 font-medium py-2 text-primary dark:text-secondary">
                <pre><code>{0}</code><span class="inline-block ml-2"></span></pre>
            </td>
            <td class="align-top font-mono text-xs/6 font-medium py-2 text-primary dark:text-secondary">
                <pre><code>{1}</code><span class="inline-block ml-2"></span></pre>
            </td>
            <td class="align-top font-mono text-xs/6 py-2 text-violet-600 dark:text-violet-400">
                <pre><code>{2}</code></pre>
            </td>
        </tr>
        """;

    [GeneratedRegex(@"var\(\s*--[A-Za-z][A-Za-z0-9_-]*", RegexOptions.Compiled)]
    private static partial Regex StaticCssCustomPropsRegex();

    public string GetBasicRowMarkup3Column(string col1, string col2, string code)
    {
        return BasicRowMarkup3Column
            .Replace("{0}", col1.Replace("<", "&lt;").Replace(">", "&gt;"))
            .Replace("{1}", col2.Replace("<", "&lt;").Replace(">", "&gt;"))
            .Replace("{2}", code.Replace("<", "&lt;").Replace(">", "&gt;"));
    }

    public string GetRowMarkup(string rowId, string className, string styles, bool isExample = false, bool hasExample = false)
    {
        var classClasses = "py-2 text-primary dark:text-secondary";
        var styleClasses = "py-2 text-violet-600 dark:text-violet-400";
        var exampleClassClasses = "pb-2 text-black/65 dark:text-white/65";
        var exampleStyleClasses = "pb-2 text-black/65 dark:text-white/65";
        
        var onclick = isExample || hasExample ? 
            $"document.getElementById(`{rowId}-example`).classList.toggle(`hidden`); document.getElementById(`{rowId}-icon`).classList.toggle(`fa-chevron-up`); document.getElementById(`{rowId}-icon`).classList.toggle(`fa-chevron-down`);"
            : string.Empty;
            
        return RowMarkup
            .Replace("{classClasses}", isExample ? exampleClassClasses : classClasses)
            .Replace("{styleClasses}", isExample ? exampleStyleClasses : styleClasses)
            .Replace("fa-light", isExample ? "hidden" : hasExample ? "fa-light" : "hidden")
            .Replace("{tooltip}", isExample || hasExample ? "Toggle usage example" : string.Empty)
            .Replace("{id}", rowId + (isExample ? "-example" : string.Empty))
            .Replace("{icon}", rowId + "-icon")
            .Replace("{onclick}", onclick)
            .Replace("{rowClasses}", isExample ? "hidden italic " : string.Empty)
            .Replace("border-t", isExample ? string.Empty : "border-t")
            .Replace("{0}", className.Replace("<", "&lt;").Replace(">", "&gt;"))
            .Replace("{1}", ProcessStyleComments(styles.Replace("<", "&lt;").Replace(">", "&gt;"), isExample));
    }
    
    private string ProcessStyleComments(string template, bool isExample = false)
    {
        if (isExample)
            return template;
        
        var lines = new List<string>(template.NormalizeLinebreaks().Split('\n'));
        
        for (var i = 0; i < lines.Count; i++)
        {
            var cssCustomProperties = string.Empty;

            foreach (Match m in CssCustomPropsRegex.Matches(lines[i]))
            {
                if (CssCustomProperties.TryGetValue(m.Value.TrimStart("var(").TrimEnd(')'), out var value) == false)
                    continue;
                
                if (value != "initial")
                    cssCustomProperties += $"{value}, ";
            }

            if (string.IsNullOrEmpty(cssCustomProperties.Trim()) == false)
                lines[i] += $"<span class=\"inline-block ml-2 text-gray-500 dark:text-gray-400 italic\">/* {cssCustomProperties.TrimEnd(", ")} */</span>";
        }

        return string.Join('\n', lines);
    }
}