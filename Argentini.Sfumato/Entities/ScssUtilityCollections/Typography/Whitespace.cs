namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Whitespace : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "whitespace";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["normal"] = "white-space: normal;",
        ["nowrap"] = "white-space: nowrap;",
        ["no-wrap"] = "white-space: nowrap;",
        ["pre"] = "white-space: pre;",
        ["pre-line"] = "white-space: pre-line;",
        ["pre-wrap"] = "white-space: pre-wrap;",
        ["break-spaces"] = "white-space: break-spaces;"
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
        
        return string.Empty;
    }
}