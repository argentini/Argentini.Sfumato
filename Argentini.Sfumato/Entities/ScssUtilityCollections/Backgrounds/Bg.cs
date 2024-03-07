namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Backgrounds;

public class Bg : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "bg";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BgStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.BgStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "background-color: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "background-color: {value};", AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "background-color: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "background-size: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues("url", cssSelector, "background-image: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "background-position: {value};", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}