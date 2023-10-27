namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class Blur : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "blur";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. blur-xl)
        if (cssSelector.AppState.FilterSizeOptions.TryGetValue(cssSelector.CoreSegment, out var value))
            return $"filter: blur({value});";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"filter: blur({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}