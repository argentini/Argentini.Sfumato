namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Start : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "start";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.StartStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
        await AddToIndexAsync(appState.FractionDividendOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.StartStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessFractionModifierOptions(cssSelector, "inset-inline-start: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "inset-inline-start: {value};", out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector, "inset-inline-start: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "inset-inline-start: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}