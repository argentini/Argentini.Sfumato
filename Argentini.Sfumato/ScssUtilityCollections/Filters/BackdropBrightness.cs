namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropBrightness : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-brightness";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. brightness-1.0)
        if (cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions.TryGetValue(cssSelector.CoreSegment, out var value))
            return $"backdrop-filter: brightness({value});";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "number")
            return $"backdrop-filter: brightness({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}