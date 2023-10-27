namespace Argentini.Sfumato.ScssUtilityCollections.Tables;

public class BorderSpacingY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-spacing-y";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "border-spacing: var(--sf-border-spacing-x) 0px;",
        ["px"] = "border-spacing: var(--sf-border-spacing-x) 1px;",
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
            return $"border-spacing: var(--sf-border-spacing-x) {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"border-spacing: var(--sf-border-spacing-x) {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}