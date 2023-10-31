namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class InsetX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inset-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                left: 0px;
                right: 0px;
                """,
        ["px"] = """
                 left: 1px;
                 right: 1px;
                 """,
        ["auto"] = """
                   left: auto;
                   right: auto;
                   """,
        ["full"] = """
                   left: 100%;
                   right: 100%;
                   """,
    };
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(StaticUtilities);

        await AddToIndexAsync(appState.LayoutRemUnitOptions);

        await AddToIndexAsync(appState.FractionDividendOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(StaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessFractionModifierOptions(cssSelector,
                """
                left: {value};
                right: {value};
                """, out Result))
            return Result;
        
        #endregion

        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector,
            """
            left: {value};
            right: {value};
            """, out Result))
            return Result;

        if (ProcessListOptions(cssSelector.AppState.FractionDividendOptions, cssSelector,
            """
            left: {value};
            right: {value};
            """, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            left: {value};
            right: {value};
            """, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}