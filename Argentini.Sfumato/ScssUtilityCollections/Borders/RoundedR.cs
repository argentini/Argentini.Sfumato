namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RoundedR : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix { get; set; } = "rounded-r";
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Value preset (e.g. rounded-lg)
        if (cssSelector.AppState.RoundedOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   border-top-right-radius: {size};
                   border-bottom-right-radius: {size};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                    border-top-right-radius: {cssSelector.ArbitraryValue};
                    border-bottom-right-radius: {cssSelector.ArbitraryValue};
                    """;
        
        #endregion

        return string.Empty;
    }
}