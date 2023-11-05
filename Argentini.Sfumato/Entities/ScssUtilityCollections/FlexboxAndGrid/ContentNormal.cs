namespace Argentini.Sfumato.Entities.ScssUtilityCollections.FlexboxAndGrid;

public class ContentNormal : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "content-normal";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.ContentNormalStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;

        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.ContentNormalStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}