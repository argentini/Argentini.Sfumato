namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

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
    };
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.VerbatimFractionOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
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
            left: {value};
            right: {value};
            """, out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.VerbatimFractionOptions, cssSelector,
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