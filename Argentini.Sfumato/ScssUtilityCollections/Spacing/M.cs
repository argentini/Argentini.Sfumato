namespace Argentini.Sfumato.ScssUtilityCollections.Spacing;

public class M : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "m";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "margin: auto;",
        ["0"] = "margin: 0px;",
        ["px"] = "margin: 1px;",
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
            return $"margin: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"margin: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}