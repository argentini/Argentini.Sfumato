namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class GapX : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "gap-x";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["0"] = "column-gap: auto;",
        ["px"] = "column-gap: min-content;",
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
        
        // Value preset (e.g. gap-x-0.5)
        if (cssSelector.AppState.LayoutRemUnitOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"column-gap: {unit};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"column-gap: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}