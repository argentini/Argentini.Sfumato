namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class Contrast : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "contrast";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. brightness-1.0)
        if (cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions.TryGetValue(cssSelector.CoreSegment, out var value))
            return $"filter: contrast({value});";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "number")
            return $"filter: contrast({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}