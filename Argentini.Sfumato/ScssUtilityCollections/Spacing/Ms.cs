namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Ms : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ms";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "margin-inline-start: auto;",
        ["0"] = "margin-inline-start: 0px;",
        ["px"] = "margin-inline-start: 1px;",
    }; 
    
    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in StaticUtilities.Keys.Where(k => k != string.Empty))
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");

        foreach (var corePrefix in appState.LayoutRemUnitOptions.Keys)
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
            return $"margin-inline-start: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"margin-inline-start: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}