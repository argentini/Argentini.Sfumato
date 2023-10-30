namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class My : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "my";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                margin-top: 0px;
                margin-bottom: 0px;
                """,
        ["px"] = """
                 margin-top: 1px;
                 margin-bottom: 1px;
                 """,
        ["auto"] = """
                 margin-top: auto;
                 margin-bottom: auto;
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