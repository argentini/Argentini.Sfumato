namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RoundedB : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix { get; set; } = "rounded-b";
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.RoundedOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   border-bottom-left-radius: {size};
                   border-bottom-right-radius: {size};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                    border-bottom-left-radius: {cssSelector.ArbitraryValue};
                    border-bottom-right-radius: {cssSelector.ArbitraryValue};
                    """;
        
        #endregion

        return string.Empty;
    }
}