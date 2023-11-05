namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Left : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "left";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.LeftStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
        await AddToIndexAsync(appState.FractionDividendOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.LeftStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessFractionModifierOptions(cssSelector, "left: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "left: {value};", out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector, "left: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "left: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}