namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RoundedL : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix { get; set; } = "rounded-l";
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Value preset (e.g. rounded-lg)
        if (cssSelector.AppState.RoundedOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   border-top-left-radius: {size};
                   border-bottom-left-radius: {size};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                    border-top-left-radius: {cssSelector.ArbitraryValue};
                    border-bottom-left-radius: {cssSelector.ArbitraryValue};
                    """;
        
        #endregion

        return string.Empty;
    }
}