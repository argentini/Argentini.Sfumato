namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class RowAuto : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "row-auto";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        [""] = "grid-row: auto;",
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