namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class OverscrollY : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "overscroll-y";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.OverscrollYStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.OverscrollYStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}