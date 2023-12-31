namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class TextEllipsis : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "text-ellipsis";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.TextEllipsisStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.TextEllipsisStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}