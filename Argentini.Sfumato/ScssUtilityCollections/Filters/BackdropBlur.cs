namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropBlur : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-blur";

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Color preset (e.g. blur-xl)
        if (cssSelector.AppState.FilterSizeOptions.TryGetValue(cssSelector.CoreSegment, out var value))
            return $"backdrop-filter: blur({value});";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "length")
            return $"backdrop-filter: blur({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}