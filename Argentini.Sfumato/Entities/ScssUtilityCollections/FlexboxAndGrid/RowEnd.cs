namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class RowEnd : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "row-end";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.RowEndStaticUtilities);
        await AddToIndexAsync(appState.FlexboxAndGridWholeNumberOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.RowEndStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.FlexboxAndGridWholeNumberOptions, cssSelector, "grid-row-end: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer", cssSelector, "grid-row-end: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}