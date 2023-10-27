namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Mt : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "mt";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "margin-top: auto;",
        ["0"] = "margin-top: 0px;",
        ["px"] = "margin-top: 1px;",
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
            return $"margin-top: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"margin-top: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}