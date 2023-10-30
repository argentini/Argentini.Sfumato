namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class OutlineOffset : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "outline-offset";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.BorderWidthOptions);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.BorderWidthOptions, cssSelector, "outline-offset: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "outline-offset: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}