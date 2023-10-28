namespace Argentini.Sfumato.ScssUtilityCollections.Typography;

public class Leading : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "leading";

    public override void Initialize(SfumatoAppState appState)
    {
        Selectors.Add(SelectorPrefix);

        foreach (var corePrefix in appState.LeadingOptions.Keys)
            Selectors.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.LeadingOptions.TryGetValue(cssSelector.CoreSegment, out var unit))
            return $"line-height: {unit};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "length" or "integer" or "number")
            return $"line-height: {cssSelector.ArbitraryValue};";

        #endregion

        return string.Empty;
    }
}