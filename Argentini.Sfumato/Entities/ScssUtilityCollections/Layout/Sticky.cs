namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class Sticky : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "sticky";

    public SfumatoAppState? AppState { get; set; }

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        AppState = appState;
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.StickyStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.StickyStaticUtilities, cssSelector, AppState, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}