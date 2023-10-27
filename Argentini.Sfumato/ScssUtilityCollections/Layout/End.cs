namespace Argentini.Sfumato.ScssUtilityCollections.Layout;

public class End : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "end";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "inset-inline-end: 0px;",
        ["px"] = "inset-inline-end: 1px;",
        ["auto"] = "inset-inline-end: auto;",
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
            return $"inset-inline-end: {unit};";

        if (cssSelector.AppState.VerbatimFractionOptions.TryGetValue(cssSelector.CoreSegment, out var fraction))
            return $"inset-inline-end: {fraction};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"inset-inline-end: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}