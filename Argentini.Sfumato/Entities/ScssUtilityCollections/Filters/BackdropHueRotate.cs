namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Filters;

public class BackdropHueRotate : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "backdrop-hue-rotate";
    
    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BackdropHueRotateStaticUtilities);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.BackdropHueRotateStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("angle", cssSelector, "backdrop-filter: hue-rotate({value});", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}