namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class AutoCols : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "auto-cols";
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.AutoColsStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.AutoColsStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "grid-auto-columns: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}