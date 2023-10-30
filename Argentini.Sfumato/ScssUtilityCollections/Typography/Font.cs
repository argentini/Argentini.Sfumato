namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Font : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "font";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["sans"] = "font-family: ui-sans-serif, system-ui, -apple-system, BlinkMacSystemFont, \"Aptos\", \"Segoe UI\", Roboto, \"Helvetica Neue\", Arial, \"Noto Sans\", sans-serif, \"Apple Color Emoji\", \"Segoe UI Emoji\", \"Segoe UI Symbol\", \"Noto Color Emoji\";",
        ["serif"] = "font-family: ui-serif, Georgia, Cambria, \"Times New Roman\", Times, serif;",
        ["mono"] = "font-family: ui-monospace, SFMono-Regular, Menlo, Monaco, Consolas, \"JetBrains Mono\", \"Liberation Mono\", \"Courier New\", monospace;",
        
        ["thin"] = "font-weight: 100;",
        ["extralight"] = "font-weight: 200;",
        ["light"] = "font-weight: 300;",
        ["normal"] = "font-weight: 400;",
        ["medium"] = "font-weight: 500;",
        ["semibold"] = "font-weight: 600;",
        ["bold"] = "font-weight: 700;",
        ["extrabold"] = "font-weight: 800;",
        ["black"] = "font-weight: 900;"
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer", cssSelector, "font-weight: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "font-family: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}