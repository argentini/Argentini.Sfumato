namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RingOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring-offset";
    public override string Category => "ring";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. border-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"""
                   --sf-ring-offset-color: {color};
                   box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                   """;

        // Value preset (e.g. border-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   --sf-ring-offset-width: {size};
                   box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"""
                   --sf-ring-offset-color: {cssSelector.ArbitraryValue};
                   box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                   """;

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   --sf-ring-offset-width: {cssSelector.ArbitraryValue};
                   box-shadow: 0 0 0 var(--sf-ring-offset-width) var(--sf-ring-offset-color), var(--sf-ring-shadow);
                   """;
        
        #endregion

        return string.Empty;
    }
}