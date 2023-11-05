namespace Argentini.Sfumato.Entities.ScssUtilityCollections.Layout;

public class DisplayTableHeaderGroup : ScssUtilityClassGroupBase 
{
    public override string SelectorPrefix => "table-header-group";

    public override async Task InitializeAsync(SfumatoAppState appState)
    {
        SelectorIndex.Add(SelectorPrefix);

        await AddToIndexAsync(appState.DisplayTableHeaderGroupStaticUtilities);
    }

    public override string GetStyles(CssSelector cssSelector)
    {
        if (cssSelector.AppState is null)
            return string.Empty;
        
        #region Static Utilities
        
        if (ProcessStaticDictionaryOptions(cssSelector.AppState.DisplayTableHeaderGroupStaticUtilities, cssSelector, out Result))
            return Result;
        
        #endregion
        
        return string.Empty;
    }
}