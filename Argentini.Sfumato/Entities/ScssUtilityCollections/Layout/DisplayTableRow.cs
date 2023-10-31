namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class DisplayTableRow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-row";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "display: table-row;"
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