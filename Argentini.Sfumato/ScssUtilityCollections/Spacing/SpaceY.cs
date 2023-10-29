namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class SpaceY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "space-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = """
                & > * + * {
                    margin-top: 0px;
                }
                """,
        ["px"] = """
                 & > * + * {
                     margin-top: 1px;
                 }
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
            & > * + * {
                margin-top: {value};
            }
            """, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector,
            """
            & > * + * {
                margin-top: {value};
            }
            """, out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}