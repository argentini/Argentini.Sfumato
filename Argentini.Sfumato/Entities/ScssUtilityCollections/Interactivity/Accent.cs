namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Interactivity;

public class Accent : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "accent";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ColorOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "accent-color: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "accent-color: {value};", AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "accent-color: {value};", AppState, out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}