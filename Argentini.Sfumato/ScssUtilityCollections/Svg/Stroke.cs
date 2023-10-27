namespace Argentini.Sfumato.ScssUtilityCollections.Svg;

public class Stroke : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "stroke";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["none"] = "stroke: none;",
        ["0"] = "stroke-width: 0;",
        ["1"] = "stroke-width: 1;",
        ["2"] = "stroke-width: 2;",
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
            return $"stroke: {color};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"stroke: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "length" or "integer" or "number" or "percentage")
            return $"stroke-width: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}