namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class Ps : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ps";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "padding-inline-start: 0px;",
        ["px"] = "padding-inline-start: 1px;",
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
            return $"padding-inline-start: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"padding-inline-start: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}