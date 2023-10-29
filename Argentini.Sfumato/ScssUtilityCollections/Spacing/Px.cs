namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Px : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "px";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                padding-left: 0px;
                padding-right: 0px;
                """,
        ["px"] = """
                 padding-left: 1px;
                 padding-right: 1px;
                 """,
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
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
            padding-left: {value};
            padding-right: {value};
            """, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            padding-left: {value};
            padding-right: {value};
            """, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}