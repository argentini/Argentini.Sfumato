namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class BorderL : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-l";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. border-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"border-left-color: {color};";

        // Value preset (e.g. border-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"border-left-width: {size};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"border-left-color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"border-left-width: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"border-left-style: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}