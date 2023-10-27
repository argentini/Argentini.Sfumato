namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Right : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "right";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "right: 0px;",
        ["px"] = "right: 1px;",
        ["auto"] = "right: auto;",
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
            return $"right: {unit};";

        if (cssSelector.AppState.VerbatimFractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"right: {fraction};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"right: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}