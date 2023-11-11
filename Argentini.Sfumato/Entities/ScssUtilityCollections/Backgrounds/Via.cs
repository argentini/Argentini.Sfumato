namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Backgrounds;

public class Via : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "via";
    public override string Category => "gradients";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ViaStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.PercentageOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ViaStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector,
                """
                --sf-gradient-to: transparent var(--sf-gradient-to-position);
                --sf-gradient-stops: var(--sf-gradient-from), {value} var(--sf-gradient-via-position), var(--sf-gradient-to);
                """, out Result))
            return Result;
        
        if (ProcessDictionaryOptions(cssSelector.AppState.PercentageOptions, cssSelector, "--sf-gradient-via-position: {value};", out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;

        if (ProcessArbitraryValues("color", cssSelector,
            """
            --sf-gradient-to: transparent var(--sf-gradient-to-position);
            --sf-gradient-stops: var(--sf-gradient-from), {value} var(--sf-gradient-via-position), var(--sf-gradient-to);
            """, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}