namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class ContentBetween : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-between";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ContentBetweenStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ContentBetweenStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}