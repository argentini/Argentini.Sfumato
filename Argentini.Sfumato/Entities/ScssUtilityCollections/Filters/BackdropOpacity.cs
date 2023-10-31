namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class BackdropOpacity : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-opacity";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.EffectsFiltersOneBasedPercentageOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions, cssSelector, "backdrop-filter: opacity({value});", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("number", cssSelector, "backdrop-filter: opacity({value});", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}