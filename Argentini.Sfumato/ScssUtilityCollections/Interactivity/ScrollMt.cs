namespace Argentini.Sfumato.ScssUtilityCollections.Interactivity;

public class ScrollMt : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "scroll-mt";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "scroll-margin-top: 0;",
        ["px"] = "scroll-margin-top: 1px;",
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
            return $"scroll-margin-top: {unit};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"scroll-margin-top: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}