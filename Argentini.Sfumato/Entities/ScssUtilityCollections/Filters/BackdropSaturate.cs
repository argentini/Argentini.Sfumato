namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class BackdropSaturate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-saturate";
    public override string Category => "backdrop";
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);
        SelectorSort = 7;

        await AddToIndexAsync(appState.EffectsFiltersOneBasedPercentageOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions, cssSelector, "--sf-backdrop-saturate: saturate({value});", AppState, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("number", cssSelector, "--sf-backdrop-saturate: saturate({value});", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}