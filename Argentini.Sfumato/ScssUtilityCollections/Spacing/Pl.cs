namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Pl : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "pl";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "padding-left: 0px;",
        ["px"] = "padding-left: 1px;",
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
            return $"padding-left: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"padding-left: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}