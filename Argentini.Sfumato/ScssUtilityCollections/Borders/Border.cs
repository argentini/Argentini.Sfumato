namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class Border : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border";

    public readonly Dictionary<string, string> StaticUtilities = new()
    {
        ["solid"] = "border-style: solid;",
        ["dashed"] = "border-style: dashed;",
        ["dotted"] = "border-style: dotted;",
        ["double"] = "border-style: double;",
        ["hidden"] = "border-style: hidden;",
        ["none"] = "border-style: none;"
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
        
        // Color preset (e.g. border-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"border-color: {color};";

        // Value preset (e.g. border-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"border-width: {size};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"border-color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"border-width: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"border-style: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}