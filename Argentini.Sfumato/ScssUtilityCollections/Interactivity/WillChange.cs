namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class WillChange : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "will-change";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "will-change: auto;",
        ["scroll"] = "will-change: scroll-position;",
        ["contents"] = "will-change: contents;",
        ["transform"] = "will-change: transform;"
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