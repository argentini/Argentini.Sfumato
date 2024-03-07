namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class Contrast : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "contrast";
    public override string Category => "filter";
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);
        SelectorSort = 2;

        await AddToIndexAsync(appState.EffectsFiltersOneBasedPercentageOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions, cssSelector, "--sf-contrast: contrast({value});", AppState, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("number", cssSelector, "--sf-contrast: contrast({value});", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}