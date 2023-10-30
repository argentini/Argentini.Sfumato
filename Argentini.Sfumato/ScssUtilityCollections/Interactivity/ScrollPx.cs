namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class ScrollPx : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-px";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                scroll-padding-left: 0;
                scroll-padding-right: 0;
                """,
        ["px"] = """
                 scroll-padding-left: 1px;
                 scroll-padding-right: 1px;
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
            scroll-padding-left: {value};
            scroll-padding-right: {value};
            """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            scroll-padding-left: {value};
            scroll-padding-right: {value};
            """, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}