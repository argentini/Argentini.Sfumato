namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class ColStart : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "col-start";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["auto"] = "grid-column-start: auto;",
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
            return $"grid-column-start: {unit};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "integer")
            return $"grid-column-start: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}