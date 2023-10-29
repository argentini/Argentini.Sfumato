namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RoundedE : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix { get; set; } = "rounded-e";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.RoundedOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.RoundedOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"""
                   border-start-end-radius: {size};
                   border-end-end-radius: {size};
                   """;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"""
                   border-start-end-radius: {cssSelector.ArbitraryValue};
                   border-end-end-radius: {cssSelector.ArbitraryValue};
                   """;
        
        #endregion

        return string.Empty;
    }
}