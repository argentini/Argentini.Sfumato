namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class DisplayTableCaption : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-caption";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: table-caption;"
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