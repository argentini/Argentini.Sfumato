namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Backgrounds;

public class From : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "from";
    public override string Category => "gradients";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.FromStaticUtilities);
        await AddToIndexAsync(appState.ColorOptions);
        await AddToIndexAsync(appState.PercentageOptions);
    }
    
    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.FromStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion

        #region Calculated Utilities
        
        if (ProcessDictionaryOptions(cssSelector.AppState.ColorOptions, cssSelector,
                """
                --sf-gradient-from: {value} var(--sf-gradient-from-position);
                --sf-gradient-to: transparent var(--sf-gradient-to-position);
                --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
                """, AppState, out Result))
            return Result;

        if (ProcessDictionaryOptions(cssSelector.AppState.PercentageOptions, cssSelector, "--sf-gradient-from-position: {value};", AppState, out Result))
            return Result;
        
        #endregion
        
        #region Arbitrary Values
        
        if (cssSelector is not { HasArbitraryValue: true, CoreSegment: "" })
            return string.Empty;
        
        if (ProcessArbitraryValues("color", cssSelector,
            """
            --sf-gradient-from: {value} var(--sf-gradient-from-position);
            --sf-gradient-to: transparent var(--sf-gradient-to-position);
            --sf-gradient-stops: var(--sf-gradient-from), var(--sf-gradient-to);
            """, AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}