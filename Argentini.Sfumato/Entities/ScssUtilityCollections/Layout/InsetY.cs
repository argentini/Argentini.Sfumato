namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class InsetY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inset-y";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.InsetYStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
        await AddToIndexAsync(appState.FractionDividendOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.InsetYStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessFractionModifierOptions(cssSelector,
                """
                top: {value};
                bottom: {value};
                """, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector,
            """
            top: {value};
            bottom: {value};
            """, out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector,
            """
            top: {value};
            bottom: {value};
            """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            top: {value};
            bottom: {value};
            """, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}