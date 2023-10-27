namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class RowStart : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "row-start";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "grid-row-start: auto;",
    }; 
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        // Static utilities (e.g. flex)
        if (StaticUtilities.TryGetValue(cssSelector.CoreSegment, out var styles))
            return styles;
        
        #endregion
        
        #region Calculated Utilities
        
        // Value preset (e.g. col-start-1)
        if (cssSelector.AppState.FlexboxAndGridWholeNumberOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"grid-row-start: {unit};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "integer")
            return $"grid-row-start: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}