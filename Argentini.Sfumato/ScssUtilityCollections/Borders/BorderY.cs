namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class BorderY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-y";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. border-x-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"""
                   border-top-color: {color};
                   border-bottom-color: {color};
                   """;

        // Value preset (e.g. border-x-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   border-top-width: {size};
                   border-bottom-width: {size};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"""
                   border-top-color: {cssSelector.ArbitraryValue};
                   border-bottom-color: {cssSelector.ArbitraryValue};
                   """;

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   border-top-width: {cssSelector.ArbitraryValue};
                   border-bottom-width: {cssSelector.ArbitraryValue};
                   """;

        if (cssSelector.ArbitraryValueType == string.Empty)
            return $"""
                    border-top-style: {cssSelector.ArbitraryValue};
                    border-bottom-style: {cssSelector.ArbitraryValue};
                    """;
        
        #endregion

        return string.Empty;
    }
}