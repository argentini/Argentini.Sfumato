namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayInlineFlex : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inline-flex";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: inline-flex;"
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