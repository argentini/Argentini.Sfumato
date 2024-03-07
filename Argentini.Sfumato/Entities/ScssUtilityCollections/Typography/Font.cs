namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Font : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "font";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.FontStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.FontStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("integer", cssSelector, "font-weight: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "font-family: {value};", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}