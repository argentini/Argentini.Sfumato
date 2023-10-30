namespace Argentini.Sfumato.ScssUtilityCollections.Borders;

public class RoundedSe : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix { get; set; } = "rounded-se";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.RoundedOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.RoundedOptions, cssSelector, "border-start-end-radius: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("length,percentage", cssSelector, "border-start-end-radius: {value};", out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}