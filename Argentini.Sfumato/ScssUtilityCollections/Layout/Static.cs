namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Static : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "static";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "position: static;",
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