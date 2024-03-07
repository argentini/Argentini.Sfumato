namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Effects;

public class Shadow : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "shadow";
    public override string Category => "shadow";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ShadowStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ShadowStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "--sf-shadow-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "--sf-shadow-color: {value};", AppState, out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "--sf-shadow-color: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "box-shadow: {value};", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}