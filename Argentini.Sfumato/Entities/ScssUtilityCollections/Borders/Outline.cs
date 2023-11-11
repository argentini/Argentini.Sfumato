namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Borders;

public class Outline : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "outline";
    
    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.OutlineStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.BorderWidthOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.OutlineStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Modifier Utilities
        
        if (ProcessColorModifierOptions(cssSelector, "outline-color: {value};", out Result))
            return Result;

        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "outline-color: {value};", out Result))
            return Result;
        
        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "outline-width: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "outline-color: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues("length,percentage", cssSelector, "outline-width: {value};", out Result))
            return Result;

        if (ProcessArbitraryValues(string.Empty, cssSelector, "outline-style: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}