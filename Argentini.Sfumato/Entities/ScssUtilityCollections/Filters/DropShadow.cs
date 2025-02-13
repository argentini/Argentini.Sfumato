namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class DropShadow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "drop-shadow";
    public override string Category => "filter";
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);
        SelectorSort = 3;

        await AddToIndexAsync(appState.DropShadowStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DropShadowStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues(string.Empty, cssSelector, "--sf-drop-shadow: drop-shadow({value});", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}