namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class Sepia : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "sepia";
    public override string Category => "filter";
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);
        SelectorSort = 8;

        await AddToIndexAsync(appState.SepiaStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.SepiaStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("percentage", cssSelector, "--sf-sepia: sepia({value});", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}