namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class BackdropBrightness : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-brightness";

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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions, cssSelector, "backdrop-filter: brightness({value});", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("number", cssSelector, "backdrop-filter: brightness({value});", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}