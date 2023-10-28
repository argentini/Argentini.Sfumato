namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RoundedBl : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix { get; set; } = "rounded-bl";

    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in appState.RoundedOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.RoundedOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"border-bottom-left-radius: {size};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"border-bottom-left-radius: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}