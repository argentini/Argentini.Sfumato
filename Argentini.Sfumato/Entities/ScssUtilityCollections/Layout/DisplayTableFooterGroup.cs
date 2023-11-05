namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class DisplayTableFooterGroup : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-footer-group";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DisplayTableFooterGroupStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DisplayTableFooterGroupStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}