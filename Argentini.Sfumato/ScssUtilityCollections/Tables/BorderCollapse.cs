namespace Argentini.Sfumato.ScssUtilityCollections.Tables;

public class BorderCollapse : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-collapse";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "border-collapse: collapse;"
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