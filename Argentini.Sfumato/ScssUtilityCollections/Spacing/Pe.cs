namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Pe : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "pe";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "padding-inline-end: 0px;",
        ["px"] = "padding-inline-end: 1px;",
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
            return $"padding-inline-end: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"padding-inline-end: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}