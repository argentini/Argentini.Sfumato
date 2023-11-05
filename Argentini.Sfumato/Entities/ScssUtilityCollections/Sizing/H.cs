namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Sizing;

public class H : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "h";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.HStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
        await AddToIndexAsync(appState.FractionDividendOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.HStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessFractionModifierOptions(cssSelector, "height: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "height: {value};", out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector, "height: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "height: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}