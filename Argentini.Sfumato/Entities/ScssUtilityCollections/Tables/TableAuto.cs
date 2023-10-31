namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Tables;

public class TableAuto : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-auto";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "table-layout: auto;",
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