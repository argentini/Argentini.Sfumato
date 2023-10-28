namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Top : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "top";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "top: 0px;",
        ["px"] = "top: 1px;",
        ["auto"] = "top: auto;",
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.VerbatimFractionOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.LayoutRemUnitOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"top: {unit};";

        if (cssSelector.AppState.VerbatimFractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"top: {fraction};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"top: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}