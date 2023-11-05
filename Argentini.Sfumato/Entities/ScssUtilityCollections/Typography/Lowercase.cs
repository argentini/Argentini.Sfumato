namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Typography;

public class Lowercase : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "lowercase";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.LowercaseStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities

        if (ProcessStaticDictionaryOptions(cssSelector.AppState.LowercaseStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion

        return string.Empty;
    }
}