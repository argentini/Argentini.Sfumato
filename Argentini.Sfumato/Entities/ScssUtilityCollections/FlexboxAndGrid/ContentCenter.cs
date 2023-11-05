namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class ContentCenter : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-center";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ContentCenterStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ContentCenterStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}