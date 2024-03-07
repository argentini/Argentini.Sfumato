namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class HueRotate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "hue-rotate";
    public override string Category => "filter";
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);
        SelectorSort = 5;

        await AddToIndexAsync(appState.HueRotateStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.HueRotateStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("angle", cssSelector, "--sf-hue-rotate: hue-rotate({value});", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}