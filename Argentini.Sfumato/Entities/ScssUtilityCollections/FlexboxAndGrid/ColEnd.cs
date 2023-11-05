namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class ColEnd : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "col-end";
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ColEndStaticUtilities);

        await AddToIndexAsync(appState.FlexboxAndGridWholeNumberOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ColEndStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.FlexboxAndGridWholeNumberOptions, cssSelector, "grid-column-end: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer", cssSelector, "grid-column-end: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}