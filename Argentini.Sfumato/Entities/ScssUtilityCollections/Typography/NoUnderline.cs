namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class NoUnderline : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "no-underline";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.NoUnderlineStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.NoUnderlineStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}