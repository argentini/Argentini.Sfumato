namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Interactivity;

public class ScrollMy : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-my";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ScrollMyStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ScrollMyStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector,
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """, AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}