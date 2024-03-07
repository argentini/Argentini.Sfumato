namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class Border : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BorderStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.BorderWidthOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.BorderStaticUtilities, cssSelector, AppState, out Result))
            return Result;

        #endregion

        #region Modifier Utilities

        if (ProcessColorModifierOptions(cssSelector, "border-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "border-color: {value};", AppState, out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "border-width: {value};", AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "border-color: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "border-width: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "border-style: {value};", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}