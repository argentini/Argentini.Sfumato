namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Aspect : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "aspect";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.AspectStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.AspectStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("ratio", cssSelector, "aspect-ratio: {value};", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}