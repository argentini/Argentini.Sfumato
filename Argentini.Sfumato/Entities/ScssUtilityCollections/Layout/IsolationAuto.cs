namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class IsolationAuto : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "isolation-auto";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.IsolationAutoStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.IsolationAutoStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}