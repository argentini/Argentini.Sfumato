namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class BorderL : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "border-l";

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

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "border-left-color: {value};", out Result))
            return Result;
        
        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "border-left-width: {value};", out Result))
            return Result;
        
        #endregion

        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "border-left-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("color", cssSelector, "border-left-color: {value};", out Result))
            return Result;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "border-left-width: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "border-left-style: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}