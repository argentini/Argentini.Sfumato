namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Bottom : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "bottom";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BottomStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
        await AddToIndexAsync(appState.FractionDividendOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.BottomStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessFractionModifierOptions(cssSelector, "bottom: {value};", out Result))
            return Result;
        
        #endregion

        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "bottom: {value};", out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector, "bottom: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "bottom: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}