namespace Argentini.Sfumato.ScssUtilityCollections.TransitionsAndAnimations;

public class TranslateY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "translate-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "transform: translateY(0px);",
        ["px"] = "transform: translateY(1px);",
    }; 
    
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
            return $"transform: translateY({unit});";

        if (cssSelector.AppState.VerbatimFractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"transform: translateY({fraction});";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"transform: translateY({cssSelector.ArbitraryValue});";
      
        #endregion

        return string.Empty;
    }
}