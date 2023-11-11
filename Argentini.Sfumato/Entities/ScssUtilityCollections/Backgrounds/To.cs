namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Backgrounds;

public class To : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "to";
    public override string Category => "gradients";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ToStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.PercentageOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ToStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities

        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector, "--sf-gradient-to: {value} var(--sf-gradient-to-position);", out Result))
            return Result;
        
        if (ProcessDictionaryOptions(cssSelector.AppState.PercentageOptions, cssSelector, "--sf-gradient-to-position: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector, "--sf-gradient-to: {value} var(--sf-gradient-to-position);", out Result))
            return Result;

        if (ProcessArbitraryValues("percentage", cssSelector, "--sf-gradient-to-position: {value};", out Result))
            return Result;

        #endregion

        return string.Empty;
    }
}