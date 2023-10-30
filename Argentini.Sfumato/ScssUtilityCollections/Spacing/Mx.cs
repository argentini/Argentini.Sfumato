namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Mx : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "mx";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                margin-left: 0px;
                margin-right: 0px;
                """,
        ["px"] = """
                   margin-left: 1px;
                   margin-right: 1px;
                   """,
        ["auto"] = """
                   margin-left: auto;
                   margin-right: auto;
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
            margin-left: {value};
            margin-right: {value};
            """, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            margin-left: {value};
            margin-right: {value};
            """, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}