namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class ScrollMy : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-my";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                scroll-margin-top: 0;
                scroll-margin-bottom: 0;
                """,
        ["px"] = """
                 scroll-margin-top: 1px;
                 scroll-margin-bottom: 1px;
                 """,
    }; 
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.LayoutRemUnitOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector,
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            scroll-margin-top: {value};
            scroll-margin-bottom: {value};
            """, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}