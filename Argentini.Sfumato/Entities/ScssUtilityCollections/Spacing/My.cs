namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Spacing;

public class My : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "my";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.MyStaticUtilities);
        await AddToIndexAsync(appState.LayoutRemUnitOptions);
    }

    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.MyStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector,
            """
            margin-top: {value};
            margin-bottom: {value};
            """, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            margin-top: {value};
            margin-bottom: {value};
            """, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}