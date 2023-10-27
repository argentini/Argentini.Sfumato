namespace Argentini.Sfumato.ScssUtilityCollections.Svg;

public class Fill : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "fill";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "fill: none;",
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
        
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"fill: {color};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"fill: {cssSelector.ArbitraryValue};";
      
        #endregion

        return string.Empty;
    }
}