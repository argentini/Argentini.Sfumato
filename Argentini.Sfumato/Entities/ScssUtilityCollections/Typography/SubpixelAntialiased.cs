namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class SubpixelAntialiased : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "subpixel-antialiased";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.SubpixelAntialiasedStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.SubpixelAntialiasedStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}