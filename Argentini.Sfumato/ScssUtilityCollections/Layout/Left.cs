namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class Left : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "left";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "left: 0px;",
        ["px"] = "left: 1px;",
        ["auto"] = "left: auto;",
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
            return $"left: {unit};";

        if (cssSelector.AppState.VerbatimFractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"left: {fraction};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"left: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}