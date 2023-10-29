namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropSaturate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-saturate";

    public override void Initialize(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        foreach (var corePrefix in appState.EffectsFiltersOneBasedPercentageOptions.Keys)
            SelectorIndex.Add($"{SelectorPrefix}-{corePrefix}");
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions.TryGetValue(cssSelector.CoreSegment, out var value))
            return $"backdrop-filter: saturate({value});";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType is "number")
            return $"backdrop-filter: saturate({cssSelector.ArbitraryValue});";
        
        #endregion

        return string.Empty;
    }
}