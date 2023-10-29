namespace Argentini.Sfumato.ScssUtilityCollections.Effects;

public class Opacity : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "opacity";

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
        
        // Color preset (e.g. opacity-10)
        if (cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions.TryGetValue(cssSelector.CoreSegment, out var value))
            return $"opacity: {value};";

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (cssSelector.ArbitraryValueType == "number")
            return $"opacity: {cssSelector.ArbitraryValue};";
        
        #endregion

        return string.Empty;
    }
}