namespace Argentini.Sfumato.ScssUtilityCollections.FlexboxAndGrid;

public class GridRows : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "grid-rows";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "grid-template-rows: none;",
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
        
        // Value preset (e.g. basis-0.5)
        if (cssSelector.AppState.FlexboxAndGridWholeNumberOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"grid-template-rows: repeat({unit}, minmax(0, 1fr));";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"grid-template-rows: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}