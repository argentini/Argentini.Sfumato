namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Decoration : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "decoration";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DecorationStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DecorationStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "text-decoration-color: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "text-decoration-color: {value};", AppState, out Result))
            return Result;
        
        #endregion

        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "text-decoration-color: {value};", AppState, out Result))
            return Result;

        if (ProcessArbitraryValues("length", cssSelector, "text-decoration-thickness: {value};", AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}