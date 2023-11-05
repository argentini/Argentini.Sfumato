namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class AutoRows : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "auto-rows";
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.AutoRowsStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.AutoRowsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "grid-auto-rows: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}