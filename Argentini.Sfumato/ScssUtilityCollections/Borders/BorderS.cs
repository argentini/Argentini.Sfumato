namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class BorderS : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-s";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ColorOptions);
        
        await AddToIndexAsync(appState.BorderWidthOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "border-inline-start-color: {value};", out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "border-inline-start-width: {value};", out Result))
            return Result;
        
        #endregion

        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "border-inline-start-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("color", cssSelector, "border-inline-start-color: {value};", out Result))
            return Result;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "border-inline-start-width: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "border-inline-start-style: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}