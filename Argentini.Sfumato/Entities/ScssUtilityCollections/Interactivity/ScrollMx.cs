namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Interactivity;

public class ScrollMx : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-mx";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ScrollMxStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ScrollMxStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector,
            """
            scroll-margin-left: {value};
            scroll-margin-right: {value};
            """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            scroll-margin-left: {value};
            scroll-margin-right: {value};
            """, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}