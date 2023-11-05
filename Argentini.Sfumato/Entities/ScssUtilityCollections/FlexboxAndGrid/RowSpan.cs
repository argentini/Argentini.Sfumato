namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class RowSpan : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "row-span";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.RowSpanStaticUtilities);
        await AddToIndexAsync(appState.FlexboxAndGridWholeNumberOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.RowSpanStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.FlexboxAndGridWholeNumberOptions, cssSelector, "grid-row: span {value} / span {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "grid-row: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}