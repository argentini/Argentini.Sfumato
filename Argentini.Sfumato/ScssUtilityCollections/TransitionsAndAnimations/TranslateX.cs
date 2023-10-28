namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class TranslateX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "translate-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: translateX(0px);",
        ["px"] = "transform: translateX(1px);",
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
            return $"transform: translateX({unit});";

        if (cssSelector.AppState.VerbatimFractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"transform: translateX({fraction});";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"transform: translateX({cssSelector.ArbitraryValue});";
      
        #endregion

        return string.Empty;
    }
}