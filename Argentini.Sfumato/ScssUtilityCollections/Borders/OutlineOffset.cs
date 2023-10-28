namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class OutlineOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "outline-offset";

    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in appState.BorderWidthOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        // Value preset (e.g. outline-offset-2)
        if (cssSelector.AppState.BorderWidthOptions.TryGetValue(cssSelector.CoreSegment, out var size))
            return $"outline-offset: {size};";
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "percentage")
            return $"outline-offset: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}