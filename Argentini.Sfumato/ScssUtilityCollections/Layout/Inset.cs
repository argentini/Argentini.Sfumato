namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Inset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "inset";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "inset: 0px;",
        ["px"] = "inset: 1px;",
        ["auto"] = "inset: auto;",
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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.LayoutRemUnitOptions, cssSelector, "inset: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.VerbatimFractionOptions, cssSelector, "inset: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "inset: {value};", out Result))
            return Result;
      
        #endregion

        return string.Empty;
    }
}