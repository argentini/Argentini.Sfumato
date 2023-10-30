namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayInline : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inline";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: inline;"
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