namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class Ring : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "ring";
    public override string Category => "ring";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. border-rose-100)
        if (cssSelector.AppState.ColorOptions.TryGetValue(cssSelector.CoreSegment, out var color))
            return $"--sf-ring-color: {color};";

        // Value preset (e.g. border-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"box-shadow: var(--sf-ring-inset) 0 0 0 calc({size} + var(--sf-ring-offset-width)) var(--sf-ring-color);";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "color")
            return $"--sf-ring-color: {cssSelector.ArbitraryValue};";

        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"box-shadow: var(--sf-ring-inset) 0 0 0 calc({cssSelector.ArbitraryValue} + var(--sf-ring-offset-width)) var(--sf-ring-color);";
        
        #endregion

        return string.Empty;
    }
}