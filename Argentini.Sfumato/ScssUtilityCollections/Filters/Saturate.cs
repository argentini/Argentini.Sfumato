namespace Argentini.Sfumato.ScssUtilityCollections.Filters;

public class Saturate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "saturate";

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
        
        if (ProcessDictionaryOptions(cssSelector.AppState.EffectsFiltersOneBasedPercentageOptions, cssSelector, "filter: saturate({value});", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("number", cssSelector, "filter: saturate({value});", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}